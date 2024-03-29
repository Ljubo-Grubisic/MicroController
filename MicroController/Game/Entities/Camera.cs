﻿using microController.helpers;
using microController.graphics;
using SFML.System;
using SFML.Graphics;
using System;
using microController.system;
using SFML.Window;
using System.Management.Instrumentation;
using Microsoft.Win32;

namespace microController.game.entities
{
    public class Camera : Entity
    {
        public float Speed { get; set; } = 13;
        private float scale = 0.06f;

        private Vector2f DeltaPosition;
        private Rectangle Rectangle { get; set; } = new Rectangle(new Vector2f(), new Vector2f());
        private Texture CameraTexture { get; set; }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        public override Vector2f Size
        {
            get { return Rectangle.Size; }
            set
            {
                base.Size = value;
                Rectangle.Size = base.Size;
            }
        }
        public override float SizeX
        {
            get { return Rectangle.Size.X; }
            set
            {
                base.SizeX = value;
                Rectangle.Size = base.Size;
            }
        }
        public override float SizeY
        {
            get { return Rectangle.Size.Y; }
            set
            {
                base.SizeY = value;
                Rectangle.Size = base.Size;
            }
        }

        public Camera(Vector2f position, Game game) : base(game)
        {
            this.Position = position;
            
            this.DeltaPosition.X = (float)(Math.Cos(this.Rotation));
            this.DeltaPosition.Y = (float)(Math.Sin(this.Rotation));

            this.CameraTexture = ImageHelper.LoadImgNoBackground("Camera.png");
            this.Size = (Vector2f)CameraTexture.Size * Scale;

            this.Rectangle = new Rectangle(Position, Size, CameraTexture);
        }

        public override void Draw(RenderWindow window)
        {
            this.Rectangle.Draw(window);
        }

        protected override void OnUpdate(Map map, GameTime gameTime)
        {
            this.Rectangle.Size = (Vector2f)this.CameraTexture.Size * Scale * game.Scale.NumPixelPerCm;
            this.Size = this.Rectangle.Size;
            this.Rectangle.Position = this.DrawingPosition;
            this.Rectangle.Origin = new Vector2f(Rectangle.SizeX / 2, Rectangle.SizeY / 2);
            this.Rectangle.Rotation = MathHelper.RadiansToDegrees(this.Rotation) + 180;

            if (KeyboardManager.IsKeyDown(Keyboard.Key.A))
            {
                this.Rotation -= 0.05f * gameTime.DeltaTime * 75;
                if (this.Rotation < 0)
                {
                    this.Rotation += 2f * (float)Math.PI;
                }
                this.DeltaPosition.X = (float)(Math.Cos(this.Rotation));
                this.DeltaPosition.Y = (float)(Math.Sin(this.Rotation));
            }
            if (KeyboardManager.IsKeyDown(Keyboard.Key.D))
            {
                this.Rotation += 0.05f * gameTime.DeltaTime * 75;
                if (this.Rotation > 2f * (float)Math.PI)
                {
                    this.Rotation -= 2f * (float)Math.PI;
                }
                this.DeltaPosition.X = (float)(Math.Cos(this.Rotation));
                this.DeltaPosition.Y = (float)(Math.Sin(this.Rotation));
            }
            if (KeyboardManager.IsKeyDown(Keyboard.Key.W))
            {
                this.PositionX += this.DeltaPosition.X * gameTime.DeltaTime * game.Scale.NumPixelPerCm * this.Speed;
                this.PositionY += this.DeltaPosition.Y * gameTime.DeltaTime * game.Scale.NumPixelPerCm * this.Speed;
            }
            if (KeyboardManager.IsKeyDown(Keyboard.Key.S))
            {
                this.PositionX -= this.DeltaPosition.X * gameTime.DeltaTime * game.Scale.NumPixelPerCm * this.Speed;
                this.PositionY -= this.DeltaPosition.Y * gameTime.DeltaTime * game.Scale.NumPixelPerCm * this.Speed;
            }
        }
    }
}
