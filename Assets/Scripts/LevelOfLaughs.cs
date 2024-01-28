using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOfLaughs : MonoBehaviour
{
    public GameObject lolBar;
    public static float levelOfLaughs = 100, maxLOL = 100;
    public static JokeType favored;
    public float maxBarSize = 16.5f;
    // Start is called before the first frame update
    void Start()
    {
        int jokeTypeNum = Random.Range(0, 4);

        switch(jokeTypeNum)
        {
            case 0:
                favored = JokeType.Witty; 
                break;
            case 1:
                favored=JokeType.Surreal; 
                break;
            case 2:
                favored = JokeType.Slapstick;
                break;
            case 3:
                favored = JokeType.Corny;
                break;
        }

        Debug.Log(favored.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        recalculateLOLBar();
    }

    public static void increaseLOL(Card joke)
    {
        int bonusAmt;
        if(joke.jokeType == favored)
        {
            bonusAmt = 10;
        } else
        {
            bonusAmt = 5;
        }

        if ((levelOfLaughs + bonusAmt) >= 100)
        {
            levelOfLaughs = 100;
        } else
        {
            levelOfLaughs += bonusAmt;
        }

        //Debug.Log(levelOfLaughs.ToString());
    }

    public void recalculateLOLBar()
    {
        float lolRatio = levelOfLaughs / maxLOL;
        float newBarLength = lolRatio * maxBarSize;
        lolBar.transform.localScale = new Vector2(newBarLength, lolBar.transform.localScale.y);
    }
}
