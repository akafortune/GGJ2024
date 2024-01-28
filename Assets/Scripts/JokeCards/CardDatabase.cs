using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static Dictionary<int, Card> jokeDictionary = new Dictionary<int, Card>();

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
        List<Vector2> temp1 = new List<Vector2>();
        temp1.Add(new Vector2(1, 0));
        temp1.Add(new Vector2(2, 0));
        temp1.Add(new Vector2(3, 0));
        temp1.Add(new Vector2(4, 0));
        temp1.Add(new Vector2(5, 0));
        temp1.Add(new Vector2(6, 0));
        temp1.Add(new Vector2(7, 0));

        jokeDictionary.Add(2,
            new Card(2, "On Fire",
            "Deal damage in a straight line.",
            JokeType.Witty,
            true,
            temp1,
            5));

        //Knock-Knock?
        /* Uses absolute values for the grid
         */
        List<Vector2> temp2 = new List<Vector2>();
        temp2.Add(new Vector2(3, 0));
        temp2.Add(new Vector2(3, 1));
        temp2.Add(new Vector2(3, 2));
        temp2.Add(new Vector2(5, 0));
        temp2.Add(new Vector2(5, 1));
        temp2.Add(new Vector2(5, 2));

        jokeDictionary.Add(3,
            new Card(3, "Knock-Knock?",
            "Deal damage to enemy in the front and back columns",
            JokeType.Corny,
            false,
            temp2,
            4));

        //Cartoonish Violence
        List<Vector2> temp = new List<Vector2>();
        temp.Add(new Vector2(4, 0));
        temp.Add(new Vector2(4, 1));
        temp.Add(new Vector2(4, 2));

        jokeDictionary.Add(4,
            new Card(4, "Cartoonish Violence",
            "Deal damage to enemy in the middle column with a falling piano",
            JokeType.Slapstick,
            false,
            temp,
            8));

        //The Banana Peel Incident
        temp = new List<Vector2>();
        temp.Add(new Vector2(3, 0));
        temp.Add(new Vector2(3, 2));
        temp.Add(new Vector2(5, 0));
        temp.Add(new Vector2(5, 2));

        jokeDictionary.Add(5,
            new Card(5, "Banana Peel",
            "Throw a banana peel into the four corners",
            JokeType.Slapstick,
            false,
            temp,
            6));
        //Enemy Exclusive Jokes

        //Beam
        List<Vector2> temp100 = new List<Vector2>();
        temp100.Add(new Vector2());
        temp100.Add(new Vector2());
        temp100.Add(new Vector2());
        temp100.Add(new Vector2());

        jokeDictionary.Add(100,
            new Card(100, false,
            "Joke Lazer",
            "\"BEAMU\" - Robo-Fortune",
            JokeType.None,
            true,
            temp100,
            8));
    }

    public static void AdditionalJokeEffect(Card joke, PlayerDeck deck)
    {
        
        switch (joke.id) {
            case 1:
                deck.DrawCard();
                PlayerHand.currentFlow += 1;
                break;
        }
    }
    public static int DamageFunction(Card joke)
    {
        return joke.baseDamage;
    }
}
