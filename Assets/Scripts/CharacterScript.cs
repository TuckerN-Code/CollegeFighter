using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

namespace Assets.Scripts
{
    public class CharacterScript : MonoBehaviour
    {
        Animator animator;
        public Character character { get; set; }
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Update code framework not complete
        }
    }
}
