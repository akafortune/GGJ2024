using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Tile : MonoBehaviour 
{
    public GameObject tileObj;
    public TileState currentState;
    public int damage;
    public string statusEffect;

    public Card upcomingJoke;

    private float warningTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentState = TileState.Base;
        damage = 0;
        statusEffect = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == TileState.PlayerHazard && currentState == TileState.EnemyHazard)
        {
            currentState = TileState.Base;
            this.damage = 0;
            this.statusEffect = string.Empty;
        } 
        else if (currentState == TileState.Warning)
        {
            tileObj.GetComponent<SpriteRenderer>().color = Color.yellow;
            warningTimer += Time.deltaTime;
            if (warningTimer >= 1)
            {
                ActivateJoke();
            }
        }
    }

    public void ActivateJoke()
    {
        if(upcomingJoke.isPlayer)
        {
            currentState = TileState.EnemyHazard;
        } else
        {
            currentState = TileState.PlayerHazard;
        }

        this.damage = CardDatabase.DamageFunction(upcomingJoke);
        this.statusEffect = upcomingJoke.statusEffect;

        upcomingJoke = null;
    }

    public void ResetTile()
    {
        currentState = TileState.Base;
        damage = 0;
        statusEffect = string.Empty;
    }

    public void SetTileObj(GameObject TileObj)
    {
        tileObj = TileObj;
    }

    public Transform GetTilePos()
    {
        return tileObj.transform;
    }
}
