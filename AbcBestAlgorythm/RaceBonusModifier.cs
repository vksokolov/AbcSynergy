namespace AbcBestAlgorythm
{
    public class RaceBonusModifier
    {
        public static (int CountForBonus, float Bonus) GetValue(int count, Race race)
        {
            switch (race)
            {
                case Race.Beast:
                {
                    if (count >= 4) return (4, 1.26f);
                    if (count == 3) return (3, 1.14f);
                    if (count == 2) return (2, 1.07f);
                    return (1, 1);
                }
                case Race.Demon:
                {
                    if (count >= 4) return (4, 1.22f);
                    if (count >= 2) return (2, 1.11f);
                    return (1, 1);
                }
                case Race.Drifter:
                {
                    if (count >= 4) return (4, 1.25f);
                    if (count == 3) return (3, 1.15f);
                    if (count == 2) return (2, 1.1f);
                    return (1, 1);
                }
                case Race.Elemental:
                {
                    if (count == 3) return (3, 1.25f);
                    return (1, 1);
                }
                case Race.Elf:
                {
                    if (count >= 3) return (3, 1.26f);
                    if (count == 2) return (2, 1.16f);
                    return (1, 1);
                }
                case Race.Empire:
                {
                    if (count >= 4) return (4, 1.22f);
                    if (count >= 2) return (2, 1.088f);
                    return (1, 1);
                }
                case Race.Gnome:
                {
                    if (count >= 4) return (4, 1.28f);
                    if (count >= 2) return (2, 1.14f);
                    return (1, 1);
                }
                case Race.Plant:
                {
                    if (count >= 3) return (3, 1.26f);
                    if (count == 2) return (2, 1.12f);
                    return (1, 1);
                }
                case Race.Reptile:
                {
                    if (count >= 4) return (4, 1.8f);
                    if (count >= 2) return (2, 1.2f);
                    return (1, 1);
                }
                case Race.Tribe:
                {
                    if (count >= 3) return (3, 1.7f);
                    if (count == 2) return (2, 1.4f);
                    return (1, 1);
                }
                case Race.Undead:
                {
                    if (count >= 4) return (4, 1.30f);
                    if (count >= 2) return (2, 1.125f);
                    return (1, 1);
                }
                default:
                    return (1, 1);
            }
        }
    }
}