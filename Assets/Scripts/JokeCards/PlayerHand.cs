using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerHand : MonoBehaviour
{
    public static List<Card> playerHand;
    public static int currentCard;
    public static int currentFlow;

    public PlayerDeck deck;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI cardIndexText;
    public TextMeshProUGUI jokeTypeText;
    public TextMeshProUGUI flowText;

    public int BASE_FLOW = 2;

    public Animator earlAnim;

    private void Start()
    {
        playerHand = new List<Card>();
        deck.DrawCard();
        currentCard = 0;
    }

    // Update is called once per frame
    void Update()
    {
        earlAnim.SetBool("Throwing", false);
        earlAnim.SetBool("Casting", false);
        if (playerHand.Count <= 0)
        {
            nameText.SetText("Out of jokes");
            descriptionText.SetText("Get better material!");
            cardIndexText.SetText("");
            jokeTypeText.SetText("None");
        }
        else
        {
            //Brute force bugfixing if block
            if (currentCard >= playerHand.Count || currentCard < 0)
            {
                currentCard = 0;
            }

            string currCardNum = (currentCard + 1).ToString();
            nameText.SetText(playerHand[currentCard].GetName());
            descriptionText.SetText(playerHand[currentCard].GetDescription());
            cardIndexText.SetText(currCardNum + " / " + playerHand.Count.ToString());
            jokeTypeText.SetText(playerHand[currentCard].GetJokeType());
        }

        flowText.SetText(currentFlow.ToString());

        //Valid inputs only during CastPhase
        if (TurnHandoff.castPhase)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (currentCard - 1 < 0)
                {
                    currentCard = playerHand.Count - 1;
                }
                else
                {
                    currentCard--;
                }
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (currentCard + 1 >= playerHand.Count)
                {
                    currentCard = 0;
                }
                else
                {
                    currentCard++;
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                earlAnim.SetBool("Throwing", true);
                ThrowJoke();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                earlAnim.SetBool("Casting", true);
                TellJoke();
            }

            //End turn is in PlayerMovement script
            
        }
    }

    public void TellJoke()
    {
        if (playerHand.Count <= 0)
        {
            return;
        }

        if (currentFlow <= 0)
        {
            return;
        } 
        else
        {
            currentFlow--;
        }

        Card currentJoke = playerHand[currentCard];
        deck.discardDeck.Add(currentJoke);
        playerHand.RemoveAt(currentCard);

        if (currentCard != 0)
        {
            currentCard -= 1;
        } 
        else
        {
            currentCard = 0;
        }

        int x = PlayerMovement.playerX;
        int y = PlayerMovement.playerY;

        CardDatabase.AdditionalJokeEffect(currentJoke, deck);

        //Board is [y, x], Player is 0-3, Enemy is 4-7
        bool relative = CardDatabase.jokeDictionary[currentJoke.id].relative;
        List <Vector2> currentRange = CardDatabase.jokeDictionary[currentJoke.id].attackRange;

        if (currentRange == null)
        {
            LevelOfLaughs.increaseLOL(currentJoke);
            return;
        }

        if (relative)
        {
            for (int i = 0; i < currentRange.Count; i++)
            {
                
                int offsetX = x + (int) currentRange[i].x;
                int offsetY = y + (int) currentRange[i].y;

                //Debug.Log(currentRange[i].x + ", " + currentRange[i].y);

                if (offsetX > 5 || offsetX < 0 || offsetY > 2 || offsetY < 0)
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

        LevelOfLaughs.increaseLOL(currentJoke);
    }

    public void ThrowJoke()
    {
        if (playerHand.Count <= 0)
        {
            return;
        }

        if (currentFlow <= 0)
        {
            return;
        }
        else
        {
            
            currentFlow--;
        }
        
        deck.discardDeck.Add(playerHand[currentCard]);
        playerHand.RemoveAt(currentCard);

        //CardDatabase.DiscardEffect()

        //Decrement enemy health
        EnemyLogic.currHP -= 1;
    }
}
