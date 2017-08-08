using UnityEngine;

namespace BlendShapesDirector
{
    /// <summary>Allows to manage blend shapes associated to a specific SkinnedMeshRenderer as a whole</summary>
    [ExecuteInEditMode]
    abstract public class BlendShapesController : MonoBehaviour
    {
        /// <summary>Resets all blend shapes to 0 on Awake if set to true</summary>
        [SerializeField]
        private bool resetOnStart = true;

        /// <summary>Reference to the skinnedMeshRenderer component</summary>
        private SkinnedMeshRenderer skinnedMeshRenderer;
        /// <summary>Number of blend shapes associated to the mesh</summary>
        private int blendShapesCount;

        /// <summary>Retrieve the associated preset provider</summary>
        public abstract PresetProvider GetPresetProvider();
        
        #region UNITY METHODS

        public void Awake()
        {
            if (resetOnStart)
            {
                ResetAll();
            }

            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            blendShapesCount = skinnedMeshRenderer.sharedMesh.blendShapeCount;
        }

        #endregion

        #region HELPER FUNCTIONS

        public int GetBlendShapesCount()
        {
            return blendShapesCount;
        }

        /// <summary>Browse all blend shapes and reset every value to 0</summary>
        public void ResetAll()
        {
            for (int i = 0; i < blendShapesCount; i++)
            {
                ApplyWeight(i, 0);
            }
        }

        /// <summary>Shortened version for skinnedMeshRenderer.SetBlendShapeWeight</summary>
        /// <param name="index">Numeric index of the blend shapes</param>
        /// <param name="weight">Expected value between 0 and 1 (or 0 and 100 if performScale is false). Other values are accepted but won't do more changes than the extremes values</param>
        /// <param name="performScale">If set to true, the weight will be multiplied by 100.</param>
        public void ApplyWeight(int index, float weight, bool performScale = false)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(index, (performScale ? 100 : 1) * weight);
        }

        /// <summary>Apply a set of weights to create the corresponding deformation</summary>
        /// <param name="meshState">Instance of MeshState that provides the weight set</param>
        /// <exception cref="System.ArgumentException">Thrown when the set length is not compatible with the length of the set of blend shapes</exception>
        public virtual void ApplyMeshState(MeshState meshState)
        {
            for (int i = 0; i < blendShapesCount; i++)
            {
                ApplyWeight(i, meshState.GetWeight(i));
            }
        }

        /// <summary>Apply a deformation based on a preset</summary>
        /// <param name="e">Preset index</param>
        /// <exception cref="UnauthorizedAccessException">Thrown if the method was called while playing</exception>
        public void ApplyPreset(int index)
        {
            ApplyMeshState(new BasicMeshState(GetPresetProvider().GetPresetWeightSet(index)));
        }

        /// <summary>Save the current blend shape weights in a file so that it can be used as a preset later on (as an array of weights)</summary>
        public void SaveCurrentWeightsToFile()
        {
            Mesh m = skinnedMeshRenderer.sharedMesh;
            System.IO.File.WriteAllText("weightsOutput.txt", ""); // Clean up the file

            for (int i = 0; i < m.blendShapeCount; i++)
            {
                float weight = skinnedMeshRenderer.GetBlendShapeWeight(i);
                weight = (weight > 100f) ? 100f : weight;
                weight = (weight < 0f) ? 0f : weight;
                System.IO.File.AppendAllText("weightsOutput.txt", ((i > 0) ? "," : "") + weight);
            }
            Debug.Log("Weights file dumped successfully.");
        }

        #endregion
    }

}