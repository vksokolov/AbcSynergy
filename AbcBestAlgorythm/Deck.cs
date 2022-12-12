using System;
using System.Collections.Generic;
using System.Linq;

namespace AbcBestAlgorythm
{
    public class Deck
    {
        public static readonly List<Race> AllRaceValues = EnumExtensions.GetValues<Race>();
        public static readonly List<Class> AllClassValues = EnumExtensions.GetValues<Class>();

        private HashSet<Character> Characters { get; set; }
        
        public readonly Dictionary<Class, (int Total, int MaxBonus)> ClassCount;
        public readonly Dictionary<Race, (int Total, int MaxBonus)> RaceCount;
        private readonly float _totalMight;
        
        public Deck(HashSet<Character> characters)
        {
            Characters = characters;
            ClassCount = GetMaxPotentialClassCount(Characters);
            RaceCount = GetMaxPotentialRaceCount(Characters);
            Characters = Characters
                    .OrderByDescending(x => 
                        x.Might * 
                        RaceBonusModifier.GetValue(RaceCount[x.Race].MaxBonus, x.Race).Bonus *
                        ClassBonusModifier.GetValue(ClassCount[x.Class].MaxBonus, x.Class).CountForBonus)
                    .ToHashSet();
            
            _totalMight = Characters.Sum(x => x.Might);
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
                    characters = deck.FillWithBest(characters.Take(deck.ClassCount[x].MaxBonus).ToHashSet(), count);
                    var newDeck = new Deck(characters);
                    decks.Add(newDeck);
                }
            });
            
            // Races
            AllRaceValues.ForEach(x =>
            {
                var characters = deck.GetBestCharactersOfRace(x, count);
                
                // Max число карт
                {
                    characters = deck.FillWithBest(characters, count);
                    var newDeck = new Deck(characters);
                    decks.Add(newDeck);
                }
                
                //Min число карт
                {
                    characters = deck.FillWithBest(characters.Take(deck.RaceCount[x].MaxBonus).ToHashSet(), count);
                    var newDeck = new Deck(characters);
                    decks.Add(newDeck);
                }
            });
            
            decks.Sort((x,y) => y._totalMight.CompareTo(x._totalMight));
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

        private HashSet<Character> FillWithBestLookingForNextLevelBonus(HashSet<Character> currentHand, int count)
        {
            return null;
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


        private static Dictionary<Race, (int Total, int MaxBonus)> GetMaxPotentialRaceCount(HashSet<Character> deck)
        {
            var result = new Dictionary<Race, (int Total, int MaxBonus)>();
            AllRaceValues.ForEach(x => result.Add(x, (0, 0)));

            foreach (var x in deck)
            {
                var newTotalValue = result[x.Race].Total + 1;
                var newMaxBonusValue = RaceBonusModifier.GetValue(newTotalValue, x.Race).CountForBonus;
                result[x.Race] = (newTotalValue, newMaxBonusValue);
            }

            return result;
        }

        private static Dictionary<Class, (int Total, int MaxBonus)> GetMaxPotentialClassCount(HashSet<Character> deck)
        {
            var result = new Dictionary<Class, (int Total, int MaxBonus)>();
            AllClassValues.ForEach(x => result.Add(x, (0, 0)));
            foreach (var x in deck)
            {
                var newTotalValue = result[x.Class].Total + 1;
                var newMaxBonusValue = ClassBonusModifier.GetValue(newTotalValue, x.Class).CountForBonus;
                result[x.Class] = (newTotalValue, newMaxBonusValue);
            }

            return result;
        }

        public static Deck CreateRandom(int totalCharacters, int minMight, int maxMight)
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
                };
                characters.Add(character);
            }
            
            var deck = new Deck(characters);
            return deck;
        }

        public static Deck TakeBest(Deck deck, int count)
        {
            var characters = deck.Characters.Take(count).ToHashSet();
            return new Deck(characters);
        }

        public override string ToString()
        {
            string log = $"Deck ({Characters.Count}) Total might: {_totalMight}\n";
            Characters.ForEach(x =>
            {
                float potentialMight = x.Might * 
                                     ClassBonusModifier.GetValue(ClassCount[x.Class].Total, x.Class).Bonus *
                                     RaceBonusModifier.GetValue(RaceCount[x.Race].Total, x.Race).Bonus;
                log += x +
                       $"; Potential Might: {potentialMight}\n";
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
                int count = ClassCount[@class].Total;
                if (ClassBonusModifier.GetValue(count, @class).Bonus > 1)
                {
                    var pair = ClassBonusModifier.GetValue(count, @class);
                    var bonusPercent = (pair.Bonus - 1) * 100;
                    log += $"{@class}({count}/{pair.CountForBonus}): {bonusPercent:0.00}%\n";
                }
            });
            
            // Races
            AllRaceValues.ForEach(race =>
            {
                int count = RaceCount[race].Total;
                if (RaceBonusModifier.GetValue(count, race).Bonus > 1)
                {
                    var pair = RaceBonusModifier.GetValue(count, race);
                    var bonusPercent = (pair.Bonus - 1) * 100;
                    log += $"{(Class) race}({count}/{pair.CountForBonus}): {bonusPercent:0.00}%\n";
                }
            });

            return log;
        }
    }
}