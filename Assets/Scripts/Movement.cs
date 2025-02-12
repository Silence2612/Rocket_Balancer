using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction Rotation;
    [SerializeField] float ThrustStrength = 10;
    [SerializeField] float RotateSpeed = 10;
    [SerializeField] AudioClip MainEngine;
    [SerializeField] ParticleSystem MainThrust;
    [SerializeField] ParticleSystem RightThrust;
    [SerializeField] ParticleSystem LeftThrust;



    Rigidbody rb;
    AudioSource AudioSource;

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
    }
    private void OnEnable() 
    {
        thrust.Enable();
        Rotation.Enable();
    }


    private void FixedUpdate()
    {
        Thrusting();
        Process_Rotation();
    }

    private void Thrusting()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * ThrustStrength * Time.fixedDeltaTime);
            if (!AudioSource.isPlaying)
            {
                AudioSource.PlayOneShot(MainEngine); 
                  
            }
            if(!MainThrust.isPlaying)
            {
                MainThrust.Play();
            }
        }
        else
        {
            AudioSource.Stop();
            MainThrust.Stop();
        }
    }


    private void  Process_Rotation()
     {
        float R_val = Rotation.ReadValue<float>();
        if(R_val > 0)
        {
            ApplyRotation(R_val);
            if(!LeftThrust.isPlaying)
            {
                LeftThrust.Play();
            }
        }
        else if(R_val < 0)
        {
            ApplyRotation(R_val);
            if(!RightThrust.isPlaying)
            {
                RightThrust.Play();
            }         
        }
        if(!Rotation.IsPressed())
        {
            RightThrust.Stop();
            LeftThrust.Stop();
        }
    }

    private void ApplyRotation(float R_val)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * RotateSpeed * Time.fixedDeltaTime * -R_val);
        rb.freezeRotation = false;
    }
}
