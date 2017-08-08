using System.Linq;
using System.Collections.Generic;
using System;

namespace BlendShapesDirector
{
    /// <summary>
    /// Merge two face states to get a new one which is a combination of the first twos
    /// Allows to define a cursor (between 0 and 1) to precise the weight of both face states in the calculation
    /// </summary>
    public class MergeMeshState : MeshState
    {
        public enum MergeStrategy
        {
            Linear
        }

        /// <summary>Set of weights retrieved from the origin face state</summary>
        private Dictionary<int, float> originWeights;
        /// <summary>Set of weights retrieved from the face state that we want to get to</summary>
        private Dictionary<int, float> objectiveWeights;
        /// <summary>Value between 0 and 1 that precise the weight of both face states in the calculation</summary>
        private float cursor = 0f;
        /// <summary>Stores the computed set of weights</summary>
        private Dictionary<int, float> weightSet = null;
        /// <summary>Helps to compute weightSet efficiently</summary>
        private List<int> dictionaryKeys;
        /// <summary>Duration of the transition between the two states</summary>
        private float transitionDuration;
        /// <summary>Stores the method that should be called for merging calculations</summary>
        Func<int, float> computationMethod;

        /// <summary>Initializes all attributes. If transitionDuration is set to 0, there won't be any animation (cursor value won't change over time)</summary>
        /// <param name="origin">Origin face state</param>
        /// <param name="objective">Objective face state</param>
        /// <param name="initialCursor">Initial cursor value</param>
        public MergeMeshState(MeshState origin, MeshState objective, float initialCursor = 0f, float transitionDuration = 1f, MergeStrategy mergeStrategy = MergeStrategy.Linear)
        {
            originWeights = origin.GetWeightSet();
            objectiveWeights = objective.GetWeightSet();

            this.transitionDuration = transitionDuration;
            switch (mergeStrategy)
            {
                default:
                case MergeStrategy.Linear:
                    computationMethod = ComputeLinear;
                    break;
            }

            SetCursor(initialCursor);
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

        public void Update(float deltaTime)
        {
            if (transitionDuration != 0f)
            {
                MoveCursor(deltaTime / transitionDuration);
            }
        }

        /// <summary>Moves the cursor and returns true if 1 was reached</summary>
        /// <param name="move">Translation of the cursor</param>
        /// <returns>True if the transition to the objective face state is complete</returns>
        private bool MoveCursor(float move)
        {
            SetCursor(cursor + move);
            return (cursor == 1f);
        }

        /// <summary>Set new cursor value (and checks that the values is between 0 and 1)</summary>
        /// <param name="newCursor">New cursor value</param>
        public void SetCursor(float newCursor)
        {
            cursor = (newCursor < 0f) ? 0f : ((newCursor > 1f) ? 1f : newCursor);
            ComputeWeightSet();
        }

        /// <summary>Compute new set of weights with current parameters</summary>
        private void ComputeWeightSet()
        {
            if (weightSet == null)
            {
                weightSet = new Dictionary<int, float>();
                List<int> originKeyList = new List<int>(originWeights.Keys);
                List<int> objectiveKeyList = new List<int>(objectiveWeights.Keys);
                dictionaryKeys = originKeyList.Union(objectiveKeyList).ToList();
                foreach (int key in dictionaryKeys)
                {
                    weightSet.Add(key, 0f);
                }
            }

            foreach (int key in dictionaryKeys)
            {
                weightSet[key] = computationMethod(key);
            }
        }

        public override string ToString()
        {
            return "Merging face state (cursor at " + cursor + ")";
        }

        private float ComputeLinear(int key)
        {
            return (originWeights.ContainsKey(key) ? (1 - cursor) * originWeights[key] : 0) + (objectiveWeights.ContainsKey(key) ? cursor * objectiveWeights[key] : 0);
        }
    }

}