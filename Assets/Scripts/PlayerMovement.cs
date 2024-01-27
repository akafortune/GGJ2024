using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    GameObject[,] board = new GameObject[3,4];
    public GameObject[] tiles = new GameObject[12];

    public int playerX = 0, playerY = 0;
    // Start is called before the first frame update
    void Start()
    {
        int tileIndex = 0;

        for(int i =0; i < 3; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                board[i,j] = tiles[tileIndex];
                Debug.Log("added " + tileIndex);
                tileIndex++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Tile formatting is board[y,x]
        this.gameObject.transform.position = board[playerY, playerX].transform.position;

        if (Input.GetKeyDown(KeyCode.W))
        {
            if(playerY != 2)
            {
                playerY += 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (playerY != 0)
            {
                playerY -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (playerX != 0)
            {
                playerX -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (playerX != 3)
            {
                playerX += 1;
            }
        }
    }
}
