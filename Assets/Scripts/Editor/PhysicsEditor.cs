using UnityEditor;
using Worktest_PurpleTree.Gameplay.Physics;

namespace Worktest_PurpleTree.Editor
{
    [CustomEditor(typeof(Physics))]
    public class PhysicsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            Physics physics = (Physics)target;

            EditorGUILayout.LabelField("Physical Properties", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Velocity", physics.Velocity.ToString());
            EditorGUILayout.LabelField("Speed", physics.Velocity.magnitude.ToString());
            physics.Mass = EditorGUILayout.FloatField("Mass", physics.Mass);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Gravity", EditorStyles.boldLabel);
            physics.Gravity = EditorGUILayout.Toggle("Gravity", physics.Gravity);

            EditorGUI.indentLevel++;

            if (physics.Gravity)
            {
                physics.LocalGravity = EditorGUILayout.Toggle("Local Gravity", physics.LocalGravity);

                EditorGUI.indentLevel++;

                if (physics.LocalGravity)
                {
                    physics.LocalGravityAcceleration = EditorGUILayout.FloatField("Gravity Acceleration", physics.LocalGravityAcceleration);
                    physics.LocalGravityDirection = EditorGUILayout.Vector2Field("Gravity Direction", physics.LocalGravityDirection);
                }
            }

            EditorGUI.indentLevel -= 2;
        }
    }
}