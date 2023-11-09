using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject MainMenuObj;
    public GameObject SettingsObj;

    // Start is called before the first frame update
    void Start()
    {
        
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
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void audioVolume(float v)
    {
        PlayerPrefs.SetFloat("Volume", v);
    }
}
