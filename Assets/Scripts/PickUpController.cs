using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(-80, transform.rotation.eulerAngles.y + 90 * Time.deltaTime, 0);
    }
}
