﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace A5
{
    class Sprite
    {
        public Vector2 position = Vector2.Zero;
        public Vector2 offset = Vector2.Zero;

        public Texture2D texture;



        public Sprite()
        {
        }



        public void Load (ContentManager content, string asset)
        {
            texture = content.Load<Texture2D>(asset);
            offset = new Vector2(texture.Width / 2, texture.Height / 2);
        }



        public void Update(float deltaTime)
        {
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position - offset, Color.White);
        }
    }
}
