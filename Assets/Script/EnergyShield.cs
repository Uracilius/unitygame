using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyShield : MonoBehaviour
{
    public TMP_Text scoreGT;
    public AudioSource audioSource; // Added for audio functionality
    private bool canCollide = true;

    void Start()
    {
        // Find and initialize the score text
        GameObject scoreGO = GameObject.Find("Score");
        scoreGT = scoreGO.GetComponent<TMP_Text>();
        scoreGT.text = "0";

        // Initialize the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Update the shield's position based on mouse position
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    private void OnCollisionEnter(Collision coll)
    {
        GameObject collided = coll.gameObject;

        // Check if the collided object is a DragonEgg
        if (collided.name == "DragonEgg(Clone)" && canCollide)
        {
            Collider eggCollider = collided.GetComponent<Collider>();
            if (eggCollider != null && eggCollider.enabled)
            {
                eggCollider.enabled = false; // Disable the egg's collider to prevent further collisions

                Debug.Log("Collision detected by: " + this.name);

                // Play collision sound
                if (audioSource != null)
                {
                    audioSource.Play();
                }

                // Destroy the collided egg and update the score
                Destroy(collided);
                int score = int.Parse(scoreGT.text);
                score += 1;
                scoreGT.text = score.ToString();

                // Start the collision cooldown
                StartCoroutine(CollisionCooldown());
            }
        }
    }

    private IEnumerator CollisionCooldown()
    {
        canCollide = false; // Temporarily disable collision
        yield return new WaitForSeconds(1); // Wait for 1 second
        canCollide = true; // Re-enable collision
    }
}