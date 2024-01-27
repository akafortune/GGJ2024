using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card", order = 1)]
public class Card : ScriptableObject
{
    public int id;

    public string jokeName;
    public string jokeDescription;
    public JokeType jokeType;

    //Determines if the attackRange is an offset (true) or if it is
    //set position (false)
    //If attackRange is null it does not hit any tiles!
    public bool relative;
    public List<Vector2> attackRange = new List<Vector2>();

    public Card(int Id, string JokeName, string JokeDescription, JokeType JokeType)
    {
        this.id = Id;
        this.jokeName = JokeName;
        this.jokeDescription = JokeDescription;
        this.jokeType = JokeType;

        this.relative = false;
        this.attackRange = null;
    }

    public Card(int Id, string JokeName, string JokeDescription, JokeType JokeType,
        bool Relative, List<Vector2> AttackRange)
    {
        this.id = Id;
        this.jokeName = JokeName;
        this.jokeDescription = JokeDescription;
        this.jokeType = JokeType;

        this.relative = Relative;
        this.attackRange = AttackRange;
    }
}
