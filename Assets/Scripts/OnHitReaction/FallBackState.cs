using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBackState : BasePlayerState
{
    float fallBackTime;
    float fallbackIntialTime;
    public FallBackState(PlayerStateProperties properties, float time) : base(properties, time)
    {
        this.fallBackTime = time;
        this.fallbackIntialTime = time;
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
        fallBackTime -= Time.deltaTime;
        if (fallBackTime < 0)
        {
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
            fallBackTime = fallbackIntialTime;
        }
    }
}
