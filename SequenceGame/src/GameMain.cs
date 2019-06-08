using System;
using SwinGameSDK;

namespace MyGame
{
    public class GameMain
    {
        public static void Main()
        {
            
            //Open the game window
            SwinGame.OpenGraphicsWindow("GameMain", 1000, 800);
           
            // SwinGame.ShowSwinGameSplashScreen();
            Program screen = new Program();
            screen.LoadResources();
            //Run the game loop
            while (screen.ProgramState!=ProgramState.QUITTING && false == SwinGame.WindowCloseRequested() && false == SwinGame.KeyDown(KeyCode.EscapeKey))
            {
                //Clear the screen and draw the framerate
                //SwinGame.DrawFramerate(0, 0);     
                //Draw onto the screen
                
                screen.HandleTheInput();
                screen.DrawTheScreen();
                
                SwinGame.RefreshScreen(60);
            }





            }
        }
    }
