using System;
using Source.CodeBase.Gameplay.Environment;
using Source.CodeBase.Infrastructure.Services.InputService;
using UnityEngine;
using Zenject;

namespace Source.CodeBase.Gameplay.Character
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private Transform _lootPoint;
        [SerializeField] private float _maxDistance = 5f;
        
        private Camera _camera;
        private IInputService _inputService;
        private Loot _loot;

        private bool _isLooting;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _camera = Camera.main;
            _inputService.OnClicked += OnPlayerClicked;
        }

        private void OnDestroy()
        {
            _inputService.OnClicked -= OnPlayerClicked;
        }

        private void OnPlayerClicked()
        {
            if (_isLooting)
                Discard();
            else
                PickUp();
        }

        private void PickUp()
        {
            Vector3 centerScreenPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Ray ray = _camera.ScreenPointToRay(centerScreenPosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance))
            {
                if (hit.collider.TryGetComponent(out Loot loot))
                {
                    loot.Rigidbody.isKinematic = true;
                    loot.Transform.position = _lootPoint.position;
                    loot.Transform.SetParent(_lootPoint);
                    _loot = loot;
                    _isLooting = true;
                }
            }
        }

        private void Discard()
        {
            _loot.Rigidbody.isKinematic = false;
            _loot.Transform.SetParent(null);
            _loot = null;
            _isLooting = false;
        }
    }
}