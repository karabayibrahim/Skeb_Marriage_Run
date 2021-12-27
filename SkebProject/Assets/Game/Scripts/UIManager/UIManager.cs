using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public bool GameStart = false;
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
    public Button RetryartButton;





    void Start()
    {
        var index = PlayerPrefs.GetInt("Level") + 1;
        LevelText.text = "Level" + " " + index.ToString();
        StartPlayButton.onClick.AddListener(StartStatus);
        NextButton.onClick.AddListener(NextAdjust);
        RetryartButton.onClick.AddListener(RetryStatus);
        GameManager.FinishEvent += FinishStatus;

    }
    private void OnDisable()
    {
        StartPlayButton.onClick.RemoveListener(StartStatus);
        RetryartButton.onClick.RemoveListener(RetryStatus);
        NextButton.onClick.RemoveListener(NextAdjust);
        GameManager.FinishEvent -= FinishStatus;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StatusTextAdjust(string _text, Color _color)
    {
        StatusText.text = _text;
        StatusText.color = _color;
    }

    private void StartStatus()
    {
        GameStart = true;
        StartPanel.SetActive(false);
        InGamePanel.SetActive(true);
    }

    private void FinishStatus()
    {
        StartCoroutine(FinishTimer());
    }

    private IEnumerator FinishTimer()
    {
        yield return new WaitForSeconds(2f);
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

    private void RetryStatus()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    private void NextAdjust()
    {
        GameManager.ParticleDestroyEvent?.Invoke(gameObject);
        //if (GameManager.Instance.LevelIndex == 2)
        //{
        //    GameManager.Instance.LevelIndex = 0;
        //}
        //else
        //{
        //}
        GameManager.Instance.LevelIndex++;

        GameManager.Instance.Player.enabled = true;

       
        FinishPanel.SetActive(false);
        PlayerPrefs.SetInt("Level", GameManager.Instance.LevelIndex);
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"), LoadSceneMode.Single);
        
    }


}
