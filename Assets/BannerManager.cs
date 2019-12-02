using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class BannerManager : MonoBehaviour
{


    private const string OPPONENT_TURN_MESSAGE = "OPPONENT'S TURN";
    private const string YOUR_TURN_MESSAGE = "YOUR TURN";
    private const string WINNING_MESSAGE = "YOU WIN!";
    private const string LOSING_MESSAGE = "YOU LOST!";

    [SerializeField]
    public Text turnText;

    [SerializeField]
    public Image endTurn;

    // Start is called before the first frame update
    void Start()
    {

        //turnText = GetComponent<Text>();
        //endTurn = GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {
       // ShowOpponentsTurn();
    }


    public void ShowYourTurn()
    {
        turnText.text = YOUR_TURN_MESSAGE;
        endTurn.gameObject.SetActive(true);

        // call a handler here  
    }


    public void ShowOpponentsTurn()
    {
        turnText.text = OPPONENT_TURN_MESSAGE;
        endTurn.gameObject.SetActive(false);

        // call a handler here  
    }


    public void ShowWinMessage()
    {
        turnText.text = WINNING_MESSAGE;
        endTurn.gameObject.SetActive(false);
        AudioManager.instance.Play("Victory");
        // end game here  

    }

    public void ShowLostMessage()
    {
        turnText.text = LOSING_MESSAGE;
        endTurn.gameObject.SetActive(false);
        AudioManager.instance.Play("Defeat");
        // end game here  
    }




}
