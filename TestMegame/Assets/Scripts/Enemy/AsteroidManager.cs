using System.Collections;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [Header("Астероид")]
    [SerializeField] private GameObject BigAsteroid;
    [SerializeField] private GameObject MediumAsteroid;
    [SerializeField] private GameObject SmallAsteroid;

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
                    Instantiate(BigAsteroid, gameObject.transform.position, gameObject.transform.rotation);
                    AsteroidScript = BigAsteroid.GetComponent<Asteroid>();
                }
                break;
            case ("Medium"):
                for (int i = 0; i < numberDivision; i++)
                {
                    Instantiate(MediumAsteroid, gameObject.transform.position, gameObject.transform.rotation);
                    AsteroidScript = MediumAsteroid.GetComponent<Asteroid>();
                }
                break;
            case ("Small"):
                for (int i = 0; i < numberDivision; i++)
                {
                    Instantiate(SmallAsteroid, gameObject.transform.position, gameObject.transform.rotation);
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
