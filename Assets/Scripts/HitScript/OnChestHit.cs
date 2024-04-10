using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChestHit : MonoBehaviour
{  
    BasePlayer player;
    [SerializeField] GameObject hitparticle;
    private void Awake()
    {
        player = GetComponentInParent<BasePlayer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player1")
        {
            Debug.Log("ChestHit");
            player.ishit = true;
         //   Instantiate(hitparticle, transform.position, Quaternion.identity);
            player.stateMachine.ChangeState(player.stomachHitState);
        }
    }
}
