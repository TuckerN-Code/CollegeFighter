using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.Scripts.Characters;


namespace UnityEngine.TestTools
{
    public class EqualityTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void CompareTwoEIOF_BothDefault()
        {
            EnableInputsOnFrame e = new EnableInputsOnFrame();
            EnableInputsOnFrame i = new EnableInputsOnFrame();

            Assert.IsTrue(e.Equals(i));
        }

        [Test]
        public void CompareTwoEIOF_OneDefault()
        {
            EnableInputsOnFrame e = new EnableInputsOnFrame();
            EnableInputsOnFrame i = new EnableInputsOnFrame();
            i.Light_Button_Pressed = true;

            Assert.IsFalse(e.Equals(i));
        }

        [Test]
        public void CompareTwoEIOFHashCode_BothDefault()
        {
            EnableInputsOnFrame e = new EnableInputsOnFrame();
            EnableInputsOnFrame i = new EnableInputsOnFrame();

            Assert.IsTrue(e.GetHashCode() == i.GetHashCode());
        }

        [Test]
        public void CompareTwoEIOFHashCode_OneDefault()
        {
            EnableInputsOnFrame e = new EnableInputsOnFrame();
            EnableInputsOnFrame i = new EnableInputsOnFrame();
            i.Start_Button_Pressed = true;

            Assert.IsFalse(e.GetHashCode() == i.GetHashCode());
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator EqualityTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
