using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    ///     Class for a Level 3 Enemy.
    /// </summary>
    /// <seealso cref="Galaga.Model.EnemyShip" />
    public class FiringEnemy : EnemyShip
    {
        #region Data members

        private const int SpeedXDirection = 3;
        private const int SpeedYDirection = 0;

        /// <summary>
        ///     The primary sprite/
        /// </summary>
        public new BaseSprite PrimarySprite;

        /// <summary>
        ///     The secondary sprite
        /// </summary>
        public new BaseSprite SecondarySprite;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the score value.
        /// </summary>
        /// <value>
        ///     The score value.
        /// </value>
        public override int ScoreValue { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FiringEnemy" /> class.
        /// </summary>
        public FiringEnemy(BaseSprite mainSprite, BaseSprite alternateSprite) : base(mainSprite, alternateSprite)
        {
            Sprite = mainSprite;
            SetSpeed(SpeedXDirection, SpeedYDirection);
            this.PrimarySprite = mainSprite;
            this.SecondarySprite = alternateSprite;
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

        ///// <summary>
        /////     Swaps the sprites.
        ///// </summary>
        //public new void SwapSprites()
        //{
        //    if (Sprite == this.PrimarySprite)
        //    {
        //        Sprite = this.SecondarySprite;
        //    }
        //    else
        //    {
        //        Sprite = this.PrimarySprite;
        //    }
        //}
    }
}