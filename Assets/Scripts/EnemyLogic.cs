using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyLogic : MonoBehaviour
{
    public GameObject hpBar;
    public static int enemyX = 5, enemyY = 1;
    bool alreadyAttacked = false;
    float maxBarLength;
    public int maxHP;
    public static int currHP;

    // Start is called before the first frame update
    void Start()
    {
        maxBarLength = hpBar.transform.localScale.x;
        EnemyLogic.currHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnHandoff.enemyMovePhase)
        {
            alreadyAttacked = false;

            int xMovement = Random.Range(-1, 2);
            
            if(enemyX + xMovement > 3 && enemyX + xMovement < 5)
            {
                enemyX += xMovement;
            }

            if(PlayerMovement.playerY > enemyY && enemyY + 1 < 3)
            {
                enemyY += 1;
            }

            if (PlayerMovement.playerY < enemyY && enemyY - 1 > -1)
            {
                enemyY -= 1;
            }

            this.gameObject.transform.position = PlayerMovement.board[enemyY, enemyX].tileGameObject.transform.position;

            
            TurnHandoff.movePhase= true;
        }

        if(TurnHandoff.movePhase)
        {
            if(!alreadyAttacked)
            {
                beamAttack();
            }
        }

        if (PlayerMovement.board[enemyY, enemyX].currentState == TileState.EnemyHazard)
        {
            currHP -= PlayerMovement.board[enemyY, enemyX].damage;
            recalculateHPBar();
        }

    }

    public void beamAttack()
    {
        Card laser = new Card();
        laser = CardDatabase.jokeDictionary[100];

        for (int j = 0; j < enemyX; j++)
        {
            PlayerMovement.board[enemyY, j].currentState = TileState.Warning;
            PlayerMovement.board[enemyY, j].upcomingJoke = laser;
        }

        alreadyAttacked = true;
    }

    public void recalculateHPBar()
    {
        float hpRatio = (float)currHP / (float)maxHP;
        if (hpRatio < 0) { 
            hpRatio = 0;
        }
        float oldBarLength = hpBar.transform.localScale.x;
        float newBarLength = hpRatio * maxBarLength;
        float difference = oldBarLength - newBarLength;
        hpBar.transform.localScale = new Vector2(newBarLength, hpBar.transform.localScale.y);
        hpBar.transform.position = new Vector2(hpBar.transform.position.x + (difference / 2), hpBar.transform.position.y);
    }

}
