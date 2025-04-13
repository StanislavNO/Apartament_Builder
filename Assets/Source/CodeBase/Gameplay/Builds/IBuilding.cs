using UnityEngine;

namespace Source.CodeBase.Gameplay.Builds
{
    public interface IBuilding
    {
        public LayerMask BuildLayer { get; }
        public Mesh Mesh { get; }
        public Transform Transform { get; }

        void Sleep();
        void Build(Vector3 position, Quaternion rotation);
    }
}