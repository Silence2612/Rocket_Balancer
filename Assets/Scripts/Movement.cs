using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    
    [SerializeField] InputAction Rotation;

    [SerializeField] AudioSource AudioSource;
    [SerializeField] float ThrustStrength = 10;
    [SerializeField] float RotateSpeed = 10;
    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
    }
    private void OnEnable() {
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
            if(!AudioSource.isPlaying)
            {
                AudioSource.Play();        
            }
        }
        else{
            AudioSource.Stop();
        }
    }

     private void  Process_Rotation(){
         float R_val = Rotation.ReadValue<float>();
         if(R_val > 0)
        {
            ApplyRotation(R_val);
        }
        else if(R_val < 0){
            ApplyRotation(R_val);         }
    }

    private void ApplyRotation(float R_val)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * RotateSpeed * Time.fixedDeltaTime * -R_val);
        rb.freezeRotation = false;
    }
}
