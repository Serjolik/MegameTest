using System.Collections;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    private float spawnRate;
    private int asteroidsAmount;
    private int damageDealt;
    private float speed;
    [Header("Астероид")]
    [SerializeField] private GameObject BigAsteroid;
    [SerializeField] private GameObject MediumAsteroid;
    [SerializeField] private GameObject SmallAsteroid;

    public void Spawn()
    {
        StartCoroutine(TimerToSpawn());
    }

    private void AsteroidsSpawn()
    {
        for (int i = 0; i < asteroidsAmount; i++)
        {
            Instantiate(BigAsteroid, gameObject.transform.position, gameObject.transform.rotation);
            var AsteroidScript = BigAsteroid.GetComponent<Asteroid>();
            AsteroidScript.damageDealt = damageDealt;
            AsteroidScript.speed = speed;
        }
        asteroidsAmount++;
    }

    private IEnumerator TimerToSpawn()
    {
        yield return new WaitForSeconds(spawnRate);
        AsteroidsSpawn();
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
