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
    class Projectiles
    {
        Game1 game1 = null;
        Texture2D brownAsteroid;
        public Vector2 brownAsteroidPosition = Vector2.Zero;
        Vector2 brownAsteroidOffset = Vector2.Zero;


        public Projectiles(Game1 game)
        {
            game1 = game;
        }



        public void Load(ContentManager content)
        {
            brownAsteroid = content.Load<Texture2D>("meteorBrown");
            brownAsteroidOffset = new Vector2(brownAsteroid.Width / 2, brownAsteroid.Height / 2);
        }



        public void Update(float deltaTime)
        {
            Vector2 direction = Player1.Instance.playerSprite.position - brownAsteroidPosition;
            direction.Normalize();
            Vector2 velocity = direction * 100 * deltaTime;
            brownAsteroidPosition += velocity;
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(brownAsteroid, brownAsteroidPosition - brownAsteroidOffset, Color.White);
        }
    }
}
