//#define OLD_CONSTRUCTOR
using System;
using System.Linq;
using Prism;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Unity;
using Sweeper.Infrastructure;
using Sweeper.Models;

namespace Sweeper.ViewModels
{
    public class GamePageViewModel : AppMapViewModelBase, IActiveAware
    {
#pragma warning disable 67
        public event EventHandler IsActiveChanged;
#pragma warning restore 67

        public bool IsActive { get; set; }
       

        private IBoardModel _board;
        public IBoardModel Board { get => _board; 
                                   set => SetProperty(ref _board, value); 
                                 }
       

        private ISettingsModel _settings;
        public ISettingsModel Settings { get => _settings; 
                                    set => SetProperty(ref _settings, value); 

                                  }
        private IGameModel _game;
        public IGameModel Game
        {
            get => _game;
            set => SetProperty(ref _game, value);
        }
       

        private int pieceSeparator = 2;
        public int PieceSeparator { get => pieceSeparator; set => SetProperty(ref pieceSeparator, value); }

#if OLD_CONSTRUCTOR
        public GamePageViewModel(INavigationService navigationService, 
                                 ISettingsModel          settingsModel, 
                                 IBoardModel        board,
                                 IGameModel         game) : 
                                 base(navigationService)
        {    
            Settings = settingsModel;
            Board = board;
            Game = game;
        }
#else
        public GamePageViewModel(INavigationService navigationService,
                                 ISettingsModel settingsModel,
                                 ISweeperGameModel sweeperGameModel) :
                                 base(navigationService)
        {
            Settings = settingsModel;
            Board = sweeperGameModel.Board;
            Game = sweeperGameModel.Game;
        }
#endif

    }
}
