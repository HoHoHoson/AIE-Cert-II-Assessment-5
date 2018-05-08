using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace A5
{
    public class SoloGameOverState : A5.State
    {
        bool isLoaded = false;



        public SoloGameOverState() : base()
        {
        }



        public override void Update(ContentManager Content, GameTime gameTime)
        {
            if (isLoaded == false)
            {
                isLoaded = true;
            }
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.End();
        }



        public override void CleanUp()
        {
            isLoaded = false;
        }
    }
}
