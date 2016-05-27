using System;
using System.Collections.Generic;
using Poker.Models;
using Poker.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Poker.Tests
{
    [TestClass]
    public class PokerTests
    {
        private static Poker _poker = new Poker();

        [TestMethod]
        public void AddPlayerTest()
        {
            string playerName = "Zach";
            List<string> cardValues = new List<string>(new string[] { "2", "2", "3", "8", "11" });
            List<string> cardSuit = new List<string>(new string[] { "heart", "club", "spade", "heart", "spade" });
            Player player = _poker.AddPlayer(playerName, cardValues, cardSuit);
            Assert.AreEqual("Zach", player.Name);
            Assert.AreEqual(2, player.Hand.Cards[0].Number);
            Assert.AreEqual(2, player.Hand.Cards[1].Number);
            Assert.AreEqual(3, player.Hand.Cards[2].Number);
            Assert.AreEqual(8, player.Hand.Cards[3].Number);
            Assert.AreEqual(11, player.Hand.Cards[4].Number);
            Assert.AreEqual(Suit.Heart, player.Hand.Cards[0].Suit);
            Assert.AreEqual(Suit.Club, player.Hand.Cards[1].Suit);
            Assert.AreEqual(Suit.Spade, player.Hand.Cards[2].Suit);
            Assert.AreEqual(Suit.Heart, player.Hand.Cards[3].Suit);
            Assert.AreEqual(Suit.Spade, player.Hand.Cards[4].Suit);
        }

        [TestMethod]
        public void BuildHandTest()
        {
            List<string> cardValues = new List<string>(new string[] { "8", "3", "14", "2", "9" });
            List<string> cardSuit = new List<string>(new string[] { "heart", "club", "spade", "heart", "spade" });
            Hand hand = _poker.BuildHand(cardValues, cardSuit);
            Assert.AreEqual(8, hand.Cards[0].Number);
            Assert.AreEqual(3, hand.Cards[1].Number);
            Assert.AreEqual(14, hand.Cards[2].Number);
            Assert.AreEqual(2, hand.Cards[3].Number);
            Assert.AreEqual(9, hand.Cards[4].Number);
            Assert.AreEqual(Suit.Heart, hand.Cards[0].Suit);
            Assert.AreEqual(Suit.Club, hand.Cards[1].Suit);
            Assert.AreEqual(Suit.Spade, hand.Cards[2].Suit);
            Assert.AreEqual(Suit.Heart, hand.Cards[3].Suit);
            Assert.AreEqual(Suit.Spade, hand.Cards[4].Suit);
        }

        [TestMethod]
        public void AddCardTest()
        {
            Card card = _poker.AddCard("7", "spade");
            Assert.AreEqual(7, card.Number);
            Assert.AreEqual(Suit.Spade, card.Suit);
        }

        [TestMethod]
        public void GetSuitTest()
        {
            Suit suit = _poker.GetSuit("heart");
            Assert.AreEqual(Suit.Heart, suit);
        }

        [TestMethod]
        public void CheckForDuplicatesTest01()
        {
            string playerOneName = "Zach";
            List<string> cardValuesP1 = new List<string>(new string[] { "2", "2", "3", "8", "11" });
            List<string> cardSuitP1 = new List<string>(new string[] { "heart", "heart", "spade", "heart", "spade" });

            string playerTwoName = "Hank";
            List<string> cardValuesP2 = new List<string>(new string[] { "2", "2", "7", "9", "12" });
            List<string> cardSuitP2 = new List<string>(new string[] { "diamond", "club", "spade", "heart", "spade" });

            List<Player> players = new List<Player>();
            players.Add(_poker.AddPlayer(playerOneName, cardValuesP1, cardSuitP1));
            players.Add(_poker.AddPlayer(playerTwoName, cardValuesP2, cardSuitP2));

            bool passDuplicateCheck = _poker.CheckForDuplicates(players);
            Assert.IsFalse(passDuplicateCheck);
        }

        [TestMethod]
        public void CheckForDuplicatesTest02()
        {
            string playerOneName = "Zach";
            List<string> cardValuesP1 = new List<string>(new string[] { "2", "2", "3", "8", "11" });
            List<string> cardSuitP1 = new List<string>(new string[] { "heart", "diamond", "spade", "heart", "spade" });

            string playerTwoName = "Hank";
            List<string> cardValuesP2 = new List<string>(new string[] { "2", "2", "7", "9", "12" });
            List<string> cardSuitP2 = new List<string>(new string[] { "heart", "club", "spade", "heart", "spade" });

            List<Player> players = new List<Player>();
            players.Add(_poker.AddPlayer(playerOneName, cardValuesP1, cardSuitP1));
            players.Add(_poker.AddPlayer(playerTwoName, cardValuesP2, cardSuitP2));

            bool passDuplicateCheck = _poker.CheckForDuplicates(players);
            Assert.IsFalse(passDuplicateCheck);
        }

        [TestMethod]
        public void CheckForDuplicatesTest03()
        {
            string playerOneName = "Zach";
            List<string> cardValuesP1 = new List<string>(new string[] { "2", "2", "3", "8", "11" });
            List<string> cardSuitP1 = new List<string>(new string[] { "heart", "spade", "spade", "heart", "spade" });

            string playerTwoName = "Hank";
            List<string> cardValuesP2 = new List<string>(new string[] { "2", "2", "7", "9", "12" });
            List<string> cardSuitP2 = new List<string>(new string[] { "diamond", "club", "spade", "heart", "spade" });

            List<Player> players = new List<Player>();
            players.Add(_poker.AddPlayer(playerOneName, cardValuesP1, cardSuitP1));
            players.Add(_poker.AddPlayer(playerTwoName, cardValuesP2, cardSuitP2));

            bool passDuplicateCheck = _poker.CheckForDuplicates(players);
            Assert.IsTrue(passDuplicateCheck);
        }

        [TestMethod]
        public void ValidateNamesTest01()
        {
            string nameOne = null;
            string nameTwo = "Zach";
            bool passNameValidation = _poker.ValidateNames(nameOne, nameTwo);
            Assert.IsFalse(passNameValidation);
        }

        [TestMethod]
        public void ValidateNamesTest02()
        {
            string nameOne = "Hank";
            string nameTwo = string.Empty;
            bool passNameValidation = _poker.ValidateNames(nameOne, nameTwo);
            Assert.IsFalse(passNameValidation);
        }

        [TestMethod]
        public void ValidateNamesTest03()
        {
            string nameOne = "twentysixcharacternamefail";
            string nameTwo = "Zach";
            bool passNameValidation = _poker.ValidateNames(nameOne, nameTwo);
            Assert.IsFalse(passNameValidation);
        }

        [TestMethod]
        public void ValidateNamesTest04()
        {
            string nameOne = "non-alphanumeric";
            string nameTwo = "Zach";
            bool passNameValidation = _poker.ValidateNames(nameOne, nameTwo);
            Assert.IsFalse(passNameValidation);
        }

        [TestMethod]
        public void ValidateNamesTest05()
        {
            string nameOne = "Player1";
            string nameTwo = "Player2";
            bool passNameValidation = _poker.ValidateNames(nameOne, nameTwo);
            Assert.IsTrue(passNameValidation);
        }

        [TestMethod]
        public void SetPlayerHandsTest01()
        {
            string playerOneName = "Zach";
            List<string> cardValuesP1 = new List<string>(new string[] { "2", "2", "3", "8", "11" });
            List<string> cardSuitP1 = new List<string>(new string[] { "heart", "spade", "spade", "heart", "spade" });

            string playerTwoName = "Hank";
            List<string> cardValuesP2 = new List<string>(new string[] { "2", "3", "4", "5", "6" });
            List<string> cardSuitP2 = new List<string>(new string[] { "diamond", "diamond", "diamond", "diamond", "diamond" });

            List<Player> players = new List<Player>();
            players.Add(_poker.AddPlayer(playerOneName, cardValuesP1, cardSuitP1));
            players.Add(_poker.AddPlayer(playerTwoName, cardValuesP2, cardSuitP2));

            _poker.SetPlayerHands(players);

            Assert.AreEqual(HandType.Pair, players[0].Hand.Type);
            Assert.AreEqual(1, players[0].Hand.Matches.Count);
            Assert.AreEqual(3, players[0].Hand.Remaining.Count);
            Assert.AreEqual(HandType.StraighFlush, players[1].Hand.Type);
            Assert.AreEqual(0, players[1].Hand.Matches.Count);
            Assert.AreEqual(5, players[1].Hand.Remaining.Count);
        }

        [TestMethod]
        public void SetPlayerHandsTest02()
        {
            string playerOneName = "Zach";
            List<string> cardValuesP1 = new List<string>(new string[] { "2", "2", "2", "3", "3" });
            List<string> cardSuitP1 = new List<string>(new string[] { "heart", "spade", "club", "heart", "spade" });

            string playerTwoName = "Hank";
            List<string> cardValuesP2 = new List<string>(new string[] { "6", "6", "4", "4", "11" });
            List<string> cardSuitP2 = new List<string>(new string[] { "diamond", "spade", "diamond", "spade", "diamond" });

            List<Player> players = new List<Player>();
            players.Add(_poker.AddPlayer(playerOneName, cardValuesP1, cardSuitP1));
            players.Add(_poker.AddPlayer(playerTwoName, cardValuesP2, cardSuitP2));

            _poker.SetPlayerHands(players);

            Assert.AreEqual(HandType.FullHouse, players[0].Hand.Type);
            Assert.AreEqual(2, players[0].Hand.Matches.Count);
            Assert.AreEqual(0, players[0].Hand.Remaining.Count);
            Assert.AreEqual(HandType.TwoPair, players[1].Hand.Type);
            Assert.AreEqual(2, players[1].Hand.Matches.Count);
            Assert.AreEqual(1, players[1].Hand.Remaining.Count);
        }

        [TestMethod]
        public void SetPlayerHandsTest03()
        {
            string playerOneName = "Zach";
            List<string> cardValuesP1 = new List<string>(new string[] { "2", "2", "3", "8", "11" });
            List<string> cardSuitP1 = new List<string>(new string[] { "heart", "spade", "spade", "heart", "spade" });

            string playerTwoName = "Hank";
            List<string> cardValuesP2 = new List<string>(new string[] { "2", "3", "4", "5", "6" });
            List<string> cardSuitP2 = new List<string>(new string[] { "diamond", "diamond", "diamond", "diamond", "diamond" });

            List<Player> players = new List<Player>();
            players.Add(_poker.AddPlayer(playerOneName, cardValuesP1, cardSuitP1));
            players.Add(_poker.AddPlayer(playerTwoName, cardValuesP2, cardSuitP2));

            _poker.SetPlayerHands(players);

            Assert.AreEqual(HandType.Pair, players[0].Hand.Type);
            Assert.AreEqual(1, players[0].Hand.Matches.Count);
            Assert.AreEqual(3, players[0].Hand.Remaining.Count);
            Assert.AreEqual(HandType.StraighFlush, players[1].Hand.Type);
            Assert.AreEqual(0, players[1].Hand.Matches.Count);
            Assert.AreEqual(5, players[1].Hand.Remaining.Count);
        }

        [TestMethod]
        public void SetPlayerHandsTest04()
        {
            string playerOneName = "Zach";
            List<string> cardValuesP1 = new List<string>(new string[] { "2", "2", "2", "2", "11" });
            List<string> cardSuitP1 = new List<string>(new string[] { "heart", "spade", "diamond", "club", "spade" });

            string playerTwoName = "Hank";
            List<string> cardValuesP2 = new List<string>(new string[] { "2", "3", "11", "5", "6" });
            List<string> cardSuitP2 = new List<string>(new string[] { "diamond", "diamond", "diamond", "diamond", "diamond" });

            List<Player> players = new List<Player>();
            players.Add(_poker.AddPlayer(playerOneName, cardValuesP1, cardSuitP1));
            players.Add(_poker.AddPlayer(playerTwoName, cardValuesP2, cardSuitP2));

            _poker.SetPlayerHands(players);

            Assert.AreEqual(HandType.FourOfAKind, players[0].Hand.Type);
            Assert.AreEqual(1, players[0].Hand.Matches.Count);
            Assert.AreEqual(1, players[0].Hand.Remaining.Count);
            Assert.AreEqual(HandType.Flush, players[1].Hand.Type);
            Assert.AreEqual(0, players[1].Hand.Matches.Count);
            Assert.AreEqual(5, players[1].Hand.Remaining.Count);
        }

        [TestMethod]
        public void IsStraightTest01()
        {
            List<string> cardValues = new List<string>(new string[] { "8", "3", "14", "2", "9" });
            List<string> cardSuit = new List<string>(new string[] { "heart", "club", "spade", "heart", "spade" });
            Hand hand = _poker.BuildHand(cardValues, cardSuit);

            bool isStraight = _poker.IsStraight(hand.Cards);

            Assert.IsFalse(isStraight);
        }

        [TestMethod]
        public void IsStraightTest02()
        {
            List<string> cardValues = new List<string>(new string[] { "7", "3", "5", "4", "6" });
            List<string> cardSuit = new List<string>(new string[] { "heart", "club", "spade", "heart", "spade" });
            Hand hand = _poker.BuildHand(cardValues, cardSuit);

            bool isStraight = _poker.IsStraight(hand.Cards);

            Assert.IsTrue(isStraight);
        }

        [TestMethod]
        public void IsFlushTest01()
        {
            List<string> cardValues = new List<string>(new string[] { "8", "3", "14", "2", "9" });
            List<string> cardSuit = new List<string>(new string[] { "heart", "club", "spade", "heart", "spade" });
            Hand hand = _poker.BuildHand(cardValues, cardSuit);

            bool isFlush = _poker.IsFlush(hand.Cards);

            Assert.IsFalse(isFlush);
        }

        [TestMethod]
        public void IsFlushTest02()
        {
            List<string> cardValues = new List<string>(new string[] { "8", "3", "14", "2", "9" });
            List<string> cardSuit = new List<string>(new string[] { "club", "club", "club", "club", "club" });
            Hand hand = _poker.BuildHand(cardValues, cardSuit);

            bool isFlush = _poker.IsFlush(hand.Cards);

            Assert.IsTrue(isFlush);
        }

        [TestMethod]
        public void DetermineWinnerTest01()
        {
            string playerOneName = "Zach";
            List<string> cardValuesP1 = new List<string>(new string[] { "2", "2", "3", "8", "11" });
            List<string> cardSuitP1 = new List<string>(new string[] { "heart", "spade", "spade", "heart", "spade" });

            string playerTwoName = "Hank";
            List<string> cardValuesP2 = new List<string>(new string[] { "2", "2", "7", "9", "12" });
            List<string> cardSuitP2 = new List<string>(new string[] { "diamond", "club", "spade", "heart", "spade" });

            List<Player> players = new List<Player>();
            players.Add(_poker.AddPlayer(playerOneName, cardValuesP1, cardSuitP1));
            players.Add(_poker.AddPlayer(playerTwoName, cardValuesP2, cardSuitP2));

            _poker.SetPlayerHands(players);
            _poker.DetermineWinner(players);
            Assert.IsTrue(players[1].Winner);
            Assert.IsFalse(players[0].Winner);
        }

        [TestMethod]
        public void DetermineWinnerTest02()
        {
            string playerOneName = "Zach";
            List<string> cardValuesP1 = new List<string>(new string[] { "2", "3", "4", "5", "6" });
            List<string> cardSuitP1 = new List<string>(new string[] { "heart", "heart", "spade", "heart", "spade" });

            string playerTwoName = "Hank";
            List<string> cardValuesP2 = new List<string>(new string[] { "2", "3", "4", "5", "6" });
            List<string> cardSuitP2 = new List<string>(new string[] { "diamond", "club", "heart", "club", "club" });

            List<Player> players = new List<Player>();
            players.Add(_poker.AddPlayer(playerOneName, cardValuesP1, cardSuitP1));
            players.Add(_poker.AddPlayer(playerTwoName, cardValuesP2, cardSuitP2));

            _poker.SetPlayerHands(players);
            _poker.DetermineWinner(players);
            Assert.IsFalse(players[0].Winner);
            Assert.IsFalse(players[1].Winner);
        }

        [TestMethod]
        public void DetermineWinnerTest03()
        {
            string playerOneName = "Zach";
            List<string> cardValuesP1 = new List<string>(new string[] { "4", "4", "4", "2", "2" });
            List<string> cardSuitP1 = new List<string>(new string[] { "heart", "club", "spade", "heart", "spade" });

            string playerTwoName = "Hank";
            List<string> cardValuesP2 = new List<string>(new string[] { "2", "3", "4", "5", "6" });
            List<string> cardSuitP2 = new List<string>(new string[] { "diamond", "diamond", "diamond", "heart", "spade" });

            List<Player> players = new List<Player>();
            players.Add(_poker.AddPlayer(playerOneName, cardValuesP1, cardSuitP1));
            players.Add(_poker.AddPlayer(playerTwoName, cardValuesP2, cardSuitP2));

            _poker.SetPlayerHands(players);
            _poker.DetermineWinner(players);
            Assert.IsTrue(players[0].Winner);
            Assert.IsFalse(players[1].Winner);
        }

        [TestMethod]
        public void DetermineWinnerTest04()
        {
            string playerOneName = "Zach";
            List<string> cardValuesP1 = new List<string>(new string[] { "2", "2", "3", "8", "11" });
            List<string> cardSuitP1 = new List<string>(new string[] { "heart", "club", "spade", "heart", "spade" });

            string playerTwoName = "Hank";
            List<string> cardValuesP2 = new List<string>(new string[] { "2", "10", "7", "9", "12" });
            List<string> cardSuitP2 = new List<string>(new string[] { "diamond", "diamond", "diamond", "diamond", "diamond" });

            List<Player> players = new List<Player>();
            players.Add(_poker.AddPlayer(playerOneName, cardValuesP1, cardSuitP1));
            players.Add(_poker.AddPlayer(playerTwoName, cardValuesP2, cardSuitP2));

            _poker.SetPlayerHands(players);
            _poker.DetermineWinner(players);
            Assert.IsFalse(players[0].Winner);
            Assert.IsTrue(players[1].Winner);
        }

        [TestMethod]
        public void DetermineWinnerTest05()
        {
            string playerOneName = "Zach";
            List<string> cardValuesP1 = new List<string>(new string[] { "2", "14", "3", "8", "11" });
            List<string> cardSuitP1 = new List<string>(new string[] { "heart", "heart", "spade", "heart", "spade" });

            string playerTwoName = "Hank";
            List<string> cardValuesP2 = new List<string>(new string[] { "2", "3", "7", "9", "12" });
            List<string> cardSuitP2 = new List<string>(new string[] { "diamond", "club", "spade", "heart", "spade" });

            List<Player> players = new List<Player>();
            players.Add(_poker.AddPlayer(playerOneName, cardValuesP1, cardSuitP1));
            players.Add(_poker.AddPlayer(playerTwoName, cardValuesP2, cardSuitP2));

            _poker.SetPlayerHands(players);
            _poker.DetermineWinner(players);
            Assert.IsTrue(players[0].Winner);
            Assert.IsFalse(players[1].Winner);
        }

        [TestMethod]
        public void DetermineWinnerTest06()
        {
            string playerOneName = "Zach";
            List<string> cardValuesP1 = new List<string>(new string[] { "2", "2", "2", "8", "11" });
            List<string> cardSuitP1 = new List<string>(new string[] { "heart", "club", "spade", "heart", "spade" });

            string playerTwoName = "Hank";
            List<string> cardValuesP2 = new List<string>(new string[] { "5", "13", "7", "9", "12" });
            List<string> cardSuitP2 = new List<string>(new string[] { "diamond", "club", "spade", "heart", "spade" });

            List<Player> players = new List<Player>();
            players.Add(_poker.AddPlayer(playerOneName, cardValuesP1, cardSuitP1));
            players.Add(_poker.AddPlayer(playerTwoName, cardValuesP2, cardSuitP2));

            _poker.SetPlayerHands(players);
            _poker.DetermineWinner(players);
            Assert.IsTrue(players[0].Winner);
            Assert.IsFalse(players[1].Winner);
        }
    }
}