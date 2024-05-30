using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    
    void FixedUpdate()
    {
        transform.Rotate(0.0f, 0.5f, 0.0f);
    }
}
