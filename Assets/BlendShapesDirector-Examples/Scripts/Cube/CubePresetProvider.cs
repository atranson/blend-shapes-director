using System.Collections.Generic;
using System;

namespace BlendShapesDirector.Examples
{

    /// <summary>Used to store a set of arrays that defines presets of weights corresponding to a few deformations</summary>
    public class CubePresetProvider : PresetProvider
    {
        public enum Presets
        {
            Base,
            Deformation1,
            Deformation2,
            Deformation3
        }

        private static Dictionary<Presets, Dictionary<int, float>> presets = null;

        private static void InitializePresets()
        {
            // Check if it was not already initialized
            if (presets != null)
            {
                return;
            }

            presets = new Dictionary<Presets, Dictionary<int, float>>();
            presets[Presets.Base] = new Dictionary<int, float> { { 0, 0 }, { 1, 0 } };

            // Defining personal presets
            presets[Presets.Deformation1] = new Dictionary<int, float> { { 0, 100 } };
            presets[Presets.Deformation2] = new Dictionary<int, float> { { 1, 100 } };
            presets[Presets.Deformation3] = new Dictionary<int, float> { { 0, 15 }, { 1, 30 } };
        }

        public CubePresetProvider()
        {
            InitializePresets();
        }

        /// <summary>Retrieve the array of weights corresponding to a preset</summary>
        /// <param name="index">Numerical index of the preset</param>
        /// <returns>Array of weights for each blend shape</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the corresponding preset has not been defined properly</exception>
        /// <exception cref="ArgumentException">Thrown if the corresponding preset does not exist</exception>
        public Dictionary<int, float> GetPresetWeightSet(int index)
        {
            Presets expression = (Presets)index;
            if (!Presets.IsDefined(typeof(Presets), expression))
            {
                throw new ArgumentException("The preset at index " + index + " does not exist");
            }

            try
            {
                return presets[expression];
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException("The preset " + expression + " is not properly defined");
            }
        }

        public string[] GetPresetsOptions()
        {
            return Enum.GetNames(typeof(Presets));
        }
    }
}