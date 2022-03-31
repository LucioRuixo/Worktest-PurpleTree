using UnityEditor;
using Worktest_PurpleTree.Gameplay.Physics;

namespace Worktest_PurpleTree.Editor
{
    [CustomEditor(typeof(Physics))]
    public class PhysicsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Physics physics = (Physics)target;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Movement", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Velocity", physics.Velocity.ToString());
            EditorGUILayout.LabelField("Speed", physics.Velocity.magnitude.ToString());
        }
    }
}