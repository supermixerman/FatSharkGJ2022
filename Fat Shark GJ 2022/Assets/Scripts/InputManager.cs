using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private BallControl _ball;
    [SerializeField] private float _maxStrikeCharge;

    private Vector2 _mouseUpPosition;
    private Vector2 _mouseDownPosition;
    private bool _mouseDown;
    private bool _didAction;

    public static InputManager inputManager;

    void Awake()
    {
        if (inputManager == null)
        {
            inputManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.gameManager.victory) return;
        
        if (_mouseDown)
        {
            _ball.DrawStrikeIndicator(_mouseDownPosition, Mouse.current.position.ReadValue());
        }
        else
        {
            _ball.HideStrikeIndicator();
        }
    }

    public void LeftMouse(InputAction.CallbackContext context)
    {
        if (GameManager.gameManager.victory) return;
        if (_didAction) return;
 
        if (context.performed)
        {
            _mouseDownPosition = Mouse.current.position.ReadValue();
            _mouseDown = true;
        }
        else if (context.canceled)
        {
            _mouseUpPosition = Mouse.current.position.ReadValue();
            _mouseDown = false;

            _ball.Strike(CalculateStrike(_mouseDownPosition, _mouseUpPosition));
            _didAction = true;
        }
    }

    private Vector2 CalculateStrike(Vector2 hitPosition, Vector2 releasePosition)
    {
        Vector2 strike = hitPosition - releasePosition;
        if (strike.magnitude > _maxStrikeCharge)
        {
            strike = strike.normalized * _maxStrikeCharge;
        }
        return strike;
    }

    public void SetActiveBall(BallControl newBall){
        _ball = newBall;
        _didAction = false;
    }

    //public void ChangeBall()
    //{
    //    GameManager.gameManager.NextTurn();
    //    SetActiveBall();
    //}
}
