using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace PlayerCode
{
   public class playerController : MonoBehaviour
   {

      // Components
      private Rigidbody2D playerRB;
      private Animator animator;
      private SpriteRenderer spriteRenderer;
      private Vector3 originalScale;
      public GameObject pauseMenuScreen;
      public GameObject shopMenuScreen;
      public GameObject currentWeapon;

      // Movement
      [SerializeField] private float speed = 5f;

      // Jumping
      [SerializeField] private int maxJumps = 2;
      [SerializeField] private float firstJumpForce = 8f;
      [SerializeField] private float secondJumpForce = 5f;
      private int currentJumps = 0;

      // Ground check 
      public Transform groundCheck;
      public float groundCheckRadius = 0.2f;
      public LayerMask groundLayer;
      private bool isGrounded;

      // Projectiles
      [Header("Shooting")]
      public GameObject projectile;
      private int maxProjectiles = 10;
      private List<GameObject> activeProjectiles = new List<GameObject>();

      [Header("Gun Settings")]
      public Transform firePoint;
      public Transform gunTransform;



      // Coins
      [Header("UI")]
      public static int numberOfCoins;
      public TextMeshProUGUI coinsText;

      // --------------------------------------------------
      void Start()
      {
         numberOfCoins = PlayerPrefs.GetInt("Coins");
         playerRB = GetComponent<Rigidbody2D>();
         animator = GetComponent<Animator>();
         spriteRenderer = GetComponent<SpriteRenderer>();
         originalScale = transform.localScale;
         pauseMenuScreen.SetActive(false);
         shopMenuScreen.SetActive(false);
      }

      void Update()
      {
         HandleMovement();
         HandleJumping();
         HandleGroundCheck();
         HandleShooting();
         HandleCoinsUI();
         HandleGunAiming();
      }

      // --------------------------------------------------
      private void HandleMovement()
      {
         float moveInput = Input.GetAxisRaw("Horizontal");
         playerRB.velocity = new Vector2(moveInput * speed, playerRB.velocity.y);
         animator.SetFloat("moving", Mathf.Abs(playerRB.velocity.x));

         if (moveInput != 0)
         {
            transform.localScale = new Vector3(originalScale.x * Mathf.Sign(moveInput), originalScale.y, originalScale.z);
         }
      }

      private void HandleJumping()
      {
         if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && currentJumps < maxJumps)
         {
            float jumpForce = (currentJumps == 0) ? firstJumpForce : secondJumpForce;
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            currentJumps++;
         }
      }

      private void HandleGroundCheck()
      {
         if (groundCheck == null)
         {
            Debug.LogError("No GroundCheck assigned in Inspector!");
            return;
         }

         isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
         if (isGrounded)
         {
            currentJumps = 0;
         }
      }

      private void HandleShooting()
      {
         if (Input.GetMouseButtonDown(0))
         {
            GameObject newProjectile = Instantiate(projectile);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector2 shootDirection = (mousePosition - firePoint.position).normalized;

            newProjectile.transform.position = firePoint.position;

            Projectile.ProjectileController projectileScript = newProjectile.GetComponent<Projectile.ProjectileController>();
            if (projectileScript != null)
            {
               projectileScript.SetDirection(shootDirection);
            }
         }
      }
      private void HandleGunAiming()
      {
         if (gunTransform == null) return;

         Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         mousePosition.z = 0;

         Vector3 direction = mousePosition - gunTransform.position;
         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
         angle = Mathf.Clamp(angle, -90f, 90f);
         gunTransform.rotation = Quaternion.Euler(0, 0, angle);
      }


      private IEnumerator FlashRed()
      {
         spriteRenderer.color = Color.red;
         yield return new WaitForSeconds(0.2f);
         spriteRenderer.color = Color.white;
      }
      //UI functions
      public void PauseGame()
      {
         Time.timeScale = 0;
         pauseMenuScreen.SetActive(true);
         Debug.Log("Pause key pressed");

      }

      public void ResumeGame()
      {
         Time.timeScale = 1;
         pauseMenuScreen.SetActive(false);
      }

      public void GoToMenu()
      {
         Time.timeScale = 1;
         SceneManager.LoadScene("LevelMenu");
      }

      public void GoToShop()
      {
         shopMenuScreen.SetActive(true);
         pauseMenuScreen.SetActive(false);
      }
      public void LeaveShop()
      {
         shopMenuScreen.SetActive(false);
         pauseMenuScreen.SetActive(true);
      }

      public void TakeDamage()
      {
         StartCoroutine(FlashRed());
      }

      private void HandleCoinsUI()
      {
         coinsText.text = numberOfCoins.ToString();
      }
   }
}
