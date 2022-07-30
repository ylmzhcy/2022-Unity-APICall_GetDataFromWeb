using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json;
using TMPro;

public class APICall : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    public class Fact
    {
        public string fact { get; set; }
        public int length { get; set; }
    }

    private void Start()
    {
        GetData();
    }

    public void BtnRefresh()
    {
        GetData();
    }

    private void GetData()
    {
        StartCoroutine(GetApiRequest("https://catfact.ninja/fact"));
    }

    IEnumerator GetApiRequest(String url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(String.Format("Something went wrong : {0}", webRequest.error));
                    break;
                case UnityWebRequest.Result.Success:
                    Fact fact = JsonConvert.DeserializeObject<Fact>(webRequest.downloadHandler.text);
                    text.text = fact.fact;
                    break;
            }
        }
    }
}
