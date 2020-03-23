using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using HumanoidCharacterClass;

public class HumanCharacterController_Tests {

    [Test]
    public void HumanCharacterController_TestsSimplePasses() {
        // Use the Assert class to test conditions.
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator HumanCharacterController_TestsWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }

    [SetUp]
    public void HumanCharacterController_TestsSetup()
    {
        var hcc = new HumanoidCharacterClass.HumanoidCharacter();
    }

    [Test]
    public void HumanCharacterController_TestEquipItems()
    {
        ////arrange
        //var hcc = new Humanoid_CharacterController();
        ////act
        //for(int i =0; i < hcc.getMaxItems(); i++)
        //{
        //    hcc.SetEquippedItem((ushort)i, i);
        //}
        ////assert
        //for (int i = 0; i < hcc.getMaxItems(); i++)
        //{
        //    Assert.Equals(hcc.getItemAtSlot(i), i);
        //}
    }
}
