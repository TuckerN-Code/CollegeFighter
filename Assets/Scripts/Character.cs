using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Character 
    {
        public GameObject self_Object { get; set; }
        public List<SpecialAttack> attack_list = new List<SpecialAttack>() { };
        public InputStorage InputStorage { get; set; }
        public int max_health { get; set; }

        public int current_health { get; set; }

        public int max_stun { get; set; }

        public int current_stun { get; set ; }
        public int Meter { get; set; }
        public bool Grounded { get; set; }
        public bool Airborn { get; set; }
        public bool CounterHit { get; set ; }
        public bool Actionable { get; set; }

        public abstract void Attack();

        public abstract void Block();

        public abstract void Hit();

        public abstract void Jump();

        public abstract void Move();
    }



    public class Programmer : Character
    {
        private class SA_Bug : SpecialAttack
        {
            public SA_Bug()
            {
                Priority = 3;
                InputWindow = 10;
                AttackConditions = new AttackConditions(true, false, 0);
                activationInput = CF_Action_Inputs.Light_Button;
                AllowedInputs = new List<List<CF_Direction_Inputs>>
                        { new List<CF_Direction_Inputs>
                        {
                             CF_Direction_Inputs.Down_Direction,
                             CF_Direction_Inputs.DownLeft_Direction,
                             CF_Direction_Inputs.Left_Direction
                        }, new List<CF_Direction_Inputs>
                        {
                            CF_Direction_Inputs.Down_Direction,
                            CF_Direction_Inputs.Left_Direction
                        }
                    };

            }
        }

        Programmer()
        {
            init();
        }

        private void init()
        {
            max_health = 1000;
            current_health = max_health;
            max_stun = 800;
            current_stun = max_stun;
            Meter = 0;

            attack_list.Add(new SA_Bug());
        }

        public override void Attack()
        {

        }

        public override void Block()
        {
            throw new NotImplementedException();
        }

        public override void Hit()
        {
            throw new NotImplementedException();
        }

        public override void Jump()
        {
            throw new NotImplementedException();
        }

        public override void Move()
        {
            throw new NotImplementedException();
        }
    }

}
