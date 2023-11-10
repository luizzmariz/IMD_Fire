using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject MainMenuObj;
    public GameObject SettingsObj;
    [SerializeField] public Slider volumeSlider;
    public bool paused = false;
    public int level = -1; // workaround, please ignore (im lazy sorry)

    private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.Find("Canvas").gameObject;
        if (level >= 0) Levels.currentLevel = level;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void Settings()
    {
        MainMenuObj.SetActive(!MainMenuObj.activeSelf);
        SettingsObj.SetActive(!SettingsObj.activeSelf);
    }

    public void ExitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void audioVolume(float v)
    {
        PlayerPrefs.SetFloat("Volume", v);
    }

    public void pauseGame()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        canvas.SetActive(true);
        paused = true;

        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }

    public void resumeGame()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        canvas.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }
}
