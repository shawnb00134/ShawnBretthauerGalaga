﻿using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Galaga.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Galaga.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameCanvas
    {
        #region Data members

        private readonly GameManager gameManager;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameCanvas" /> class.
        /// </summary>
        public GameCanvas()
        {
            this.InitializeComponent();

            Width = this.canvas.Width;
            Height = this.canvas.Height;
            ApplicationView.PreferredLaunchViewSize = new Size { Width = Width, Height = Height };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(Width, Height));

            Window.Current.CoreWindow.KeyDown += this.coreWindowOnKeyDown;

            this.gameManager = new GameManager(this.canvas, this);
        }

        #endregion

        #region Methods

        private void coreWindowOnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.gameManager.MovePlayerLeft();
                    break;
                case VirtualKey.Right:
                    this.gameManager.MovePlayerRight();
                    break;
                case VirtualKey.Space:
                    this.gameManager.FireMissile();
                    break;
            }
        }

        /// <summary>
        ///     Displays the "You Win" text.
        /// </summary>
        public void DisplayYouWinText()
        {
            this.gameOverYouLWin.Visibility = Visibility.Visible;
        }

        /// <summary>
        ///     Displays the "You Lose" text.
        /// </summary>
        public void DisplayYouLoseText()
        {
            this.gameOverYouLose.Visibility = Visibility.Visible;
        }

        /// <summary>
        ///     Updates the score board.
        /// </summary>
        /// <param name="scoreText">The score text.</param>
        public void updateScoreBoard(string scoreText)
        {
            this.scoreBoard.Text = scoreText;
        }

        #endregion
    }
}