using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public Animator JSAnim;
    public GameObject hpBar;
    public GameObject retryScreen;
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
        JSAnim.SetBool("IsAttacking", false);
        JSAnim.SetBool("IsHit", false);
        if (TurnHandoff.enemyMovePhase)
        {
            alreadyAttacked = false;

            int xMovement = Random.Range(-1, 2);
            
            if(enemyX + xMovement >= 3 && enemyX + xMovement <= 5)
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
                JSAnim.SetBool("IsAttacking", true);
                int randInt =(int) Random.Range(0,2);

                colAttack();
            }
        }

        if (PlayerMovement.board[enemyY, enemyX].currentState == TileState.EnemyHazard)
        {
            JSAnim.SetBool("IsHit", true);
            currHP -= PlayerMovement.board[enemyY, enemyX].damage;

            if(currHP <= 0 && LevelOfLaughs.levelOfLaughs >= 75)
            {
                retryScreen.SetActive(true);
                Time.timeScale = 0;
            }else if(currHP <= 0 && LevelOfLaughs.levelOfLaughs < 75)
            {
                currHP = 1;
            }

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

    public void colAttack()
    {

        Card column = new Card();
        column = CardDatabase.jokeDictionary[101];

        for (int j = 0; j < enemyX; j++)
        {
            PlayerMovement.board[enemyY, j].currentState = TileState.Warning;
            PlayerMovement.board[enemyY, j].upcomingJoke = column;

        }
        alreadyAttacked = true;
    }

    public void recalculateHPBar()
    {
        float hpRatio = (float)currHP / (float)maxHP;
        if (hpRatio <= 0)
        {
            hpRatio = 0;
        }
        float newBarLength = hpRatio * maxBarLength;
        hpBar.transform.localScale = new Vector2(newBarLength, hpBar.transform.localScale.y);
    }

    /*
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
    */

}
