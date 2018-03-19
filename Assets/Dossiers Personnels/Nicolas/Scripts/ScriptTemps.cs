using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptTemps : MonoBehaviour {

    private Text monText;
    private Image monImage;
    private int seconde=0;
    private int minute=0;
    private float temps;

    private GameController GM;

	// Use this for initialization
	void Start () {
        GM = GameObject.Find("GameController").GetComponent<GameController>();
        monImage = GetComponentInChildren<Image>();
        monText = GetComponentInChildren<Text>();
        monText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(!GM.GetLoseLifeWave())
        {
            if (monText.enabled == false)
            {
                monText.enabled = true;
            }
            if(monImage.enabled == false)
            {
                monImage.enabled = true;
            }
            temps = GM.GetTimeLimit() - GM.GetTimer();
            minute = (int)(temps / 60);
            seconde = (int)(temps % 60);

            if (minute < 10 && seconde > 9)
            {
                monText.text = "0" + minute + ":" + seconde;
            }
            else if (minute < 10 && seconde < 10)
            {
                monText.text = "0" + minute + ":0" + seconde;
            }
            else if (minute > 10 && seconde < 9)
            {
                monText.text = minute + ":0" + seconde;
            }
            else
            {
                monText.text = minute + ":" + seconde;
            }
        }
        else if(monText.enabled == true || monImage.enabled == true)
        {
            monImage.enabled = false;
            monText.enabled = false;
        }
	}
}
