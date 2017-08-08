using UnityEditor;
using UnityEngine;

namespace BlendShapesDirector
{
    /// <summary>
    /// Adds a selector to easily apply presets defined by the class implementing PresetProvider which is given by
    /// the associated BlendShapesController.
    /// Also allows to dump to a file the current values of weights and reset those values.
    /// </summary>
    [CustomEditor(typeof(BlendShapesController), true)]
    public class BlendShapesControllerEditor : Editor
    {
        private int index = 0;

        public override void OnInspectorGUI()
        {
            // Default unity inspector (serialized fields and public fields)
            DrawDefaultInspector();
            
            BlendShapesController controller = (BlendShapesController)target;

            PresetProvider presetProdiver = controller.GetPresetProvider();
            string[] presets = presetProdiver.GetPresetsOptions();

            // Preset selector
            EditorGUILayout.BeginHorizontal();
            index = EditorGUILayout.Popup(index, presets);
            if (GUILayout.Button("Apply"))
            {
                controller.ApplyPreset(index);
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            // Shortcut to save to file the current weights
            if (GUILayout.Button("Save current weights to file"))
            {
                controller.SaveCurrentWeightsToFile();
            }
            // Shortcut to reset all blend shapes to 0
            if (GUILayout.Button("Reset blend shapes"))
            {
                controller.ResetAll();
            }
            EditorGUILayout.EndHorizontal();
        }
    }

}