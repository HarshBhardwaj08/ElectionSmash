using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrapple : BasePlayerState
{
    float grappleTime;
    float grappleIntialTime;
    public PlayerGrapple(PlayerStateProperties properties, float time) : base(properties, time)
    {
        this.grappleIntialTime = time;
        this.grappleTime = time;
    }

    public override void enter()
    {
        base.enter();
      
    }

    public override void exit()
    {
        base.exit();
    
    }

    public override void update()
    {
        base.update();
        grappleIntialTime -= Time.deltaTime;

        if (grappleIntialTime < 0)
        {
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
            grappleIntialTime = grappleTime;
        }
    }
}
