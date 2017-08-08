using System.Collections.Generic;
using System.Linq;

namespace BlendShapesDirector
{
    /// <summary>
    /// Is composed of multiple sub mesh states stored in a dictionary whose keys
    /// are of type T
    /// </summary>
    public class CompositeMeshState<T> : MeshState
    {

        private Dictionary<T, MeshState> subMeshStates = null;

        public CompositeMeshState()
        {
            subMeshStates = new Dictionary<T, MeshState>();
        }

        public void SetComponent(T meshId, MeshState subMeshState)
        {
            if (subMeshStates.ContainsKey(meshId))
            {
                subMeshStates[meshId] = subMeshState;
            }
            else
            {
                subMeshStates.Add(meshId, subMeshState);
            }
        }

        /// <inheritDoc/>
        public Dictionary<int, float> GetWeightSet()
        {
            Dictionary<int, float> weightSet = new Dictionary<int, float>();
            Dictionary<int, int> normalizationSet = new Dictionary<int, int>();
            foreach (KeyValuePair<T, MeshState> entry in subMeshStates)
            {
                Dictionary<int, float> subSet = entry.Value.GetWeightSet();
                foreach (KeyValuePair<int, float> weight in subSet)
                {
                    if (weightSet.ContainsKey(weight.Key))
                    {
                        weightSet[weight.Key] += weight.Value;
                        normalizationSet[weight.Key]++;
                    }
                    else
                    {
                        weightSet.Add(weight.Key, weight.Value);
                        normalizationSet.Add(weight.Key, 1);
                    }
                }
            }

            foreach (int key in weightSet.Keys.ToList())
            {
                weightSet[key] = weightSet[key] / (float)normalizationSet[key];
            }

            return weightSet;
        }

        /// <inheritDoc/>
        public float GetWeight(int index)
        {
            int i = 0;
            float total = 0f;
            foreach (KeyValuePair<T, MeshState> entry in subMeshStates)
            {
                float w = entry.Value.GetWeight(index);
                if (w != -1f)
                {
                    total += w;
                    i++;
                }
            }
            return (total != 0f) ? total / (float)i : -1f;
        }

    }

}