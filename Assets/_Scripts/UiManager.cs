using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // UiManager

    public TextMeshProUGUI scoreCard;

    public static UiManager instance;

    [SerializeField] Canvas _gameOverMenu;

   

    private void Awake()
    {
        instance = this;
    }

  

    public void OpenGameOver()
    {
        _gameOverMenu.enabled = true;
    }
}
