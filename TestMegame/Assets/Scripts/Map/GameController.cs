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

    [SerializeField] private int AsteroidDamage;
    [SerializeField] private float AsteroidSpeed;

    private GameObject UICanvas;
    private TextMeshProUGUI PointsPanelText;

    private float screenSizeHeight;
    private float screenSizeWidth;

    private int currentAmountOfSmallAsteroids;

    private string PointsResult;
    private int points;

    private bool isAlive;

    private void Start()
    {
        UICanvas = GameObject.FindGameObjectWithTag("UI");
        PointsPanelText = UICanvas.GetComponentInChildren<TextMeshProUGUI>();

        SetAsteroidParam();

        currentAmountOfSmallAsteroids = startAsteroidAmount * 2 * 2;

        isAlive = true;
        points = 0;
        AsteroidManager.StageSpawn();
    }

    private void Update()
    {
        if (!isAlive)
        {
            Debug.Log("GameIsEnd");
            return;
        }
        
        if (currentAmountOfSmallAsteroids <= 0)
        {
            startAsteroidAmount++;
            AsteroidManager.SetAsteroidsAmount(startAsteroidAmount);
            AsteroidManager.StageSpawn();
            currentAmountOfSmallAsteroids = startAsteroidAmount * 2 * 2;
        }
    }

    private void PointsChange(int points)
    {
        this.points += points;
        PointsPanelText.text = "Points: " + this.points;
    }

    public void GameEnded()
    {
        isAlive = false;
        Debug.Log("Game is end with " + points + " points");
    }

    public void AsteroidDemolish(string asteroidType)
    {
        Debug.Log("Asteroid is demolish");
        Debug.Log(asteroidType + " asteroid");
        switch (asteroidType)
        {
            case ("Big"):
                PointsChange(pointsForBigAsteroid);
                AsteroidManager.AsteroidsSpawn("Medium");
                break;
            case ("Medium"):
                PointsChange(pointsForMediumAsteroid);
                AsteroidManager.AsteroidsSpawn("Small");
                break;
            case ("Small"):
                PointsChange(pointsForSmallAsteroid);
                currentAmountOfSmallAsteroids--;
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
    }
}
