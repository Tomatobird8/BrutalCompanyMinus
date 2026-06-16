using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoTurrets : MEvent
    {
        public override string Name() => nameof(NoTurrets);

        public static NoTurrets Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "No turrets", "No more home security defense system.", "This planet is safe from GLaDOS's tyranny." };
            ColorHex = "#008000";
            Type = EventType.Remove;

            EventsToRemove = new List<string>() { nameof(Turrets), nameof(OutsideTurrets), nameof(Warzone), nameof(GrabbableTurrets), nameof(Hell), nameof(MobileTurrets) };
        }

        public override bool AddEventIfOnly() => Manager.HazardSpawnExists(Assets.ObjectName.Turret);

        public override void Execute()
        {
            Manager.RemoveHazardSpawn(Assets.ObjectName.Turret); 

            Manager.RemoveSpawn("WalkerTurret");
        }

    }
}
