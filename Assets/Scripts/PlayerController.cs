using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerInput.Enable();

        _playerInput.CubeControl.Move.started += OnMove;
        //_playerInput.CubeControl.MoveNorth.started += OnMoveNorth;
        //_playerInput.CubeControl.MoveSouth.started += OnMoveSouth;
        //_playerInput.CubeControl.MoveEast.started += OnMoveEast;
        //_playerInput.CubeControl.MoveWest.started += OnMoveWest;
    }
 
    private void OnDisable()
    {
        _playerInput.Disable();

        _playerInput.CubeControl.Move.started -= OnMove;
        //_playerInput.CubeControl.MoveNorth.started -= OnMoveNorth;
        //_playerInput.CubeControl.MoveSouth.started -= OnMoveSouth;
        //_playerInput.CubeControl.MoveEast.started -= OnMoveEast;
        //_playerInput.CubeControl.MoveWest.started -= OnMoveWest;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (InputIsUnambiguous(moveInput))
        {
            Debug.Log("applied movement");
        }
    }

    private bool InputIsUnambiguous(Vector2 moveInput)
    {
        //Debug.Log("MoveInput: " + moveInput);
        //Debug.Log("MoveInput magnitude: " + moveInput.magnitude);
        return moveInput.magnitude == 1f;
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
