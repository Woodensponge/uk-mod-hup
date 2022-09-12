using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using UMM;
using MonoMod.RuntimeDetour;

namespace HupMod
{
    [UKPlugin("Hup Mod", "1.0.0", "Jumping makes the \"Hup\" sound, regardless of UI or weapon position.", true, true)]
    public class HupMod : UKMod
    {
        public override void OnModLoaded()
        {
            On.OptionsMenuToManager.CheckEasterEgg += OptionsMenuToManager_CheckEasterEgg;
            On.NewMovement.Start += NewMovement_Start;
        }

        private void NewMovement_Start(On.NewMovement.orig_Start orig, NewMovement self)
        {
            orig(self);
            EnableQuakeJump();
        }

        public override void OnModUnload()
        {
            On.OptionsMenuToManager.CheckEasterEgg -= OptionsMenuToManager_CheckEasterEgg;

            //Check if the player meets the vanilla requirements for the easter egg.
            var nmov = MonoSingleton<NewMovement>.Instance;
            if (MonoSingleton<PrefsManager>.Instance.GetInt("weaponHoldPosition", 0) == 1 && MonoSingleton<PrefsManager>.Instance.GetInt("hudType", 0) >= 2)
            {
                nmov.quakeJump = true;
                return;
            }
            nmov.quakeJump = false;
        }

        private void OptionsMenuToManager_CheckEasterEgg(On.OptionsMenuToManager.orig_CheckEasterEgg orig, OptionsMenuToManager self)
        {
            orig(self);
            EnableQuakeJump();
        }

        void EnableQuakeJump()
        {
            UnityEngine.Debug.Log("ASS");
            var nmov = MonoSingleton<NewMovement>.Instance;
            nmov.quakeJump = true;
        }
    }
}
