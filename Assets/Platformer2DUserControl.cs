using System;
using UnityEngine;

    [RequireComponent(typeof (PlayerMovement))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlayerMovement m_Character;
        private bool m_Jump;


        private void Awake()
        {
            m_Character = GetComponent<PlayerMovement>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = Input.GetButtonDown("Jump");
            }
        }
        private void FixedUpdate()
        {
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = Input.GetAxis("Horizontal");
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
