using System;
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



        private static readonly GameMaster instance = new GameMaster();
        /// <summary>
        /// Singleton Patter
        /// </summary>
        public static GameMaster Instance
        {
            get
            {
                return instance;
            }
        }
        private GameMaster()
        {
            BoardDeck = new CardDeck();
            BoardDeck.ShuffleDeck();
            PlayDeck = new CardDeck();
            //Create more Jacks for the game!
            for (int i = 0; i < 5; i++)
            {
                PlayDeck.DeckOfCard.Add(new JackOneEye(Suit.Spades, Rank.Jack));
                PlayDeck.DeckOfCard.Add(new JackOneEye(Suit.Heart, Rank.Jack));
                PlayDeck.DeckOfCard.Add(new JackTwoEyes(Suit.Clubs, Rank.Jack));
                PlayDeck.DeckOfCard.Add(new JackTwoEyes(Suit.Diamonds, Rank.Jack));
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
        //Clear piece registry?

        ////Add Random card to player hand
        //public void GetNewCard()
        //{
        //    Players[_indexOfPlayerPlaying].PlayerHand.Put(Board.CardDeck.DealTheCard());
        //}
        //Set everything back to 0 before create a new game
        public void ReleaseCardInCells()
        {
            Players[0].RealeaseCardInCellsInHand();
            Players[1].RealeaseCardInCellsInHand();
            Board.ReleaseCardInCells();
        }
        //Set up game
        public void SetUpGame()
        {
            BoardDeck = new CardDeck();
            BoardDeck.ShuffleDeck();
            PlayDeck = new CardDeck();

            for (int i = 0; i < 5; i++)
            {
                PlayDeck.DeckOfCard.Add(new JackOneEye(Suit.Spades, Rank.Jack));
                PlayDeck.DeckOfCard.Add(new JackOneEye(Suit.Heart, Rank.Jack));

            }
            for (int i = 0; i < 10; i++)
            {
                //PlayDeck.DeckOfCard.Add(new JackOneEye(Suit.Heart, Rank.Jack));
                //PlayDeck.DeckOfCard.Add(new JackOneEye(Suit.Spades, Rank.Jack));
                PlayDeck.DeckOfCard.Add(new JackTwoEyes(Suit.Clubs, Rank.Jack));
                PlayDeck.DeckOfCard.Add(new JackTwoEyes(Suit.Diamonds, Rank.Jack));
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
                SwinGame.DrawBitmap(SwinGame.BitmapNamed("DiscardBin.png"), Card.CARD_WIDTH * 12, Card.CARD_HEIGHT * 6);
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
                    if (WinColor==PlayerColors.Blue)
                SwinGame.DrawText(WinMessage,Color.Blue , SwinGame.FontNamed("arial"), TOP_BUTTON_X+50,TOP_BUTTON_Y-100);
                    else
                SwinGame.DrawText(WinMessage, Color.Green, SwinGame.FontNamed("Chelsea"), TOP_BUTTON_X + 100, TOP_BUTTON_Y - 100);
                SwinGame.DrawBitmap(SwinGame.BitmapNamed("BackToGame"), TOP_BUTTON_X, TOP_BUTTON_Y);
                SwinGame.DrawBitmap(SwinGame.BitmapNamed("MainMenuBig"), MIDDLE_BUTTON_X, MIDDLE_BUTTON_Y);
                SwinGame.DrawBitmap(SwinGame.BitmapNamed("Quit"), BOTTOM_BUTTON_X, BOTTOM_BUTTON_Y);
            }

        }
        public Bitmap MyBitMap()
        {
            return SwinGame.BitmapNamed("Background1.png");
        }
        public void DrawBackground()
        {
            SwinGame.DrawBitmap(this.MyBitMap(), 0, 0);
            SwinGame.DrawBitmap(SwinGame.BitmapNamed("BluePlayer.png"), 0, Card.CARD_HEIGHT * 7);
            SwinGame.DrawBitmap(SwinGame.BitmapNamed("GreenPlayer.png"), (int)(Card.CARD_WIDTH * 10.5), 0);
        }
        // Return the player in turn\?
        public Player PlayerInturn
        {
            get
            {
                return _players[IndexOfPlayerPlaying];
            }
        }
        // Get the array of players?
        public Player[] PlayersArray
        {
            get
            {
                return _players;
            }
        }

        public string WinMessage { get => _winMessage; set => _winMessage = value; }
        public int IndexOfPlayerPlaying { get => _indexOfPlayerPlaying; set => _indexOfPlayerPlaying = value; }
        public int IndexOfPlayerWaiting { get => _indexOfPlayerWaiting; set => _indexOfPlayerWaiting = value; }
        public PlayerColors WinColor { get => _winColor; set => _winColor = value; }


        //Take the turn for each player Control the player to take turn
        public void TakeTheTurn(Point2D pt)
        {
            Players[IndexOfPlayerPlaying].TakeTurn(pt, this);
        }
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
        public void MainMenuButton(Point2D pt, Program program)
        {
            if (SwinGame.PointInRect(pt, MAIN_MENU_X, MAIN_MENU_Y, MAIN_MENU_WIDTH, MAIN_MENU_HEIGHT))
                program.ChangeScreenViewing(ProgramState.MENU);
        }
        public void HandleInputInWinScreen(Point2D pt,Program program)
        {
            if (SwinGame.PointInRect(pt, TOP_BUTTON_X, TOP_BUTTON_Y,BUTTON_WIDTH , BUTTON_HEIGHT))
                ChangeState(GameState.Done);
            if (SwinGame.PointInRect(pt, MIDDLE_BUTTON_X, MIDDLE_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT))
            { program.ChangeScreenViewing(ProgramState.MENU);
                ChangeState(GameState.Done);
            }
            if (SwinGame.PointInRect(pt, BOTTOM_BUTTON_X, BOTTOM_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT))
            {
                program.ChangeScreenViewing(ProgramState.QUITTING);
            }

        }
        //Fetch the Card in Player'Hand
        public Cell FetchTheCellInHAnd(Point2D pt)
        {
            return Players[IndexOfPlayerPlaying].PlayerHand.FetchACell(pt);
        }
        public Cell FetchTheCellInBoard(Point2D pt)
        {
            return Board.FetchACell(pt);
        }

        //Update the game
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
        public void TurnHandUpSideDown()
        {
            if (!Players[IndexOfPlayerPlaying].PlayerHand.Cells[1].IsFaceUp)
                Players[IndexOfPlayerPlaying].PlayerHand.TurnOverAllCardsInHand();
            if (Players[IndexOfPlayerWaiting].PlayerHand.Cells[1].IsFaceUp)
                Players[IndexOfPlayerWaiting].PlayerHand.TurnOverAllCardsInHand();
        }
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

