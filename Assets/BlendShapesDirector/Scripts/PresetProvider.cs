using System.Collections.Generic;

namespace BlendShapesDirector
{
    /// <summary>Defines contract for classes that loads/defines presets of blend shapes weights sets</summary>
    public interface PresetProvider
    {
        /// <summary>Retrieves names of the various presets (used in Unity editor for example)</summary>
        /// <returns>Array of strings containing the names of the various presets</returns>
        string[] GetPresetsOptions();

        /// <summary>Get a weight set corresponding to a specific preset</summary>
        /// <param name="index">Numerical index of the preset</param>
        /// <returns>Weight set of the preset</returns>
        Dictionary<int, float> GetPresetWeightSet(int index);
    }

}