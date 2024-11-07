using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    ///     Enemy level 1 class.
    /// </summary>
    /// <seealso cref="Galaga.Model.EnemyShip" />
    public class NonFiringEnemy : EnemyShip
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
        ///     Initializes a new instance of the <see cref="NonFiringEnemy" /> class.
        /// </summary>
        public NonFiringEnemy(BaseSprite mainSprite, BaseSprite alternateSprite) : base(mainSprite, alternateSprite)
        {
            Sprite = mainSprite;
            SetSpeed(SpeedXDirection, SpeedYDirection);
            this.PrimarySprite = mainSprite;
            this.SecondarySprite = alternateSprite;
        }

        #endregion

        /// <summary>
        ///     Swaps the sprites.
        /// </summary>
        public new void SwapSprites(int tick)
        {
            //if (tick % 2 == 0)
            //{
            //    Sprite = this.PrimarySprite;
            //}
            //else
            //{
            //    Sprite = this.SecondarySprite;
            //}
            if (Sprite == this.PrimarySprite)
            {
                Sprite = this.SecondarySprite;
            }
            else
            {
                Sprite = this.PrimarySprite;
            }
        }
    }
}