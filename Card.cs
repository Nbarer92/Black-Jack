using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using BlackJack;

public enum Suit
{
    Diamonds, Clubs, Hearts, Spades
}

public enum Face
{
    Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
    Jack, Queen, King
}



public class Card
{
    public Suit Suit { get; }
    public Face Face { get; }
    public string FaceName { get; }

    // set value is for Ace because Ace can have value 1 or 11.
    public int Value { get; set; }

    public char Symbol { get; }

    public ConsoleColor CardColor { get; set; }

    /// Initialize Value and Suit Symbol
    public Card(Suit suit, Face face)
    {
        Suit = suit;
        Face = face;

        switch (Suit)
        {
            case Suit.Clubs:
                Symbol = '♣';
                break;
            case Suit.Spades:
                Symbol = '♠';
                break;
            case Suit.Diamonds:
                Symbol = '♦';                
                break;
            case Suit.Hearts:
                Symbol = '♥';                
                break;
        }

        switch (Face)
        {
            case Face.Ten:
                Value = 10;
                FaceName = "10";
                break;
            case Face.Jack:
                Value = 10;
                FaceName = "J";
                break;
            case Face.Queen:
                Value = 10;
                FaceName = "Q";
                break;
            case Face.King:
                Value = 10;
                FaceName = "K";
                break;
            case Face.Ace:
                Value = 11;
                FaceName = "A";
                break;
            default:
                Value = (int)face + 1;
                FaceName = Value.ToString();
                break;
        }
    }

    public void PrintCardColor()
    {
        Utility.WriteLineInColor($"{this.Symbol}{this.FaceName}", this.CardColor);
    }

    public void PrintCard(Card _card)
    {
        Console.Write($"Drawn card is ");
        Utility.WriteLineInColor($"{_card.Symbol}{_card.FaceName}", _card.CardColor);
    }
}