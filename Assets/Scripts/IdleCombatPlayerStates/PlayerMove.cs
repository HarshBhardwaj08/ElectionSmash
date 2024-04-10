using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerGroundedState
{
    public PlayerMove(PlayerStateProperties properties, float time) : base(properties, time)
    {
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
        properties.Player.move(inputX * -1);
        if (inputX == 0)
        {
            properties.StateMachine.ChangeState(properties.Player.playerIdle);
        }
    }
}
