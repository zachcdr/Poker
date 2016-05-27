using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Poker.Models;
using Poker.Models.Enums;
using System.Text.RegularExpressions;

namespace Poker
{
    public partial class Poker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DropDowns dropsDowns = GetDropDowns();
            if (!Page.IsPostBack)
            {
                PopulateDdls(dropsDowns);
            }
        }

        public DropDowns GetDropDowns()
        {
            DropDowns dropDowns = new DropDowns();
            dropDowns.PlayerOneCardValues = new List<DropDownList>();
            dropDowns.PlayerOneCardValues.AddRange(new DropDownList[] { ddlPlayer1Card1Num, ddlPlayer1Card2Num, ddlPlayer1Card3Num, ddlPlayer1Card4Num, ddlPlayer1Card5Num });
            dropDowns.PlayerOneSuits = new List<DropDownList>();
            dropDowns.PlayerOneSuits.AddRange(new DropDownList[] { ddlPlayer1Card1Suit, ddlPlayer1Card2Suit, ddlPlayer1Card3Suit, ddlPlayer1Card4Suit, ddlPlayer1Card5Suit });
            dropDowns.PlayerTwoCardValues = new List<DropDownList>();
            dropDowns.PlayerTwoCardValues.AddRange(new DropDownList[] { ddlPlayer2Card1Num, ddlPlayer2Card2Num, ddlPlayer2Card3Num, ddlPlayer2Card4Num, ddlPlayer2Card5Num });
            dropDowns.PlayerTwoSuits = new List<DropDownList>();
            dropDowns.PlayerTwoSuits.AddRange(new DropDownList[] { ddlPlayer2Card1Suit, ddlPlayer2Card2Suit, ddlPlayer2Card3Suit, ddlPlayer2Card4Suit, ddlPlayer2Card5Suit });
            return dropDowns;
        }


        protected void btnWhoWonClick(object sender, EventArgs e)
        {
            pnlWinner.Visible = false;
            string playerOne = tbP1Name.Text;
            string playerTwo = tbP2Name.Text;
            if (!ValidateNames(playerOne, playerTwo))
            {
                DisplayErrorMessage("You have entered an invalid Name(s).");
                return;
            }
            DropDowns dropDowns = GetDropDowns();
            if (!ValidateDropDowns(dropDowns))
            {
                DisplayErrorMessage("You have not selected a value for a Card(s).");
                return;
            }

            List<Player> players = BuildPlayers(playerOne, playerTwo, dropDowns);

            if (!CheckForDuplicates(players))
            {
                DisplayErrorMessage("Duplicate cards were selected.  No cheating allowed!");
                return;
            }
            litErrorMessage.Visible = false;
            SetPlayerHands(players);
            DetermineWinner(players);
            pnlWinner.Visible = true;
            List<string> winners = players.Where(player => player.Winner).Select(winner => winner.Name).ToList();
            if (!winners.Any())
            {
                lblWinner.Text = "Looks like we have a TIE!";
            }
            else
            {
                lblWinner.Text = winners[0];
            }
        }

        public bool CheckForDuplicates(List<Player> players)
        {
            foreach (Player player in players)
            {
                var duplicates = player.Hand.Cards.GroupBy(x => new { x.Number, x.Suit })
                   .Where(x => x.Skip(1).Any()).ToArray();
                if (duplicates.Any())
                {
                    return false;
                }
            }
            if (players[0].Hand.Cards.Any(card => players[1].Hand.Cards.Any(second => second.Number == card.Number && second.Suit == card.Suit)))
            {
                return false;
            }
            return true;
        }

        public List<Player> BuildPlayers(string playerOne, string playerTwo, DropDowns dropDowns)
        {
            List<Player> players = new List<Player>();
            players.Add(AddPlayer(playerOne, dropDowns.PlayerOneCardValues.Select(list => list.SelectedValue).ToList(), dropDowns.PlayerOneSuits.Select(list => list.SelectedValue).ToList()));
            players.Add(AddPlayer(playerTwo, dropDowns.PlayerTwoCardValues.Select(list => list.SelectedValue).ToList(), dropDowns.PlayerTwoSuits.Select(list => list.SelectedValue).ToList()));
            return players;
        }

        public Player AddPlayer(string playerName, List<string> cardValues, List<string> cardSuits)
        {
            Player player = new Player();
            player.Name = playerName;
            player.Hand = BuildHand(cardValues, cardSuits);
            return player;
        }

        public Hand BuildHand(List<string> cardValues, List<string> cardSuits)
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>();
            for (int i = 0; i < 5; i++)
            {
                hand.Cards.Add(AddCard(cardValues[i], cardSuits[i]));
            }
            return hand;
        }

        public Card AddCard(string cardValue, string cardSuit)
        {
            Card card = new Card();
            card.Number = Convert.ToInt32(cardValue);
            card.Suit = GetSuit(cardSuit);
            return card;
        }

        public Suit GetSuit(string cardSuit)
        {
            Suit suit = new Suit();
            switch (cardSuit)
            {
                case "spade":
                    suit = Suit.Spade;
                    break;
                case "heart":
                    suit = Suit.Heart;
                    break;
                case "club":
                    suit = Suit.Club;
                    break;
                case "diamond":
                    suit = Suit.Diamond;
                    break;
            }
            return suit;
        }

        public bool ValidateNames(string playerOne, string playerTwo)
        {
            if (!String.IsNullOrWhiteSpace(playerOne)
                && playerOne.Length <= 25
                && !String.IsNullOrWhiteSpace(playerTwo)
                && playerTwo.Length <= 25)
            {
                Match playerOneMatch = Regex.Match(playerOne, "^[a-zA-Z0-9]+$");
                Match playerTwoMatch = Regex.Match(playerTwo, "^[a-zA-Z0-9]+$");
                if (playerOneMatch.Success && playerTwoMatch.Success)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValidateDropDowns(DropDowns dropDowns)
        {
            if (dropDowns.PlayerOneCardValues.Any(dropDown => dropDown.SelectedValue == "-1"))
            {
                return false;
            }
            if (dropDowns.PlayerOneSuits.Any(dropDown => dropDown.SelectedValue == "-1"))
            {
                return false;
            }
            if (dropDowns.PlayerTwoCardValues.Any(dropDown => dropDown.SelectedValue == "-1"))
            {
                return false;
            }
            if (dropDowns.PlayerTwoSuits.Any(dropDown => dropDown.SelectedValue == "-1"))
            {
                return false;
            }
            return true;
        }

        public void DisplayErrorMessage(string message)
        {
            litErrorMessage.Text = message;
            litErrorMessage.Visible = true;
        }

        public void SetPlayerHands(List<Player> players)
        {
            foreach (Player player in players)
            {
                List<Card> cards = player.Hand.Cards;
                List<int> cardValues = cards.Select(card => card.Number).ToList();
                player.Hand.Matches = new List<Matches>();
                player.Hand.Remaining = new List<int>();
                var groupedByNumber = cards.GroupBy(card => card.Number).Select(x => new { Number = x.Key, Count = x.Distinct().Count() }).ToList();
                foreach (var match in groupedByNumber)
                {
                    if (match.Count > 1)
                    {
                        player.Hand.Matches.Add(new Matches()
                        {
                            Number = match.Number,
                            Total = match.Count
                        });
                    }
                    else
                    {
                        player.Hand.Remaining.Add(match.Number);
                    }
                }
                List<int> matchValues = player.Hand.Matches.Select(match => match.Number).ToList();
                switch (player.Hand.Matches.Count)
                {
                    case 0:
                        bool isFlush = IsFlush(cards);
                        bool isStraight = IsStraight(cards);
                        if (isFlush && isStraight)
                        {
                            player.Hand.Type = HandType.StraighFlush;
                        }
                        else if (isStraight)
                        {
                            player.Hand.Type = HandType.Straight;
                        }
                        else if (isFlush)
                        {
                            player.Hand.Type = HandType.Flush;
                        }
                        else
                        {
                            player.Hand.Type = HandType.HighCard;
                        }
                        player.Hand.Remaining = cardValues;
                        break;
                    case 1:
                        if (player.Hand.Matches.Any(match => match.Total == 4))
                        {
                            player.Hand.Type = HandType.FourOfAKind;
                        }
                        else
                        {
                            player.Hand.Type = HandType.Pair;
                        }
                        break;
                    case 2:
                        if (player.Hand.Matches.Any(match => match.Total == 3))
                        {
                            player.Hand.Type = HandType.FullHouse;
                        }
                        else
                        {
                            player.Hand.Type = HandType.TwoPair;
                        }
                        break;
                }
            }
        }

        public bool IsFlush(List<Card> cards)
        {
            if (cards.All(card => card.Suit == Suit.Spade)
            || cards.All(card => card.Suit == Suit.Heart)
            || cards.All(card => card.Suit == Suit.Club)
            || cards.All(card => card.Suit == Suit.Diamond))
            {
                return true;
            }
            return false;
        }

        public bool IsStraight(List<Card> cards)
        {
            List<int> orderedCards = cards.Select(card => card.Number).OrderByDescending(number => number).ToList();
            for (int i = 0; i < orderedCards.Count - 1; i++)
            {
                if (orderedCards[i] - orderedCards[i + 1] != 1)
                {
                    return false;
                }
            }
            return true;
        }

        public void PopulateDdls(DropDowns dropDowns)
        {
            foreach (DropDownList list in dropDowns.PlayerOneCardValues)
            {
                AddFaceValue(list);
            }
            foreach (DropDownList list in dropDowns.PlayerTwoCardValues)
            {
                AddFaceValue(list);
            }
            foreach (DropDownList list in dropDowns.PlayerOneSuits)
            {
                AddSuit(list);
            }
            foreach (DropDownList list in dropDowns.PlayerTwoSuits)
            {
                AddSuit(list);
            }
        }

        public void AddFaceValue(DropDownList list)
        {
            for (int i = 2; i <= 14; i++)
            {
                switch (i)
                {
                    case 14:
                        list.Items.Add(new ListItem("ACE", i.ToString()));
                        break;
                    case 13:
                        list.Items.Add(new ListItem("KING", i.ToString()));
                        break;
                    case 12:
                        list.Items.Add(new ListItem("QUEEN", i.ToString()));
                        break;
                    case 11:
                        list.Items.Add(new ListItem("JACK", i.ToString()));
                        break;
                    default:
                        list.Items.Add(new ListItem(i.ToString(), i.ToString()));
                        break;
                }
            }
        }

        public void AddSuit(DropDownList list)
        {
            list.Items.Add(new ListItem("SPADES", "spade"));
            list.Items.Add(new ListItem("HEARTS", "heart"));
            list.Items.Add(new ListItem("DIAMONDS", "diamond"));
            list.Items.Add(new ListItem("CLUBS", "club"));
        }

        public void DetermineWinner(List<Player> players)
        {
            if (players[0].Hand.Type != players[1].Hand.Type)
            {
                if (players[0].Hand.Type > players[1].Hand.Type)
                {
                    players[0].Winner = true;
                }
                else
                {
                    players[1].Winner = true;
                }
            }
            else if (players[0].Hand.Type == HandType.FullHouse)
            {
                List<int> playerOneHigh = players[0].Hand.Matches.Where(match => match.Total == 3).Select(full => full.Number).ToList();
                List<int> playerTwoHigh = players[1].Hand.Matches.Where(match => match.Total == 3).Select(full => full.Number).ToList();
                if (playerOneHigh[0] > playerTwoHigh[0])
                {
                    players[0].Winner = true;
                }
                else
                {
                    players[1].Winner = true;
                }
            }
            else
            {
                List<int> playerOneHighest = players[0].Hand.Matches.Select(match => match.Number).OrderByDescending(val => val).ToList();
                List<int> playerTwoHighest = players[1].Hand.Matches.Select(match => match.Number).OrderByDescending(val => val).ToList();
                bool continueHighCardLookup = TieBreaker(players, playerOneHighest, playerTwoHighest);
                if (continueHighCardLookup)
                {
                    playerOneHighest = players[0].Hand.Remaining.OrderByDescending(value => value).ToList();
                    playerTwoHighest = players[1].Hand.Remaining.OrderByDescending(value => value).ToList();
                    TieBreaker(players, playerOneHighest, playerTwoHighest);
                }
            }
        }

        public bool TieBreaker(List<Player> players, List<int> playerOneTieBreaker, List<int> playerTwoTiewBreaker)
        {
            bool continueHighCardLookup = true;
            for (int i = 0; i < playerOneTieBreaker.Count(); i++)
            {
                if (playerOneTieBreaker[i] != playerTwoTiewBreaker[i])
                {
                    if (playerOneTieBreaker[i] > playerTwoTiewBreaker[i])
                    {
                        players[0].Winner = true;
                    }
                    else
                    {
                        players[1].Winner = true;
                    }
                    continueHighCardLookup = false;
                    break;
                }
            }
            return continueHighCardLookup;
        }
    }
} 