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



        public SoloGameState() : base()
        {
        }



        public override void Update(ContentManager Content, GameTime gameTime)
        {
            if(isLoaded == false)
            {
                isLoaded = true;
                arial = Content.Load<SpriteFont>("Arial");
            }
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.End();
        }



        public override void CleanUp()
        {
            arial = null;
            isLoaded = false;
        }
    }
}
