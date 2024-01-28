using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileDisplay : MonoBehaviour
{
    public Tile linkedTile;

    public GameObject spriteDisplay;

    public float expStayTimer = 0, expStayTimerMax = 5f;

    public Sprite explosion, warning;

    public int yTileCoord, xTileCoord;

    public float warningTimer = 0;

    private void Start()
    {
        linkedTile = PlayerMovement.board[yTileCoord, xTileCoord];
    }

    void Update()
    {
        if(expStayTimer >= expStayTimerMax)
        {
            spriteDisplay.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (linkedTile.currentState == TileState.PlayerHazard || linkedTile.currentState == TileState.EnemyHazard)
        {
            linkedTile.currentState = TileState.Base;
            linkedTile.damage = 0;
            linkedTile.statusEffect = string.Empty;

        }
        else if (linkedTile.currentState == TileState.Warning)
        {
            expStayTimer += Time.deltaTime;

            spriteDisplay.GetComponent<SpriteRenderer>().enabled = true;
            spriteDisplay.GetComponent<SpriteRenderer>().sprite = warning;
            warningTimer += Time.deltaTime;
            if (warningTimer >= 1)
            {
                ActivateJoke();
                warningTimer = 0;
            }
        }
    }


    public void ActivateJoke()
    {
        
        if (linkedTile.upcomingJoke == null)
        {
            Debug.Log("Null ref to Tile in ActivateJoke()");
            linkedTile.currentState = TileState.Base;
            return;
        }

        if (linkedTile.upcomingJoke.isPlayer)
        {
           linkedTile.currentState = TileState.EnemyHazard;
        }
        else
        {
            linkedTile.currentState = TileState.PlayerHazard;
        }

        spriteDisplay.GetComponent<SpriteRenderer>().sprite = explosion;
        expStayTimer = 0;


        linkedTile.tileGameObject.GetComponent<SpriteRenderer>().color = Color.red;

        linkedTile.damage = CardDatabase.DamageFunction(linkedTile.upcomingJoke);
        linkedTile.statusEffect = linkedTile.upcomingJoke.statusEffect;

        linkedTile.upcomingJoke = null;
    }
}
