using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private int hp = 5;
    [SerializeField] private float force = 0.05f;
    [SerializeField] private float speedLimit = 0.05f;
    [SerializeField] private float rotateVelocity = 1f;
    [SerializeField] private float inertionMultiplier = 1f;
    [Space]
    [Header("Controller")]
    [SerializeField] GameController GameController;

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
            GameController.GameEnded();
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
}
