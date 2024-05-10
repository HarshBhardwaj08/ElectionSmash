using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuard : BasePlayerState
{
    float Guardtime;
    float GuardNormalTime;
    public PlayerGuard(PlayerStateProperties properties, float time) : base(properties, time)
    {
        this.Guardtime = time;
        GuardNormalTime = time;
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
        Guardtime -= Time.deltaTime;

        if (Guardtime < 0)
        {
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
            Guardtime = GuardNormalTime;
        }
    }
}
