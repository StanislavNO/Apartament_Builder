using UnityEngine;

namespace Source.CodeBase.Infrastructure.Services.InputService
{
    public interface IInputService
    {
        Vector3 MoveAxis { get; }
        float VerticalAxis { get;}
        float HorizontalAxis { get;}
        Vector2 MouseAxis { get;}
    }
}