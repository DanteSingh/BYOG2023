using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;

    public bool CanMove = false;
    public bool CanJump = false;
    public bool CanFlip = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
		if (CanJump) Jump();
        if(CanFlip) Flip();
	}
	private void FixedUpdate()
	{
        if (CanMove) Move();
	}

	private void Move()
	{
		horizontal = Input.GetAxisRaw("Horizontal");
		rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
	}
	private void Jump()
	{
		if (Input.GetButtonDown("Jump") && IsGrounded())
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
		}

		if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
		}
	}
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
