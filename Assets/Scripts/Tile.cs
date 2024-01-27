using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    GameObject tileObj;
    bool blocked;
    bool dangerous;
    bool damage;

    public Tile()
    {
        damage = false;
        blocked= false;
        dangerous= false;
        tileObj= new GameObject();
    }

    public void setTileObj(GameObject tileObj)
    {
        this.tileObj= tileObj;
    }

    public Transform getTilePos()
    {
        return this.tileObj.transform;
    }

    public void dangerOn()
    {
        this.dangerous = true;
        this.tileObj.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public void reset()
    {
        this.damage = false;
        this.dangerous = false;
        this.tileObj.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void damageOn()
    {
        this.damage = true;
        this.tileObj.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public bool getDamage()
    {
        return this.damage;
    }
}
