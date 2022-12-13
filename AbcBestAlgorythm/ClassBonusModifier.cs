using System.Collections.Generic;

namespace AbcBestAlgorythm
{
    public class ClassBonusModifier
    {
        private static Dictionary<Class, BonusModifierData> _classBonusModifiers;

        public static Dictionary<Class, BonusModifierData> ClassBonusModifiers
        {
            get
            {
                if (_classBonusModifiers == null)
                {
                    _classBonusModifiers = new Dictionary<Class, BonusModifierData>
                    {
                        {Class.Guardian, new BonusModifierData(new[]
                            {
                                new BonusModifierItem {Characters = 0, Level = 0, Modifier = 1f},
                                new BonusModifierItem {Characters = 2, Level = 1, Modifier = 1.1f},
                                new BonusModifierItem {Characters = 4, Level = 2, Modifier = 1.25f},
                            })}, {Class.Healer, new BonusModifierData(new[]
                            {
                                new BonusModifierItem {Characters = 0, Level = 0, Modifier = 1f},
                                new BonusModifierItem {Characters = 2, Level = 1, Modifier = 1.11f},
                                new BonusModifierItem {Characters = 3, Level = 2, Modifier = 1.16f},
                                new BonusModifierItem {Characters = 4, Level = 3, Modifier = 1.22f},
                            })}, {Class.Mage, new BonusModifierData(new[]
                            {
                                new BonusModifierItem {Characters = 0, Level = 0, Modifier = 1f},
                                new BonusModifierItem {Characters = 2, Level = 1, Modifier = 1.09f},
                                new BonusModifierItem {Characters = 4, Level = 2, Modifier = 1.23f},
                            })}, {Class.Shaman, new BonusModifierData(new[]
                            {
                                new BonusModifierItem {Characters = 0, Level = 0, Modifier = 1f},
                                new BonusModifierItem {Characters = 2, Level = 1, Modifier = 1.12f},
                                new BonusModifierItem {Characters = 3, Level = 2, Modifier = 1.26f},
                                new BonusModifierItem {Characters = 4, Level = 3, Modifier = 1.52f},
                            })}, {Class.Shooter, new BonusModifierData(new[]
                            {
                                new BonusModifierItem {Characters = 0, Level = 0, Modifier = 1f},
                                new BonusModifierItem {Characters = 2, Level = 1, Modifier = 1.1f},
                                new BonusModifierItem {Characters = 4, Level = 2, Modifier = 1.23f},
                            })}, {Class.Slayer, new BonusModifierData(new[]
                            {
                                new BonusModifierItem {Characters = 0, Level = 0, Modifier = 1f},
                                new BonusModifierItem {Characters = 2, Level = 1, Modifier = 1.1f},
                                new BonusModifierItem {Characters = 4, Level = 2, Modifier = 1.23f},
                            })}, {Class.Soaring, new BonusModifierData(new[]
                            {
                                new BonusModifierItem {Characters = 0, Level = 0, Modifier = 1f},
                                new BonusModifierItem {Characters = 2, Level = 1, Modifier = 1.2f},
                                new BonusModifierItem {Characters = 3, Level = 2, Modifier = 1.27f},
                                new BonusModifierItem {Characters = 4, Level = 3, Modifier = 1.4f},
                            })}, {Class.Thief, new BonusModifierData(new[]
                            {
                                new BonusModifierItem {Characters = 0, Level = 0, Modifier = 1f},
                                new BonusModifierItem {Characters = 2, Level = 1, Modifier = 1.23f},
                                new BonusModifierItem {Characters = 4, Level = 2, Modifier = 1.4f},
                            })}, {Class.Warlock, new BonusModifierData(new[]
                            {
                                new BonusModifierItem {Characters = 0, Level = 0, Modifier = 1f},
                                new BonusModifierItem {Characters = 2, Level = 1, Modifier = 1.05f},
                                new BonusModifierItem {Characters = 3, Level = 2, Modifier = 1.075f},
                                new BonusModifierItem {Characters = 4, Level = 3, Modifier = 1.12f},
                            })}, {Class.Warrior, new BonusModifierData(new[]
                            {
                                new BonusModifierItem {Characters = 0, Level = 0, Modifier = 1f},
                                new BonusModifierItem {Characters = 2, Level = 1, Modifier = 1.1f},
                                new BonusModifierItem {Characters = 4, Level = 2, Modifier = 1.23f},
                            })
                        },
                    };
                }

                return _classBonusModifiers;
            }
        }

        public static BonusModifierItem GetValue(int count, Class @class)
        {
            var data = ClassBonusModifiers[@class];
            return data.GetItem(count);
        }

        public static bool TryGetNextLevel(int currentCharacterCount, Class @class, out BonusModifierItem item)
        {
            var data = ClassBonusModifiers[@class];
            var currentItem = data.GetItem(currentCharacterCount);
            var nextItem  = data.GetItemByLevel(currentItem.Level + 1);
            
            if (currentItem.Level == nextItem.Level)
            {
                item = currentItem;
                return false;
            }
            
            item = nextItem;
            return true;
        }
    }
}