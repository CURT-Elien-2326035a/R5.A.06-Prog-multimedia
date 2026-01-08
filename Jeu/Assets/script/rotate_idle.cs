using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIdle : MonoBehaviour
{
    // Time (in seconds) for a full 360° rotation. Default 2 seconds.
    public float period = 2f;
    // Axis to rotate around (default = up/y axis).
    public Vector3 axis = Vector3.up;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Rotate so that a full turn (360°) takes 'period' seconds.
        if (period > 0f)
        {
            float degreesPerSecond = 360f / period;
            transform.Rotate(axis.normalized * degreesPerSecond * Time.deltaTime, Space.Self);
        }
    }
}
