using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float LoadDelay = 2.0f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    
    AudioSource AudioSource;

    private void Start() 
    {
        AudioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
             case "Fuel":
             Debug.Log("Safe");
             break;
             case "Friendly":
             Debug.Log("Start");
             break;
             case "Finish":
             Debug.Log("Finished");
             NextLevelSequence();
             break;
             default:
             Debug.Log("Explode");
             StartCrashSequence();
             break;
        }
    }

    private void NextLevelSequence()
    {
        AudioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel" , LoadDelay);
    }

    void StartCrashSequence()
    {
        AudioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel" , LoadDelay);
    }

    void ReloadLevel()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScene);
    }

    void NextLevel()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        if(CurrentScene == SceneManager.sceneCountInBuildSettings - 1)
        {
            CurrentScene = -1;
        }
            SceneManager.LoadScene(CurrentScene+1);
    }
}
