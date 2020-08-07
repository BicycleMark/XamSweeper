using Sweeper.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Sweeper.Models
{
    public class BoardModel : BaseModel
    {
        private IPropertyRepository propRepo;
        private ISettings boardSettings;
        private bool loadedFromRepo;
        
        public ObservableCollection<GamePieceModel> Model { get; private set; }

        public int Rows
        {
            get { return boardSettings.Rows; }
            set { boardSettings.Rows = value; }
        }
        public int Columns
        {
            get { return boardSettings.Columns; }
            set { boardSettings.Columns = value; }
        }

        public BoardModel(IPropertyRepository repo, ISettings settings, bool loadFromRepo ) : base(repo)
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
                Model = new ObservableCollection<GamePieceModel>();
                var max = boardSettings.Rows * boardSettings.Columns;
                for (int r = 0; r < settings.Rows; r++  )
                {
                    for (int c = 0; c < settings.Rows; c++)
                    {   // At This point the GamePiece.ShownValue is Button
                        // The reason we do not set the mines now is we wait for the caller to 
                        // set them after the first played item is played. Don't want the user to 
                        // lose on first play
                        Model.Add(new GamePieceModel(r, c));
                    }
                }
            }       
        }

        public bool Play(GridPoint gp)
        {
            /////////////////////////////////////// LOCAL ////////////////////////////////////////////////////////////
            void setNeighborCounts()
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
            /////////////////////////////////////// LOCAL ////////////////////////////////////////////////////////////
            void placeMines(GridPoint ep)
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
            ///////////////////////// Main function ////////////////////////////////////////////////////////////
            {
                // Exclude Out of Bounds points
                if (!inBounds(gp))
                {
                    throw new InvalidEnumArgumentException(Resources.Sweeper.ExceptionExcludePointIsOutOfBounds);
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
                                    PlayBlankNeighbors(gp);
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
                        if (piece.ItemValue == GamePieceModel.PieceValues.NOMINE)
                        {
                            piece.ShownValue = GamePieceModel.PieceValues.BLANK;
                        }
                        else
                        {
                            piece.ShownValue = piece.ItemValue;
                        } 
                       
                    }
                }
                return didLose;
            }
        }

        bool inBounds(GridPoint point)
        {
            return (point.R > 0 && point.R < Rows &&
                    point.C > 0 && point.C < Columns);

        }
        private void PlayBlankNeighbors(GridPoint gp)
        {
            if (!inBounds(gp)
                || this[gp.R, gp.C].ItemValue != GamePieceModel.PieceValues.NOMINE
                || this[gp.R, gp.C].IsPlayed)
            {
                return;
            }
            this[gp.R, gp.C].ShownValue = GamePieceModel.PieceValues.BLANK;
            PlayBlankNeighbors(new GridPoint(gp.R + 1, gp.C));
            PlayBlankNeighbors(new GridPoint(gp.R - 1, gp.C));
            PlayBlankNeighbors(new GridPoint(gp.R, gp.C + 1));
            PlayBlankNeighbors(new GridPoint(gp.R, gp.C - 1));             
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
    }
}
