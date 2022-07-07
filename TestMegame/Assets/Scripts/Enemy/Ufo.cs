using UnityEngine;

public class Ufo : MonoBehaviour
{
    private GameController gameController;
    private PlayerStats playerStats;

    private Transform ufoTransform;
    private Vector3 vectorMovement;

    [SerializeField] private Transform playerTransform;
    private Vector3 playerPosition;

    private float speed;
    private float attackSpeed;
    private int damage;

    private float spawnDistanceToPlayer;

    private void Start()
    {
        gameObject.SetActive(false);
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        ufoTransform = gameObject.transform;

        var direction = Random.Range(0, 1 + 1);
        if (direction == 0)
        {
            direction = -1;
        }
        vectorMovement = new Vector3(direction, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            gameController.UfoDemolish("Demolish");
        }
        else if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            playerStats.DamageGiven(damage);
            gameController.UfoDemolish("Bump");
        }
    }

    private void Update()
    {
        ufoTransform.Translate(vectorMovement * speed * Time.deltaTime);
    }

    public void Activation()
    {
        ufoTransform.position = SetPosition();
        gameObject.SetActive(true);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetAttackSpeed(float attackSpeed)
    {
        this.attackSpeed = attackSpeed;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetPlayerPosition()
    {
        playerPosition = playerTransform.position;
    }

    public void SetDistanceToPlayer(float spawnDistanceToPlayer)
    {
        if (spawnDistanceToPlayer >= 9f)
        {
            Debug.Log("Distance is too huge (>= 9). Param set to 5");
            spawnDistanceToPlayer = 5;
        }
        this.spawnDistanceToPlayer = spawnDistanceToPlayer;
    }

    private Vector3 SetPosition()
    {
        SetPlayerPosition();
        var newPosition = new Vector3(playerPosition.x, playerPosition.y, 0);
        while (Vector3.Distance(newPosition, playerPosition) <= spawnDistanceToPlayer)
        {
            var x_position = Random.Range(-9f * 0.75f, (9f + 1) * 0.75f);
            var y_position = Random.Range(-4f * 0.75f, (4f + 1) * 0.75f);
            newPosition = new Vector3(x_position, y_position, 0);
        }
        return newPosition;
    }

}
