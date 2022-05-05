using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.Scripts.Characters;

namespace UnityEditor.TestTools
{
    public class InputDectionTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void DoesEIOFHaveLight_True()
        {
            EnableInputsOnFrame e = new EnableInputsOnFrame();
            e.Light_Button_Pressed = true;

            Assert.IsTrue(e.Has(CF_Action_Inputs.Light_Button));
        }
        public void DoesEIOFHaveHeavy_True()
        {
            EnableInputsOnFrame e = new EnableInputsOnFrame();
            e.Heavy_Button_Pressed = true;

            Assert.IsTrue(e.Has(CF_Action_Inputs.Heavy_Button));
        }
        public void DoesEIOFHaveLow_True()
        {
            EnableInputsOnFrame e = new EnableInputsOnFrame();
            e.Low_Button_Pressed = true;

            Assert.IsTrue(e.Has(CF_Action_Inputs.Low_Button));
        }
        public void DoesEIOFHaveStart_True()
        {
            EnableInputsOnFrame e = new EnableInputsOnFrame();
            e.Start_Button_Pressed = true;

            Assert.IsTrue(e.Has(CF_Action_Inputs.Start_Button));
        }
        public void DoesEIOFHaveSelect_True()
        {
            EnableInputsOnFrame e = new EnableInputsOnFrame();
            e.Select_Button_Pressed = true;

            Assert.IsTrue(e.Has(CF_Action_Inputs.Select_Button));
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator InputDectionTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
