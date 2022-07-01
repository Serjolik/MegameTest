using UnityEngine;

public class WallsTeleport : MonoBehaviour
{
    [Header("Position can be up, down, left or right")]
    [Space]
    [SerializeField] private string position;
    private Vector3 newPos;
    private string playerResolution = "16:10";
    private float teleportDistance;

    private void Start()
    {
        if (playerResolution == "16:10")
            teleportDistance = 11.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        newPos = collision.gameObject.transform.position;
        switch (position)
        {
            case ("up"):
                newPos.y -= teleportDistance;
                break;
            case ("down"):
                newPos.y += teleportDistance;
                break;
            case ("left"):
                newPos.x += 1.9f * teleportDistance;
                break;
            case ("right"):
                newPos.x -= 1.9f * teleportDistance;
                break;
            default:
                Debug.Log("Unknown position");
                break;
            }
            collision.gameObject.transform.position = newPos;
    }
}
