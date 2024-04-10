using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHighKick : BasePlayerState
{
    float highkicktime;
    float highKickIntialTime;
    public PlayerHighKick(PlayerStateProperties properties, float time) : base(properties, time)
    {
        this.highKickIntialTime = time;
        this.highkicktime = time;
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
        highKickIntialTime -= Time.deltaTime;
     
        if (highKickIntialTime < 0)
        {
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
            highKickIntialTime = highkicktime;
        }
    }
}

