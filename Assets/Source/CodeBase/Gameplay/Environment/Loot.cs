using UnityEngine;

namespace Source.CodeBase.Gameplay.Environment
{
    public class Loot : MonoBehaviour
    {
        [field:SerializeField] public Transform Transform {get; private set;}
        [field: SerializeField] public Rigidbody Rigidbody {get; private set;}
    }
}