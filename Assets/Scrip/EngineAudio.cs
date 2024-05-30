using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public AudioSource runningSound;
    public float runningMaxVolume;
    public float runningMaxPitch;
    public AudioSource idleSound;
    public float idleMaxVolume;
    public float idleingMaxPitch;
    private CarControll carControll;
    public float velocityRatio;
   
    
    void Start()
    {
        carControll = GetComponent<CarControll>();  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(carControll != null)
        {
            velocityRatio=carControll.carVelocityRatio;
           
            
        }
        runningSound.volume=Mathf.Lerp(0.1f,runningMaxVolume,velocityRatio);
        runningSound.pitch= Mathf.Lerp(0.3f,runningMaxPitch,velocityRatio);
    }
}
