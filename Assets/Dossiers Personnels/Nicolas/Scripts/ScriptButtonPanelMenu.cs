using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptButtonPanelMenu : MonoBehaviour
{

    private Text monText;
    private Button monButton;
    
    public GameObject panelClassement;
    public GameObject panelCommandes;
    public GameObject panelSons;

    // Use this for initialization
    void Start()
    {
        panelClassement.SetActive(false);
        panelCommandes.SetActive(false);
        panelSons.SetActive(false);
    }

    public void LoadSceneJouer()
    {
        SceneManager.LoadScene("Jouer");
        monText.fontSize = 20;
    }

    public void PanelClassement()
    {
        panelClassement.SetActive(true);
        monText.fontSize = 20;
    }

    public void PanelCommandes()
    {
        panelCommandes.SetActive(true);
        monText.fontSize = 20;
    }

    public void PanelSons()
    {
        panelSons.SetActive(true);
        monText.fontSize = 20;
    }

    public void Quitter()
    {
        Application.Quit();
    }

    public void AugmenterTaille()
    {
        monText = this.gameObject.GetComponentInChildren<Text>();
        monText.fontSize = 30;
        switch(monText.text)
        {
            case "Jouer":
                monText.text = "+ Jouer +";
                break;

            case "Classement":
                monText.text = "; Classement ;";
                break;

            case "Commandes":
                monText.text = "' Commandes '";
                break;

            case "Sons":
                monText.text = "= Sons =";
                break;

            case "Quitter":
                monText.text = "$ Quitter $";
                break;
        }
    }

    public void DiminuerTaille()
    {
        monText = this.gameObject.GetComponentInChildren<Text>();
        monText.fontSize = 20;
        switch (monText.text)
        {
            case "+ Jouer +":
                monText.text = "Jouer";
                break;

            case "; Classement ;":
                monText.text = "Classement";
                break;

            case "' Commandes '":
                monText.text = "Commandes";
                break;

            case "= Sons =":
                monText.text = "Sons";
                break;

            case "$ Quitter $":
                monText.text = "Quitter";
                break;
        }

    }
}