using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum positional_State
{
    Airborn,
    Standing,
    Crouching,
    Knocked_Down
}

public enum Movement_State
{
    Able_To_Airbone_Move,
    Able_To_Grounded_Move,
    Unable_To_Move
}

public class PlayerStateManager
{
    //Positional State
    public positional_State state_Position { get; private set; }


    //Actionable States
    public Movement_State state_Movement { get; private set; }
    public bool state_Able_To_Attack { get; private set; }
    public bool state_Able_To_Jump_Cancel { get; private set; }


    //Status States
    public bool state_In_Hitlag { get; private set; }
    public bool state_Starting_Attack { get; private set; }
    public bool state_Airborne_Juggle { get; private set; }
    public bool state_Airborne_Reset { get; private set; }


    public PlayerStateManager()
    {
        state_Position = positional_State.Standing;

        state_Movement = Movement_State.Unable_To_Move;
        state_Able_To_Attack = false;
        state_Able_To_Jump_Cancel = false;

        state_In_Hitlag = false;
        state_Starting_Attack = false;
        state_Airborne_Juggle = false;
        state_Airborne_Reset = false;
    }
    public void setJumping()
    {
        state_Position = positional_State.Airborn;
        state_Movement = Movement_State.Able_To_Airbone_Move;
        state_Able_To_Jump_Cancel = false;
    }
    public void setStanding()
    {
        state_Position = positional_State.Standing;
        state_Movement = Movement_State.Able_To_Grounded_Move;
    }
    public void setAttacking()
    {
        state_Starting_Attack = true;
        state_Movement = Movement_State.Unable_To_Move;
    }
    public void AttackActive()
    {
        state_Starting_Attack = false;
    }
    public void AttackFinished()
    {
        state_Movement = Movement_State.Able_To_Grounded_Move;
        state_Able_To_Jump_Cancel = false;
    }
    public void setCrouching()
    {
        state_Position = positional_State.Crouching;
    }

    public void AttackJumpCancelable()
    {
        state_Able_To_Jump_Cancel = true;
    }
}
