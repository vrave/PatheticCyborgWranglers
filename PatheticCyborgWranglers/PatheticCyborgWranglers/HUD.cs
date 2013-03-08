using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PatheticCyborgWranglers
{
    public class HUD
    {
        public int playerScore, screenWidth, screenHeight;
        public SpriteFont playerScoreFont;
        public Vector2 playerScorePos;
        public bool showHUD;

        // Constructor
        public HUD()
        {
            playerScore = 0;
            showHUD = true;
            screenHeight = 950;
            screenWidth = 800;

            playerScoreFont = null;
            playerScorePos = new Vector2(screenWidth/2, 50);
        }

        // Load Content
        public void LoadContent(ContentManager Content)
        {

            playerScoreFont = Content.Load<SpriteFont>("georgia");

        }
        
        // Draw Function

        public void Draw(SpriteBatch spriteBatch)
        {
            if (showHUD == true) 
            {
                spriteBatch.DrawString(playerScoreFont, "Score :"+playerScore, playerScorePos, Color.Red);
            }
        }


       // Update

       public void Update(GameTime gameTime)
       {

           // Key state for toggling HUD
           KeyboardState keyState = Keyboard.GetState();

       }
    }
}
