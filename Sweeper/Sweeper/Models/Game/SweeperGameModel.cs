using Sweeper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using Timer = System.Timers.Timer;

namespace Sweeper.Models.Game
{
    public class SweeperGameModel : BaseModel, IBoardModel, IGameModel, ISweeperGameModel
    {
        private Timer _timer;
        private bool loadedFromRepo;
        public ObservableCollection<GamePieceModel> Model { get; private set; }

        private IPropertyRepository _repo;
        public IPropertyRepository Repo { get => _repo; set => _repo = value; }

        private int _rows;
        public int Rows
        {
            get { return _rows; }
            private set { SetProperty(ref _rows, value); }
        }

        private int _columns;
        public int Columns
        {
            get { return _columns; }
            private set { SetProperty(ref _columns, value); }
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!Settings.DisableTimerUpdatesForTesting &&
                _gameState == GameStates.IN_PLAY)
            {
                GameTime += 1;
            }
        }

        public int Mines
        {
            get
            {
                return Model.Count(m => m.ItemValue == GamePieceModel.PieceValues.MINE);
            }
        }

        public int MineCount
        {
            get
            {
                return Mines;
            }
        }

        IBoardModel _board;
        public IBoardModel Board
        {
            get { return _board; }
            private set { _board = value; }
        }

        public List<List<GamePieceModel>> RowItems
        {
            get { List<List<GamePieceModel>> lst = new List<List<GamePieceModel>>();
                  for(int r= 0; r < Rows; r++ )
                  {
                    List<GamePieceModel> row = new List<GamePieceModel>();
                    for (int c = 0; c < Columns; c++)
                    {
                        row.Add(this[r, c]);
                    }
                    lst.Add(row);
                  }
                  return lst;
                }  
        }

        IGameModel _game;
        public IGameModel Game
        {
            get { return _game; }
            private set { _game = value; }
        }

        private ISettingsModel _settings;
        public ISettingsModel Settings { get => _settings; set => _settings = value; }

        private GameStates _gameState = GameStates.NOT_STARTED;
        public GameStates GameState
        {
            get { return _gameState; }
            set { SetProperty(ref _gameState, value, OnGameStateChanged); }
        }

        private int _gameTime;
        public int GameTime
        {
            get { return _gameTime; }
            set
            {
                SetProperty(ref _gameTime, value);
            }
        }

        public int RemainingMines
        {
            get { return Mines - Model.Count(m => m.ShownValue == GamePieceModel.PieceValues.FLAGGED); }
        }

        public bool AllCorrectlyFlagged
        {
            get
            {
                {
                    int nMines = Mines;
                    int nMinesFlaggedCorrectly =
                        Model.Count(m => m.IsFlagged &&
                                         m.ItemValue == GamePieceModel.PieceValues.MINE);
                    return (nMinesFlaggedCorrectly == nMines);
                }
            }
        }

        public SweeperGameModel(IPropertyRepository repo, ISettingsModel settings, bool loadFromRepo) : base(repo)
        {
            Repo = repo;
            Settings = settings;
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += _timer_Elapsed;
            loadedFromRepo = loadFromRepo;
            Board = this;
            Game = this;
            Rows = settings.Rows;
            Columns = settings.Columns;
            if (loadedFromRepo)
            {
                throw new NotImplementedException("TODO Add Load From Repo F(x)");
               // object obj = repo.LoadProperty(nameof(Model));
            }
            else
            {
                // Create a new Empty Board with all items shown with BUTTONS
                InitializeBoard();
            }
        }

        public GameStates Play(int r, int c)
        {
            if (GameState == GameStates.IN_PLAY || GameState == GameStates.NOT_STARTED)
            {
                if (Board.Play(new GridPoint(r, c)))
                {
                    GameState = EvaluateGameState();
                }
                else
                {
                    // You hit a mine

                    GameState = GameStates.LOST;
                }
            }
            else
            {
                throw new InvalidOperationException(
                    Resources.Sweeper.InvalidGamePlayOperationMustBeInGameStateNOT_STARTED_OR_INPLAY);
            }
            return GameState;
        }

