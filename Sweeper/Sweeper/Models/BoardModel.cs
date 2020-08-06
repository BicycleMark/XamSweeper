using Prism.Mvvm;
using Sweeper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

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

        public void Play()
        {

        
        }
        public void Save()
        {
            propRepo.SaveProperty(nameof(Model), Model);
        }

        /// <summary>
        /// Places the mines after FirstClick on Board
        /// </summary>
        /// <param name="excludePoint"> 
        /// The point that was played first
        /// </param>
        private void PlaceMineAndNeighborValues(GridPoint excludePoint)
        {
            if (loadedFromRepo)
            {
                throw new InvalidOperationException(Resources.Resources.ExceptionCannotPlaceMinesFromBoardLoadedFromRepo);
            }
            if (excludePoint.R >= boardSettings.Rows    || 
                excludePoint.C >= boardSettings.Columns     )
            {
                throw new InvalidEnumArgumentException(Resources.Resources.ExceptionExcludePointIsOutOfBounds);
            }
            int max = Model.Count;
            Random random = new Random();
            while (boardSettings.MineCount < 
                   Model.Count(p => p.ItemValue == GamePieceModel.PieceValues.MINE) )
            {
                int proposedIndex = random.Next(max);
                if (Model[proposedIndex].ItemValue != GamePieceModel.PieceValues.MINE)
                {
                    Model[proposedIndex].ItemValue = GamePieceModel.PieceValues.MINE;
                }
            }
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
