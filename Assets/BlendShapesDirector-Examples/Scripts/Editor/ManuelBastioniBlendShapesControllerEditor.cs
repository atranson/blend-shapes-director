using UnityEditor;
using UnityEngine;

namespace BlendShapesDirector.Examples
{
    /// <summary>
    /// Adds two sliders to control gaze direction of the avatar
    /// </summary>
    [CustomEditor(typeof(ManuelBastioniBlendShapesController), true)]
    public class ManuelBastioniBlendShapesControllerEditor : BlendShapesControllerEditor
    {
        private float leftRightPosition = 0f;
        private float upDownPosition = 0f;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            bool stateIncludeEyes = serializedObject.FindProperty("stateIncludeEyes").boolValue;
            if (stateIncludeEyes)
            {
                EditorGUI.BeginDisabledGroup(true);
            }

            ManuelBastioniBlendShapesController controller = (ManuelBastioniBlendShapesController)target;
            leftRightPosition = EditorGUILayout.Slider("Left-right gaze", leftRightPosition, -1f, 1f);
            upDownPosition = EditorGUILayout.Slider("Up-down gaze", upDownPosition, -1f, 1f);

            if (stateIncludeEyes)
            {
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                controller.MoveEyes(new Vector2(leftRightPosition, upDownPosition));
            }
        }
    }

}