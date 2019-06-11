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
        private const int X_SMALL_DISTANCE = 15;
        private const int Y_SMALL_DISTANCE = 10;
        private PlayerColors _chipColor;
        public Chip(PlayerColors color)
        {
            _chipColor = color;
        }
        
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
            SwinGame.DrawBitmap(MyBitMap(), x+X_SMALL_DISTANCE, y+Y_SMALL_DISTANCE);
        }
    }
}
