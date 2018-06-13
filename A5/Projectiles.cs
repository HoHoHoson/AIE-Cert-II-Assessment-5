using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Diagnostics;

namespace A5
{
    class Projectiles
    {
        public Sprite b_AsteroidSprite = new Sprite();
        public Vector2 randB_AsteroidSpawn = Vector2.Zero;
        public Vector2 spawnVelocity = Vector2.Zero;
        public Rectangle b_AsteroidRect;
        public Circle b_AsteroidCircle;
        public bool rebound = false;




        public Projectiles()
        { 
        }




        public void Load(ContentManager content)
        {
            b_AsteroidSprite.Load(content, "meteorBrown");
        }




        public void Update(float deltaTime)
        {
            randB_AsteroidSpawn += spawnVelocity;
            b_AsteroidSprite.position = randB_AsteroidSpawn;

            if (b_AsteroidSprite.position.X - b_AsteroidSprite.texture.Width / 2 < 0 && spawnVelocity.X < 0 || b_AsteroidSprite.position.X + b_AsteroidSprite.texture.Width / 2 > Game1.Instance.ScreenWidth && spawnVelocity.X > 0)
            {
                spawnVelocity.X = -spawnVelocity.X;
            }

            b_AsteroidRect = new Rectangle((int)(b_AsteroidSprite.position.X - b_AsteroidSprite.offset.X), (int)(b_AsteroidSprite.position.Y - b_AsteroidSprite.offset.Y), b_AsteroidSprite.texture.Width, b_AsteroidSprite.texture.Height);
            b_AsteroidCircle = new Circle((b_AsteroidSprite.position - b_AsteroidSprite.offset), (b_AsteroidSprite.texture.Width / 2));
        }




        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            b_AsteroidSprite.Draw(spriteBatch);

            //spriteBatch.DrawString(font, "Debug: " + randB_AsteroidAngle.ToString(), new Vector2(20, 20), Color.Red);
        }
    }
}
