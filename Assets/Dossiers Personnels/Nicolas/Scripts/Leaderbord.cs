using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Leaderbord : MonoBehaviour
{

    private GameData json = new GameData();
    private List<PlayerData> list = new List<PlayerData>();
    
    public int nbRanges;
    private List<Rows> ranges = new List<Rows>();
    private List<PlayerData> datas = new List<PlayerData>();
    private List<string> classement = new List<string>();

    // Use this for initialization
    void Awake()
    {
        list = json.LoadPlayerData();
        foreach (PlayerData l in list)
        {
            PlayerData p = new PlayerData(l.pseudo, l.score);
            datas.Add(p);
        }

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



        int a = 0;

        foreach (Transform ligneChild in transform)
        {
            if (a != 0)
            {
                foreach (Transform child in ligneChild)
                {
                    if (a < datas.Count + 1)
                    {
                        int c = a;
                        Text texte = child.gameObject.GetComponent<Text>();
                        switch (child.name)
                        {
                            case "Place":
                                texte.text = c.ToString();
                                break;

                            case "Pseudo":
                                texte.text = datas[a - 1].Name;
                                break;

                            case "Score":
                                texte.text = datas[a - 1].Score.ToString();
                                break;
                        }
                    }
                    else
                    {
                        Text texte = child.gameObject.GetComponent<Text>();
                        switch (child.name)
                        {
                            case "Place":
                                texte.text = "--";
                                break;

                            case "Pseudo":
                                texte.text = "---";
                                break;

                            case "Score":
                                texte.text = "---";
                                break;
                        }
                    }
                }
            }
            a++;
        }

        
        //Test

    }

    // Update is called once per frame
    void Update()
    {

    }
}
