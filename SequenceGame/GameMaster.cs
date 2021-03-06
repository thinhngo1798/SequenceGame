﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
namespace MyGame
{
    public sealed class GameMaster
    {
        private const int TOP_BUTTON_X = 300;
        private const int TOP_BUTTON_Y = 350;
        private const int MIDDLE_BUTTON_X = 600;
        private const int BUTTON_WIDTH = 150;
        private const int BUTTON_HEIGHT = 100;
        private const int MIDDLE_BUTTON_Y = 350;
        private const int BOTTOM_BUTTON_X = 450;
        private const int BOTTOM_BUTTON_Y = 500;
        public int REDO_X = 580;
        public int REDO_Y = 672;
        public int RESET_X = 630;
        public int RESET_Y = 672;
        public int SAVE_X = 680;
        public int SAVE_Y = 672;
        public int MAIN_MENU_X = 615;
        public int MAIN_MENU_Y = 722;
        public int MAIN_MENU_WIDTH = 70;
        public int MAIN_MENU_HEIGHT = 55;
        private string FILE_NAME = "C:/Users/trang/Desktop/SequenceGame.txt";
        private Player[] _players = new Player[3];
        private GameState _currentstate;
        private TheBoard _board;
        private CardDeck _playDeck;
        private CardDeck _boardDeck;
        private string _winMessage = null;
        private int _indexOfPlayerPlaying;
        private int _indexOfPlayerWaiting;
        PlayerColors _winColor = new PlayerColors();
        private Stack<Card> _discardPile;
        private Stack<string> _discardHistory = new Stack<string>();

        public Player[] Players { get => _players; set => _players = value; }
        public GameState Currentstate { get => _currentstate; set => _currentstate = value; }
        public TheBoard Board { get => _board; set => _board = value; }
        public CardDeck PlayDeck { get => _playDeck; set => _playDeck = value; }
        public CardDeck BoardDeck { get => _boardDeck; set => _boardDeck = value; }
        public Stack<Card> DiscardPile { get => _discardPile; set => _discardPile = value; }
        public Stack<string> DiscardHistory { get => _discardHistory; set => _discardHistory = value; }
        public string WinMessage { get => _winMessage; set => _winMessage = value; }
        public int IndexOfPlayerPlaying { get => _indexOfPlayerPlaying; set => _indexOfPlayerPlaying = value; }
        public int IndexOfPlayerWaiting { get => _indexOfPlayerWaiting; set => _indexOfPlayerWaiting = value; }
        public PlayerColors WinColor { get => _winColor; set => _winColor = value; }

        private static readonly GameMaster _instance = new GameMaster();
        /// <summary>
        /// Singleton Patter
        /// </summary>
        public static GameMaster Instance
        {
            get
            {
                return _instance;
            }
        }
        private GameMaster()
        {
            BoardDeck = new CardDeck();
            BoardDeck.ShuffleDeck();
            PlayDeck = new CardDeck();
            //Create more Jacks for the game!
            for (int i = 0; i < 2; i++)
            {
                PlayDeck.DeckOfCard.Add(new Card(Suit.Spades, Rank.Jack));
                PlayDeck.DeckOfCard.Add(new Card(Suit.Heart, Rank.Jack));
                PlayDeck.DeckOfCard.Add(new Card(Suit.Clubs, Rank.Jack));
                PlayDeck.DeckOfCard.Add(new Card(Suit.Diamonds, Rank.Jack));
            }
            PlayDeck.ShuffleDeck();
            DiscardPile = new Stack<Card>();
            Players[0] = new Player(PlayerColors.Blue, 0, PlayDeck);
            Players[1] = new Player(PlayerColors.Green, 1, PlayDeck);

            Players[0].Opponent = Players[1];
            Players[1].Opponent = Players[0];

            Board = new TheBoard(_boardDeck);
            Players[0].Board = Board;
            Players[1].Board = Board;

            Currentstate = GameState.SelectingCardInHand;
            IndexOfPlayerPlaying = 0;
            IndexOfPlayerWaiting = 1;
        }

