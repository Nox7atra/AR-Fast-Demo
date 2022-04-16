using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class UIShowServerAddress : MonoBehaviour
{
    [SerializeField] private WebServer.WebServer _server;
    [SerializeField] private TMP_Text _hostText;

    private List<string> _urls;
    private int _currentIndex;
    void Start()
    {
        _urls = new List<string>();
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                var ipStr = ip.ToString();
                _urls.Add($"http://{ipStr}:{10020}");
            }
        }

        _currentIndex = 0;
        _hostText.text = _urls[_currentIndex];
    }

    public void Next()
    {
        _currentIndex = (_currentIndex + 1) % _urls.Count;
        _hostText.text = _urls[_currentIndex];
    }

    public void Prev()
    {
        _currentIndex--;
        if (_currentIndex < 0)
        {
            _currentIndex = _urls.Count - 1;
        }
        _hostText.text = _urls[_currentIndex];
    }
}
