using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _clack;
    [SerializeField] private AudioClip _roll;
    [SerializeField] private AudioClip _levelClear;
    [SerializeField] private AudioClip _buzz;
    [SerializeField] private AudioClip _jump;
    [SerializeField] private AudioClip _flip;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventManager.Instance.DiceClacked += OnDiceClacked;
        EventManager.Instance.LevelCleared += OnLevelCleared;
        EventManager.Instance.BadTileStep += OnBadTileStep;
        EventManager.Instance.Jumped += OnJumped;
        EventManager.Instance.RandomRolled += OnRandomRolled;
        //EventManager.Instance.Flipped += OnFlipped;
    }

    private void OnDisable()
    {
        EventManager.Instance.DiceClacked -= OnDiceClacked;
        EventManager.Instance.LevelCleared -= OnLevelCleared;
        EventManager.Instance.BadTileStep -= OnBadTileStep;
        EventManager.Instance.Jumped -= OnJumped;
        EventManager.Instance.RandomRolled -= OnRandomRolled;
        //EventManager.Instance.Flipped -= OnFlipped;
    }

    private void OnDiceClacked()
    {
        _audioSource.PlayOneShot(_clack);
    }

    private void OnLevelCleared()
    {
        _audioSource.PlayOneShot(_levelClear);
    }

    private void OnBadTileStep()
    {
        _audioSource.PlayOneShot(_buzz);
    }
    private void OnJumped()
    {
        _audioSource.PlayOneShot(_jump);
    }

    private void OnRandomRolled()
    {
        _audioSource.PlayOneShot(_roll);
    }

    //private void OnFlipped()
    //{
    //    _audioSource.PlayOneShot(_flip);
    //}
}
