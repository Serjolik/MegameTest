using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float startRange = 15f;
    private float maxRange;
    private Transform bulletTransform;

    private void Start()
    {
        maxRange = startRange;
        bulletTransform = gameObject.transform;
    }
    private void Update()
    {
        bulletTransform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
        maxRange -= speed * Time.deltaTime;
        if (maxRange < 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        maxRange = startRange;
    }
}
