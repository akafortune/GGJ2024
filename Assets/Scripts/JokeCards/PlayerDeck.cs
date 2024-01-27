using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> playerDeck = new List<Card>();
    public List<Card> discardDeck = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        System.String decklist = RuntimeText.ReadString();
        string[] cardIds = decklist.Split();
        for (int i = 0; i < cardIds.Length; i++) {
            try
            {
                int j = System.Int32.Parse(cardIds[i]);
                playerDeck.Add(CardDatabase.jokeDictionary[j]);
            }
            catch
            {
                Debug.Log("Failed to parse PlayerDeck.txt while building" +
                    "PlayerDeck at card #" + i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FullShuffle()
    {
        //Put cards in hand back in deck
        List<Card> hand = PlayerHand.playerHand;
        for (int i = 0; i < hand.Count; i++)
        {
            playerDeck.Add(hand[i]);
            PlayerHand.playerHand.RemoveAt(i);
        }

        //Put cards in discard back into deck
        for (int i = 0; i < discardDeck.Count; i++)
        {
            playerDeck.Add(discardDeck[i]);
            discardDeck.RemoveAt(i);
        }

        Shuffle();
    }

    public void Shuffle()
    {
        Card container;

        for (int i = 0; i < playerDeck.Count; i++)
        {
            int k = Random.Range(i, playerDeck.Count);
            container = playerDeck[i];
            playerDeck[i] = playerDeck[k];
            playerDeck[k] = container;
        }
    }

    public void DrawCard()
    {
        //Add the top card of the deck to player hand
        int topDeck = playerDeck.Count - 1;
        PlayerHand.playerHand.Add(playerDeck[topDeck]);
        playerDeck.RemoveAt(topDeck);
    }
}
