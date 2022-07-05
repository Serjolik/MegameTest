using System.Collections;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    private Asteroid AsteroidScript;

    private int numberDivision = 2;
    private float spawnRate;
    private int asteroidsAmount;
    private int damageDealt;
    private float speed;
    private Vector3 playerPosition;
    private float spawnDistanceToPlayer;

    public void StageSpawn()
    {
        StartCoroutine(TimerToSpawn());
    }

    private IEnumerator TimerToSpawn()
    {
        yield return new WaitForSeconds(spawnRate);
        AsteroidsSpawn("BigAsteroid");
    }

    public void AsteroidsSpawn(string asteroidType)
    {
        for (int i = 0; i < asteroidsAmount; i++)
        {
            AsteroidInstantiate(asteroidType, new Vector3(0, 0, 0));
        }
    }

    public void AsteroidsSpawn(string asteroidType, Vector3 ParentPosition)
    {
        for (int i = 0; i < numberDivision; i++)
        {
            AsteroidInstantiate(asteroidType, ParentPosition);
        }
    }

    private void AsteroidInstantiate(string asteroidType, Vector3 ParentPosition)
    {
        GameObject Asteroid = ObjectPool.SharedInstance.GetPooledObject(asteroidType);
        Debug.Log(asteroidType);
        if (Asteroid != null)
        {
            Asteroid.SetActive(true);
            if (asteroidType == "BigAsteroid")
            {
                AsteroidSetParams(Asteroid);
            }
            else
            {
                AsteroidSetParams(Asteroid, asteroidType, ParentPosition);
            }
        }
        else
        {
            Debug.Log("Incorrect Type");
        }
    }

    private void AsteroidSetParams(GameObject Asteroid, string asteroidType, Vector3 ParentPosition)
    {
        Asteroid.transform.position = ParentPosition;
        AsteroidScript = Asteroid.GetComponent<Asteroid>();
        AsteroidScript.damageDealt = damageDealt;
        AsteroidScript.speed = speed;
        AsteroidScript.asteroidType = asteroidType;
    }

    private void AsteroidSetParams(GameObject Asteroid)
    {
        Asteroid.transform.position = SetPosition();
        AsteroidScript = Asteroid.GetComponent<Asteroid>();
        AsteroidScript.damageDealt = damageDealt;
        AsteroidScript.speed = speed;
        AsteroidScript.asteroidType = "BigAsteroid";
    }

    private Vector3 SetPosition()
    {
        var newPosition = new Vector3(playerPosition.x , playerPosition.y, 0);
        while (Vector3.Distance(newPosition, playerPosition) <= spawnDistanceToPlayer)
        {
            var x_position = Random.Range(-9f, 9f);
            var y_position = Random.Range(-4f, 4f);
            newPosition = new Vector3(x_position, y_position, 0);
        }
        return newPosition;
    }

    public int GiveAsteroidsAmount()
    {
        return asteroidsAmount;
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

    public void SetPlayerPosition(Vector3 position)
    {
        playerPosition = position;
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
}
