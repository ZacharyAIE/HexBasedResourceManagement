using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMover : MonoBehaviour
{   
    CharacterController cc;
    Vector2 moveInput = new Vector2();
    bool jumpInput = false;

    public Vector3 hitDirection;
    public float speed = 10;
    public Vector3 velocity = new Vector3();
    public float jumpVelocity = 10;
    public bool isGrounded = false;

    public float physicsImpactStrength = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        

        Vector3 delta;
        delta = (moveInput.x * Vector3.right + moveInput.y * Vector3.forward) * speed * Time.fixedDeltaTime;
        if (isGrounded || moveInput.x != 0 || moveInput.y != 0)
        {
            velocity.x = delta.x;
            velocity.z = delta.z;
        }

        // check for jumping
        if (jumpInput && isGrounded)
            velocity.y = jumpVelocity;
        
        // check if we've hit ground from falling. If so, remove our velocity
        if (isGrounded && velocity.y < 0)
            velocity.y = 0;

        // apply gravity
        velocity += Physics.gravity * Time.fixedDeltaTime;

        if (!isGrounded)
            hitDirection = Vector3.zero;

        // slide objects off surfaces they're hanging on to
        if (moveInput.x == 0 && moveInput.y == 0)
        {
            Vector3 horizontalHitDirection = hitDirection;
            horizontalHitDirection.y = 0;
            float displacement = horizontalHitDirection.magnitude;
            if (displacement > 0)
                velocity -= 0.2f * horizontalHitDirection / displacement;
        }

        // and apply this to our positional update this frame
        delta += velocity * Time.fixedDeltaTime;

        cc.Move(delta);
        isGrounded = cc.isGrounded;
    }

    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        Rigidbody body = hit.collider.GetComponent<Rigidbody>();
        if (body != null)
        {
            body.AddForce(100 * -hit.normal);
        }
        else
            hitDirection = hit.point - transform.position;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        moveInput.x = -ctx.ReadValue<Vector2>().x;
        moveInput.y = -ctx.ReadValue<Vector2>().y;
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.action.triggered && ctx.action.ReadValue<float>() > 0)
            jumpInput = true;
        else
            jumpInput = false;
    }
}
