using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Poker.Models
{
    public class DropDowns
    {
        public List<DropDownList> PlayerOneCardValues { get; set; }
        public List<DropDownList> PlayerTwoCardValues { get; set; }
        public List<DropDownList> PlayerOneSuits { get; set; }
        public List<DropDownList> PlayerTwoSuits { get; set; }
    }
}