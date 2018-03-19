using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

    [SerializeField]
    private int healthGiven;

    public int GetHealth()
    {
        return healthGiven;
    }
}
