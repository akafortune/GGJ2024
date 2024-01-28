using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Tile {
    public TileState currentState;
    public int damage;
    public string statusEffect;
    public GameObject tileGameObject;

    public Card upcomingJoke;

    public Tile()
    {
        this.currentState = TileState.Base;
        this.damage = 0;
        this.statusEffect = string.Empty;
        this.tileGameObject = null;
    }

    public Tile(TileState CurrentState, int Damage, string StatusEffect, GameObject tileGameObject)
    {
        this.currentState = CurrentState;
        this.damage = Damage;
        this.statusEffect = StatusEffect;
        this.tileGameObject = tileGameObject;
    }

    public void ResetTile()
    {

    }
}
