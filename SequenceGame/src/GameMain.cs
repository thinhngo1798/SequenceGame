using System;
using SwinGameSDK;

namespace MyGame
{
    public class GameMain
    {
        public static void Main()
        {
            
            SwinGame.OpenGraphicsWindow("GameMain", 1000, 800);
            Program program = new Program();
            program.LoadResources();
            //Run the game loop
            while (program.ProgramState!=ProgramState.QUITTING && false == SwinGame.WindowCloseRequested() && false == SwinGame.KeyDown(KeyCode.EscapeKey))
            {
                program.HandleTheInput();
                program.DrawTheScreen();
                SwinGame.RefreshScreen(60);
            }
            }
        }
    }
