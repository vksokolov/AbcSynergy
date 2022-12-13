using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AbcBestAlgorythm
{
    class Program
    {
        private const string HighestBonusLevelName = "Highest bonus level hand";
        private const string SmartAlgoName = "Smart algo hand";
        private const string CombinedAlgoName = "Combined algo hand";
        private const string TopMightDeckName = "Top might characters";

        private enum AlgoType
        {
            HighestBonusLevel,
            Smart,
            Combined,
            TopMight,
        }
        
        static void Main()
        {
            var handSize = 8;
            char command = 'g';
            DeckGenerationData deck = null;
            
            List<DeckGenerationData> hands = new List<DeckGenerationData>();
            
            while (command != 'e')
            {
                switch (command)
                {
                    case 'g':
                    {
                        deck = GenerateDeck(50, 50, 200, true);
                        hands.Clear();
                        hands.Add(GenerateHand(deck.Deck, AlgoType.HighestBonusLevel, handSize, HighestBonusLevelName));
                        hands.Add(GenerateHand(deck.Deck, AlgoType.Smart, handSize, SmartAlgoName));
                        hands.Add(GenerateHand(deck.Deck, AlgoType.Combined, handSize, CombinedAlgoName));
                        hands.Add(GenerateHand(deck.Deck, AlgoType.TopMight, handSize, TopMightDeckName));
                        
                        Print(deck, hands);
                        break;
                    }
                    case 's':
                    {
                        Print(deck, hands);
                        break;
                    }
                }
                        
                command = Console.ReadKey().KeyChar;
            }
        }

        private static DeckGenerationData GenerateDeck(int totalCharacters, int minMight, int maxMight,
            bool distinctByClassAndRace = false, float mightDecreaseRatio = 1)
        {
            return new DeckGenerationData()
            {
                GenerationTime = 0,
                Deck = Deck.CreateRandom(totalCharacters, minMight, maxMight, distinctByClassAndRace,
                    mightDecreaseRatio),
                DeckName = "Deck"
            };
        }

        private static DeckGenerationData GenerateHand(Deck deck, AlgoType type, int handSize, string handName = null)
        {
            Deck hand;
            Stopwatch watch = Stopwatch.StartNew();
            switch (type)
            {
                case AlgoType.HighestBonusLevel:
                {
                    hand = Deck.TakeBest(deck, handSize);
                    break;
                }
                case AlgoType.Smart:
                {
                    hand = SmartHandPicker.PickBestHand(deck, handSize);
                    break;
                }
                case AlgoType.Combined:
                {
                    var preHand = Deck.GetBestHand(deck, handSize);
                    hand = SmartHandPicker.PickBestHand(deck, handSize, preHand);
                    break;
                }
                case AlgoType.TopMight:
                {
                    hand = Deck.GetBestHand(deck, handSize);
                    break;
                }
                default:
                    throw new Exception("Algo not found");
            }
            
            watch.Stop();

            return new DeckGenerationData()
            {
                DeckName = handName,
                GenerationTime = watch.ElapsedMilliseconds,
                Deck = hand
            };
        }

        private static void Print(DeckGenerationData deck, List<DeckGenerationData> hands)
        {
            Console.WriteLine(deck.DeckName);
            Console.WriteLine(deck.Deck.ToString());
            
            hands.ForEach(Print);
        }
        
        private static void Print(DeckGenerationData data)
        {
            Console.WriteLine($"{data.DeckName} ({data.GenerationTime} ms):");
            Console.WriteLine(data.Deck.ToStringWithBonuses());
        }
    }
}