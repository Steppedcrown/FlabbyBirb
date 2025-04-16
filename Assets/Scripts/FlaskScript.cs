using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using UnityEngine.UI;

public class FlaskScript : MonoBehaviour
{
    public Text aiResponseText;
    void Start()
    {
        StartCoroutine(SendRequest());
    }

    IEnumerator SendRequest()
    {
        string userPrompt = UnityWebRequest.EscapeURL("What's 1 + 2?");
        string url = $"http://127.0.0.1:5000/ask?prompt={userPrompt}";

        Debug.Log("Request URL: " + url);

        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("AI Response: " + request.downloadHandler.text);
            aiResponseText.text = request.downloadHandler.text;
        }
        else
        {
            Debug.LogError("Error: " + request.error);
            aiResponseText.text = "Error: " + request.error;
        }
    }
}

