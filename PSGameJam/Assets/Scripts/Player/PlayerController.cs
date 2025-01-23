using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;



public class PlayerController : MonoBehaviour
{
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
    private float jetUseTime;
    [SerializeField]
    private float jetCooldown;
    [SerializeField]
    private GameObject jetPackVFX;
    [SerializeField]
    private Vector2 jetVFXOffset;

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

    private String[] typesOfWeapons = {
        "none", // 0
        "shotgun", // 1
        "firehose", // 2
        "sword", // 3
        "lazer", // 4
    };

    
    [SerializeField]
    public SeedLogic seeds;


    // N = 0, NE = 1, E = 2, SE = 3
    // S = 4, NW = 5, W = 6, SW = 7
    private float waterStartChargeTime;
    private Vector2 moveDirection;
    private Vector2 isoDirection;
    private Vector2 lastMoveDirection;
    private bool canMove;
    private bool usingWeapon;
    private bool usingJets;

    private Rigidbody2D rig;

    private Animator playerAnimatior;

    private string CurrentWeapon;

    private GameObject laserGO;
    private GameObject shotgunGO;
    private GameObject firehoseGO;
    private GameObject harvestBladeGO;
    private GameObject jetPackVFXGO;

    private float laserCooldownTimer;
    private float shotgunCooldownTimer;
    private float fireHoseCooldownTimer;
    private float harvestBladeCooldownTimer;
    private float jetCooldownTimer;
    private float jetStartUseTimer;

    private void Awake() {
        canMove = true;
        usingWeapon = false;
        usingJets = false;
        lastMoveDirection = new Vector2(ISO_X_DIAGNOL_DIR, ISO_Y_DIAGNOL_DIR);
        playerAnimatior = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update() {
    }

    private void FixedUpdate() {
        if(usingJets)
        {

            Vector2 vel = rig.linearVelocity + (isoDirection * jetOffsetSpeed);
            if (vel.magnitude < jetSpeed)
                rig.linearVelocity = vel;
            else
                rig.linearVelocity = vel.normalized * jetSpeed;

            if (jetStartUseTimer < Time.time)
            {
                Destroy(jetPackVFXGO);
                jetCooldownTimer = Time.time + jetCooldown;
                usingJets = false;
            }
        }
        else
        {
            rig.linearVelocity = isoDirection * moveSpeed;
            
            playerAnimatior.SetFloat("Horizontal", lastMoveDirection.x);
            playerAnimatior.SetFloat("Vertical", lastMoveDirection.y);
            playerAnimatior.SetFloat("Magnitude", moveDirection.magnitude);
        }
    }

    private void ChangeDirection() {
        lastMoveDirection = moveDirection;

        if(usingJets)
        {
            Vector3 origin = transform.position + jetPackVFX.transform.position;
            var angle = Vector2.Angle(Vector2.left, moveDirection);

            // Flip if face positive direction
            if (moveDirection.y > 0)
                angle = -angle;

            Vector3 rot_offset = TileManager.rotate(jetVFXOffset, angle);
            jetPackVFXGO.transform.position = origin + rot_offset;

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
                if(moveDirection != Vector2.zero)
                    ChangeDirection();
            };

            TimerManager.AddTimer(changeDir, changeDirectionTimer);
        }
    }

    public void JetBoost(InputAction.CallbackContext context) {
        // Get direction of player and move
        if (context.performed && canMove && jetCooldownTimer < Time.time)
        {
            jetStartUseTimer = Time.time + jetUseTime;
            rig.linearVelocity = moveDirection * jetSpeed;

            // Spawn and set up jetPackVFXGO
            jetPackVFXGO = Instantiate(jetPackVFX, transform);
            var angle = Vector2.Angle(Vector2.left, moveDirection);

            // Flip if face positive direction
            if (moveDirection.y > 0)
                angle = -angle;

            Vector3 rot_offset = TileManager.rotate(jetVFXOffset, angle);
            jetPackVFXGO.transform.position = jetPackVFXGO.transform.position + rot_offset;

            usingJets = true;
        }
        else if(context.canceled && usingJets)
        {
            Destroy(jetPackVFXGO);
            jetCooldownTimer = Time.time + jetCooldown;
            usingJets = false;
        }

    }

