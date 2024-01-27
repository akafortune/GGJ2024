using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyLogic : MonoBehaviour
{
    public static int enemyX = 6;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnHandoff.enemyMovePhase)
        {
            int xMovement = Random.Range(-1, 2);

            Debug.Log(xMovement);
            
            if(enemyX + xMovement > 3 && enemyX + xMovement < 8)
            {
                enemyX += xMovement;
            }

            this.gameObject.transform.position = PlayerMovement.board[PlayerMovement.playerY, enemyX].getTilePos().position;
            TurnHandoff.movePhase= true;
        }
    }
}
