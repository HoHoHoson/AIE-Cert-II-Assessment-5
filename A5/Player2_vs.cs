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
    class Player2_vs
    {
        public Sprite playerSprite = new Sprite();
        public Game1 game1 = null;
        public float acceleration = 0.0f;
        static Player2_vs instance;
        public Rectangle player2Rect;
        public BoundingBox b_playerBox;
        

        public static Player2_vs Instance
        {
            get
            {
                return instance;
            }
        }



        public Player2_vs(Game1 game)
        {
            game1 = game;           
            instance = this;
        }



        public void Load(ContentManager content)
        {
            playerSprite.Load(content, "buttonRed");
        }


        public void Update(float deltaTime)
        {
            playerSprite.Update(deltaTime);
            bool wasMovingRight = acceleration > 0f;
            bool wasMovingLeft = acceleration < 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Right) == true)
            {
                acceleration += 2f;
            }
            else if (wasMovingRight == true)
            {
                acceleration -= 0.5f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) == true)
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

            playerSprite.origin.X += acceleration;
            playerSprite.origin.X = MathHelper.Clamp(playerSprite.origin.X, 0 + playerSprite.texture.Width / 2, game1.ScreenWidth - playerSprite.texture.Width / 2);
            acceleration = MathHelper.Clamp(acceleration, -10, 10);
            playerSprite.origin.Y = 0 + playerSprite.texture.Height;
            player2Rect = new Rectangle((int)(playerSprite.origin.X - playerSprite.offset.X), (int)(playerSprite.origin.Y - playerSprite.offset.Y),
                            playerSprite.texture.Width, playerSprite.texture.Height);
            b_playerBox = new BoundingBox(new Vector3(playerSprite.origin.X - playerSprite.texture.Width / 2, playerSprite.origin.Y - playerSprite.texture.Height / 2, 1),
                                            new Vector3(playerSprite.texture.Width + playerSprite.origin.X, playerSprite.origin.Y + playerSprite.texture.Height / 2, 1));
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            playerSprite.Draw(spriteBatch);
            //spriteBatch.Draw(playerSprite.texture, null, new Rectangle((int)b_playerBox.Min.X, (int)b_playerBox.Min.Y, (int)b_playerBox.Max.X, (int)b_playerBox.Max.Y));
        }
    }
}