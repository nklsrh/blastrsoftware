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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Bot : Microsoft.Xna.Framework.GameComponent
    {
        public Bot(Game game)
            : base(game)
        { }

        public Vector2 Position;
        public Vector2 StartPosition;
        public Vector2 Speed;
        public bool isDead;
        public Texture2D Sprite;
        float[] Distance = new float[2];

        public void Initialize(Game1 game)
        {
            Distance[0] = new float();
            Distance[1] = new float();

            Sprite = game.Content.Load<Texture2D>("bombot2");
            base.Initialize();
        }

        public void Update(GameTime gameTime, Texture2D CollisionMap, Player[] Players)
        {
            Color[] bgColorArr = new Color[1];
            CollisionMap.GetData<Color>(0, new Rectangle((int)Position.X, (int)Position.Y, 1, 1), bgColorArr, 0, 1);
            Color bgColor = bgColorArr[0];

            if (bgColor == new Color(88, 88, 88)) //FALLS OFFFFFFFFFFF
            {
                isDead = true;
                Position = StartPosition;
            }

            for (int x = 0; x < 2; x++)
            {
                Distance[x] = Vector2.Distance(Players[x].Position, Position);
            }

            if (Distance[0] < Distance[1])
            {
                Speed = Vector2.Normalize(Vector2.Subtract(Players[0].Position, Position)) / 10f;
            }
            else
            {
                Speed = Vector2.Normalize(Vector2.Subtract(Players[1].Position, Position)) / 10f;
            }

            Position += Speed;

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(Sprite, Position, Color.White);
            sb.End();
        }
    }
}