using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class TitleController : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    public void Start()
    {
        highScoreText.text = "ハイスコア：" + PlayerPrefs.GetInt("HighScore");
    }
    public void OnStartButtonClicked()
    {

        SceneManager.LoadScene("game");
    }
}
