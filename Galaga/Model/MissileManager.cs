using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace Galaga.Model
{
    /// <summary>
    ///     Missile controller
    /// </summary>
    public class MissileManager
    {
        private readonly Random random;
        private const int MissileDelayLimit = 10;
        private const int EnemyFireCounter = 30;
        private const int PlayerMissileLimit = 3;
        private int PlayerMissileCount { get; set; }
        private int DelayTicker;
        

        /// <summary>
        ///     Initializes a new instance of the <see cref="MissileManager"/> class.
        /// </summary>
        public MissileManager()
        {
            this.random = new Random();
            this.PlayerMissileCount = 0;
            this.DelayTicker = 10;
        }

        /// <summary>
        ///     Fires the missile.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="canvas">The canvas.</param>
        /// <returns></returns>
        public GameObject FireMissile(Player player, Canvas canvas)
        {
            if (this.PlayerMissileCount < PlayerMissileLimit && this.DelayTicker > MissileDelayLimit)
            {
                this.PlayerMissileCount++;
                this.DelayTicker = 0;

                var missile = new PlayerMissile();
                missile.X = player.X + player.Width / 2.0 - missile.Width / 2.0;
                missile.Y = player.Y - missile.Height;
                canvas.Children.Add(missile.Sprite);

                return missile;
            }

            return null;
        }

        /// <summary>
        ///     Moves the missiles.
        /// </summary>
        /// <param name="missiles">The missiles.</param>
        public void MoveMissiles(List<GameObject> missiles)
        {
            foreach (var missile in missiles)
            {
                if (missile is PlayerMissile)
                {
                    missile.MoveUp();
                }
                
                if (missile is EnemyMissile)
                {
                    missile.MoveDown();
                }
            }
        }

        /// <summary>
        ///     Fires the enemy missiles.
        /// </summary>
        /// <param name="enemyShips">The enemy ships.</param>
        /// <param name="canvas">The canvas.</param>
        /// <returns></returns>
        public EnemyMissile FireEnemyMissiles(List<EnemyShip> enemyShips, Canvas canvas)
        {
            GameObject missileObject = null;

            if (this.random.Next(EnemyFireCounter) == 0)
            {
                var eligibleShips = enemyShips.Where(ship => ship is FiringEnemy);
                var count = eligibleShips.Count();

                if (count > 0)
                {
                    var randomIndex = this.random.Next(count);
                    var randomShip = eligibleShips.ElementAt(randomIndex);
                    var missile = randomShip.FireMissile();
                    canvas.Children.Add(missile.Sprite);
                    missileObject = missile;
                }
            }

            return missileObject as EnemyMissile;
        }

        /// <summary>
        ///     Decrements the player missile count.
        /// </summary>
        public void DecrementPlayerMissileCount()
        {
            this.PlayerMissileCount--;
        }

        /// <summary>
        ///     Updates the delay tick.
        /// </summary>
        public void UpdateDelayTick()
        {
            this.DelayTicker++;
        }
    }
}
