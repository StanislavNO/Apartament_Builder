using UnityEngine;

namespace Source.CodeBase.Gameplay.Builds
{
    public class BuildItem : MonoBehaviour , IBuilding
    {
        [field: SerializeField] public LayerMask BuildLayer { get; private set; }
        [field: SerializeField] public Mesh Mesh { get; private set; }
        
        public Transform Transform { get; private set; }

        private void Awake()
        {
            Transform = transform;
        }

        public void Sleep() => gameObject.SetActive(false);

        public void Build(Vector3 position, Quaternion rotation)
        {
            gameObject.SetActive(true);
            Transform.SetPositionAndRotation(position, rotation);
        }
    }
}