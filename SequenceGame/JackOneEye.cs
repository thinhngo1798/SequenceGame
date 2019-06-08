using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
namespace MyGame
{
    public class JackOneEye : Card
    {
        public JackOneEye(Suit s,Rank rank) : base()
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
        public override void Draw(int x,int y)
        {
            SwinGame.DrawBitmap(this.MyBitMap(),  x, y);

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
}
