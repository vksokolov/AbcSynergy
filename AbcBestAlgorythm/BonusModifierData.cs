using System.Collections.Generic;

namespace AbcBestAlgorythm
{
    public class BonusModifierData
    {
        private int _charactersForMaxBonus;
        private readonly BonusModifierItem[] _bonusModifiers;
        
        public BonusModifierData(BonusModifierItem[] bonusModifiers)
        {
            _bonusModifiers = bonusModifiers;
            _charactersForMaxBonus = _bonusModifiers[^1].Characters;
        }
        
        public BonusModifierItem GetItem(int characters)
        {
            if (characters >= _charactersForMaxBonus)
                return _bonusModifiers[^1];
            if (characters < 2)
                return _bonusModifiers[0];
            
            for(int i=0;i<_bonusModifiers.Length;i++)
            {
                if (_bonusModifiers[i].Characters > characters)
                    return _bonusModifiers[i-1];
            }

            return _bonusModifiers[0];
        }

        public BonusModifierItem GetItemByLevel(int level)
        {
            if (level > _bonusModifiers.Length - 1)
                return _bonusModifiers[^1];
            
            return _bonusModifiers[level];
        }
    }

    public struct BonusModifierItem
    {
        public int Level;
        public int Characters;
        public float Modifier;
        public ModifierType ModifierType;
    }
    
    public enum ModifierType
    {
        Everybody,
        Dangerous,
        TraitOnly,
        HasEnergy,
    }
}