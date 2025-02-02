using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool paused = false; // To track if the game is paused
    public GameObject panel; // Reference to the pause menu panel

    void Update()
    {
        // Toggle pause on Space key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!paused)
            {
                Time.timeScale = 0; // Pause the game
                paused = true;
                panel.SetActive(true); // Show the pause menu
            }
            else
            {
                Time.timeScale = 1; // Resume the game
                paused = false;
                panel.SetActive(false); // Hide the pause menu
            }
        }

        // Go to the previous scene on Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}