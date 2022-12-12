using System.Collections.Generic;

namespace AbcBestAlgorythm
{
    public class BonusModifierData
    {
        private int _charactersForMaxBonus;
        private BonusModifierItem[] _bonusModifiers;
        
        public BonusModifierData(BonusModifierItem[] bonusModifiers)
        {
            _charactersForMaxBonus = _bonusModifiers[^1].Characters;
            _bonusModifiers = bonusModifiers;
        }
    }

    public struct BonusModifierItem
    {
        public int Level;
        public int Characters;
        public float Bonus;
    }
}