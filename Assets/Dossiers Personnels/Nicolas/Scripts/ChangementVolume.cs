using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangementVolume : MonoBehaviour
{

    public Slider Volume;
    public AudioSource maMusic;

    // Update is called once per frame
    void Update()
    {
        maMusic.volume = Volume.value;
    }
}