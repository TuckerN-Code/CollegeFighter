using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
namespace Assets.Scripts
{
    public class AttackInfo
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
        private readonly Stack<Input> inputBuffer;
        public InputDetector()
        {
            inputBuffer = new Stack<Input>();
        }

        public void NewInput(Input input)
        {
            if (input.Direction != inputBuffer.Last<Input>().Direction)
                inputBuffer.Push(input);
            
        }

        public void detectSpecial()
        {
            //Must finish input reading
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

        Light_Heavy_Button = Light_Button | Heavy_Button,
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

        // Checks if a character meets the requirments to do the attack
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

    public abstract class Attack
    {
        //List of allowed motion inputs. Special attacks will have quite a few
        public List<List<CF_Direction_Inputs>> AllowedInputs { get; protected set; }
        //The input required to activate the attack
        public CF_Action_Inputs activationInput { get; protected set; }
        //The priority of the attack. Higher means more important
        public int Priority { get; protected set; }
        //The amount of frames that will be checked to determine if
        //  the inputs were completed. 
        public int InputWindow { get; protected set; }
        //The conditions that must be met to execute the attack
        public AttackConditions AttackConditions { get; protected set; }
        //The information about the attack such as damage and stun
        public AttackInfo AttackInfo { get; protected set; }
    }

    public abstract class InputStorage
    {
        //Saved list of unique inputs
        public List<Input> CF_Inputs { get; set; }
        //List of the attacks that the parent has
        public List<Attack> parent_AttackList { get; set;}
        protected InputStorage(List<Attack> in_parent_AttackList)
        {
            CF_Inputs = new List<Input>();
            parent_AttackList = in_parent_AttackList;
        }
        //Adds an input the storage if either the Direction or Action input change
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

        public Attack GetBestAttack(Character character)
        {
            List<Attack> attackList = new List<Attack>();
            foreach(Attack attack in GetAttacksWithAction(CF_Inputs[0].Action).Where(x => CheckForAttackAllowed(x, character)))
            {
                attackList.Add(attack);
            }

            Attack bestAttack = attackList.First();
            foreach (Attack attack in attackList.Where(x => x.Priority > bestAttack.Priority))
            {
                bestAttack = attack;
            }
            return bestAttack;
        }

        public List<Attack> GetAttacksWithAction(CF_Action_Inputs input)
        {
            List<Attack> returnAttacks = new List<Attack>();
            foreach(Attack attack in parent_AttackList)
            {
                if(attack.activationInput == input)
                {
                    returnAttacks.Add(attack);
                }
            }
            return returnAttacks;
        }

        public bool CheckForAttackAllowed(Attack special, Character character)
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
        public List<Input> GetInputsInFrameWindow(Attack specialAttack)
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
