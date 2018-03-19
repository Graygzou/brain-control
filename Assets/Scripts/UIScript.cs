using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {
    [SerializeField]
    private Image player1Hp;
    [SerializeField]
    private Image player1Stamina;
    [SerializeField]
    private Text player1ScoreTxt;
    private int player1Score;
    [SerializeField]
    private Text player1KillsTxt;
    private int player1Kills;
    private GameObject player1;


    [SerializeField]
    private Image player2Hp;
    [SerializeField]
    private Image player2Stamina;
    [SerializeField]
    private Text player2ScoreTxt;
    private int player2Score;
    [SerializeField]
    private Text player2KillsTxt;
    private int player2Kills;
    private GameObject player2;


    // Update is called once per frame
    void Update () {
        if(player1 == null || player2 == null)
        {
            GameObject[] listPlayer = GameObject.FindGameObjectsWithTag("Player");
            player1 = listPlayer[0];
            player2 = listPlayer[1];
        }

        else
        {

            player1Hp.fillAmount = player1.GetComponent<PlayerStatus>().GetEnergyRatio();
            player1Stamina.fillAmount = player1.GetComponent<PlayerCon2d>().GetCurrentStaminaRatio();
            player2Hp.fillAmount = player2.GetComponent<PlayerStatus>().GetEnergyRatio();
            player2Stamina.fillAmount = player2.GetComponent<PlayerCon2d>().GetCurrentStaminaRatio();

            player1Score = player1.GetComponent<PlayerStatus>().GetScore();
            player2Score = player2.GetComponent<PlayerStatus>().GetScore();
            player1ScoreTxt.text = "Score: " + player1Score;
            player2ScoreTxt.text = "Score: " + player2Score;

            player1Kills = player1.GetComponent<PlayerStatus>().GetKills();
            player2Kills = player2.GetComponent<PlayerStatus>().GetKills();
            player1KillsTxt.text = "Kills: " + player1Kills;
            player2KillsTxt.text = player2Kills + " :Kills";

        }
    }

    public GameObject GetPlayer1()
    {
        return player1;
    }
    public GameObject GetPlayer2()
    {
        return player2;
    }
}
