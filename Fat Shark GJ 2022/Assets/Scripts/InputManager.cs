using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private BallControl _ball;

    private Vector2 _mouseUpPosition;
    private Vector2 _mouseDownPosition;

    public void LeftMouse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _mouseDownPosition = Mouse.current.position.ReadValue();
            Debug.Log($"Mouse was pressed down at: {_mouseDownPosition}");
        }
        else if (context.canceled)
        {
            _mouseUpPosition = Mouse.current.position.ReadValue();
            Debug.Log($"Mouse was released at: {_mouseUpPosition}");

            _ball.Strike(CalculateStroke(_mouseDownPosition, _mouseUpPosition));
        }
    }

    private Vector2 CalculateStroke(Vector2 hitPosition, Vector2 releasePosition)
    {
        Vector2 strike = hitPosition - releasePosition;
        return strike;
    }
}
