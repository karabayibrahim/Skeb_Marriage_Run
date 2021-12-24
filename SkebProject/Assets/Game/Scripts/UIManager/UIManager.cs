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
    [Header("FinishPanel")]
    public GameObject FinishPanel;
    public GameObject FailPanel;
    public GameObject CompletePanel;
    public Button NextButton;





    void Start()
    {
        StartPlayButton.onClick.AddListener(StartStatus);
        NextButton.onClick.AddListener(RestartAdjust);
        GameManager.FinishEvent += FinishStatus;
        var index=GameManager.Instance.LevelIndex+1;
        LevelText.text = index.ToString();
    }
    private void OnDisable()
    {
        StartPlayButton.onClick.RemoveListener(StartStatus);
        NextButton.onClick.RemoveListener(RestartAdjust);
        GameManager.FinishEvent -= FinishStatus;
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

    private void FinishStatus()
    {
        StartCoroutine(FinishTimer());
    }

    private IEnumerator FinishTimer()
    {
        yield return new WaitForSeconds(5f);
        FinishPanel.SetActive(true);
        switch (GameManager.Instance.Player.RelationStatus)
        {
            case RelationStatus.TERRIBLE:
                FailPanel.SetActive(true);
                CompletePanel.SetActive(false);
                break;
            case RelationStatus.BAD:
                FailPanel.SetActive(true);
                CompletePanel.SetActive(false);
                break;
            case RelationStatus.NORMAL:
                FailPanel.SetActive(false);
                CompletePanel.SetActive(true);
                break;
            case RelationStatus.GOOD:
                FailPanel.SetActive(false);
                CompletePanel.SetActive(true);
                break;
            case RelationStatus.EXCELLENT:
                FailPanel.SetActive(false);
                CompletePanel.SetActive(true);
                break;
            default:
                break;
        }
        yield break;
    }

    private void RestartAdjust()
    {
        //GameManager.Instance.LevelIndex++;
        Application.LoadLevel(Application.loadedLevel);
    }
}
