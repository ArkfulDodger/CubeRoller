using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls _playerInput;
    private Vector2 _moveInput;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _playerInput = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerInput.Enable();

        _playerInput.CubeControl.Move.started += OnMoveInput;
        //_playerInput.CubeControl.MoveNorth.started += OnMoveNorth;
        //_playerInput.CubeControl.MoveSouth.started += OnMoveSouth;
        //_playerInput.CubeControl.MoveEast.started += OnMoveEast;
        //_playerInput.CubeControl.MoveWest.started += OnMoveWest;
    }
 
    private void OnDisable()
    {
        _playerInput.Disable();

        _playerInput.CubeControl.Move.started -= OnMoveInput;
        //_playerInput.CubeControl.MoveNorth.started -= OnMoveNorth;
        //_playerInput.CubeControl.MoveSouth.started -= OnMoveSouth;
        //_playerInput.CubeControl.MoveEast.started -= OnMoveEast;
        //_playerInput.CubeControl.MoveWest.started -= OnMoveWest;
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        // get the vector 2 of the move input
        _moveInput = context.ReadValue<Vector2>();

        if (InputIsUnambiguous(_moveInput))
        {
            _moveDirection.x = _moveInput.x;
            _moveDirection.y = 0;
            _moveDirection.z = _moveInput.y;

            AttemptMove(_moveDirection);
        }
    }

    private bool InputIsUnambiguous(Vector2 moveInput)
    {
        // return true if movement is North/South/East/West and not on a diagonal
        return moveInput.magnitude == 1f;
    }

    private void AttemptMove(Vector3 move)
    {
        if (IsMoveAllowed(move))
            transform.position += move;
    }

    private bool IsMoveAllowed(Vector3 move)
    {
        return true;
    }

    
    //private void OnMoveWest(InputAction.CallbackContext obj)
    //{
    //    throw new NotImplementedException();
    //}

    //private void OnMoveEast(InputAction.CallbackContext obj)
    //{
    //    throw new NotImplementedException();
    //}

    //private void OnMoveSouth(InputAction.CallbackContext obj)
    //{
    //    throw new NotImplementedException();
    //}

    //private void OnMoveNorth(InputAction.CallbackContext obj)
    //{
    //    throw new NotImplementedException();
    //}

}
