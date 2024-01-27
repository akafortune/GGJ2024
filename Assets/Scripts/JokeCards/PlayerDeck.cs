using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public Queue<Card> playerDeck = new Queue<Card>();

    // Start is called before the first frame update
    void Start()
    {
        string decklist = RuntimeText.ReadString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
