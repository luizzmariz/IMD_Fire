using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameVictory : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text += PlayerPrefs.GetInt("Score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(Levels.levels[PlayerPrefs.GetInt("CurrentLevel")]);
    }

    public void Exit()
    {
        PlayerPrefs.SetInt("CurrentLevel", 0); //lazy workaround
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
