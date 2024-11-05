using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    ///     Class for a Level 3 Enemy.
    /// </summary>
    /// <seealso cref="Galaga.Model.EnemyShip" />
    public class EnemyLevel3 : EnemyShip
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
        public override int ScoreValue { get; } = 3;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnemyLevel3" /> class.
        /// </summary>
        public EnemyLevel3()
        {
            Sprite = new EnemyLevel3Sprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Fires the missile.
        /// </summary>
        public override EnemyMissile FireMissile()
        {
            var missile = new EnemyMissile();
            missile.X = X + Width / 2.0 - missile.Width / 2.0;
            missile.Y = Y + Height;
            return missile;
        }

        #endregion
    }
}