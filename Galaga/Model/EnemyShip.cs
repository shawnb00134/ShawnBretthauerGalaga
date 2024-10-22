using Galaga.View.Sprites;

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

        #endregion

    }
}
