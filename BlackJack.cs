using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
 
public class BlackJackGame
{
    private Deck deck;
    public void Play()
    {
        bool continuePlay = true;
        Screen.SplashScreen();
        Screen.PromptPlayerName();

        var player = new Player(Console.ReadLine());

        var dealerComputer = new Player();

        deck = new Deck();

        while (continuePlay)
        {
            // Initialize screen and reset player and dealer's certain property
            // for the new round.
            Console.Clear();
            player.ResetPlayerHand();
            dealerComputer.ResetPlayerHand();

            // Create a new deck if remaining cards are less than 20
            if (deck.GetRemainingDeckCount() < 20)
                deck = new Deck();

            deck.ShowRemainingDeckCount();

            // Show player bank roll
            Console.WriteLine($"{player.Name} Chips Balance: {player.ChipsOnHand}");

            if (player.ChipsOnHand <= 10)
            {
                Utility.WriteLineInColor("Insufficient chips in your account.", ConsoleColor.Red);
                Utility.WriteLineInColor("Please reload your chips from the counter to continue to play.\n", ConsoleColor.Red);

                continuePlay = false;
                break;
            }

            // Get bet amount from player
            Console.Write("Enter chips: ");
            player.ChipsOnBet = Convert.ToInt16(Console.ReadLine());
            // for brevity, no input validation here.

            // Deal first two cards to player (Background)
            deck.DrawCard(player);
            deck.DrawCard(player);

            // Show player's hand (UI)
            player.ShowUpCards();
            Utility.Sleep();

            Utility.Line();

            // Deal first two cards to dealer (Background)
            deck.DrawCard(dealerComputer);
            deck.DrawCard(dealerComputer);

            // Show dealer's hand (UI)        
            dealerComputer.ShowUpCards(true);
            Utility.Sleep();

            Utility.Line();

            // Check natural black jack
            if (CheckNaturalBlackJack(player, dealerComputer) == false)
            {
                // If both also don't have natural black jack, 
                // then player's turn to continue. 
                // After player's turn, it will be dealer's turn.
                TakeAction(player);
                TakeAction(dealerComputer, player.IsBusted);

                AnnounceWinnerForTheRound(player, dealerComputer);
            }

            Console.WriteLine("This round is over.");

            Console.Write("\nPlay again? Y or N? ");

            continuePlay = Console.ReadLine().ToUpper() == "Y" ? true : false;
            // for brevity, no input validation
        }

        PrintEndGame(player, dealerComputer);
    }



    private void TakeAction(Player currentPlayer, bool isPlayerBusted = false)
    {
        string opt = "";
        currentPlayer.Turn = true;

        Console.WriteLine($"\n{currentPlayer.Name}'s turn. ");

        while (currentPlayer.Turn)
        {
            if (currentPlayer.Name.Equals("Dealer"))
            {
                Utility.Sleep(2000); // faking thinking time.               
                // Mini A.I for dealer.
                if (isPlayerBusted) // if player bust, dealer can stand to win                
                    // break; // Dealer is required to still reveal hole card even though the player bust
                    opt = "S";
                else
                    opt = currentPlayer.GetHandValue() <= 16 ? "H" : "S";
            }
            else
            {
                // Prompt player to enter Hit or Stand.
                Console.Write("Hit (H) or Stand (S): ");
                opt = Console.ReadLine();
            }

            switch (opt.ToUpper())
            {
                case "H":
                    currentPlayer.Hit(deck);
                    currentPlayer.ShowHandValue();

                    break;
                case "S":
                    //if (currentPlayer.CanPlayerStand(isPlayerBusted))
                    currentPlayer.Stand();

                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }

            CheckPlayerCard(currentPlayer);
        }

        Console.WriteLine($"{currentPlayer.Name}'s turn is over.");
        Utility.Line();
        Utility.Sleep();
    }



    private void CheckPlayerCard(Player _currentPlayer)
    {
        // If current player is busted, turn is over.
        if (_currentPlayer.GetHandValue() > 21)
        {
            Utility.WriteLineInColor("Bust!", ConsoleColor.Red);
            Utility.Sleep();

            _currentPlayer.IsBusted = true;
            _currentPlayer.Turn = false;
        }
        // If current player total card in hand is 5, turn is over.
        else if (_currentPlayer.Hand.Count == 5)
        {
            Console.WriteLine($"{_currentPlayer.Name} got 5 cards in hand already.");
            Utility.Sleep();

            _currentPlayer.Turn = false;
        }
    }

    private bool CheckNaturalBlackJack(Player _player, Player _dealer)
    {
        Console.WriteLine();
        if (_dealer.IsNaturalBlackJack && _player.IsNaturalBlackJack)
        {
            Console.WriteLine("Player and Dealer got natural BlackJack. Tie Game!");
            _dealer.ShowUpCards();
            return true;
        }
        else if (_dealer.IsNaturalBlackJack && !_player.IsNaturalBlackJack)
        {
            Console.WriteLine($"{_dealer.Name} got natural BlackJack. {_dealer.Name} won!");
            _dealer.ShowUpCards();
            _player.ChipsOnHand -= (int)Math.Floor(_player.ChipsOnBet * 1.5);
            return true;
        }
        else if (!_dealer.IsNaturalBlackJack && _player.IsNaturalBlackJack)
        {
            Console.WriteLine($"{_player.Name} got natural BlackJack. {_player.Name} won!");
            _player.AddWinCount();
            _player.ChipsOnHand += (int)Math.Floor(_player.ChipsOnBet * 1.5);
            return true;
        }

        // guard block
        return false;
    }

    private void AnnounceWinnerForTheRound(Player _player, Player _dealer)
    {
        Console.WriteLine();
        if (!_dealer.IsBusted && _player.IsBusted)
        {
            Console.WriteLine($"{_dealer.Name} won.");
            _dealer.AddWinCount();
            _player.ChipsOnHand -= _player.ChipsOnBet;
        }
        else if (_dealer.IsBusted && !_player.IsBusted)
        {
            Console.WriteLine($"{_player.Name} won.");
            _player.AddWinCount();
            _player.ChipsOnHand += _player.ChipsOnBet;
        }
        else if (_dealer.IsBusted && _player.IsBusted)
        {
            Console.WriteLine("Tie game.");
        }
        else if (!_dealer.IsBusted && !_player.IsBusted)
            if (_player.GetHandValue() > _dealer.GetHandValue())
            {
                Console.WriteLine($"{_player.Name} won.");
                _player.AddWinCount();
                _player.ChipsOnHand += _player.ChipsOnBet;
            }
            else if (_player.GetHandValue() < _dealer.GetHandValue())
            {
                Console.WriteLine($"{_dealer.Name} won.");
                _dealer.AddWinCount();
                _player.ChipsOnHand -= _player.ChipsOnBet;
            }

            else if (_player.GetHandValue() == _dealer.GetHandValue())
                Console.WriteLine("Tie game.");

    }

    private void PrintEndGame(Player player, Player dealerComputer)
    {
        Console.WriteLine($"{player.Name} won {player.TotalWins} times.");
        Console.WriteLine($"{dealerComputer.Name} won {dealerComputer.TotalWins} times.");
        Console.WriteLine("Game over. Thank you for playing.");
    }
}
}
