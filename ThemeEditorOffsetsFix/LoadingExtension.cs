using ICities;

namespace ThemeEditorOffsetsFix
{
    public class LoadingExtension : LoadingExtensionBase
    {
        private static bool initialized;

        public override void OnReleased()
        {
            base.OnReleased();
            if (!initialized)
            {
                return;
            }
            TerrainPropertiesPanelDetour.Revert();
            initialized = false;
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            if (initialized)
            {
                return;
            }
            TerrainPropertiesPanelDetour.Deploy();
            TerrainPropertiesPanelDetour.Initialize();
            initialized = true;
        }
    }
}