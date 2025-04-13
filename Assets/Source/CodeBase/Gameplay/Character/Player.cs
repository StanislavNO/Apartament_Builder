using Source.CodeBase.Gameplay.Builds;
using UnityEngine;

namespace Source.CodeBase.Gameplay.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Transform Transform { get; private set; }
        [field: SerializeField] public Transform Camera { get; private set; }
        [field: SerializeField] public BuildPrototype BuildingPoint { get; private set; }
        [field: SerializeField] public Transform BuildingStartPoint { get; private set; }
        [field: SerializeField] public MovementData Data { get; private set; }
        
    }
}