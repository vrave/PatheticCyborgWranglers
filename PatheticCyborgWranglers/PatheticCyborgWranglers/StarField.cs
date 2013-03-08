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
    public class StarField
    {
        public Texture2D texture;
        public Vector2 bgPos1, bgPos2; // Parallaxing Background
        public int speed;

        // Constructor
        public StarField()
        {

            texture = null;
            bgPos1 = new Vector2(0, 0); // 1st background to cover entire screen at the beggining
            bgPos2 = new Vector2(0, -950); // 2nd background above 1st background to make a consistent loop
            speed = 5;

        }

        // Load Content
        public void LoadContent(ContentManager Content)
        {

            texture = Content.Load<Texture2D>("space");

        }

        // Draw Function

        public void Draw(SpriteBatch spriteBatch) 
        {

            spriteBatch.Draw(texture, bgPos1, Color.White);
            spriteBatch.Draw(texture, bgPos2, Color.White);
        }

        // Update

        public void Update(GameTime gameTime) 
        {

            bgPos1.Y = bgPos1.Y + speed;
            bgPos2.Y = bgPos2.Y + speed;

            // Repeating/Scrolling Background

            if (bgPos1.Y >= 950)
            {

                bgPos1.Y = 0;
                bgPos2.Y = -950;

            }

        }

    }
}
