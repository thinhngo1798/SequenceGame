using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
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
