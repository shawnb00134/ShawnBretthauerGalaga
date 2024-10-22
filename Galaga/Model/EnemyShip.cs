﻿using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    /// Abstract class for enemy ships.
    /// </summary>
    /// <seealso cref="Galaga.Model.GameObject" />
    public abstract class EnemyShip : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 3;
        private const int SpeedYDirection = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyShip"/> class.
        /// </summary>
        protected EnemyShip()
        {
            Sprite = new EnemyLevel1Sprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        public virtual EnemyMissile FireMissile()
        {
            var missile = new EnemyMissile();
            missile.X = this.X + this.Width / 2.0 - missile.Width / 2.0;
            missile.Y = this.Y + this.Height;
            return missile;
        }

        #endregion

    }
}