using UnityEngine;

public class Ufo : MonoBehaviour
{
    [SerializeField] private GameObject PlayerShip;
    [SerializeField] private GameController gameController;

    [SerializeField] private float speed = 1;
    [SerializeField] private int damage = 1;
    [SerializeField] private float spawnDistanceToPlayer = 5;

    private PlayerStats playerStats;
    private Transform ufoTransform;
    private Vector3 vectorMovement;
    private Transform playerTransform;
    private Vector3 playerPosition;

    private int max_x;
    private int max_y;

    private void Awake()
    {
        playerStats = PlayerShip.GetComponentInParent<PlayerStats>();
        playerTransform = PlayerShip.GetComponent<Transform>();

        gameObject.SetActive(false);
        ufoTransform = gameObject.transform;
        var direction = Random.Range(0, 1 + 1);
        if (direction == 0)
        {
            direction = -1;
        }
        vectorMovement = new Vector3(direction, 0, 0);
        SetDistanceToPlayer(spawnDistanceToPlayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            gameController.UfoDemolish("Demolish");
        }
        else if (collision.gameObject.tag == "Ship")
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

    public void SetPlayerPosition()
    {
        playerPosition = playerTransform.position;
    }

    public void SetDistanceToPlayer(float spawnDistanceToPlayer)
    {
        if (spawnDistanceToPlayer >= Screen.width)
        {
            Debug.Log("Distance is too huge (>= Screen width). Param set to 5");
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
            max_x = Screen.width / 100;
            max_y = Screen.height / 100;
            var x = (float)max_x * 0.75f;
            var y = (float)max_y * 0.75f;
            var x_position = Random.Range(-x, x + 1f);
            var y_position = Random.Range(-y, y + 1f);
            newPosition = new Vector3(x_position, y_position, 0);
        }
        return newPosition;
    }

}
