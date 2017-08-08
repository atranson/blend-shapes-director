using UnityEngine;

namespace BlendShapesDirector.Examples
{

    /// <summary>Manages blend shapes associated to a specific SkinnedMeshRenderer for ManuelBastioni avatars</summary>
    public class ManuelBastioniBlendShapesController : BlendShapesController
    {
        public enum Side
        {
            Left,
            Right
        }

        [SerializeField]
        private bool stateIncludeEyes = false;
        private PresetProvider presetProvider = null;

        public override PresetProvider GetPresetProvider()
        {
            if(presetProvider == null)
            {
                presetProvider = new ManuelBastioniPresetProvider();
            }
            return presetProvider;
        }

        /// <summary>Apply a set of weights to create the corresponding face expression</summary>
        /// <param name="preset">Array containing all the weights needed</param>
        /// <exception cref="System.ArgumentException">Thrown when the set length is not compatible with the length of the set of blend shapes.</exception>
        public override void ApplyMeshState(MeshState meshState)
        {
            // Base function is overriden to be able to manage eyes movements independently
            for (int i = 0; i < GetBlendShapesCount(); i++)
            {
                if (stateIncludeEyes || !ManuelBastioniBlendShapes.BelongsToZone(ManuelBastioniBlendShapes.Zone.Eyes, i)) // (stateIncludeEyes OR (!stateIncludeEyes AND !BelongsToZone(Eyes, i)))
                {
                    ApplyWeight(i, meshState.GetWeight(i));
                }
            }
        }

        #region EYES CONTROL METHODS

        /// <summary>Allows to control the gaze of the avatar</summary>
        /// <param name="direction">Define the direction for both axis. Values outside of the range [-1 ; 1] do not have any effect.</param>
        public void MoveEyes(Vector2 direction)
        {
            // Look left or right
            ApplyWeight(ManuelBastioniBlendShapes.LookRight, direction.x, true);
            ApplyWeight(ManuelBastioniBlendShapes.LookLeft, -direction.x, true);

            // Look up or down
            ApplyWeight(ManuelBastioniBlendShapes.LookUp, direction.y, true);
            ApplyWeight(ManuelBastioniBlendShapes.LookDown, -direction.y, true);
        }

        /// <summary>Allows to perform a wink with one eye</summary>
        /// <param name="side">Defines which eye is concerned</param>
        /// <param name="intensity">Between 0 (normal) and 1 (wink).</param>
        public void ControlWink(Side side, float intensity)
        {
            ApplyWeight(side == Side.Left ? ManuelBastioniBlendShapes.WinkLeft : ManuelBastioniBlendShapes.WinkRight, intensity);
        }

        /// <summary>Allows to close the eye lid or to open wide the eye.</summary>
        /// <param name="side">Defines which eye is concerned</param>
        /// <param name="intensity">Between -1 (wide open) and 1 (closed).</param>
        public void ControlEyeLid(Side side, float intensity)
        {
            int closeIndex, openWideIndex;
            if (side == Side.Left)
            {
                closeIndex = ManuelBastioniBlendShapes.EyelidLeftClose;
                openWideIndex = ManuelBastioniBlendShapes.EyelidLeftOpenWide;
            }
            else
            {
                closeIndex = ManuelBastioniBlendShapes.EyelidRightClose;
                openWideIndex = ManuelBastioniBlendShapes.EyelidRightOpenWide;
            }
            ApplyWeight((intensity > 0) ? closeIndex : openWideIndex, Mathf.Abs(intensity));
            ApplyWeight((intensity > 0) ? openWideIndex : closeIndex, -Mathf.Abs(intensity));
        }

        #endregion
    }

}