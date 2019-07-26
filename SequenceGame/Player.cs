 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SwinGameSDK;
namespace MyGame
{
    /// <summary>
    /// The Player represents the player who will decide which card to play and where to put the chip on the board.
    /// </summary>
    public class Player
    {
        private PlayerColors _color;
        private Hand _playerHand;
        private TheBoard _board;
        private Player _opponent;
        private Chip _chip;
        private int _indexOfPlayer;
        private Cell _chosenCellInHand;
        public PlayerColors PlayerColor { get => _color; set => _color = value; }
        public Hand PlayerHand { get => _playerHand; set => _playerHand = value; }
        public TheBoard Board { get => _board; set => _board = value; }
        public Player Opponent { get => _opponent; set => _opponent = value; }
        public Chip Chip { get => _chip; set => _chip = value; }
        public int IndexOfPlayer { get => _indexOfPlayer; set => _indexOfPlayer = value; }
        public Cell ChosenCellInHand { get => _chosenCellInHand; set => _chosenCellInHand = value; }

        

        /// <summary>
        /// Initializing the color of the player and the position of the player's hand in the board.
        /// </summary>
        /// <param name="color"></param>
        public Player(PlayerColors color, int index, CardDeck cardDeck)
        {
            Chip = new Chip(color);
            PlayerColor = color;
            IndexOfPlayer = index;
            if (index == 0)
            {
                _playerHand = new Hand(Card.CARD_WIDTH, Card.CARD_HEIGHT * 7, Card.CARD_WIDTH, 0, cardDeck);
            }
            else
            {
                _playerHand = new Hand((int)(Card.CARD_WIDTH * 10.5), Card.CARD_HEIGHT, 0, Card.CARD_HEIGHT, cardDeck);
            }

        }
        /// <summary>
        /// Draw the cards in the hand of the player on the screen.
        /// </summary>
        public void DrawCellsInHand()
        {
            PlayerHand.DrawCells();
        }
        public void RemoveAndAddNewCard(Cell cell, Card card)
        {
            PlayerHand.RemoveACardInChosenCellAndAddNewOne(cell, card);
        }

        public void RealeaseCardInCellsInHand()
        {
            PlayerHand.ReleaseCardInCells();
        }
        public bool ShowingPossiblCellInTheBoard(Cell cell, GameMaster game)
        {
            int counting = 0;
            if (cell.CardInCell.IsCardJack())
                return true;
            foreach (Cell c in Board.Cells)
            {
                if ((cell.CardInCell.Rank == c.CardInCell.Rank) && (cell.CardInCell.Suit == c.CardInCell.Suit))
                {
                    if (!c.IsChipAt())
                    {
                        c.MarkCellSelected();
                        counting++;
                    }
                }
            }
            //After deselecting card in cell that have chip in it,
            //there are still some selecting cells.
            if (counting == 0)
            {
                game.DiscardPile.Push(ChosenCellInHand.CardInCell);
                this.RemoveAndAddNewCard(ChosenCellInHand, game.PlayDeck.DealCard());
                game.DiscardHistory.Push($"{IndexOfPlayer.ToString()},{ChosenCellInHand.Column.ToString()}");
                ChosenCellInHand.MarkCellUnSelected();
                return false;
            }
            return true;
        }

        /// <summary>
        ///Handle the input and take a turn for Human Player  
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="game"></param>
        public void TakeTurn(Point2D pt, GameMaster game)
        {
            if (game.Currentstate == GameState.SelectingCardInHand)
            {
                ChosenCellInHand = game.FetchTheCellInHAnd(pt);
                if (ChosenCellInHand != null)
                {
                    ChosenCellInHand.MarkCellSelected();
                    if (ShowingPossiblCellInTheBoard(ChosenCellInHand, game))
                        game.ChangeState(GameState.SelectingTheCellInBoard);
                    else
                        game.ChangeState(GameState.SelectingCardInHand);
                }
            }
            else if (game.Currentstate == GameState.SelectingTheCellInBoard)
            {
                Cell chosenCellInBoard = game.FetchTheCellInBoard(pt);
                if (chosenCellInBoard != null)
                {
                    if (ChosenCellInHand.CardInCell.IsCardJack())
                    {
                        if ((ChosenCellInHand.CardInCell.Suit==Suit.Spades)||(ChosenCellInHand.CardInCell.Suit == Suit.Heart))
                        {

                            if ((chosenCellInBoard.IsChipAt()) && (chosenCellInBoard.Chip.ChipColor == Opponent.PlayerColor))
                            {
                                Board.TakingAction(this, chosenCellInBoard, "remove");
                            }

                        }
                        if ((ChosenCellInHand.CardInCell.Suit == Suit.Diamonds) || (ChosenCellInHand.CardInCell.Suit == Suit.Clubs))
                        {
                            if (!chosenCellInBoard.IsChipAt())
                            {
                                Board.TakingAction(this, chosenCellInBoard, "place");
                            }
                        }
                        game.DiscardPile.Push(ChosenCellInHand.CardInCell);
                        this.RemoveAndAddNewCard(ChosenCellInHand, game.PlayDeck.DealCard());
                        game.DiscardHistory.Push($"{IndexOfPlayer.ToString()},{ChosenCellInHand.Column.ToString()}");
                        game.ChangeState(GameState.Done);
                        game.Update();
                        ChosenCellInHand.MarkCellUnSelected();
                        game.ChangeTurn();
                        Opponent.TakeTurn(pt, game);
                        
                    }
                    else
                    {
                        foreach (Cell cell in Board.Cells)
                        {
                            if (cell.IsSelected())
                            {
                                if (chosenCellInBoard == cell)
                                {
                                    Board.TakingAction(this, cell, "place");
                                    cell.MarkCellUnSelected();
                                    game.DiscardPile.Push(ChosenCellInHand.CardInCell);
                                    this.RemoveAndAddNewCard(ChosenCellInHand, game.PlayDeck.DealCard());
                                    game.DiscardHistory.Push($"{IndexOfPlayer.ToString()},{ChosenCellInHand.Column.ToString()}");

                                }
                                else
                                    cell.MarkCellUnSelected();
                            }
                        }
                        ChosenCellInHand.MarkCellUnSelected();
                        game.ChangeState(GameState.Done);
                        game.Update();
                        game.ChangeTurn();
                        Opponent.TakeTurn(pt, game);
                    }
                }
            }
        }
        public void SaveTheHand(StreamWriter writter)
        {
            PlayerHand.SaveCells(writter);
        }
        public void LoadCellsForHand(StreamReader reader)
        {
            PlayerHand.LoadCells(reader);
        }
        
    }
    
}
