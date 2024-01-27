using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnHandoff.enemyMovePhase)
        {
            this.gameObject.transform.position = PlayerMovement.board[PlayerMovement.playerY, 6].getTilePos().position;
            TurnHandoff.movePhase= true;
        }
    }
}
