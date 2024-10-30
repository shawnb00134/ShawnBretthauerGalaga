using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Galaga.Model
{
    /// <summary>
    ///     Manages the Galaga game play.
    /// </summary>
    public class GameManager
    {
        #region Data members

        private const double PlayerOffsetFromBottom = 30;
        private const int PlayerSpeedBoundary = 3;
        private const int TickTimer = 50;
        private const int TickCounterReset = 40;
        private const int PlayerMissileLimit = 1;

        private List<EnemyShip> enemyShips;
        private readonly List<GameObject> listOfShips;
        private readonly List<GameObject> missiles;
        private readonly Canvas canvas;
        private readonly double canvasHeight;
        private readonly double canvasWidth;

        private readonly Random random;
        private readonly DispatcherTimer timer;
        private int tickCounter;
        private int playerMissileCount;

        private Player player;
        private readonly Physics physics;
        private readonly EnemyManager enemyManager;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        /// </summary>
        public GameManager(Canvas canvas)
        {
            this.canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));

            this.canvas = canvas;
            this.canvasHeight = canvas.Height;
            this.canvasWidth = canvas.Width;

            this.enemyShips = new List<EnemyShip>();
            this.listOfShips = new List<GameObject>();
            this.missiles = new List<GameObject>();
            this.enemyManager = new EnemyManager(this.canvas);

            this.initializeGame();

            this.tickCounter = 0;
            this.playerMissileCount = 0;
            this.random = new Random();
            this.physics = new Physics();

            this.timer = new DispatcherTimer();
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, TickTimer);
            this.timer.Tick += this.timer_Tick;
            this.timer.Start();
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
            System.Diagnostics.Debug.WriteLine(this.playerMissileCount + " " + PlayerMissileLimit);
            if (this.playerMissileCount < PlayerMissileLimit)
            {
                this.playerMissileCount++;

                var missile = new PlayerMissile();
                missile.X = this.player.X + this.player.Width / 2.0 - missile.Width / 2.0;
                missile.Y = this.player.Y - missile.Height;
                this.canvas.Children.Add(missile.Sprite);
                this.missiles.Add(missile);
            }
            System.Diagnostics.Debug.WriteLine(this.playerMissileCount + " " + PlayerMissileLimit);
        }

        private void moveMissiles()
        {
            foreach (var missile in this.missiles)
            {
                if (missile is PlayerMissile)
                {
                    missile.MoveUp();
                }
                else
                {
                    missile.MoveDown();
                }
            }
        }

        private void enemyFireMissiles()
        {
            //TODO: Fix the ToList issue
            if (this.random.Next(30) == 0)
            {
                foreach (var enemyShip in this.enemyShips)
                {
                    var eleigibleShips = this.enemyShips.Where(ship => ship is EnemyLevel3).ToList();
                    if (eleigibleShips.Any())
                    {
                        var randomShip = eleigibleShips.ElementAt(this.random.Next(eleigibleShips.Count));
                        var missile = randomShip.FireMissile();
                        this.canvas.Children.Add(missile.Sprite);
                        this.missiles.Add(missile);
                        break;
                    }
                }
            }
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
                if (obj is Player)
                {
                    this.timer.Stop();
                }

                if (obj is EnemyShip)
                {
                    this.enemyShips.Remove((EnemyShip)obj);
                    this.listOfShips.Remove(obj);
                }

                if (obj is EnemyMissile || obj is PlayerMissile)
                {
                    this.missiles.Remove(obj);
                }

                if (obj is PlayerMissile)
                {
                    this.playerMissileCount--;
                }

                this.canvas.Children.Remove(obj.Sprite);
            }
        }

        #endregion
    }
}