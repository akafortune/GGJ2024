using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyLogic : MonoBehaviour
{
    public static int enemyX = 6, enemyY = 1;
    float atkDelay = 1, atkDelayTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnHandoff.enemyMovePhase)
        {
            atkDelayTimer = 0;

            int xMovement = Random.Range(-1, 2);

            Debug.Log(xMovement);
            
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

            this.gameObject.transform.position = PlayerMovement.board[enemyY, enemyX].getTilePos().position;

            
            TurnHandoff.movePhase= true;
        }

        if(TurnHandoff.movePhase)
        {
            beamWarning();

            atkDelayTimer += Time.deltaTime;

            if(atkDelayTimer > atkDelay)
            {
                beamAttack();
            }
        }
    }

    public void beamWarning()
    {
        for (int j = 0; j < enemyX; j++)
        {
            PlayerMovement.board[enemyY, j].dangerOn();
        }
    }

    public void beamAttack()
    {
        for (int j = 0; j < enemyX; j++)
        {
            PlayerMovement.board[enemyY, j].damageOn();
        }
    }

}
