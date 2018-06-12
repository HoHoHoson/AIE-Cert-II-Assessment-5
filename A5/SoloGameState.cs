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
        bool isLoaded = false;
        bool endGame = false;
        static SoloGameState instance;
        Game1 game = null;
        Player1 player1 = null;
        Projectiles projectiles;
        List<Projectiles> myProjectiles = new List<Projectiles>();
        List<Projectiles> dedProjectiles = new List<Projectiles>();
        SpriteFont arial = null;
        Texture2D background = null;
        Audio gameMusic = new Audio();
        Audio miniExplosion = new Audio();
        Audio deathExplosion = new Audio();
        public Random random = new Random();
        int playerHealth = 3;
        float m_timer = 0f;
        float progressiveTimer = 0f;
        float b_AsteroidSpeed = 1f;




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
            player1.playerSprite.position.X = Game1.Instance.ScreenWidth / 2;
        }




        public override void Update(ContentManager content, GameTime gameTime)
        {
            if(isLoaded == false)
            {
                isLoaded = true;
                player1.Load(content);
                arial = content.Load<SpriteFont>("Arial");
                background = content.Load<Texture2D>("SoloBackground");
                gameMusic.Load(content, "A5_Solo_Music");
                miniExplosion.Load(content, "explodemini");
                deathExplosion.Load(content, "bigExplosion");
                foreach (var p in myProjectiles)
                {
                    p.Load(content);
                }

                gameMusic.soundInstance.IsLooped = true;
                gameMusic.soundInstance.Play();
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            player1.Update(deltaTime);

            progressiveTimer += deltaTime;
            if (progressiveTimer >= 5f)
            {
                progressiveTimer = 0;
                b_AsteroidSpeed += 0.1f;
            }

            m_timer += deltaTime;
            if (m_timer >= 1.0f)
            {
                projectiles = new Projectiles();
                projectiles.Load(content);
                projectiles.randB_AsteroidSpawn = new Vector2(random.Next(0 + projectiles.b_AsteroidSprite.texture.Width, Game1.Instance.ScreenWidth - projectiles.b_AsteroidSprite.texture.Width), 0 - projectiles.b_AsteroidSprite.texture.Height);
                double randomNumber = (float)random.NextDouble();
                float randB_AsteroidAngle = MathHelper.Lerp(-1.3f, +1.3f, (float)randomNumber);
                Vector2 B_AsteroidDirection = new Vector2(-(float)Math.Sin(randB_AsteroidAngle), (float)Math.Cos(randB_AsteroidAngle));
                B_AsteroidDirection.Normalize();
                projectiles.spawnVelocity = B_AsteroidDirection * b_AsteroidSpeed;
                myProjectiles.Add(projectiles);
                m_timer = 0.0f;
            }


            foreach (Projectiles p in myProjectiles)
            {
                p.Update(deltaTime);

                if (p.b_AsteroidRect.Intersects(Player1.Instance.player1Rect) && p.b_AsteroidRect.Bottom - 9999 < Player1.Instance.player1Rect.Top && p.rebound == false)
                {
                    p.rebound = true;
                    p.spawnVelocity.Y = -p.spawnVelocity.Y;
                }
                else if (p.b_AsteroidRect.Intersects(Player1.Instance.player1Rect))
                {
                    dedProjectiles.Add(p);
                    miniExplosion.sound.Play();
                }

                if (p.randB_AsteroidSpawn.Y > Game1.Instance.ScreenHeight + p.b_AsteroidSprite.texture.Height)
                {
                    endGame = true;
                }
            }
            
            for (int i = 0; i < myProjectiles.Count - 1; i++)
            {
                var asteroid1 = myProjectiles[i];
                Circle asteroid1Rect = asteroid1.b_AsteroidCircle;

                for (int j = i + 1; j < myProjectiles.Count; j++)
                {
                    var asteroid2 = myProjectiles[j];
                    Circle asteroid2Rect = asteroid2.b_AsteroidCircle;

                    if (asteroid1Rect.Intersects(asteroid2Rect))
                    {
                        dedProjectiles.Add(asteroid1);
                        dedProjectiles.Add(asteroid2);
                        miniExplosion.sound.Play();
                    }
                }
            }

            foreach (Projectiles p in dedProjectiles)
            {
                myProjectiles.Remove(p);
            }
            myProjectiles.RemoveAll(projectiles => projectiles.randB_AsteroidSpawn.Y > Game1.Instance.ScreenHeight + projectiles.b_AsteroidSprite.texture.Height);
            myProjectiles.RemoveAll(projectiles => projectiles.randB_AsteroidSpawn.Y < (0 - projectiles.b_AsteroidSprite.texture.Height * 2));

            if (endGame == true)
            {
                endGame = false;
                isLoaded = false;
                StateManager.ChangeState("Solo GameOver");
                gameMusic.soundInstance.Stop();
                deathExplosion.soundInstance.Play();
                myProjectiles.Clear();
                dedProjectiles.Clear();
                player1.playerSprite.position.X = Game1.Instance.ScreenWidth / 2;
                m_timer = 0f;
                b_AsteroidSpeed = 1f;
            }
        }
    



        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(background, new Rectangle(0, 0, Game1.Instance.ScreenWidth, Game1.Instance.ScreenHeight), Color.White);
            player1.Draw(spriteBatch);
            switch (playerHealth)
            {
                case 0:

                    break;
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
                default:
                    break;
            }
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
