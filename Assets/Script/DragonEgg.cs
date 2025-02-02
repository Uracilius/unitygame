using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEgg : MonoBehaviour
{
    public static float bottomY = -8; // The Y position at which the egg is destroyed
    public AudioSource audioSource; // AudioSource for playing sound

    void Start()
    {
        // Initialize any required setup for the egg
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Add any logic here if needed when a trigger collision occurs
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (ps != null)
        {
            var em = ps.emission;
            em.enabled = true;
        }

        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.enabled = false;
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        // Logic to check if the egg falls below the bottomY position
        if (this.gameObject.name == "DragonEgg")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 5000, transform.position.z);
        }

        if (transform.position.y < bottomY)
        {
            if (this.gameObject.name == "DragonEgg(Clone)")
            {
                ParticleSystem ps = GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    var em = ps.emission;
                    em.enabled = true;
                }

                if (audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                Destroy(this.gameObject, 1);
                DragonPicker apScript = Camera.main.GetComponent<DragonPicker>();
                if (apScript != null)
                {
                    apScript.DragonEggDestroyed();
                }
            }
        }
    }
}