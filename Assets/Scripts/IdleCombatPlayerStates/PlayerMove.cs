using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerGroundedState
{
    float highkicktime;
    float highKickIntialTime;
    public PlayerMove(PlayerStateProperties properties, float time) : base(properties, time)
    {
        this.highKickIntialTime = time;
        this.highkicktime = time;
    }

    public override void enter()
    {
        base.enter();
        if(isPlayer == false)
        properties.Player.move(2);
    }

    public override void exit()
    {
        base.exit();
    }

    public override void update()
    {
        base.update();
        if(isPlayer == true)
        {
            properties.Player.move(inputX);
            if (inputX == 0)
            {
                properties.StateMachine.ChangeState(properties.Player.playerIdle);
            }
        }
        if(isPlayer == false) 
        {
           
        }
  
    }
}
