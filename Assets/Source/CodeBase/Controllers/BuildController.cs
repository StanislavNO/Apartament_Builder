using System;
using Source.CodeBase.Configs;
using Source.CodeBase.Gameplay.Builds;
using Source.CodeBase.Gameplay.Character;
using Source.CodeBase.Infrastructure.Services.InputService;
using UnityEngine;
using Zenject;

namespace Source.CodeBase.Controllers
{
    public class BuildController : IInitializable, IDisposable, IFixedTickable
    {
        private readonly IInputService _input;
        private readonly Camera _camera;
        private readonly BuildPrototype _prototype;
        private readonly Transform _prototypeStartPoint;

        private readonly float _maxDistance;
        private readonly float _rotationStep;
        private readonly float _detectionRadius;

        private IBuilding _currentBuilding;
        private bool _isActive;

        public BuildController(IInputService input, Player player, BuildConfig config)
        {
            _maxDistance = config.MaxDistanceRay;
            _rotationStep = config.RotationStep;
            _detectionRadius = config.DetectionRadiusRay;

            _input = input;
            _camera = Camera.main;
            _prototype = player.BuildingPoint;
            _prototypeStartPoint = player.BuildingStartPoint;
        }

        public void Initialize()
        {
            _input.OnClicked += OnPlayerClicked;
            _input.OnScrolled += OnPlayerScrolled;
        }

        public void Dispose()
        {
            _input.OnClicked -= OnPlayerClicked;
            _input.OnScrolled -= OnPlayerScrolled;
        }

        public void FixedTick()
        {
            if (_isActive == false)
                return;

            MagnetizeToNearestSurface();
        }

        private void MagnetizeToNearestSurface()
        {
            Vector3 origin = _prototypeStartPoint.position;

            Collider[] colliders = Physics.OverlapSphere(origin, _detectionRadius, _currentBuilding.BuildLayer);

            if (colliders.Length == 0)
            {
                _prototype.Transform.position = _prototypeStartPoint.position;
                _prototype.IsActive = false;
                return;
            }

            float closestDistance = float.MaxValue;
            RaycastHit closestHit = default;
            bool foundHit = false;

            foreach (var collider in colliders)
            {
                Vector3 direction = (collider.bounds.center - origin).normalized;

                if (Physics.Raycast(origin, direction, out RaycastHit hit, _detectionRadius,
                        _currentBuilding.BuildLayer))
                {
                    float distance = Vector3.Distance(origin, hit.point);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestHit = hit;
                        foundHit = true;
                    }
                }
            }

            if (foundHit)
                _prototype.Transform.position = closestHit.point;

            _prototype.IsActive = true;
        }

        private void OnPlayerScrolled(float delta)
        {
            if (_isActive != true)
                return;

            if (delta > 0f)
                _prototype.SetRotationOffset(_rotationStep);
            else if (delta < 0f)
                _prototype.SetRotationOffset(-_rotationStep);
        }

        private void OnPlayerClicked()
        {
            if (_isActive)
                Build();
            else
                SelectBuilding();
        }

        private void SelectBuilding()
        {
            Vector3 centerScreenPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Ray ray = _camera.ScreenPointToRay(centerScreenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance))
            {
                if (hit.collider.TryGetComponent(out BuildItem building))
                {
                    _prototype.SetView(building.Mesh, building.BuildLayer, building.Transform.rotation);
                    _currentBuilding = null;
                    _currentBuilding = building;
                    _isActive = true;
                    _currentBuilding.Sleep();
                }
            }
        }

        private void Build()
        {
            if (_prototype.CanBuild == false)
                return;

            _isActive = false;
            _currentBuilding.Build(_prototype.Transform.position, _prototype.Transform.rotation);
            _prototype.Clear();
            _currentBuilding = null;
        }
    }
}