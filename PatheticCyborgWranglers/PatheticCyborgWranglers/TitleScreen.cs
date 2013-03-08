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
   public class TitleScreen
    {
        public Texture2D texture;
        public Vector2  position;

        // Constructor
        public TitleScreen(Vector2 Position)
        {

            position = Position;
        }

        //Load Content
        public void LoadContent(ContentManager Content)
        {

            texture = Content.Load<Texture2D>("TitlePage");

        }

        // Draw Function

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }


        // Update
        public void Update(GameTime gameTime)
        {
            position.X = 0;
            position.Y = 0;

        }

    }
}
