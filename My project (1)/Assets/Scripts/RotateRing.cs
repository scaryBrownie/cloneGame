using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRing : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public RotateDirection rotateDirection = RotateDirection.Right;
    public GameObject otherCircle;

    public enum RotateDirection
    {
        Right,
        Left
    }

    private RotateRing otherRotateRing;
    private Vector3 initialScale;

    private float direction;

    void Start()
    {
        if (otherCircle != null)
        {
            otherRotateRing = otherCircle.GetComponent<RotateRing>();
            otherRotateRing.rotationSpeed = rotationSpeed;
            otherRotateRing.rotateDirection = (rotateDirection == RotateDirection.Right) ? RotateDirection.Left : RotateDirection.Right;
        }

        initialScale = transform.localScale;
        direction = (rotateDirection == RotateDirection.Right) ? 1f : -1f;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * direction * rotationSpeed * Time.deltaTime);

        if (otherRotateRing != null)
        {
            otherRotateRing.rotationSpeed = rotationSpeed;
            otherRotateRing.rotateDirection = (rotateDirection == RotateDirection.Right) ? RotateDirection.Left : RotateDirection.Right;

            Vector3 scale = otherCircle.transform.localScale;
            scale.x = (rotateDirection == RotateDirection.Right) ? -Mathf.Abs(initialScale.x) : Mathf.Abs(initialScale.x);
            otherCircle.transform.localScale = scale;
        }
    }
}
