using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private int damageDealt = 1;
    [SerializeField] private float speed = 1;
    [SerializeField] private int points = 100;

    private PlayerStats playerStats;
    private Transform asteroidTransform;
    private Vector3 vectorMovement;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        asteroidTransform = gameObject.transform;
        vectorMovement = new Vector3(1, 1, 0);
    }

    private void Update()
    {
        asteroidTransform.Translate(vectorMovement * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerStats.DamageGiven(damageDealt);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            playerStats.PointsEarn(points);
            Destroy(gameObject);
        }
    }
}
