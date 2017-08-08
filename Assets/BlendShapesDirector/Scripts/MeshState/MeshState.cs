using System.Collections.Generic;

namespace BlendShapesDirector
{

    /// <summary>Defines contract for MeshState implementations</summary>
    public interface MeshState
    {
        /// <summary>Retrieves set of weights that corresponds to a set of blend shapes</summary>
        /// <returns>Set of floats that corresponds to a set of blend shapes</returns>
        Dictionary<int, float> GetWeightSet();

        /// <summary>Retrieve a weight corresponding to a specific blend shape</summary>
        /// <param name="index">Index of the weight</param>
        /// <returns>Weight corresponding to a specific blend shape. If the weight is not assigned in the set, -1 should be returned</returns>
        float GetWeight(int index);
    }

}