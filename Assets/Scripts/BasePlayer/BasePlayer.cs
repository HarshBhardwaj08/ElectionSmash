using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public bool isPlayer;
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
    public float Distance;
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
    public PlayerGuard playerGuard;
    #endregion

    public BackFlipTaunt backFlipTaunt;
    public Animator animator { get; set; }
    public Rigidbody rb { get; set; }
    public List<AnimationClip> idleCombatanimationClip = new List<AnimationClip>();
    public List<AnimationClip> moveCombatanimationClip = new List<AnimationClip>();
    public List<AnimationClip> hurtAnimationClip = new List<AnimationClip>();
    public List<AnimationClip> comboAnimation = new List<AnimationClip>();
    public bool ishit;
    protected float GetAnimationLength(List<AnimationClip> clipList, int index)
    {
        if (index >= 0 && index < clipList.Count)
            return clipList[index].length;
        else
        {
            Debug.LogWarning("Index out of range for animationClip list.");
            return 0f;
        }
    }

    // Example usage:
    protected float GetIdleCombatAnimationLength(int index)
    {
        return GetAnimationLength(idleCombatanimationClip, index);
    }

    protected float GetMoveCombatAnimationLength(int index)
    {
        return GetAnimationLength(moveCombatanimationClip, index);
    }

    protected float GetHurtAnimationLength(int index)
    {
        return GetAnimationLength(hurtAnimationClip, index);
    }

    protected float GetComboAnimationLength(int index)
    {
        return GetAnimationLength(comboAnimation, index);
    }
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    public virtual void Start()
    {
        
    }
    public virtual void Update()
    {
        
    }
    public virtual void move(float move)
    {
      
    }

}
