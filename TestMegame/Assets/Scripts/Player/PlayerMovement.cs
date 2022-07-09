using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats playerStats;
    private float force;
    private float speedLimit;
    private float rotateVelocity;
    private float inertionMultiplier;
    private Vector3 _inertion;
    [HideInInspector] public bool controlMode;
    float speed;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        force = playerStats.GiveForce();
        speedLimit = playerStats.GiveSpeedLimit();
        rotateVelocity = playerStats.GiveRotateVelocity();
        inertionMultiplier = playerStats.GiveInertionMultiplier();
    }

    private void FixedUpdate()
    {
        if (controlMode)
        {

            if (Input.GetAxis("Vertical") > 0 || Input.GetMouseButton(1))
            {
                speed = force * 1;
                _inertion += transform.up * speed * Time.fixedDeltaTime;
                _inertion = Vector3.ClampMagnitude(_inertion, speedLimit);
                _inertion *= inertionMultiplier;
            }


            double dx = Input.mousePosition.x - Screen.width / 2.0 - transform.position.x * 50;
            double dy = Input.mousePosition.y - Screen.height / 2.0 - transform.position.y * 50;
            float sdx = (float)dx;
            float sdy = (float)dy;

            float sR = Mathf.Atan2(sdx, sdy);
            float sD = 360 * sR / (2 * Mathf.PI);

            float startAngle_y = transform.rotation.eulerAngles.y;
            float startAngle_x = transform.rotation.eulerAngles.x;

            Quaternion target = Quaternion.Euler(startAngle_x, startAngle_y, -sD);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotateVelocity);
        }
        else
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                speed = force * 1;
                _inertion += transform.up * speed * Time.fixedDeltaTime;
                _inertion = Vector3.ClampMagnitude(_inertion, speedLimit);
                _inertion *= inertionMultiplier;
            }

            transform.Rotate(Vector3.forward, Input.GetAxis("Horizontal") * rotateVelocity * -1);
        }

        transform.Translate(_inertion, Space.World);
    }
}
