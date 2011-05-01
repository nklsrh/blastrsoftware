using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace blastrs
{
    public class Blast : Microsoft.Xna.Framework.GameComponent
    {
        public Blast(Game game)
            : base(game)
        {
        }
        public float Radius;
        public Vector2 Position;
        public float Power;
        public Vector2 Direction;
        public Circle Area;
        public Texture2D Sprite;
        public bool Ready;
        public TimeSpan blastTime = new TimeSpan(0,0,0,0,500);

        public override void Initialize()
        {
            Area = new Circle();
            Ready = true;

            Power = 12;
            Radius = 50;

            base.Initialize();
        }

        public void Update(GameTime gameTime, Player Player)
        {
            if (!Ready)
            {
                blastTime -= gameTime.ElapsedGameTime;
                Position += Direction;

                Area.Center = Position;
                Area.Radius = Radius;

                if (blastTime <= TimeSpan.Zero)
                {
                    Ready = true;
                    Player.Blasting = false;
                    blastTime = new TimeSpan(0, 0, 0, 0, 500);
                }
            }

            base.Update(gameTime);
        }

        public void PlayerImpact(GameTime gameTime, Player Player, int index)
        {
            if (Area.Intersects(new Rectangle((int)Player.Position.X, (int)Player.Position.Y, 1, 1)))
            {
                Player.Speed += Direction;
                GamePad.SetVibration((PlayerIndex)(index), 1.0f, 1.0f);
            }
            else
            {
                GamePad.SetVibration((PlayerIndex)(index), 0f, 0f);
            }
        }
        public void BoxImpact(GameTime gameTime, Box Box)
        {
            if (Area.Intersects(new Rectangle((int)Box.Position.X, (int)Box.Position.Y, 1, 1)))
            {
                Box.Position += Direction * 0.5f;
            }
            else
            {
            }
        }
        public void BotImpact(GameTime gameTime, Bot Bot)
        {
            if (Area.Intersects(new Rectangle((int)Bot.Position.X, (int)Bot.Position.Y, 1, 1)))
            {
                Bot.Position += Direction * 0.4f;
            }
            else
            {
            }
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            if (!Ready)
            {
                if (Direction.Y <= 0)
                { sb.Draw(Sprite, Position, null, Color.White, (float)(Math.Atan(-Direction.X / Direction.Y)), new Vector2(Sprite.Width / 2, Sprite.Height / 2), (float)(Radius / (50)), SpriteEffects.None, 0f); }
                if (Direction.Y > 0)
                { sb.Draw(Sprite, Position, null, Color.White, (float)(Math.PI + Math.Atan(-Direction.X / Direction.Y)), new Vector2(Sprite.Width / 2, Sprite.Height / 2), (float)(Radius / (50)), SpriteEffects.None, 0f); }
            }
            sb.End();
        }
    }
}