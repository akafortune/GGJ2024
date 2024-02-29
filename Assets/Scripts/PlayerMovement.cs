using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public int maxHP;
    public float maxBarLength;

    public GameObject[] tiles;
    public static Tile[,] board = new Tile[3,6];
    public GameObject hpBar;

    public bool inEncore = false;

    public Animator earlAnim;

    public GameObject retryScreen;

    

    public static int playerX = 0, playerY = 0, currHP;
    bool alreadyHit = false;
    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1;
        BuildBoard();
        maxBarLength = hpBar.transform.localScale.x;
    }

    void Start()
    {
        currHP = maxHP;    
    }

    private void BuildBoard()
    {
        int tileIndex = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 6; j++)
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
        earlAnim.SetBool("Moving", false);
        earlAnim.SetBool("Hit", false);
        if (TurnHandoff.movePhase)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (playerY != 2)
                {
                    playerY += 1;
                    earlAnim.SetBool("Moving", true);
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (playerY != 0)
                {
                    playerY -= 1;
                    earlAnim.SetBool("Moving", true);
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (playerX != 0)
                {
                    playerX -= 1;
                    earlAnim.SetBool("Moving", true);
                }
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (playerX != 2)
                {
                    playerX += 1;
                    earlAnim.SetBool("Moving", true);
                }
            }


            //Tile formatting is board[y,x]
            this.gameObject.transform.position = board[playerY, playerX].tileGameObject.transform.position;

            //If the current tile is dangerous, take damage
            if (board[playerY, playerX].currentState == TileState.PlayerHazard && !alreadyHit)
            {
                earlAnim.SetBool("Hit", true);

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
            if (currHP - board[playerY, playerX].damage <= 0 && LevelOfLaughs.levelOfLaughs >= 100)
            {
                currHP = (int) maxHP / 2;
            } else if (currHP - board[playerY, playerX].damage <= 0 && LevelOfLaughs.levelOfLaughs < 100)
            {
                currHP = 0;
                Time.timeScale = 0;
                retryScreen.SetActive(true);

            } else if (currHP - board[playerY, playerX].damage > 0)
            {
                currHP -= board[playerY, playerX].damage;
            }
            recalculateHPBar();
        }
    }

    public void recalculateHPBar()
    {
        float hpRatio = (float) currHP / (float) maxHP;
        if (hpRatio <= 0)
        {
            hpRatio = 0;
        }
        float newBarLength = hpRatio * maxBarLength;
        hpBar.transform.localScale = new Vector2(newBarLength, hpBar.transform.localScale.y);
    }

   
}
    
