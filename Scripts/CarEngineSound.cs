using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CarEngineSound : MonoBehaviour
{
    public float maxPitch;
    public float minPitch;

    public AudioSource engine;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate ()
    {
        float curSpeed = rb.velocity.magnitude;
        float pitch = curSpeed / 30.0f;

        engine.pitch = minPitch + pitch;
        
        if (engine.pitch < minPitch)
        {
            engine.pitch = minPitch;
        }
        else if (engine.pitch > maxPitch)
        {
            engine.pitch = maxPitch;
        }
    }
}
