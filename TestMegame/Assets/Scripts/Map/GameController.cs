using UnityEngine;
using System.Collections;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private AsteroidManager AsteroidManager;
    [SerializeField] private Ufo Ufo;
    [SerializeField] private GameObject Menu;
    [Space]
    [Header("Variables")]
    [SerializeField] private int asteroidSpawnReloading = 2;
    [SerializeField] private int startAsteroidAmount = 1;

    [SerializeField] private int pointsForBigAsteroid = 20;
    [SerializeField] private int pointsForMediumAsteroid = 50;
    [SerializeField] private int pointsForSmallAsteroid = 100;
    [SerializeField] private int pointsForUFO = 200;

    [SerializeField] private int ObjectsBumpDamage = 1;
    [SerializeField] private float asteroidSpeed = 1f;
    [SerializeField] private float ufoSpeed = 1f;
    [SerializeField] private float ufoAttackSpeed = 1f;

    [Header("На какой дистанции от игрока могут появлятся астероиды")]
    [SerializeField] private float distanceToPlayer = 5f;

    private Transform playerTransform;
    private Vector3 playerPosition;

    private GameObject UICanvas;
    private TextMeshProUGUI[] TextPanels;
    private TextMeshProUGUI PointsPanelText;
    private TextMeshProUGUI HealthPanelText;

    private float screenSizeHeight;
    private float screenSizeWidth;

    private int currentAmountOfSmallAsteroids;

    private int points;

    private MenuController menuController;

    [HideInInspector] public bool isAlive;
    private bool isUfoAlive;
    private bool isPositiveSpawnAngle;
    [HideInInspector] public bool inMenu;

    private void Awake()
    {
        UICanvas = GameObject.FindGameObjectWithTag("UI");

        TextPanels = UICanvas.GetComponentsInChildren<TextMeshProUGUI>();
        PointsPanelText = TextPanels[0];
        HealthPanelText = TextPanels[1];

        playerTransform = gameObject.GetComponentInChildren<Transform>();
        playerPosition = playerTransform.position;

        currentAmountOfSmallAsteroids = startAsteroidAmount * 2 * 2;

        isAlive = false;
        points = 0;
    }

    private void Start()
    {
        SetAsteroidParam();
        SetUfoParam();
        AsteroidManager.StageSpawn();
        UfoAddedToScene();

        Menu = GameObject.FindGameObjectWithTag("Menu");
        menuController = Menu.GetComponent<MenuController>();
        menuController.GameStarting();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isAlive)
        {
            if (!inMenu)
            {
                inMenu = true;
                Time.timeScale = 0;
                Menu.SetActive(true);
            }
            else
            {
                inMenu = false;
                Time.timeScale = 1;
                Menu.SetActive(false);
            }
        }
    }
    private void PointsChange(int points)
    {
        this.points += points;
        PointsPanelText.text = "Points: " + this.points;
    }

    private void NewStage()
    {
        startAsteroidAmount++;
        AsteroidManager.SetAsteroidsAmount(startAsteroidAmount);
        AsteroidManager.StageSpawn();
        currentAmountOfSmallAsteroids = startAsteroidAmount * 2 * 2;
    }

    public void GameEnded()
    {
        isAlive = false;
        Debug.Log("Game is end with " + points + " points");
    }

    public void AsteroidDemolish()
    {
        Debug.Log("Small asteroid is demolish");
        PointsChange(pointsForSmallAsteroid);
        currentAmountOfSmallAsteroids--;
        if (currentAmountOfSmallAsteroids <= 0)
        {
            NewStage();
        }
    }

    public void AsteroidDemolish(string asteroidType, Vector3 asteroidPosition, Vector3 vectorMovement)
    {
        switch (asteroidType)
        {
            case ("BigAsteroid"):
                PointsChange(pointsForBigAsteroid);
                AsteroidManager.AsteroidsSpawn("MediumAsteroid", asteroidPosition, vectorMovement);
                break;
            case ("MediumAsteroid"):
                PointsChange(pointsForMediumAsteroid);
                AsteroidManager.AsteroidsSpawn("SmallAsteroid", asteroidPosition, vectorMovement);
                break;
            default:
                Debug.Log("Incorrect asteroid type");
                break;
        }
    }

    public void AsteroidDeleted(string asteroidType)
    {
        switch (asteroidType)
        {
            case ("BigAsteroid"):
                currentAmountOfSmallAsteroids -= 4;
                break;
            case ("MediumAsteroid"):
                currentAmountOfSmallAsteroids -= 2;
                break;
            case ("SmallAsteroid"):
                currentAmountOfSmallAsteroids--;
                break;
            default:
                Debug.Log("Incorrect asteroid type");
                break;
        }
        if (currentAmountOfSmallAsteroids <= 0)
        {
            NewStage();
        }
    }

    private void UfoAddedToScene()
    {
        if (isUfoAlive)
        {
            Debug.Log("Ufo spawning");
            Ufo.Activation();
        }
        else
        {
            StartCoroutine(WaitForNextUfo());
        }
    }

    private IEnumerator WaitForNextUfo()
    {
        yield return new WaitForSeconds(Random.Range(20, 40 + 1));
        isUfoAlive = true;
        UfoAddedToScene();
    }

    public void UfoDemolish(string type)
    {
        isUfoAlive = false;
        UfoAddedToScene();
        if (type == "Demolish")
        PointsChange(pointsForUFO);
        else if (type == "Bump")
        {
            Debug.Log("No points for bump)");
        }
        else
        {
            Debug.Log("Unknown demolishing type");
        }
    }

    private void SetAsteroidParam()
    {
        AsteroidManager.SetSpawnRate(asteroidSpawnReloading);
        AsteroidManager.SetAsteroidsAmount(startAsteroidAmount);
        AsteroidManager.SetDamage(ObjectsBumpDamage);
        AsteroidManager.setSpeed(asteroidSpeed);
        AsteroidManager.SetDistanceToPlayer(distanceToPlayer);
    }

    private void SetUfoParam()
    {
        Ufo.SetSpeed(ufoSpeed);
        Ufo.SetDamage(ObjectsBumpDamage);
        Ufo.SetDistanceToPlayer(distanceToPlayer);
    }

    public void HealthChange(int hp)
    {
        HealthPanelText.text  = "Health: " + hp;
    }
}
