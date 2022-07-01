using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsTeleport : MonoBehaviour
{
    [Header("Position can be up, down, left or right")]
    [Space]
    [SerializeField] private string position;
    private Vector3 newPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        newPos = collision.gameObject.transform.position;
        switch (position)
        {
            case ("up"):
                newPos.y -= 11.5f;
                break;
            case ("down"):
                newPos.y += 11.5f;
                break;
            case ("left"):
                newPos.x += 22;
                break;
            case ("right"):
                newPos.x -= 22;
                break;
            default:
                Debug.Log("Unknown position");
                break;
            }
            collision.gameObject.transform.position = newPos;
    }
}
