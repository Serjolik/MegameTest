using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoShot : MonoBehaviour
{
    [Header("Пуля")]
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform ufoTransform;

    private UfoBulletMovement bulletMovement;
    private Vector3 playerPosition;

    private bool canShot = true;
    private Vector3 direction;

    private float bulletReload = 1f;

    private void Update()
    {
        if (canShot)
        {
            Shot();
            StartCoroutine(shotReload());
        }
    }

    private void OnEnable()
    {
        StartCoroutine(shotReload());
    }

    private IEnumerator shotReload()
    {
        canShot = false;
        bulletReload = Random.Range(2, 5);
        yield return new WaitForSeconds(bulletReload);
        canShot = true;
    }

    private void Shot()
    {
        playerPosition = playerTransform.position;
        Bullet.transform.position = ufoTransform.transform.position;
        bulletMovement = Bullet.GetComponent<UfoBulletMovement>();
        bulletMovement.playerTransform = playerTransform;
        bulletMovement.SetRotation();
        Instantiate(Bullet);
    }
}