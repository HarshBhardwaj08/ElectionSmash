using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static ReadyPlayerMe.Core.Analytics.Constants;

public class RahulGandhi : BasePlayer
{
    public float direction;
    public float yInput;
    public float xPos;
    public float Distances;
    [SerializeField] Vector3 locationOffset;
    [SerializeField] GameObject Player1;
    [SerializeField] GameObject destination;
    ModiPlayer player1Script;
    [SerializeField] float timeDalyTohit;
    public bool isblock ;
    public override void Awake()
    {
        base.Awake();
        player1Script = Player1.GetComponentInChildren<ModiPlayer>();
        stateMachine = new PlayerStateMachine();
        var idleStateProperties = new PlayerStateProperties(this, stateMachine, "Idle");
        playerIdle = new PlayerIdle(idleStateProperties, GetIdleCombatAnimationLength(0));
        var moveStateProperties = new PlayerStateProperties(this, stateMachine, "Walk");
        moveState = new PlayerMove(moveStateProperties, GetIdleCombatAnimationLength(1));
        #region IdleAnimCombatAnimation
        var combatStateProperties = new PlayerStateProperties(this, stateMachine, "Punch");
        lowpunchState = new PlayerLowPunch(combatStateProperties, GetIdleCombatAnimationLength(0));
        var kickStateProperties = new PlayerStateProperties(this, stateMachine, "Kick");
        playerKick = new PlayerKick(kickStateProperties, GetIdleCombatAnimationLength(1));
        var legsweep = new PlayerStateProperties(this, stateMachine, "LegSweep");
        playerHighKick = new PlayerHighKick(legsweep, GetIdleCombatAnimationLength(2));
        #endregion
        var GuardStateProperties = new PlayerStateProperties(this, stateMachine, "Guard");
        playerGuard = new PlayerGuard(GuardStateProperties, GetHurtAnimationLength(0));
    }
    public override void Start()
    {
        base.Start();
        stateMachine.OnEnterState(playerIdle);
        destination.transform.position = Player1.transform.position;

    }
    float value = 0f;
    public override void Update()
    {
        base.Update();
        // stateMachine.UpdateCurrentState();

        if (player1Script.Distance > Distance && player1Script.Distance != 0 && isblock == false)
        {
            stateMachine.ChangeState(moveState);
            Move(Player1.transform.position);
        }
        else
        {
            stateMachine.ChangeState(playerIdle);
            value += Time.deltaTime;
            if (value > timeDalyTohit)
            {
                int num = Random.Range(0, 4);
                SwitchState(0);
            }
        }
      
    }
    public void Move(Vector3 movement)
    {
        Vector3 Destination = Vector3.Slerp(transform.position, movement, 0.2f * Time.deltaTime);
        transform.position = Destination;

    }
    public void DefaultState()
    {
        stateMachine.ChangeState(this.playerIdle);
    }
    IEnumerator BackToWalk(int n)
    {
        yield return new WaitForSecondsRealtime(n);
        destination.transform.position = Player1.transform.position;
    }

    private void SwitchState(int n)
    {
        switch (n)
        {
            case 0:
                stateMachine.ChangeState(lowpunchState);
           
                value = 0;
                break;
            case 1:
                stateMachine.ChangeState(playerKick);
              
                value = 0;
                break;
            case 2:
                stateMachine.ChangeState(playerHighKick);
              
                value = 0;
                break;
            case 3:
                stateMachine.ChangeState(playerIdle);
               
                value = 0;
                break;
            case 4:
                Distance = 3.3f;
                destination.transform.position = Player1.transform.position - new Vector3(0, 0, 3);
                break;
            case 5:
                stateMachine.ChangeState(playerGuard);
                break;
        }
    }
    public void onHitLeg()
    {
        SignalManager.Instance.Fire(new OnHitPlayer1Signal() { isLegHit = true });
    }
    public void onHitStomach()
    {
        SignalManager.Instance.Fire(new OnHitPlayer1Signal() { isStomachHit = true });
    }
    public void onHitHead()
    {
        SignalManager.Instance.Fire(new OnHitPlayer1Signal() { isheadHit = true });
    }
}
