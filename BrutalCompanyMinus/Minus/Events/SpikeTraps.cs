using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class SpikeTraps : MEvent
    {
        public override string Name() => nameof(SpikeTraps);

        public static SpikeTraps Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "Spikes!!!", "I recommend looking up.", "Hydraulic press!" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            ScaleList.Add(ScaleType.MinAmount, new Scale(3.0f, 0.09f, 3.0f, 12.0f));
            ScaleList.Add(ScaleType.MaxAmount, new Scale(4.0f, 0.12f, 4.0f, 16.0f));
        }

        public override bool AddEventIfOnly() => Manager.HazardSpawnExists(Assets.ObjectName.SpikeRoofTrap);

        public override void Execute()
        {
            Manager.HazardSpawnSettings settings = new Manager.HazardSpawnSettings
            {
                numberToSpawn = new AnimationCurve(new Keyframe(0f, Get(ScaleType.MinAmount)), new Keyframe(1f, Get(ScaleType.MaxAmount))),
                spawnFacingWall = true,
                spawnFacingAwayFromWall = true,
                spawnWithBackToWall = true,
                spawnWithBackFlushAgainstWall = true,
                requireDistanceBetweenSpawns = true,
                disallowSpawningNearEntrances = false,
                allowInMineshaft = true // evil
            };
            Manager.AddHazardSpawn(Assets.ObjectName.SpikeRoofTrap, settings);
        }
    }
}
