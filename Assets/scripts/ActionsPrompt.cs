using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionsPrompt : MonoBehaviour
{
    private TextMeshProUGUI textDisplay;
    private ArrayList prompts;

    // Start is called before the first frame update
    void Start()
    {
        prompts = new ArrayList();
        textDisplay = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (prompts.Count > 0)
        {
            prompts.RemoveAt(0);
            alterText();
        }
    }

    public void Show(string text)
    {
        if (prompts.Count < 4 && !prompts.Contains(text))
        {
            prompts.Add(text);
            alterText();
        }
    }

    void alterText()
    {
        string final_text = "";

        foreach (string s in prompts)
        {
            final_text += s + "<br>";
        }

        textDisplay.text = final_text;
    }
}
