using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehavoir : MonoBehaviour
{
    public Transform lookTarget;
    public Transform followTarget;
    [Range (0, 1)] public float smoothTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void LateUpdate ()
    {
        
    }
}
