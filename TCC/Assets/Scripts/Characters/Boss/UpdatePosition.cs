using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePosition : MonoBehaviour
{
    public enum Type{ COPY_POSITION, HORN_POSITION }
    public Type type;
    public Transform target;
    public Vector3 offset;
    public float speedRotation;

    void Update()
    {
        switch (type)
        {
            case Type.COPY_POSITION:
                CopyPosition();
            break;
            case Type.HORN_POSITION:
                UpdateHornPosition();
            break;
        }
    }

    public void CopyPosition()
    {
        transform.position = target.position + offset;
    }

    public void UpdateHornPosition()
    {
        transform.position = new Vector3(target.position.x + offset.x, transform.position.y, target.position.z + offset.z);
        transform.rotation = Quaternion.Euler(0f, target.rotation.y * speedRotation, 0f);
    }
}
