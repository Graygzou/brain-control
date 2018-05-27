using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass {

    private int id;
    private bool isAlive;

    public TestClass(int id)
    {
        this.id = id;
        isAlive = true;
    }

    public void Death()
    {
        isAlive = false;
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public void Revive(int team)
    {
        isAlive = true;
    }
}
