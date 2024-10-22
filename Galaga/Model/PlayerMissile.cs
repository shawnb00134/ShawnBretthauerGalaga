using Galaga.View.Sprites;

namespace Galaga.Model
{
    /// <summary>
    /// Player missile class
    /// </summary>
    /// <seealso cref="Galaga.Model.GameObject" />
    public class PlayerMissile : GameObject
    {
        private const int SpeedXDirection = 0;
        private const int SpeedYDirection = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerMissile"/> class.
        /// </summary>
        public PlayerMissile()
        {
            Sprite = new PlayerMissileSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
    }
}
