using System;
using UnityEngine;

namespace Source.CodeBase.Infrastructure.Services.InputService
{
    public interface IInputService
    {
        event Action OnClicked;
        event Action<float> OnScrolled;
        
        float VerticalAxis { get;}
        float HorizontalAxis { get;}
        Vector2 RotateAxis { get;}
    }
}