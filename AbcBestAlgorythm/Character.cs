using System;
using System.Linq;

namespace AbcBestAlgorythm
{
    public class Character
    {
        private Deck _parentDeck;
        
        public Class Class;
        public Race Race;
        public int Might;
        public override string ToString()
        {
            var classString = Class.ToString();
            while (classString.Length < 8)
                classString += " ";
            
            var raceString = Race.ToString();
            while (raceString.Length < 8)
                raceString += " ";
            return $"{classString} \t{raceString} \tMight: {Might}";
        }
    }
}