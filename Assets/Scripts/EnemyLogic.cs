using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyLogic : MonoBehaviour
{
    public static int enemyX = 6, enemyY = 1;
    bool alreadyAttacked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnHandoff.enemyMovePhase)
        {
            alreadyAttacked = false;

            int xMovement = Random.Range(-1, 2);
            
            if(enemyX + xMovement > 3 && enemyX + xMovement < 8)
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

}
