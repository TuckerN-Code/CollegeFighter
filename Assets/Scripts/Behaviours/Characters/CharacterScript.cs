using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Characters
{
    public class CharacterScript : MonoBehaviour
    {
        Animator animator;
        public PlayerInput playerInput;
        InputStorage storage;
        private EnableInputsOnFrame enableInputs;

        private string currentControlScheme;
        private string actionMapPlayerControls = "Player Controls";
        private string actionMapMenuControls = "Menu Controls";
        
        public Character character { get; set; }
        // Start is called before the first frame update
        void Start()
        {
            currentControlScheme = playerInput.currentControlScheme;
            enableInputs = new EnableInputsOnFrame();
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
            storage.Update(new InputOnFrame(CF_Direction_Inputs.None, enableInputs, Time.frameCount));
        }

        public void OnLight(InputAction.CallbackContext value)
        {
            if (value.started)
                enableInputs.Light_Button_Pressed = true;
        }

        public void OnHeavy(InputAction.CallbackContext value)
            {
            if (value.started)
                enableInputs.Heavy_Button_Pressed = true;
        }

        //disable normal controls when menu is open
        public void SetInputActiveState(bool gameIsPaused)
        {
            switch (gameIsPaused)
            {
                case true:
                    playerInput.DeactivateInput();
                    break;
                case false:
                    playerInput.ActivateInput();
                    break;
            }
        }

        public void EnableGameplayControls()
        {
            playerInput.SwitchCurrentActionMap(actionMapPlayerControls);
        }

        public void EnablePauseMenuControls()
        {
           playerInput.SwitchCurrentActionMap(actionMapMenuControls);
        }
    }
}