        public void ChangeState(GameState state)
        {
            Currentstate = state;
        }
        public void ChangeTurn()
        {
            int tempTurn;
            tempTurn = IndexOfPlayerPlaying;
            IndexOfPlayerPlaying = IndexOfPlayerWaiting;
            IndexOfPlayerWaiting = tempTurn;
        }
        /// <summary>
        /// Release card in cells of both board and hands.
        /// </summary>
        public void ReleaseCardInCells()
        {
            Players[0].RealeaseCardInCellsInHand();
            Players[1].RealeaseCardInCellsInHand();
            Board.ReleaseCardInCells();
        }
        /// <summary>
        /// Set up game
        /// </summary>
        public void SetUpGame()
        {
            BoardDeck = new CardDeck();
            BoardDeck.ShuffleDeck();
            PlayDeck = new CardDeck();

            for (int i = 0; i < 2; i++)
            {
                PlayDeck.DeckOfCard.Add(new Card(Suit.Spades, Rank.Jack));
                PlayDeck.DeckOfCard.Add(new Card(Suit.Heart, Rank.Jack));
                PlayDeck.DeckOfCard.Add(new Card(Suit.Clubs, Rank.Jack));
                PlayDeck.DeckOfCard.Add(new Card(Suit.Diamonds, Rank.Jack));
            }

            PlayDeck.ShuffleDeck();
            DiscardPile = new Stack<Card>();
            Players[0] = new Player(PlayerColors.Blue, 0, PlayDeck);
            Players[1] = new Player(PlayerColors.Green, 1, PlayDeck);

            Players[0].Opponent = Players[1];
            Players[1].Opponent = Players[0];


            Board = new TheBoard(_boardDeck);
            Players[0].Board = Board;
            Players[1].Board = Board;

            Currentstate = GameState.SelectingCardInHand;
            IndexOfPlayerPlaying = 0;
            IndexOfPlayerWaiting = 1;
        }

        
        /// <summary>
        ///Draw the game! Draw background, draw board, draw cards of player, draw Buttons of option in Playing Screen
        ///Depend on the game state, it can also draw components of Win Screen.
        /// </summary>
        public void DrawEverything()
        {
            if (Currentstate != GameState.EndGame)
            {
                DrawBackground();
                Board.DrawCells();
                Players[0].DrawCellsInHand();
                Players[1].DrawCellsInHand();
                SwinGame.DrawBitmap(SwinGame.BitmapNamed("DiscardBin.png"), (int)(Card.CARD_WIDTH * 12.5) +10, Card.CARD_HEIGHT * 6+30);
                if (DiscardPile.Count == 0)
                    SwinGame.DrawBitmap(SwinGame.BitmapNamed("BackOfCard.bmp"), Card.CARD_WIDTH * 12 + Card.CARD_WIDTH / 2, Card.CARD_HEIGHT * 7);
                else
                {
                    DiscardPile.Peek().Draw(Card.CARD_WIDTH * 12 + Card.CARD_WIDTH / 2, Card.CARD_HEIGHT * 7);
                }
                SwinGame.DrawBitmap(SwinGame.BitmapNamed("Redo.png"), REDO_X, REDO_Y);
                SwinGame.DrawBitmap(SwinGame.BitmapNamed("ResetButton.png"), RESET_X, RESET_Y);
                SwinGame.DrawBitmap(SwinGame.BitmapNamed("SaveButton.png"), SAVE_X, SAVE_Y);
                SwinGame.DrawBitmap(SwinGame.BitmapNamed("MainMenu.png"), MAIN_MENU_X, MAIN_MENU_Y);

            }
            else
            {
                if (WinColor == PlayerColors.Blue)
                    SwinGame.DrawText(WinMessage, Color.Blue, SwinGame.FontNamed("arial"), TOP_BUTTON_X + 50, TOP_BUTTON_Y - 100);
                else
                {
                    SwinGame.DrawText(WinMessage, Color.Green, SwinGame.FontNamed("Chelsea"), TOP_BUTTON_X + 100, TOP_BUTTON_Y - 100);
                }
                SwinGame.DrawBitmap(SwinGame.BitmapNamed("BackToGame"), TOP_BUTTON_X, TOP_BUTTON_Y);
                SwinGame.DrawBitmap(SwinGame.BitmapNamed("MainMenuBig"), MIDDLE_BUTTON_X, MIDDLE_BUTTON_Y);
                SwinGame.DrawBitmap(SwinGame.BitmapNamed("Quit"), BOTTOM_BUTTON_X, BOTTOM_BUTTON_Y);
            }

        }
        /// <summary>
        /// Draw Background in Playing Screen
        /// </summary>
        public void DrawBackground()
        {
            SwinGame.DrawBitmap(SwinGame.BitmapNamed("Background1.png"), 0, 0);
            SwinGame.DrawBitmap(SwinGame.BitmapNamed("BluePlayer.png"), 0, Card.CARD_HEIGHT * 7);
            SwinGame.DrawBitmap(SwinGame.BitmapNamed("GreenPlayer.png"), (int)(Card.CARD_WIDTH * 10.5), 0);
        }
        /// <summary>
        /// Return player who has the playing index.
        /// </summary>
        public Player PlayerInturn
        {
            get
            {
                return _players[IndexOfPlayerPlaying];
            }
        }

