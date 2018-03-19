using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeGeneral : MonoBehaviour {
    
    private Text monText;
	// Use this for initialization
	void Start () {
        monText = this.gameObject.GetComponent<Text>();
	}
	
	public void SetVolume(string vol) {
        monText.text = vol;
	}
}
