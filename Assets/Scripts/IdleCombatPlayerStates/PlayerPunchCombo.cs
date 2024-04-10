using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunchCombo : BasePlayerState
{
    float punchtime;
    float punchIntialTime;
    public PlayerPunchCombo(PlayerStateProperties properties, float time) : base(properties, time)
    {
        punchtime = time;
        punchIntialTime = time;
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
        punchtime -= Time.deltaTime;
        if (punchtime < 0.0f)
        {
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
            punchtime = punchIntialTime;
        }
    }
}
