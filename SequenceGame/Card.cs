using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
namespace MyGame
{
    public class Card
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
        
        public Card(Suit s,Rank r)
        {
            _suit = s;
            _rank = r;

        }
        public Suit Suit { get => _suit; set => _suit = value; }
        public Rank Rank { get => _rank; set => _rank = value; }
        public Suit SuitType
        {
            get
            {
                return Suit;
            }
        }
        public Rank RankType
        {
            get
            {
                return Rank;
            }
        }

        public Bitmap MyBitMap()
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
                            case (Rank.Jack):
                                {
                                    return SwinGame.BitmapNamed("JackClubs");
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
                            case (Rank.Jack):
                                {
                                    return SwinGame.BitmapNamed("JackDiamonds");
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
                            case (Rank.Jack):
                                {
                                    return SwinGame.BitmapNamed("JackSpades");
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
                            case (Rank.Jack):
                                {
                                    return SwinGame.BitmapNamed("JackHeart");
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
        public void Draw(int x, int y)

        {
            SwinGame.DrawBitmap(this.MyBitMap(), x, y);
        }

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
