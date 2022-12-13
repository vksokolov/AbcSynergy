using System;
using System.Collections.Generic;

namespace AbcBestAlgorythm
{
    class Program
    {
        static void Main(string[] args)
        {
            var handSize = 8;
            char command = 'g';
            Deck deck = null;
            Deck smartAlgoDeck = null;
            Deck highestBonusLevelHand = null;
            Deck topMightCharactersHand = null;
            Deck combinedAlgoHand = null;
            while (command != 'e')
            {
                switch (command)
                {
                    case 'g':
                    {
                        deck = Deck.CreateRandom(200, 90, 100);
                        highestBonusLevelHand = Deck.GetBestHand(deck, handSize);
                        topMightCharactersHand = Deck.TakeBest(deck, handSize);
                        smartAlgoDeck = SmartHandPicker.PickBestHand(deck, handSize);
                        combinedAlgoHand = SmartHandPicker.PickBestHand(deck, handSize, highestBonusLevelHand);
            
                        Console.WriteLine("Deck:");
                        Console.WriteLine(deck);
            
                        Console.WriteLine("Best hand:");
                        Console.WriteLine(highestBonusLevelHand.ToStringWithBonuses());
            
                        Console.WriteLine("Smart algo hand:");
                        Console.WriteLine(smartAlgoDeck.ToStringWithBonuses());
            
                        Console.WriteLine("Combined algo hand:");
                        Console.WriteLine(combinedAlgoHand.ToStringWithBonuses());
            
                        Console.WriteLine("Top might characters:");
                        Console.WriteLine(topMightCharactersHand.ToStringWithBonuses());
                        
                        command = Console.ReadKey().KeyChar;
                        break;
                    }
                    case 's':
                    {
                        Console.WriteLine("Best deck:");
                        Console.WriteLine(deck);
            
                        Console.WriteLine("Best hand:");
                        Console.WriteLine(highestBonusLevelHand.ToStringWithBonuses());
            
                        Console.WriteLine("Best characters:");
                        Console.WriteLine(topMightCharactersHand.ToStringWithBonuses());
                        
                        command = Console.ReadKey().KeyChar;
                        break;
                    }
                }
            }
            
        }
    }
}