        public GameStates ToggleFlag(int r, int c)
        {
            var retVal = GameStates.IN_PLAY;
            if (GameState == GameStates.IN_PLAY)
            {
                Board.ToggleFlag(new GridPoint(r, c));
                retVal = EvaluateGameState();
                GameState = retVal;
            }
            RaisePropertyChanged(nameof(RemainingMines));
            return retVal;
        }

        public GamePieceModel.PieceValues ToggleFlag(GridPoint gp)
        {
            this[gp.R, gp.C].ToggleFlag();
            return this[gp.R, gp.C].ShownValue;
        }

        private GameStates EvaluateGameState()
        {
            var retVal = GameState;

            if (GameState == GameStates.NOT_STARTED)
            {
                retVal = GameStates.IN_PLAY;
            }
            else
            {
                if (GameState == GameStates.IN_PLAY)
                {
                    if (GameTime >= 999)
                    {
                        retVal = GameStates.LOST;
                    }
                    else
                    {
                        if (Board.AllCorrectlyFlagged)
                        {
                            retVal = GameStates.WON;
                        }
                    }
                }
            }
            return retVal;
        }

        public void Resize(ISettingsModel settings)
        {
            _settings = settings;
            InitializeBoard();
        }

       
        public GamePieceModel this[int r, int c]
        {
            get
            {
                return Model.FirstOrDefault(p => p.GridPoint.R == r &&
                                                 p.GridPoint.C == c);
            }
            set
            {
                var item = Model.FirstOrDefault(p => p.GridPoint.R == r &&
                                                     p.GridPoint.C == c);
                item = value;
            }
        }

        public bool Play(GridPoint gp)
        {
            ///////////////////////// Main function ////////////////////////////////////////////////////////////
            {
                // Exclude Out of Bounds points
                if (!inBounds(gp.R, gp.C))
                {
                    throw new ArgumentOutOfRangeException(Resources.Sweeper.ExceptionExcludePointIsOutOfBounds);
                }
                // Do not allow additional plays if a mine has already been selected
                if (Model.Count(m => m.ShownValue == GamePieceModel.PieceValues.WRONGCHOICE) > 0)
                {
                    throw new InvalidOperationException(Resources.Sweeper.InvalidBoardOperationException);
                }
                var didLose = false;
                var piece = this[gp.R, gp.C];
                if (!piece.IsFlagged &&
                     piece.ShownValue == GamePieceModel.PieceValues.BUTTON)
                {
                    // If it was loaded from a repo Mine Count will be > 0
                    if (Model.Count(p => p.IsPlayed) != 0)
                    {
                        switch (piece.ItemValue)
                        {
                            // You Lost
                            case (GamePieceModel.PieceValues.MINE):
                                {
                                    piece.ShownValue = GamePieceModel.PieceValues.WRONGCHOICE;
                                    didLose = true;
                                    GameState = GameStates.LOST;
                                    break;
                                }
                            // Cool Several Tiles will be turned (all Contiguous Blanks)
                            case (GamePieceModel.PieceValues.NOMINE):
                                {
                                    PlayBlankNeighbors(gp.R, gp.C);
                                    break;
                                }
                            // A single Tile  
                            default:
                                {
                                    piece.ShownValue = piece.ItemValue;
                                    break;
                                }
                        }
                    }
                    else // So we need to Initialize the Board and Play the Point user selected 

                    {
                        placeMines(gp);
                        setNeighborCounts();
                        if (this[gp.R, gp.C].ItemValue == GamePieceModel.PieceValues.NOMINE)
                        {
                            PlayBlankNeighbors(gp.R, gp.C);
                        }
                        else
                        {
                            piece.ShownValue = piece.ItemValue;
                        }
                        GameState = GameStates.IN_PLAY;
                    }
                }
                return !didLose;
            }
        }

