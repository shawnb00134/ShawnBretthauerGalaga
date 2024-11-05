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

        /// <summary>
        ///     The primary sprite/
        /// </summary>
        public BaseSprite PrimarySprite;

        /// <summary>
        ///     The secondary sprite
        /// </summary>
        //public EnemyLevel1SpriteAlternate SecondarySprite;
        public BaseSprite SecondarySprite;

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
        protected EnemyShip(BaseSprite mainSprite, BaseSprite alternateSprite)
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
        /// <returns></returns>
        public virtual EnemyMissile FireMissile()
        {
            var missile = new EnemyMissile();
            missile.X = X + Width / 2.0 - missile.Width / 2.0;
            missile.Y = Y + Height;
            return missile;
        }

        /// <summary>
        ///     Swaps the sprites.
        /// </summary>
        public void SwapSprites(int tick)
        {
            if (tick % 2 == 0)
            {
                Sprite = this.PrimarySprite;
            }
            else
            {
                Sprite = this.SecondarySprite;
            }
            //if (Sprite == this.PrimarySprite)
            //{
            //    Sprite = this.SecondarySprite;
            //}
            //else
            //{
            //    Sprite = this.PrimarySprite;
            //}
        }

        #endregion
    }
}