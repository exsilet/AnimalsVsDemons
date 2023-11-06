using System;
using System.Collections.Generic;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Tower;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State HeroState;
        public WorldData WorldData;
        public int CurrentSoul;
        public int StartSouls = 100;
        public int NumberOfMinions = 1;
        public List<TowerStaticData> StaticData = new();

        public PlayerProgress(string initialLevel)
        {
            HeroState = new State();
            WorldData = new WorldData(initialLevel);
        }
    }
}