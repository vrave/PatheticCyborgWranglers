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
    public class Enemy
    {
        public Rectangle boundingBox;
        public Texture2D texture, bulletTexture;
        public Vector2 position;
        public int health, speed, bulletDelay, currentDifficultyLevel;
        public bool isVisible;
        public List<Bullet> bulletList;

        // Constructor
        public Enemy(Texture2D newTexture, Vector2 newPosition, Texture2D newBulletTexture)
        {
            bulletList = new List<Bullet>();
            texture = newTexture;
            bulletTexture = newBulletTexture;
            health = 2;
            position = newPosition;
            currentDifficultyLevel = 1;
            bulletDelay = 40;
            speed = 3;
            isVisible = true;
        }


        // Draw Function
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw Enemy ship
            spriteBatch.Draw(texture, position, Color.White);
            // Draw Enemy bullets
            foreach(Bullet b in bulletList)
            {
                b.Draw(spriteBatch);
            }

        }

        // Update
        public void Update(GameTime gameTime)
        {

            // Update Collision Rectangle
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            
            // Update Enemy Movement
            position.Y += speed;

            // If enemy goes at the very bottom spawn back to the very top
            if (position.Y >= 950)
                position.Y = -75;

            EnemyShoot();
            UpdateBullet();

        }

        // Update Bullets
        public void UpdateBullet() 
        {

            // for each bullet in our bulletList: update the movement and if the bullets hit the top of the screen then remove it from the list
            foreach (Bullet b in bulletList)
            {
                // set BoundingBox
                b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);

                // set bullet movement
                b.position.Y = b.position.Y + b.speed;

                if (b.position.Y >= 950)
                    b.isVisible = false;

            }

            for (int i = 0; i < bulletList.Count; i++)
            {

                if (!bulletList[i].isVisible)
                {
                    bulletList.RemoveAt(i);
                    i++;
                }
            }
        }

        public void EnemyShoot() 
        {
        
            // Shoot only if bulletdelay resets
            if (bulletDelay >= 0)
                bulletDelay--;
            // Creates new bullet in front & center of enemy ship
            if (bulletDelay <= 0) 
            {
                Bullet newBullet = new Bullet(bulletTexture);
                newBullet.position = new Vector2(position.X + texture.Width /2 - newBullet.texture.Width/2, position.Y + 30);

                newBullet.isVisible = true;

                if (bulletList.Count() < 20)
                    bulletList.Add(newBullet);

                // Reset Bullet Delay
                if (bulletDelay == 0)
                    bulletDelay = 90;
            
            }

        }

    }
}
