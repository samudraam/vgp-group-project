using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float jumpForce = 8f;
    public float attackRange = 2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null) return;

        HandleGroundCheck();
        MoveTowardsPlayer();
        TryAttack();
    }

    void HandleGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void MoveTowardsPlayer()
    {
        float distance = player.position.x - transform.position.x;

        if (Mathf.Abs(distance) > attackRange)
        {
            float direction = Mathf.Sign(distance);
            rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
            sr.flipX = direction < 0;
            animator.SetFloat("moving", Mathf.Abs(rb.velocity.x));

            if (isGrounded && !IsPathClear())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                animator.SetTrigger("jump");
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetFloat("moving", 0);
        }
    }

    void TryAttack()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        Debug.Log("Distance to player: " + distance);

        if (distance <= attackRange)
        {
            Debug.Log("SLASH TRIGGERED");
            animator.SetTrigger("slash");
        }
    }

    bool IsPathClear()
    {
        Vector2 direction = sr.flipX ? Vector2.left : Vector2.right;
        return !Physics2D.Raycast(transform.position, direction, 0.5f, groundLayer);
    }
}
