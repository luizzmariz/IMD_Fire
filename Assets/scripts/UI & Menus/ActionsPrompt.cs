using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionsPrompt : MonoBehaviour
{
    private TextMeshProUGUI textDisplay;
    private ArrayList prompts;
    private Dictionary<string, float> demands;
    private float updateDisplayFreq = 0.25f; // each 0.25f seconds
    private float r_updateDisplayFreq = 0f;

    // Start is called before the first frame update
    void Start()
    {
        prompts = new ArrayList();
        demands = new Dictionary<string, float>();
        textDisplay = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Removes elements from 'prompts' after they run out of demand
        for (int i = 0; i < prompts.Count; i ++)
        {
            if (demands[(string) prompts[i]] < Time.deltaTime)
            {
                prompts.RemoveAt(i);
                break;
            }
        }

        // Updating display
        if (r_updateDisplayFreq > updateDisplayFreq) {
            alterText();
            r_updateDisplayFreq = 0f;
        }
        r_updateDisplayFreq += Time.deltaTime;

        // Removing demand from prompts
        foreach(string s in prompts) 
        {
            demands[s] -= Time.deltaTime;
        }
    }

    // Adds 'text' to the prompts on the screen
    public void Show(string text)
    {
        if (prompts.Count < 4 && !prompts.Contains(text))
        {
            prompts.Add(text);
            alterText();
            registerDemand(text);
        }
    }

    // Increases demand for 'text'
    private void registerDemand(string text)
    {
        if (!demands.ContainsKey(text))
        {
            demands.Add(text, 1f);
        }
        else {
            demands[text] = 1f;
        }
    }

    // Normalizes/Adapts text to be shown as a vertical list on the screen
    void alterText()
    {
        prompts.Sort();

        string final_text = "";

        foreach (string s in prompts)
        {
            final_text += s + "<br>";
        }

        textDisplay.text = final_text;
    }
}
