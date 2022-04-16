using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "NGINXSettings", menuName = "NGINX/Settings")]
public class NGINXSettings : ScriptableObject
{
    public const int ServerPort = 10020;
    public const string LogPath = "logs";
    public const string PidFileName = "nginx.pid";
    public Object Nginx;
    public string NginxPath => Path.GetFullPath(AssetDatabase.GetAssetPath(Nginx));
    
    public void StartNginx()
    {
        var dir = Path.GetDirectoryName(NginxPath) ?? string.Empty;
        if (!File.Exists(Path.Combine(dir, LogPath, PidFileName)))
        {
            ExecuteCommand(NginxPath , "");
        }
        else
        {
            Debug.Log("Nginx already started!");
        }
    }

    public void StopNginx()
    {
        ExecuteCommand(NginxPath, "-s quit");
    }
    private void ExecuteCommand (string pathToExe, string args)
    {
        Process process = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        startInfo.FileName = pathToExe;
        startInfo.Arguments = args;
        startInfo.UseShellExecute = false;
        var path = Path.GetDirectoryName(NginxPath) ?? string.Empty;

        if (!string.IsNullOrEmpty(path))
        {
            startInfo.WorkingDirectory = path;
        }
        startInfo.CreateNoWindow = true;
        process.EnableRaisingEvents = true;
        process.StartInfo = startInfo;
        process.Start();
        Debug.Log($"Success {pathToExe} {args}");
    }
    
}
