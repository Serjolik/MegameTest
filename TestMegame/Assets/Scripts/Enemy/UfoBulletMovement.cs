using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoBulletMovement : MonoBehaviour
{
    [HideInInspector] public Transform playerTransform;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float startRange = 15f;
    [SerializeField] private int damage = 1;

    private PlayerStats playerStats;

    private float maxRange;
    private Transform bulletTransform;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        bulletTransform = gameObject.transform;
        maxRange = startRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            Destroy(gameObject);
            playerStats.DamageGiven(damage);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        bulletTransform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
        maxRange -= speed * Time.deltaTime;
        if (maxRange < 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetRotation()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(playerTransform.position.y - transform.position.y, playerTransform.position.x - transform.position.x) * Mathf.Rad2Deg - 90);
    }
}
