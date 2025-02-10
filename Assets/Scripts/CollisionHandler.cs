using UnityEngine;

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
             Debug.Log("Finished");
             break;
             default:
             Debug.Log("Explode");
             break;
        }
       

    }
}
