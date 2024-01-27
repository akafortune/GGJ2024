using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHand : MonoBehaviour
{
    public static List<Card> playerHand;
    public static int currentCard;
    public static int currentFlow;

    public PlayerDeck deck;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public int BASE_FLOW = 2;

    private void Start()
    {
        playerHand = new List<Card>();
        deck.DrawCard();
        currentCard = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHand.Count <= 0)
        {
            nameText.SetText("Out of jokes");
            descriptionText.SetText("Get better material!");
        }
        else
        {
            nameText.SetText(playerHand[currentCard].GetName());
            descriptionText.SetText(playerHand[currentCard].GetDescription());
        }
    }

    public void TellJoke()
    {

    }
}
