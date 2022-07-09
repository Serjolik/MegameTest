using System.Collections;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Asteroid AsteroidScript;

    private int numberDivision = 2;
    private float spawnRate;
    private int asteroidsAmount;
    private int damageDealt;
    private float speed;
    private Vector3 playerPosition;
    private float spawnDistanceToPlayer;
    private Vector3 parentPosition;
    private Vector3 parentMovement;

    private float max_x;
    private float max_y;

    private bool isPositiveSpawnAngle;

    public void StageSpawn()
    {
        StartCoroutine(TimerToSpawn());
    }

    private IEnumerator TimerToSpawn()
    {
        yield return new WaitForSeconds(spawnRate);
        SetPlayerPosition();
        AsteroidsSpawn("BigAsteroid");
    }

    public void AsteroidsSpawn(string asteroidType)
    {
        for (int i = 0; i < asteroidsAmount; i++)
        {
            AsteroidInstantiate(asteroidType);
        }
    }

    public void AsteroidsSpawn(string asteroidType, Vector3 ParentPosition, Vector3 parentMovement)
    {
        Debug.Log(parentMovement);
        this.parentMovement = parentMovement;

        SetParentPosition(ParentPosition);
        for (int i = 0; i < numberDivision; i++)
        {
            AsteroidInstantiate(asteroidType);
        }
    }

    private void AsteroidInstantiate(string asteroidType)
    {
        GameObject Asteroid = ObjectPool.SharedInstance.GetPooledObject(asteroidType);
        Debug.Log(asteroidType);
        if (Asteroid != null)
        {
            Asteroid.SetActive(true);
            AsteroidSetParams(Asteroid, asteroidType);
            ScriptSetting(Asteroid, asteroidType);
        }
        else
        {
            Debug.Log("Incorrect Type");
        }
    }

    private void AsteroidSetParams(GameObject Asteroid, string asteroidType)
    {
        if (asteroidType == "BigAsteroid")
        {
            Asteroid.transform.position = SetPosition();
        }
        else
        {
            Asteroid.transform.position = parentPosition;
        }
    }

    private Vector3 SetPosition()
    {
        var newPosition = new Vector3(playerPosition.x , playerPosition.y, 0);
        while (Vector3.Distance(newPosition, playerPosition) <= spawnDistanceToPlayer)
        {
            max_x = Screen.width / 100;
            max_y = Screen.height / 100;
            var x = (float)max_x;
            var y = (float)max_y;
            var x_position = Random.Range(-x, x + 1f);
            var y_position = Random.Range(-y, y + 1f);
            newPosition = new Vector3(x_position, y_position, 0);
        }
        return newPosition;
    }

    public void SetSpawnRate(int spawnRate)
    {
        this.spawnRate = spawnRate;
    }

    public void SetAsteroidsAmount(int asteroidsAmount)
    {
        this.asteroidsAmount = asteroidsAmount;
    }

    public void SetDamage(int damageDealt)
    {
        this.damageDealt = damageDealt;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetPlayerPosition()
    {
        playerPosition = playerTransform.position;
    }

    public void SetParentPosition(Vector3 position)
    {
        this.parentPosition = position;
    }

    public void SetDistanceToPlayer(float spawnDistanceToPlayer)
    {
        if (spawnDistanceToPlayer >= Screen.width)
        {
            Debug.Log("Distance is too huge (>= screen width). Param set to 5");
            spawnDistanceToPlayer = 5;
        }
        this.spawnDistanceToPlayer = spawnDistanceToPlayer;
    }

    private void ScriptSetting(GameObject Asteroid, string asteroidType)
    {
        AsteroidScript = Asteroid.GetComponent<Asteroid>();
        AsteroidScript.damageDealt = damageDealt;
        AsteroidScript.speed = speed;
        AsteroidScript.asteroidType = asteroidType;
        if (asteroidType != "BigAsteroid")
        {
            AngleSwither();
            AsteroidScript.SetVectorMovement(parentMovement, isPositiveSpawnAngle);
        }
    }

    private void AngleSwither()
    {
        isPositiveSpawnAngle = !isPositiveSpawnAngle;
    }
}
