using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public static Tile[,] board = new Tile[3,8];
    public GameObject[] tiles;

    public static int playerX = 0, playerY = 0, hp = 100;
    public int damage = 10;
    bool alreadyHit = false;
    // Start is called before the first frame update
    void Start()
    {
        int tileIndex = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                board[i,j] = new Tile();
            }
        }

        for (int i =0; i < 3; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                board[i,j].SetTileObj(tiles[tileIndex]);
                tileIndex++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnHandoff.movePhase)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (playerY != 2)
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
            //Tile formatting is board[y,x]
            this.gameObject.transform.position = board[playerY, playerX].GetTilePos().position;

            if (board[playerY, playerX].currentState == TileState.PlayerHazard && !alreadyHit)
            {
                hp -= board[playerY, playerX].damage;
                alreadyHit= true;          
            }
        }

        if (TurnHandoff.castPhase)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TurnHandoff.enemyMovePhase = true;
                alreadyHit = false;
            }
        }
    }


    public void decrementHP(int damage)
    {
        //update later to trigger cutscene/encore on death
        hp -= damage;
    }
}
