using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;

namespace Assets.Scripts
{
   public class Character
    {
        public GameObject self_Object { get; set; }
        public int max_health { get; }
        public int current_health { get; set; }
        public int max_stun { get; }
        public int current_stun { get; set; }

        public bool Airborn { get; set; }
        public bool CounterHit { get; set; }
        public bool Actionable { get; set; }

        public void Hit()
        {

        }
        public void Block()
        {

        }
        public void Attack()
        {

        }
        public void Move()
        {

        }
        public void Jump()
        {

        }

        Dictionary<int, Attack> AttackLibrary { get; set; }
    }
}
