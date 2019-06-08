using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
//<summary>
// The cells represent the rectangular of each card in the board.
namespace MyGame
{
    public class Cell
    {
       
        public const int CELL_WIDTH = 70;
        public const int CELL_HEIGHT = 120;
        private int _x;
        private int _y;
        private int _column;
        private int _row;
        private Chip _chip ;
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


        //public int CellWidth { get => _cellWidth; set => _cellWidth = value; }
        //public int CellHeight { get => _cellHeight; set => _cellHeight = value; }

        //<summary>
        //Constructor of the cell
        public Cell (int x, int y,Card c, Chip chip)
        {
            X = x;
            Y = y;
            
            //CellWidth = width;
            //CellHeight = height;
            CardInCell = c;
            _chip = chip;
            _isFaceUp = true;
        }
        //<summary>
        //Check whether the cell has a chip in it.
        public bool IsChipAt()
        {
            return (Chip != null);
        }

        //<summary>
        //Remove a chip from the cell
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
            Chip.Draw(X,Y);
        }

        ////Check whether the cell is chosen
        //public bool IsChosen(Point2D pt)
        //{
        //   return SwinGame.PointInRect(pt,X,Y, CELL_WIDTH, (CELL_HEIGHT / 2));
        //}
        public void MarkCellSelected()
        {
            _selected = true;
        }
        public void MarkCellUnSelected()
        {
            _selected = false;
        }
        public bool IsSelected()
        {
            return _selected; 
        }
        

      
        //<summary>
        //Draw outline of the cell when player chooses it to mark the chip on
        public void DrawOutLine()
        {
           
            float finalX = X;// TOP_LEFT_CELL_X + X * CELL_WIDTH;
            float finalY = Y;// TOP_LEFT_CELL_Y + Y * (CELL_HEIGHT / 2);
            if ((int)(Y/60)==9)
            {
                SwinGame.DrawRectangle(Color.Orange, finalX, finalY ,Card.CARD_WIDTH,Card.CARD_HEIGHT);
                SwinGame.DrawRectangle(Color.Orange, finalX - 1, finalY - 1, Card.CARD_WIDTH, Card.CARD_HEIGHT);
                SwinGame.DrawRectangle(Color.Orange, finalX + 1, finalY +1, Card.CARD_WIDTH, Card.CARD_HEIGHT);
                SwinGame.DrawRectangle(Color.Orange, finalX+2, finalY+2 ,Card.CARD_WIDTH,Card.CARD_HEIGHT);
            }
            else
            {
                SwinGame.DrawRectangle(Color.Orange, finalX, finalY, Card.CARD_WIDTH, CELL_HEIGHT/2);
                SwinGame.DrawRectangle(Color.Orange, finalX - 1, finalY - 1, Card.CARD_WIDTH, CELL_HEIGHT/2);
                SwinGame.DrawRectangle(Color.Orange, finalX + 1, finalY + 1, Card.CARD_WIDTH, CELL_HEIGHT/2);
                SwinGame.DrawRectangle(Color.Orange, finalX + 2, finalY + 2, Card.CARD_WIDTH, CELL_HEIGHT/2);

            }
            //SwinGame.DrawText("(" + X.ToString() + "," + Y.ToString() + ")", Color.Black, finalX + 10, finalY + 32);
        }
        public void DraOutLineForHand()
        {
            SwinGame.DrawRectangle(Color.Orange, X, Y, Card.CARD_WIDTH, Card.CARD_HEIGHT);
            SwinGame.DrawRectangle(Color.Orange, X+1, Y+1, Card.CARD_WIDTH, Card.CARD_HEIGHT);
            SwinGame.DrawRectangle(Color.Orange, X -1, Y -1, Card.CARD_WIDTH, Card.CARD_HEIGHT);
            SwinGame.DrawRectangle(Color.Orange, X+2, Y+2, Card.CARD_WIDTH, Card.CARD_HEIGHT);

        }
        public void Draw()
        {
            if (_isFaceUp)
                CardInCell.Draw(X, Y);
            else
                CardInCell.DrawFaceDown(X,Y);
        }
        public void Save(StreamWriter writter)
        {
            //writter.WriteLine(X);
            //writter.WriteLine(Y);
            //writter.WriteLine(Column);
            //writter.WriteLine(Row);
            if (Chip == null)
            { writter.WriteLine("null"); }
            else
            {
                writter.WriteLine(Chip.ChipColor);
            }
            writter.WriteLine(_selected);
            writter.WriteLine(_isFaceUp);
            writter.WriteLine(CardInCell.Suit);
            writter.WriteLine(CardInCell.Rank);
        }
        public void Load(StreamReader reader)
        {
            string[] text = new string[5];
            for (int i=0;i<5;i++)
            {
                text[i] = reader.ReadLine();
            }
            if (text[0] == "null")
                Chip = null;
            else
                Chip = new Chip( (PlayerColors)Enum.Parse(typeof(PlayerColors), text[0]));
            if (text[1] == "True")
                _selected = true;
            else
                _selected = false;
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
}
