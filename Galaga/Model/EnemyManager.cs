using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Galaga.Model
{
    /// <summary>
    /// EnenemyManager class.
    /// </summary>
    public class EnemyManager
    {
        private readonly Canvas canvas;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyManager"/> class.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        public EnemyManager(Canvas canvas)
        {
            this.canvas = canvas;
        }

        /// <summary>
        /// Creates and places the enemy ships.
        /// </summary>
        /// <returns></returns>
        public List<EnemyShip> CreateAndPlaceEnemyShip()
        {
            List<EnemyShip> enemyShips = new List<EnemyShip>();

            double canvasWidth = this.canvas.Width;
            int[] enemiesPerRow = { 2, 3, 4 };
            const double startY = 200;
            const double rowSpacing = 100;

            for (int rowIndex = 0; rowIndex < enemiesPerRow.Length; rowIndex++)
            {
                int enemyCount = enemiesPerRow[rowIndex];
                double spacing = canvasWidth / (enemyCount + 1);

                for (int i = 0; i < enemyCount; i++)
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

                    double xPosition = (i + 1) * spacing - enemyShip.Width / 2.0;
                    enemyShip.X = xPosition;
                    enemyShip.Y = startY - rowIndex * rowSpacing;
                }
            }

            return enemyShips;
        }

        /// <summary>
        /// Moves the enemy ships.
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
    }
}
