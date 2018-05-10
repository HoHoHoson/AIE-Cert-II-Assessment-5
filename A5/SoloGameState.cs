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



        public SoloGameState(Game1 game) : base()
        {
            player1 = new Player1(game);
        }



        public override void Update(ContentManager Content, GameTime gameTime)
        {
            if(isLoaded == false)
            {
                isLoaded = true;
                arial = Content.Load<SpriteFont>("Arial");
                player1.Load(Content);
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            player1.Update(deltaTime);
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
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
