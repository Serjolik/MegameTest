using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float force = 0.05f;
    [SerializeField] private float speedLimit = 0.05f;
    [SerializeField] private float rotateVelocity = 1f;
    [SerializeField] private float inertionMultiplier = 1f;
    private AudioSource ForceSound;
    private Vector3 _inertion;
    private float speed;

    [HideInInspector] public bool Invulnerability;

    [HideInInspector] public bool ControlMode;

    private void Awake()
    {
        ForceSound = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {

        if (ControlMode)
        {

            if (Input.GetAxis("Vertical") > 0 || Input.GetMouseButton(1))
            {
                PlaySound();
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
                PlaySound();
                speed = force * 1;
                _inertion += transform.up * speed * Time.fixedDeltaTime;
                _inertion = Vector3.ClampMagnitude(_inertion, speedLimit);
                _inertion *= inertionMultiplier;
            }

            transform.Rotate(Vector3.forward, Input.GetAxis("Horizontal") * rotateVelocity * -1);
        }

        transform.Translate(_inertion, Space.World);
    }

    private void PlaySound()
    {
        if (!ForceSound.isPlaying)
            ForceSound.Play();
    }

    public void ToStartPosition()
    {
        transform.position = new Vector3(0, 0, 0);
    }

}
