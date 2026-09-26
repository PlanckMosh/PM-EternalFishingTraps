using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;

namespace PMEternalFishTraps
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class TRPMFishTraps : BaseUnityPlugin
    {
        public const string modGUID = "com.planckmosh.efishtrap";
        public const string modName = "Eternal Fish Traps";
        public const string modVersion = "1.0.0";

        internal static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger; // Idk why but when I couldn't get things to work, putting this in made it work. Load-bearing coconut DO NOT REMOVE . .  I think this is supposed to 
                          //equate the BepInEx tool to the word Log??
            Log.LogInfo($"{modName} v{modVersion} initializing. . .");

            Harmony harmony = new Harmony(modGUID);
            try // Using a try-catch felt better to me and my eyes after having all those 'if's down there
            {
                harmony.PatchAll();
                Log.LogInfo($"{modName} loaded :3");
            }
            catch (Exception ex)
            {
                Log.LogError($"Failed to patch {modName}, sorry :( " + ex.Message); // If the mod DOES mess up this should apologize to you and give you a possible reason why
            }
        }
    }
    [HarmonyPatch(typeof(Nasa))]
    [HarmonyPatch("Start")]
    public static class ETInitPatch // Patching through on waking up, loading a relevant area, or loading a save
    {
        [HarmonyPostfix]
        public static void Postfix(Nasa __instance) //When the game loads an instance of Nasa (fish trap), it does these things
        {
            if (__instance == null) return;
            __instance.remainingUses = 99999; // Should set trap uses count to a huge number, basically infinite

            if (__instance.remainingUsesSlot != null) 
            {
                for (int i = 0; i < __instance.remainingUsesSlot.Length; i++)
                {
                    if (__instance.remainingUsesSlot[i] != null)
                    {
                        __instance.remainingUsesSlot[i].SetActive(false); // Tries to turn off the dots if there are any shown, probably doesn't actually work
                    }
                }
                __instance.remainingUsesSlot = new UnityEngine.GameObject[0]; // This is supposed to be my solution to the dots still displaying after patching the uses. I hope this doesn't cause an exception!!
            }
        }
    }
}
