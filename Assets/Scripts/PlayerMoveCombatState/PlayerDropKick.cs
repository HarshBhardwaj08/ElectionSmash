using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropKick : BasePlayerState
{
    float kicktime;
    float kickrate;
    public PlayerDropKick(PlayerStateProperties properties, float time) : base(properties, time)
    {
        this.kicktime = time;
        kickrate = time;
    }

    public override void enter()
    {
        base.enter();
        isMoving = false;
        isButtonPressed = false;
    }

    public override void exit()
    {
        base.exit();
        isMoving = true;
        isButtonPressed = true;
    }

    public override void update()
    {
        base.update();
        if (kicktime < 0)
        {
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
            kicktime = kickrate;
        }
    }
}
