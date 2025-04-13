using System;
using UnityEngine;
using Zenject;

namespace Source.CodeBase.Infrastructure.Services.InputService
{
    public class StandaloneInput : IInputService, ITickable
    {
        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";
        private const string MOUSE_VERTICAL_AXIS = "Mouse Y";
        private const string MOUSE_HORIZONTAL_AXIS = "Mouse X";
        private const string MOUSE_SCROLL_AXIS = "Mouse ScrollWheel";

        public event Action OnClicked;
        public event Action<float> OnScrolled;

        public float VerticalAxis { get; private set; }
        public float HorizontalAxis { get; private set; }
        public Vector2 RotateAxis { get; private set; }

        public void Tick()
        {
            UpdateAxis();
            ReadMouseClick();
            ReadMouseScroll();
        }

        private void ReadMouseScroll()
        {
            float scrollDelta = Input.GetAxis(MOUSE_SCROLL_AXIS);

            if (scrollDelta != 0)
                OnScrolled?.Invoke(scrollDelta);
        }

        private void ReadMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
                OnClicked?.Invoke();
        }

        private void UpdateAxis()
        {
            VerticalAxis = Input.GetAxis(VERTICAL_AXIS);
            HorizontalAxis = Input.GetAxis(HORIZONTAL_AXIS);

            RotateAxis = new(
                Input.GetAxis(MOUSE_HORIZONTAL_AXIS),
                Input.GetAxis(MOUSE_VERTICAL_AXIS));
        }
    }
}