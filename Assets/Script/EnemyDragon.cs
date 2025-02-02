using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject dragonEggPrefab; // Prefab for the dragon's egg
    public float speed = 1f; // Movement speed of the dragon
    public float timeBetweenEggDrops = 1f; // Time interval for dropping eggs
    public float leftRightDistance = 10f; // Distance dragon moves left and right
    public float chanceDirections = 0.1f; // Chance of changing direction

    void Start()
    {
        Invoke("DropEgg", 2f); // Start dropping eggs after 2 seconds
    }

    void DropEgg()
    {
        Vector3 myVector = new Vector3(0f, 5f, 0f);

        if (dragonEggPrefab != null) // Ensure prefab is valid
            {
                // Instantiate a new egg at the dragon's position
                GameObject egg = Instantiate(dragonEggPrefab, transform.position, Quaternion.identity);
                egg.transform.position = transform.position + myVector;

                // Schedule the next egg drop
                Invoke("DropEgg", timeBetweenEggDrops);

        }

        // Vector offset for where the egg will spawn
        

    }

    void Update()
    {
        // Move the dragon horizontally
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Reverse direction at the boundaries
        if (pos.x < -leftRightDistance)
        {
            speed = Mathf.Abs(speed); // Move right
        }
        else if (pos.x > leftRightDistance)
        {
            speed = -Mathf.Abs(speed); // Move left
        }
    }

    private void FixedUpdate()
    {
        // Random chance to reverse direction
        if (Random.value < chanceDirections)
        {
            speed *= -1;
        }
    }
}
