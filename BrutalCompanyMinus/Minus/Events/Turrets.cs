using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class Turrets : MEvent
    {
        public override string Name() => nameof(Turrets);

        public static Turrets Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "Turrets!!", "Home defense systems.", "Panic and scream!", "+Turrets" };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            ScaleList.Add(ScaleType.MinAmount, new Scale(3.0f, 0.12f, 3.0f, 15.0f));
            ScaleList.Add(ScaleType.MaxAmount, new Scale(4.0f, 0.16f, 4.0f, 20.0f));
        }

        public override bool AddEventIfOnly() => Manager.HazardSpawnExists(Assets.ObjectName.Turret);

        public override void Execute()
        {
            Manager.HazardSpawnSettings settings = new Manager.HazardSpawnSettings
            {
                numberToSpawn = new AnimationCurve(new Keyframe(0f, Get(ScaleType.MinAmount)), new Keyframe(1f, Get(ScaleType.MaxAmount))),
                spawnFacingWall = true,
                spawnFacingAwayFromWall = false,
                spawnWithBackToWall = false,
                spawnWithBackFlushAgainstWall = false,
                requireDistanceBetweenSpawns = false,
                disallowSpawningNearEntrances = false,
                allowInMineshaft = true
            };
            Manager.AddHazardSpawn(Assets.ObjectName.Turret, settings);
        }
    }
}
