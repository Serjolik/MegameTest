using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxRange = 100f;
    private Transform bulletTransform;

    private void Start()
    {
        bulletTransform = gameObject.transform;
    }
    private void Update()
    {
        bulletTransform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
        maxRange -= speed * Time.deltaTime;
        if (maxRange < 0)
        {
            Destroy(gameObject);
        }
    }
}