        private void InitializeBoard()
        {
            if (Model != null)
            {
                Model.Clear();
            }
            else
            {
                Model = new ObservableCollection<GamePieceModel>();
            }
            Rows = _settings.Rows;
            Columns = _settings.Columns;
            for (int r = 0; r < _settings.Rows; r++)
            {
               
                for (int c = 0; c < _settings.Columns; c++)
                {
                    Model.Add(new GamePieceModel(r, c));
                }
            }
        }

        public bool Disposed
        {
            get { return _disposed; }
        }

        //public IBoardModel Board { get => this; }

        bool _disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _timer.Stop();
                _timer.Elapsed -= _timer_Elapsed;
                _timer.Dispose();
            }
            _disposed = true;
        }

        /////////////////////////////////////// LOCAL ////////////////////////////////////////////////////////////
        private void setNeighborCounts()
        {
            var mines = from gpm in Model where gpm.ItemValue == GamePieceModel.PieceValues.MINE select gpm;
            foreach (GamePieceModel p in mines)
            {
                var mincol = p.GridPoint.C - 1; //it does not matter if it is < 0 the Query wont return any values
                var maxcol = p.GridPoint.C + 1; //it does not matter if it is > Borad.Columns the Query wont return any values
                var minrow = p.GridPoint.R - 1; //it does not matter if it is < 0 the Query wont return any values
                var maxrow = p.GridPoint.R + 1; //it does not matter if it is > Borad.Rows the Query wont return any value
                var neighbors = Model.Where(evalPiece => evalPiece.GridPoint.C >= mincol &&
                                                         evalPiece.GridPoint.C <= mincol &&
                                                         evalPiece.GridPoint.R >= minrow &&
                                                         evalPiece.GridPoint.R <= maxrow &&
                                                         evalPiece.ItemValue != GamePieceModel.PieceValues.MINE);
                foreach (var n in neighbors)
                {
                    var val = (int)n.ItemValue + 1;
                    n.ItemValue = (GamePieceModel.PieceValues)val;
                }
            }
        }

        private void placeMines(GridPoint ep)
        {
            int max = Model.Count;
            Random random = new Random();
            while (_settings.MineCount >
                   Model.Count(p => p.ItemValue == GamePieceModel.PieceValues.MINE))
            {
                int proposedIndex = random.Next(max);
                var propsedGridPoint = Model[proposedIndex].GridPoint;
                if (Model[proposedIndex].ItemValue != GamePieceModel.PieceValues.MINE &&
                    // Don't put mine under the first played item
                    !(propsedGridPoint.R == ep.R && propsedGridPoint.C == ep.C))
                {
                    Model[proposedIndex].ItemValue = GamePieceModel.PieceValues.MINE;
                }
            }
        }

        private bool inBounds(int r, int c)
        {
            return (r >= 0 && r < Rows &&
                    c >= 0 && c < Columns);
        }

        private void PlayBlankNeighbors(int r, int c)
        {
            if (!inBounds(r, c)
                || this[r, c].ItemValue != GamePieceModel.PieceValues.NOMINE
                || this[r, c].IsPlayed)
            {
                return;
            }
            //Set To Fill Value
            this[r, c].ShownValue = GamePieceModel.PieceValues.BLANK;
            PlayBlankNeighbors(r + 1, c);
            PlayBlankNeighbors(r - 1, c);
            PlayBlankNeighbors(r, c + 1);
            PlayBlankNeighbors(r, c - 1);
        }
        ////GameModel privates
        private void OnGameStateChanged()
        {
            switch (_gameState)
            {
                case GameStates.IN_PLAY:
                    _timer.Enabled = true;
                    GameTime = Settings.DisableTimerUpdatesForTesting ? GameTime : 1;
                    break;
                case GameStates.LOST:
                case GameStates.WON:
                    _timer.Enabled = false;
                    break;
            }
        }
    }
}
