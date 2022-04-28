using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
namespace Assets.Scripts
{
    public class Attack
    {
        public int StartupFrames { get; }
        public int ActiveFrames { get; }
        public int RecoveryFrames { get; }
        public int HitStunFrames { get; }
        public int HitStopFrames { get; }
        public int ChipDamage { get; }
        public int HitDamage { get; }
    }

    public class InputDetector
    {
        private Stack<Input> inputBuffer;
        public InputDetector()
        {
            inputBuffer = new Stack<Input>();
        }

        public void NewInput(Input input)
        {
            if(input.Direction != inputBuffer.Last<Input>().Direction)
            inputBuffer.Push(input);
            
        }

        public void detectSpecial()
        {
        }
    }

    public struct Input
    {
        public CF_Direction_Inputs Direction { get; set; }
        public CF_Action_Inputs Action { get; set; }
        public int inputFrame { get; set; }
        public Input(CF_Direction_Inputs dir,CF_Action_Inputs act, int frame)
        {
            Direction = dir;
            Action = act;
            inputFrame = frame;
        }
    }

    public enum CF_Direction_Inputs
    {
        Neutral_Input = 5,
        None = 0,
        Down_Direction = 2,
        Left_Direction = 4,
        Up_Direction = 8,
        Right_Direction = 6,
        DownLeft_Direction = Down_Direction | Left_Direction,
        DownRight_Direction = Down_Direction | Right_Direction,
        UpLeft_Direction = Up_Direction | Left_Direction,
        UpRight_Direction = Up_Direction | Right_Direction,
        TapToHoldButtonShift = 19,
        HeldDown_Direction = Down_Direction << TapToHoldButtonShift,
        HeldLeft_Direction = Left_Direction << TapToHoldButtonShift,
        HeldUp_Direction = Up_Direction << TapToHoldButtonShift,
        HeldRight_Direction = Right_Direction << TapToHoldButtonShift,
        HeldDownLeft_Direction = HeldDown_Direction | HeldLeft_Direction,
        HeldDownRight_Direction = HeldDown_Direction | HeldRight_Direction,
        HeldUpLeft_Direction = HeldUp_Direction | HeldLeft_Direction,
        HeldUpRight_Direction = HeldUp_Direction | HeldRight_Direction,
    }

    public enum CF_Action_Inputs
    {
        None = 0,
        Light_Button = 1,
        Heavy_Button = 2,
        Low_Button = 3,
        Selection_Button = 4,
        Cencel_Button = 5,
        Menu_Pause_Button = 6,
    }


    public struct AttackConditions
    {
        public bool Grounded { get; }
        public bool Airborne { get; }
        public int Meter { get; }
        public AttackConditions (bool gr, bool ab, int me)
        {
            Grounded = gr;
            Airborne = ab;
            Meter = me;
        }

        public bool IsAllowed(Character character)
        {
            if (character.Meter > Meter
                    && character.Grounded == Grounded
                    && character.Airborn == Airborne)
            return true;

            else
                return false;
        }
    }

    public abstract class SpecialAttack
    {
        public List<List<CF_Direction_Inputs>> AllowedInputs { get; protected set; }
        public CF_Action_Inputs activationInput { get; protected set; }
        public int Priority { get; protected set; }
        public int InputWindow { get; protected set; }
        public AttackConditions AttackConditions { get; protected set; }
        public Attack Attack { get; protected set; }
    }

    public class InputStorage
    {
        public List<Input> CF_Inputs;
        public InputStorage()
        {
            CF_Inputs = new List<Input>();
        }
        public void Update(Input input)
        {
            if(input.Direction != CF_Inputs.First().Direction)
            {
                CF_Inputs.Insert(0, input);
            }
            else if (input.Action != CF_Inputs.First().Action)
            {
                CF_Inputs.Insert(0, input);
            }
        }
        public bool CheckForSpecial(SpecialAttack special, Character character)
        {

            if (special.AttackConditions.IsAllowed(character))
            {
                foreach (List<CF_Direction_Inputs> allowedInput in special.AllowedInputs)
                {
                    Stack<CF_Direction_Inputs> inputStack = new Stack<CF_Direction_Inputs>(allowedInput);
                    foreach (Input i in GetInputsInFrameWindow(special))
                    {
                        if (i.Direction == inputStack.Peek())
                            inputStack.Pop();
                    }
                    if (inputStack.Count == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public List<Input> GetInputsInFrameWindow(SpecialAttack specialAttack)
        {
            List<Input> inputs = new List<Input>();
            int currentFrame = Time.frameCount;
            foreach (Input i in CF_Inputs.GetRange(0, specialAttack.InputWindow))
            {
                if(i.inputFrame - currentFrame <= specialAttack.InputWindow )
                {
                    inputs.Insert(0, i);
                }
            }
            return inputs;
        }
    }
}
