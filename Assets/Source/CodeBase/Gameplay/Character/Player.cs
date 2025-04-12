using System;
using UnityEngine;

namespace Source.CodeBase.Gameplay.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Transform Transform { get; private set; }
        [field: SerializeField] public Transform Camera { get; private set; }
        [field: SerializeField] public MovementData Data { get; private set; }
    }

    [Serializable]
    public class MovementData
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float StrafeSpeed { get; private set; }
        
        [field: SerializeField] public float HorizontalTurnSensitivity { get; private set; }
        [field: SerializeField] public float VerticalTurnSensitivity { get; private set; }
        
        [field: SerializeField] public float VerticalMinAngle { get; private set; } = -89;
        [field: SerializeField] public float VerticalMaxAngle { get; private set; } = 89;
    }
}