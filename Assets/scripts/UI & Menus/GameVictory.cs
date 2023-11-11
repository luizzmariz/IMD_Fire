using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameVictory : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public AudioClip levelcomplete_audio;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text += PlayerPrefs.GetInt("Score");
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(levelcomplete_audio);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(Levels.levels[Levels.currentLevel]);
    }

    public void Exit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
