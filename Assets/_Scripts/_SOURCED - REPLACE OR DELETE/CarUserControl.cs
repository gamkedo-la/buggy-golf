using System;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
		public string horizontalButton = "Horizontal";
		public string verticalButton = "Vertical";
		public string handbrakeButton = "Handbrake";

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            //float h = CrossPlatformInputManager.GetAxis(horizontalButton);
            float h = Input.GetAxis(horizontalButton);
            //float v = CrossPlatformInputManager.GetAxis(verticalButton);
            float v = Input.GetAxis(verticalButton);
#if !MOBILE_INPUT
            float handbrake = Input.GetAxis(handbrakeButton);
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}