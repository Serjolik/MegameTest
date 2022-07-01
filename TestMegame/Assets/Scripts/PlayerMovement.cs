using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float force = 0.05f;
    [SerializeField] private float speedLimit = 0.05f;
    [SerializeField] private float rotateVelocity = 1f;
    [SerializeField] private float inertionMultiplier = 1f;
    private Vector3 _inertion;
    float speed;

    private void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            speed = force * Input.GetAxis("Vertical");
            _inertion += transform.up * Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;
            _inertion = Vector3.ClampMagnitude(_inertion, speedLimit);
            _inertion *= inertionMultiplier;
        }

        transform.Rotate(Vector3.forward, Input.GetAxis("Horizontal") * rotateVelocity * -1);
        transform.Translate(_inertion, Space.World);
    }
}
