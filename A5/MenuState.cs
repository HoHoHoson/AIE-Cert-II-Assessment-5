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
    public class MenuState : A5.State
    {
        bool isLoaded = false;
        Texture2D back;
        SpriteFont arial;
        Vector2 middle = Vector2.Zero;
        Vector2 title = Vector2.Zero;
        float alpha;



        public MenuState() : base()
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
                Vector2 titleMeasure = (arial.MeasureString("[Name of this Game Here]"));
                title = new Vector2(middle.X - titleMeasure.X / 2, middle.Y - titleMeasure.Y / 2);
                alpha = 0f;
            }
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            MathHelper.Clamp(alpha, 0f, 1f);
            alpha += 0.25f * deltaTime;
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(back, new Rectangle(0, 0, Game1.Instance.ScreenWidth, Game1.Instance.ScreenHeight), Color.White);
            spriteBatch.DrawString(arial, "[Name of this Game Here]", title, Color.White * alpha);
            spriteBatch.End();
        }



        public override void CleanUp()
        {
            isLoaded = false;
        }
    }
}
