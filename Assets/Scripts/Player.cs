using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    
    public HealthBar healthBar;
    public int tempMaxHealth = 100;
    public int tempCurrentHealth;

    public Transform playerPoint;
    public LayerMask playerCollisionLayers;

    public float playerRangeLength = 1.1f;
    public float playerRangeWidth = 1.1f;

    public Rigidbody2D rb;
    public Animator animator;
   // public Camera cam;

    Vector2 movement;
    //Vector2 mousePos;

    // Update is called once per frame
    
    void Start()
    {
        tempCurrentHealth = tempMaxHealth;
        healthBar.GetComponent<HealthBar>().SetMaxHealth(tempMaxHealth);
        //healthBar.SetMaxHealth(tempMaxHealth);
    }

    void TakeDamage(int damage)
    {
        tempCurrentHealth -= damage;

        // if (tempCurrentHealth == 0)
        // Game over
        // Destroy(gameObject);

        healthBar.GetComponent<HealthBar>().SetHealth(tempCurrentHealth);
        healthBar.SetHealth(tempCurrentHealth);
    }

    void Heal(int healed)
    {
        tempCurrentHealth += healed;
        healthBar.SetHealth(tempCurrentHealth);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

       // mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);

        //Vector2 lookDir = mousePos - rb.position;
       // float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //rb.rotation = angle;
    }

   void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(playerPoint.position, new Vector2(playerRangeWidth, playerRangeLength));
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        Collider2D[] hitObjects = Physics2D.OverlapBoxAll(playerPoint.position, new Vector2(playerRangeWidth, playerRangeLength), 0f, playerCollisionLayers);

        foreach (Collider2D obj in hitObjects)
        {
            if (obj.name == "Skeleton(Clone)") {
                TakeDamage(20);
            }

            if (obj.name == "HealthPotion(Clone)")
            {
                Heal(20);
                Destroy(col.gameObject);
            }
        }
/*
        if (col.tag == "Skeleton")
        {
            TakeDamage(20);
            Debug.Log(tempCurrentHealth);
        }

        if (col.tag == "HealthPotion")
        {
            Heal(20);
        }*/
    }
}
