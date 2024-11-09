﻿using Galaga.View.Sprites;
using Windows.UI.Xaml.Controls;

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

        private readonly Canvas canvas;

        private BaseSprite[] sprites;

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
        public NonFiringEnemy(BaseSprite mainSprite, BaseSprite alternateSprite, Canvas canvas) : base(mainSprite, alternateSprite, canvas)
        {
            Sprite = mainSprite;
            this.canvas = canvas;
            SetSpeed(SpeedXDirection, SpeedYDirection);
            this.sprites = new [] { mainSprite, alternateSprite };
        }

        /// <summary>
        /// Adds the enemy sprites to the canvas and sets it initial visibility.
        /// </summary>
        public override void addEnemyToCanvas()
        {
            this.canvas.Children.Add(this.sprites[0]);
            this.sprites[0].Visibility = Windows.UI.Xaml.Visibility.Visible;

            this.canvas.Children.Add(this.sprites[1]);
            this.sprites[1].Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        #endregion

        /// <summary>
        ///     Swaps the sprites.
        /// </summary>
        public override void SwapSprites()
        {
            if (this.sprites[0].Visibility == Windows.UI.Xaml.Visibility.Visible)
            {
                this.sprites[0].Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.sprites[1].Visibility = Windows.UI.Xaml.Visibility.Visible;
                Sprite = this.sprites[1];
            }
            else
            {
                this.sprites[0].Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.sprites[1].Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Sprite = this.sprites[0];
            }
        }
    }
}