using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.Scripts.Characters;

namespace UnityEngine.TestTools
{
    public class InputDectionTest
    {

        public Character InitCharacter()
        {
            Character character = new Programmer();
            character.Grounded = true;
            character.Airborn = false;
            character.Meter = 100;
            return character;
        }
        public InputStorage InitShoto()
        {
            InputStorage shoto = new InputStorage();
            List<Attack> attackList = new List<Attack>();
            attackList.Add(new Hadoken());
            attackList.Add(new Shoryuken());
            shoto.parent_AttackList = attackList;

            return shoto;
        }

        public class Hadoken : Attack
        {
            public Hadoken()
            {
                AttackConditions = new AttackConditions(true, false, 0);
                activationInput = CF_Action_Inputs.Light_Button;
                AllowedInputs = new List<List<CF_Direction_Inputs>>
                {
                    new List<CF_Direction_Inputs>
                    {
                        CF_Direction_Inputs.Down_Direction,
                        CF_Direction_Inputs.DownForward_Direction,
                        CF_Direction_Inputs.Forward_Direction
                    },
                };
            }
        }
        public class Shoryuken : Attack
        {
            public Shoryuken()
            {
                AttackConditions = new AttackConditions(true, false, 0);
                activationInput = CF_Action_Inputs.Light_Button;
                AllowedInputs = new List<List<CF_Direction_Inputs>>
                {
                    new List<CF_Direction_Inputs>
                    {
                        CF_Direction_Inputs.Forward_Direction,
                        CF_Direction_Inputs.Down_Direction,
                        CF_Direction_Inputs.DownForward_Direction
                    },
                };
            }
        }

        // A Test behaves as an ordinary method
        [Test]
        public void DoesEIOFHaveLight_True()
        {
            EnableInputsOnFrame e = new EnableInputsOnFrame();
            e.Light_Button_Pressed = true;

            Assert.IsTrue(e.Has(CF_Action_Inputs.Light_Button));
        }

        [Test]
        public void DoesEIOFHaveHeavy_True()
        {
            EnableInputsOnFrame e = new EnableInputsOnFrame();
            e.Heavy_Button_Pressed = true;

            Assert.IsTrue(e.Has(CF_Action_Inputs.Heavy_Button));
        }
        [Test]
        public void DoesEIOFHaveLow_True()
        {
            EnableInputsOnFrame e = new EnableInputsOnFrame();
            e.Low_Button_Pressed = true;

            Assert.IsTrue(e.Has(CF_Action_Inputs.Low_Button));
        }
        [Test]
        public void DoesEIOFHaveStart_True()
        {
            EnableInputsOnFrame e = new EnableInputsOnFrame();
            e.Start_Button_Pressed = true;

            Assert.IsTrue(e.Has(CF_Action_Inputs.Start_Button));
        }
        [Test]
        public void DoesEIOFHaveSelect_True()
        {
            EnableInputsOnFrame e = new EnableInputsOnFrame();
            e.Select_Button_Pressed = true;

            Assert.IsTrue(e.Has(CF_Action_Inputs.Select_Button));
        }
        [Test]
        public void DoesInputStorageContainQuarterCircle()
        {
            InputStorage shoto = InitShoto();

            shoto.Update(new InputOnFrame(CF_Direction_Inputs.Neutral_Input, new EnableInputsOnFrame(), 1));
            shoto.Update(new InputOnFrame(CF_Direction_Inputs.Down_Direction, new EnableInputsOnFrame(), 2));
            shoto.Update(new InputOnFrame(CF_Direction_Inputs.DownForward_Direction, new EnableInputsOnFrame(), 3));
            shoto.Update(new InputOnFrame(CF_Direction_Inputs.Forward_Direction, new EnableInputsOnFrame() { Light_Button_Pressed = true }, 4));
            Attack hado = shoto.parent_AttackList[0];
            Character chari = InitCharacter();

            Assert.IsTrue(shoto.CheckForAttackAllowed(hado, chari));
        }
        [Test]
        public void DoesInputStorageContainDragonPunch()
        {
            InputStorage shoto = InitShoto();

            shoto.Update(new InputOnFrame(CF_Direction_Inputs.Neutral_Input, new EnableInputsOnFrame(), 0));
            shoto.Update(new InputOnFrame(CF_Direction_Inputs.Forward_Direction, new EnableInputsOnFrame(), 1));
            shoto.Update(new InputOnFrame(CF_Direction_Inputs.Down_Direction, new EnableInputsOnFrame(), 2));
            shoto.Update(new InputOnFrame(CF_Direction_Inputs.DownForward_Direction, new EnableInputsOnFrame(), 3));

            Assert.IsTrue(shoto.CheckForAttackAllowed(shoto.parent_AttackList[1], InitCharacter()));
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.

    }
}
