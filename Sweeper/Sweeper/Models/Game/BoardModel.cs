using Sweeper.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Sweeper.Models.Game
{
    public class BoardModel : BaseModel, IBoardModel
    {
        private IPropertyRepository propRepo;
        private ISettingsModel boardSettings;
        private bool loadedFromRepo;

        public ObservableCollection<GamePieceModel> Model { get; private set; }

        public int Rows
        {
            get { return boardSettings.Rows; }
            //private set { boardSettings.Rows = value; }
        }
        public int Columns
        {
            get { return boardSettings.Columns; }
            //private set { boardSettings.Columns = value; }
        }

        public int Mines
        {
            get { return 12;//return boardSettings.Mine;
            }
            //private set { boardSettings.Columns = value; }
        }

        public BoardModel(IPropertyRepository repo, ISettingsModel settings, bool loadFromRepo) : base(repo)
        {
            propRepo = repo;
            boardSettings = settings;
            loadedFromRepo = loadFromRepo;
            if (loadedFromRepo)
            {
                throw new NotImplementedException("TODO Add Load From Repo F(x)");
                object obj = repo.LoadProperty(nameof(Model));
            }
            else
            {
                // Create a new Empty Board with all items shown with BUTTONS
                InitializeBoard();
            }
        }

        public void Resize(ISettingsModel settings)
        {
            boardSettings = settings;
            InitializeBoard();
        }

        public void Save()
        {
            propRepo.SaveProperty(nameof(Model), Model);
        }

        public GamePieceModel this[int r, int c]
        {
            get
            {
                return Model.FirstOrDefault(p => p.GridPoint.R == r && p.GridPoint.C == c);
            }
            set
            {
                var item = Model.FirstOrDefault(p => p.GridPoint.R == r && p.GridPoint.C == c);
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
            var max = boardSettings.Rows * boardSettings.Columns;
            for (int r = 0; r < boardSettings.Rows; r++)
            {
                for (int c = 0; c < boardSettings.Rows; c++)
                {
                    Model.Add(new GamePieceModel(r, c));
                }
            }
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
            while (boardSettings.MineCount >
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
    }
}
