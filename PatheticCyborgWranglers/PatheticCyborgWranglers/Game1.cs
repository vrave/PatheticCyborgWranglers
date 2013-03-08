using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PatheticCyborgWranglers
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        // State enum
        public enum State
        {
            Menu,
            Playing,
            GameOver
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random();
        public int enemyBulletDamage;

        TitleScreen ts = new TitleScreen(new Vector2(0,0));
        EndingTitle et = new EndingTitle(new Vector2(0, 0));
        // Player and Starfield Instantiation
        Player p = new Player();
        StarField sf = new StarField();
        // HUD
        HUD hud = new HUD();

        // Asteroids List
        List<Asteroid> asteroidList = new List<Asteroid>();

        // Enemy List
        List<Enemy> enemyList = new List<Enemy>();

        //Explosion List
        List<Explosion> explosionList = new List<Explosion>();

        // Sound Manager
        SoundManager sm = new SoundManager();

        // Set First State
        State gameState = State.Menu;

        // Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            // Screen/Window Size
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 950;
            // Game Title
            this.Window.Title = "Pathetic Cyborg Wranglers";
            enemyBulletDamage = 10;
            Content.RootDirectory = "Content";
        }

        // Init
        protected override void Initialize()
        {

            base.Initialize();
        }

        // Load Content
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            hud.LoadContent(Content);
            p.LoadContent(Content);
            sf.LoadContent(Content);
            sm.LoadContent(Content);
            MediaPlayer.Play(sm.bgMusic);
            ts.LoadContent(Content);
            et.LoadContent(Content);
            
        }

        // Unload Content
        protected override void UnloadContent()
        {

        }

        // Update Content
        protected override void Update(GameTime gameTime)
        {

            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            // generating random asteroids, update, and check for collision

                
                // Updating Playing State
                switch (gameState)
                {
                    case State.Playing:
                        {
                            
                            foreach (Asteroid a in asteroidList)
                            {
                                // asteroid:player collision
                                if (a.boundingBox.Intersects(p.boundingBox))
                                {
                                    sm.explodeSound.Play();
                                    explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion"), new Vector2(p.position.X, p.position.Y)));
                                    
                                    p.health -= 10;
                                    a.isVisible = false;
                                }

                                // asteroid:bullet collision
                                for (int i = 0; i < p.bulletList.Count; i++)
                                {
                                    if (a.boundingBox.Intersects(p.bulletList[i].boundingBox))
                                    {
                                        sm.explodeSound.Play();
                                        explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion"), new Vector2(a.position.X, a.position.Y)));
                                        a.isVisible = false;
                                        p.bulletList.ElementAt(i).isVisible = false;
                                        hud.playerScore += 25;
                                    }
                                }

                                a.Update(gameTime);
                            }
                        
                        // enemy:spawn, collision
                        foreach (Enemy e in enemyList)
                        {
                            // enemy:player collision
                            if (e.boundingBox.Intersects(p.boundingBox))
                            {
                                sm.explodeSound.Play();
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion"), new Vector2(p.position.X, p.position.Y)));
                                    
                                p.health -= 40;
                                e.isVisible = false;
                            }

                            // enemybullet:player collision with player
                            for (int i = 0; i < e.bulletList.Count; i++)
                            {
                                if (p.boundingBox.Intersects(e.bulletList[i].boundingBox))
                                {
                                    sm.explodeSound.Play();
                                    explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion"), new Vector2(p.position.X + 20, p.position.Y)));
                                    p.health -= enemyBulletDamage;
                                    e.bulletList[i].isVisible = false;
                                }
                            }

                            // playerbullet :enemy collision with enemy
                            for (int i = 0; i < p.bulletList.Count; i++)
                            {
                                if (p.bulletList[i].boundingBox.Intersects(e.boundingBox))
                                {
                                    sm.explodeSound.Play();
                                    explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion"), new Vector2(e.position.X, e.position.Y)));
                                    hud.playerScore += 100;
                                    p.bulletList[i].isVisible = false;
                                    e.isVisible = false;
                                }
                            }
                            // bullet:bullet collision STILL IN PROGRESS

                            e.Update(gameTime);
                            if (p.health <= 0) 
                            {
                                gameState = State.GameOver;
                            }
                        }
                        foreach (Explosion ex in explosionList) 
                        {
                            ex.Update(gameTime);
                        }

                        p.Update(gameTime);
                        sf.Update(gameTime);
                        hud.Update(gameTime);
                        ManageExplosions();
                        LoadAsteroids();
                        LoadEnemy();

                        break;
                        }

                    // Menu State
                    case State.Menu:
                        {
                            if (keyState.IsKeyDown(Keys.Enter))
                                gameState = State.Playing;

                            break;
                        }
                    // GameOver State
                    case State.GameOver:
                        {
                            
                            MediaPlayer.Stop();
                            MediaPlayer.Play(sm.gameMusic);
                            if (keyState.IsKeyDown(Keys.Escape))
                                this.Exit();
                            break;
                            
                        }
                }


                base.Update(gameTime);

            
        }

        // Draw Function
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            switch (gameState)
            {

                // Drawing Playing State
                case State.Playing:
                    {
                        // BG first then Player so that Player floats above the background
                        sf.Draw(spriteBatch);
                        p.Draw(spriteBatch);
                        foreach (Asteroid a in asteroidList)
                        {
                            a.Draw(spriteBatch);
                        }
                        foreach (Explosion ex in explosionList)
                        {
                            ex.Draw(spriteBatch);
                        }
                        foreach (Enemy e in enemyList)
                        {
                            e.Draw(spriteBatch);
                        }
                        hud.Draw(spriteBatch);
                        break;
                    }
                // Drawing Menu State
                case State.Menu:
                    {
                        ts.Draw(spriteBatch);
                        break;
                    }
                // Drawing GameOver State
                case State.GameOver:
                    {
                        et.Draw(spriteBatch);
                        break;
                    }

            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void LoadAsteroids()
        {
            // Asteroid Randomizer with limited x and y coordinates
            int randY = random.Next(-600, -50);
            int randX = random.Next(0, 750);

            if (asteroidList.Count() < 5) // 5 asteroid count, if less than 5 create more
            {

                asteroidList.Add(new Asteroid(Content.Load<Texture2D>("asteroid"), new Vector2(randX, randY)));

            }

            // if asteroid(s) is destroyed, remove from list, if less than 5 create more
            for (int i = 0; i < asteroidList.Count; i++)
            {

                if (!asteroidList[i].isVisible)
                {
                    asteroidList.RemoveAt(i);
                    i--;
                }

            }

        }

        public void LoadEnemy()
        {
            // Asteroid Randomizer with limited x and y coordinates
            int randY = random.Next(-600, -50);
            int randX = random.Next(0, 750);

            if (enemyList.Count() < 3) // 3 enemy count, if less than 5 create more
            {

                enemyList.Add(new Enemy(Content.Load<Texture2D>("enemyship"), new Vector2(randX, randY), Content.Load<Texture2D>("EnemyBullet")));

            }

            // if asteroid(s) is destroyed, remove from list, if less than 5 create more
            for (int i = 0; i < enemyList.Count; i++)
            {

                if (!enemyList[i].isVisible)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }

            }

        }

        public void ManageExplosions()
        {
            for (int i = 0; i < explosionList.Count; i++)
            {
                if (!explosionList[i].isVisible)
                {
                    explosionList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
