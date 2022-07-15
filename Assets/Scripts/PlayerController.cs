using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls _playerInput;
    private Vector3 _defaultChildPosition = new Vector3(0f, 0.5f, 0f);
    [SerializeField] private Vector2 _moveInput;
    [SerializeField] private Vector3 _moveDirection;
    [SerializeField] private Transform _pivotPoint;
    [SerializeField] private GameObject _dice;
    [SerializeField] private Vector3 _pivotAxis;

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
            Move(move);
    }

    private bool IsMoveAllowed(Vector3 move)
    {
        return true;
    }

    private void Move(Vector3 move)
    {
        _pivotPoint.localPosition = move * 0.5f;
        _pivotPoint.localRotation = Quaternion.LookRotation(move, Vector3.up);
        _pivotAxis = move.z != 0 ? Vector3.right : Vector3.forward;
        float multiplier = (_moveInput.x > 0 || _moveInput.y < 0) ? -1f : 1f;
        //transform.RotateAround(Vector3.zero, move, 90f);
        Debug.Log("pivot world: " + _pivotPoint.rotation.eulerAngles);
        _dice.transform.RotateAround(_pivotPoint.position, _pivotAxis, 90f * multiplier);

        MovePlayerAndResetDicePosition();
    }

    private void MovePlayerAndResetDicePosition()
    {
        transform.position += _moveDirection.normalized;
        _pivotPoint.localPosition = _defaultChildPosition;
        _dice.transform.localPosition = _defaultChildPosition;
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
