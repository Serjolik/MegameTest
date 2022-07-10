using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [HideInInspector] public int damageDealt = 1;
    [HideInInspector] public float speed = 1;
    [HideInInspector] public string asteroidType = "BigAsteroid";
    [SerializeField] AudioClip ExplousionSound;

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

        if (asteroidType == "BigAsteroid")
        {
            multiplier = PowInTen(decimal_point_precision);
            x_direction = (float)Random.Range(-multiplier, multiplier) / multiplier;
            y_direction = (float)Random.Range(-multiplier, multiplier) / multiplier;
            vectorMovement = new Vector3(x_direction, y_direction, 0);
        }
    }

    private void Update()
    {
        asteroidTransform.Translate(vectorMovement * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            AudioSource.PlayClipAtPoint(ExplousionSound, transform.position);
            PlayerBump();
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            AudioSource.PlayClipAtPoint(ExplousionSound, transform.position);
            gameObject.SetActive(false);
            Demolishing();
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Ufo")
        {
            AudioSource.PlayClipAtPoint(ExplousionSound, transform.position);
            collision.gameObject.SetActive(false);
            UfoBump();
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

    private void Demolishing()
    {
        if(asteroidType != "SmallAsteroid")
        {
            gameController.AsteroidDemolish(asteroidType, asteroidTransform.position, vectorMovement);
        }
        else
        {
            gameController.AsteroidDemolish();
        }
    }

    private void PlayerBump()
    {
        gameObject.SetActive(false);
        playerStats.DamageGiven(damageDealt);
        gameController.AsteroidDeleted(asteroidType);
    }

    private void UfoBump()
    {
        gameObject.SetActive(false);
        gameController.AsteroidDeleted(asteroidType);
        gameController.UfoDemolish("Bump");
    }

    private int SetRotation(bool angleSwither)
    {
        var rotation = 0;
        if (angleSwither)
        {
            rotation = 45;
        }
        else
        {
            rotation = -45;
        }
        return rotation;
    }

    public void SetVectorMovement(Vector3 vectorMovement, bool angleSwither, float speedModify)
    {
        var angle = SetRotation(angleSwither);
        vectorMovement = Quaternion.AngleAxis(angle, Vector3.forward) * vectorMovement;
        this.vectorMovement = vectorMovement * speedModify;
    }
}
