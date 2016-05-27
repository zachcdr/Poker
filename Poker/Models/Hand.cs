using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Poker.Models.Enums;

namespace Poker.Models
{
    public class Hand
    {
        public HandType Type { get; set; }
        public List<Card> Cards { get; set; }
        public List<int> Remaining { get; set; }
        public List<Matches> Matches { get; set; }
    }
}