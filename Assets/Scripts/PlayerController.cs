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
    private float _rollTime = 0.2f;
    private float _randomRollTime = 0.7f;
    private float _randomRollHeight = 1f;
    private float _randomRollLiftTime = 0.5f;
    private float _randomRollLowerTime = 0.3f;
    private float _randomRollPauseTime = 0.1f;
    //private Vector3 _peekOffset = new Vector3(47.9f, 7.8f, 0f);


    private Dictionary<int, Vector3> _vectorToLandOnDiceSide = new Dictionary<int, Vector3>()
    {
        {1, Vector3.forward * 90f},
        {2, Vector3.forward * 180f},
        {3, Vector3.left * 90f},
        {4, Vector3.right * 90f},
        {5, Vector3.forward * 360f},
        {6, Vector3.back * 90f},

    };

    private void Awake()
    {
        _playerInput = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerInput.Enable();

        _playerInput.CubeControl.Move.started += OnMoveInput;
        _playerInput.CubeControl.Roll.started += OnRollInput;
    }
 
    private void OnDisable()
    {
        _playerInput.Disable();

        _playerInput.CubeControl.Roll.started -= OnRollInput;
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

    private void OnRollInput(InputAction.CallbackContext context)
    {
        int sideToLandOn = UnityEngine.Random.Range(1, 7);
        Vector3 yRotation = Vector3.up * (90f * UnityEngine.Random.Range(0, 4));
        Vector3 targetRotation = _vectorToLandOnDiceSide[sideToLandOn] + yRotation;

        StartCoroutine("Lift");
        StartCoroutine("RandomRollSpin", targetRotation);
    }

    IEnumerator RandomRollSpin(Vector3 targetRotation)
    {
        Vector3 startingRotation = _dice.transform.rotation.eulerAngles;
        Vector3 fullRotation;
        fullRotation.x = targetRotation.x != 0 ? targetRotation.x + UnityEngine.Random.Range(3, 5) * 360f : 0f;
        fullRotation.y = targetRotation.y + 360f;
        fullRotation.z = targetRotation.z != 0 ? targetRotation.z + UnityEngine.Random.Range(3, 5) * 360f : 0f;

        float time = 0f;
        while (time < _randomRollTime)
        {
            float progress = time / _randomRollTime;
            _dice.transform.localEulerAngles = Vector3.Lerp(startingRotation, fullRotation, Mathf.SmoothStep(0f, 1f, progress));

            yield return null;
            time += Time.deltaTime;
        }
        _dice.transform.localEulerAngles = fullRotation;
        yield return new WaitForSeconds(_randomRollPauseTime);

        StartCoroutine("Lower");
        yield return null;
    }

    IEnumerator Lift()
    {
        _playerInput.Disable();

        Vector3 startPos = _dice.transform.localPosition;
        Vector3 targetPos = startPos + Vector3.up * _randomRollHeight;
        float time = 0f;
        while (time < _randomRollLiftTime)
        {
            float progress = time / _randomRollLiftTime;
            _dice.transform.localPosition = Vector3.Lerp(startPos, targetPos, Mathf.SmoothStep(0f, 1f, progress));

            yield return null;
            time += Time.deltaTime;
        }
        _dice.transform.localPosition = targetPos;
        yield return null;
    }

    IEnumerator Lower()
    {
        Vector3 startPos = _dice.transform.localPosition;
        Vector3 targetPos = _defaultChildPosition;
        float time = 0f;
        while (time < _randomRollLowerTime)
        {
            float progress = time / _randomRollLowerTime;
            _dice.transform.localPosition = Vector3.Lerp(startPos, targetPos, Mathf.SmoothStep(0f, 1f, progress));

            yield return null;
            time += Time.deltaTime;
        }
        _dice.transform.localPosition = targetPos;

        _playerInput.Enable();
        yield return null;

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