    public void FireWeapon(InputAction.CallbackContext context) {
        // Creating statements to fire weapon based off of the current weapon.
        if (typesOfWeapons[0] == CurrentWeapon)
        {
            Debug.Log("Putting away items");
        }
        else if (typesOfWeapons[1] == CurrentWeapon && !usingJets && seeds.ShootSeed(0) != 0)
        {
            if (context.action.name == "Cursor")
            {
                FireShotGun(context);
                //Debug.Log(context.action.name);
            }

        }
        else if (typesOfWeapons[2] == CurrentWeapon && !usingJets)
        {
            if (context.action.name == "Spacebar")
            {
                //Debug.Log("Spraying Water!");
                FireWaterHose(context);
            }
        }
        else if (typesOfWeapons[3] == CurrentWeapon)
        {
            if (context.action.name == "Cursor")
            {
                FireHarvestBlade(context);
                //Debug.Log("Swing Sword");
            }
        }
        else if (typesOfWeapons[4] == CurrentWeapon && !usingJets)
        {
            if (context.action.name == "Cursor")
            {
                FireLaser(context);
            }
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
                laserGO.GetComponent<LaserLogic>().Fire();
        }
    }

    public void FireShotGun(InputAction.CallbackContext context) {

        if (context.started && shotgunCooldownTimer < Time.time)
        {
            shotgunCooldownTimer = Time.time + shotgunCooldown;
            // Spawn aimer
            shotgunGO = Instantiate(shotgunPrefab, transform);
            shotgunGO.GetComponent<ShotgunLogic>().BulletSpread(lastMoveDirection);

            // Stop Player
            moveDirection = Vector2.zero;
            isoDirection = Vector2.zero;
            canMove = false;
            usingWeapon = true;

        }
        else if (context.canceled && shotgunGO != null)
        {
            
            int seedCount = seeds.ShootSeed(5);
            Debug.Log(seedCount);
            if (seedCount != 0) {
                shotgunGO.GetComponent<ShotgunLogic>().Fire();
            }

            shotgunGO.GetComponent<ShotgunLogic>().Fire();
            canMove = true;
            usingWeapon = false;
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
        }
        else if (context.canceled && firehoseGO != null && waterTankLevel > 0.0f)
        {
            var endTime = Time.time;
            var heldTime = endTime - waterStartChargeTime; 
            float waitTimetoMove = firehoseGO.GetComponent<FireHoseLogic>().SprayWater(heldTime);
            Action moveFunc = () => {
                canMove = true;
                usingWeapon = false;
            };
            TimerManager.AddTimer(moveFunc, waitTimetoMove);
            float wlevel = waterTankLevel - 25.0f;
            PlayerWaterTankLevel(wlevel);
        }
        else {
            Debug.Log("Player out of water. Need to Refil. Waterlevel=" + waterTankLevel);
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
        }
    }

    public void SwitchWeapons (InputAction.CallbackContext context) 
    {
        int keyNumPress = Convert.ToInt16(context.control.name);
        // context.control will have 3 actions/ output. action, cancel, something else.
        //Debug.Log(context.performed);

        // Check user number key input. Switch weapon based off key input.
        if (context.performed && !usingWeapon) {
            Debug.Log(context.control.name);
            String dbugmsg = "Setting current weapon to";

            switch (keyNumPress)
            {
                case 0:
                    Debug.Log(dbugmsg + " " + typesOfWeapons[keyNumPress]);
                    CurrentWeapon = typesOfWeapons[keyNumPress];
                    break;            
                case 1:
                    Debug.Log(dbugmsg + " " + typesOfWeapons[keyNumPress]);
                    CurrentWeapon = typesOfWeapons[keyNumPress];
                    break;
                case 2:
                    Debug.Log(dbugmsg + " " + typesOfWeapons[keyNumPress]);
                    CurrentWeapon = typesOfWeapons[keyNumPress];
                    break;
                case 3:
                    Debug.Log(dbugmsg + " " + typesOfWeapons[keyNumPress]);
                    CurrentWeapon = typesOfWeapons[keyNumPress];
                    break;
                case 4:
                    Debug.Log(dbugmsg + " " + typesOfWeapons[keyNumPress]);
                    CurrentWeapon = typesOfWeapons[keyNumPress];
                    break;
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
        if (context.control.name == "u" && context.canceled) {
            seeds.NextSeed(context);
        }
    }
}
