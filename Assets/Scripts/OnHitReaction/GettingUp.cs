using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingUp : BasePlayerState
{
    float gettingUptime;
    float gettingUpIntialTime;
    public GettingUp(PlayerStateProperties properties, float time) : base(properties, time)
    {
        this.gettingUptime = time;
        this.gettingUpIntialTime = time;
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
        gettingUptime -= Time.deltaTime;
        if (gettingUptime < 0)
        {
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
            gettingUptime = gettingUpIntialTime;
        }
    }
}
