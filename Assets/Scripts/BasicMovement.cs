using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BasicMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
    }

    public void CheckMovement()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, gameObject.transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
        }

    }
}