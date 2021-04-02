using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxDistancePickedUp;
    public float speedRotate;
    public void RotateObject()
    {
        transform.Rotate(0f, speedRotate, 0f);
    }
}
