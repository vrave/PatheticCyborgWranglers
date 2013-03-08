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
    public class SoundManager
    {
        public SoundEffect playerShootSound;
        public SoundEffect explodeSound;
        public Song bgMusic;
        public Song gameMusic;

        // Constructor
        public SoundManager() 
        {
            playerShootSound = null;
            explodeSound = null;
            bgMusic = null;
            gameMusic = null;
        }
        // Load Content
        public void LoadContent(ContentManager Content)
        {
            playerShootSound = Content.Load<SoundEffect>("playershoot");
            explodeSound = Content.Load<SoundEffect>("explode");
            bgMusic = Content.Load<Song>("theme");
            gameMusic = Content.Load<Song>("end");
        }

    }
}
