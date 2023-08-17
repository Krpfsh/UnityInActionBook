using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed = 3.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,speed,0);
    }
}
