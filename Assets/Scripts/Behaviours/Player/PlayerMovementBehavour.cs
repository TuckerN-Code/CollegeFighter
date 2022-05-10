using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehavour : MonoBehaviour
{

    [Header("Component References")]
    public Rigidbody playerRigidBody;

    [Header("Movement Settings")]
    public float movementSpeed = .03f;

    //Stored values
    private Camera mainCamera;
    private Vector3 MovementDirection;


    public void SetupBehaviour()
    {
        
    }

    void SetGameplayCamera()
    {

    }

    public void UpdateMovementData(Vector3 newMovementDirection)
    {
        MovementDirection = newMovementDirection;
    }

    void FixedUpdate()
    {
        MoveThePlayer();
    }
    
    void MoveThePlayer()
    {
        Vector3 movement = MovementDirection * movementSpeed;
        playerRigidBody.MovePosition(transform.position + movement);
    }

    void TurnThePlayer()
    {

    }

}
