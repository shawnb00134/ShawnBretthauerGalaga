using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Model
{
    /// <summary>
    /// Player missle class
    /// </summary>
    /// <seealso cref="Galaga.Model.GameObject" />
    public class PlayerMissle : GameObject
    {
        private const int SpeedXDirection = 0;
        private const int SpeedYDirection = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerMissle"/> class.
        /// </summary>
        public PlayerMissle()
        {
            Sprite = new PlayerMissle();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
    }
}
