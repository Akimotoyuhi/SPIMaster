using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class UpdateButton : Editor
{
    public override void OnInspectorGUI()
    {
        GameManager gameManager = target as GameManager;
        if (GUILayout.Button("Update"))
        {
            gameManager.MasterDataUpdate();
        }
        base.OnInspectorGUI();
    }
}