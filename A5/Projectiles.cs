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
        public Texture2D brownAsteroid;
        public Vector2 brownAsteroidOffset = Vector2.Zero;
        public ArrayList brownAsteroidPositions = new ArrayList();
        public ArrayList brownAsteroidVelocities = new ArrayList();
        Random random = new Random();
        public float randB_AsteroidAngle = 0;
        Vector2 B_AsteroidDirection = Vector2.Zero;
        public Rectangle b_AsteroidRect;



        public Projectiles(Game1 game)
        {
            game1 = game;
        }



        public void Load(ContentManager content)
        {
            brownAsteroid = content.Load<Texture2D>("meteorBrown");
            brownAsteroidOffset = new Vector2(brownAsteroid.Width / 2, brownAsteroid.Height / 2);

            for (int i = 0; i < 5; i++)
            {
                Vector2 randB_AsteroidSpawn = new Vector2(random.Next(0 + brownAsteroid.Width, Game1.Instance.ScreenWidth - brownAsteroid.Width), 0 - brownAsteroid.Height);
                brownAsteroidPositions.Add(randB_AsteroidSpawn);
                double randomNumber = (float)random.NextDouble();
                randB_AsteroidAngle = MathHelper.Lerp(-1.3f, +1.3f, (float)randomNumber);
                B_AsteroidDirection = new Vector2(-(float)Math.Sin(randB_AsteroidAngle), (float)Math.Cos(randB_AsteroidAngle));
                B_AsteroidDirection.Normalize();
                Vector2 velocity = B_AsteroidDirection * 1;
                brownAsteroidVelocities.Add(velocity);
            } 
        }



        public void Update(float deltaTime)
        {
            for (int b_Asteroids = 0; b_Asteroids < brownAsteroidPositions.Count; b_Asteroids++)
            {
                Vector2 position = (Vector2)brownAsteroidPositions[b_Asteroids];
                Vector2 velocity = (Vector2)brownAsteroidVelocities[b_Asteroids];
                position += velocity;
                brownAsteroidPositions[b_Asteroids] = position;
                if (position.X - brownAsteroid.Width / 2 < 0 && velocity.X < 0 || position.X + brownAsteroid.Width / 2 > Game1.Instance.ScreenWidth && velocity.X > 0)
                {
                    velocity.X = -velocity.X;
                    brownAsteroidVelocities[b_Asteroids] = velocity;
                }
            }
        }



        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            for (int b_Asteroids = 0; b_Asteroids < brownAsteroidPositions.Count; b_Asteroids++)
            {
                Vector2 position = (Vector2)brownAsteroidPositions[b_Asteroids];
                spriteBatch.Draw(brownAsteroid, position, null, null, brownAsteroidOffset, 0, null, Color.White);
            }

            //spriteBatch.DrawString(font, "Debug: " + randB_AsteroidAngle.ToString(), new Vector2(20, 20), Color.Red);
        }
    }
}
