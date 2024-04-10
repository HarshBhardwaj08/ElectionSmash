using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWindKick : BasePlayerState
{
    float Dropkicktime;
    float DropKickIntialTime;
    public PlayerWindKick(PlayerStateProperties properties, float time) : base(properties, time)
    {
        this.DropKickIntialTime = time -0.02f;
        this.Dropkicktime = time - 0.02f;
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
        DropKickIntialTime -= Time.deltaTime;

        if (DropKickIntialTime < 0)
        {
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
            DropKickIntialTime = Dropkicktime;
        }
    }
}
