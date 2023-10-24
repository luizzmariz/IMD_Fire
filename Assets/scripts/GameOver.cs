using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI deathText;

    // Start is called before the first frame update
    void Start()
    {
        deathText.text += PlayerPrefs.GetString("DeathReason");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //PlayerPrefs.SetString("DeathReason", reason);
            SceneManager.LoadScene("MainGame");
        }
    }
}
