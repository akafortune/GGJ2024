using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public int id;
    public bool isPlayer;

    public string jokeName;
    public string jokeDescription;
    public JokeType jokeType;

    //Determines if the attackRange is an offset (true) or if it is
    //set position (false)
    //If attackRange is null it does not hit any tiles!
    public bool relative;
    public List<Vector2> attackRange = new List<Vector2>();
    public int baseDamage;

    public string statusEffect;

    public Card()
    {
        id = -1;
    }
    public Card(int Id, string JokeName, string JokeDescription, JokeType JokeType)
    {
        this.id = Id;
        this.isPlayer = true;
        this.jokeName = JokeName;
        this.jokeDescription = JokeDescription;
        this.jokeType = JokeType;

        this.relative = false;
        this.attackRange = null;
        this.baseDamage = 0;  
        this.statusEffect = string.Empty;
    }

    public Card(int Id, string JokeName, string JokeDescription, JokeType JokeType,
        bool Relative, List<Vector2> AttackRange, int BaseDamage)
    {
        this.id = Id;
        this.isPlayer = true;
        this.jokeName = JokeName;
        this.jokeDescription = JokeDescription;
        this.jokeType = JokeType;

        this.relative = Relative;
        this.attackRange = AttackRange;
        this.baseDamage = BaseDamage;
        this.statusEffect = string.Empty;
    }

    public Card(int Id, bool IsPlayer, string JokeName, string JokeDescription, JokeType JokeType,
        bool Relative, List<Vector2> AttackRange, int BaseDamage)
    {
        this.id = Id;
        this.isPlayer = IsPlayer;
        this.jokeName = JokeName;
        this.jokeDescription = JokeDescription;
        this.jokeType = JokeType;

        this.relative = Relative;
        this.attackRange = AttackRange;
        this.baseDamage = BaseDamage;
        this.statusEffect = string.Empty;
    }

    public string GetName()
    {
        return this.jokeName;
    }

    public string GetDescription()
    {
        return this.jokeDescription;
    }

    public string GetJokeType()
    {
        return this.jokeType.ToString();
    }
}
