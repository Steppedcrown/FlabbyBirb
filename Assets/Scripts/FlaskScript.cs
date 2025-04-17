using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using UnityEngine.UI;

public class FlaskScript : MonoBehaviour
{
    public Text aiResponseText;
    public Text defaultText;
    public InputField userInputField;
    void Start()
    {
        StartCoroutine(SendRequest());
    }

    IEnumerator SendRequest()
    {
        string userPrompt = userInputField.text;
        if (string.IsNullOrEmpty(userPrompt))
        {
            userPrompt = UnityWebRequest.EscapeURL("What's 1 + 2?");
        }
        Debug.Log("User Prompt: " + userPrompt);

        string url = $"http://127.0.0.1:5000/ask?prompt={userPrompt}";
        //Debug.Log("Request URL: " + url);

        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            aiResponseText.text = request.downloadHandler.text;
            Debug.Log("AI Response: " + aiResponseText.text);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
            aiResponseText.text = "Error: " + request.error;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(SendRequest());
            userInputField.text = defaultText.text;
        }
    }
}

