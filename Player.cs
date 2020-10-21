using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }

        public bool IsNaturalBlackJack { get; set; }

        public bool IsBusted { get; set; } = false;

        public int TotalWins { get; set; } = 0;
        public static int TotalWinsCounter { get; private set; } = 0;


        public int ChipsOnHand { get; set; } = 500;

        public int ChipsOnBet { get; set; }

        public bool Turn { get; set; } = true;

        public Player(string Name = "Dealer")
        {
            this.Name = Name;
            Hand = new List<Card>(5);
        }
        public int GetHandValue()
        {
            int value = 0;
            foreach (Card card in Hand)
                value += card.Value;

            return value;
        }

        public void ShowHandValue()
        {
            Console.WriteLine($"{this.Name}'s hand value is: {this.GetHandValue()} ({this.Hand.Count} cards)");
        }

        public void ShowUpCards(bool isDealer = false)
        {
            Console.WriteLine($"\n{this.Name}'s hand has:");
            if (isDealer)
            {
                Utility.WriteLineInColor($"{this.Hand[0].Symbol}{this.Hand[0].FaceName}", this.Hand[0].CardColor);

                Utility.WriteLineInColor("<Hole Card>", ConsoleColor.Magenta);

                Console.WriteLine($"{this.Name}'s Hand value is: {this.Hand[0].Value}");
            }
            else
            {
                foreach (var card in this.Hand)
                    card.PrintCardColor();

                ShowHandValue();
            }
        }



        public void AddWinCount()
        {
            this.TotalWins = ++TotalWinsCounter;
        }

        public void Hit(Deck deck)
        {
            Console.Write($"{this.Name} hits. ");
            Utility.Sleep();

            // Take a card from the deck and put into player's Hand.
            //Card card = new Card(Suit.Hearts, Face.Ace); //deck.DrawCard();        
            Card card = deck.DrawCard(this);
            // If there is any Ace in the Hand, change all the Ace's value to 1.
            // if (this.GetHandValue() + card.Value > 21 && card.Face == Face.Ace)
            //     card.Value = 1;

            //Hand.Add(card); // Background
            card.PrintCardColor(); // UI
            Utility.Sleep();
        }

        public void Stand()
        {
            Console.WriteLine($"{this.Name} stands."); // UI
            Utility.Sleep();

            this.ShowUpCards(); // UI
            Utility.Sleep();

            this.Turn = false;
        }

        public bool CanPlayerStand(bool isPlayerBusted)
        {
            // Player can stand without condition
            if (!this.Name.Equals("Dealer"))
                return true;
            else if (isPlayerBusted) // for dealer to auto stand if player busted        
                return true;

            return false;
        }

        public void ResetPlayerHand()
        {
            this.Hand = new List<Card>(5);
            this.IsNaturalBlackJack = false;
            this.IsBusted = false;
        }
    }
}
