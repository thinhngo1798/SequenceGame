using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
namespace MyGame
{
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

        public Hand(int x,int y,int xDistance,int yDistance,CardDeck carddeck)
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
    public void RemoveACardInChosenCellAndAddNewOne(Cell cell,Card card)
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
            { if (c.Column == column)
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
}

