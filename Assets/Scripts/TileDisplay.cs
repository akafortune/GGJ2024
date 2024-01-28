using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDisplay : MonoBehaviour
{
    public Tile linkedTile;

    public int yTileCoord, xTileCoord;

    public float warningTimer = 0;

    private void Start()
    {
        linkedTile = PlayerMovement.board[yTileCoord, xTileCoord];
    }

    void Update()
    {
        if (linkedTile.currentState == TileState.PlayerHazard || linkedTile.currentState == TileState.EnemyHazard)
        {
            linkedTile.currentState = TileState.Base;
            linkedTile.tileGameObject.GetComponent<SpriteRenderer>().color = Color.white;
            linkedTile.damage = 0;
            linkedTile.statusEffect = string.Empty;
        }
        else if (linkedTile.currentState == TileState.Warning)
        {
            linkedTile.tileGameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            warningTimer += Time.deltaTime;
            if (warningTimer >= 1)
            {
                Debug.Log("HAZARD DUTY PAY by JPEGMAFIA");
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

        linkedTile.tileGameObject.GetComponent<SpriteRenderer>().color = Color.red;

        linkedTile.damage = CardDatabase.DamageFunction(linkedTile.upcomingJoke);
        linkedTile.statusEffect = linkedTile.upcomingJoke.statusEffect;

        linkedTile.upcomingJoke = null;
    }
}
