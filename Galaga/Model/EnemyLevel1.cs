using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    /// Enemy level 1 class.
    /// </summary>
    /// <seealso cref="Galaga.Model.EnemyShip" />
    public class EnemyLevel1 : EnemyShip
    {
        #region Data members

        private const int SpeedXDirection = 3;
        private const int SpeedYDirection = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyLevel1"/> class.
        /// </summary>
        public EnemyLevel1()
        {
            Sprite = new EnemyLevel1Sprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        #endregion
    }
}
