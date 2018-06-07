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
        public Sprite playerSprite = new Sprite();
        public Game1 game1 = null;
        public float acceleration = 0.0f;
        static Player1 instance;
        public Rectangle player1Rect;



        public static Player1 Instance
        {
            get
            {
                return instance;
            }
        }



        public Player1(Game1 game)
        {
            game1 = game;
            instance = this;
        }



        public void Load(ContentManager content)
        {
            playerSprite.Load(content, "buttonBlue");
        }



        public void Update(float deltaTime)
        {
            playerSprite.Update(deltaTime);
            bool wasMovingRight = acceleration > 0f;
            bool wasMovingLeft = acceleration < 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.D) == true)
            {
                acceleration += 2f;
            }
            else if (wasMovingRight == true)
            {
                acceleration -= 0.5f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) == true)
            {
                acceleration -= 2f;
            }
            else if (wasMovingLeft)
            {
                acceleration += 0.5f;
            }
            if ((wasMovingRight && (acceleration < 0f)) || (wasMovingLeft && (acceleration > 0f)))
            {
                acceleration = 0f;
            }

            playerSprite.position.X += acceleration;
            playerSprite.position.X = MathHelper.Clamp(playerSprite.position.X, 0 + playerSprite.texture.Width / 2, game1.ScreenWidth - playerSprite.texture.Width / 2);
            acceleration = MathHelper.Clamp(acceleration, -10, 10);
            playerSprite.position.Y = game1.ScreenHeight - playerSprite.texture.Height - 10;
            player1Rect = new Rectangle((int)(playerSprite.position.X - playerSprite.offset.X), (int)(playerSprite.position.Y - playerSprite.offset.Y), playerSprite.texture.Width, playerSprite.texture.Height);
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            playerSprite.Draw(spriteBatch);
        }
    }
}
