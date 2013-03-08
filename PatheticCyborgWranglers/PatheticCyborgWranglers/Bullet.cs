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
    public class Bullet
    {
        public Texture2D texture;
        public Rectangle boundingBox;
        public Vector2 origin, position;
        public bool isVisible;
        public float speed;

        // Constructor
        public Bullet(Texture2D newTexture)
        {

            speed = 10.0f;
            texture = newTexture;
            isVisible = false;


        }

/*        // Load Content
        public void LoadContent(ContentManager Content)
        {

            texture = Content.Load<Texture2D>("playerbullet");

        }
*/
        // Draw Function

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

/*      Updates would be done under Player class
        // Update

        public void Update(GameTime gameTime)
        {


        }
*/
    }
}
