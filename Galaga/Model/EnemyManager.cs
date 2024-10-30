using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Galaga.Model
{
    public class EnemyManager
    {
        private Canvas canvas;

        public EnemyManager(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public List<EnemyShip> CreateAndPlaceEnemyShip()
        {
            List<EnemyShip> enemyShips = new List<EnemyShip>();

            double canvasWidth = this.canvas.Width;
            int[] enemiesPerRow = { 2, 3, 4 };
            double startY = 200;
            double rowSpacing = 100;

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
                if (ship == null)
                {
                    System.Diagnostics.Debug.WriteLine(tickCounter);
                }

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
