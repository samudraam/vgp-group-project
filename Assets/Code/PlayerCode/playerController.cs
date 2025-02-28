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
      public int jumpsLeft;

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
         // //move up (to be changed to jump)  
         // if (Input.GetKey(KeyCode.UpArrow))
         // {
         //    transform.position += new Vector3(0, 0.2f, 0);
         // }
         // //move down (to be changed to fall through platforms/fastfall)  
         // if (Input.GetKey(KeyCode.DownArrow))
         // {
         //    transform.position += new Vector3(0, -0.2f, 0);
         // }
         // //move backwards
         // if (Input.GetKey(KeyCode.LeftArrow))
         // {
         //    transform.position += new Vector3(-0.2f, 0, 0);
         // }
         // //move forewards
         // if (Input.GetKey(KeyCode.LeftArrow))
         // {
         //    transform.position += new Vector3(0.2f, 0, 0);
         // }

         float moveInput = Input.GetAxisRaw("Horizontal");

         //jump on key down
         if (Input.GetKeyDown(KeyCode.UpArrow))
         { if(jumpsLeft>0){
               jumpsLeft--;
               playerRB.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
         }
      
         }
         playerRB.velocity = new Vector2(moveInput * speed, playerRB.velocity.y);

         animator.SetFloat("moving", Mathf.Abs(playerRB.velocity.x));

         if (moveInput != 0)
         {
            transform.localScale = new Vector3(originalScale.x * Mathf.Sign(moveInput), originalScale.y, originalScale.z);
         }
         //shooting mechanics
         if (Input.GetMouseButtonDown(0))
         {
            GameObject newProjectile = Instantiate(projectile);
            newProjectile.transform.position = transform.position;
         }
      }
         void OnCollisionStay2D (Collision2D other) {
         if(other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.7f);
               for(int i = 0; i < hits. Length; i++) {
                  RaycastHit2D hit = hits[i];
                  if(hit.collider.gameObject.layer == LayerMask. NameToLayer("Ground")) {
                     jumpsLeft = 2;
                        }
                     }
               }
            }
         
   }
}