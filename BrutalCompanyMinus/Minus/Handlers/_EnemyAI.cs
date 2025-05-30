using GameNetcodeStuff;
using HarmonyLib;
using System.Collections;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Handlers
{
    [HarmonyPatch]
    [HarmonyPatch(typeof(EnemyAI))]
    internal class _EnemyAI
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(EnemyAI.MeetsStandardPlayerCollisionConditions))]
        private static void OnMeetsStandardPlayerCollisionConditions(ref PlayerControllerB __result, ref Collider other, ref EnemyType ___enemyType, ref bool ___isEnemyDead, ref bool inKillAnimation, ref float ___stunNormalizedTimer) // This fix works, maybe theres a better way
        {
            // Why am i doing this again? just gona leave it here since nothing breaks.
            PlayerControllerB controller = other.gameObject.GetComponent<PlayerControllerB>();
            if (controller != null)
            {
                if (!___isEnemyDead && ___stunNormalizedTimer < 0.0f && !inKillAnimation && __result == null) // (This may have some unintended consequences)
                {
                    if (controller.actualClientId == GameNetworkManager.Instance.localPlayerController.actualClientId) __result = controller;
                }
            }
        }

        private static IEnumerator UpdateHP(EnemyAI __instance)
        {
            yield return new WaitUntil(() => Net.Instance.receivedSyncedValues);
            __instance.enemyHP = (int)Mathf.Clamp(__instance.enemyHP + Manager.bonusEnemyHp, 1.1f, 99999999.0f);
        }
    }
}
