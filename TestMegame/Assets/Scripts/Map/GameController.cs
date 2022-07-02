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
    [SerializeField] private int startAsteroidAmount = 2;
    [SerializeField] private int pointsForAsteroid = 100;

    [SerializeField] private int AsteroidDamage;
    [SerializeField] private float AsteroidSpeed;

    private GameObject UICanvas;
    private TextMeshProUGUI PointsPanelText;

    private float screenSizeHeight;
    private float screenSizeWidth;

    private int currentAmountOfAsteroids;

    private string PointsResult;
    private int points;

    private bool isAlive;

    private void Start()
    {
        UICanvas = GameObject.FindGameObjectWithTag("UI");
        PointsPanelText = UICanvas.GetComponentInChildren<TextMeshProUGUI>();

        SetAsteroidParam();

        currentAmountOfAsteroids = startAsteroidAmount;

        isAlive = true;
        points = 0;
        AsteroidManager.Spawn();
    }

    private void Update()
    {
        if (!isAlive)
        {
            Debug.Log("GameIsEnd");
            return;
        }
        
        if (currentAmountOfAsteroids <= 0)
        {
            startAsteroidAmount++;
            AsteroidManager.SetAsteroidsAmount(startAsteroidAmount);
            AsteroidManager.Spawn();
            currentAmountOfAsteroids = AsteroidManager.GiveAsteroidsAmount();
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

    public void AsteroidDemolish()
    {
        Debug.Log("Asteroid is demolish");
        PointsChange(pointsForAsteroid);
        currentAmountOfAsteroids -= 1;
    }

    private void SetAsteroidParam()
    {
        AsteroidManager.SetSpawnRate(asteroidSpawnReloading);
        AsteroidManager.SetAsteroidsAmount(startAsteroidAmount);
        AsteroidManager.SetDamage(AsteroidDamage);
        AsteroidManager.setSpeed(AsteroidSpeed);
    }
}
