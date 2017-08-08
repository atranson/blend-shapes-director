namespace BlendShapesDirector.Examples
{

    /// <summary>Manages blend shapes associated to a specific SkinnedMeshRenderer for a simple cube with a few blend shapes</summary>
    public class CubeBlendShapesController : BlendShapesController
    {
        private PresetProvider presetProvider = null;

        public override PresetProvider GetPresetProvider()
        {
            if(presetProvider == null)
            {
                presetProvider = new CubePresetProvider();
            }
            return presetProvider;
        }
    }

}