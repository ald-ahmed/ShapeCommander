﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class BannerManager : MonoBehaviour
{


    private const string OPPONENT_TURN_MESSAGE = "OPPONENT'S TURN";
    private const string YOUR_TURN_MESSAGE = "YOUR TURN";
    private const string WINNING_MESSAGE = "YOU WIN!";
    private const string LOSING_MESSAGE = "YOU LOST!";

    public Text turnText;
    public Button endTurn;

    // Start is called before the first frame update
    void Start()
    {

        turnText = GetComponent<Text>();
        endTurn = GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ShowYourTurn()
    {
        turnText.text = YOUR_TURN_MESSAGE;
        endTurn.gameObject.SetActive(true);

        // call a handler here  
    }


    void ShowOpponentsTurn()
    {
        turnText.text = OPPONENT_TURN_MESSAGE;
        endTurn.gameObject.SetActive(false);

        // call a handler here  
    }


    void ShowWinMessage()
    {
        turnText.text = OPPONENT_TURN_MESSAGE;
        endTurn.gameObject.SetActive(false);

        // end game here  

    }

    void ShowLostMessage()
    {
        turnText.text = LOSING_MESSAGE;
        endTurn.gameObject.SetActive(false);

        // end game here  
    }




}
