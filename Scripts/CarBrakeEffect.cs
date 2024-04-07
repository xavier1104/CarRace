using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[System.Serializable]
public class WheelEffect
{
    public WheelCollider wheel;
    public TrailRenderer trail;
    public ParticleSystem smoke;
}

public class CarBrakeEffect : MonoBehaviour
{
    public List<WheelEffect> effects;
    public AudioSource brake;
    public float effectThreshold;
    public float sidewayThreshold;
    // Start is called before the first frame update
    void Start ()
    {

    }

    // Update is called once per frame
    private void Update ()
    {
        bool isSideWaySlip = false;
        foreach (WheelEffect effect in effects)
        {
            WheelHit hit;
            effect.wheel.GetGroundHit (out hit);
            
            if (Mathf.Abs (hit.sidewaysSlip) + Mathf.Abs (hit.forwardSlip) > effectThreshold)
            {
                effect.trail.emitting = true;
                effect.smoke.Emit (1);
            }
            else
            {
                effect.trail.emitting = false;
            }

            if (Mathf.Abs (hit.sidewaysSlip) > sidewayThreshold)
            {
                isSideWaySlip = true;
            }
        }

        if (isSideWaySlip)
        {
            if (!brake.isPlaying)
            {
                brake.Play ();
            }
        }
        else
        {
            brake.Stop ();
        }
    }
}
