using System.Collections.Generic;

namespace AbcBestAlgorythm
{
    public class ClassBonusModifier
    {
        public static (int CountForBonus, float Bonus) GetValue(int count, Class @class)
        {
            switch (@class)
            {
                case Class.Guardian:
                {
                    if (count >= 4) return (4, 1.25f);
                    if (count >= 2) return (2, 1.1f);
                    return (1, 1);
                }
                case Class.Healer:
                {
                    if (count >= 4) return (4, 1.22f);
                    if (count == 3) return (3, 1.16f);
                    if (count == 2) return (2, 1.11f);
                    return (1, 1);
                }
                case Class.Mage:
                {
                    if (count >= 4) return (4, 1.23f);
                    if (count >= 2) return (2, 1.09f);
                    return (1, 1);
                }
                case Class.Shaman:
                {
                    if (count >= 4) return (4, 1.52f);
                    if (count == 3) return (3, 1.26f);
                    if (count == 2) return (2, 1.12f);
                    return (1, 1);
                }
                case Class.Shooter:
                {
                    if (count >= 4) return (4, 1.23f);
                    if (count >= 2) return (2, 1.1f);
                    return (1, 1);
                }
                case Class.Slayer:
                {
                    if (count >= 4) return (4, 1.23f);
                    if (count >= 2) return (2, 1.1f);
                    return (1, 1);
                }
                case Class.Soaring:
                {
                    if (count >= 4) return (4, 1.4f);
                    if (count == 3) return (3, 1.27f);
                    if (count == 2) return (2, 1.2f);
                    return (1, 1);
                }
                case Class.Thief:
                {
                    if (count >= 4) return (4, 1.40f);
                    if (count >= 2) return (2, 1.23f);
                    return (1, 1);
                }
                case Class.Warlock:
                {
                    if (count >= 4) return (4, 1.12f);
                    if (count == 3) return (3, 1.075f);
                    if (count == 2) return (2, 1.05f);
                    return (1, 1);
                }
                case Class.Warrior:
                {
                    if (count >= 4) return (4, 1.23f);
                    if (count >= 2) return (2, 1.1f);
                    return (1, 1);
                }
                default:
                    return (1, 1);
            }
        }
    }
}