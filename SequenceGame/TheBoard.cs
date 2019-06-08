using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;


namespace MyGame
{
    public class TheBoard: IHaveCells
    {
        
        //private const int _WIDTH = 10;
        //private const int _HEIGHT = 10;
        private const int _STARTX=0;
        private const int _STARTY=0;
        private const int _XDISTANCE=70;
        private const int _YDISTANCE=60;
        private const int _NUMBEROFCHIPSTOWIN = 5;
        private const int _SMALLDISTANCE = 10;
        private List<Cell> _cells = new List<Cell>();

        /// <summary>
        /// Create list of cells, cells mean card with position
        /// </summary>
        public List<Cell> Cells { get => _cells; set => _cells = value; }
        /// <summary>
        /// Initializing the board in the construtor
        /// </summary>
        /// <param name="carddeck"></param>
        public TheBoard(CardDeck carddeck)
        { int k = 0;
           for (int i=0;i<100;i++)
            {
                if (k > 9)
                    k = 0;
                int x = _STARTX + k * _XDISTANCE;
                int y = _STARTY+(int)(i/ 10) * (_YDISTANCE);
                Cell resultCell = new Cell(x, y, carddeck.DealCard(), null);
                resultCell.Column = k;
                resultCell.Row = (int)(i / 10);
                Cells.Add(resultCell); 
                k++;
            }
            CommandHistory = new List<string>();
        }
      
        /// <summary>
        /// Checking where there are any sequence in the board in term of current player's color
        /// </summary>
        /// <param name="player"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public bool CheckingWinerOfTheGame(Player player,GameMaster game)
        {
            int i = 1;
            foreach (Cell cell in Cells)
            {
                if (cell.IsChipAt() && (cell.Chip.ChipColor == player.PlayerColor))
                {
                    int currentCellIndex = cell.Column % 10 + cell.Row * 10;
                    for (i = 1; i < _NUMBEROFCHIPSTOWIN; i++)
                    {
                        if ((currentCellIndex + i ) > (Cells.Count()-1))
                        {
                            break;
                        }
                        if ((Cells[currentCellIndex + i].IsChipAt() && (Cells[currentCellIndex + i].Chip.ChipColor == player.PlayerColor)))
                        {
                            if (currentCellIndex / 10 == ((currentCellIndex + i) / 10))
                            {

                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (i == 5)
                    { return true; }
                    for (i = 1; i < _NUMBEROFCHIPSTOWIN; i++)
                    {
                        if ((currentCellIndex + i * 10) > (Cells.Count()-1))
                        {
                            break;
                        }
                        if ((Cells[currentCellIndex + i * 10].IsChipAt() && (Cells[currentCellIndex + i * 10].Chip.ChipColor == player.PlayerColor)))
                        {
                            if (currentCellIndex / 10 != ((currentCellIndex + i*10) / 10))
                            {

                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (i==5)
                    { return true; }
                    for (i = 1; i < _NUMBEROFCHIPSTOWIN; i++)
                    {
                        if ((currentCellIndex + i * (10+1)) >(Cells.Count()-1))
                        {
                            break;
                        }
                        if ((Cells[currentCellIndex + i *( 10 + 1)].IsChipAt() && (Cells[currentCellIndex + i * (10 + 1)].Chip.ChipColor == player.PlayerColor) ))
                        {
                            if (currentCellIndex / 10 != ((currentCellIndex + i*(10+1)) / 10))
                            {

                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                        
                    }
                    if (i == 5)
                    { return true; }
                    for (i = 1; i < _NUMBEROFCHIPSTOWIN; i++)
                    {
                        if ((currentCellIndex + i * (10 - 1)) > (Cells.Count() - 1))
                        {
                            break;
                        }
                        if ((Cells[currentCellIndex + i * (10 - 1)].IsChipAt() && (Cells[currentCellIndex + i * (10 - 1)].Chip.ChipColor == player.PlayerColor)))
                        {
                            if (currentCellIndex / 10 != ((currentCellIndex + i)*(10-1) / 10))
                            {

                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }

                    }
                    if (i == 5)
                    { return true; }

                }
            }
            return false;
        }
        /// <summary>
        /// Draw the board
        /// </summary>
        public void DrawCells()
        {
            foreach (Cell cell in Cells)
            {
                cell.CardInCell.Draw(cell.X, cell.Y);
                if (cell.IsChipAt())
                    cell.DrawTheChipInTheCell();
                if (cell.IsSelected())
                    cell.DrawOutLine();
            }

        }
       
        public void DrawOutLineCell()
        {
            foreach (Cell cell in Cells)
            {
                cell.DrawOutLine();
            }
        }
        /// <summary>
        /// Fetch a cell in the board.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public Cell FetchACell(Point2D pt)
        {
            foreach (Cell c in Cells)
            {
                if ((int)(c.Y / 60) == 9)
                {
                    if (SwinGame.PointInRect(pt, c.X, c.Y, _XDISTANCE, _YDISTANCE*2))
                    {
                        return c;
                    }
                }
                else
                {
                    if (SwinGame.PointInRect(pt, c.X, c.Y, _XDISTANCE, _YDISTANCE))
                    {
                        return c;
                    }

                }
            }
            return null;
        }
        private List<string> _commandHistory;
        public List<string> CommandHistory { get => _commandHistory; set => _commandHistory = value; }

        public void TakingAction(Player player,Cell cellInBoard, string action)
        {
            //Place the current player's color.
            if (action == "place")
            {
                cellInBoard.PlaceChip(player.PlayerColor);
            }
            //Remove the opponent's chip color
            if (action == "remove")
            {
                cellInBoard.RemoveChip();
            }
            int indexOfCellInBoard = cellInBoard.Row * 10 + cellInBoard.Column;
            CommandHistory.Add($"{player.IndexOfPlayer.ToString()},{action},{indexOfCellInBoard.ToString()}");


        }
        public void RedoAction(GameMaster gameMaster)
        {
            if (CommandHistory.Count == 0)
                return;
            string lastAction = CommandHistory[CommandHistory.Count - 1];
            string[] info = lastAction.Split(',');
            if (info[1] == "place")
            {
                TakingAction(gameMaster.PlayersArray[System.Convert.ToInt32(info[0])], Cells[System.Convert.ToInt32(info[2])], "remove");
            }
            else
            {
                TakingAction(gameMaster.PlayersArray[System.Convert.ToInt32(info[0])].Opponent, Cells[System.Convert.ToInt32(info[2])], "place");
            }
            CommandHistory.RemoveAt(CommandHistory.Count - 1);
            CommandHistory.RemoveAt(CommandHistory.Count - 1);
            gameMaster.ChangeTurn();
            gameMaster.TurnHandUpSideDown();
        }
        public void ResetAction(GameMaster gameMaster)
        {
            while (CommandHistory.Count != 0)
            {
                RedoAction(gameMaster);
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
