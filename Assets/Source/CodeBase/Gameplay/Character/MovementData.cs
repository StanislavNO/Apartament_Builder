using System;
using UnityEngine;

namespace Source.CodeBase.Gameplay.Character
{
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