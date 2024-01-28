using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public GameObject[] tiles;
    public static Tile[,] board = new Tile[3,8];
    public GameObject hpBar;
    

    public static int playerX = 0, playerY = 0, currHP = 100, maxHP = 100;
    public float maxBarLength;
    bool alreadyHit = false;
    // Start is called before the first frame update
    void Awake()
    {
        BuildBoard();
        maxBarLength = hpBar.transform.localScale.x;
    }

    private void BuildBoard()
    {
        int tileIndex = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                board[i, j] = new Tile();
                board[i, j].tileGameObject = tiles[tileIndex];
                
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
            this.gameObject.transform.position = board[playerY, playerX].tileGameObject.transform.position;

            //If the current tile is dangerous, take damage
            if (board[playerY, playerX].currentState == TileState.PlayerHazard && !alreadyHit)
            {
                currHP -= board[playerY, playerX].damage;
                
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

        if (board[playerY, playerX].currentState == TileState.PlayerHazard)
        {
            currHP -= board[playerY, playerX].damage;
            recalculateHPBar();
        }
    }

    public void recalculateHPBar()
    {
        float hpRatio = (float)currHP / (float)maxHP;
        float oldBarLength = hpBar.transform.localScale.x;
        float newBarLength = hpRatio * maxBarLength;
        float difference = oldBarLength - newBarLength;
        hpBar.transform.localScale = new Vector2(newBarLength, hpBar.transform.localScale.y);
        hpBar.transform.position = new Vector2(hpBar.transform.position.x - (difference/2), hpBar.transform.position.y);
    }
}
