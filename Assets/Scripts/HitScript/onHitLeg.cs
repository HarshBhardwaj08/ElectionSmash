using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onHitLeg : MonoBehaviour
{
    private bool isPlay;
    private bool isTrigger;
    private BasePlayer player;
    private void Awake()
    {
        player = GetComponentInParent<ModiPlayer>();
    }
    private void OnEnable()
    {
        SignalManager.Instance.Subscribe<OnHitPlayer1Signal>(onLegHit);
    }
    private void OnDisable()
    {
        SignalManager.Instance.Unsubscribe<OnHitPlayer1Signal>(onLegHit);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player")
        {
            isTrigger = true;
        }
    }
    private void onLegHit(OnHitPlayer1Signal notify)
    {
        isPlay = notify.isLegHit;
        if(isPlay == true && isTrigger == true)
        {   
            player.ishit = true;
            player.stateMachine.ChangeState(player.hitBackWards);
        }
        isPlay = false;
        isTrigger = false;
    }

}
