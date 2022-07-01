using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    private float force;
    private float speedLimit;
    private float rotateVelocity;
    private float inertionMultiplier;
    private Vector3 _inertion;
    float speed;

    private void Start()
    {
        force = playerStats.GiveForce();
        speedLimit = playerStats.GiveSpeedLimit();
        rotateVelocity = playerStats.GiveRotateVelocity();
        inertionMultiplier = playerStats.GiveInertionMultiplier();
    }

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
