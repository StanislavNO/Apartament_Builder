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

        public Vector3 MoveAxis => new(HorizontalAxis, 0, VerticalAxis);
        public float VerticalAxis { get; private set; }
        public float HorizontalAxis { get; private set; }
        public Vector2 MouseAxis { get; private set; }

        public void Tick() => UpdateAxis();

        private void UpdateAxis()
        {
            VerticalAxis = Input.GetAxis(VERTICAL_AXIS);
            HorizontalAxis = Input.GetAxis(HORIZONTAL_AXIS);

            MouseAxis = new(
                Input.GetAxis(MOUSE_HORIZONTAL_AXIS),
                Input.GetAxis(MOUSE_VERTICAL_AXIS));
        }
    }
}