using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class NewEditModeReal {

	[Test]
	public void AliveWhenCreated() {
        TestClass testObj = new TestClass(1);
        Assert.AreEqual(true, testObj.IsAlive());
    }


    [Test]
    public void DeadWhenKilled()
    {
        TestClass testObj = new TestClass(1);
        testObj.Death();
        Assert.AreEqual(false, testObj.IsAlive());
    }

    [Test]
    public void AliveWhenRevive()
    {
        int team = 0;

        TestClass testObj = new TestClass(1);
        testObj.Death();
        Assert.AreEqual(false, testObj.IsAlive());
        testObj.Revive(team);
        Assert.AreEqual(true, testObj.IsAlive());
    }
}
