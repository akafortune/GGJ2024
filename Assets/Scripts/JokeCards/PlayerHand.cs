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
    public TextMeshProUGUI cardIndexText;

    

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
            cardIndexText.SetText("");
        }
        else
        {
            string currCardNum = (currentCard + 1).ToString();
            nameText.SetText(playerHand[currentCard].GetName());
            descriptionText.SetText(playerHand[currentCard].GetDescription());
            cardIndexText.SetText(currCardNum + " / " + playerHand.Count.ToString());
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currentCard -1 < 0)
            {
                currentCard = playerHand.Count - 1;
            }
            else
            {
                currentCard--;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currentCard +1 >= playerHand.Count)
            {
                currentCard = 0;
            } else
            {
                currentCard++;
            }
        }
    }

    public void TellJoke()
    {
        Card currentJoke = playerHand[currentCard];
        deck.discardDeck.Add(currentJoke);
        playerHand.RemoveAt(currentCard);

        int x = PlayerMovement.playerX;
        int y = PlayerMovement.playerY;

        CardDatabase.AdditionalJokeEffect(currentJoke, deck);

        //Board is [y, x], Player is 0-3, Enemy is 4-7
        bool relative = CardDatabase.jokeDictionary[currentJoke.id].relative;
        List <Vector2> currentRange = CardDatabase.jokeDictionary[currentJoke.id].attackRange;

        if (currentRange == null)
        {
            return;
        }

        if (relative)
        {
            foreach (Vector2 offset in currentRange)
            {
                int offsetX = x + (int) offset.x;
                int offsetY = y + (int) offset.y;

                if (x > 7 || x < 0 || y > 2 || y < 0)
                {
                    break;
                }

                PlayerMovement.board[offsetY, offsetX].upcomingJoke = currentJoke;
                PlayerMovement.board[offsetY, offsetX].currentState = TileState.Warning;
            }
        }
        else
        {
            foreach (Vector2 position in currentRange)
            {
                int posX = (int) position.x;
                int posY = (int) position.y;

                PlayerMovement.board[posY, posX].upcomingJoke = currentJoke;
                PlayerMovement.board[posY, posX].currentState = TileState.Warning;
            }
        }
    }

    public void ThrowJoke()
    {
        Debug.Log("Throw card [INCOMPLETE]");
    }
}