        public Player[] PlayersArray
        {
            get
            {
                return _players;
            }
        }
        /// <summary>
        /// The player take the turn and then player'opponent take the turn. It is symmetric
        /// </summary>
        /// <param name="pt"></param>
        public void TakeTheTurn(Point2D pt)
        {
            Players[IndexOfPlayerPlaying].TakeTurn(pt, this);
        }
        /// <summary>
        /// Handle the game when player click Redo button in Playing Screen
        /// </summary>
        /// <param name="pt"></param>
        public void RedoButton(Point2D pt)
        {
            if (SwinGame.PointInRect(pt, REDO_X, REDO_Y, 40, 40))
            {
                Board.RedoAction(this);
                RedoForDiscardPile();
            }
            if (Currentstate == GameState.Done)
            {
                ChangeState(GameState.TemporaryState);
            }
        }
        /// <summary>
        /// Redo when the player press redo button
        /// </summary>
        public void RedoForDiscardPile()
        {
            if (DiscardHistory.Count == 0)
                return;
            string LastRemove = DiscardHistory.Pop();
            string[] infor = LastRemove.Split(',');
            int playerIndex = System.Convert.ToInt32(infor[0]);
            Cell CellToReplace = Players[playerIndex].PlayerHand.FetchACell(System.Convert.ToInt32(infor[1]));
            if (DiscardPile.Count == 0)
                return;
            else
                Players[playerIndex].PlayerHand.RemoveACardInChosenCellAndAddNewOne(CellToReplace, DiscardPile.Pop());
        }
        /// <summary>
        /// Reset the game when player click Reset button
        /// </summary>
        /// <param name="pt"></param>
        public void ResetButton(Point2D pt)
        {
            if (SwinGame.PointInRect(pt, RESET_X, RESET_Y, 40, 40))
            {
                Board.ResetAction(this);
                while (DiscardHistory.Count != 0)
                {
                    RedoForDiscardPile();
                }
            }
        }
        /// <summary>
        /// Going back to Main Menu when starting the game. 
        /// User can select loading the old game or quit the game.
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="program"></param>
        public void MainMenuButton(Point2D pt, Program program)
        {
            if (SwinGame.PointInRect(pt, MAIN_MENU_X, MAIN_MENU_Y, MAIN_MENU_WIDTH, MAIN_MENU_HEIGHT))
                program.ChangProgramState(ProgramState.MENU);
        }
        /// <summary>
        /// When 1 player has won the game, there will be 3 choices for user to choose
        /// This method handles the input from user.
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="program"></param>
        public void HandleInputInWinScreen(Point2D pt,Program program)
        {
            if (SwinGame.PointInRect(pt, TOP_BUTTON_X, TOP_BUTTON_Y,BUTTON_WIDTH , BUTTON_HEIGHT))
                ChangeState(GameState.Done);
            if (SwinGame.PointInRect(pt, MIDDLE_BUTTON_X, MIDDLE_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT))
            { program.ChangProgramState(ProgramState.MENU);
                ChangeState(GameState.Done);
            }
            if (SwinGame.PointInRect(pt, BOTTOM_BUTTON_X, BOTTOM_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT))
            {
                program.ChangProgramState(ProgramState.QUITTING);
            }

        }
        /// <summary>
        ///Fetch the Card in Player'Hand
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public Cell FetchTheCellInHAnd(Point2D pt)
        {
            return Players[IndexOfPlayerPlaying].PlayerHand.FetchACell(pt);
        }
        public Cell FetchTheCellInBoard(Point2D pt)
        {
            return Board.FetchACell(pt);
        }

