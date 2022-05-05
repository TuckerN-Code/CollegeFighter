using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Characters
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
        private readonly Stack<InputOnFrame> inputBuffer;
        public InputDetector()
        {
            inputBuffer = new Stack<InputOnFrame>();
        }

        public void NewInput(InputOnFrame input)
        {
            if (input.Direction != inputBuffer.Last<InputOnFrame>().Direction)
                inputBuffer.Push(input);

        }

        public void detectSpecial()
        {
            //Must finish input reading
        }
    }

    public struct InputOnFrame
    {
        public CF_Direction_Inputs Direction { get; set; }
        public EnableInputsOnFrame Actions { get; set; }
        public int inputFrame { get; set; }
        public InputOnFrame(CF_Direction_Inputs dir, EnableInputsOnFrame act, int frame)
        {
            Direction = dir;
            Actions = act;
            inputFrame = frame;
        }
    }

    public enum CF_Direction_Inputs
    {
        Neutral_Input = 5,
        None = 0,
        Down_Direction = 2,
        Forward_Direction = 6,
        Up_Direction = 8,
        Back_Direction = 4,
        DownForward_Direction = Down_Direction | Forward_Direction,
        DownBack_Direction = Down_Direction | Back_Direction,
        UpForward_Direction = Up_Direction | Forward_Direction,
        UpBack_Direction = Up_Direction | Back_Direction,
    }

    public enum CF_Action_Inputs
    {
        None = 0,
        Light_Button = 1,
        Heavy_Button = 2,
        Low_Button = 3,
        Start_Button = 4,
        Cencel_Button = 5,
        Select_Button = 6,
    }

    public class EnableInputsOnFrame
    {
        public bool Light_Button_Pressed { get; set; }
        public bool Heavy_Button_Pressed { get; set; }
        public bool Low_Button_Pressed { get; set; }
        public bool Start_Button_Pressed { get; set; }
        public bool Select_Button_Pressed { get; set; }

        public bool Has(CF_Action_Inputs input)
        {
            switch (input)
            {
                case CF_Action_Inputs.None:
                    {
                        if (!Light_Button_Pressed
                            && !Heavy_Button_Pressed
                            && !Low_Button_Pressed
                            && !Start_Button_Pressed
                            && !Select_Button_Pressed)
                            return true;
                        break;
                    }
                case CF_Action_Inputs.Light_Button:
                    {
                        if (Light_Button_Pressed)
                            return true;
                        break;
                    }
                case CF_Action_Inputs.Heavy_Button:
                    {
                        if (Heavy_Button_Pressed)
                            return true;
                        break;
                    }
                case CF_Action_Inputs.Low_Button:
                    {
                        if (Low_Button_Pressed)
                            return true;
                        break;
                    }
                case CF_Action_Inputs.Start_Button:
                    {
                        if (Start_Button_Pressed)
                            return true;
                        break;
                    }
                case CF_Action_Inputs.Select_Button:
                    {
                        if (Select_Button_Pressed)
                            return true;
                        break;
                    }
                default:
                    {
                        return false;
                    }
            }
            return false;
        }

        public static bool operator ==(EnableInputsOnFrame a, EnableInputsOnFrame b)
        {
            if (a.Light_Button_Pressed == b.Light_Button_Pressed
                && a.Heavy_Button_Pressed == b.Heavy_Button_Pressed
                && a.Low_Button_Pressed == b.Low_Button_Pressed
                && a.Start_Button_Pressed == b.Start_Button_Pressed
                && a.Select_Button_Pressed == b.Select_Button_Pressed)
                return true;
            else
                return false;
        }
        public static bool operator !=(EnableInputsOnFrame a, EnableInputsOnFrame b)
            => !(a == b);

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                EnableInputsOnFrame a = (EnableInputsOnFrame)obj;
                return (this == a);
            }
        }
        public override int GetHashCode()
        {
            int hash = 23;
            hash = hash * 23 + Light_Button_Pressed.GetHashCode();
            hash = hash * 23 + Heavy_Button_Pressed.GetHashCode();
            hash = hash * 23 + Low_Button_Pressed.GetHashCode();
            hash = hash * 23 + Start_Button_Pressed.GetHashCode();
            hash = hash * 23 + Select_Button_Pressed.GetHashCode();
            return hash;
        }
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
        public int Priority { get; set; }
        //The amount of frames that will be checked to determine if
        //  the inputs were completed. 
        public int InputWindow { get; protected set; }
        //The conditions that must be met to execute the attack
        public AttackConditions AttackConditions { get; protected set; }
        //The information about the attack such as damage and stun
        public AttackInfo AttackInfo { get; protected set; }
    }

    public class InputStorage
    {
        //Saved list of unique inputs
        public List<InputOnFrame> CF_Inputs { get; set; }
        //List of the attacks that the parent has
        public List<Attack> parent_AttackList { get; set;}
        public InputStorage(List<Attack> in_parent_AttackList)
        {
            CF_Inputs = new List<InputOnFrame>();
            parent_AttackList = in_parent_AttackList;
        }

        public InputStorage()
        {
            CF_Inputs = new List<InputOnFrame>();
        }

        //Adds an input the storage if either the Direction or Action input change
        public void Update(InputOnFrame input)
        {
            if(CF_Inputs.Count == 0)
            {
                CF_Inputs.Add(input);
                Debug.Log("Added input");
            }
            else if(input.Direction != CF_Inputs.First().Direction ||
                input.Actions != CF_Inputs.First().Actions)
            {
                CF_Inputs.Insert(0, input);
                string inputs = string.Join(",", input.Actions);
                Debug.Log("Added input: " + input.Direction + " " + inputs);
            }
            
        }

        public Attack GetBestAttack(Character character)
        {
            List<Attack> attackList = new List<Attack>();
            foreach(Attack attack in GetAttacksWithAction(CF_Inputs[0].Actions).Where(x => CheckForAttackAllowed(x, character)))
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

        public List<Attack> GetAttacksWithAction(EnableInputsOnFrame input)
        {
            List<Attack> returnAttacks = new List<Attack>();
            foreach (Attack attack in parent_AttackList.Where(x => input.Has(x.activationInput)))
            {
                returnAttacks.Add(attack);
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
                    foreach (InputOnFrame i in GetInputsInFrameWindow(special))
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
        public List<InputOnFrame> GetInputsInFrameWindow(Attack specialAttack)
        {
            List<InputOnFrame> inputs = new List<InputOnFrame>();
            int currentFrame = Time.frameCount;
            foreach (InputOnFrame i in CF_Inputs.GetRange(0, specialAttack.InputWindow))
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
