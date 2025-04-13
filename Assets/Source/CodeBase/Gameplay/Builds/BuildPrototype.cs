using UnityEngine;

namespace Source.CodeBase.Gameplay.Builds
{
    public class BuildPrototype : MonoBehaviour
    {
        [SerializeField] private MeshFilter _view;
        [SerializeField] private MeshCollider _collider;

        [SerializeField] private Material _material;
        [SerializeField] private Color _buildColor;
        [SerializeField] private Color _dontBuildColor;

        private LayerMask _ignoreLayer;
        private bool _isActive;
        private float _rotationOffset;

        [field: SerializeField] 
        public Transform Transform { get; private set; }
        public bool CanBuild { get; private set; }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (value == false)
                {
                    CanBuild = false;
                    _material.color = _dontBuildColor;
                }

                _isActive = value;
            }
        }

        private void Update()
        {
            if (IsActive == false)
                return;

            _material.color = CanBuild ? _buildColor : _dontBuildColor;
        }

        private void LateUpdate()
        {
            if (IsActive == false)
                return;

            Transform.rotation = Quaternion.Euler(
                0f, Transform.rotation.y + _rotationOffset, 0f);
        }

        private void FixedUpdate()
        {
            if (IsActive == false)
                return;

            CheckCollisionWithMesh();
        }

        public void SetRotationOffset(float degrees) => _rotationOffset += degrees;

        private void CheckCollisionWithMesh()
        {
            var size = _view.sharedMesh.bounds.extents;
            Collider[] hits = Physics.OverlapBox(
                _collider.bounds.center, size, Quaternion.identity);

            foreach (var hit in hits)
            {
                if (((1 << hit.gameObject.layer) & _ignoreLayer) != 0 
                    || hit.gameObject == gameObject)
                    continue;

                CanBuild = false;
                return;
            }

            CanBuild = true;
        }

        public void SetView(Mesh mesh, LayerMask ignoreLayer, Quaternion rotation)
        {
            _rotationOffset += rotation.eulerAngles.y;
            _ignoreLayer = ignoreLayer;
            _view.mesh = mesh;
            _collider.sharedMesh = mesh;
            IsActive = true;
            CanBuild = true;
        }

        public void Clear()
        {
            _rotationOffset = 0;
            _view.mesh = null;
            IsActive = false;
        }
    }
}