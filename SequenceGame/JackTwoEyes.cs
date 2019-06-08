using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
namespace MyGame
{
    class JackTwoEyes : Card
    {
        private string _name = "JackTwoEyes";

        public JackTwoEyes(Suit s,Rank rank) : base()
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
        public override void Draw(int x,int y)
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
}
