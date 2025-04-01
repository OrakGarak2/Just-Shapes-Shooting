using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotation : MonoBehaviour
{
    [SerializeField] Vector3 rotationOffset;

    void Update()
    {
        transform.Rotate(rotationOffset * Time.deltaTime);
    }
}
