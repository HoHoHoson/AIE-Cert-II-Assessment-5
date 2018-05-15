using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace A5
{
    public class SoloGameState : A5.State
    {
        bool isLoaded = false;
        SpriteFont arial = null;
        Player1 player1 = null;
        Texture2D background = null;
        Game1 game = null;




        public SoloGameState(Game1 game) : base()
        {
            player1 = new Player1(game);
            this.game = game;
        }



        public override void Update(ContentManager Content, GameTime gameTime)
        {
            if(isLoaded == false)
            {
                isLoaded = true;
                player1.Load(Content);
                arial = Content.Load<SpriteFont>("Arial");
                background = Content.Load<Texture2D>("SoloBackground");
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            player1.Update(deltaTime);
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(background, new Rectangle(0, 0, Game1.Instance.ScreenWidth, Game1.Instance.ScreenHeight), Color.White);
            player1.Draw(spriteBatch);
            spriteBatch.End();
        }



        public override void CleanUp()
        {
            arial = null;
            isLoaded = false;
        }
    }
}
