using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    public GameManager gameManager;
    public LevelGenerator levelGenerator;
    public Shooting sword;
    public HealthBar healthBar;
    public Door door;

    public AttackImage attackImage;
    public SpeedImage speedImage;

    public Rigidbody2D rb;
    public Animator animator;

    public Transform playerPoint;
    public LayerMask playerCollisionLayers;

    //public Sprite door;
     
    public float maxHealth;
    public float currentHealth;

    public float damageTaken;
    public float damageHealed;

    public bool doorOn = false;
    public bool invincibilityOn = false;
    public bool attackPotionOn = false;
    public bool speedPotionOn = false;

    public float doorTimer = 10f;
    public float invincibilityTimer;
    public float attackPotionTimer = 10f;
    public float speedPotionTimer = 10f;

    public float attackPotionSetTimer = 10f;
    public float speedPotionSetTimer = 10f;
    
    public float playerRangeLength = 1.1f;
    public float playerRangeWidth = 1.1f;

    public int killCount = 0;
   // public Camera cam;

    Vector2 movement;
    //Vector2 mousePos;

    // Update is called once per frame
    
    void Start()
    {
        speed = 5f;
        invincibilityTimer = 1f;
        doorTimer = 10f;
        attackPotionTimer = attackPotionSetTimer;
        speedPotionTimer = speedPotionSetTimer;

        invincibilityOn = false;
        doorOn = false;
        attackPotionOn = false;
        speedPotionOn = false;

        gameManager = FindObjectOfType<GameManager>();
        levelGenerator = FindObjectOfType<LevelGenerator>();

        attackImage = FindObjectOfType<AttackImage>();
        attackImage.gameObject.SetActive(false);

        speedImage = FindObjectOfType<SpeedImage>();
        speedImage.gameObject.SetActive(false);

        door = FindObjectOfType<Door>();
        door.gameObject.SetActive(false);

        currentHealth = maxHealth;
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);
        //.GetComponent<HealthBar>().SetMaxHealth(maxHealth);
        //healthBar.SetMaxHealth(maxHealth);
        //.GetComponent<HealthBar>().SetMaxHealth(maxHealth);
        //healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        //if (currentHealth == 0)
        //{
        //    gameObject.SetActive(false);
        //    gameManager.GameOverOn();
        //}

        healthBar.SetHealth(currentHealth);
    }

    public void Heal(float healed)
    {
        currentHealth += healed;
        healthBar.SetHealth(currentHealth);
    }
    
    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);

        if (invincibilityOn)
        {
            invincibilityTimer -= Time.fixedDeltaTime;

            if (invincibilityTimer < 0)
            {
                invincibilityOn = false;
                invincibilityTimer = 2f;
            }
        }

        if (!doorOn)
        {
            doorTimer -= Time.fixedDeltaTime;

            if (doorTimer < 0)
            {
                door.gameObject.SetActive(true);
                doorOn = true;
            }
        }

        if (attackPotionOn)
        {
            attackPotionTimer -= Time.fixedDeltaTime;
            
            if (attackPotionTimer < 0)
            {
                sword.swordSetDamage /= 2;
                attackPotionTimer = attackPotionSetTimer;
                attackPotionOn = false;
                attackImage.gameObject.SetActive(false);
            }
        }

        if (speedPotionOn)
        {
            speedPotionTimer -= Time.fixedDeltaTime;

            if (speedPotionTimer < 0)
            {
                speed /= 2;
                sword.fireRate /= 2;
                speedPotionTimer = speedPotionSetTimer;
                speedPotionOn = false;
                speedImage.gameObject.SetActive(false);
            }
        }

        // mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //Vector2 lookDir = mousePos - rb.position;
        // float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //rb.rotation = angle;
    }

    //void OnDrawGizmosSelected()
    // {
    //     Gizmos.DrawWireCube(playerPoint.position, new Vector2(playerRangeWidth, playerRangeLength));
    // }

    void OnCollisionEnter2D(Collision2D col)
    {

        Collider2D[] hitObjects = Physics2D.OverlapBoxAll(playerPoint.position, new Vector2(playerRangeWidth, playerRangeLength), 0f, playerCollisionLayers);

        foreach (Collider2D obj in hitObjects)
        {
            if (obj.name == "Skeleton(Clone)") {
                if (!invincibilityOn)
                {
                    invincibilityOn = true;
                    TakeDamage(damageTaken);
                }
            }

            if (obj.name == "HealthPotion(Clone)" && currentHealth != maxHealth)
            {
                Heal(damageHealed);
                Destroy(col.gameObject);
            }

            if (obj.name == "AttackPotion(Clone)" && attackPotionOn != true)
            {
                attackImage.gameObject.SetActive(true);
                sword.swordSetDamage *= 2;
                attackPotionOn = true;

                Destroy(col.gameObject);
            }

            if (obj.name == "SpeedPotion(Clone)" && speedPotionOn != true)
            {
                speedImage.gameObject.SetActive(true);
                speed *= 2;
                sword.fireRate *= 2;
                speedPotionOn = true;

                Destroy(col.gameObject);
            }

            if (!gameManager.gemCreated)
            {
                if (obj.name == "Tilemap_Door")
                {
                    playerPoint.position = Vector2.zero;// new Vector2(0, 0);
                    doorOn = false;
                    doorTimer = 10f;
                    door.gameObject.SetActive(false);
                    levelGenerator.RestartLevel();
                    gameManager.ResetKills();
                }
            }

            if (obj.name == "LevelGem(Clone)")
            {
                gameManager.IncrementRoomCleared();

                //if (gameManager.roomsCleared >= 3)
                //{
                //    gameManager.GameOverOn();
                //}
                //else
                //{
                    doorOn = false;
                    doorTimer = 10f;
                    door.gameObject.SetActive(false);
                    levelGenerator.RestartLevel();
                    gameManager.ResetKills();
                    Destroy(col.gameObject);
               // }
            }
        }
    }

}
