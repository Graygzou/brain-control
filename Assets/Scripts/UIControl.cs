
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour {
    [SerializeField]
    GameObject gameCan;
    [SerializeField]
    GameObject endCan;
    GameController Gm;
    int winner, score;
    [SerializeField]
    private Text WinnerText;
    [SerializeField]
    private InputField winnerName;
    [SerializeField]
    private Button submitButton, retryButton, quitButton;
    string winnerNameString;
    private bool over, started, done;
    private GameObject[] playerList;
    private GameData json = new GameData();
    private List<PlayerData> datas = new List<PlayerData>();
    private List<PlayerData> list = new List<PlayerData>();

    // Use this for initialization
    void Start () {
        Gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        started = false;
        done = false;
        gameCan.SetActive(true);
        endCan.SetActive(false);
        retryButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        score = 0;

        playerList = GameObject.FindGameObjectsWithTag("Player");
    }


    private void Update()
    {
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
        if ((!playerList[0].GetComponent<PlayerStatus>().GetAlive() || !playerList[1].GetComponent<PlayerStatus>().GetAlive()) && !over)
        {
            over = true;
            gameCan.SetActive(false);
            endCan.SetActive(true);
            winner = Gm.GetWinner();
        }
        if (over && !done)
        {
            done = true;
            WinnerText.text = "Player " + winner + " Won!\nPlease Enter Your Name Below!\n";
        }

    }

    public void onSubmit()
    { 
        foreach (GameObject player in playerList) if(player.GetComponent<PlayerCon2d>().GetnJoueur() == winner)
            {
                score = player.GetComponent<PlayerStatus>().GetScore();
            }
        winnerNameString = winnerName.text;
        //score = score et winnerNameString = nom du joueur
        
        // On crée datas
        list = json.LoadPlayerData();
        foreach (PlayerData l in list)
        {
            PlayerData p = new PlayerData(l.pseudo, l.score);
            datas.Add(p);
        }

        // On ajoute le joueur
        PlayerData test = new PlayerData(winnerNameString, score);

        // Si c'est un record
        if (test.score >= datas[datas.Count - 1].Score)
        {
            // On l'ajoute
            datas.Add(test);
            // On trie
            for (int b = datas.Count - 2; b >= 0; b--)
            {
                for (int c = 0; c <= b; c++)
                {
                    if (datas[c + 1].score > datas[c].score)
                    {
                        PlayerData t = datas[c + 1];
                        datas[c + 1] = datas[c];
                        datas[c] = t;
                    }
                }
            }

            // On réécris le Json
            string ct = "";
            int varTest = 0;
            if (datas.Count < 8)
                varTest = datas.Count;
            else
                varTest = 8;

            ct = ct + "{ \"pseudo\":\" " + datas[0].Name + "\", \"score\":\"" + datas[0].Score + "\"}";
            for (int v = 1; v < varTest; v++)
            {
                Debug.Log(varTest);
                ct = ct + "/{ \"pseudo\":\" " + datas[v].Name + "\", \"score\":\"" + datas[v].Score + "\"}";
            }
            
            string filePath = Path.Combine(Application.streamingAssetsPath, "player.json");
            File.WriteAllText(filePath, ct);
        }
        
        
        Destroy(winnerName.gameObject);
        Destroy(submitButton.gameObject);
        WinnerText.text = "Would you like to play again?";
        retryButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        SceneManager.LoadScene("Nicolas_Dev_Scene");
    }



}
