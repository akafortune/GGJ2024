using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandoff : MonoBehaviour
{
    public static bool movePhase = true;
    public static bool enemyMovePhase = false;
    public static bool castPhase = false;
    public float moveTimer = 0;
    public static float moveTimeLimit = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movePhase)
        {
            Debug.Log("Move Phase");
            enemyMovePhase = false;
            castPhase= false;
            moveTimer += Time.deltaTime;

            if(moveTimer > moveTimeLimit)
            {
                castPhase= true;
            }
        }
        
        if(castPhase)
        {
            resetBoard();
            Debug.Log(moveTimer);
            movePhase = false;
            enemyMovePhase = false;
            castPhase= true;
            moveTimer= 0;
        }
        
        if(enemyMovePhase)
        {
            castPhase = false;
            movePhase = false;
            Debug.Log("Enemy Move Phase");
        }
    }

    public void resetBoard()
    {
        foreach(Tile t in PlayerMovement.board)
        {
            t.reset();
        }
    }
}
