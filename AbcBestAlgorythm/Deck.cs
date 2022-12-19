using System;
using System.Collections.Generic;
using System.Linq;

namespace AbcBestAlgorythm
{
    public class Deck
    {
        public static readonly List<Race> AllRaceValues = EnumExtensions.GetValues<Race>();
        public static readonly List<Class> AllClassValues = EnumExtensions.GetValues<Class>();

        public HashSet<Character> Characters { get; set; }
        
        public readonly Dictionary<Class, (int CharactersInDeck, int CharactersForMaxBonus)> ClassCount;
        public readonly Dictionary<Race, (int CharactersInDeck, int CharactersForMaxBonus)> RaceCount;
        public readonly float TotalMight;
        public Character MostDangerous;
        
        public Deck(IEnumerable<Character> characters)
        {
            Characters = characters.ToHashSet();
            ClassCount = GetMaxPotentialClassCount(Characters);
            RaceCount = GetMaxPotentialRaceCount(Characters);
            Characters = Characters
                    .OrderByDescending(x => 
                        x.Might * 
                        RaceBonusModifier.GetValue(RaceCount[x.Race].CharactersForMaxBonus, x.Race).Modifier *
                        ClassBonusModifier.GetValue(ClassCount[x.Class].CharactersForMaxBonus, x.Class).Modifier)
                    .ToHashSet();

            MostDangerous = Characters.OrderByDescending(x => x.Might).First();
            TotalMight = Characters.Sum(x => x.GetActualMight(this));
        }
        public static Deck GetBestHand(Deck deck, int count)
        {
            List<Deck> decks = new List<Deck>();
            
            /*
             * For each class we need to find the best hand with the minimum/maximum number of cards with max bonus
             */
            
            // Classes
            AllClassValues.ForEach(x =>
            {
                var characters = deck.GetBestCharactersOfClass(x, count);
                
                // Max cards with max bonus
                {
                    characters = deck.FillWithBest(characters, count);
                    var newDeck = new Deck(characters);
                    decks.Add(newDeck);
                }
                
                //Min cards with max bonus
                {
                    characters = deck.FillWithBest(characters.Take(deck.ClassCount[x].CharactersForMaxBonus).ToHashSet(), count);
                    var newDeck = new Deck(characters);
                    decks.Add(newDeck);
                }
            });
            
            // Races
            AllRaceValues.ForEach(x =>
            {
                var characters = deck.GetBestCharactersOfRace(x, count);
                
                // Max cards with max bonus
                {
                    characters = deck.FillWithBest(characters, count);
                    var newDeck = new Deck(characters);
                    decks.Add(newDeck);
                }
                
                //Min cards with max bonus
                {
                    characters = deck.FillWithBest(characters.Take(deck.RaceCount[x].CharactersForMaxBonus).ToHashSet(), count);
                    var newDeck = new Deck(characters);
                    decks.Add(newDeck);
                }
            });
            
            decks.Sort((x,y) => y.TotalMight.CompareTo(x.TotalMight));
            return decks.First();
        }
        
        private HashSet<Character> FillWithBest(HashSet<Character> currentHand, int count)
        {
            var cardRemainsCount = count - currentHand.Count;
            var cardRemains = Characters.Except(currentHand).ToHashSet().Take(cardRemainsCount);

            foreach (var cardRemain in cardRemains)
            {
                currentHand.Add(cardRemain);
            }
            
            return currentHand;
        }
        
        private HashSet<Character> GetBestCharactersOfClass(Class @class, int count)
        {
            var bestCards = Characters
                .Where(x => x.Class == @class)
                .Take(count)
                .ToHashSet();
            return bestCards;
        }
        
        private HashSet<Character> GetBestCharactersOfRace(Race race, int count)
        {
            var bestCards = Characters
                .Where(x => x.Race == race)
                .Take(count)
                .ToHashSet();
            return bestCards;
        }


