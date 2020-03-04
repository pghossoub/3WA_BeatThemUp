#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

/// <summary>
/// Customisation des éléments GameEvent
/// </summary>
[CustomEditor(typeof(GameEvent))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        //Utilise l'inspecteur par défaut
        DrawDefaultInspector();
        //Actif si l'application est en cours
        GUI.enabled = Application.isPlaying;
        //Ajoute aux objets "GameEvent" un bouton "Raise" qui enclenche la fonction "Raise()"
        GameEvent gameEvent = (GameEvent)target;
        if (GUILayout.Button("Raise"))
        {
            gameEvent.Raise();
        }
    }
}
#endif