        /// <summary>
        /// Update the state of the game. It checks whether the playdeck has been expired and fill it up.
        /// It also checks have any players won the game.
        /// </summary>
        public void Update()
        {
            if (PlayDeck.DeckOfCard.Count==0)
            {
               PlayDeck.AddCards(DiscardPile);
                DiscardPile.Clear();
            }
            if (Currentstate == GameState.Done)
            {
                if (Board.CheckingWinerOfTheGame(Players[IndexOfPlayerPlaying], this))
                {
                    WinColor = (Players[IndexOfPlayerPlaying].PlayerColor);
                    switch (WinColor)
                    {
                        case PlayerColors.Blue:
                            WinMessage = "Player Blue Won. Congratulation!!";
                            ChangeState(GameState.EndGame);
                            break;
                        case PlayerColors.Green:
                            WinMessage = "Player Green Won. Congratulation!!";
                            ChangeState(GameState.EndGame);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    this.ChangeState(GameState.TemporaryState);
                    Players[IndexOfPlayerPlaying].PlayerHand.TurnOverAllCardsInHand();
                    if (!Players[IndexOfPlayerWaiting].PlayerHand.Cells[1].IsFaceUp)
                        Players[IndexOfPlayerWaiting].PlayerHand.TurnOverAllCardsInHand();
                }

            }
        }
        /// <summary>
        /// Turn all the cards of the hand of 1 player. It is to clarify which player is on the turn.
        /// </summary>
        public void TurnHandUpSideDown()
        {
            if (!Players[IndexOfPlayerPlaying].PlayerHand.Cells[1].IsFaceUp)
                Players[IndexOfPlayerPlaying].PlayerHand.TurnOverAllCardsInHand();
            if (Players[IndexOfPlayerWaiting].PlayerHand.Cells[1].IsFaceUp)
                Players[IndexOfPlayerWaiting].PlayerHand.TurnOverAllCardsInHand();
        }
        /// <summary>
        /// Save the game when player click Save button in the playing screen
        /// </summary>
        /// <param name="pt"></param>
        public void SaveButton(Point2D pt)
        {
            if (SwinGame.PointInRect(pt, SAVE_X, SAVE_Y, 40, 40))
            {
                StreamWriter writter = new StreamWriter(FILE_NAME);
                writter.AutoFlush = true;
                writter.WriteLine(Currentstate);
                writter.WriteLine(IndexOfPlayerPlaying);
                Players[IndexOfPlayerPlaying].SaveTheHand(writter);
                Players[IndexOfPlayerWaiting].SaveTheHand(writter);
                Board.SaveCells(writter);
                writter.Close();
            }
        }
        /// <summary>
        /// Load the old game which users saved before.
        /// </summary>
        public void LoadGame()
        {if (Board.Cells!=null)
            ReleaseCardInCells();
            StreamReader reader = new StreamReader(FILE_NAME);
            string gameState = reader.ReadLine();
            ChangeState((GameState)Enum.Parse(typeof(GameState), gameState));
            IndexOfPlayerPlaying = Convert.ToInt32(reader.ReadLine());
            if (IndexOfPlayerPlaying == 1)
            { IndexOfPlayerWaiting = 0; }
            else
            {
                IndexOfPlayerWaiting = 1;
            }
            Players[IndexOfPlayerPlaying].LoadCellsForHand(reader);
            Players[IndexOfPlayerWaiting].LoadCellsForHand(reader);
            Board.LoadCells(reader);
            reader.Close();
        }
      
    }
}

