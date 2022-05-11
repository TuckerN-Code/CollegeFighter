using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBehaviour : MonoBehaviour
{
    private PlayerStateManager m_stateManager;

    public void SetupBehaviour(ref PlayerStateManager stateManager)
    {
        m_stateManager = stateManager;
    }
    public void do_S_Light()
    {
        m_stateManager.setAttacking();
    }
    public void do_C_Light()
    {

    }
    public void do_J_Light()
    {

    }
}
