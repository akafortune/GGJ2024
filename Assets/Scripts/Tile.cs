using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    GameObject tileObj;
    bool blocked;
    bool dangerous;

    public Tile()
    {
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
}
