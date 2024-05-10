using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerStateProperties
{
    public BasePlayer Player { get; }
    public PlayerStateMachine StateMachine { get; }
    public string AnimBoolName { get; }
    
    public PlayerStateProperties(BasePlayer player, PlayerStateMachine stateMachine, string animBoolName)
    {
        Player = player;
        StateMachine = stateMachine;
        AnimBoolName = animBoolName;
    }
}
public class BasePlayerState : IPlayer
{
    protected readonly PlayerStateProperties properties;
    protected Rigidbody Rigidbody;
    protected float inputX;
    protected float inputY;
    protected float animTime;
    protected bool isMoving = true;
    protected bool isButtonPressed = true;
    protected bool iscrouch = false;
    private bool isHit;
    protected bool isPlayer;
    protected Vector3 playerPos;
    public BasePlayerState(PlayerStateProperties properties, float time)
    {
        this.properties = properties;
        Rigidbody = this.properties.Player.rb;
        this.animTime = time;
        isPlayer = properties.Player.isPlayer;
    }

    public virtual void enter()
    {
       properties.Player.animator.SetBool(properties.AnimBoolName, true);
        isHit = properties.Player.ishit;
    }

    public virtual void exit()
    {
        properties.Player.animator.SetBool(properties.AnimBoolName, false);
    }

    public virtual void update()
    { 
        if(isPlayer == true)
        {   
            playerPos = properties.Player.transform.position;
            MovementVoid();
            playerIdleCombatState();
            PlayerMoveCombatState();
        }
       
    }
    private void MovementVoid()
    {
        if (isMoving == true)
        {
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
        }


        if (inputX != 0 && isHit == false)
        {
            properties.StateMachine.ChangeState(properties.Player.moveState);
        }
        if (inputY < 0)
        {
            properties.StateMachine.ChangeState(properties.Player.playerCrouch);
            iscrouch = true;
        }
        if (inputY > 0 && iscrouch == true)
        {
            exit();
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
            iscrouch = false;
        }
    }
    protected void playerIdleCombatState()
    {
        var Player = properties.Player;
        //Punches
        HandleButtonInput(KeyCode.P, KeyCode.Joystick1Button2, Player.lowpunchState);
        HandleButtonInput(KeyCode.O, KeyCode.Joystick1Button3, Player.playerhighpunch);
        HandleComboButton(KeyCode.P, KeyCode.O, Player.punchCombo);
        HandleButtonInput(KeyCode.T, KeyCode.Joystick1Button5, Player.playerTaunt);
        HandleButtonInput(KeyCode.G, KeyCode.Joystick1Button4, Player.playerGrapple);
        HandleComboButton(KeyCode.Joystick1Button3, KeyCode.Joystick1Button2, Player.playerWindKick);
        //PlayerKicks
        HandleButtonInput(KeyCode.L, KeyCode.Joystick1Button0, Player.playerHighKick);
        HandleButtonInput(KeyCode.K, KeyCode.Joystick1Button1, Player.playerKick);
        HandleComboButton(KeyCode.K, KeyCode.L, Player.playerKickCombo);
        HandleComboButton(KeyCode.Joystick1Button1, KeyCode.Joystick1Button0, Player.playerKickCombo);
    }

    protected void PlayerMoveCombatState()
    {
        var Player = properties.Player;
        MoveButtonInput(inputX, KeyCode.L, KeyCode.Joystick1Button0, Player.dropKick);
        MoveButtonInput(inputX, KeyCode.K, KeyCode.Joystick1Button1, Player.stompKick);
        if (iscrouch == false)
        {
            MoveButtonInput(inputY, KeyCode.L, KeyCode.Joystick1Button0, Player.playerWindKick);
        }
    }
    protected void MoveButtonInput(float input, KeyCode keyCode, KeyCode keyCode1, IPlayer stateName)
    {
        if ((input > 0 && Input.GetKeyDown(keyCode) || Input.GetKeyDown(keyCode1)))
        {
            properties.StateMachine.ChangeState(stateName);
        }
    }
    protected void HandleButtonInput(KeyCode key, KeyCode key1, IPlayer stateName)
    {
        if (Input.GetKeyDown(key) || Input.GetKeyDown(key1))
        {
            properties.StateMachine.ChangeState(stateName);
        }
    }
    protected void HandleComboButton(KeyCode key, KeyCode key1, IPlayer stateName)
    {
        if (Input.GetKeyDown(key) && Input.GetKeyDown(key1))
        {
            properties.StateMachine.ChangeState(stateName);
        }
    }
}
