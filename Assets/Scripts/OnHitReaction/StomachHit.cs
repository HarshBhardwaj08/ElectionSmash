using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StomachHit : BasePlayerState
{
    float Dizzytime;
    float dizzyIntialTime;
    public StomachHit(PlayerStateProperties properties, float time) : base(properties, time)
    {
        this.Dizzytime = time;
        this.dizzyIntialTime = time;
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
        Dizzytime -= Time.deltaTime;
        if (Dizzytime < 0)
        {
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
            Dizzytime = dizzyIntialTime;
        }
    }
}
