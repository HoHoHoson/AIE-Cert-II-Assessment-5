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
        Vector2 b_AsteroidPos = Vector2.Zero;
        public Vector2 brownAsteroidOffset = Vector2.Zero;
        public Vector2 randB_AsteroidSpawn = Vector2.Zero;
        public Vector2 spawnVelocity = Vector2.Zero;
        public Texture2D brownAsteroid;
        public Rectangle b_AsteroidRect;
        public Circle b_AsteroidCircle;
        public int asteroidCount = 10;




        public Projectiles()
        { 
        }




        public void Load(ContentManager content)
        {
            brownAsteroid = content.Load<Texture2D>("meteorBrown");
            brownAsteroidOffset = new Vector2(brownAsteroid.Width / 2, brownAsteroid.Height / 2);
        }




        public void Update(float deltaTime)
        {
            randB_AsteroidSpawn += spawnVelocity;
            b_AsteroidPos = randB_AsteroidSpawn;

            if (b_AsteroidPos.X - brownAsteroid.Width / 2 < 0 && spawnVelocity.X < 0 || b_AsteroidPos.X + brownAsteroid.Width / 2 > Game1.Instance.ScreenWidth && spawnVelocity.X > 0)
            {
                spawnVelocity.X = -spawnVelocity.X;
            }

            b_AsteroidRect = new Rectangle((int)(b_AsteroidPos.X - brownAsteroidOffset.X), (int)(b_AsteroidPos.Y - brownAsteroidOffset.Y), brownAsteroid.Width, brownAsteroid.Height);
            b_AsteroidCircle = new Circle((b_AsteroidPos - brownAsteroidOffset), (brownAsteroid.Width / 2));
        }




        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(brownAsteroid, b_AsteroidPos, null, null, brownAsteroidOffset, 0, null, Color.White);

            //spriteBatch.DrawString(font, "Debug: " + randB_AsteroidAngle.ToString(), new Vector2(20, 20), Color.Red);
        }
    }
}
