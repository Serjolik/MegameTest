using System.Collections;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [Header("���������� ��������� � �������")]
    [SerializeField] private float bulletReload = 1f;
    [Space]
    [Header("����")]
    [SerializeField] private GameObject Bullet;

    private bool canShot = true;
    private Vector3 direction;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canShot)
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
    }

    private IEnumerator shotReload()
    {
        canShot = false;
        yield return new WaitForSeconds(1 / bulletReload);
        canShot = true;
    }
}
