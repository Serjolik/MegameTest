using System.Collections;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [Header("Количество выстрелов в секунду")]
    [SerializeField] private float bulletReload = 1f;
    [Space]
    [Header("Пуля")]
    [SerializeField] private GameObject Bullet;

    [HideInInspector] public bool controlMode;
    private bool canShot = true;
    private Vector3 direction;

    private void Update()
    {
        if (controlMode)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && canShot)
            {
                Shotting();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && canShot)
            {
                Shotting();
            }
        }
    }

    private void Shotting()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject("Bullet");
        if (bullet != null)
        {
            bullet.transform.position = gameObject.transform.position;
            bullet.transform.rotation = gameObject.transform.rotation;
            bullet.SetActive(true);
        }
        StartCoroutine(shotReload());
    }
    private IEnumerator shotReload()
    {
        canShot = false;
        yield return new WaitForSeconds(1 / bulletReload);
        canShot = true;
    }
}
