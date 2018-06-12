using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A5
{
    class Audio
    {
        public SoundEffect sound = null;
        public SoundEffectInstance soundInstance = null;




        public Audio()
        {
        }




        public void Load(ContentManager content, string asset)
        {
            sound = content.Load<SoundEffect>(asset);
            soundInstance = sound.CreateInstance();
        }




        public void Update(float deltaTime)
        {
        }




        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
