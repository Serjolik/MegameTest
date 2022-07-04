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

    public void StageSpawn()
    {
        StartCoroutine(TimerToSpawn());
    }

    private IEnumerator TimerToSpawn()
    {
        yield return new WaitForSeconds(spawnRate);
        AsteroidsSpawn("Big");
    }

    public void AsteroidsSpawn(string asteroidType)
    {
        switch (asteroidType) {
            case ("Big"):
                for (int i = 0; i < asteroidsAmount; i++)
                {
                    GameObject BigAsteroid = ObjectPool.SharedInstance.GetPooledObject("BigAsteroid");
                    if (BigAsteroid != null)
                    {
                        BigAsteroid.transform.position = gameObject.transform.position;
                        BigAsteroid.transform.rotation = gameObject.transform.rotation;
                        BigAsteroid.SetActive(true);
                    }
                    AsteroidScript = BigAsteroid.GetComponent<Asteroid>();
                }
                break;
            case ("Medium"):
                for (int i = 0; i < numberDivision; i++)
                {
                    GameObject MediumAsteroid = ObjectPool.SharedInstance.GetPooledObject("MediumAsteroid");
                    if (MediumAsteroid != null)
                    {
                        MediumAsteroid.transform.position = gameObject.transform.position;
                        MediumAsteroid.transform.rotation = gameObject.transform.rotation;
                        MediumAsteroid.SetActive(true);
                    }
                    AsteroidScript = MediumAsteroid.GetComponent<Asteroid>();
                }
                break;
            case ("Small"):
                for (int i = 0; i < numberDivision; i++)
                {
                    GameObject SmallAsteroid = ObjectPool.SharedInstance.GetPooledObject("SmallAsteroid");
                    if (SmallAsteroid != null)
                    {
                        SmallAsteroid.transform.position = gameObject.transform.position;
                        SmallAsteroid.transform.rotation = gameObject.transform.rotation;
                        SmallAsteroid.SetActive(true);
                    }
                    AsteroidScript = SmallAsteroid.GetComponent<Asteroid>();
                }
                break;
            default:
                Debug.Log("Asteroid type incorrect");
                break;
        }
        AsteroidScript.damageDealt = damageDealt;
        AsteroidScript.speed = speed;
        AsteroidScript.asteroidType = asteroidType;
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
}
