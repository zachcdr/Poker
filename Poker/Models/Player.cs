using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poker.Models
{
    public class Player
    {
        public string Name { get; set; }
        public Hand Hand { get; set; }
        public bool Winner { get; set; }
    }
}