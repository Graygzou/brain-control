using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParametreSons : MonoBehaviour
{
    private VolumeGeneral VolGeneral;
    public string volume;
    private Slider monSlider;

    // Use this for initialization
    void Start()
    {
        monSlider = this.gameObject.GetComponent<Slider>();
    }

    public void Volume()
    {
        VolGeneral.SetVolume(monSlider.value.ToString());
    }
}
