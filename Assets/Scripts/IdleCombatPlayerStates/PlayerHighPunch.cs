using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHighPunch : BasePlayerState
{
    float kicktime;
    float kickrate;
    public PlayerHighPunch(PlayerStateProperties properties, float time) : base(properties, time)
    {
        this.kicktime = time;
        kickrate = time;
    }

    public override void enter()
    {
        base.enter();
        isButtonPressed = false;
    }

    public override void exit()
    {
        base.exit();
        isButtonPressed = true;
    }

    public override void update()
    {
        base.update();
        kicktime -= Time.deltaTime;
        if (kicktime < 0)
        {
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
            kicktime = kickrate;
        }
    }
}
