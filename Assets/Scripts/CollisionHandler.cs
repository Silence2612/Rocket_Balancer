using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag){
             case "Fuel":
             Debug.Log("Safe");
             break;
             case "Friendly":
             Debug.Log("Start");
             break;
             case "Finish":
             Invoke("NextLevel" , 2.0f);
             Debug.Log("Finished");
             break;
             default:
             Debug.Log("Explode");
             Invoke("ReloadLevel" , 2.0f);
             break;
        }
    }
    void ReloadLevel(){
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScene);
    }

    void NextLevel(){
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        if(CurrentScene == SceneManager.sceneCountInBuildSettings - 1){
            CurrentScene = -1;
        }
            SceneManager.LoadScene(CurrentScene+1);
    }
}
