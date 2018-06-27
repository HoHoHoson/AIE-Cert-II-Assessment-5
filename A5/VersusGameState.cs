using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace A5
{
    public class VersusGameState : A5.State
    {
        bool isLoaded = false;
        SpriteFont arial = null;
        Player1_vs player1 = null;
        Player2_vs player2 = null;
        Texture2D background = null;
        Game1 Game1 = null;
        Projectiles_vs projectiles;
        Audio bgMusic = new Audio();
        public static bool p1Wins = false;
        float startTimer;
        
        



        public VersusGameState(Game1 game) : base()
        {
            this.Game1 = game;
            player1 = new Player1_vs(game);
            player2 = new Player2_vs(game);
            projectiles = new Projectiles_vs(game);
        }



        public override void Update(ContentManager Content, GameTime gameTime)
        {
            if (isLoaded == false)
            {
                isLoaded = true;
                player1.Load(Content);
                player2.Load(Content);
                projectiles.Load(Content);
                bgMusic.Load(Content, "versusMusic");
                bgMusic.soundInstance.IsLooped = true;
                bgMusic.soundInstance.Play();
                arial = Content.Load<SpriteFont>("Arial");
                background = Content.Load<Texture2D>("VersusBackground");
                startTimer = 0f;
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            startTimer += deltaTime;

            player1.Update(deltaTime);

            player2.Update(deltaTime);
            if (startTimer >= 3)
            projectiles.Update(deltaTime);

            Vector2 pos = new Vector2(-player1.playerSprite.texture.Width / 2, player1.playerSprite.texture.Height / 2);
            Vector2 pos2 = new Vector2(player1.playerSprite.texture.Width / 2, player1.playerSprite.texture.Height / 2);
            Vector2 bet = pos2 - pos;
            bet.Normalize();
            Vector2 nor = Vector2.Zero;
            nor.X = bet.Y;
            nor.Y = -bet.X;
            Vector2 newVel = projectiles.GetVelocity() - 2 * Vector2.Dot(projectiles.GetVelocity(), nor) * nor;
            if (projectiles.b_projectileSphere.Intersects(player1.b_playerBox))
            {
                projectiles.hitPlayer1 = true;
                projectiles.SetVelocity(newVel);
                p1Wins = true;
            }
            Vector2 p2pos1 = new Vector2(player2.playerSprite.origin.X, player2.playerSprite.origin.Y);
            Vector2 p2Pos2 = new Vector2(player2.playerSprite.origin.X + player2.playerSprite.texture.Width, player2.playerSprite.texture.Height + player2.playerSprite.origin.Y);
            Vector2 p2bet = p2Pos2 - p2pos1;
            p2bet.Normalize();
            Vector2 p2Normal = Vector2.Zero;
            p2Normal.X = p2bet.Y;
            p2Normal.Y = -p2bet.X;
            Vector2 p2newVel = projectiles.GetVelocity() - 2 * Vector2.Dot(projectiles.GetVelocity(), p2Normal) * p2Normal;

            if (player2.b_playerBox.Intersects(projectiles.b_projectileSphere))
            {
                projectiles.hitPlayer1 = true;           
                projectiles.SetVelocity(p2newVel);
            }

            if (projectiles.projSprite.origin.X >= Game1.Instance.ScreenWidth)
            {
                projectiles.hitPlayer1 = true;           

                Vector2 position1 = new Vector2(Game1.Instance.ScreenWidth, 0);
                Vector2 position2 = new Vector2(Game1.Instance.ScreenWidth, Game1.Instance.ScreenHeight);
                Vector2 between = position2 - position1;
                between.Normalize();
                Vector2 normal = Vector2.Zero;
                normal.X = between.Y;
                normal.Y = -between.X;
                Vector2 newVelocity = projectiles.GetVelocity() - 2 * Vector2.Dot(projectiles.GetVelocity(), normal) * normal;
                projectiles.SetVelocity(newVelocity);
            }

            if (projectiles.projSprite.origin.X <= 0)
            {
                projectiles.hitPlayer2 = true;

                Vector2 position1 = new Vector2(0, 0);
                Vector2 position2 = new Vector2(0, Game1.Instance.ScreenHeight);
                Vector2 between = position2 - position1;
                between.Normalize();
                Vector2 normal = Vector2.Zero;
                normal.X = between.Y;
                normal.Y = -between.X;
                Vector2 newVelocity = projectiles.GetVelocity() - 2 * Vector2.Dot(projectiles.GetVelocity(), normal) * normal;
                projectiles.SetVelocity(newVelocity);
            }

            if (projectiles.projSprite.origin.Y + projectiles.projSprite.offset.Y <= 0 || projectiles.projSprite.origin.Y + projectiles.projSprite.offset.Y >= Game1.Instance.ScreenHeight)
            {
                StateManager.ChangeState("Versus GameOver");
                bgMusic.soundInstance.Stop();
                isLoaded = false;
            }


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(background, new Rectangle(0, 0, Game1.Instance.ScreenWidth, Game1.Instance.ScreenHeight), Color.White);
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            
            projectiles.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void CleanUp()
        {
            arial = null;
            isLoaded = false;
        }
    }
}