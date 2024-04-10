using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzyIdle : BasePlayerState
{
    float Dizzytime;
    float dizzyIntialTime;
    public DizzyIdle(PlayerStateProperties properties, float time) : base(properties, time)
    {
        this.Dizzytime = time ;
        this.dizzyIntialTime = time;
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
        Dizzytime -= Time.deltaTime;
        if (Dizzytime < 0)
        {
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
            Dizzytime = dizzyIntialTime;
        }
    }
}
