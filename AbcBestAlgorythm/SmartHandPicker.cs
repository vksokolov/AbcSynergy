using System.Collections.Generic;
using System.Linq;

namespace AbcBestAlgorythm
{
    public static class SmartHandPicker
    {
        public static Deck PickBestHand(Deck deck, int count, Deck initialHand = null)
        {
            Deck hand;
            if (initialHand == null)
                hand = new Deck(deck.Characters.OrderByDescending(c => c.Might).Take(count).ToHashSet());
            else
                hand = new Deck(initialHand.Characters);
            
            return FindNearestBonuses(hand, deck, 5, 3);
        }

        private static Deck FindNearestBonuses(Deck hand, Deck deck, int steps, int depth)
        {
            var delta = 2;
            while (steps > 0 && delta < 3)
            {
                var characters = hand.Characters;
                List<Deck> newDecks = new List<Deck>(){hand};
                Dictionary<Class, int> charactersForNextLevelByClass = new Dictionary<Class, int>();
                Dictionary<Race, int> charactersForNextLevelByRace = new Dictionary<Race, int>();
                
                // Class
                Deck.AllClassValues
                    .ForEach(@class =>
                    {
                        int currentCharactersOfThisClass = hand.ClassCount[@class].CharactersInDeck;
                        if (ClassBonusModifier.TryGetNextLevel(currentCharactersOfThisClass, @class, out var item))
                        {
                            var foundDelta = item.Characters - currentCharactersOfThisClass;
                            if (foundDelta <= delta)
                                charactersForNextLevelByClass.Add(@class, foundDelta);
                        }
                    });
                foreach (var (@class, count) in charactersForNextLevelByClass)
                {
                    var replaced = ReplaceCharactersWithSpecialClass(hand, deck, @class, count);
                    if (depth > 1)
                        replaced = FindNearestBonuses(replaced, deck, steps, depth - 1);
                    newDecks.Add(replaced);
                }

                
                // Race
                Deck.AllRaceValues
                    .ForEach(race =>
                    {
                        int currentCharactersOfThisRace = hand.RaceCount[race].CharactersInDeck;
                        if (RaceBonusModifier.TryGetNextLevel(currentCharactersOfThisRace, race, out var item))
                        {
                            var foundDelta = item.Characters - currentCharactersOfThisRace;
                            if (foundDelta <= delta)
                                charactersForNextLevelByRace.Add(race, foundDelta);
                        }
                    });
                
                foreach (var (race, count) in charactersForNextLevelByRace)
                {
                    var replaced = ReplaceCharactersWithSpecialRace(hand, deck, race, count);
                    if (depth > 1)
                        replaced = FindNearestBonuses(replaced, deck, steps, depth - 1);
                    newDecks.Add(replaced);
                }
                
                var orderedByMight = newDecks.OrderByDescending(d => d.TotalMight);
                var newBestHand = orderedByMight.First();
                if (newBestHand == hand)
                {
                    delta++;
                    steps++;
                }
                else
                    hand = newBestHand;
                steps--;
            }

            return hand;
        }
        
        private static Deck ReplaceCharactersWithSpecialClass(Deck hand, Deck deck, Class targetClass, int count)
        {
            var newCharacters = 
                deck
                    .Characters
                    .Except(hand.Characters)
                    .Where(c => c.Class == targetClass)
                    .OrderByDescending(c => c.Might)
                    .Take(count);
            var charactersToReplace = 
                hand
                    .Characters
                    .Where(c => c.Class != targetClass)
                    .OrderBy(c => c.Might)
                    .Take(count);

            var newHand = hand
                .Characters
                .Except(charactersToReplace)
                .Concat(newCharacters)
                .OrderByDescending(c => c.Might)
                .ToHashSet();
            
            return new Deck(newHand);
        }
        
        private static Deck ReplaceCharactersWithSpecialRace(Deck hand, Deck deck, Race targetRace, int count)
        {
            var newCharacters = 
                deck
                    .Characters
                    .Except(hand.Characters)
                    .Where(c => c.Race == targetRace)
                    .OrderByDescending(c => c.Might)
                    .Take(count);
            var charactersToReplace = 
                hand
                    .Characters
                    .Where(c => c.Race != targetRace)
                    .OrderBy(c => c.Might)
                    .Take(count);

            var newHand = hand
                .Characters
                .Except(charactersToReplace)
                .Concat(newCharacters)
                .OrderByDescending(c => c.Might)
                .ToHashSet();
            
            return new Deck(newHand);
        }
    }
}