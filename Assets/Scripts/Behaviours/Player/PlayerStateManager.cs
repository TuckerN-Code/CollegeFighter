using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager
{
    //Positional States
    public bool state_Airborne { get; private set; }
    public bool state_Standing { get; private set; }
    public bool state_Crouching { get; private set; }
    public bool state_Knocked_Down { get; private set; }

    //Actionable States
    public bool state_Able_To_Airborne_Move { get; private set; }
    public bool state_Able_To_Grounded_Move { get; private set; }
    public bool state_Able_To_Attack { get; private set; }


    //Status States
    public bool state_In_Hitlag { get; private set; }
    public bool state_Starting_Attack { get; private set; }
    public bool state_Airborne_Juggle { get; private set; }
    public bool state_Airborne_Reset { get; private set; }


    public PlayerStateManager()
    {
        state_Airborne = false;
        state_Standing = false;
        state_Crouching = false;
        state_Knocked_Down = false;

        state_Able_To_Grounded_Move = false;
        state_Able_To_Airborne_Move = false;
        state_Able_To_Attack = false;

        state_In_Hitlag = false;
        state_Starting_Attack = false;
        state_Airborne_Juggle = false;
        state_Airborne_Reset = false;
    }
    public void setAirborn()
    { 
        state_Airborne = true;
        state_Standing = false;
        state_Crouching = false;
        state_Able_To_Grounded_Move = false;
    }
    public void setGrounded()
    {
        state_Airborne = false;
        state_Standing = true;
        state_Able_To_Grounded_Move = true;
    }
    public void setAttacking()
    {
        state_Starting_Attack = true;
        state_Able_To_Grounded_Move = false;
    }
    public void AttackActive()
    {
        state_Starting_Attack = false;
    }
    public void AttackFinished()
    {
        state_Able_To_Grounded_Move = false;
    }
    public void setCrouching()
    {
        if(state_Able_To_Grounded_Move)
        {
            state_Standing = false;
            state_Crouching = true;
        }
    }
    public void setStanding()
    {
        if(state_Able_To_Grounded_Move)
        {
            state_Standing = true;
            state_Crouching = false;
        }
    }
}
