using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptButtonRetour : MonoBehaviour {

    private Text monText;

    public GameObject panelClassement;
    public GameObject panelCommandes;
    public GameObject panelSons;
    public Image b;

    private ScriptMenuManette smm;

    void Start()
    {
        smm = GameObject.Find("PanelMenu").GetComponentInChildren<ScriptMenuManette>();
        b.enabled = false;
    }

    void Update()
    {
        if (!smm.joystic)
        {
            b.enabled = false;
        }
        if(smm.joystic)
        {
            b.enabled = true;
        }

        if (Input.GetButtonDown("bButton"))
        {
            panelClassement.SetActive(false);
            panelCommandes.SetActive(false);
            panelSons.SetActive(false);
        }
    }

    public void RetourMenuClassment()
    {
        panelClassement.SetActive(false);
        monText.fontSize = 14;
    }

    public void RetourMenuCommandes()
    {
        panelCommandes.SetActive(false);
        monText.fontSize = 14;
    }

    public void RetourMenuSons()
    {
        panelSons.SetActive(false);
        monText.fontSize = 14;
    }
    
    public void AugmenterTaille()
    {
        monText = this.gameObject.GetComponentInChildren<Text>();
        monText.fontSize = 18;
    }

    public void DiminuerTaille()
    {
        monText = this.gameObject.GetComponentInChildren<Text>();
        monText.fontSize = 14;
    }
}
