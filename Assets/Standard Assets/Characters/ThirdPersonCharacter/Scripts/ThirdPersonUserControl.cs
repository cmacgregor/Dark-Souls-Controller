using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
		private Vector3 m_Move;					  // the world-relative desired move direction, calculated from the camForward and user input.
        private bool m_Roll;                      
		private bool m_Attack;        
		private bool m_Sprint;

		private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            //get the the character
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
			//probably get roll here maybe?
		
			/*
            if (!m_Jump)
            {
            	m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
            */
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

			//Roll if space is pressed 
			m_Roll = Input.GetKey (KeyCode.Space);

			//check for attack 
			m_Attack = Input.GetMouseButton(0);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }

			if (m_Attack) {
				m_Character.Attack (m_Attack);
			} else {
				//Sprint toggle
				if (Input.GetKey (KeyCode.LeftShift)) {
					m_Sprint = true;
				}
				// pass all parameters to the character control script
				m_Character.Move (m_Move, crouch, m_Roll, m_Sprint);


			}
        }
    }
}
