using System;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer.Unity;

namespace Services
{
    public class InputHandler : ITickable
    {
        public event Action LeftButtonPressed;
        public event Action RightButtonPressed;
        public event Action ForwardButtonPressed;
        public event Action BackButtonPressed;

        public void Tick()
        {
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                ForwardButtonPressed?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                BackButtonPressed?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                LeftButtonPressed?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                RightButtonPressed?.Invoke();
            }
        }
    }
}