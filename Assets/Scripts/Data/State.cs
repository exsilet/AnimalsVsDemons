using System;

namespace Data
{
    [Serializable]
    public class State
    {
        public int GameLevel;
        public int Difficult;
        public int CurrentHP;
        public int MaxHP;
        public int SpellAmount;

        public int PriceLevel;
        public int PriceSpell;
        public int PriceNewMinions;
        public void ResetHP() => CurrentHP = MaxHP;

        public State()
        {
            GameLevel = 0;
            Difficult = 1;
            CurrentHP = 500;
            MaxHP = 500;
            SpellAmount = 2;

            PriceLevel = 250;
            PriceSpell = 100;
            PriceNewMinions = 200;
        }
    }
}