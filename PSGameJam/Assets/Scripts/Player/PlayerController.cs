using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using TMPro;



public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float wallet;

    const float ISO_X_DIAGNOL_DIR = 0.894427f;
    const float ISO_Y_DIAGNOL_DIR = 0.447214f;

    [SerializeField]
    private float waterTankLevel = 100.0f;

    [SerializeField]
    private float laserCooldown;
    [SerializeField]
    private float shotgunCooldown;
    [SerializeField]
    private float fireHoseCooldown;
    [SerializeField]
    private float harvestBladeCooldown;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jetSpeed;
    [SerializeField]
    private float jetOffsetSpeed;
    [SerializeField]
    private float overHeatRate;
    [SerializeField]
    private float cooldownRate;
    [SerializeField] 
    private float screenShakeDuration;
    [SerializeField]
    private AnimationCurve screenShakeStrength;
    [SerializeField]
    private GameObject jetPackVFX;
    [SerializeField]
    private Vector2 jetVFXOffset;

    [SerializeField]
    private Vector2 FlamethrowerOffset;

    [SerializeField]
    private float changeDirectionTimer;

    [SerializeField]
    private InputActionReference playerInput;

    [SerializeField]
    private GameObject laserPrefab;

    [SerializeField]
    private GameObject shotgunPrefab;

    [SerializeField]
    private GameObject harvestBladePrefab;

    [SerializeField]
    private GameObject fireHosePrefab;
    public ScoreSystem scoreSystem;

    [SerializeField]
    private GameObject flameThrowerPrefab;

    [SerializeField]
    private float goTime;

    [SerializeField]
    private float stopTime;

    private String[] typesOfWeapons = {
        "none", // 0
        "lazer", // 1
        "shotgun", // 2
        "firehose", // 3
        "sword", // 4
        "flamethrower", // 5
    };

    public SeedLogic seeds;

    private float waterStartChargeTime;
    private Vector2 moveDirection;
    private Vector2 isoDirection;
    private Vector2 lastMoveDirection;
    private bool canMove;
    private bool usingWeapon;
    private bool usingJets;
    private bool jetLockdown;
    private bool flameThrowerLockdown;
    private bool releaseShotgun;

    private Rigidbody2D rig;

    private Animator playerAnimatior;

    [HideInInspector]
	public SFXController sfx;
	
	private FootStep foot;

    private string CurrentWeapon;

    private GameObject laserGO;
    private GameObject shotgunGO;
    private GameObject firehoseGO;
    private GameObject harvestBladeGO;
    private GameObject flameThrowerGO;
    private GameObject jetPackVFXGO;

    // Seed Count text mesh pro
    private TextMeshProUGUI sCount;

    private float laserCooldownTimer;
    private float shotgunCooldownTimer;
    private float fireHoseCooldownTimer;
    private float harvestBladeCooldownTimer;
    private float speedInterval;
    private float speedIntervalTimer;
    private float overheatVal;

    public float Wallet { get => wallet; set => wallet = value; }

    /*
        UI<>
        SSSC = Seed Selection Seed Count
        SSSC = Seed Selection Seed Type Icon
    */
    private GameObject UIM;

    private GameObject UISSSC;
    
    private void Awake() {
        wallet = 0.0f;
        canMove = true;
        usingWeapon = false;
        usingJets = false;
        jetLockdown = false;
        flameThrowerLockdown = false;
        speedIntervalTimer = 0.0f;
        overheatVal = 0.0f;
        lastMoveDirection = new Vector2(1, 1).normalized;
        playerAnimatior = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
		sfx = GetComponent<SFXController>();
		foot = GetComponent<FootStep>();
        seeds = GetComponent<SeedLogic>();
        jetPackVFXGO = Instantiate(jetPackVFX, transform);
        jetPackVFXGO.GetComponent<VisualEffect>().Stop();
        UIM = GameObject.FindGameObjectWithTag("UIMoney");
        UISSSC = GameObject.FindGameObjectWithTag("SeedSelectionSeedCount");
    }

    private void Update() {
        UIM.GetComponent<TMP_Text>().text = "Money: $" + Wallet;
        UISSSC.GetComponent<TMP_Text>().text = "" + seeds.GetSeedCount(seeds.currentSeed);
        ProcessOverHeat();
    }

    private void FixedUpdate() {
        if(usingJets)
        {
            Vector2 vel = rig.linearVelocity + (isoDirection * jetOffsetSpeed);
            if (vel.magnitude < jetSpeed)
                rig.linearVelocity = vel;
            else
                rig.linearVelocity = vel.normalized * jetSpeed;

            if (jetLockdown)
            {
                jetPackVFXGO.GetComponent<VisualEffect>().Stop();
                usingJets = false;
            }
        }
        else
        {
            rig.linearVelocity = isoDirection * speedInterval;
            
            playerAnimatior.SetFloat("Horizontal", lastMoveDirection.x);
            playerAnimatior.SetFloat("Vertical", lastMoveDirection.y);
            playerAnimatior.SetFloat("Magnitude", moveDirection.magnitude);
        }

        if (moveDirection.magnitude > 0)
        {
            if (speedIntervalTimer < Time.time)
            {
                if (speedInterval == 0) // Go Time
                {
                    speedInterval = moveSpeed;
                    speedIntervalTimer = Time.time + goTime;

                    if (usingJets == false)
                        sfx.playSound(8); //try playing footstep

                }
                else // Stop Time
                {
                    speedInterval = 0;
                    speedIntervalTimer = Time.time + stopTime;

                    if (usingJets == false)
                    {
                        var angle = Vector2.Angle(Vector2.left, lastMoveDirection);
                        // Flip if face positive direction
                        if (moveDirection.y > 0)
                            angle = -angle;


                        foot.makeStep(angle);

                        //Shake screen
                        StartCoroutine(ScreenShake());

                        if (foot.left)
                            foot.left = false;
                        else
                            foot.left = true;
                    }

                }
            }
        }
        else
        {
            speedInterval = moveSpeed;
            foot.left = true;
        }
    }

    private void ChangeDirection() {
        lastMoveDirection = moveDirection;

        if(usingJets)
        {
            Vector3 origin = transform.position + jetPackVFX.transform.position;
            var angle = Vector2.Angle(Vector2.left, lastMoveDirection);

            // Flip if face positive direction
            if (moveDirection.y > 0)
                angle = -angle;

            Vector3 rot_offset = TileManager.rotate(jetVFXOffset, angle);
            jetPackVFXGO.transform.position = origin + rot_offset;
        }

        if (flameThrowerGO != null)
        {
            var angle = Vector2.Angle(Vector2.left, lastMoveDirection);

            // Flip if face positive direction
            if (lastMoveDirection.y > 0)
                angle = -angle;

            flameThrowerGO.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
            Vector3 rot_offset = TileManager.rotate(FlamethrowerOffset, angle);
            Vector3 flameThrowerCenterOffset = new Vector3(0.0f, 0.4f, 0.0f);

            flameThrowerGO.transform.position = transform.position + flameThrowerCenterOffset + rot_offset;
        }
    }

    public void DetermineDirection(InputAction.CallbackContext context) {
        moveDirection = Vector2.zero;
        isoDirection = Vector2.zero;

        // Get direction of player and move
        if (context.performed && canMove)
        {
            moveDirection = playerInput.action.ReadValue<Vector2>();
            isoDirection = moveDirection;

            if (moveDirection.x > 0.0f && moveDirection.y > 0.0f)
                isoDirection = new Vector2(ISO_X_DIAGNOL_DIR, ISO_Y_DIAGNOL_DIR);
            else if (moveDirection.x > 0.0f && moveDirection.y < 0.0f)
                isoDirection = new Vector2(ISO_X_DIAGNOL_DIR, -ISO_Y_DIAGNOL_DIR);
            else if (moveDirection.x < 0.0f && moveDirection.y > 0.0f)
                isoDirection = new Vector2(-ISO_X_DIAGNOL_DIR, ISO_Y_DIAGNOL_DIR);
            else if (moveDirection.x < 0.0f && moveDirection.y < 0.0f)
                isoDirection = new Vector2(-ISO_X_DIAGNOL_DIR, -ISO_Y_DIAGNOL_DIR);


            Action changeDir = () =>
            {
                if(moveDirection != Vector2.zero && lastMoveDirection != moveDirection)
                    ChangeDirection();
            };

            TimerManager.AddTimer(changeDir, changeDirectionTimer);
        }
    }

    public void JetBoost(InputAction.CallbackContext context) {
        // Get direction of player and move
        if (context.performed && canMove && !jetLockdown)
        {
            rig.linearVelocity = moveDirection * jetSpeed;

            // Spawn and set up jetPackVFXGO
            
            var angle = Vector2.Angle(Vector2.left, moveDirection);

            jetPackVFXGO.GetComponent<VisualEffect>().Play();
            //PLAY THE JET SOUND :)
            sfx.playSound(0);
			
            // Flip if face positive direction
            if (moveDirection.y > 0)
                angle = -angle;

            Vector3 rot_offset = TileManager.rotate(jetVFXOffset, angle);
            jetPackVFXGO.transform.position = jetPackVFXGO.transform.position + rot_offset;

            usingJets = true;
        }
        else if(context.canceled && usingJets)
        {
            jetPackVFXGO.GetComponent<VisualEffect>().Stop();
            usingJets = false;
			
			//STOP THE JET SOUND. FLOAT IS HOW LONG TO QUIET IT DOWN. 
			sfx.stopSound(1.0f);
			foot.left = true;
        }

    }

    public void FireWeapon(InputAction.CallbackContext context) {
        // Creating statements to fire weapon based off of the current weapon.
        if (typesOfWeapons[0] == CurrentWeapon)
        {
            Debug.Log("Putting away items");
        }
        else if (typesOfWeapons[1] == CurrentWeapon && !usingJets)
        {
            if (context.action.name == "Cursor")
            {
                FireLaser(context);
            }
        }
        else if (typesOfWeapons[2] == CurrentWeapon && !usingJets && seeds.ShootSeed(0) != 0)
        {
            if (context.action.name == "Cursor")
            {
                FireShotGun(context);
                //Debug.Log(context.action.name);
            }

        }
        else if (typesOfWeapons[3] == CurrentWeapon && !usingJets)
        {
            if (context.action.name == "Cursor")
            {
                //Debug.Log("Spraying Water!");
                FireWaterHose(context);
            }
        }
        else if (typesOfWeapons[4] == CurrentWeapon)
        {
            if (context.action.name == "Cursor")
            {
                FireHarvestBlade(context);
                //Debug.Log("Swing Sword");
            }
        }
        else if (typesOfWeapons[5] == CurrentWeapon)
        {
            if (context.action.name == "Cursor")
            {
                FireFlameThrower(context);
            }
        }
    }
    public void FireFlameThrower(InputAction.CallbackContext context)
    {
        if (context.started && !usingWeapon && !flameThrowerLockdown)
        {
			sfx.playSound(0); //play flame opener sound 
			sfx.playSound(1); //play flame continuous sound
            flameThrowerGO = Instantiate(flameThrowerPrefab, transform);

            var angle = Vector2.Angle(Vector2.left, lastMoveDirection);

            // Flip if face positive direction
            if (lastMoveDirection.y > 0)
                angle = -angle;

            flameThrowerGO.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
            Vector3 rot_offset = TileManager.rotate(FlamethrowerOffset, angle);
            Vector3 flameThrowerCenterOffset = new Vector3(0.0f, 0.4f, 0.0f);

            flameThrowerGO.transform.position = transform.position + flameThrowerCenterOffset + rot_offset;

            usingWeapon = true;

        }
        else if (context.canceled && flameThrowerGO != null)
        {
            flameThrowerGO.GetComponent<FlameThrowerLogic>().StopFlame();
            usingWeapon = false;
			sfx.stopSound(0.5f); //quiet the flame continuous sound
        }
    }

    public void FireLaser(InputAction.CallbackContext context) {
        if (context.started && !usingWeapon && laserCooldownTimer < Time.time)
        {
            laserCooldownTimer = Time.time + laserCooldown;
            // Spawn aimer
            laserGO = Instantiate(laserPrefab, transform);
            laserGO.GetComponent<LaserLogic>().Charge(lastMoveDirection);
            // Stop Player
            moveDirection = Vector2.zero;
            isoDirection = Vector2.zero;
            canMove = false;
            usingWeapon = true;

        }
        else if (context.canceled && laserGO != null)
        {
            if(!laserGO.GetComponent<LaserLogic>().Fired())
			{
                laserGO.GetComponent<LaserLogic>().Fire();
				sfx.playSound(5);		//play laser sound 
			}
        }
    }

    public void FireShotGun(InputAction.CallbackContext context) {

        if (context.started && shotgunCooldownTimer < Time.time)
        {
            releaseShotgun = true;
            shotgunCooldownTimer = Time.time + shotgunCooldown;
            // Spawn aimer
            shotgunGO = Instantiate(shotgunPrefab, transform);
            shotgunGO.GetComponent<ShotgunLogic>().BulletSpread(lastMoveDirection);

            // Stop Player
            moveDirection = Vector2.zero;
            isoDirection = Vector2.zero;
            canMove = false;
            usingWeapon = true;

            playerAnimatior.SetFloat("Horizontal", lastMoveDirection.x);
            playerAnimatior.SetFloat("Vertical", lastMoveDirection.y);
            playerAnimatior.SetBool("IsAttacking", true);

        }
        else if (context.canceled && shotgunGO != null && releaseShotgun)
        {
            releaseShotgun = false;
            shotgunGO.GetComponent<ShotgunLogic>().Fire();
			sfx.playSound(6); //play shotgun sound
            canMove = true;
            usingWeapon = false;
            playerAnimatior.SetBool("IsAttacking", false);
            sfx.playSound(7); //play reload sound
        }
    }

    public void FireWaterHose(InputAction.CallbackContext context) {
        if (context.started && waterTankLevel > 0.0f && !usingWeapon && fireHoseCooldownTimer < Time.time)
        {

            fireHoseCooldownTimer = Time.time + fireHoseCooldown;
            waterStartChargeTime = Time.time;

            // Spawn aimer
            firehoseGO = Instantiate(fireHosePrefab, transform);
            firehoseGO.GetComponent<FireHoseLogic>().WaterSpread(lastMoveDirection);

            // Stop Player
            moveDirection = Vector2.zero;
            isoDirection = Vector2.zero;
            canMove = false;
            usingWeapon = true;
            playerAnimatior.SetFloat("Horizontal", lastMoveDirection.x);
            playerAnimatior.SetFloat("Vertical", lastMoveDirection.y);
            playerAnimatior.SetBool("IsAttacking", true);

            Debug.Log("Waterlevel=" + waterTankLevel);
        }
        else if (context.canceled && firehoseGO != null && waterTankLevel > 0.0f)
        {
            var endTime = Time.time;
            var heldTime = endTime - waterStartChargeTime; 
            float waitTimetoMove = firehoseGO.GetComponent<FireHoseLogic>().SprayWater(heldTime);

            if (waitTimetoMove > 0.0f)
                sfx.playSound(3); //play hydro sfx
			Action moveFunc = () => {
                canMove = true;
                usingWeapon = false;
                playerAnimatior.SetBool("IsAttacking", false);
            };
            TimerManager.AddTimer(moveFunc, waitTimetoMove);
            float wlevel = waterTankLevel - 25.0f;
            PlayerWaterTankLevel(wlevel);
        }

    }

    public void FireHarvestBlade(InputAction.CallbackContext context)
    {
        if (context.started && harvestBladeCooldownTimer < Time.time)
        {
            harvestBladeCooldownTimer = Time.time + harvestBladeCooldown;

            // Spawn aimer
            harvestBladeGO = Instantiate(harvestBladePrefab, transform);
            harvestBladeGO.GetComponent<HarvestBladeLogic>().Fire(lastMoveDirection);
			sfx.playSound(2);  //play sound
        }
    }

    public void SwitchWeapons (InputAction.CallbackContext context) 
    {
        // context.control will have 3 actions/ output. action, cancel, something else.
        //Debug.Log(context.performed);

        // Check user number key input. Switch weapon based off key input.
        if (context.performed && !usingWeapon)
        {
            int keyNumPress = Convert.ToInt16(context.control.name);
            Debug.Log(context.control.name);
            String dbugmsg = "Setting current weapon to";

            if(keyNumPress < typesOfWeapons.Length)
            {
                Debug.Log(dbugmsg + " " + typesOfWeapons[keyNumPress]);
                CurrentWeapon = typesOfWeapons[keyNumPress];
            }
        }
    }

    public void SetMove(bool move) {
        canMove = move;
        usingWeapon = false;
    }
    
    public void PlayerWaterTankLevel(float level) {
        waterTankLevel = level;
        Debug.Log(waterTankLevel);
    }

    public void SwitchSeeds(InputAction.CallbackContext context) {
        if ((context.control.name == "e" || context.control.name == "q") && context.canceled) {
            seeds.NextSeed(context);
        }

    }
    
    private void ProcessOverHeat()
    {
        if (usingJets)
            overheatVal += overHeatRate / 100;

        if (flameThrowerGO != null)
            overheatVal += overHeatRate / 100;

        if (overheatVal > 100.0f)
        {
            overheatVal = 100.0f;
            jetLockdown = true;
            flameThrowerLockdown = true;

            if (flameThrowerGO != null)
            {
                usingWeapon = false;
                flameThrowerGO.GetComponent<FlameThrowerLogic>().StopFlame();
            }
        }

        if (!usingJets && flameThrowerGO == null)
            overheatVal -= cooldownRate / 100;

        if (overheatVal < 0.0f)
        {
            overheatVal = 0.0f;
            jetLockdown = false;
            flameThrowerLockdown = false;
        }
    }

    IEnumerator ScreenShake()
    {
        float elapsed = 0f;

        while (elapsed < screenShakeDuration)
        {
            elapsed += Time.deltaTime;
            float stength = screenShakeStrength.Evaluate(elapsed / screenShakeDuration);
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z) + (Vector3)UnityEngine.Random.insideUnitCircle * stength;
            yield return null;
        }

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }


    public bool GetUsingJets()
    { return usingJets; }
}
