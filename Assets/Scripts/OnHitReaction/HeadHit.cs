using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHit : BasePlayerState
{
    float headHitTime;
    float headHItIntialTime;

    public HeadHit(PlayerStateProperties properties, float time) : base(properties, time)
    {
        this.headHitTime = time;
        this.headHItIntialTime = time;
    }

    public override void enter()
    {
        base.enter();
        
    }

    public override void exit()
    {
        base.exit();
        properties.Player.ishit = false;
    }

    public override void update()
    {
        base.update();
        headHitTime -= Time.deltaTime;
        if (headHitTime < 0)
        {
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
            headHitTime = headHItIntialTime;
        }
    }
}
