
using Windows.Security.Cryptography.Core;

namespace Galaga.Model
{
    /// <summary>
    /// EnemyMissle class.
    /// </summary>
    public class EnemyMissle : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 0;
        private const int SpeedYDirection = 3;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyMissle"/> class.
        /// </summary>
        public EnemyMissle()
        {
            Sprite = new EnemyMissile();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        #endregion
    }
}
