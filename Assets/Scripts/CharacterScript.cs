using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Assets.Scripts;

namespace Assets.Scripts
{
    public class CharacterScript : MonoBehaviour
    {
        Animator animator;
        public PlayerInput playerInput;
        InputStorage storage;

        private string currentControlScheme;
        
        public Character character { get; set; }
        // Start is called before the first frame update
        void Start()
        {
            currentControlScheme = playerInput.currentControlScheme;
            storage = new InputStorage();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            InputHandler();
            //Update code framework not complete
        }

        void InputHandler()
        {
            List<CF_Action_Inputs> action_Inputs = new List<CF_Action_Inputs>();
            public void OnLight(InputAction. CallbackContext value)
            {
                if (value.started)
                    action_Inputs.Add(CF_Action_Inputs.Heavy_Button);
            }
            if (true)
            {
                action_Inputs.Add(CF_Action_Inputs.Light_Button);
            }
            else if (true)
            {
                action_Inputs.Add(CF_Action_Inputs.None);
            }

            storage.Update(new Input(CF_Direction_Inputs.None, action_Inputs, Time.frameCount));
        }    
    }
}
