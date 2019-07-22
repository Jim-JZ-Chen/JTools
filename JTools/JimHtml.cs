using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;
using System.Collections.Generic;

public class JimHtml : MonoBehaviour
{

    public static void DownLoadText(MonoBehaviour obj, string url, Action<string, bool> processString)
    {
        obj.StartCoroutine(DownloadingText(url, processString));
    }

    private static IEnumerator DownloadingText(string url, Action<string, bool> processString)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        
        //yield return www.Send();
        yield return www.SendWebRequest();
        processString(www.downloadHandler.text, www.isNetworkError);
        /*
        if (www.isNetworkError)
        {
            Debug.Log(www.error);
            processString(www.downloadHandler.text, false);
        }
        else
        {
            processString(www.downloadHandler.text, true);
        }
        */

    }

    public static void DownLoadJson<T>(MonoBehaviour obj, string url, bool isArray, Action<T, bool> processT)
    {
        //obj.StartCoroutine(DownloadingJson(url, isArray, processT));
        print(url);
        DownLoadText(obj, url, (string json, bool isError)=> {
            T t = default(T);
            print(url +" a "+json);
            if (json == "") { isError = true; }
            if (!isError)
            {
                t = GetT<T>(isArray, json);
            }
            processT(t, isError); 

        });
    }

    public static T GetT<T>(bool isArray, string sJson)
    {
        if (isArray)
        {
            //if there is only one item in Array
            Debug.Log(sJson);
            if (sJson[0] != '[')
            {
                sJson = "[ " + sJson + "]";
            }
            sJson = "{ \"list\": " + sJson + "}";
        }
        //Debug.Log(sJson);
        return JsonUtility.FromJson<T>(sJson);
    }

    public static UnityWebRequest PostText(string url , WWWForm form)
    {
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        www.SendWebRequest();
        return www;
    }

    public static void PostText(string url, WWWForm form, Action<string, bool> processString, MonoBehaviour obj)
    {
        obj.StartCoroutine(PostingText(url, form, processString));
    }

    private static IEnumerator PostingText(string url, WWWForm form, Action<string, bool> processString)
    {
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();      
        processString(www.isNetworkError ? www.error : www.downloadHandler.text, www.isNetworkError);
    }
}


