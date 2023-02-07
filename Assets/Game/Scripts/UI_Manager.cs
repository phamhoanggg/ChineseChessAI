using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : FastSingleton<UI_Manager>
{
    public GameObject MainMenu, GamePlay, Loading,EndGame, PlayBtn;
    public Transform PrePos;
    public Text TurnText, EndGameText;
    public Animator TextAnim;
    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        GamePlay.SetActive(false);
        PlayBtn.SetActive(false);
        PrePos.gameObject.SetActive(false);
    }


    public void SelectEasy()
    {
        GameController.instance.difficulty = Difficulty.Easy;
        PlayBtn.SetActive(true);
    }

    public void SelectHard()
    {
        GameController.instance.difficulty = Difficulty.Hard;
        PlayBtn.SetActive(true);
    }

    public void SelectPlay()
    {
        PlayBtn.SetActive(false);
        MainMenu.SetActive(false);
        Loading.SetActive(true);
        Invoke(nameof(SetGamePlayActive), 0.5f);
    }

    public void SelectMainMenu()
    {
        SceneManager.LoadScene("GamePlayScene");
    }

    public void SetGamePlayActive()
    {
        Loading.SetActive(false);
        GamePlay.SetActive(true);
        GameController.instance.StartGame();
    }

    public void TextAppear(string text)
    {
        TurnText.text = text;
        TextAnim.SetTrigger("appear");
        Invoke(nameof(TextStatic), 1);
    }

    public void TextStatic()
    {
        TextAnim.ResetTrigger("appear");
    }
}
