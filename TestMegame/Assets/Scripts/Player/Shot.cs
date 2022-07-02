using System.Collections;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [Header("Количество выстрелов в секунду")]
    [SerializeField] private float bulletReload = 1f;
    [Space]
    [Header("Пуля")]
    [SerializeField] private GameObject Bullet;

    private bool canShot = true;
    private Vector3 direction;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canShot)
        {
            Instantiate(Bullet, gameObject.transform.position, gameObject.transform.rotation);
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
