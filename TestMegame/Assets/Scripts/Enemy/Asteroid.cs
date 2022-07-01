using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private int damageDealt;
    [SerializeField] private float speed;

    private Transform asteroidTransform;
    private Vector3 vectorMovement;

    private void Start()
    {
        asteroidTransform = gameObject.transform;
        vectorMovement = new Vector3(1, 1, 0);
    }

    private void Update()
    {
        asteroidTransform.Translate(vectorMovement * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerStats.DamageGiven(damageDealt);
            Destroy(gameObject);
        }
    }
}
