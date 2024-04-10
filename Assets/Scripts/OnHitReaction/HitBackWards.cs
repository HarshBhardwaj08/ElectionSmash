using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBackWards : BasePlayerState
{
    float hitbackwardtime;
    float hitbackWardIntialTime;
    public HitBackWards(PlayerStateProperties properties, float time) : base(properties, time)
    {
        this.hitbackwardtime = time;
        this.hitbackWardIntialTime = time;
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
        hitbackwardtime -= Time.deltaTime;
        if (hitbackwardtime < 0)
        {
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
            hitbackwardtime = hitbackWardIntialTime;
        }
    }
}
