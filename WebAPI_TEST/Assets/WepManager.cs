using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class GameResult
{
    public string UserName;
    public int Score;
}


public class WepManager : MonoBehaviour
{
    string _baseUrl = "https://localhost:7207/api";

    // Start is called before the first frame update
    void Start()
    {
        GameResult res = new GameResult()
        {
            UserName = "Garam",
            Score = 999,
        };

        SendPostRequest("Ranking", res , (uwr) =>
        {
            Debug.Log("TODO : UI 갱신하기, POST");
        });

        SendGetRequest("Ranking", (uwr) =>
        {
            Debug.Log("TODO : UI 갱신하기, GET");
        });
    }

    public void SendPostRequest(string url, object obj, Action<UnityWebRequest> callback)
    {
        StartCoroutine(CoSendHttpRequest(url, "POST", obj, callback));
    }

    public void SendGetRequest(string url, Action<UnityWebRequest> callback)
    {
        StartCoroutine(CoSendHttpRequest(url, "GET", null, callback));
    }


    IEnumerator CoSendHttpRequest(string url, string method, object obj, Action<UnityWebRequest> callback)
    {
        string sendUrl = $"{_baseUrl}/{url}";

        byte[] jsonBytes = null;

        if (obj != null)
        {
            string jsonStr = JsonUtility.ToJson(obj);
            jsonBytes = Encoding.UTF8.GetBytes(jsonStr);
        }

        var uwr = new UnityWebRequest(sendUrl, method);
        uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            Debug.Log("Recv " + uwr.downloadHandler.text);
            callback.Invoke(uwr);
        }
    }
}
