using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private float force  = 0.05f;
    [SerializeField] private float speedLimit = 0.05f;
    [SerializeField] private float rotateVelocity = 1f;
    [SerializeField] private float inertionMultiplier = 1f;

    private bool isAlive;

    private void Start()
    {
        isAlive = true;
    }

    public void DamageGiven(int damage)
    {
        if (damage <= 0)
        {
            Debug.Log("damage <= 0");
            return;
        }
        hp -= damage;
        if (hp <= 0)
        {
            EndGame();
        }
    }

    public float GiveForce()
    {
        return force;
    }

    public float GiveSpeedLimit()
    {
        return speedLimit;
    }

    public float GiveRotateVelocity()
    {
        return rotateVelocity;
    }
    
    public float GiveInertionMultiplier()
    {
        return inertionMultiplier;
    }

    private void EndGame()
    {
        Debug.Log("GameIsOver");
        isAlive = false;
    }

}
