using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptMenuManette : MonoBehaviour {

    public Button boutonJouer;
    public Button boutonClassment;
    public Button boutonCommandes;
    public Button boutonSons;
    public Button boutonQuitter;

    public GameObject panelClassement;
    public GameObject panelCommandes;
    public GameObject panelSons;

    private Text monText;

    private bool jouerIsSelected = false;
    private bool classementIsSelected = false;
    private bool commandesIsSelected = false;
    private bool sonsIsSelected = false;
    private bool quitterIsSelected = false;
    public bool joystic = false;
    private bool valJoysticPrecedent;
    private bool selectionPossible = true;

    private int positionCourante;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Mathf.Abs(Input.GetAxis("Vertical1")) < 0.1)
        {
            selectionPossible = true;
        }

        if((Input.GetAxis("Vertical1") > 0.5 || Input.GetAxis("Vertical1") < -0.5) && !joystic)
        {
            joystic = true;
            Cursor.visible = false;
            jouerIsSelected = true;
            positionCourante = 0;
        }

        if ((Input.GetAxis("Vertical1") > 0.9 || Input.GetAxis("Vertical1") < -0.9) && joystic && selectionPossible)
        {
            selectionPossible = false;
            if(Input.GetAxis("Vertical1") > 0.9)
            {
                switch(positionCourante)
                {
                    case 0:
                        break;

                    case 1:
                        jouerIsSelected = true;
                        classementIsSelected = false;
                        break;

                    case 2:
                        classementIsSelected = true;
                        commandesIsSelected = false;
                        break;

                    case 3:
                        commandesIsSelected = true;
                        sonsIsSelected = false;
                        break;

                    case 4:
                        sonsIsSelected = true;
                        quitterIsSelected = false;
                        break;
                }
            }

            if (Input.GetAxis("Vertical1") < -0.9)
            {
                switch (positionCourante)
                {
                    case 0:
                        jouerIsSelected = false;
                        classementIsSelected = true;
                        break;

                    case 1:
                        classementIsSelected = false;
                        commandesIsSelected = true;
                        break;

                    case 2:
                        commandesIsSelected = false;
                        sonsIsSelected = true;
                        break;

                    case 3:
                        sonsIsSelected = false;
                        quitterIsSelected = true;
                        break;

                    case 4:
                        break;
                }
            }
        }

        if ((Mathf.Abs(Input.GetAxis("MHS")) > 8000 || Mathf.Abs(Input.GetAxis("MVS")) > 8000) && joystic)
        {
            Cursor.visible = true;
            joystic = false;
            jouerIsSelected = false;
            classementIsSelected = false;
            commandesIsSelected = false;
            sonsIsSelected = false;
            quitterIsSelected = false;
            bJouer();
            bClassement();
            bCommandes();
            bSons();
            bQuitter();

        }
        if(joystic)
        {
            bJouer();
            bClassement();
            bCommandes();
            bSons();
            bQuitter();
        }

        if (Input.GetButtonDown("aButton"))
        {
            switch (positionCourante)
            {
                case 0:
                    SceneManager.LoadScene("MergecScene");
                    break;

                case 1:
                    panelClassement.SetActive(true);
                    break;

                case 2:
                    panelCommandes.SetActive(true);
                    break;

                case 3:
                    panelSons.SetActive(true);
                    break;

                case 4:
                    Application.Quit();
                    break;
            }
        }
	}

    public void bJouer()
    {
        if (jouerIsSelected)
        {
            positionCourante = 0;
            monText = boutonJouer.GetComponentInChildren<Text>();
            monText.fontSize = 30;
            monText.text = "+ Jouer +";
        }
        if (!jouerIsSelected)
        {
            monText = boutonJouer.GetComponentInChildren<Text>();
            monText.fontSize = 20;
            monText.text = "Jouer";
        }
    }

    public void bClassement()
    {
        if (classementIsSelected)
        {
            positionCourante = 1;
            monText = boutonClassment.GetComponentInChildren<Text>();
            monText.fontSize = 30;
            monText.text = "; Classement ;";
        }
        if (!classementIsSelected)
        {
            monText = boutonClassment.GetComponentInChildren<Text>();
            monText.fontSize = 20;
            monText.text = "Classement";
        }
    }

    public void bCommandes()
    {
        if (commandesIsSelected)
        {
            positionCourante = 2;
            monText = boutonCommandes.GetComponentInChildren<Text>();
            monText.fontSize = 30;
            monText.text = "' Commandes '";
        }
        if (!commandesIsSelected)
        {
            monText = boutonCommandes.GetComponentInChildren<Text>();
            monText.fontSize = 20;
            monText.text = "Commandes";
        }
    }

    public void bSons()
    {
        if (sonsIsSelected)
        {
            positionCourante = 3;
            monText = boutonSons.GetComponentInChildren<Text>();
            monText.fontSize = 30;
            monText.text = "= Sons =";
        }
        if (!sonsIsSelected)
        {
            monText = boutonSons.GetComponentInChildren<Text>();
            monText.fontSize = 20;
            monText.text = "Sons";
        }
    }

    public void bQuitter()
    {
        if (quitterIsSelected)
        {
            positionCourante = 4;
            monText = boutonQuitter.GetComponentInChildren<Text>();
            monText.fontSize = 30;
            monText.text = "$ Quitter $";
        }
        if (!quitterIsSelected)
        {
            monText = boutonQuitter.GetComponentInChildren<Text>();
            monText.fontSize = 20;
            monText.text = "Quitter";
        }
    }

}
