using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    ///     Abstract class for enemy ships.
    /// </summary>
    /// <seealso cref="Galaga.Model.GameObject" />
    public abstract class EnemyShip : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 3;
        private const int SpeedYDirection = 0;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the score value.
        /// </summary>
        /// <value>
        ///     The score value.
        /// </value>
        public virtual int ScoreValue { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnemyShip" /> class.
        /// </summary>
        protected EnemyShip()
        {
            Sprite = new EnemyLevel1Sprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Fires the missile.
        /// </summary>
        /// <returns></returns>
        public virtual EnemyMissile FireMissile()
        {
            var missile = new EnemyMissile();
            missile.X = X + Width / 2.0 - missile.Width / 2.0;
            missile.Y = Y + Height;
            return missile;
        }

        #endregion
    }
}