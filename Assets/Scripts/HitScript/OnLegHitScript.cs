using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLegHitScript : MonoBehaviour
{
    BasePlayer player;
    [SerializeField] GameObject hitparticle;
    private void Awake()
    {
        player = GetComponentInParent<BasePlayer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {

            player.ishit = true;
        //    Instantiate(hitparticle,transform.position,Quaternion.identity);
            player.stateMachine.ChangeState(player.hitBackWards);
        }
    }
}
