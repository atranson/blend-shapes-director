using System.Collections.Generic;


namespace BlendShapesDirector
{

    /// <summary>Simple face state based on a preset</summary>
    public class BasicMeshState : MeshState
    {
        /// <summary>Stores the set of weights</summary>
        private Dictionary<int, float> weightSet;

        public BasicMeshState(Dictionary<int, float> weightSet = null)
        {
            this.weightSet = (weightSet == null) ? new Dictionary<int, float>() : weightSet;
        }

        /// <inheritDoc/>
        public Dictionary<int, float> GetWeightSet()
        {
            return weightSet;
        }

        /// <inheritDoc/>
        public float GetWeight(int index)
        {
            if(weightSet.ContainsKey(index))
            {
                return weightSet[index];
            }
            else
            {
                return -1f;
            }
        }

        public override string ToString()
        {
            return "Basic mesh state";
        }
    }

}