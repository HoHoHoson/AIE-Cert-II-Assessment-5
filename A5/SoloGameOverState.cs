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
        float countDown = 5.4f;
        SpriteFont arial;



        public SoloGameOverState() : base()
        {
        }



        public override void Update(ContentManager Content, GameTime gameTime)
        {
            if (isLoaded == false)
            {
                isLoaded = true;
                arial = Content.Load<SpriteFont>("Arial");
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            countDown -= deltaTime;

            if (countDown <= 0)
            {
                countDown = 5.4f;
                StateManager.ChangeState("Solo Game");
            }
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 size = arial.MeasureString("GAME OVER");
            Vector2 count = arial.MeasureString("Retry in " + countDown.ToString("0"));

            spriteBatch.Begin();
            spriteBatch.DrawString(arial, "GAME OVER", new Vector2(Game1.Instance.ScreenWidth / 2 - size.X / 2, Game1.Instance.ScreenHeight / 2 - size.Y /  2), Color.Red);
            spriteBatch.DrawString(arial, "Retry in " + countDown.ToString("0"), new Vector2(Game1.Instance.ScreenWidth / 2 - count.X / 2, Game1.Instance.ScreenHeight / 2  - count.Y / 2 + size.Y), Color.Red);
            spriteBatch.End();
        }



        public override void CleanUp()
        {
            isLoaded = false;
        }
    }
}
