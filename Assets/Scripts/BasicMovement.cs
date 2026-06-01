using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    void Update()
    {
        CheckMovement();
    }

    public void CheckMovement()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += Vector3.up;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += Vector3.down;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;
        }
    }
}