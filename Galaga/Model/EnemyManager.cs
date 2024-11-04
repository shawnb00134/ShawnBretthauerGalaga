using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Galaga.Model
{
    /// <summary>
    ///     EnenemyManager class.
    /// </summary>
    public class EnemyManager
    {
        #region Data members

        private readonly Canvas canvas;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnemyManager" /> class.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        public EnemyManager(Canvas canvas)
        {
            this.canvas = canvas;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Creates and places the enemy ships.
        /// </summary>
        /// <returns></returns>
        public List<EnemyShip> CreateAndPlaceEnemyShip()
        {
            var enemyShips = new List<EnemyShip>();

            const double rowSpacing = 100;
            var canvasWidth = this.canvas.Width;
            var startY = this.canvas.Height / 2;
            int[] enemiesPerRow = { 2, 3, 4 };

            for (var rowIndex = 0; rowIndex < enemiesPerRow.Length; rowIndex++)
            {
                var enemyCount = enemiesPerRow[rowIndex];
                var spacing = canvasWidth / (enemyCount + 1);

                for (var i = 0; i < enemyCount; i++)
                {
                    EnemyShip enemyShip;
                    if (rowIndex == 0)
                    {
                        enemyShip = new EnemyLevel1();
                    }
                    else if (rowIndex == 1)
                    {
                        enemyShip = new EnemyLevel2();
                    }
                    else
                    {
                        enemyShip = new EnemyLevel3();
                    }

                    this.canvas.Children.Add(enemyShip.Sprite);
                    enemyShips.Add(enemyShip);

                    var xPosition = (i + 1) * spacing - enemyShip.Width / 2.0;
                    enemyShip.X = xPosition;
                    enemyShip.Y = startY - rowIndex * rowSpacing;
                }
            }

            return enemyShips;
        }

        /// <summary>
        ///     Moves the enemy ships.
        /// </summary>
        /// <param name="enemyShips">The enemy ships.</param>
        /// <param name="tickCounter">The tick counter.</param>
        public void MoveEnemyShips(List<EnemyShip> enemyShips, int tickCounter)
        {
            foreach (var ship in enemyShips)
            {
                if (ship != null)
                {
                    if (tickCounter < 10)
                    {
                        ship.MoveLeft();
                    }
                    else if (tickCounter < 30)
                    {
                        ship.MoveRight();
                    }
                    else if (tickCounter < 40)
                    {
                        ship.MoveLeft();
                    }
                }
            }
        }

        #endregion
    }
}