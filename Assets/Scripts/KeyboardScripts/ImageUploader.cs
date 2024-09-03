using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using UnityEngine.Networking;

public class ImageLoader : MonoBehaviour
{
    string path;
    [SerializeField] RawImage image;
    private void Awake()
    {
        image = GetComponentInChildren<RawImage>();
    }

    public void OpenExplorer()
    {
        // I'm actually not sure whether I need "#if UNITY_EDITOR" here, just trying to fix a bug that doesn't let me build the app.
        #if UNITY_EDITOR
        path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
        GetImage();
        #endif
    }

    void GetImage()
    {
        if (path != null)
        {
            UpdateImage();
        }
    }

    void UpdateImage()
    {
        StartCoroutine(DownloadImage());
    }

    IEnumerator DownloadImage()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture("file:///" + path);
        yield return request.SendWebRequest();

        if ((request.result == UnityWebRequest.Result.ConnectionError) || (request.result == UnityWebRequest.Result.ProtocolError))
        {
            Debug.Log(request.error);
        }
        else
        {
            image.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
}