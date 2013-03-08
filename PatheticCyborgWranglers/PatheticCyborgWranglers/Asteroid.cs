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
    public class Asteroid
    {
        public Texture2D texture;
        public Rectangle boundingBox;
        public Vector2 position;
        public Vector2 origin;
        public float rotationangle;
        public float speed;

        public bool isVisible;
        Random random = new Random();
        public float randX;
        public float randY;

        // Constructor
        public Asteroid(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
            speed = 4.0f;
            isVisible = true;
            randX = random.Next(0, 750);
            randY = random.Next(-600, -50);
        }

        // Load Content
        public void LoadContent(ContentManager Content)
        {

        }


        // Draw Function
        public void Draw(SpriteBatch spriteBatch)
        {

            if (isVisible)
                spriteBatch.Draw(texture, position, Color.White);


        }

        // Update
        public void Update(GameTime gameTime)
        {

            // Draws invisible box around asteroid to detect collision
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 45, 45);

            // Update Movement
            position.Y = position.Y + speed;
            if (position.Y >= 950)
                position.Y = -50;
            /*
            // To find the center of the asteroid to make a constant rotation
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
            */
            /* Rotate Asteroid
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            rotationangle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotationangle = rotationangle % circle;
            */
        }

    }
}
