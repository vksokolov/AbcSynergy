using System;
using System.Collections.Generic;

namespace AbcBestAlgorythm
{
    class Program
    {
        static void Main(string[] args)
        {
            var handSize = 8;
            
            var deck = Deck.CreateRandom(200, 100, 4000);
            var bestCalculatedHand = Deck.GetBestHand(deck, handSize);
            var bestCharactersHand = Deck.TakeBest(deck, handSize);
            
            Console.WriteLine("Best deck:");
            Console.WriteLine(deck);
            
            Console.WriteLine("Best hand:");
            Console.WriteLine(bestCalculatedHand.ToStringWithBonuses());
            
            Console.WriteLine("Best characters:");
            Console.WriteLine(bestCharactersHand.ToStringWithBonuses());
        }
    }
}