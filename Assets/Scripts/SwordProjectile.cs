using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    // public Rigidbody2D rb;
    public Transform swordPoint;

    public LayerMask swordCollisionLayers;

    public float swordRange = 1.2f;

    public float swordDamage = 20f;

    // Start is called before the first frame update
    void Start()
    {
        //swordDamage = 20f;
    }

  void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(swordPoint.position, swordRange);
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(swordPoint.position, swordRange, swordCollisionLayers);

        foreach (Collider2D obj in hitObjects)
        {
            if(obj.name == "Skeleton(Clone)")
            {
                // Debug.Log(swordDamage);
                obj.GetComponent<Enemy>().TakeEnemyDamage(swordDamage);
            }
            // Debug.Log("We hit: " + obj.name);
            Destroy(gameObject);

        }
    }

}
