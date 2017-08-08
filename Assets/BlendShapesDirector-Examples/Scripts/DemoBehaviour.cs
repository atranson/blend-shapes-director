using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace BlendShapesDirector.Examples
{
    public class DemoBehaviour : MonoBehaviour
    {

        private enum Mode
        {
            Single,
            Merge,
            Combine
        }

        private Mode currentMode = Mode.Single;
        private List<string> presetsOptions;
        private MeshState[] meshStates = new MeshState[4];
        private float[] cursorValues = new float[4];
        private MergeMeshState mergeMeshState;
        private CombinedMeshState combineMeshState;

        [SerializeField]
        private BlendShapesController controller;
        [SerializeField]
        private Transform singleMeshSelectionGroup;
        [SerializeField]
        private Transform mergeMeshSelectionGroup;
        [SerializeField]
        private Transform combineMeshSelectionGroup;

        private List<Dropdown> dropdowns = new List<Dropdown>();
        private List<Slider> sliders = new List<Slider>();

        public void Awake()
        {
            // Register all dropdowns and sliders for each demo mode
            Transform[] groups = new Transform[3] { singleMeshSelectionGroup, mergeMeshSelectionGroup, combineMeshSelectionGroup };
            foreach(Transform group in groups)
            {
                Dropdown[] groupDropdowns = group.GetComponentsInChildren<Dropdown>();
                Slider[] groupSliders = group.GetComponentsInChildren<Slider>();
                foreach(Dropdown d in groupDropdowns) { dropdowns.Add(d); }
                foreach (Slider s in groupSliders) { sliders.Add(s); }
            }

            presetsOptions = new List<string>(controller.GetPresetProvider().GetPresetsOptions());
            foreach (Dropdown presetDropdown in dropdowns)
            {
                presetDropdown.ClearOptions();
                presetDropdown.AddOptions(presetsOptions);
            }

            // Reset choice to default
            HandleModeChoice(0);
        }

        public void HandleModeChoice(int choice)
        {
            DisableAllGroups();
            Reset();

            switch (choice)
            {
                // Combine multiple mesh states
                case 2:
                    currentMode = Mode.Combine;
                    SetActiveChildrenGroup(combineMeshSelectionGroup, true);
                    break;

                // Merge two mesh states
                case 1:
                    currentMode = Mode.Merge;
                    SetActiveChildrenGroup(mergeMeshSelectionGroup, true);
                    break;

                // Single mesh state
                default:
                case 0:
                    currentMode = Mode.Single;
                    SetActiveChildrenGroup(singleMeshSelectionGroup, true);
                    break;
            }
        }

        public void SetCursor(int index, float cursor)
        {
            cursorValues[index] = cursor;
            UpdateMeshState();
        }

        public void SetMeshState(int index, int presetIndex)
        {
            meshStates[index] = new BasicMeshState(controller.GetPresetProvider().GetPresetWeightSet(presetIndex));
            UpdateMeshState();
        }

        private void UpdateMeshState()
        {
            switch(currentMode)
            {
                case Mode.Combine:
                    List<CombinedMeshState.MeshStateWeightPair> meshStatesCombination = new List<CombinedMeshState.MeshStateWeightPair>();
                    for (int i = 0; i < meshStates.Length; i++)
                    {
                        meshStatesCombination.Add(new CombinedMeshState.MeshStateWeightPair(meshStates[i], cursorValues[i]));
                    }
                    controller.ApplyMeshState(new CombinedMeshState(meshStatesCombination));
                    break;

                case Mode.Merge:
                    controller.ApplyMeshState(new MergeMeshState(meshStates[0], meshStates[1], cursorValues[0]));
                    break;

                default:
                case Mode.Single:
                    controller.ApplyMeshState(meshStates[0]);
                    break;
            }
        }

        private void Reset()
        {
            MeshState neutralMeshState = new BasicMeshState(new Dictionary<int, float>());
            for(int i=0; i<meshStates.Length; i++)
            {
                cursorValues[i] = 0f;
                meshStates[i] = neutralMeshState;
            }

            foreach (Dropdown presetDropdown in dropdowns)
            {
                presetDropdown.value = 0;
                int dropdownIndex = (int)char.GetNumericValue(presetDropdown.gameObject.name[presetDropdown.gameObject.name.Length - 1]) - 1;
                UnityAction<int> partialSetMeshState = x => SetMeshState(dropdownIndex, x);
                presetDropdown.onValueChanged.AddListener(partialSetMeshState);
            }

            foreach (Slider slider in sliders)
            {
                slider.value = 0;
                int sliderIndex = (int)char.GetNumericValue(slider.gameObject.name[slider.gameObject.name.Length - 1]) - 1;
                UnityAction<float> partialSetCursor = x => SetCursor(sliderIndex, x);
                slider.onValueChanged.AddListener(partialSetCursor);
            }

            UpdateMeshState();
        }

        private void DisableAllGroups()
        {
            SetActiveChildrenGroup(singleMeshSelectionGroup, false);
            SetActiveChildrenGroup(mergeMeshSelectionGroup, false);
            SetActiveChildrenGroup(combineMeshSelectionGroup, false);
        }

        private void SetActiveChildrenGroup(Transform parent, bool active)
        {
            foreach (Transform child in parent)
            {
                child.gameObject.SetActive(active);
            }
        }
    }
}