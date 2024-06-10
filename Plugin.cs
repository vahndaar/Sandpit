using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using Bloodstone.API;

namespace Sandpit
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency("gg.deca.VampireCommandFramework")]
    [BepInDependency("gg.deca.Bloodstone")]
    public class Plugin : BasePlugin
    {
        public static ManualLogSource Logger { get; private set; }

        internal static Plugin Instance { get; private set; }

        public Plugin() : base()
        {
            Plugin.Logger = Log;
            Instance = this;
        }

        public override void Load()
        {
            // Hooks
            if (VWorld.IsServer)
            {
                Hooks.OnInitialize.Initialize();
            }

            if (VWorld.IsClient)
            {
                return;
            }

        }

        public override bool Unload()
        {
            if (VWorld.IsServer)
            {
                Hooks.OnInitialize.Uninitialize();
            }

            return true;
        }

    }
}

