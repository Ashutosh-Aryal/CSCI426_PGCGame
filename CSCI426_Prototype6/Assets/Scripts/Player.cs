using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private Rigidbody2D rb2d;
    private BoxCollider2D box2d;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpVelocity;
    private bool canDoubleJump;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        box2d = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (IsGrounded())
        {
            canDoubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                rb2d.velocity = Vector2.up * jumpVelocity;
            }
            else
            {
                if (canDoubleJump)
                {
                    rb2d.velocity = Vector2.up * jumpVelocity;
                    canDoubleJump = false;
                }
            }
        }

        // Movement
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(x, y);
        Movement(direction);
    }

    private bool IsGrounded()
    {
        RaycastHit2D ray = Physics2D.BoxCast(box2d.bounds.center, box2d.bounds.size, 0f, Vector2.down, 0.1f, layerMask);
        return ray.collider != null; 
    }

    private void Movement(Vector2 direction)
    {
        rb2d.velocity = new Vector2(direction.x * moveSpeed, rb2d.velocity.y);
    }

    public void JumpRefresh()
    {
        canDoubleJump = true;
    }
}
