
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectLoader : MonoBehaviour
{
    private const string Host_Key = "HOST_KEY";
    private const string Bundle_Key = "BUNDLE_KEY";
    private const string Object_Key = "OBJECT_KEY";
    [SerializeField] private ObjectPlacer _objectPlacer;
    [SerializeField] private TMP_InputField _host;
    [SerializeField] private TMP_InputField _bundleName;
    [SerializeField] private TMP_InputField _objectName;
    [SerializeField] private TMP_Text _error;
    [SerializeField] private GameObject _loadObjectPanel;

    private Dictionary<string, AssetBundle> _loadedAssetBundles = new Dictionary<string, AssetBundle>();

    private void Start()
    {
        if (PlayerPrefs.HasKey(Host_Key))
        {
            _host.text = PlayerPrefs.GetString(Host_Key);
            _bundleName.text = PlayerPrefs.GetString(Bundle_Key);
            _objectName.text = PlayerPrefs.GetString(Object_Key);
        }
    }

    public void OnBack()
    {
        _loadObjectPanel.SetActive(true);
    }
    public void OnLoadObjectButton()
    {

        if (_loadedAssetBundles.ContainsKey(_bundleName.text))
        {
            _loadObjectPanel.SetActive(false);
            _objectPlacer.Load(_loadedAssetBundles[_bundleName.text].LoadAsset(_objectName.text) as GameObject);
        }
        else
        {
            LoadObject(_host.text, _bundleName.text, _objectName.text);
        }
    }
    private void LoadObject(string host, string bundleName, string objectId)
    {
        PlayerPrefs.SetString(Host_Key, host);
        PlayerPrefs.SetString(Bundle_Key, bundleName);
        PlayerPrefs.SetString(Object_Key, objectId);
        PlayerPrefs.Save();
        StartCoroutine(LoadBundle(host, bundleName, objectId, go =>
        {
            _loadObjectPanel.SetActive(false);
            _objectPlacer.Load(go);
        }));
    }

    private IEnumerator LoadBundle(string url, string bundleName, string objectId, Action<GameObject> OnSuccess)
    {
        if (url[^1] == '/')
        {
            url = $"{url}{bundleName}";
        }
        else
        {
            url = $"{url}/{bundleName}";
        }
        using (var request = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return request.SendWebRequest();
            if (request.error != null)
            {
                _error.text = request.error;
            }
            
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
            OnSuccess?.Invoke(bundle.LoadAsset(objectId) as GameObject);
        }
    }
}
