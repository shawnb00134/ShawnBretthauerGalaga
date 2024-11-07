using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Galaga.View;

namespace Galaga.Model
{
    /// <summary>
    ///     Manages the Galaga game play.
    /// </summary>
    public class GameManager
    {
        #region Data members

        private const int PlayerSpeedBoundary = 3;
        private const int TickTimer = 50;
        private const int TickCounterReset = 40;
        private const double PlayerOffsetFromBottom = 30;

        private readonly Canvas canvas;
        private readonly double canvasHeight;
        private readonly double canvasWidth;
        private readonly GameCanvas gameCanvas;
        private readonly MissileManager missileManager;

        private readonly DispatcherTimer timer;
        private int tickCounter;

        private Player player;
        private int score;

        private List<EnemyShip> enemyShips;
        private readonly List<GameObject> listOfShips;
        private readonly List<GameObject> missiles;
        private readonly Physics physics;
        private readonly EnemyManager enemyManager;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        /// </summary>
        public GameManager(Canvas canvas, GameCanvas gameCanvas)
        {
            this.canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
            this.gameCanvas = gameCanvas ?? throw new ArgumentNullException(nameof(gameCanvas));

            this.canvas = canvas;
            this.canvasHeight = canvas.Height;
            this.canvasWidth = canvas.Width;

            this.missileManager = new MissileManager();

            this.enemyShips = new List<EnemyShip>();
            this.listOfShips = new List<GameObject>();
            this.missiles = new List<GameObject>();
            this.enemyManager = new EnemyManager(this.canvas);

            this.initializeGame();

            this.tickCounter = 0;
            this.score = 0;
            this.physics = new Physics();

            this.timer = new DispatcherTimer();
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, TickTimer);
            this.timer.Tick += this.timer_Tick;
            this.timer.Start();
            this.updateScore(this.score);
        }

        #endregion

        #region Methods

        private void timer_Tick(object sender, object e)
        {
            this.enemyFireMissiles();

            this.enemyManager.MoveEnemyShips(this.enemyShips, this.tickCounter);
            this.moveMissiles();
            this.tickCounter++;

            if (this.tickCounter >= TickCounterReset)
            {
                this.tickCounter = 0;
            }

            this.missileManager.UpdateDelayTick();
            this.enemyManager.swapSpritesAnimation(this.enemyShips);
            this.checkForMissileOutOfBounds();
            this.checkForCollisions();
        }

        private void initializeGame()
        {
            this.createAndPlacePlayer();
            this.createEnemyShips();
        }

        private void createAndPlacePlayer()
        {
            this.player = new Player();
            this.canvas.Children.Add(this.player.Sprite);
            this.listOfShips.Add(this.player);
            this.updatePlayerLives();

            this.placePlayerNearBottomOfBackgroundCentered();
        }

        private void createEnemyShips()
        {
            this.enemyShips = this.enemyManager.CreateAndPlaceEnemyShip();

            foreach (var enemyShip in this.enemyShips)
            {
                this.listOfShips.Add(enemyShip);
            }
        }

        private void placePlayerNearBottomOfBackgroundCentered()
        {
            this.player.X = this.canvasWidth / 2 - this.player.Width / 2.0;
            this.player.Y = this.canvasHeight - this.player.Height - PlayerOffsetFromBottom;
        }

        /// <summary>
        ///     Moves the player left.
        /// </summary>
        public void MovePlayerLeft()
        {
            if (this.player.X <= PlayerSpeedBoundary)
            {
                this.player.X = PlayerSpeedBoundary;
            }

            this.player.MoveLeft();
        }

        /// <summary>
        ///     Moves the player right.
        /// </summary>
        public void MovePlayerRight()
        {
            if (this.player.X >= this.canvasWidth - this.player.Width - PlayerSpeedBoundary)
            {
                this.player.X = this.canvasWidth - this.player.Width - PlayerSpeedBoundary;
            }

            this.player.MoveRight();
        }

        /// <summary>
        ///     Fires the missile.
        /// </summary>
        public void FireMissile()
        {
            this.missiles.Add(this.missileManager.FireMissile(this.player, this.canvas));
        }

        private void moveMissiles()
        {
            this.missileManager.MoveMissiles(this.missiles);
        }

        private void enemyFireMissiles()
        {
            this.missiles.Add(this.missileManager.FireEnemyMissiles(this.enemyShips, this.canvas));
        }

        private void checkForCollisions()
        {
            var objectsToRemove = this.physics.CheckCollisions(this.listOfShips, this.missiles);
            this.removeObjectsFromCanvas(objectsToRemove);
        }

        private void checkForMissileOutOfBounds()
        {
            var objectsToRemove = new List<GameObject>();

            foreach (var missile in this.missiles)
            {
                if (this.physics.CheckMissileBoundary(missile, this.canvas))
                {
                    objectsToRemove.Add(missile);
                }
            }

            this.removeObjectsFromCanvas(objectsToRemove);
        }

        private void removeObjectsFromCanvas(List<GameObject> objectsToRemove)
        {
            foreach (var obj in objectsToRemove)
            {
                switch (obj)
                {
                    case Player _:
                        //this.listOfShips.Remove(obj);
                        this.checkPlayerLives(obj);
                        break;
                    case EnemyShip enemyShip:
                        this.updateScore(enemyShip.ScoreValue);
                        this.enemyShips.Remove(enemyShip);
                        this.listOfShips.Remove(enemyShip);
                        break;
                    case EnemyMissile _:
                        this.missiles.Remove(obj);
                        break;
                    case PlayerMissile _:
                        this.missiles.Remove(obj);
                        this.missileManager.DecrementPlayerMissileCount();
                        break;
                }

                this.canvas.Children.Remove(obj.Sprite);
                this.checkForEndGame();
            }
        }

        private void checkPlayerLives(GameObject playerObject)
        {
            this.player.removePlayerLife();
            this.updatePlayerLives();
            if (this.player.PlayerLives > 0)
            {
                //this.listOfShips.Remove(playerObject);
                //this.canvas.Children.Remove(playerObject.Sprite);
                this.placePlayerNearBottomOfBackgroundCentered();
                //this.createAndPlacePlayer();
            }
            else
            {
                this.listOfShips.Remove(playerObject);
                this.canvas.Children.Remove(playerObject.Sprite);
                this.checkForEndGame();
            }
        }

        private void updateScore(int scoreValue)
        {
            this.score += scoreValue;
            this.gameCanvas.updateScoreBoard("Score: " + this.score);
        }

        private void updatePlayerLives()
        {
            this.gameCanvas.updatePlayerLivesBoard("Lives: " + this.player.PlayerLives);
        }

        private void checkForEndGame()
        {
            if (!this.listOfShips.Contains(this.player))
            {
                this.timer.Stop();
                this.gameCanvas.DisplayYouLoseText();
            }

            if (!this.enemyShips.Any())
            {
                this.timer.Stop();
                this.gameCanvas.DisplayYouWinText();
            }
        }

        #endregion
    }
}