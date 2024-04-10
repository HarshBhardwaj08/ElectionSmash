using System.Collections;
using System.Collections.Generic;
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
    public BasePlayerState(PlayerStateProperties properties, float time)
    {
        this.properties = properties;
        Rigidbody = this.properties.Player.rb;
        this.animTime = time;
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
        if(inputY > 0 && iscrouch == true)
        {
            exit();
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
            iscrouch= false;
        }
        playerIdleCombatState();
        PlayerMoveCombatState();
    }

    protected void playerIdleCombatState()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            properties.StateMachine.ChangeState(properties.Player.lowpunchState);
        }
        if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            properties.StateMachine.ChangeState(properties.Player.playerhighpunch);
        }
        if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            properties.StateMachine.ChangeState(properties.Player.playerKick);
        }
        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            properties.StateMachine.ChangeState(properties.Player.playerHighKick);
        }
        if (Input.GetKeyDown(KeyCode.P) && Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.Joystick1Button2)
                && Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            properties.StateMachine.ChangeState(properties.Player.punchCombo);
        }
        if (Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.Joystick1Button1) &&
              Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            properties.StateMachine.ChangeState(properties.Player.playerKickCombo);
        }
    }
    protected void PlayerMoveCombatState()
    {
        if (inputX > 0 && (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.Joystick1Button0)))
        {
            properties.StateMachine.ChangeState(properties.Player.dropKick);
        }
        if (inputX > 0 && (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Joystick1Button1)))
        {
            properties.StateMachine.ChangeState(properties.Player.stompKick);
        }

        if (Input.GetKeyDown(KeyCode.T) || (Input.GetKeyDown(KeyCode.Joystick1Button5)))
        {
            properties.StateMachine.ChangeState(properties.Player.playerTaunt);
        }
        if (Input.GetKeyDown(KeyCode.G) || (Input.GetKeyDown(KeyCode.Joystick1Button4)))
        {
            properties.StateMachine.ChangeState(properties.Player.playerGrapple);
        }
        if (inputY > 0 && Input.GetKeyDown(KeyCode.Joystick1Button0) && iscrouch == false)
        {
            properties.StateMachine.ChangeState(properties.Player.playerWindKick);
        }

    }
}
