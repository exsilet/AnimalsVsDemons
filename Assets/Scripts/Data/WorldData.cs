using System;

namespace Data
{
    [Serializable]
    public class WorldData
    {
        public string Level;

        public WorldData(string level)
        {
            Level = level;
        }
    }
}