using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStompKick : BasePlayerState
{

    float Stompkicktime;
    float StopmKickIntialTime;
    public PlayerStompKick(PlayerStateProperties properties, float time) : base(properties, time)
    {
        this.Stompkicktime = time;
        this.StopmKickIntialTime = time;
    }

    public override void enter()
    {
        base.enter();
        isMoving = false;
        isButtonPressed = true;
    }

    public override void exit()
    {
        base.exit();
        isMoving = true;
    }

    public override void update()
    {
        base.update();
        StopmKickIntialTime -= Time.deltaTime;

        if (StopmKickIntialTime < 0)
        {
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
            StopmKickIntialTime = Stompkicktime;
        }
    }
}
