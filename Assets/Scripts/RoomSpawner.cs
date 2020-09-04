using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 - U, needs D door 
    // 2 - L, needs R door
    // 3 - R, needs L door
    // 4 - D, needs U door

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (spawned == false) {
            // UP
            if (openingDirection == 1)
            {
                // Need to spawn a room with a DOWN door
                rand = Random.Range(0, templates.downRooms.Length);
                Instantiate(templates.downRooms[rand], transform.position, templates.downRooms[rand].transform.rotation);
            }
            // DOWN
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a UP door
                rand = Random.Range(0, templates.upRooms.Length);
                Instantiate(templates.upRooms[rand], transform.position, templates.upRooms[rand].transform.rotation);
            }
            // RIGHT
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            // LEFT
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("SpawnPoint")) {
            Destroy(gameObject);
        }
        spawned = true;
    }
}
