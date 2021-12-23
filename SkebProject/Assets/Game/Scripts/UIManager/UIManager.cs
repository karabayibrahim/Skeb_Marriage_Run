using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Image Bar;
    public Text StatusText;
    [Header("StartPanel")]
    public GameObject StartPanel;
    public Button StartPlayButton;
    [Header("InGamePanel")]
    public GameObject InGamePanel;
    public Text LevelText;
    public Button RestartButton;


    void Start()
    {
        StartPlayButton.onClick.AddListener(StartStatus);
        var index=GameManager.Instance.LevelIndex+1;
        LevelText.text = index.ToString();
    }
    private void OnDisable()
    {
        StartPlayButton.onClick.RemoveListener(StartStatus);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StatusTextAdjust(string _text,Color _color)
    {
        StatusText.text = _text;
        StatusText.color = _color;
    }

    private void StartStatus()
    {
        StartPanel.SetActive(false);
        InGamePanel.SetActive(true);
    }
}
