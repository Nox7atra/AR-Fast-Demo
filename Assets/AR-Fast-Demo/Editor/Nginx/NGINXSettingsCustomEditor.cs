using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NGINXSettings))]
public class NGINXSettingsCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var nginxSettings = target as NGINXSettings;
        base.OnInspectorGUI();
        if (GUILayout.Button("Start Nginx"))
        {
            nginxSettings.StartNginx();
        }
        if (GUILayout.Button("Stop Nginx"))
        {
            nginxSettings.StopNginx();
        }
    }
}
