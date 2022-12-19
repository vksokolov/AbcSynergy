using System;
using System.Linq;

namespace AbcBestAlgorythm
{
    public class Character
    {
        public Class Class;
        public Race Race;
        public int Might;
        public bool HasMana;
        
        public override string ToString()
        {
            var classString = Class.ToString();
            while (classString.Length < 8)
                classString += " ";
            
            var raceString = Race.ToString();
            while (raceString.Length < 8)
                raceString += " ";
            return $"{classString} \t{raceString} \tMight: {Might}\tHasMana: {HasMana}";
        }

        public int GetActualMight(Deck parentDeck)
        {
            var might = Might;
            var character = this;

            Deck.AllRaceValues.ForEach(race =>
            {
                var raceModifierValue = 1f;
                var raceModifier = RaceBonusModifier.GetValue(parentDeck.RaceCount[race].CharactersForMaxBonus, race);

                switch (raceModifier.ModifierType)
                {
                    case ModifierType.Everybody:
                        raceModifierValue = raceModifier.Modifier;
                        break;
                    case ModifierType.Dangerous:
                        if (character.Equals(parentDeck.MostDangerous))
                            raceModifierValue = raceModifier.Modifier;
                        break;
                    case ModifierType.TraitOnly:
                        if (character.Race == race)
                            raceModifierValue = raceModifier.Modifier;
                        break;
                    case ModifierType.HasEnergy:
                        if (character.HasMana)
                            raceModifierValue = raceModifier.Modifier;
                        break;
                }

                might = (int) (might * raceModifierValue);
            });


            Deck.AllClassValues.ForEach(@class =>
            {
                var classModifierValue = 1f;
                var classModifier = ClassBonusModifier.GetValue(parentDeck.ClassCount[@class].CharactersForMaxBonus, @class);

                switch (classModifier.ModifierType)
                {
                    case ModifierType.Everybody:
                        classModifierValue = classModifier.Modifier;
                        break;
                    case ModifierType.Dangerous:
                        if (character.Equals(parentDeck.MostDangerous))
                            classModifierValue = classModifier.Modifier;
                        break;
                    case ModifierType.TraitOnly:
                        if (character.Class == @class)
                            classModifierValue = classModifier.Modifier;
                        break;
                    case ModifierType.HasEnergy:
                        if (character.HasMana)
                            classModifierValue = classModifier.Modifier;
                        break;
                }

                might = (int) (might * classModifierValue);
            });
            return might;
        }
    }
}