using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelText : MonoBehaviour
{
    private TMP_Text _levelText;
    private int _levelNum;

    private void Awake()
    {
        _levelText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        _levelNum = SceneManager.GetActiveScene().buildIndex;
        _levelText.text = "Level " + _levelNum;
    }

}
