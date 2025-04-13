using UnityEngine;

namespace Source.CodeBase.Configs
{
    [CreateAssetMenu(fileName = "BuildConfig", menuName = "BuildConfig")]
    public class BuildConfig : ScriptableObject
    {
        [field: SerializeField] public float MaxDistanceRay { get; private set; } = 10f;
        [field: SerializeField] public float RotationStep { get; private set; }  = 45f;
        [field: SerializeField] public float DetectionRadiusRay { get; private set; } = 4f; 
    }
}