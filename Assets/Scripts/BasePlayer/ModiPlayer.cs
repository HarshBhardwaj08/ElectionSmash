using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ModiPlayer : BasePlayer
{
    [SerializeField] private GameObject player2;
    public float direction;
    public float yInput;
    private float xPos;
   
    public override void Awake()
    {
        base.Awake();
        xPos = transform.position.x;
        stateMachine = new PlayerStateMachine();
        var idleStateProperties = new PlayerStateProperties(this, stateMachine, "Idle");
        playerIdle = new PlayerIdle(idleStateProperties, GetIdleCombatAnimationLength(0));
        var moveStateProperties = new PlayerStateProperties(this, stateMachine, "Move");
        moveState = new PlayerMove(moveStateProperties, GetIdleCombatAnimationLength(1));
        var crouchStateProperties = new PlayerStateProperties(this, stateMachine, "Crouch");
        playerCrouch = new PlayerStandToCrouch(crouchStateProperties, GetIdleCombatAnimationLength(2));
        var grapleStateProperties = new PlayerStateProperties(this, stateMachine, "Grapple");
        playerGrapple = new PlayerGrapple(grapleStateProperties, GetMoveCombatAnimationLength(3));
        #region IdleAnimCombatAnimation
        var combatStateProperties = new PlayerStateProperties(this, stateMachine, "LowPunch");
        lowpunchState = new PlayerLowPunch(combatStateProperties, GetIdleCombatAnimationLength(2));
        var HightPunchStateProperties = new PlayerStateProperties(this, stateMachine, "HighPunch");
        playerhighpunch = new PlayerHighPunch(HightPunchStateProperties, GetIdleCombatAnimationLength(3));
        var playerpunchComboProperties = new PlayerStateProperties(this, stateMachine, "PunchCombo");
        punchCombo = new PlayerPunchCombo(playerpunchComboProperties, GetIdleCombatAnimationLength(4));
        var kickStateProperties = new PlayerStateProperties(this, stateMachine, "Kick");
        playerKick = new PlayerKick(kickStateProperties, GetIdleCombatAnimationLength(5));
        var highkickstateProperties = new PlayerStateProperties(this, stateMachine, "HighKick");
        playerHighKick = new PlayerHighKick(highkickstateProperties, GetIdleCombatAnimationLength(6));
        var playerkickComboProperties = new PlayerStateProperties(this, stateMachine, "KickCombo");
        playerKickCombo = new PlayerKickCombo(playerkickComboProperties, GetIdleCombatAnimationLength(7));
        #endregion
        #region MoveAnimCombatAnimation
        var playerWindKickProperties = new PlayerStateProperties(this, stateMachine, "HurricaneKick");
        playerWindKick = new PlayerWindKick(playerWindKickProperties, GetMoveCombatAnimationLength(0) - 0.8f);
        var stompkickProperties = new PlayerStateProperties(this, stateMachine, "StompKICK");
        stompKick = new PlayerStompKick(stompkickProperties, GetMoveCombatAnimationLength(1));
        var DropKickStateProperties = new PlayerStateProperties(this, stateMachine, "DropKick");
        dropKick = new PlayerDropKick(DropKickStateProperties, GetMoveCombatAnimationLength(4));
        var TauntStateProperties = new PlayerStateProperties(this, stateMachine, "Taunt");
        playerTaunt = new PlayerTaunt(TauntStateProperties, GetMoveCombatAnimationLength(5));
        #endregion
        #region OnHitAnimation
        var dizzyStateProperies = new PlayerStateProperties(this, stateMachine, "Dizzy");
        idleDizzy = new DizzyIdle(dizzyStateProperies, GetHurtAnimationLength(0));
        var backStateProperties = new PlayerStateProperties(this, stateMachine, "FallingBackDeath");
        backState = new FallBackState(backStateProperties, GetHurtAnimationLength(1));
        var hitBackwardState = new PlayerStateProperties(this, stateMachine, "GettingHitBackwards");
        hitBackWards = new HitBackWards(hitBackwardState, GetHurtAnimationLength(2));
        var gettingUpStateProperties = new PlayerStateProperties(this, stateMachine, "GettingUp");
        gettingUpState = new GettingUp(gettingUpStateProperties, GetHurtAnimationLength(3));
        var headHitStateProperties = new PlayerStateProperties(this, stateMachine, "HeadHit");
        headHitState = new HeadHit(headHitStateProperties, GetHurtAnimationLength(4));
        var stomachHitProperties = new PlayerStateProperties(this, stateMachine, "StomachHit");
        stomachHitState = new StomachHit(stomachHitProperties, GetHurtAnimationLength(7));
        #endregion
        #region Combos
        var backFlipTauntProperties = new PlayerStateProperties(this, stateMachine, "BackFlip");
        backFlipTaunt = new BackFlipTaunt(backFlipTauntProperties, GetComboAnimationLength(0));
        #endregion
    }

    public override void Start()
    {
        stateMachine.OnEnterState(playerIdle);
    }
    public override void Update()
    {
        base.Update();
        Distance = Vector3.Distance(player2.transform.position, transform.position);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        stateMachine.UpdateCurrentState();
        yInput = Input.GetAxis("Vertical");
        Vector3 directionRay = player2.transform.position - this.transform.position;
        direction = directionRay.normalized.z;
        if (direction < 0f)
        {
            StartCoroutine(rotatePlayer(1.5f, 180.0f, 1));
        }
        else
        {
            StartCoroutine(rotatePlayer(1.5f, 0.0f, -1));
        }
    }

    IEnumerator rotatePlayer(float intime,float angle,float scale)
    {
       yield return new WaitForSeconds(intime);
        transform.rotation = Quaternion.Euler(0, angle, 0);
        transform.localScale = new Vector3(scale, 1, 1);
    }
  
    public void DefaultState()
    {
        stateMachine.ChangeState(this.playerIdle);
     
    }
    public void addforce()
    {
       transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z +2.0f);
    }

    public override void move(float move)
    {
        base.move(move);
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, move*-1);
    }
    public void onHitLeg()
    {
        SignalManager.Instance.Fire(new OnHitPlayer2Signal() { isLegHit = true });
    }
    public void onHitStomach()
    {
        SignalManager.Instance.Fire(new OnHitPlayer2Signal() { isStomachHit = true });
    }
    public void onHitHead()
    {
        SignalManager.Instance.Fire(new OnHitPlayer2Signal() { isheadHit = true });
    }
}
