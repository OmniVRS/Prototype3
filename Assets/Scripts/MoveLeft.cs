using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    private PlayerController playerController;

    private float moveSpeed = 30f;
    private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.gameOver == false)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
