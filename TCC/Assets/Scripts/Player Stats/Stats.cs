using UnityEngine;
using FMODUnity;

public class Stats : MonoBehaviour
{
    public float maxDistancePickedUp;
    public float speedRotate;

    [EventRef] public string collectSound;
    public void RotateObject()
    {
        transform.Rotate(0f, speedRotate, 0f);
    }
}
