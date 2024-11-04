using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    ///     Enemy level 2 class.
    /// </summary>
    /// <seealso cref="Galaga.Model.EnemyLevel1" />
    public class EnemyLevel2 : EnemyLevel1
    {
        #region Data members

        private const int SpeedXDirection = 3;
        private const int SpeedYDirection = 0;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnemyLevel2" /> class.
        /// </summary>
        public EnemyLevel2()
        {
            Sprite = new EnemyLevel2Sprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        #endregion
    }
}