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
    public class Player
    {
        public Texture2D texture, healthTexture;
        public Vector2 position;
        public float speed;
        public int health;
        // Bullets Variables
        public Texture2D bulletTexture;
        public float bulletDelay = 0.5f;
        public List<Bullet> bulletList;
        public Vector2 healthBarPosition;
        // Collision Variables
        public Rectangle boundingBox, healthRectangle; // to detect collision
        public bool isColliding;
        SoundManager sm = new SoundManager();
        // Constructor
        public Player()
        {
            texture = null;
            position = new Vector2(300,300);
            speed = 10;
            isColliding = false;

            bulletList = new List<Bullet>();
            bulletDelay = 10;

            health = 200;
            healthBarPosition = new Vector2(50,50) ;
        }

        // Load Content
        public void LoadContent(ContentManager Content) 
        {

            texture = Content.Load<Texture2D>("ship");
            bulletTexture = Content.Load<Texture2D>("playerbullet");

            healthTexture = Content.Load<Texture2D>("healthbar");

            sm.LoadContent(Content);
        }

        // Draw Function
        public void Draw(SpriteBatch spriteBatch) 
        {
            
            spriteBatch.Draw(texture,position, Color.White);
            // Draws HealthBar
            spriteBatch.Draw(healthTexture, healthRectangle, Color.White);
            // Draws Bullet
            foreach(Bullet b in bulletList)
            b.Draw(spriteBatch);

        }

        // Update Function

        public void Update(GameTime gameTime) 
        {

            // Getting Keyboard State
            KeyboardState keyState = Keyboard.GetState();

            // BoundingBox for Player Ship
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            // HealthBar
            healthRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, health, 25);


            // Ship controls
            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W))
            {
                position.Y = position.Y - speed;
            }
            if (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S))
            {
                position.Y = position.Y + speed;
            }
            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))
            {
                position.X = position.X - speed;
            }
            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
            {
                position.X = position.X + speed;
            }

            // Player Screen Bounds

            if (position.X <= 0) position.X = 0;
            if (position.X >= 800 - texture.Width) position.X = 800 - texture.Width;
            
            if (position.Y <= 0) position.Y = 0;
            if (position.Y >= 950 - texture.Height) position.Y = 950 - texture.Height;

            // Shooting Mechanism
            if (keyState.IsKeyDown(Keys.Space)) 
            {
                Shoot();
            }

            UpdateBullet();

        }

        // Shooting Mechanism (startPos,Delay,Collision)
        public void Shoot() 
        {
        
            // Shoots only if bullet delay resets
            if (bulletDelay >= 0)
                bulletDelay --; 

            // If bulletDelay is at 0 creates new bullet at player position, make it visible on the screen, then add that bullet to the list
            if (bulletDelay <= 0)
            {
                sm.playerShootSound.Play();
                Bullet newBullet = new Bullet(bulletTexture);
                newBullet.position = new Vector2(position.X + 32 - newBullet.texture.Width / 2, position.Y + 30);
                newBullet.isVisible = true;

                if (bulletList.Count() < 20) // Limits fired bullets at 20 ammunition
                    bulletList.Add(newBullet);
            }

            if (bulletDelay == 0)
                bulletDelay = 10;

        }


        // Update Bullet Function
        public void UpdateBullet() 
        {
        
            // for each bullet in our bulletList: update the movement and if the bullets hit the top of the screen then remove it from the list
            foreach(Bullet b in bulletList)
            {
                // set BoundingBox
                b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);

                // set bullet movement
                b.position.Y = b.position.Y - b.speed;
                
                if (b.position.Y <= 0)
                    b.isVisible = false;
                
            }
        
            for(int i = 0; i < bulletList.Count; i++)
            {
            
                if(!bulletList[i].isVisible)
                {
                    bulletList.RemoveAt(i);
                    i++;
                }


            }

        }

    }
}
