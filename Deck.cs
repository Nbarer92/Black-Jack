using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    
    public class Deck
    {
        // Field
        private List<Card> deck;


        public Deck()
        {
            
            deck = new List<Card>(52);
            IDeck();
            Shuffle();
            Screen.PrintShufflingDeck();
        }

        private void IDeck()
        {
            var suitAsList = Enum.GetValues(typeof(Suit)).Cast<Suit>().ToArray();

            
            deck = suitAsList
            .SelectMany(suit => Enumerable.Range(0, 12), (suit, rank) => new Card((Suit)suit, (Face)rank)) .ToList();

        }

        
        public Card DrawCard(Player person, bool test = false)
        {
            Card card;
            if (test)
            {
                card = new Card(Suit.Clubs, Face.Ace);
            }
            else
            {
                card = deck[0];
            }


            if (person.GetHandValue() + card.Value == 21 && person.Hand.Count == 1)
                            
                person.IsNaturalBlackJack = true;
            else if (person.GetHandValue() + card.Value > 21 && card.Face == Face.Ace)
               
                card.Value = 1;

            person.Hand.Add(card);
            deck.Remove(card);
            return card;
        }

        private void Shuffle()
        {
            Random rng = new Random();

            int n = deck.Count;

           
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card card = deck[k];
                deck[k] = deck[n];
                deck[n] = card;
            }
        }

        public void ShowRemainingDeckCount()
        {
            Console.WriteLine("\nRemaining cards in the deck: " + GetRemainingDeckCount());
        }

        public int GetRemainingDeckCount()
        {
            return deck.Count;
        }
    }
}
