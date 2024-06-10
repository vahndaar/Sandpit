using HarmonyLib;
using ProjectM;
using Unity.Collections;
using Sandpit.Tools;

namespace Sandpit.Hooks
{
    public static class OnInitialize
    {
        private static Harmony _harmony;

        public static bool HasInitialized { get; private set; } = false;

        public static void Initialize()
        {
            _harmony = Harmony.CreateAndPatchAll(typeof(Patch));
        }

        public static void Uninitialize()
        {
            _harmony.UnpatchSelf();
        }

        private static class Patch
        {

            [HarmonyPatch(typeof(ModifyUnitStatBuffSystem_Spawn), nameof(ModifyUnitStatBuffSystem_Spawn.OnUpdate))]
            [HarmonyPrefix]
            public static void Initialize(ModifyUnitStatBuffSystem_Spawn __instance)
            {
                var entities = __instance.__query_1735840491_0.ToEntityArray(Allocator.Temp);

                foreach (var entity in entities)
                {

                    var buffer = entity.ReadBuffer<ModifyUnitStatBuff_DOTS>();
                    UnitStatType statType = UnitStatType.DamageVsWood;
                    ModifyUnitStatBuff_DOTS newStatBuff = new()
                    {
                        StatType = statType,
                        ModificationType = ModificationType.AddToBase,
                        Value = 2.0f,
                        Modifier = 1,
                        IncreaseByStacks = false,
                        ValueByStacks = 0,
                        Priority = 0,
                        Id = ModificationId.Empty
                    };
                    buffer.Add(newStatBuff);
                }

            }

        }
    }
}
