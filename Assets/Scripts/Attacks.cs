using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public CF_Inputs Direction { get; set; }
        public int inputFrame { get; set; }
        public Input(CF_Inputs dir, int frame)
        {
            Direction = dir;
            inputFrame = frame;
        }
    }

    public enum CF_Inputs
    {
        Neutral_Input = 1 << 18,
        None = 0,
        Down_Direction = 1 << 0,
        Left_Direction = 1 << 1,
        Up_Direction = 1 << 2,
        Right_Direction = 1 << 3,
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
        Light_Button = 1 << 4,
        Heavy_Button = 1 << 5,
        Low_Button = 1 << 6,
        Selection_Button = 1 << 7,
        Cencel_Button = 1 << 8,
        Menu_Pause_Button = 1 << 9,
    }

    public class InputStorage
    {
        public LinkedList<Input> activeInputs;
        public List<Input> deadInputs;
        private int maxActiveInputs;

        public InputStorage()
        {
            activeInputs = new LinkedList<Input>();
            deadInputs = new List<Input>();
        }

        public void AddInput(Input input)
        {
            if(activeInputs.Count <= maxActiveInputs)
            {
                activeInputs.AddLast(input);
            }
            else if(activeInputs.Count > maxActiveInputs)
            {
                deadInputs.Add(activeInputs.First());
                activeInputs.RemoveFirst();
            }
        }

        public void checkSpecialInput()
        {
            
        }

    }
}
