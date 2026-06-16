using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class GrabbableLandmines : MEvent
    {
        public static bool Active = false;
        public static bool LandmineDisabled = false;
        public override string Name() => nameof(GrabbableLandmines);

        public static GrabbableLandmines Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 3;
            Descriptions = new List<string>() { "Some mines have turned into scrap...", "This was a wonderful idea!", "Beep! Beep! Beep!", "You can now sell some of the landmines." };
            ColorHex = "#FF0000";
            Type = EventType.Bad;

            ScaleList.Add(ScaleType.Rarity, new Scale(0.33f, 0.0066f, 0.33f, 1.0f));
            ScaleList.Add(ScaleType.MinAmount, new Scale(2.0f, 0.08f, 2.0f, 10.0f));
            ScaleList.Add(ScaleType.MaxAmount, new Scale(3.0f, 0.12f, 3.0f, 15.0f));
        }

        public override bool AddEventIfOnly() => Manager.HazardSpawnExists(Assets.ObjectName.Landmine);

        public override void Execute() {
            Active = true;
            LandmineDisabled = false;
            Manager.HazardSpawnSettings settings = new Manager.HazardSpawnSettings
            {
                numberToSpawn = new AnimationCurve(new Keyframe(0f, Get(ScaleType.MinAmount)), new Keyframe(1f, Get(ScaleType.MaxAmount))),
                spawnFacingWall = false,
                spawnFacingAwayFromWall = false,
                spawnWithBackToWall = false,
                spawnWithBackFlushAgainstWall = false,
                requireDistanceBetweenSpawns = false,
                disallowSpawningNearEntrances = false,
                allowInMineshaft = true
            };
            Manager.AddHazardSpawn(Assets.ObjectName.Landmine, settings);
        } 

        public override void OnShipLeave() {
            Active = false;
            LandmineDisabled = true;
        } 

        public override void OnGameStart()
        {
            Active = false;
            LandmineDisabled = false;
        }
    }
}
