using System.Collections.Generic;


namespace BlendShapesDirector.Examples
{
    /// <summary>
    /// Used in place of an enum to store readable integer constants that corresponds to blend shapes index
    /// Get around the problem of int casting for each use of enum value: http://stackoverflow.com/a/11057004 
    /// </summary>
    public static class ManuelBastioniBlendShapes
    {
        public enum Zone
        {
            Brows,
            Cheeks,
            Eyes,
            Mouth
        }

        // Brows related blend shapes [0-5]
        public const int RaiseBrowsCenter = 0;           // Expressions_brow01_max
        public const int EyebrowFrown = 1;               // Expressions_brow01_min
        public const int StretchLeftBrowToCenter = 2;    // Expressions_brow02L_max
        public const int StretchRightBrowToCenter = 3;   // Expressions_brow02R_max
        public const int RaiseLeftBrow = 4;              // Expressions_brow03L_max
        public const int RaiseRightBrow = 5;             // Expressions_brow03R_max
        // Cheeks related blend shapes [6-9]
        public const int RaiseLeftCheek = 6;             // Expressions_cheek01L_max
        public const int RaiseRightCheek = 7;            // Expressions_cheek01R_max
        public const int ContractLeftCheek = 8;          // Expressions_cheek02L_max
        public const int ContractRightCheek = 9;         // Expressions_cheek02R_max
        // Eyes related blend shapes [10-19]
        public const int WinkLeft = 10;                  // Expressions_eye01L_max
        public const int WinkRight = 11;                 // Expressions_eye01R_max
        public const int EyelidLeftClose = 12;           // Expressions_eye02L_max
        public const int EyelidLeftOpenWide = 13;        // Expressions_eye02L_min
        public const int EyelidRightClose = 14;          // Expressions_eye02R_max
        public const int EyelidRightOpenWide = 15;       // Expressions_eye02R_min
        public const int LookRight = 16;                 // Expressions_eye03_max
        public const int LookLeft = 17;                  // Expressions_eye03_min
        public const int LookUp = 18;                    // Expressions_eye04_max
        public const int LookDown = 19;                  // Expressions_eye04_min
        // Mouth related blend shapes [20-43]
        public const int TenseFaceShowingTeeth = 20;     // Expressions_mouth01_max
        public const int LowerChinMouseClosed = 21;      // Expressions_mouth01_min
        public const int SmileMouseClosed = 22;          // Expressions_mouth02_max
        public const int Pout = 23;                      // Expressions_mouth02_min
        public const int SmileShowingTeeth = 24;         // Expressions_mouth03_max
        public const int FrownMouseClosed = 25;          // Expressions_mouth03_min
        public const int SmileMouseOpened = 26;          // Expressions_mouth04_max
        public const int BiteLips = 27;                  // Expressions_mouth04_min
        public const int SmirkLeft = 28;                 // Expressions_mouth05_max
        public const int ShiftJawLeft = 29;              // Expressions_mouth05_min
        public const int SmirkRight = 30;                // Expressions_mouth06_max
        public const int ShiftJawRight = 31;             // Expressions_mouth06_min
        public const int TenseFaceShowingTeethBis = 32;  // Expressions_mouth07_max
        public const int MoveForwardLips = 33;           // Expressions_mouth07_min
        public const int DubiousMouthFrownLeft = 34;     // Expressions_mouth08_max
        public const int DubiousMouthFrownRight = 35;    // Expressions_mouth08_min
        public const int PullingLips = 36;               // Expressions_mouth09_max
        public const int ShowingTeethNeutral = 37;       // Expressions_mouth09_min
        public const int MouthOpenWideShowingTeeth = 38; // Expressions_mouth10_max
        public const int MouthOpenWide = 39;             // Expressions_mouth10_min
        public const int InflateCheeks = 40;             // Expressions_mouth11_max
        public const int HollowingCheeks = 41;           // Expressions_mouth11_min
        public const int MoveForwardChin = 42;           // Expressions_mouth12_max
        public const int ShowLowerTeeth = 43;            // Expressions_mouth12_min

        private static Dictionary<string, int> _dict = new Dictionary<string, int>
        {
            // Brows related blend shapes [0-5]
            {"Expressions_brow01_max", 0},
            {"Expressions_brow01_min", 1},
            {"Expressions_brow02L_max", 2},
            {"Expressions_brow02R_max", 3},
            {"Expressions_brow03L_max", 4},
            {"Expressions_brow03R_max", 5},
            // Cheeks related blend shapes [6-9]
            {"Expressions_cheek01L_max", 6},
            {"Expressions_cheek01R_max", 7},
            {"Expressions_cheek02L_max", 8},
            {"Expressions_cheek02R_max", 9},
            // Eyes related blend shapes [10-19]
            {"Expressions_eye01L_max", 10},
            {"Expressions_eye01R_max", 11},
            {"Expressions_eye02L_max", 12},
            {"Expressions_eye02L_min", 13},
            {"Expressions_eye02R_max", 14},
            {"Expressions_eye02R_min", 15},
            {"Expressions_eye03_max", 16},
            {"Expressions_eye03_min", 17},
            {"Expressions_eye04_max", 18},
            {"Expressions_eye04_min", 19},
            // Mouth related blend shapes [20-43]
            {"Expressions_mouth01_max", 20},
            {"Expressions_mouth01_min", 21},
            {"Expressions_mouth02_max", 22},
            {"Expressions_mouth02_min", 23},
            {"Expressions_mouth03_max", 24},
            {"Expressions_mouth03_min", 25},
            {"Expressions_mouth04_max", 26},
            {"Expressions_mouth04_min", 27},
            {"Expressions_mouth05_max", 28},
            {"Expressions_mouth05_min", 29},
            {"Expressions_mouth06_max", 30},
            {"Expressions_mouth06_min", 31},
            {"Expressions_mouth07_max", 32},
            {"Expressions_mouth07_min", 33},
            {"Expressions_mouth08_max", 34},
            {"Expressions_mouth08_min", 35},
            {"Expressions_mouth09_max", 36},
            {"Expressions_mouth09_min", 37},
            {"Expressions_mouth10_max", 38},
            {"Expressions_mouth10_min", 39},
            {"Expressions_mouth11_max", 40},
            {"Expressions_mouth11_min", 41},
            {"Expressions_mouth12_max", 42},
            {"Expressions_mouth12_min", 43},
        };

        /// <exception cref="KeyNotFoundException">Thrown if the corresponding preset has not been defined properly</exception>
        public static int GetIndex(string blendShapeName)
        {
            try
            {
                return _dict[blendShapeName];
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException("No blendshape with the name " + blendShapeName + " exists.");
            }
        }
        
        /// <summary>Checks if a specific blend shape belongs to a zone of the face (e.g. mouth)</summary>
        /// <param name="zoneName">Name of the zone which is being tested</param>
        /// <param name="index">Numerical index of the blend shape</param>
        /// <returns>True if the blend shape belongs to the specified zone</returns>
        public static bool BelongsToZone(Zone zoneName, int index)
        {
            switch (zoneName)
            {
                case Zone.Brows:
                    return (index >= 0 && index < 6);

                case Zone.Cheeks:
                    return (index >= 6 && index < 10);

                case Zone.Eyes:
                    return (index >= 10 && index < 20);

                case Zone.Mouth:
                    return (index >= 20 && index < 44);

                default:
                    throw new System.Exception("That zone name is not managed");
            }
        }
    }

}