using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChestHit : MonoBehaviour
{  
    private BasePlayer player;
    private bool isPlay;
    private bool isTrigger;
    private void Awake()
    {
        player = GetComponentInParent<ModiPlayer>();
    }
    private void OnEnable()
    {
        SignalManager.Instance.Subscribe<OnHitPlayer1Signal>(chestChit);
    }
    private void OnDisable()
    {
        SignalManager.Instance.Unsubscribe<OnHitPlayer1Signal>(chestChit);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player1" || other.gameObject.tag == "Player")
        {
          isTrigger = true;
        }
    }
    private void chestChit(OnHitPlayer1Signal notify)
    {
        isPlay = notify.isStomachHit;
        if (isPlay == true && isTrigger == true)
        {
            player.ishit = true;
            player.stateMachine.ChangeState(player.stomachHitState);
          
        }
        isPlay = false;
        isTrigger = false;
    }
}
