using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
    interface IHaveCells
    {
        void DrawCells();
        Cell FetchACell(Point2D pt);
        void LoadCells(StreamReader reader);
        void SaveCells(StreamWriter writter);
        void ReleaseCardInCells();
    }
}
