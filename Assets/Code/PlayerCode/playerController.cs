using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCode
{
   public class playerController : MonoBehaviour
   {
      Rigidbody2D playerRB;
      Animator animator;
      float speed = 5f;
      private Vector3 originalScale;
      public GameObject projectile;

      //jump vars 
      private int maxJumps = 2;
      private int currentJumps = 0;
      public float firstJumpForce = 8f;
      public float secondJumpForce = 5f;

      // Ground check (I think this is broken)
      public Transform groundCheck;
      public float groundCheckRadius = 0.2f;
      public LayerMask groundLayer;
      private bool isGrounded;

      //projectile vars
      private int maxProjectiles = 10;
      List<GameObject> activeProjectiles = new List<GameObject>();


      // Start is called before the first frame update
      void Start()
      {
         playerRB = GetComponent<Rigidbody2D>();
         animator = GetComponent<Animator>();
         originalScale = transform.localScale;
      }

      // Update is called once per frame
      void Update()
      {
         //-----------------------------------------------------------------------------------------------------
         //Movement

         float moveInput = Input.GetAxisRaw("Horizontal");
         playerRB.velocity = new Vector2(moveInput * speed, playerRB.velocity.y);
         animator.SetFloat("moving", Mathf.Abs(playerRB.velocity.x));

         if (moveInput != 0)
         {
            transform.localScale = new Vector3(originalScale.x * Mathf.Sign(moveInput), originalScale.y, originalScale.z);
         }

         isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

         if (isGrounded)
         {
            currentJumps = 0;
         }

         if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && currentJumps < maxJumps)
         {
            float jumpForce = (currentJumps == 0) ? firstJumpForce : secondJumpForce;
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            currentJumps++;
         }

         //-----------------------------------------------------------------------------------------------------
         //Projectiles 

         if (Input.GetMouseButtonDown(0))
         {
            GameObject newProjectile = Instantiate(projectile);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Vector2 shootDirection = (mousePosition - transform.position).normalized;

            float offset = 0.5f;
            newProjectile.transform.position = (Vector2)transform.position + shootDirection * offset;

            Projectile.ProjectileControler projectileScript = newProjectile.GetComponent<Projectile.ProjectileControler>();
            if (projectileScript != null)
            {
               projectileScript.SetDirection(shootDirection);
            }
         }

      }

   }
}