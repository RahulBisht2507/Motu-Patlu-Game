using UnityEngine;




namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool Fire;
		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		 VariableJoystick Joystick;
		 FixedTouchField TouchField;
		
        private void Start()
        {
            Joystick = FindAnyObjectByType<VariableJoystick>();
			TouchField = FindObjectOfType<FixedTouchField>();
        }
        private void Update()
        {
			MoveInput(Joystick.Direction);
			LookInput(TouchField.Look);
        }

#if ENABLE_INPUT_SYSTEM
		/*public void OnMove(InputValue value)
		{
			MoveInput(Joystick.joystickPosition);
		}*/

		/*public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}*/

		/*public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}*/
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}


        public void FireInput(bool newFireState)
        {
            Fire = newFireState;
        }

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.None : CursorLockMode.None;
		}
	}
	
}