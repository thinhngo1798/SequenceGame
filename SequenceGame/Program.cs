 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
namespace MyGame
{
    public class Program
    {
        private GameMaster _gameMaster;
        private ProgramState _programState;
        private Menu _menu;
        public GameMaster GameMaster { get => _gameMaster; set => _gameMaster = value; }
        public ProgramState ProgramState { get => _programState; set => _programState = value; }
        public Menu Menu { get => _menu; set => _menu = value; }

        public Program()
        {
            _programState = ProgramState.MENU;
            Menu = new Menu(); 
        }
        /// <summary>
        /// Create Instance of Game Master. It only happens once.
        /// </summary>
        public void InitializeTheGame()
        {
            _gameMaster = GameMaster.Instance;           
        }
        /// <summary>
        /// Remove cells in Board and Player's Hand.
        /// </summary>
        public void ReleaseGame()
        {
            _gameMaster.Board.Cells.Clear();
            _gameMaster.Players[0].PlayerHand.Cells.Clear();
            _gameMaster.Players[1].PlayerHand.Cells.Clear();
        }

        /// <summary>
        /// Loads the resources.
        /// </summary>
        public void LoadResources()
        {
            SwinGame.LoadBitmapNamed("TwoClubs","c02.bmp");
            SwinGame.LoadBitmapNamed("ThreeClubs", "c03.bmp");
            SwinGame.LoadBitmapNamed("FourClubs", "c04.bmp");
            SwinGame.LoadBitmapNamed("FiveClubs", "c05.bmp");
            SwinGame.LoadBitmapNamed("SixClubs", "c06.bmp");
            SwinGame.LoadBitmapNamed("SevenClubs", "c07.bmp");
            SwinGame.LoadBitmapNamed("EightClubs", "c08.bmp");
            SwinGame.LoadBitmapNamed("NineClubs", "c09.bmp");
            SwinGame.LoadBitmapNamed("TenClubs", "c10.bmp");
            SwinGame.LoadBitmapNamed("JackClubs", "c11.bmp");
            SwinGame.LoadBitmapNamed("QueenClubs", "c12.bmp");
            SwinGame.LoadBitmapNamed("KingClubs", "c13.bmp");
            SwinGame.LoadBitmapNamed("AceClubs", "c01.bmp");

            SwinGame.LoadBitmapNamed("TwoDiamonds", "d02.bmp");
            SwinGame.LoadBitmapNamed("ThreeDiamonds", "d03.bmp");
            SwinGame.LoadBitmapNamed("FourDiamonds", "d04.bmp");
            SwinGame.LoadBitmapNamed("FiveDiamonds", "d05.bmp");
            SwinGame.LoadBitmapNamed("SixDiamonds", "d06.bmp");
            SwinGame.LoadBitmapNamed("SevenDiamonds", "d07.bmp");
            SwinGame.LoadBitmapNamed("EightDiamonds", "d08.bmp");
            SwinGame.LoadBitmapNamed("NineDiamonds", "d09.bmp");
            SwinGame.LoadBitmapNamed("TenDiamonds", "d10.bmp");
            SwinGame.LoadBitmapNamed("JackDiamonds", "d11.bmp");
            SwinGame.LoadBitmapNamed("QueenDiamonds", "d12.bmp");
            SwinGame.LoadBitmapNamed("KingDiamonds", "d13.bmp");
            SwinGame.LoadBitmapNamed("AceDiamonds", "d01.bmp");

            SwinGame.LoadBitmapNamed("TwoSpades", "s02.bmp");
            SwinGame.LoadBitmapNamed("ThreeSpades", "s03.bmp");
            SwinGame.LoadBitmapNamed("FourSpades", "s04.bmp");
            SwinGame.LoadBitmapNamed("FiveSpades", "s05.bmp");
            SwinGame.LoadBitmapNamed("SixSpades", "s06.bmp");
            SwinGame.LoadBitmapNamed("SevenSpades", "s07.bmp");
            SwinGame.LoadBitmapNamed("EightSpades", "s08.bmp");
            SwinGame.LoadBitmapNamed("NineSpades", "s09.bmp");
            SwinGame.LoadBitmapNamed("TenSpades", "s10.bmp");
            SwinGame.LoadBitmapNamed("JackSpades", "s11.bmp");
            SwinGame.LoadBitmapNamed("QueenSpades", "s12.bmp");
            SwinGame.LoadBitmapNamed("KingSpades", "s13.bmp");
            SwinGame.LoadBitmapNamed("AceSpades", "s01.bmp");

            SwinGame.LoadBitmapNamed("TwoHeart", "h02.bmp");
            SwinGame.LoadBitmapNamed("ThreeHeart", "h03.bmp");
            SwinGame.LoadBitmapNamed("FourHeart", "h04.bmp");
            SwinGame.LoadBitmapNamed("FiveHeart", "h05.bmp");
            SwinGame.LoadBitmapNamed("SixHeart", "h06.bmp");
            SwinGame.LoadBitmapNamed("SevenHeart", "h07.bmp");
            SwinGame.LoadBitmapNamed("EightHeart", "h08.bmp");
            SwinGame.LoadBitmapNamed("NineHeart", "h09.bmp");
            SwinGame.LoadBitmapNamed("TenHeart", "h10.bmp");
            SwinGame.LoadBitmapNamed("JackHeart", "h11.bmp");
            SwinGame.LoadBitmapNamed("QueenHeart", "h12.bmp");
            SwinGame.LoadBitmapNamed("KingHeart", "h13.bmp");
            SwinGame.LoadBitmapNamed("AceHeart", "h01.bmp");
          
            ///Load figures for background:
            SwinGame.LoadBitmapNamed("Background", "Background.png");
            SwinGame.LoadBitmapNamed("Background1", "Background1.png");
            SwinGame.LoadBitmapNamed("GreenPlayer", "GreenPlayer.png");
            SwinGame.LoadBitmapNamed("BluePlayer", "BluePlayer.png");
            
            //Load figure for chips
            SwinGame.LoadBitmapNamed("GreenChip", "GreenChip.png");
            SwinGame.LoadBitmapNamed("BlueChip", "BlueChip.png");
            SwinGame.LoadBitmapNamed("RedChip", "BlueChip.png");

            SwinGame.LoadBitmapNamed("Newgame", "New_game.png");
            SwinGame.LoadBitmapNamed("TheBoard", "TheBoard.png");
            SwinGame.LoadFontNamed("arial", "arial.ttf",25);
            SwinGame.LoadBitmapNamed("BackOfCard", "BackOfCard.bmp");
            SwinGame.LoadBitmapNamed("Quit", "Quit.png");

            SwinGame.LoadBitmapNamed("LoadGame", "LoadGame.png");

            //Draw button
            SwinGame.LoadBitmapNamed("Redo", "Redo.png");
            SwinGame.LoadBitmapNamed("ResetButton", "ResetButton.png");
            SwinGame.LoadBitmapNamed("SaveButton", "SaveButton.png");
            SwinGame.LoadBitmapNamed("MainMenu", "MainMenu.png");
            //Draw button for Win Screen

            SwinGame.LoadBitmapNamed("MainMenuBig", "MainMenuBig.png");
            SwinGame.LoadBitmapNamed("BackToGame", "BackToGame.png");
        }
        /// <summary>
        /// Handle input from user in the TOP level. Decide which screen state of program to display
        /// </summary>
        public void HandleTheInput()
        {
            SwinGame.ProcessEvents();
            switch (_programState)
            {
                case (MyGame.ProgramState.MENU):
                    {
                        if (SwinGame.MouseClicked(MouseButton.LeftButton))
                            Menu.HandleTheGameInMenu(SwinGame.MousePosition(), this);
                        break;

                    }
                case (MyGame.ProgramState.PLAYINGGAME):
                    {
                        HandleGame();
                        if (GameMaster.Currentstate == GameState.TemporaryState)

                        {
                            GameMaster.ChangeState(GameState.SelectingCardInHand);
                        }
                        break;
                    }
                default:
                    break;

            }
        }
        /// <summary>
        /// Handl in Playing Screen (SECOND LEVEL)
        /// </summary>
        public void HandleGame()
        {
            if (SwinGame.MouseClicked(MouseButton.LeftButton))
            {
                GameMaster.TakeTheTurn(SwinGame.MousePosition());
                GameMaster.RedoButton(SwinGame.MousePosition());
                GameMaster.ResetButton(SwinGame.MousePosition());
                GameMaster.SaveButton(SwinGame.MousePosition());
                GameMaster.MainMenuButton(SwinGame.MousePosition(),this);
                if (GameMaster.Currentstate==GameState.EndGame)
                {
                    GameMaster.HandleInputInWinScreen(SwinGame.MousePosition(), this);
                }
            }
        }
        /// <summary>
        /// Draw the Screen, depending on the Program State.
        /// </summary>
        public void DrawTheScreen()
        {
            SwinGame.ClearScreen(Color.White);

            switch (_programState)
            {
                case ProgramState.MENU:
                    Menu.DrawMenu();
                    break;
                case ProgramState.PLAYINGGAME:
                    GameMaster.DrawEverything();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Change the view screen depends on the program state
        /// </summary>
        /// <param name="screenState"></param>
        public void ChangProgramState(ProgramState screenState)
        {
            ProgramState = screenState;
        }
    }
}
