using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [Header("Количество выстрелов в секунду")]
    [SerializeField] private float bulletReload = 1f;
    [Space]
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform playerTransform;

    private bool canShot = true;
    private Vector3 direction;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canShot)
        {
            Instantiate(Bullet, playerTransform.position, playerTransform.rotation);
            StartCoroutine(shotReload());
        }
    }

    private IEnumerator shotReload()
    {
        canShot = false;
        yield return new WaitForSeconds(1 / bulletReload);
        canShot = true;
    }
}
