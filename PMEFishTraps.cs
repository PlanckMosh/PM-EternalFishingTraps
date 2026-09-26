using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;

namespace PMEternalFishTraps
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class TRPMFishTraps : BaseUnityPlugin
    {
        public const string ModGUID = "com.planckmosh.efishtrap";
        public const string ModName = "Eternal Fish Traps";
        public const string ModVersion = "1.0.0";

        internal static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;

            Log.LogInfo($"{ModName} v{ModVersion} initializing. . .");

            Harmony harmony = new Harmony(ModGUID);
            try
            {
                harmony.PatchAll();
                Log.LogInfo($"{ModName} loaded :3");
            }
            catch (Exception ex)
            {
                Log.LogError($"Failed to patch {ModName}, sorry :( " + ex.Message);
            }
        }
    }
    [HarmonyPatch(typeof(Nasa))]
    [HarmonyPatch("Start")]
    public static class ETInitPatch
    {
        [HarmonyPostfix]
        public static void Postfix(Nasa __instance)
        {
            if (__instance == null) return;
            __instance.remainingUses = 99999;

            if (__instance.remainingUsesSlot != null)
            {
                for (int i = 0; i < __instance.remainingUsesSlot.Length; i++)
                {
                    if (__instance.remainingUsesSlot[i] != null)
                    {
                        __instance.remainingUsesSlot[i].SetActive(false);
                    }
                }
                __instance.remainingUsesSlot = new UnityEngine.GameObject[0];
            }
        }
    }
}