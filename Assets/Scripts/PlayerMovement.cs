using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementMode { Walking, Running, Sprinting, Crouching, Proning}

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public Transform t_mesh;
    public float walkSpeed = 2.25f;
    public float runSpeed = 10.0f;
    private float smoothSpeed;
    private float rotationSpeed = 10.0f;

    private float speed;

    public Rigidbody rb;    

    MovementMode movementMode;

    private Vector3 velocity;

    private void Start()
    {
        speed = walkSpeed;
        rb = GetComponent<Rigidbody>();          //initialize rigibody reference
    }

    void Update()
    {
        // if player is moving, rotate player mesh to match camera facing.
        if (velocity.magnitude > 0)
        {
            rb.velocity = new Vector3(velocity.normalized.x * smoothSpeed, rb.velocity.y, velocity.normalized.z * smoothSpeed);
            smoothSpeed = Mathf.Lerp(smoothSpeed, speed, Time.deltaTime);
            //t_mesh.rotation = Quaternion.LookRotation(velocity);
            t_mesh.rotation = Quaternion.Lerp(t_mesh.rotation, Quaternion.LookRotation(velocity), Time.deltaTime * rotationSpeed);

        }
        else
        {
            smoothSpeed = Mathf.Lerp(speed, smoothSpeed, Time.deltaTime);
        }
    }

    public Vector3 Velocity { get => rb.velocity; set => velocity = value; }

    public void SetMovementMode(MovementMode mode)
    {
        movementMode = mode;
        switch (mode)
        {
            case MovementMode.Walking:
                {
                    speed = walkSpeed;
                    break;
                }   
            case MovementMode.Running:
                {
                    speed = runSpeed;
                    break;
                }            
            case MovementMode.Crouching:                
                {
                    speed = walkSpeed *0.5f;
                    break;
                }
            case MovementMode.Proning:
                {
                    speed = walkSpeed * 0.25f;
                    break;
                }
        }
    }

    public MovementMode GetMovementMode()
    {
        return movementMode;
    }
}
