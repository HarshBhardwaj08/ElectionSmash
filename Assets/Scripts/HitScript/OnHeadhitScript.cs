using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHeadhitScript : MonoBehaviour
{
    private  BasePlayer player;
    private bool isPlay;
    private bool isTrigger;
    private void Awake()
    {
        player =  GetComponentInParent<ModiPlayer>();
    }
    private void OnEnable()
    {
        SignalManager.Instance.Subscribe<OnHitPlayer1Signal>(HeadHit);
    }
    private void OnDisable()
    {
        SignalManager.Instance.Unsubscribe<OnHitPlayer1Signal>(HeadHit);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player")
        {
           isTrigger = true;
        }
    }
    private void HeadHit(OnHitPlayer1Signal notify)
    {
        isPlay = notify.isheadHit;
        if (isPlay == true && isTrigger == true)
        {
            player.ishit = true;
            player.stateMachine.ChangeState(player.headHitState);
         
        }
        isPlay = false;
        isTrigger = false;
    }
}
