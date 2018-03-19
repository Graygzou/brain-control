using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialImage : MonoBehaviour {

    [SerializeField]
    private Image active1;
    [SerializeField]
    private Image inactive1;
    [SerializeField]
    private Image active2;
    [SerializeField]
    private Image inactive2;

    private Sprite player1SpriteSpecial;
    private Sprite player2SpriteSpecial;

    [SerializeField]
    private Sprite lmg;
    [SerializeField]
    private Sprite rifle;
    [SerializeField]
    private Sprite rpg;
    [SerializeField]
    private Sprite railgun;
    [SerializeField]
    private Sprite shotgun;
    [SerializeField]
    private Sprite pistol;
    [SerializeField]
    private Sprite none;

    private GameObject player1;
    private GameObject player2;

    void Update () {
		if(player1 == null || player2 == null)
        {
            GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
            player1 = playerList[0];
            player2 = playerList[1];
        }

        else
        {
            foreach(Transform child in player1.transform) if (child.CompareTag("Special"))
                {
                    string nomSpecial = child.GetComponent<special_shoot>().nom;
                    switch (nomSpecial)
                    {
                        case "lmg":
                            player1SpriteSpecial = lmg;
                            break;
                        case "rifle":
                            player1SpriteSpecial = rifle;
                            break;
                        case "rpg":
                            player1SpriteSpecial = rpg;
                            break;
                        case "railgun":
                            player1SpriteSpecial = railgun;
                            break;
                        case "shotgun":
                            player1SpriteSpecial = shotgun;
                            break;
                        default:
                            player1SpriteSpecial = none;
                            break;
                    }
                }
            if (player1.GetComponent<PlayerCon2d>().GetActive().CompareTag("Basic"))
            {
                active1.sprite = pistol;
                active1.GetComponentInChildren<Text>().text = "";
                inactive1.sprite = player1SpriteSpecial;
            }
            else
            {
                active1.sprite = player1SpriteSpecial;
                active1.GetComponentInChildren<Text>().text = "" + player1.GetComponentInChildren<special_shoot>().GetCurrentAmmo();
                inactive1.sprite = pistol;
            }
            foreach (Transform child in player2.transform) if (child.CompareTag("Special"))
                {
                    string nomSpecial = child.GetComponent<special_shoot>().nom;
                    switch (nomSpecial)
                    {
                        case "lmg":
                            player2SpriteSpecial = lmg;
                            break;
                        case "rifle":
                            player2SpriteSpecial = rifle;
                            break;
                        case "rpg":
                            player2SpriteSpecial = rpg;
                            break;
                        case "railgun":
                            player2SpriteSpecial = railgun;
                            break;
                        case "shotgun":
                            player2SpriteSpecial = shotgun;
                            break;
                        default:
                            player2SpriteSpecial = none;
                            break;
                    }
                }
            if (player2.GetComponent<PlayerCon2d>().GetActive().CompareTag("Basic"))
            {
                active2.sprite = pistol;
                active2.GetComponentInChildren<Text>().text = "";
                inactive2.sprite = player2SpriteSpecial;
            }
            else
            {
                active2.sprite = player2SpriteSpecial;
                active2.GetComponentInChildren<Text>().text = "" + player2.GetComponentInChildren<special_shoot>().GetCurrentAmmo();
                inactive2.sprite = pistol;
            }
        }
	}
}
