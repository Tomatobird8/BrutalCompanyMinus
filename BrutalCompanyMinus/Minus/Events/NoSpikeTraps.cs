using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class NoSpikeTraps : MEvent
    {
        public override string Name() => nameof(NoSpikeTraps);

        public static NoSpikeTraps Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "No spikes!", "No roof traps", "No hydraulic press" };
            ColorHex = "#008000";
            Type = EventType.Remove;

            EventsToRemove = new List<string>() { nameof(SpikeTraps), nameof(Hell) };
        }

        public override bool AddEventIfOnly() => Manager.HazardSpawnExists(Assets.ObjectName.SpikeRoofTrap);

        public override void Execute() => Manager.RemoveHazardSpawn(Assets.ObjectName.SpikeRoofTrap);

    }
}
