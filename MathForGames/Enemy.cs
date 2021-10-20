using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Enemy : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Actor _target;
        private Vector2 _forward = new Vector2(1, 0);
        private Vector2 fieldOfView = new Vector2(100, 50);

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Vector2 Forward
        {
            get { return _forward; }
            set { _forward = value; }
        }

        public Enemy(char icon, float x, float y, float speed, Color color, Actor target, string name = "Enemy")
            : base(icon, x, y, color, name)
        {
            _target = target;
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            // Create a vector that stores the move input.
            Vector2 moveDirection = _target.Position - Position;

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            if(GetTargetInSight() && moveDirection <= fieldOfView)
                Position += Velocity;
        }

        public bool GetTargetInSight()
        {
            Vector2 directionOfTarget = (_target.Position - Position).Normalized;
            Vector2 moveDirection = _target.Position - Position;

            if (moveDirection.X > 150 || moveDirection.Y > 100)
                return false;
            else if (moveDirection.X < -150 || moveDirection.Y < -100)
                return false;

            return Vector2.DotProduct(directionOfTarget, Forward) > 0;
        }
    }
}
