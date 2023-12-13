using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlaySoundOnButtonPress : MonoBehaviour
{
    public AudioSource audioSource; // Assign the AudioSource component to this variable in the Inspector window

    // Function that will be called when the button is pressed
    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            UnityEngine.Debug.LogError("AudioSource is not assigned. Please assign it in the Inspector window.");
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene("Test scene");
    }
}