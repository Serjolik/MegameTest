using UnityEngine;

public class WallsTeleport : MonoBehaviour
{
    private Vector3 newPos;
    private float teleportDistance_x;
    private float teleportDistance_y;
    private float position_x;
    private float position_y;

    enum Position
    {
        up ,
        down,
        left,
        right
    }
    Position pos;

    private void Awake()
    {
        float x_size = Screen.width / 100;
        float y_size = Screen.height / 100;

        teleportDistance_x = x_size * 2;
        teleportDistance_y = y_size * 2;
        position_x = gameObject.transform.position.x;
        position_y = gameObject.transform.position.y;

        if (position_x > x_size)
        {
            pos = Position.right;
        }
        else if (position_x < -x_size)
        {
            pos = Position.left;
        }
        else if (position_y > y_size)
        {
            pos = Position.up;
        }
        else if (position_y < -y_size)
        {
            pos = Position.down;
        }
        else
        {
            Debug.Log("Wall unknown position");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        newPos = collision.gameObject.transform.position;
        switch (pos)
        {
            case (Position.up):
                newPos.y -= teleportDistance_y;
                break;
            case (Position.down):
                newPos.y += teleportDistance_y;
                break;
            case (Position.left):
                newPos.x += teleportDistance_x;
                break;
            case (Position.right):
                newPos.x -= teleportDistance_x;
                break;
            default:
                Debug.Log("Wall possition not set");
                break;
        }
            collision.gameObject.transform.position = newPos;
    }
}
