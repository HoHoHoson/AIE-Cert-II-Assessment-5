using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections;

namespace A5
{
    public class SoloGameState : A5.State
    {
        static SoloGameState instance;
        bool isLoaded = false;
        SpriteFont arial = null;
        Player1 player1 = null;
        Texture2D background = null;
        Game1 game = null;
        public Random random = new Random();
        Projectiles projectiles;
        List<Projectiles> myProjectiles = new List<Projectiles>();
        List<Projectiles> dedProjectiles = new List<Projectiles>();
        float m_timer;




        public static SoloGameState Instance
        {
            get
            {
                return instance;
            }
        }




        public SoloGameState(Game1 game) : base()
        {
            this.game = game;
            player1 = new Player1(game);
            projectiles = new Projectiles();
        }




        public override void Update(ContentManager Content, GameTime gameTime)
        {
            if(isLoaded == false)
            {
                isLoaded = true;
                player1.Load(Content);
                arial = Content.Load<SpriteFont>("Arial");
                background = Content.Load<Texture2D>("SoloBackground");
                foreach (var p in myProjectiles)
                {
                    p.Load(Content);
                }
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            m_timer += deltaTime;
            player1.Update(deltaTime);


            if (m_timer >= 1.0f)
            {
                projectiles = new Projectiles();
                projectiles.Load(Content);
                projectiles.randB_AsteroidSpawn = new Vector2(random.Next(0 + projectiles.brownAsteroid.Width, Game1.Instance.ScreenWidth - projectiles.brownAsteroid.Width), 0 - projectiles.brownAsteroid.Height);
                double randomNumber = (float)random.NextDouble();
                float randB_AsteroidAngle = MathHelper.Lerp(-1.3f, +1.3f, (float)randomNumber);
                Vector2 B_AsteroidDirection = new Vector2(-(float)Math.Sin(randB_AsteroidAngle), (float)Math.Cos(randB_AsteroidAngle));
                B_AsteroidDirection.Normalize();
                projectiles.spawnVelocity = B_AsteroidDirection * 1;
                myProjectiles.Add(projectiles);
                m_timer = 0.0f;
            }

            foreach (Projectiles p in myProjectiles)
            {
                p.Update(deltaTime);

                if (p.b_AsteroidRect.Intersects(Player1.Instance.player1Rect) && p.b_AsteroidRect.Bottom - 2 < Player1.Instance.player1Rect.Top)
                {
                    p.spawnVelocity.Y = -p.spawnVelocity.Y;
                }
                else if (p.b_AsteroidRect.Intersects(Player1.Instance.player1Rect))
                {
                    dedProjectiles.Add(p);
                }
            }

            foreach (Projectiles p in dedProjectiles)
            {
                myProjectiles.Remove(p);
            }
        }
    



        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(background, new Rectangle(0, 0, Game1.Instance.ScreenWidth, Game1.Instance.ScreenHeight), Color.White);
            player1.Draw(spriteBatch);
            foreach (Projectiles p in myProjectiles)
            {
                p.Draw(spriteBatch, arial);
            }
            spriteBatch.End();
        }




        public override void CleanUp()
        {
            arial = null;
            isLoaded = false;
        }
    }
}
