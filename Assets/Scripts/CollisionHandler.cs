using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float LoadDelay = 2.0f;
    [SerializeField] AudioClip SuccessSFX;
    [SerializeField] AudioClip CrashSFX;
    [SerializeField] ParticleSystem SuccessParticles;
    [SerializeField] ParticleSystem CrashParticles;
    [SerializeField] ParticleSystem BabyParticles;


    
    AudioSource AudioSource;
    bool IsControllable = true;
    bool IsCrashable = true;

    private void Start() 
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        RespondToDebugKeys();
        if(!IsCrashable)
            {
                Debug.Log("Baby Mode");
            }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(IsControllable == true)
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
                NextLevelSequence(true);
                break;
                default:
                if(IsCrashable)
                {
                    Debug.Log("Explode");
                    StartCrashSequence();
                }
                else
                {
                    StartBabymode();
                }
                break;
            }
        }
    }

    void StartBabymode()
    {
        BabyParticles.Play();
    }

    private void NextLevelSequence(bool var)
    {
        if(var)
        {
            SuccessParticles.Play();
            AudioSource.Stop();
            AudioSource.PlayOneShot(SuccessSFX);
        }
        IsControllable = false;
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel" , LoadDelay);
    }

    void StartCrashSequence()
    {
        IsControllable = false;
        AudioSource.Stop();
        AudioSource.PlayOneShot(CrashSFX);
        CrashParticles.Play();
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

    void RespondToDebugKeys()
    {
        if(Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadDelay = 0.3f;
            NextLevelSequence(false);
        }
        else if(Keyboard.current.cKey.wasPressedThisFrame)
        {
            IsCrashable = !IsCrashable;
        }
    }
}
