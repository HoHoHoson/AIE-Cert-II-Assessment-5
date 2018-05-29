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
        SpriteFont arial = null;
        Player1 player1 = null;
        Texture2D background = null;
        Game1 game = null;
        Projectiles projectiles;
        List<Projectiles> myProjectiles = new List<Projectiles>();


        public SoloGameState(Game1 game) : base()
        {
            this.game = game;
            player1 = new Player1(game);
            projectiles = new Projectiles(game);

            for (int i = 0; i < 10; ++i)
            {
                Projectiles p = new Projectiles(game);
                myProjectiles.Add(p);
            }

        }



        public override void Update(ContentManager Content, GameTime gameTime)
        {
            if(isLoaded == false)
            {
                isLoaded = true;
                player1.Load(Content);
                projectiles.Load(Content);
                arial = Content.Load<SpriteFont>("Arial");
                background = Content.Load<Texture2D>("SoloBackground");
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            player1.Update(deltaTime);
            projectiles.Update(deltaTime);

            for (int b_Asteroid = 0; b_Asteroid < projectiles.brownAsteroidPositions.Count; b_Asteroid++)
            {
                Vector2 position = (Vector2)projectiles.brownAsteroidPositions[b_Asteroid];
                Vector2 velocity = (Vector2)projectiles.brownAsteroidVelocities[b_Asteroid];
                Rectangle b_AsteroidRect = new Rectangle((int)(position.X - projectiles.brownAsteroidOffset.X), (int)(position.Y - projectiles.brownAsteroidOffset.Y), projectiles.brownAsteroid.Width, projectiles.brownAsteroid.Height);
                if (b_AsteroidRect.Intersects(player1.player1Rect) && b_AsteroidRect.Bottom - 2 < player1.player1Rect.Top)
                {
                    velocity.Y = -velocity.Y;
                    projectiles.brownAsteroidVelocities[b_Asteroid] = velocity;
                }
                else if (b_AsteroidRect.Intersects(player1.player1Rect))
                {
                    projectiles.brownAsteroidPositions.RemoveAt(b_Asteroid);
                    projectiles.brownAsteroidVelocities.RemoveAt(b_Asteroid);
                }

                if (position != (Vector2)projectiles.brownAsteroidPositions[b_Asteroid])
                {
                    projectiles.brownAsteroidPositions.RemoveAt(b_Asteroid);
                    projectiles.brownAsteroidVelocities.RemoveAt(b_Asteroid);
                }
                //if (b_AsteroidRect.Intersects(b_AsteroidRect))
                //{
                //        projectiles.brownAsteroidPositions.RemoveAt(b_Asteroid);
                //    projectiles.brownAsteroidVelocities.RemoveAt(b_Asteroid);
                //}


            }
            foreach (var porjectile in myProjectiles)
            {
                //porjectile.asteriodPos = new Vector2
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(background, new Rectangle(0, 0, Game1.Instance.ScreenWidth, Game1.Instance.ScreenHeight), Color.White);
            player1.Draw(spriteBatch);
            projectiles.Draw(spriteBatch, arial);
            spriteBatch.End();
        }



        public override void CleanUp()
        {
            arial = null;
            isLoaded = false;
        }
    }
}
