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
    class Projectiles_vs
    {
        Game1 game1 = null;
        public Sprite projSprite = new Sprite();
        //public Texture2D versusProjectile;
        //public Vector2 versusProjectilePosition = Vector2.Zero;
        Vector2 versusProjectileOffset = Vector2.Zero;
        public BoundingSphere b_projectileSphere;
        public Vector2 velocity = Vector2.Zero;
        public bool hitPlayer1;
        public bool hitPlayer2;
        Random rnd = new Random();
        public static int firstDirection;
        public Vector2 direction;



        public Projectiles_vs(Game1 game)
        {
            hitPlayer1 = false;
            hitPlayer2 = false;
            game1 = game;
        }



        public void Load(ContentManager content)
        {
            projSprite.Load(content, "versusProjectile");
            projSprite.offset = new Vector2(projSprite.texture.Width / 2, projSprite.texture.Height / 2);
            projSprite.origin = new Vector2(game1.ScreenWidth / 2, game1.ScreenHeight / 2);
            firstDirection = rnd.Next(1, 800);
        }


        public void Update(float deltaTime)
        {

            if (!hitPlayer1)
            {
                direction = new Vector2(game1.ScreenWidth - firstDirection, game1.ScreenHeight) - projSprite.origin;
                direction.Normalize();
                velocity = direction * 4;
            }
            //if (!hitPlayer2)
            //{
            //    Vector2 direction = new Vector2(game1.ScreenWidth - 20, game1.ScreenHeight) - projSprite.position;
            //    direction.Normalize();
            //    velocity = direction * 300 * deltaTime;
            //}
            projSprite.origin += velocity;


            b_projectileSphere = new BoundingSphere(new Vector3(projSprite.origin, 0), 15);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            projSprite.Draw(spriteBatch);
        }

        public void SetVelocity(Vector2 vel)
        {
            this.velocity = vel;
        }
        public Vector2 GetVelocity()
        {
            return velocity;
        }
    }
}
