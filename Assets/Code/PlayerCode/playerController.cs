using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using weaponCode;

namespace PlayerCode
{
   public class playerController : MonoBehaviour
   {

      // Components
      private Rigidbody2D playerRB;
      private Animator animator;
      private SpriteRenderer spriteRenderer;
      private Vector3 originalScale;
      private weaponController weaponCtrl;
      public GameObject pauseMenuScreen;
      public GameObject shopMenuScreen;
      public GameObject upgradeMenuScreen;
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
      public TextMeshProUGUI spaceSuitLevelText;
      public TextMeshProUGUI jetPackLevelText;
      public TextMeshProUGUI powerSystemLevelText;
      public TextMeshProUGUI spaceSuitPriceText;
      public TextMeshProUGUI jetPackPriceText;
      public TextMeshProUGUI powerSystemPriceText;

      [Header("Audio")]
      public AudioSource audioSource;
      public AudioClip hurtSound;
      public AudioClip walkSound;

      public AudioSource oneShotSource;
      
      //Player upgrade
      public int spaceSuitLevel = 0;
      public int jetPackLevel = 0;
      public int powerSystemLevel = 0;

      

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
         upgradeMenuScreen.SetActive(false);
         if (currentWeapon != null)
         {
            weaponCtrl = currentWeapon.GetComponent<weaponController>();
         }
      }

      void Update()
      {
         HandleMovement();
         HandleJumping();
         HandleGroundCheck();
         //HandleShooting();
         HandleCoinsUI();
         HandlePlayerFlipWithMouse();

         //HandleGunAiming();
      }

      // --------------------------------------------------
      private void HandleMovement()
      {
         float moveInput = Input.GetAxisRaw("Horizontal");
         bool isMoving = moveInput != 0;

         playerRB.velocity = new Vector2(moveInput * speed, playerRB.velocity.y);
         animator.SetFloat("moving", Mathf.Abs(playerRB.velocity.x));

         if (isMoving && isGrounded)
         {
            if (!audioSource.isPlaying || audioSource.clip != walkSound)
            {
               audioSource.clip = walkSound;
               audioSource.loop = true;
               audioSource.Play();
            }
         }
         else
         {
            if (audioSource.clip == walkSound)
            {
               audioSource.Stop();
            }
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

      // private void HandleShooting()
      // {
      //    if (Input.GetMouseButtonDown(0))
      //    {
      //       GameObject newProjectile = Instantiate(projectile);

      //       Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      //       mousePosition.z = 0;
      //       Vector2 shootDirection = (mousePosition - firePoint.position).normalized;

      //       newProjectile.transform.position = firePoint.position;

      //       Projectile.ProjectileController projectileScript = newProjectile.GetComponent<Projectile.ProjectileController>();
      //       if (projectileScript != null)
      //       {
      //          projectileScript.SetDirection(shootDirection);
      //       }
      //    }
      // }
      // private void HandleGunAiming()
      // {
      //    if (gunTransform == null) return;

      //    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      //    mousePosition.z = 0;

      //    Vector3 direction = mousePosition - gunTransform.position;
      //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      //    angle = Mathf.Clamp(angle, -90f, 90f);
      //    gunTransform.rotation = Quaternion.Euler(0, 0, angle);
      // }


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
      
      public void GoToUpgrade()
      {
         upgradeMenuScreen.SetActive(true);
         shopMenuScreen.SetActive(false);
      }
      
      public void BackToShop()
      {
         upgradeMenuScreen.SetActive(false);
         shopMenuScreen.SetActive(true);
      }
      public void LeaveShop()
      {
         shopMenuScreen.SetActive(false);
         pauseMenuScreen.SetActive(true);
      }
      
      public void LeaveUpgrade()
      {
         upgradeMenuScreen.SetActive(false);
         pauseMenuScreen.SetActive(true);
      }

      public void TakeDamage()
      {
         if (hurtSound != null && oneShotSource != null)
         {
            oneShotSource.PlayOneShot(hurtSound);
         }
         StartCoroutine(FlashRed());
      }

      private void HandleCoinsUI()
      {
         coinsText.text = numberOfCoins.ToString();
      }
      public void OnBuyRifleAmmoClicked()
      {
         weaponController foundWeapon = FindObjectOfType<weaponController>();
         if (foundWeapon != null)
         {
            foundWeapon.BuyRifleAmmo();
         }
         else
         {
            Debug.LogError("Cannot buy rifle ammo - weapon controller reference is missing!");
         }
      }

      public void OnBuyShotgunAmmoClicked()
      {
         weaponController foundWeapon = FindObjectOfType<weaponController>();

         if (foundWeapon != null)
         {
            foundWeapon.BuyShotgunAmmo();
         }
         else
         {
            Debug.LogError("Cannot buy shotgun ammo - no weaponController found in scene!");
         }
      }
      public void OnBuyPistolAmmoClicked()
      {
         weaponController foundWeapon = FindObjectOfType<weaponController>();

         if (foundWeapon != null)
         {
            foundWeapon.BuyPistolAmmo();
         }
         else
         {
            Debug.LogError("Cannot buy pistol ammo - no weaponController found in scene!");
         }
      }
      private void HandlePlayerFlipWithMouse()
      {
         Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

         if (mousePosition.x < transform.position.x)
         {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
         }
         else
         {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
         }
      }
      
      public void HealthUpgrade()
      {
         var cost = (spaceSuitLevel + 1) * 5;
         if (numberOfCoins >= cost && spaceSuitLevel < 2)
         {
            numberOfCoins -= cost;
            PlayerPrefs.SetInt("Coins", numberOfCoins);
            PlayerPrefs.Save();
            spaceSuitLevel++;
            PlayerHealth playerHeath = FindObjectOfType<PlayerHealth>();
            playerHeath.IncreaseHealth(200);
            if (spaceSuitLevel == 1)
            {
               spaceSuitLevelText.text = "Level 2";
               spaceSuitPriceText.text = "-10";
            } else if (spaceSuitLevel == 2) 
            {
               spaceSuitLevelText.text = "Max Level";
               spaceSuitPriceText.text = "0";
            }
         }
      }
      public void JumpUpgrade()
      {
         var cost = (jetPackLevel + 1) * 5;
         if (numberOfCoins >= cost && jetPackLevel < 2)
         {
            numberOfCoins -= cost;
            PlayerPrefs.SetInt("Coins", numberOfCoins);
            PlayerPrefs.Save();
            jetPackLevel++;
            firstJumpForce += jetPackLevel * 1f;
            secondJumpForce += jetPackLevel * 1f;
            if (jetPackLevel == 1)
            {
               jetPackLevelText.text = "Level 2";
               jetPackPriceText.text = "-10";
            } else if (jetPackLevel == 2) 
            {
               jetPackLevelText.text = "Max Level";
               jetPackPriceText.text = "0";
            }
         }
      }
      
      public void SpeedUpgrade()
      {
         var cost = (powerSystemLevel + 1) * 5;
         if (numberOfCoins >= cost && powerSystemLevel < 2)
         {
            numberOfCoins -= cost;
            PlayerPrefs.SetInt("Coins", numberOfCoins);
            PlayerPrefs.Save();
            powerSystemLevel++;
            speed += powerSystemLevel * 2f;
            if (powerSystemLevel == 1)
            {
               powerSystemLevelText.text = "Level 2";
               powerSystemPriceText.text = "-10";
            } else if (powerSystemLevel == 2) 
            {
               powerSystemLevelText.text = "Max Level";
               powerSystemPriceText.text = "0";
            }
         }
      }

   }


}
