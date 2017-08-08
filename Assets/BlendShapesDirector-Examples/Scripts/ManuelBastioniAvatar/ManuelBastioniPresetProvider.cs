using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;

namespace BlendShapesDirector.Examples
{

    /// <summary>Used to store a set of arrays that defines presets of weights corresponding to specific face expressions</summary>
    public class ManuelBastioniPresetProvider : PresetProvider
    {
        /// <summary>Enumerates face expressions' presets</summary>
        public enum Presets
        {
            // as defined in Manuel Bastioni plugin
            neutral,
            amused,
            anxious,
            arrogant,
            attentive,
            bored,
            crying,
            crying02,
            curious,
            disgusted,
            disgusted02,
            drained,
            excited,
            frown,
            furious,
            furious02,
            grin,
            grin02,
            kiss,
            lazy,
            pain,
            peaceful,
            pleasured,
            pouty,
            relaxed,
            sad,
            sad02,
            scared,
            serene,
            shock,
            shy,
            sleepy,
            smile,
            surprise,
            surprise02,
            surprise03,
            terrified,
            tired,
            upset,
            upset02,
            worried
        }

        private static Dictionary<Presets, Dictionary<int, float>> presets = null;

        /// <summary>Load presets for a json file that contains all weights as defined by the Manuel Bastioni plugin in Blender</summary>
        private static void LoadPresets()
        {
            if (presets != null)
            {
                return;
            }

            presets = new Dictionary<Presets, Dictionary<int, float>>();
            presets[Presets.neutral] = new Dictionary<int, float>();

            // Parsing JSON file
            TextAsset jsonFile = Resources.Load<TextAsset>("avatarExpressions");
            string jsonString = jsonFile.text;
            var parsedJson = JSON.Parse(jsonString).AsObject;
            foreach (KeyValuePair<string, JSONNode> entry in parsedJson)
            {
                // Must cast string to enum (http://stackoverflow.com/a/16104)
                Presets currentExpression = (Presets)Enum.Parse(typeof(Presets), entry.Key, true);
                presets[currentExpression] = new Dictionary<int, float>();

                var subDict = entry.Value.AsObject;
                foreach (KeyValuePair<string, JSONNode> subEntry in subDict)
                {
                    presets[currentExpression].Add(ManuelBastioniBlendShapes.GetIndex(subEntry.Key), subEntry.Value.AsFloat * 100f);
                }
            }

        }

        public ManuelBastioniPresetProvider()
        {
            LoadPresets();
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