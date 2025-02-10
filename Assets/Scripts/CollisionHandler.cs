using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float LoadDelay = 2.0f;
    
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
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel" , LoadDelay);
    }

    void StartCrashSequence()
    {
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
        if(CurrentScene == SceneManager.sceneCountInBuildSettings - 1){
            CurrentScene = -1;
        }
            SceneManager.LoadScene(CurrentScene+1);
    }
}
