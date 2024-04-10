using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public PlayerStateMachine stateMachine;
    [Header("IdleCombatAnimation")]
    #region IdleCombatAnimation
    public PlayerIdle playerIdle;
    public PlayerMove moveState;
    public PlayerStandToCrouch playerCrouch;
    public PlayerLowPunch lowpunchState;
    public PlayerHighPunch playerhighpunch;
    public PlayerPunchCombo punchCombo;
    public PlayerKick playerKick;
    public PlayerHighKick playerHighKick;
    public PlayerKickCombo playerKickCombo;
    #endregion

    #region MoveCombatAnimation
    public PlayerDropKick dropKick;
    public PlayerStompKick stompKick;
    public PlayerTaunt playerTaunt;
    public PlayerGrapple playerGrapple;
    public PlayerWindKick playerWindKick;
    #endregion

    #region OnHitAnimation
    public DizzyIdle idleDizzy;
    public FallBackState backState;
    public HitBackWards hitBackWards;
    public GettingUp gettingUpState;
    public HeadHit headHitState;
    public StomachHit stomachHitState;
    #endregion

    public BackFlipTaunt backFlipTaunt;
    public Animator animator { get; set; }
    public Rigidbody rb { get; set;}
    public List<AnimationClip> idleCombatanimationClip = new List<AnimationClip>();
    public List<AnimationClip> moveCombatanimationClip = new List<AnimationClip>();
    public List<AnimationClip> hurtAnimationClip = new List<AnimationClip>();
    public List<AnimationClip> comboAnimation = new List<AnimationClip>();
    [SerializeField] private GameObject player2;
    public float direction;
    public float yInput;
    public bool ishit;
    private float xPos;
    private void Awake()
    {
        xPos = transform.position.x;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        stateMachine = new PlayerStateMachine();
        var idleStateProperties = new PlayerStateProperties(this, stateMachine, "Idle");
         playerIdle = new PlayerIdle(idleStateProperties, GetAnimationLength(0)); 
        var moveStateProperties = new PlayerStateProperties(this, stateMachine, "Move");
        moveState = new PlayerMove(moveStateProperties, GetAnimationLength(1));
        var crouchStateProperties = new PlayerStateProperties(this,stateMachine,"Crouch");
        playerCrouch = new PlayerStandToCrouch (crouchStateProperties, GetAnimationLength(2));
        var grapleStateProperties = new PlayerStateProperties(this, stateMachine, "Grapple");
        playerGrapple = new PlayerGrapple (grapleStateProperties, GetMoveAnimationLength(3));
        #region IdleAnimCombatAnimation
        var combatStateProperties = new  PlayerStateProperties(this,stateMachine, "LowPunch");
        lowpunchState = new PlayerLowPunch(combatStateProperties, GetAnimationLength(2));
        var HightPunchStateProperties = new PlayerStateProperties(this, stateMachine, "HighPunch");
        playerhighpunch = new PlayerHighPunch(HightPunchStateProperties, GetAnimationLength(3));
        var playerpunchComboProperties = new PlayerStateProperties(this,stateMachine,"PunchCombo");
        punchCombo = new PlayerPunchCombo(playerpunchComboProperties, GetAnimationLength(4));
        var kickStateProperties = new PlayerStateProperties(this, stateMachine, "Kick");
        playerKick = new PlayerKick(kickStateProperties, GetAnimationLength(5));
        var highkickstateProperties = new PlayerStateProperties(this,stateMachine,"HighKick");
        playerHighKick = new PlayerHighKick(highkickstateProperties, GetAnimationLength(6));
        var playerkickComboProperties = new PlayerStateProperties(this, stateMachine,"KickCombo");
        playerKickCombo = new PlayerKickCombo(playerkickComboProperties, GetAnimationLength(7));
        #endregion
        #region MoveAnimCombatAnimation
        var playerWindKickProperties = new PlayerStateProperties(this, stateMachine, "HurricaneKick");
        playerWindKick = new PlayerWindKick (playerWindKickProperties, GetAnimationLength(0)-0.8f);
        var stompkickProperties = new PlayerStateProperties(this, stateMachine, "StompKICK");
        stompKick = new PlayerStompKick (stompkickProperties, GetMoveAnimationLength(1));
        var DropKickStateProperties = new PlayerStateProperties(this,stateMachine,"DropKick");
        dropKick = new PlayerDropKick(DropKickStateProperties,GetMoveAnimationLength(4));
        var TauntStateProperties = new PlayerStateProperties(this, stateMachine, "Taunt");
        playerTaunt = new PlayerTaunt (TauntStateProperties, GetMoveAnimationLength(5));
        #endregion
        #region OnHitAnimation
        var dizzyStateProperies = new PlayerStateProperties(this, stateMachine, "Dizzy");
        idleDizzy = new DizzyIdle(dizzyStateProperies, GetHitAnimationLength(0));
        var backStateProperties = new PlayerStateProperties(this, stateMachine, "FallingBackDeath");
        backState = new FallBackState(backStateProperties, GetHitAnimationLength(1));
        var hitBackwardState = new PlayerStateProperties(this, stateMachine, "GettingHitBackwards");
        hitBackWards = new HitBackWards(hitBackwardState, GetHitAnimationLength(2));
        var gettingUpStateProperties = new PlayerStateProperties(this, stateMachine, "GettingUp");
        gettingUpState = new GettingUp(gettingUpStateProperties, GetHitAnimationLength(3));
        var headHitStateProperties = new PlayerStateProperties(this, stateMachine, "HeadHit");
        headHitState = new HeadHit(headHitStateProperties, GetHitAnimationLength(4));
        var stomachHitProperties = new PlayerStateProperties(this, stateMachine, "StomachHit");
        stomachHitState = new StomachHit(stomachHitProperties, GetHitAnimationLength(7));
        #endregion
        #region Combos
        var backFlipTauntProperties = new PlayerStateProperties(this, stateMachine, "BackFlip");
        backFlipTaunt = new BackFlipTaunt(backFlipTauntProperties, GetComboAnimationLength(0));
        #endregion

    }
    private float GetAnimationLength(int index)
    {
        if (index >= 0 && index < idleCombatanimationClip.Count)
            return idleCombatanimationClip[index].length;
        else
        {
            Debug.LogWarning("Index out of range for animationClip list.");
            return 0f;
        }
    }
    private float GetMoveAnimationLength(int index)
    {
        if (index >= 0 && index < moveCombatanimationClip.Count)
            return moveCombatanimationClip[index].length;
        else
        {
            Debug.LogWarning("Index out of range for animationClip list.");
            return 0f;
        }
    }
    private float GetHitAnimationLength(int index)
    {
        if (index >= 0 && index < hurtAnimationClip.Count)
            return hurtAnimationClip[index].length;
        else
        {
            Debug.LogWarning("Index out of range for animationClip list.");
            return 0f;
        }
    }
    private float GetComboAnimationLength(int index)
    {
        if (index >= 0 && index < comboAnimation.Count)
            return hurtAnimationClip[index].length;
        else
        {
            Debug.LogWarning("Index out of range for animationClip list.");
            return 0f;
        }
    }
    void Start()
    {
        stateMachine.OnEnterState(playerIdle);
    }

   
    void Update()
    {
        transform.position = new Vector3(xPos,transform.position.y,transform.position.z);
        stateMachine.UpdateCurrentState();
       yInput = Input.GetAxis("Vertical");
        Vector3 directionRay = player2.transform.position - this.transform.position;
        direction = directionRay.normalized.z;
        if(direction < 0f)
        {
            StartCoroutine(rotatePlayer(1.5f, 180.0f,1));
        }
        else
        {
            StartCoroutine(rotatePlayer(1.5f,0.0f,-1));
        }
    }
    IEnumerator rotatePlayer(float intime,float angle,float scale)
    {
       yield return new WaitForSeconds(intime);
        transform.rotation = Quaternion.Euler(0, angle, 0);
        transform.localScale = new Vector3(scale, 1, 1);
    }
    public void move(float move)
    {
        rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y,move);
    }
    public void DefaultState()
    {
        stateMachine.ChangeState(this.playerIdle);
    }
    public void addforce()
    {
       transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z +2.0f);
    }
}
