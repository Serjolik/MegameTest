using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [HideInInspector] public int damageDealt = 1;
    [HideInInspector] public float speed = 1;
    [HideInInspector] public string asteroidType = "Big";

    private PlayerStats playerStats;
    private GameController gameController;
    private Transform asteroidTransform;
    private Vector3 vectorMovement;

    private int decimal_point_precision = 3;
    private int multiplier;

    private float x_direction;
    private float y_direction;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        asteroidTransform = gameObject.transform;

        multiplier = PowInTen(decimal_point_precision);
        x_direction = (float)Random.Range(-multiplier, multiplier) / multiplier;
        y_direction = (float)Random.Range(-multiplier, multiplier) / multiplier;
        vectorMovement = new Vector3(x_direction, y_direction, 0);
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
            gameController.AsteroidDemolish(asteroidType);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private int PowInTen(int power)
    {
        int result = 1;
        int multiplier = 10;
        if (power < 1)
        {
            Debug.Log("Incorrected power. Multiplier set 10");
            return multiplier;
        }
        while (power != 0)
        {
            result *= multiplier;
            power--;
        }
        return result;
    }
}
