using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jetSpeed;
    [SerializeField]
    private float jetOffsetSpeed;
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

    private String[] typesOfWeapons = {
        "Why are we using key input 0.. GROSS. Need this as place holder for array. Until we actually want to use this.",
        "lazer",
        "shotgun",
        "sword",
        "missle",
    };

    // N = 0, NE = 1, E = 2, SE = 3
    // S = 4, NW = 5, W = 6, SW = 7
    private int playerDirection;
    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;
    private bool canMove;
    private bool usingJets;

    private Rigidbody2D rig;

    private Animator playerAnimatior;

    private string CurrentWeapon;

    private GameObject laserGO;

    private GameObject shotgunGO;

    private GameObject harvestBladeGO;

    private void Awake() {
        playerDirection = 1;
        canMove = true;
        usingJets = false;
        moveDirection = new Vector2();
        playerAnimatior = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update() {
    }

    private void FixedUpdate() {
        if(usingJets)
        {
            Vector2 vel = rig.linearVelocity + (moveDirection * jetOffsetSpeed);
            if (vel.magnitude < jetSpeed)
                rig.linearVelocity = vel;
            else
                rig.linearVelocity = vel.normalized * jetSpeed;
        }
        else
        {
            rig.linearVelocity = moveDirection * moveSpeed;
            
            playerAnimatior.SetFloat("Horizontal", lastMoveDirection.x);
            playerAnimatior.SetFloat("Vertical", lastMoveDirection.y);
            playerAnimatior.SetFloat("Magnitude", moveDirection.magnitude);
        }
    }

    private void ChangeDirection() {
        lastMoveDirection = moveDirection;
        playerDirection = (int)(Vector2.Angle(Vector2.up, moveDirection) / 45.0f);

        if (moveDirection.x < 0)
            playerDirection += 4;
    }

    public void DetermineDirection(InputAction.CallbackContext context) {
        moveDirection = Vector2.zero;

        // Get direction of player and move
        if (context.performed && canMove)
        {
            moveDirection = playerInput.action.ReadValue<Vector2>();
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
        if (context.performed && canMove)
        {
            rig.linearVelocity = moveDirection * jetSpeed;
            usingJets = true;
        }
        else if(context.canceled)
            usingJets = false;
    }

    public void FireWeapon(InputAction.CallbackContext context) {
        // I HATE HAVING TO DO THIS PLZ HELP SHOW ME THE WAY. DO YOU KNOW DA WHEY? 

        // Creating statements to fire weapon based off of the current weapon.
        if (typesOfWeapons[0] == CurrentWeapon)
        {
        }
        else if (typesOfWeapons[1] == CurrentWeapon)
        {
            FireLaser(context);
        } 
        else if (typesOfWeapons[2] == CurrentWeapon)
        {
            FireShotGun(context);
        } 
        else if (typesOfWeapons[3] == CurrentWeapon)
        {
            FireHarvestBlade(context);
        }
        else if (typesOfWeapons[4] == CurrentWeapon)
        {
        }
    }

    public void FireLaser(InputAction.CallbackContext context) {
        if (!usingJets)
        {
            if (context.started)
            {
                // Stop Player
                moveDirection = Vector2.zero;
                canMove = false;

                // Spawn aimer
                laserGO = Instantiate(laserPrefab, transform);
                laserGO.GetComponent<LaserLogic>().Charge(playerDirection);
            }
            else if (context.canceled && laserGO != null)
            {
                if(laserGO != null && !laserGO.GetComponent<LaserLogic>().Fired())
                    laserGO.GetComponent<LaserLogic>().Fire();
            }
        }
    }

    public void FireShotGun(InputAction.CallbackContext context) {
        if (!usingJets)
        {
            if (context.started)
            {
                // Stop Player
                moveDirection = Vector2.zero;
                canMove = false;

                // Spawn aimer
                shotgunGO = Instantiate(shotgunPrefab, transform);
                shotgunGO.GetComponent<ShotgunLogic>().Charge(playerDirection);
            }
            else if (context.canceled && shotgunGO != null)
            {
                shotgunGO.GetComponent<ShotgunLogic>().Fire();
                canMove = true;
            }
        }
    }

    public void SwitchWeapons (InputAction.CallbackContext context) 
    {
        int keyNumPress = Convert.ToInt16(context.control.name);
        // context.control will have 3 actions/ output. action, cancel, something else.
        //Debug.Log(context.performed);

        // Check user number key input. Switch weapon based off key input.
        if (context.performed) {
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
    }

    public void FireHarvestBlade(InputAction.CallbackContext context) {
        if (!usingJets)
        {
            if (context.started)
            {
                // Stop Player
                moveDirection = Vector2.zero;
                canMove = false;

                // Spawn aimer
                harvestBladeGO = Instantiate(harvestBladePrefab, transform);
                harvestBladeGO.GetComponent<HarvestBladeLogic>().Fire(playerDirection);
            }
        }
    }
    
}
