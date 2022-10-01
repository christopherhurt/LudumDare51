using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public float initRatePerSecond;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + initRatePerSecond * Time.deltaTime, transform.position.z);
    }
}