        private static Dictionary<Race, (int CharactersInDeck, int CharactersForMaxBonus)> GetMaxPotentialRaceCount(HashSet<Character> deck)
        {
            var result = new Dictionary<Race, (int CharactersInDeck, int CharactersForMaxBonus)>();
            AllRaceValues.ForEach(x => result.Add(x, (0, 0)));

            foreach (var x in deck)
            {
                var newTotalValue = result[x.Race].CharactersInDeck + 1;
                var newMaxBonusValue = RaceBonusModifier.GetValue(newTotalValue, x.Race).Characters;
                result[x.Race] = (newTotalValue, newMaxBonusValue);
            }

            return result;
        }

        private static Dictionary<Class, (int CharactersInDeck, int CharactersForMaxBonus)> GetMaxPotentialClassCount(HashSet<Character> deck)
        {
            var result = new Dictionary<Class, (int CharactersInDeck, int CharactersForMaxBonus)>();
            AllClassValues.ForEach(x => result.Add(x, (0, 0)));
            foreach (var x in deck)
            {
                var newTotalValue = result[x.Class].CharactersInDeck + 1;
                var newMaxBonusValue = ClassBonusModifier.GetValue(newTotalValue, x.Class).Characters;
                result[x.Class] = (newTotalValue, newMaxBonusValue);
            }

            return result;
        }

        public static Deck CreateRandom(int totalCharacters, int minMight, int maxMight, bool distinctByClassAndRace = false, float mightDecreaseRatio = 1)
        {
            var totalRaces = Enum.GetValues(typeof(Race)).Length;
            var totalClasses = Enum.GetValues(typeof(Class)).Length;
            var random = new Random();
            var characters = new HashSet<Character>();
            for (var i = 0; i < totalCharacters; i++)
            {
                var character = new Character()
                {
                    Class = (Class) random.Next(0, totalClasses),
                    Race = (Race) random.Next(0, totalRaces),
                    Might = random.Next(minMight, maxMight),
                    HasMana = random.Next(0, 4) == 0,
                };
                characters.Add(character);
                
                maxMight = (int) (maxMight * mightDecreaseRatio);
                minMight = (int) (minMight * mightDecreaseRatio);
            }

            if (distinctByClassAndRace)
                characters = characters
                    .GroupBy(c => new {c.Class, c.Race})
                    .Select(g => g.First())
                    .ToHashSet();

            var deck = new Deck(characters);
            return deck;
        }

        public static Deck TakeBest(Deck deck, int count)
        {
            var characters = deck.Characters.OrderByDescending(c => c.Might).Take(count).ToHashSet();
            return new Deck(characters);
        }

        public override string ToString()
        {
            string log = $"Deck ({Characters.Count}) Total might: {TotalMight}\n";
            Characters
                .OrderByDescending(c => c.Might)
                .ToList()
                .ForEach(x =>
            {
                float potentialMight = x.GetActualMight(this);
                log += x +
                       $"   Actual Might: {potentialMight} ({(potentialMight / x.Might - 1) * 100:0.00}%)";
                if (x == MostDangerous)
                    log += "\t [Most Dangerous]";
                log += "\n";
            });
            return log;
        }

        public string ToStringWithBonuses()
        {
            var log = ToString();
            log += "Bonuses:\n";
            
            // Classes
            AllClassValues.ForEach(@class =>
            {
                int charactersInDeck = ClassCount[@class].CharactersInDeck;
                var data = ClassBonusModifier.GetValue(charactersInDeck, @class);
                if (data.Modifier > 1.00001f)
                {
                    var bonusPercent = (data.Modifier - 1) * 100;
                    log += $"{@class}({charactersInDeck}/{data.Characters}): {bonusPercent:0.00}% ({data.ModifierType})\n";
                }
            });
            
            // Races
            AllRaceValues.ForEach(race =>
            {
                int charactersInDeck = RaceCount[race].CharactersInDeck;
                var data = RaceBonusModifier.GetValue(charactersInDeck, race);
                if (data.Modifier > 1.00001f)
                {
                    var bonusPercent = (data.Modifier - 1) * 100;
                    log += $"{race}({charactersInDeck}/{data.Characters}): {bonusPercent:0.00}% ({data.ModifierType})\n";
                }
            });

            return log;
        }
    }
}