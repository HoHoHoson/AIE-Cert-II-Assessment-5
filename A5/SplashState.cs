using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A5
{
    public class SplashState : A5.State
    {
        bool isLoaded = false;
        SpriteFont arial;
        Texture2D back;
        Vector2 middle = Vector2.Zero;
        float alpha = 0.0f;
        float splashTimer = 0f;



        public SplashState() : base()
        {
        }



        public override void Update(ContentManager content, GameTime gameTime)
        {
            if (isLoaded == false)
            {
                isLoaded = true;
                middle = new Vector2(Game1.Instance.ScreenWidth / 2, Game1.Instance.ScreenHeight / 2);
                back = content.Load<Texture2D>("back");
                arial = content.Load<SpriteFont>("Arial");
            }
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            splashTimer += deltaTime;
            alpha = MathHelper.Clamp(alpha, 0f, 1f);
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
                StateManager.ChangeState("Menu");
            if (splashTimer >= 0.5f && splashTimer <= 5.5f)
                alpha += 0.25f * deltaTime;
            else alpha -= 0.25f * deltaTime;
            if (splashTimer >= 10)
            {
                splashTimer = 0;
                StateManager.ChangeState("Menu");
            }
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 credit = arial.MeasureString("A Brandon & Hoson Production");

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(back, new Rectangle(0, 0, Game1.Instance.ScreenWidth, Game1.Instance.ScreenHeight), Color.White);
            spriteBatch.DrawString(arial, "A Brandon & Hoson Production", new Vector2(middle.X - credit.X / 2, middle.Y - credit.Y / 2), Color.White * alpha);
            spriteBatch.End();
        }



        public override void CleanUp()
        {
            isLoaded = false;
        }
    }
}
