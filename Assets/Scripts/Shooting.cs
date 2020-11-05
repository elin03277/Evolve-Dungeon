using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public Camera cam;

    public float swordSetDamage = 20f;
    public float projectileForce = 20f;

    public float fireRate = 2f;
    public float fireCooldown = 0f;

    Vector3 mousePos;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        fireCooldown -= Time.fixedDeltaTime;

        if (fireCooldown < 0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                // Reference and inspector if need to play more
                FindObjectOfType<AudioManager>().Play("SwordFire");
                shoot();
                fireCooldown = 1f / fireRate;
            }
        }
    }

    void shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<SwordProjectile>().swordDamage = swordSetDamage;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector3 rotationZ = mousePos - firePoint.position;
        float angle = Mathf.Atan2(rotationZ.y, rotationZ.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;  //Quaternion.Euler(0f, 0f, angle);
        rb.AddForce(rotationZ * projectileForce, ForceMode2D.Impulse);
    }
}
