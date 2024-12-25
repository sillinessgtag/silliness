using HarmonyLib;
using GorillaLocomotion;
using static silliness.Menu.Main;

namespace silliness.Patches
{
    [HarmonyPatch(typeof(Player))]
    [HarmonyPatch("GetSlidePercentage", MethodType.Normal)]
    internal class SlidePatch
    {
        private static void Postfix(Player __instance, ref float __result)
        {
            try
            {
                if (EverythingSlippery == true)
                    __result = 1;

                if (EverythingGrippy == true)
                    __result = 0;
            }
            catch { }
        }
    }
}