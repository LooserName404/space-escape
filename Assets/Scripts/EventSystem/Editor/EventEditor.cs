using UnityEditor;
using UnityEngine;

namespace SpaceEscape.EventSystem.Editor
{
    [CustomEditor(typeof(GameEvent), editorForChildClasses: true)]
    public class EventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            var e = target as GameEvent;
            if (GUILayout.Button("Raise") && e != null)
            {
                e.Raise();
            }
        }
    }
}