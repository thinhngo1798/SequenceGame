using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
   public class Menu
    {
        private const int TOP_BUTTON_X = 230;
        private const int TOP_BUTTON_Y = 350;
        private const int MIDDLE_BUTTON_X = 600;
        private const int BUTTON_WIDTH = 150;
        private const int BUTTON_HEIGHT = 100;
        private const int MIDDLE_BUTTON_Y = 350;
        private const int BOTTOM_BUTTON_X = 400;
        private const int BOTTOM_BUTTON_Y = 500;
        /// <summary>
        /// Handle game in Menu Screen (SECOND level)
        /// </summary>
        /// <param name="point"></param>
        /// <param name="screen"></param>
        public void HandleTheGameInMenu(Point2D point, Program screen)
        {
            if (SwinGame.PointInRect(point, TOP_BUTTON_X, TOP_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT))
            {
                //screen.GameMaster.ReleaseGame();

                //screen.InitializeTheGame();
                bool WasTheGameIsReleased = false;
                if (screen.GameMaster != null)
                {
                    screen.ReleaseGame();
                    WasTheGameIsReleased = true;
                }
                screen.InitializeTheGame();
                if (WasTheGameIsReleased)
                screen.GameMaster.SetUpGame();
                screen.ChangeScreenViewing(ProgramState.PLAYINGGAME);
            }
            if (SwinGame.PointInRect(point, BOTTOM_BUTTON_X, BOTTOM_BUTTON_Y, BUTTON_WIDTH, BUTTON_HEIGHT))
            {
                screen.InitializeTheGame();
                //screen.GameMaster.ReleaseCardInCells();
                screen.GameMaster.LoadGame();
                screen.ChangeScreenViewing(ProgramState.PLAYINGGAME);
            }
            if (SwinGame.PointInRect(point, MIDDLE_BUTTON_X, MIDDLE_BUTTON_Y,BUTTON_WIDTH,BUTTON_HEIGHT))
            {
                screen.ChangeScreenViewing(ProgramState.QUITTING);
            }
            
        }
        public void DrawMenu()
        {
            SwinGame.DrawBitmap(SwinGame.BitmapNamed("Background"), 0,0);
            SwinGame.DrawBitmap(SwinGame.BitmapNamed("Newgame"), TOP_BUTTON_X, TOP_BUTTON_Y);
           SwinGame.DrawBitmap(SwinGame.BitmapNamed("Quit"), MIDDLE_BUTTON_X, MIDDLE_BUTTON_Y);
           SwinGame.DrawBitmap(SwinGame.BitmapNamed("LoadGame"), BOTTOM_BUTTON_X, BOTTOM_BUTTON_Y);

        }
    }
}
