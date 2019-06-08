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
        //Change later
        
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
}
