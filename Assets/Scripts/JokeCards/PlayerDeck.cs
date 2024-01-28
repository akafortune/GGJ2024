
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> playerDeck = new List<Card>();
    public List<Card> discardDeck = new List<Card>();

    public TextMeshProUGUI cardsInDeck;


    void Awake()
    {
        System.String decklist = "1,1,1,1,2,2,2,2,3,3,3,3,4,4,4,4,5,5,5,5,1,1,1,1,2,2,2,2,3,3,3,3,4,4,4,4,5,5,5,5";
            //RuntimeText.ReadString();
        string[] cardIds = decklist.Split(",");
        for (int i = 0; i < cardIds.Length; i++) {
            try
            {
                int j = int.Parse(cardIds[i]);
                playerDeck.Add(CardDatabase.jokeDictionary[j]);
            }
            catch(FormatException e)
            {
                Debug.Log(e.Message);
            }
        }

        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        cardsInDeck.SetText(playerDeck.Count.ToString());
    }

    public void FullShuffle()
    {
        if(!TurnHandoff.castPhase)
        {
            //if not players turn to cast, don't shuffle
            return;
        }

        //Put cards in hand back in deck
        //Need to iterate twice without hard copy
        int k = PlayerHand.playerHand.Count;
        for (int i = 0; i < k; i++)
        {
            playerDeck.Add(PlayerHand.playerHand[i]);
        }
        for (int i = 0; i < k; i++)
        {
            PlayerHand.playerHand.RemoveAt(0);
        }

        //Put cards in discard back into deck
        //Need to iterate twice without hard copy
        k = discardDeck.Count;
        for (int i = 0; i < k; i++)
        {
            playerDeck.Add(discardDeck[i]);

        }
        for (int i = 0; i < k; i++)
        {
            discardDeck.RemoveAt(0);
        }

        Shuffle();

        DrawCard();
        DrawCard();
        DrawCard();
        DrawCard();
    }

    public void Shuffle()
    {
        Card container;

        for (int i = 0; i < playerDeck.Count; i++)
        {
            int k = UnityEngine.Random.Range(i, playerDeck.Count);
            container = playerDeck[i];
            playerDeck[i] = playerDeck[k];
            playerDeck[k] = container;
        }
    }

    public void DrawCard()
    {
        if (playerDeck.Count <= 0)
        {
            return;
        }
        //Add the top card of the deck to player hand
        int topDeck = playerDeck.Count - 1;
        PlayerHand.playerHand.Add(playerDeck[topDeck]);
        playerDeck.RemoveAt(topDeck);
    }
}
