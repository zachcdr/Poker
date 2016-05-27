using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poker.Models.Enums
{
    public enum HandType
    {
        StraighFlush = 9,
        FourOfAKind = 8,
        FullHouse = 7,
        Flush = 6,
        Straight = 5,
        ThreeOfAKind = 4,
        TwoPair = 3,
        Pair = 2,
        HighCard = 1
    }
}