using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField]
    float parallaxFactor;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.parent.transform.position.x * parallaxFactor, 0f, 10f); 
    }
}
