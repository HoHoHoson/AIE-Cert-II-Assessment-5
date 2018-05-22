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
        Game1 game1 = null;
        Texture2D brownAsteroid;
        public Vector2 brownAsteroidPosition = Vector2.Zero;
        Vector2 brownAsteroidOffset = Vector2.Zero;
        ArrayList brownAsteroidPositions = new ArrayList();
        ArrayList brownAsteroidVelocities = new ArrayList();
        Random random = new Random();
        Vector2 B_AsteroidVelocity;
        public float randB_AsteroidAngle = 0;

        public Projectiles(Game1 game)
        {
            game1 = game;
        }



        public void Load(ContentManager content)
        {
            brownAsteroid = content.Load<Texture2D>("meteorBrown");
            brownAsteroidOffset = new Vector2(brownAsteroid.Width / 2, brownAsteroid.Height / 2);

            //Vector2 randB_AsteroidSpawn = new Vector2(random.Next(0 + brownAsteroid.Width, Game1.Instance.ScreenWidth - brownAsteroid.Width), 0 - brownAsteroid.Height);
            Vector2 randB_AsteroidSpawn = new Vector2(Game1.Instance.ScreenWidth / 2, Game1.Instance.ScreenHeight / 2);
            brownAsteroidPosition = randB_AsteroidSpawn;
            randB_AsteroidAngle = random.Next(0, 3);
            Vector2 B_AsteroidDirection = new Vector2(-(float)Math.Sin(randB_AsteroidAngle), (float)Math.Cos(randB_AsteroidAngle));
            B_AsteroidDirection.Normalize();
            B_AsteroidVelocity = B_AsteroidDirection * 1;
        }



        public void Update(float deltaTime)
        {
            //Vector2 B_AsteroidDirection = new Vector2(-(float)Math.Sin(randB_AsteroidAngle), (float)Math.Cos(randB_AsteroidAngle));
            //B_AsteroidDirection.Normalize();
            //B_AsteroidVelocity = B_AsteroidDirection * 1;
            brownAsteroidPosition += B_AsteroidVelocity;
            //if (Keyboard.GetState().IsKeyDown(Keys.Left) == true)
            //{
            //    randB_AsteroidAngle -= 1;
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.Right) == true)
            //{
            //    randB_AsteroidAngle += 1;
            //}
        }



        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(brownAsteroid, brownAsteroidPosition - brownAsteroidOffset, Color.White);
            spriteBatch.DrawString(font, "testing AsteriodAngle: " + randB_AsteroidAngle.ToString(), new Vector2(20, 20), Color.Red);
        }
    }
}
