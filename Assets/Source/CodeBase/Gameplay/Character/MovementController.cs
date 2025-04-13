using Source.CodeBase.Infrastructure.Services.InputService;
using UnityEngine;
using Zenject;

namespace Source.CodeBase.Gameplay.Character
{
    public class MovementController : ITickable
    {
        private readonly CharacterController _playerController;
        private readonly Transform _playerCamera;
        private readonly Transform _playerTransform;
        private readonly MovementData _movementData;
        private readonly IInputService _input;

        private float _cameraAngle;

        private Vector3 Forward => Vector3.ProjectOnPlane(_playerCamera.forward, Vector3.up).normalized;
        private Vector3 Right => Vector3.ProjectOnPlane(_playerCamera.right, Vector3.up).normalized;

        private Vector3 PlayerSpeed =>
            (Forward * _input.VerticalAxis * _movementData.MoveSpeed +
             Right * _input.HorizontalAxis * _movementData.StrafeSpeed) *
            Time.deltaTime;

        public MovementController(Player player, IInputService input)
        {
            _playerController = player.CharacterController;
            _input = input;
            _movementData = player.Data;
            _playerCamera = player.Camera;
            _playerTransform = player.Transform;

            _cameraAngle = _playerCamera.localEulerAngles.x;
        }

        public void Tick()
        {
            Move();
            Rotate();
        }

        private void Rotate()
        {
            _cameraAngle -= _input.RotateAxis.y * _movementData.VerticalTurnSensitivity;
            _cameraAngle = Mathf.Clamp(_cameraAngle, _movementData.VerticalMinAngle, _movementData.VerticalMaxAngle);

            _playerCamera.localEulerAngles = Vector3.right * _cameraAngle;
            _playerTransform.Rotate(Vector3.up * (_movementData.HorizontalTurnSensitivity * _input.RotateAxis.x));
        }

        private void Move()
        {
            if (_playerController.isGrounded)
                _playerController.Move(PlayerSpeed + Vector3.down);
            else
                _playerController.Move(_playerController.velocity + Physics.gravity * Time.deltaTime);
        }
    }
}