using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls _playerInput;
    private Vector3 _defaultChildPosition = new Vector3(0f, 0.5f, 0f);
    private Vector2 _moveInput;
    private Vector3 _moveDirection;
    [SerializeField] private Transform _pivotPoint;
    private Vector3 _pivotAxis;
    [SerializeField] private GameObject _dice;
    [SerializeField] private float _rollTime = 2f;

    private void Awake()
    {
        _playerInput = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerInput.Enable();

        _playerInput.CubeControl.Move.started += OnMoveInput;
    }
 
    private void OnDisable()
    {
        _playerInput.Disable();

        _playerInput.CubeControl.Move.started -= OnMoveInput;
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
        // If tile exists in map at coordinates, move is allowed
        Vector2 targetCoordinates = (transform.position + move).GetCoordinateFromPosition();
        return GameManager.Instance.CurrentLevel.IsTileAtCoordinates(targetCoordinates);
    }

    private void Move(Vector3 move)
    {
        _pivotPoint.localPosition = move * 0.5f;
        _pivotAxis = move.z != 0 ? Vector3.right : Vector3.forward;

        StartCoroutine("RollToNextSide");


        //_pivotPoint.localRotation = Quaternion.LookRotation(move, Vector3.up);
        //float targetAngle = (_moveInput.x > 0 || _moveInput.y < 0) ? -90f : 90f;
        //_dice.transform.RotateAround(_pivotPoint, _pivotAxis, targetAngle);

        
    }

    private void MovePlayerAndResetDicePosition()
    {
        transform.position += _moveDirection.normalized;
        _pivotPoint.localPosition = _defaultChildPosition;
        _dice.transform.localPosition = _defaultChildPosition;
    }

    
    IEnumerator RollToNextSide()
    {
        _playerInput.Disable();

        float multiplier = (_moveInput.x > 0 || _moveInput.y < 0) ? -1f : 1f;

        float angleProgress = 0f;
        float time = 0f;
        while( time < _rollTime)
        {
            float targetRotation = Mathf.Lerp(0f, 90f * multiplier, time / _rollTime);
            float thisRotation = targetRotation - angleProgress;
            _dice.transform.RotateAround(_pivotPoint.position, _pivotAxis, thisRotation);

            angleProgress = targetRotation;

            yield return null;

            time += Time.deltaTime;
        }

        float finalRotation = 90f * multiplier - angleProgress;
        _dice.transform.RotateAround(_pivotPoint.position, _pivotAxis, finalRotation);
        MovePlayerAndResetDicePosition();
        _playerInput.Enable();
        yield return null;
    }

}
