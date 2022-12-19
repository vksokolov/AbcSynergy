using System.Collections.Generic;

namespace AbcBestAlgorythm
{
    public class RaceBonusModifier
    {
        public static readonly Dictionary<Race, BonusModifierData> RaceBonusModifiers =
            new Dictionary<Race, BonusModifierData>
            {
                {
                    Race.Beast, new BonusModifierData(new[]
                    {
                        new BonusModifierItem {Characters = 0, Level = 0, ModifierType = ModifierType.Everybody, Modifier = 1f},
                        new BonusModifierItem {Characters = 2, Level = 1, ModifierType = ModifierType.Everybody, Modifier = 1.07f},
                        new BonusModifierItem {Characters = 3, Level = 2, ModifierType = ModifierType.Everybody, Modifier = 1.14f},
                        new BonusModifierItem {Characters = 4, Level = 3, ModifierType = ModifierType.Everybody, Modifier = 1.26f},
                    })
                },
                {
                    Race.Demon, new BonusModifierData(new[]
                    {
                        new BonusModifierItem {Characters = 0, Level = 0, ModifierType = ModifierType.Everybody, Modifier = 1f},
                        new BonusModifierItem {Characters = 2, Level = 1, ModifierType = ModifierType.Everybody, Modifier = 1.11f},
                        new BonusModifierItem {Characters = 4, Level = 2, ModifierType = ModifierType.Everybody, Modifier = 1.22f},
                    })
                },
                {
                    Race.Drifter, new BonusModifierData(new[]
                    {
                        new BonusModifierItem {Characters = 0, Level = 0, ModifierType = ModifierType.Everybody, Modifier = 1f},
                        new BonusModifierItem {Characters = 2, Level = 1, ModifierType = ModifierType.Everybody, Modifier = 1.1f},
                        new BonusModifierItem {Characters = 3, Level = 2, ModifierType = ModifierType.Everybody, Modifier = 1.15f},
                        new BonusModifierItem {Characters = 4, Level = 3, ModifierType = ModifierType.Everybody, Modifier = 1.25f},
                    })
                },
                {
                    Race.Elemental, new BonusModifierData(new[]
                    {
                        new BonusModifierItem {Characters = 0, Level = 0, ModifierType = ModifierType.Everybody, Modifier = 1f},
                        new BonusModifierItem {Characters = 3, Level = 1, ModifierType = ModifierType.TraitOnly, Modifier = 1.25f},
                    })
                },
                {
                    Race.Elf, new BonusModifierData(new[]
                    {
                        new BonusModifierItem {Characters = 0, Level = 0, ModifierType = ModifierType.Everybody, Modifier = 1f},
                        new BonusModifierItem {Characters = 2, Level = 1, ModifierType = ModifierType.TraitOnly, Modifier = 1.16f},
                        new BonusModifierItem {Characters = 3, Level = 2, ModifierType = ModifierType.Everybody, Modifier = 1.26f},
                    })
                },
                {
                    Race.Empire, new BonusModifierData(new[]
                    {
                        new BonusModifierItem {Characters = 0, Level = 0, ModifierType = ModifierType.Everybody, Modifier = 1f},
                        new BonusModifierItem {Characters = 2, Level = 1, ModifierType = ModifierType.Everybody, Modifier = 1.088f},
                        new BonusModifierItem {Characters = 4, Level = 2, ModifierType = ModifierType.Everybody, Modifier = 1.22f},
                    })
                },
                {
                    Race.Gnome, new BonusModifierData(new[]
                    {
                        new BonusModifierItem {Characters = 0, Level = 0, ModifierType = ModifierType.Everybody, Modifier = 1f},
                        new BonusModifierItem {Characters = 2, Level = 1, ModifierType = ModifierType.TraitOnly, Modifier = 1.14f},
                        new BonusModifierItem {Characters = 4, Level = 2, ModifierType = ModifierType.Everybody, Modifier = 1.28f},
                    })
                },
                {
                    Race.Plant, new BonusModifierData(new[]
                    {
                        new BonusModifierItem {Characters = 0, Level = 0, ModifierType = ModifierType.Everybody, Modifier = 1f},
                        new BonusModifierItem {Characters = 2, Level = 1, ModifierType = ModifierType.Everybody, Modifier = 1.12f},
                        new BonusModifierItem {Characters = 3, Level = 2, ModifierType = ModifierType.Everybody, Modifier = 1.26f},
                    })
                },
                {
                    Race.Reptile, new BonusModifierData(new[]
                    {
                        new BonusModifierItem {Characters = 0, Level = 0, ModifierType = ModifierType.Everybody, Modifier = 1f},
                        new BonusModifierItem {Characters = 2, Level = 1, ModifierType = ModifierType.TraitOnly, Modifier = 1.2f},
                        new BonusModifierItem {Characters = 4, Level = 2, ModifierType = ModifierType.Everybody, Modifier = 1.8f},
                    })
                },
                {
                    Race.Tribe, new BonusModifierData(new[]
                    {
                        new BonusModifierItem {Characters = 0, Level = 0, ModifierType = ModifierType.Everybody, Modifier = 1f},
                        new BonusModifierItem {Characters = 2, Level = 1, ModifierType = ModifierType.Dangerous, Modifier = 1.4f},
                        new BonusModifierItem {Characters = 3, Level = 2, ModifierType = ModifierType.Dangerous, Modifier = 1.7f},
                    })
                },
                {
                    Race.Undead, new BonusModifierData(new[]
                    {
                        new BonusModifierItem {Characters = 0, Level = 0, ModifierType = ModifierType.Everybody, Modifier = 1f},
                        new BonusModifierItem {Characters = 2, Level = 1, ModifierType = ModifierType.Everybody, Modifier = 1.125f},
                        new BonusModifierItem {Characters = 4, Level = 2, ModifierType = ModifierType.Everybody, Modifier = 1.3f},
                    })
                },
            };
        
        public static BonusModifierItem GetValue(int count, Race race)
        {
            return RaceBonusModifiers[race].GetItem(count);
        }

        public static bool TryGetNextLevel(int currentCharacterCount, Race race, out BonusModifierItem item)
        {
            var data = RaceBonusModifiers[race];
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