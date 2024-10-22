using Galaga.View.Sprites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Galaga.Model
{
    /// <summary>
    /// A simple physics class to check for collisions.
    /// </summary>
    public class Physics
    {
        /// <summary>
        /// Checks the collisions.
        /// </summary>
        /// <param name="listOfShips">The enemy ships.</param>
        /// <param name="missiles">The missiles.</param>
        public List<GameObject> CheckCollisions(List<GameObject> listOfShips, List<GameObject> missiles)
        {
            List<GameObject> objectsToRemove = new List<GameObject>();

            foreach (var ship in listOfShips)
            {
                foreach (var missile in missiles)
                {
                    if (!(ship is EnemyShip) && !(missile is EnemyMissile))
                    {
                        if (IsColliding(ship, missile))
                        {
                            if (!objectsToRemove.Contains(ship))
                            {
                                objectsToRemove.Add(ship);
                            }
                            if (!objectsToRemove.Contains(missile))
                            {
                                objectsToRemove.Add(missile);
                            }
                        }
                    }
                }
            }
            return objectsToRemove;
        }

        private bool IsColliding(GameObject ship, GameObject missile)
        {
            Rectangle shipRectangle = new Rectangle((int)ship.X,(int)ship.Y, (int)ship.Width, (int)ship.Height);
            Rectangle missileRectangle = new Rectangle((int)missile.X, (int)missile.Y, (int)missile.Width, (int)missile.Height);

            return shipRectangle.IntersectsWith(missileRectangle);
        }

        /// <summary>
        /// Checks the missile boundary.
        /// </summary>
        /// <param name="missiles">The missiles.</param>
        /// <param name="canvas">The canvas.</param>
        /// <returns></returns>
        public bool CheckMissileBoundary(List<GameObject> missiles, Canvas canvas)
        {
            foreach (var missile in missiles)
            {
                if (missile is EnemyMissile && missile.Y > canvas.Height)
                {
                    return true;
                }
                if (missile is PlayerMissile && missile.Y < 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
