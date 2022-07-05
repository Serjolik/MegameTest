using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private AsteroidManager AsteroidManager;
    [Space]
    [Header("Variables")]
    [SerializeField] private int asteroidSpawnReloading = 2;
    [SerializeField] private int startAsteroidAmount = 1;

    [SerializeField] private int pointsForBigAsteroid = 20;
    [SerializeField] private int pointsForMediumAsteroid = 50;
    [SerializeField] private int pointsForSmallAsteroid = 100;
    [SerializeField] private int pointsForNLO = 200;

    [SerializeField] private int AsteroidDamage = 1;
    [SerializeField] private float AsteroidSpeed = 1f;

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

    private string PointsResult;
    private int points;

    private bool isAlive;

    private void Awake()
    {
        UICanvas = GameObject.FindGameObjectWithTag("UI");
        TextPanels = UICanvas.GetComponentsInChildren<TextMeshProUGUI>();
        PointsPanelText = TextPanels[0];
        HealthPanelText = TextPanels[1];

        playerTransform = gameObject.GetComponentInChildren<Transform>();
        playerPosition = playerTransform.position;

        currentAmountOfSmallAsteroids = startAsteroidAmount * 2 * 2;

        isAlive = true;
        points = 0;
    }

    private void Start()
    {
        SetAsteroidParam();
        AsteroidManager.StageSpawn();
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

    public void AsteroidDemolish(string asteroidType, Vector3 asteroidPosition)
    {
        switch (asteroidType)
        {
            case ("BigAsteroid"):
                PointsChange(pointsForBigAsteroid);
                AsteroidManager.AsteroidsSpawn("MediumAsteroid", asteroidPosition);
                break;
            case ("MediumAsteroid"):
                PointsChange(pointsForMediumAsteroid);
                AsteroidManager.AsteroidsSpawn("SmallAsteroid", asteroidPosition);
                break;
            default:
                Debug.Log("Incorrect asteroid type");
                break;
        }
    }

    private void SetAsteroidParam()
    {
        AsteroidManager.SetSpawnRate(asteroidSpawnReloading);
        AsteroidManager.SetAsteroidsAmount(startAsteroidAmount);
        AsteroidManager.SetDamage(AsteroidDamage);
        AsteroidManager.setSpeed(AsteroidSpeed);
        AsteroidManager.SetDistanceToPlayer(distanceToPlayer);
        AsteroidManager.SetPlayerPosition(playerPosition);
    }

    public void HealthChange(int hp)
    {
        HealthPanelText.text  = "Health: " + hp;
    }
}
