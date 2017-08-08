using System.Collections.Generic;
using UnityEngine;

namespace BlendShapesDirector
{
    /// <summary>
    /// Combines multiple mesh states by assigning each of them a certain weight
    /// Performs a weighted mean for each blend shape's weight
    /// </summary>
    public class CombinedMeshState : MeshState
    {
        // Basic structure to hold pair MeshState <> float (since Tuple is not available with the current version of Unity)
        public struct MeshStateWeightPair
        {
            public MeshStateWeightPair(MeshState meshState, float weight)
            {
                this.meshState = meshState;
                this.weight = weight;
            }

            public MeshState meshState;
            public float weight;
        }

        private Dictionary<int, float> weightSet = null;

        public CombinedMeshState(List<MeshStateWeightPair> meshStates)
        {
            if (meshStates.Count == 0)
            {
                throw new System.Exception("No entry in the face states dictionary");
            }

            // The resulting weight set is directly computed
            float totalWeight = 0f;
            weightSet = new Dictionary<int, float>();
            foreach (MeshStateWeightPair entry in meshStates)
            {
                if (entry.weight > 1f)
                {
                    Debug.Log("[CombineMeshState] Warning: Applying a mesh state with weight over 1");
                }

                Dictionary<int, float> MeshStateWeightSet = entry.meshState.GetWeightSet();
                foreach (KeyValuePair<int, float> weightPair in MeshStateWeightSet)
                {
                    if (weightSet.ContainsKey(weightPair.Key))
                    {
                        weightSet[weightPair.Key] += entry.weight * weightPair.Value;
                    }
                    else
                    {
                        weightSet.Add(weightPair.Key, entry.weight * weightPair.Value);
                    }
                }
                totalWeight += entry.weight;
            }

            if (totalWeight > 1f)
            {
                Debug.Log("[CombineMeshState] Warning: Sum of weigths is over 1");
            }
        }

        /// <inheritDoc/>
        public Dictionary<int, float> GetWeightSet()
        {
            return weightSet;
        }

        /// <inheritDoc/>
        public float GetWeight(int index)
        {
            if (weightSet.ContainsKey(index))
            {
                return weightSet[index];
            }
            else
            {
                return -1f;
            }
        }

        /// <inheritDoc/>
        public void Update(float deltaTime) { }
    }

}