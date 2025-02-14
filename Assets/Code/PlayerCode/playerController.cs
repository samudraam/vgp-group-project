using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerCode
{
   {
      Rigidbody2D playerRB;
      Animator animator;
      float speed = 5f;
      private Vector3 originalScale;

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
         {
            playerRB.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
         }

         playerRB.velocity = new Vector2(moveInput * speed, playerRB.velocity.y);

         animator.SetFloat("moving", Mathf.Abs(playerRB.velocity.x));

         if (moveInput != 0)
         {
            transform.localScale = new Vector3(originalScale.x * Mathf.Sign(moveInput), originalScale.y, originalScale.z);
         }

      }
   }
}
