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
    public class Explosion
    {
        public Texture2D texture;
        public Vector2 position, origin;
        public float timer, interval;
        public int currentFrame, spriteWidth, spriteHeight;
        public Rectangle sourceRect;
        public bool isVisible;

        // Constructor
        public Explosion(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
            timer = 0f;
            interval = 20.0f;
            currentFrame = 1;
            spriteWidth = 128;
            spriteHeight = 128;
            isVisible = true;
        }

        // Load Content
        public void LoadContent(ContentManager Content)
        {

        }

        // Draw Function

        public void Draw(SpriteBatch spriteBatch)
        {
            if(isVisible == true)
            spriteBatch.Draw(texture, position, sourceRect, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }

        // Update
        public void Update(GameTime gameTime)
        {

            //increase the timer by the number of milliseconds since update was last called
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Check the timer is more thhan the chosen interval
            if (timer > interval)
            {
                // Show the next frame
                currentFrame++;
                timer = 0f;
            }

            // creates the animation for player bullet:asteroid/enemy collision
            if (currentFrame == 17)
            {
                //hides the animation after 17th frame
                isVisible = false;
                //resets the animation the next time it hits something
                currentFrame = 0;
            }

            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            origin = new Vector2(sourceRect.Width /2, sourceRect.Height /2);
        }

    }
}
