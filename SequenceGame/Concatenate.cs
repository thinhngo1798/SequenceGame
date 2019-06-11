using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
    public abstract class Card
    {
        public const int CARD_WIDTH = 70;
        public const int CARD_HEIGHT = 96;
        private int _x;
        private int _y;
        protected Suit _suit;
        protected Rank _rank;

        /// <summary>
        /// X-coordinate of the piece
        /// </summary>
        /// <value>X-coordinate</value>
        public int X
        {
            get
            {
                return _x;
            }
            set
            { _x = value; }
        }
        /// <summary>
        /// Y-coordinate of the piece
        /// </summary>
        /// <value>Y-coordinate</value>
        public int Y
        {
            get
            {
                return _y;
            }
            set
            { _y = value; }
        }

        public Suit Suit { get => _suit; set => _suit = value; }
        public Rank Rank { get => _rank; set => _rank = value; }
        public abstract Suit SuitType
        {
            get;
        }
        public abstract Rank RankType
        {
            get;
        }

        public abstract Bitmap MyBitMap();
        public abstract void Draw(int x, int y);
        public void DrawFaceDown(int x, int y)
        {
            SwinGame.DrawBitmap(SwinGame.BitmapNamed("BackOfCard"), x, y);
        }
        public bool IsCardJack()
        {
            return Rank == Rank.Jack;
        }

    }
    public class CardDeck
    {

        private Random _random = new Random();
        private List<Card> _deckOfCards;
        public Random Random { get => _random; set => _random = value; }

        public List<Card> DeckOfCard
        {
            get
            { return _deckOfCards; }
            set
            {
                _deckOfCards = value;
            }
        }

        public CardDeck()
        {
            _deckOfCards = new List<Card>();
            InitializeDeck();
        }

        public void InitializeDeck()
        {
            // Creates card objects. NEVER MAKES AGAIN. This function is the only one that has the word "new" in it. No where else.
            Array SuitArray = Enum.GetValues(typeof(Suit));//
            foreach (Suit s in SuitArray)
            {
                for (int i = 0; i <= 11; i++)
                {
                    Rank r = (Rank)i;
                    NormalCard NewCard = new NormalCard(s, r);

                    DeckOfCard.Add(NewCard);
                }
            }
            //DeckOfCard.Add(new JackOneEye(Suit.Heart));
            //DeckOfCard.Add(new JackOneEye(Suit.Spades));
            //DeckOfCard.Add(new JackTwoEyes(Suit.Clubs));
            //DeckOfCard.Add(new JackTwoEyes(Suit.Diamonds));
            // 52 cards in total.
            foreach (Suit s in SuitArray)
            {
                for (int i = 0; i <= 11; i++)
                {
                    Rank r = (Rank)i;
                    NormalCard NewCard = new NormalCard(s, r);

                    DeckOfCard.Add(NewCard);
                }
            }
            DeckOfCard.Add(new NormalCard(Suit.Diamonds, Rank.Five));
            DeckOfCard.Add(new NormalCard(Suit.Clubs, Rank.Five));
            DeckOfCard.Add(new NormalCard(Suit.Spades, Rank.Six));
            DeckOfCard.Add(new NormalCard(Suit.Diamonds, Rank.Seven));
            //DeckOfCard.Add(new JackOneEye(Suit.Heart));
            //DeckOfCard.Add(new JackOneEye(Suit.Spades));
            //// 100 cards in total.
        }
        public void ShuffleDeck()
        {
            // Randomises the deck..

            List<Card> _newDeck = new List<Card>();  //Create a temporary list of cards (empty)
            while (_deckOfCards.Count > 0)
            {
                int k = Random.Next(0, _deckOfCards.Count);  //get a random index in the ;current; deck
                _newDeck.Add(_deckOfCards[k]);     // add that card to the new deck
                _deckOfCards.RemoveAt(k);         // remove from the old deck
            }
            _deckOfCards = _newDeck;            // replace current deck with new "shuffled" deck

        }

        public Card DealCard()
        {
            // If _cards.Count > 0 //safety check...
            // Card cardToRemove = _cards[0];
            // _cards.RemoveAt(0);
            // return cardToRemove
            if (_deckOfCards.Count > 0)
            {
                Card CardToRemove = _deckOfCards[0];
                _deckOfCards.RemoveAt(0);
                return CardToRemove;
            }
            return null;
        }

        public void AddCards(Stack<Card> cardsToAdd)
        {
            // _cards.AddRange(cardsToAdd);
            // OR you can do soethign like
            // Foreach (Card c in cardsToAdd)
            // _cards.add(c);
            foreach (Card c in cardsToAdd)
            {
                _deckOfCards.Add(c);
            }
        }
    }
    public class Cell
    {

        public const int CELL_WIDTH = 70;
        public const int CELL_HEIGHT = 120;
        private int _x;
        private int _y;
        private int _column;
        private int _row;
        private Chip _chip;
        private Card _cardInCell;
        private bool _selected = false;
        private bool _isFaceUp;
        private PlayerColors _playerColor;

        public int Column { get => _column; set => _column = value; }
        public int Row { get => _row; set => _row = value; }
        public Chip Chip { get => _chip; set => _chip = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public Card CardInCell { get => _cardInCell; set => _cardInCell = value; }
        public bool IsFaceUp { get => _isFaceUp; set => _isFaceUp = value; }
        public bool Selected { get => _selected; set => _selected = value; }
        public PlayerColors PlayerColor { get => _playerColor; set => _playerColor = value; }


        /// <summary>
        /// Constructor of the cell
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="c"></param>
        /// <param name="chip"></param>
        public Cell(int x, int y, Card c, Chip chip)
        {
            X = x;
            Y = y;
            CardInCell = c;
            _chip = chip;
            _isFaceUp = true;
        }

        /// <summary>
        /// Check whether the cell has a chip in it.
        /// </summary>
        /// <returns></returns>
        public bool IsChipAt()
        {
            return (Chip != null);
        }

        /// <summary>
        /// Remove a chip from the cell
        /// </summary>
        public void RemoveChip()
        {
            Chip = null;
        }
        public void PlaceChip(PlayerColors color)
        {
            Chip = new Chip(color);
        }
        public void DrawTheChipInTheCell()
        {
            Chip.Draw(X, Y);
        }
        /// <summary>
        /// Check whether the cell is chosen
        /// </summary>
        public void MarkCellSelected()
        {
            Selected = true;
        }
        public void MarkCellUnSelected()
        {
            Selected = false;
        }
        public bool IsSelected()
        {
            return Selected;
        }
        /// <summary>
        ///Draw outline of the cell of the board when player chooses it to mark the chip on
        /// </summary>
        public void DrawOutLineInBoard()
        {

            float finalX = X;
            float finalY = Y;
            if ((int)(Y / (CELL_HEIGHT / 2)) == 9)
            {
                SwinGame.DrawRectangle(Color.Orange, finalX, finalY, Card.CARD_WIDTH, Card.CARD_HEIGHT);
                SwinGame.DrawRectangle(Color.Orange, finalX - 1, finalY - 1, Card.CARD_WIDTH, Card.CARD_HEIGHT);
                SwinGame.DrawRectangle(Color.Orange, finalX + 1, finalY + 1, Card.CARD_WIDTH, Card.CARD_HEIGHT);
                SwinGame.DrawRectangle(Color.Orange, finalX + 2, finalY + 2, Card.CARD_WIDTH, Card.CARD_HEIGHT);
            }
            else
            {
                SwinGame.DrawRectangle(Color.Orange, finalX, finalY, Card.CARD_WIDTH, CELL_HEIGHT / 2);
                SwinGame.DrawRectangle(Color.Orange, finalX - 1, finalY - 1, Card.CARD_WIDTH, CELL_HEIGHT / 2);
                SwinGame.DrawRectangle(Color.Orange, finalX + 1, finalY + 1, Card.CARD_WIDTH, CELL_HEIGHT / 2);
                SwinGame.DrawRectangle(Color.Orange, finalX + 2, finalY + 2, Card.CARD_WIDTH, CELL_HEIGHT / 2);

            }
        }
        /// <summary>
        ///Draw OutLine for cells in hand. 
        /// </summary>
        public void DraOutLineInHand()
        {
            SwinGame.DrawRectangle(Color.Orange, X, Y, Card.CARD_WIDTH, Card.CARD_HEIGHT);
            SwinGame.DrawRectangle(Color.Orange, X + 1, Y + 1, Card.CARD_WIDTH, Card.CARD_HEIGHT);
            SwinGame.DrawRectangle(Color.Orange, X - 1, Y - 1, Card.CARD_WIDTH, Card.CARD_HEIGHT);
            SwinGame.DrawRectangle(Color.Orange, X + 2, Y + 2, Card.CARD_WIDTH, Card.CARD_HEIGHT);

        }
        /// <summary>
        /// Draw Cell. This one draw the card in hand, the face of the card depends on IsFaceUp.
        /// </summary>
        public void Draw()
        {
            if (_isFaceUp)
                CardInCell.Draw(X, Y);
            else
                CardInCell.DrawFaceDown(X, Y);
        }
        /// <summary>
        /// Save information of the cell, including isSelected, isFaceUp, suit and rank.
        /// </summary>
        /// <param name="writter"></param>
        public void Save(StreamWriter writter)
        {
            if (Chip == null)
            { writter.WriteLine("null"); }
            else
            {
                writter.WriteLine(Chip.ChipColor);
            }
            writter.WriteLine(Selected);
            writter.WriteLine(_isFaceUp);
            writter.WriteLine(CardInCell.Suit);
            writter.WriteLine(CardInCell.Rank);
        }
        /// <summary>
        /// Load each components of the cell from the information which is saved before.
        /// </summary>
        /// <param name="reader"></param>
        public void Load(StreamReader reader)
        {
            string[] text = new string[5];
            for (int i = 0; i < 5; i++)
            {
                text[i] = reader.ReadLine();
            }
            if (text[0] == "null")
                Chip = null;
            else
                Chip = new Chip((PlayerColors)Enum.Parse(typeof(PlayerColors), text[0]));
            if (text[1] == "True")
                Selected = true;
            else
                Selected = false;
            if (text[2] == "True")
                _isFaceUp = true;
            else
                _isFaceUp = false;
            Suit suit = (Suit)Enum.Parse(typeof(Suit), text[3]);
            Rank rank = (Rank)Enum.Parse(typeof(Rank), text[4]);
            if (rank == Rank.Jack)
            {
                if (suit == Suit.Spades || suit == Suit.Heart)
                {
                    CardInCell = new JackOneEye(suit, rank);
                }
                else
                    CardInCell = new JackTwoEyes(suit, rank);
            }
            else
                CardInCell = new NormalCard(suit, rank);

        }

    }
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

            for (int i = 0; i < 5; i++)
            {
                PlayDeck.DeckOfCard.Add(new JackOneEye(Suit.Spades, Rank.Jack));
                PlayDeck.DeckOfCard.Add(new JackOneEye(Suit.Heart, Rank.Jack));

            }
            for (int i = 0; i < 10; i++)
            {
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
                SwinGame.DrawBitmap(SwinGame.BitmapNamed("DiscardBin.png"), (int)(Card.CARD_WIDTH * 12.5) + 10, Card.CARD_HEIGHT * 6 + 30);
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
                program.ChangeScreenViewing(ProgramState.MENU);
        }
        /// <summary>
        /// When 1 player has won the game, there will be 3 choices for user to choose
        /// This method handles the input from user.
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="program"></param>
        public void HandleInputInWinScreen(Point2D pt, Program program)
        {
            if (SwinGame.PointInRect(pt, TOP_BUTTON_X, TOP_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT))
                ChangeState(GameState.Done);
            if (SwinGame.PointInRect(pt, MIDDLE_BUTTON_X, MIDDLE_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT))
            {
                program.ChangeScreenViewing(ProgramState.MENU);
                ChangeState(GameState.Done);
            }
            if (SwinGame.PointInRect(pt, BOTTOM_BUTTON_X, BOTTOM_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT))
            {
                program.ChangeScreenViewing(ProgramState.QUITTING);
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
            if (PlayDeck.DeckOfCard.Count == 0)
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
        {
            if (Board.Cells != null)
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
    public enum GameState
    {
        SelectingCardInHand,
        SelectingTheCellInBoard,
        Done,
        TemporaryState,
        EndGame

    }
    public class Hand : IHaveCells
    {
        private int _STARTX;
        private int _STARTY;
        private int _XDISTANCE;
        private int _YDISTANCE;

        private List<Cell> _cells = new List<Cell>();
        public List<Cell> Cells { get => _cells; set => _cells = value; }

        //private List<Card> _cards;
        //private int _indexOfPlayer;
        //public List<Card> Cards { get => _cards; set => _cards = value; }
        //public int IndexOfPlayer { get => _indexOfPlayer; set => _indexOfPlayer = value; }

        public Hand(int x, int y, int xDistance, int yDistance, CardDeck carddeck)
        {
            _STARTX = x;
            _STARTY = y;
            _XDISTANCE = xDistance;
            _YDISTANCE = yDistance;
            Cells = new List<Cell>();
            for (int i = 0; i < 7; i++)
            {
                int finalX = x + xDistance * i;
                int finalY = y + yDistance * i;
                Cell newCell = new Cell(finalX, finalY, carddeck.DealCard(), null);
                newCell.Column = i;
                Cells.Add(newCell);
            }

        }
        public void RemoveACardInChosenCellAndAddNewOne(Cell cell, Card card)
        {
            cell.CardInCell = null;
            cell.CardInCell = card;
        }

        /// <summary>
        /// Draw the cards in the hand of the player on the screen.
        /// </summary>
        public void DrawCells()
        {
            foreach (Cell c in Cells)
            {
                c.Draw();
                if (c.IsChipAt())
                    c.DrawTheChipInTheCell();
                if (c.IsSelected())
                    c.DraOutLineInHand();
            }

        }

        /// <summary>
        /// Return the cell in the hand and draw outline it.
        /// </summary>
        /// <param name="pt"> Need the point information</param>
        /// <returns></returns>
        public Cell FetchACell(Point2D pt)
        {
            foreach (Cell c in Cells)
            {
                if (SwinGame.PointInRect(pt, c.X, c.Y, Card.CARD_WIDTH, Card.CARD_HEIGHT))
                {
                    c.DraOutLineInHand();

                    return c;
                }
            }
            return null;
        }
        /// <summary>
        /// Return a cell by its column in Hand
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public Cell FetchACell(int column)
        {
            foreach (Cell c in Cells)
            {
                if (c.Column == column)
                    return c;
            }
            return null;
        }
        /// <summary>
        /// Turn all the cards in hand up side down
        /// </summary>
        public void TurnOverAllCardsInHand()
        {
            foreach (Cell cell in Cells)
            {
                cell.IsFaceUp = !cell.IsFaceUp;
            }
        }
        public void SaveCells(StreamWriter writer)
        {
            foreach (Cell cell in Cells)
            {
                cell.Save(writer);
            }
        }
        public void LoadCells(StreamReader reader)
        {
            foreach (Cell cell in Cells)
            {
                cell.Load(reader);
            }
        }
        public void ReleaseCardInCells()
        {
            foreach (Cell cell in Cells)
            {
                cell.CardInCell = null;
            }
        }

    }
    interface IHaveCells
    {
        void DrawCells();
        Cell FetchACell(Point2D pt);
        void LoadCells(StreamReader reader);
        void SaveCells(StreamWriter writter);
        void ReleaseCardInCells();
    }
    public class JackOneEye : Card
    {
        public JackOneEye(Suit s, Rank rank) : base()
        {
            Suit = s;
            Rank = rank;

        }
        public override Bitmap MyBitMap()
        {
            switch (SuitType)
            {
                case (Suit.Spades):
                    {
                        return SwinGame.BitmapNamed("JackSpades");
                        break;
                    }
                case (Suit.Heart):
                    {
                        return SwinGame.BitmapNamed("JackHeart");
                        break;
                    }
                default:
                    break;
            }
            return null;
        }
        public override void Draw(int x, int y)
        {
            SwinGame.DrawBitmap(this.MyBitMap(), x, y);

        }


        public override Rank RankType
        {
            get
            {
                return Rank.Jack;
            }
        }
        public override Suit SuitType
        {
            get
            {
                return this.Suit;
            }
        }

    }
    class JackTwoEyes : Card
    {
        private string _name = "JackTwoEyes";

        public JackTwoEyes(Suit s, Rank rank) : base()
        {
            Suit = s;
            Rank = rank;
        }
        public override Bitmap MyBitMap()
        {

            switch (SuitType)
            {
                case (Suit.Clubs):
                    {
                        return SwinGame.BitmapNamed("JackClubs");
                        break;
                    }
                case (Suit.Diamonds):
                    {
                        return SwinGame.BitmapNamed("JackDiamonds");
                        break;
                    }
                default:
                    break;
            }
            return null;
        }
        public override void Draw(int x, int y)
        {
            SwinGame.DrawBitmap(this.MyBitMap(), x, y);

        }

        public override Rank RankType
        {
            get
            {
                return Rank.Jack;
            }
        }
        public override Suit SuitType
        {
            get
            {
                return this.Suit;
            }
        }

        public string Name { get => _name; set => _name = value; }
    }
    public class Menu
    {
        private const int TOP_BUTTON_X = 230;
        private const int TOP_BUTTON_Y = 350;
        private const int MIDDLE_BUTTON_X = 600;
        private const int BUTTON_WIDTH = 150;
        private const int BUTTON_HEIGHT = 100;
        private const int MIDDLE_BUTTON_Y = 350;
        private const int BOTTOM_BUTTON_X = 400;
        private const int BOTTOM_BUTTON_Y = 500;
        /// <summary>
        /// Handle game in Menu Screen (SECOND level)
        /// </summary>
        /// <param name="point"></param>
        /// <param name="program"></param>
        public void HandleTheGameInMenu(Point2D point, Program program)
        {
            if (SwinGame.PointInRect(point, TOP_BUTTON_X, TOP_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT))
            {
                //screen.GameMaster.ReleaseGame();

                //screen.InitializeTheGame();
                bool WasTheGameIsReleased = false;
                if (program.GameMaster != null)
                {
                    program.ReleaseGame();
                    WasTheGameIsReleased = true;
                }
                program.InitializeTheGame();
                if (WasTheGameIsReleased)
                    program.GameMaster.SetUpGame();
                program.ChangeScreenViewing(ProgramState.PLAYINGGAME);
            }
            if (SwinGame.PointInRect(point, BOTTOM_BUTTON_X, BOTTOM_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT))
            {
                program.InitializeTheGame();
                //screen.GameMaster.ReleaseCardInCells();
                program.GameMaster.LoadGame();
                program.ChangeScreenViewing(ProgramState.PLAYINGGAME);
            }
            if (SwinGame.PointInRect(point, MIDDLE_BUTTON_X, MIDDLE_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT))
            {
                program.ChangeScreenViewing(ProgramState.QUITTING);
            }

        }
        public void DrawMenu()
        {
            SwinGame.DrawBitmap(SwinGame.BitmapNamed("Background"), 0, 0);
            SwinGame.DrawBitmap(SwinGame.BitmapNamed("Newgame"), TOP_BUTTON_X, TOP_BUTTON_Y);
            SwinGame.DrawBitmap(SwinGame.BitmapNamed("Quit"), MIDDLE_BUTTON_X, MIDDLE_BUTTON_Y);
            SwinGame.DrawBitmap(SwinGame.BitmapNamed("LoadGame"), BOTTOM_BUTTON_X, BOTTOM_BUTTON_Y);

        }
    }
    public class NormalCard : Card
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="s"></param>
        /// <param name="r"></param>
        public NormalCard(Suit s, Rank r) : base()
        {
            Suit = s;
            Rank = r;
        }
        public override Rank RankType
        {
            get
            {
                return base.Rank;
            }
        }
        public override Suit SuitType
        {
            get
            {
                return base.Suit;
            }
        }
        public override Bitmap MyBitMap()
        {
            switch (SuitType)
            {

                case (Suit.Clubs):
                    {
                        switch (RankType)
                        {
                            case (Rank.Two):
                                {
                                    return SwinGame.BitmapNamed("TwoClubs");
                                    break;
                                }
                            case (Rank.Three):
                                {
                                    return SwinGame.BitmapNamed("ThreeClubs");
                                    break;
                                }
                            case (Rank.Four):
                                {
                                    return SwinGame.BitmapNamed("FourClubs");
                                    break;
                                }
                            case (Rank.Five):
                                {
                                    return SwinGame.BitmapNamed("FiveClubs");
                                    break;
                                }
                            case (Rank.Six):
                                {
                                    return SwinGame.BitmapNamed("SixClubs");
                                    break;
                                }
                            case (Rank.Seven):
                                {
                                    return SwinGame.BitmapNamed("SevenClubs");
                                    break;
                                }
                            case (Rank.Eight):
                                {
                                    return SwinGame.BitmapNamed("EightClubs");
                                    break;
                                }
                            case (Rank.Nine):
                                {
                                    return SwinGame.BitmapNamed("NineClubs");
                                    break;
                                }
                            case (Rank.Ten):
                                {
                                    return SwinGame.BitmapNamed("TenClubs");
                                    break;
                                }
                            case (Rank.Queen):
                                {
                                    return SwinGame.BitmapNamed("QueenClubs");
                                    break;
                                }
                            case (Rank.King):
                                {
                                    return SwinGame.BitmapNamed("KingClubs");
                                    break;
                                }
                            case (Rank.Ace):
                                {
                                    return SwinGame.BitmapNamed("AceClubs");
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case (Suit.Diamonds):
                    {
                        switch (RankType)
                        {
                            case (Rank.Two):
                                {
                                    return SwinGame.BitmapNamed("TwoDiamonds");
                                    break;
                                }
                            case (Rank.Three):
                                {
                                    return SwinGame.BitmapNamed("ThreeDiamonds");
                                    break;
                                }
                            case (Rank.Four):
                                {
                                    return SwinGame.BitmapNamed("FourDiamonds");
                                    break;
                                }
                            case (Rank.Five):
                                {
                                    return SwinGame.BitmapNamed("FiveDiamonds");
                                    break;
                                }
                            case (Rank.Six):
                                {
                                    return SwinGame.BitmapNamed("SixDiamonds");
                                    break;
                                }
                            case (Rank.Seven):
                                {
                                    return SwinGame.BitmapNamed("SevenDiamonds");
                                    break;
                                }
                            case (Rank.Eight):
                                {
                                    return SwinGame.BitmapNamed("EightDiamonds");
                                    break;
                                }
                            case (Rank.Nine):
                                {
                                    return SwinGame.BitmapNamed("NineDiamonds");
                                    break;
                                }
                            case (Rank.Ten):
                                {
                                    return SwinGame.BitmapNamed("TenDiamonds");
                                    break;
                                }
                            case (Rank.Queen):
                                {
                                    return SwinGame.BitmapNamed("QueenDiamonds");
                                    break;
                                }
                            case (Rank.King):
                                {
                                    return SwinGame.BitmapNamed("KingDiamonds");
                                    break;
                                }
                            case (Rank.Ace):
                                {
                                    return SwinGame.BitmapNamed("AceDiamonds");
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case (Suit.Spades):
                    {
                        switch (RankType)
                        {
                            case (Rank.Two):
                                {
                                    return SwinGame.BitmapNamed("TwoSpades");
                                    break;
                                }
                            case (Rank.Three):
                                {
                                    return SwinGame.BitmapNamed("ThreeSpades");
                                    break;
                                }
                            case (Rank.Four):
                                {
                                    return SwinGame.BitmapNamed("FourSpades");
                                    break;
                                }
                            case (Rank.Five):
                                {
                                    return SwinGame.BitmapNamed("FiveSpades");
                                    break;
                                }
                            case (Rank.Six):
                                {
                                    return SwinGame.BitmapNamed("SixSpades");
                                    break;
                                }
                            case (Rank.Seven):
                                {
                                    return SwinGame.BitmapNamed("SevenSpades");
                                    break;
                                }
                            case (Rank.Eight):
                                {
                                    return SwinGame.BitmapNamed("EightSpades");
                                    break;
                                }
                            case (Rank.Nine):
                                {
                                    return SwinGame.BitmapNamed("NineSpades");
                                    break;
                                }
                            case (Rank.Ten):
                                {
                                    return SwinGame.BitmapNamed("TenSpades");
                                    break;
                                }
                            case (Rank.Queen):
                                {
                                    return SwinGame.BitmapNamed("QueenSpades");
                                    break;
                                }
                            case (Rank.King):
                                {
                                    return SwinGame.BitmapNamed("KingSpades");
                                    break;
                                }
                            case (Rank.Ace):
                                {
                                    return SwinGame.BitmapNamed("AceSpades");
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }

                case (Suit.Heart):
                    {
                        switch (RankType)
                        {
                            case (Rank.Two):
                                {
                                    return SwinGame.BitmapNamed("TwoHeart");
                                    break;
                                }
                            case (Rank.Three):
                                {
                                    return SwinGame.BitmapNamed("ThreeHeart");
                                    break;
                                }
                            case (Rank.Four):
                                {
                                    return SwinGame.BitmapNamed("FourHeart");
                                    break;
                                }
                            case (Rank.Five):
                                {
                                    return SwinGame.BitmapNamed("FiveHeart");
                                    break;
                                }
                            case (Rank.Six):
                                {
                                    return SwinGame.BitmapNamed("SixHeart");
                                    break;
                                }
                            case (Rank.Seven):
                                {
                                    return SwinGame.BitmapNamed("SevenHeart");
                                    break;
                                }
                            case (Rank.Eight):
                                {
                                    return SwinGame.BitmapNamed("EightHeart");
                                    break;
                                }
                            case (Rank.Nine):
                                {
                                    return SwinGame.BitmapNamed("NineHeart");
                                    break;
                                }
                            case (Rank.Ten):
                                {
                                    return SwinGame.BitmapNamed("TenHeart");
                                    break;
                                }
                            case (Rank.Queen):
                                {
                                    return SwinGame.BitmapNamed("QueenHeart");
                                    break;
                                }
                            case (Rank.King):
                                {
                                    return SwinGame.BitmapNamed("KingHeart");
                                    break;
                                }
                            case (Rank.Ace):
                                {
                                    return SwinGame.BitmapNamed("AceHeart");
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }

                default:
                    break;


            }
            return null;
        }

        public override void Draw(int x, int y)

        {
            SwinGame.DrawBitmap(this.MyBitMap(), x, y);
        }



    }
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
                        if (ChosenCellInHand.CardInCell.GetType().Name == "JackOneEye")
                        {

                            if ((chosenCellInBoard.IsChipAt()) && (chosenCellInBoard.Chip.ChipColor == Opponent.PlayerColor))
                            {
                                Board.TakingAction(this, chosenCellInBoard, "remove");
                            }

                        }
                        if (ChosenCellInHand.CardInCell.GetType().Name == "JackTwoEyes")
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
                                    // cell.PlaceChip(PlayerColor);
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
    public enum PlayerColors
    {
        Green,
        Blue,
        Red
    }
    public class Program
    {
        private GameMaster _gameMaster;
        private ProgramState _programState;
        private Menu _menu;
        public GameMaster GameMaster { get => _gameMaster; set => _gameMaster = value; }
        public ProgramState ProgramState { get => _programState; set => _programState = value; }
        public Menu Menu { get => _menu; set => _menu = value; }

        public Program()
        {
            _programState = ProgramState.MENU;
            Menu = new Menu();
        }
        /// <summary>
        /// Create Instance of Game Master. It only happens once.
        /// </summary>
        public void InitializeTheGame()
        {
            _gameMaster = GameMaster.Instance;
        }
        /// <summary>
        /// Remove cells in Board and Player's Hand.
        /// </summary>
        public void ReleaseGame()
        {
            _gameMaster.Board.Cells.Clear();
            _gameMaster.Players[0].PlayerHand.Cells.Clear();
            _gameMaster.Players[1].PlayerHand.Cells.Clear();
        }

        /// <summary>
        /// Loads the resources.
        /// </summary>
        public void LoadResources()
        {
            SwinGame.LoadBitmapNamed("TwoClubs", "c02.bmp");
            SwinGame.LoadBitmapNamed("ThreeClubs", "c03.bmp");
            SwinGame.LoadBitmapNamed("FourClubs", "c04.bmp");
            SwinGame.LoadBitmapNamed("FiveClubs", "c05.bmp");
            SwinGame.LoadBitmapNamed("SixClubs", "c06.bmp");
            SwinGame.LoadBitmapNamed("SevenClubs", "c07.bmp");
            SwinGame.LoadBitmapNamed("EightClubs", "c08.bmp");
            SwinGame.LoadBitmapNamed("NineClubs", "c09.bmp");
            SwinGame.LoadBitmapNamed("TenClubs", "c10.bmp");
            SwinGame.LoadBitmapNamed("QueenClubs", "c12.bmp");
            SwinGame.LoadBitmapNamed("KingClubs", "c13.bmp");
            SwinGame.LoadBitmapNamed("AceClubs", "c01.bmp");

            SwinGame.LoadBitmapNamed("TwoDiamonds", "d02.bmp");
            SwinGame.LoadBitmapNamed("ThreeDiamonds", "d03.bmp");
            SwinGame.LoadBitmapNamed("FourDiamonds", "d04.bmp");
            SwinGame.LoadBitmapNamed("FiveDiamonds", "d05.bmp");
            SwinGame.LoadBitmapNamed("SixDiamonds", "d06.bmp");
            SwinGame.LoadBitmapNamed("SevenDiamonds", "d07.bmp");
            SwinGame.LoadBitmapNamed("EightDiamonds", "d08.bmp");
            SwinGame.LoadBitmapNamed("NineDiamonds", "d09.bmp");
            SwinGame.LoadBitmapNamed("TenDiamonds", "d10.bmp");
            SwinGame.LoadBitmapNamed("QueenDiamonds", "d12.bmp");
            SwinGame.LoadBitmapNamed("KingDiamonds", "d13.bmp");
            SwinGame.LoadBitmapNamed("AceDiamonds", "d01.bmp");

            SwinGame.LoadBitmapNamed("TwoSpades", "s02.bmp");
            SwinGame.LoadBitmapNamed("ThreeSpades", "s03.bmp");
            SwinGame.LoadBitmapNamed("FourSpades", "s04.bmp");
            SwinGame.LoadBitmapNamed("FiveSpades", "s05.bmp");
            SwinGame.LoadBitmapNamed("SixSpades", "s06.bmp");
            SwinGame.LoadBitmapNamed("SevenSpades", "s07.bmp");
            SwinGame.LoadBitmapNamed("EightSpades", "s08.bmp");
            SwinGame.LoadBitmapNamed("NineSpades", "s09.bmp");
            SwinGame.LoadBitmapNamed("TenSpades", "s10.bmp");
            SwinGame.LoadBitmapNamed("QueenSpades", "s12.bmp");
            SwinGame.LoadBitmapNamed("KingSpades", "s13.bmp");
            SwinGame.LoadBitmapNamed("AceSpades", "s01.bmp");

            SwinGame.LoadBitmapNamed("TwoHeart", "h02.bmp");
            SwinGame.LoadBitmapNamed("ThreeHeart", "h03.bmp");
            SwinGame.LoadBitmapNamed("FourHeart", "h04.bmp");
            SwinGame.LoadBitmapNamed("FiveHeart", "h05.bmp");
            SwinGame.LoadBitmapNamed("SixHeart", "h06.bmp");
            SwinGame.LoadBitmapNamed("SevenHeart", "h07.bmp");
            SwinGame.LoadBitmapNamed("EightHeart", "h08.bmp");
            SwinGame.LoadBitmapNamed("NineHeart", "h09.bmp");
            SwinGame.LoadBitmapNamed("TenHeart", "h10.bmp");
            SwinGame.LoadBitmapNamed("QueenHeart", "h12.bmp");
            SwinGame.LoadBitmapNamed("KingHeart", "h13.bmp");
            SwinGame.LoadBitmapNamed("AceHeart", "h01.bmp");

            SwinGame.LoadBitmapNamed("JackSpades", "s11.bmp");
            SwinGame.LoadBitmapNamed("JackHeart", "h11.bmp");
            SwinGame.LoadBitmapNamed("JackClubs", "c11.bmp");
            SwinGame.LoadBitmapNamed("JackDiamonds", "d11.bmp");

            ///Load figures for background:
            SwinGame.LoadBitmapNamed("Background", "Background.png");
            SwinGame.LoadBitmapNamed("Background1", "Background1.png");
            SwinGame.LoadBitmapNamed("GreenPlayer", "GreenPlayer.png");
            SwinGame.LoadBitmapNamed("BluePlayer", "BluePlayer.png");

            //Load figure for chips
            SwinGame.LoadBitmapNamed("GreenChip", "GreenChip.png");
            SwinGame.LoadBitmapNamed("BlueChip", "BlueChip.png");
            SwinGame.LoadBitmapNamed("RedChip", "BlueChip.png");

            SwinGame.LoadBitmapNamed("Newgame", "New_game.png");
            SwinGame.LoadBitmapNamed("TheBoard", "TheBoard.png");
            SwinGame.LoadFontNamed("arial", "arial.ttf", 25);
            SwinGame.LoadBitmapNamed("BackOfCard", "BackOfCard.bmp");
            SwinGame.LoadBitmapNamed("Quit", "Quit.png");

            SwinGame.LoadBitmapNamed("LoadGame", "LoadGame.png");

            //Draw button
            SwinGame.LoadBitmapNamed("Redo", "Redo.png");
            SwinGame.LoadBitmapNamed("ResetButton", "ResetButton.png");
            SwinGame.LoadBitmapNamed("SaveButton", "SaveButton.png");
            SwinGame.LoadBitmapNamed("MainMenu", "MainMenu.png");
            //Draw button for Win Screen

            SwinGame.LoadBitmapNamed("MainMenuBig", "MainMenuBig.png");
            SwinGame.LoadBitmapNamed("BackToGame", "BackToGame.png");
        }
        /// <summary>
        /// Handle input from user in the TOP level. Decide which screen state of program to display
        /// </summary>
        public void HandleTheInput()
        {
            SwinGame.ProcessEvents();
            switch (_programState)
            {
                case (MyGame.ProgramState.MENU):
                    {
                        if (SwinGame.MouseClicked(MouseButton.LeftButton))
                            Menu.HandleTheGameInMenu(SwinGame.MousePosition(), this);
                        break;

                    }
                case (MyGame.ProgramState.PLAYINGGAME):
                    {
                        HandleGame();
                        if (GameMaster.Currentstate == GameState.TemporaryState)

                        {
                            GameMaster.ChangeState(GameState.SelectingCardInHand);
                        }
                        break;
                    }
                default:
                    break;

            }
        }
        /// <summary>
        /// Handl in Playing Screen (SECOND LEVEL)
        /// </summary>
        public void HandleGame()
        {
            if (SwinGame.MouseClicked(MouseButton.LeftButton))
            {
                GameMaster.TakeTheTurn(SwinGame.MousePosition());
                GameMaster.RedoButton(SwinGame.MousePosition());
                GameMaster.ResetButton(SwinGame.MousePosition());
                GameMaster.SaveButton(SwinGame.MousePosition());
                GameMaster.MainMenuButton(SwinGame.MousePosition(), this);
                if (GameMaster.Currentstate == GameState.EndGame)
                {
                    GameMaster.HandleInputInWinScreen(SwinGame.MousePosition(), this);
                }
            }
        }
        /// <summary>
        /// Draw the Screen, depending on the Program State.
        /// </summary>
        public void DrawTheScreen()
        {
            SwinGame.ClearScreen(Color.White);

            switch (_programState)
            {
                case ProgramState.MENU:
                    Menu.DrawMenu();
                    break;
                case ProgramState.PLAYINGGAME:
                    GameMaster.DrawEverything();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Change the view screen depends on the program state
        /// </summary>
        /// <param name="screenState"></param>
        public void ChangeScreenViewing(ProgramState screenState)
        {
            ProgramState = screenState;
        }
    }
    public enum ProgramState
    {
        MENU,
        PLAYINGGAME,
        QUITTING

    }
    public enum Rank
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Queen,
        King,
        Ace,
        Jack


    }
    // <summary>
    // The suit of cards in the game.
    public enum Suit
    {
        Clubs,
        Diamonds,
        Spades,
        Heart

    }
    public class Chip
    {
       // public const int BOTTOM_LEFT_CELL_X = 45;
        //public const int BOTTOM_LEFT_CELL_Y = 448;
        //public const int CELL_WIDTH = 58;

        //private int _x;
        //private int _y;
        private PlayerColors _chipColor;
       // private bool _isChosen;

        public Chip(PlayerColors color)
        {
            _chipColor = color;
        }

        //public int X { get => _x; set => _x = value; }
        //public int Y { get => _y; set => _y = value; }
        public PlayerColors ChipColor { get => _chipColor; set => _chipColor = value; }

        public Bitmap MyBitMap()
        {
            switch (ChipColor)
            {
                case (PlayerColors.Green):
                    {
                        return SwinGame.BitmapNamed("GreenChip");
                        break;
                    }
                case (PlayerColors.Blue):
                    {
                        return SwinGame.BitmapNamed("BlueChip");
                        break;
                    }
                case (PlayerColors.Red):
                    {
                        return SwinGame.BitmapNamed("RedChip");
                    }
                default:
                    break;
            }
            return null;
        }
        public void Draw(int x, int y)
        {
            SwinGame.DrawBitmap(MyBitMap(), x+15, y+10);
        }
    }
}
