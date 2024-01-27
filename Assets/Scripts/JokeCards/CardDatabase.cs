using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static Dictionary<int, Card> jokeDictionary = new Dictionary<int, Card>();

    private List<Vector2> temp = new List<Vector2>();

    // Start is called before the first frame update
    void Awake()
    {
        //Add default joke
        jokeDictionary.Add(0,
            new Card(0, "None",
            "None",
            JokeType.None));

        //Well-Timed Break
        jokeDictionary.Add(1,
            new Card(1, "Well-Timed Break",
            "Gain 1 Flow and draw a new joke.",
            JokeType.Witty));

        //On Fire
        /* Offset values for which tiles to hit
         * values can go outside the grid if player is not in the back column
         * so add outofbounds checks in execution function
        */
        temp.Add(new Vector2(1, 0));
        temp.Add(new Vector2(2, 0));
        temp.Add(new Vector2(3, 0));
        temp.Add(new Vector2(4, 0));
        temp.Add(new Vector2(5, 0));
        temp.Add(new Vector2(6, 0));

        jokeDictionary.Add(2,
            new Card(2, "On Fire",
            "Deal damage proportional to the Level of Laughs in a straight line.",
            JokeType.Witty,
            true,
            temp,
            1));

        temp.Clear();

        //Knock-Knock?
        /* Uses absolute values for the grid
         */
        temp.Add(new Vector2(4, 0));
        temp.Add(new Vector2(4, 1));
        temp.Add(new Vector2(4, 2));
        temp.Add(new Vector2(7, 0));
        temp.Add(new Vector2(7, 1));
        temp.Add(new Vector2(7, 2));

        jokeDictionary.Add(3,
            new Card(3, "Knock-Knock?",
            "Deal damage to enemy in the front and back columns and push enemy " +
            "towards the center.",
            JokeType.Corny,
            false,
            temp,
            2));

        temp.Clear();

        //New Joke Here
    }
}
