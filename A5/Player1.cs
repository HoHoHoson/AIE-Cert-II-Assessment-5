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
    class Player1
    {
        Sprite playerSprite = new Sprite();
        Game1 game1 = null;



        public Player1(Game1 game)
        {
            this.game1 = game;
        }



        public void Load(ContentManager content)
        {
            playerSprite.Load(content, "buttonBlue");
        }



        public void Update(float deltaTime)
        {
            playerSprite.Update(deltaTime);

            if (Keyboard.GetState().IsKeyDown(Keys.D) == true)
            {
                playerSprite.position += new Vector2(5, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) == true)
            {
                playerSprite.position += new Vector2(-5, 0);
            }

            playerSprite.position.X = MathHelper.Clamp(playerSprite.position.X, 0, game1.ScreenWidth - playerSprite.texture.Width);
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            playerSprite.Draw(spriteBatch);
        }
    }
}
