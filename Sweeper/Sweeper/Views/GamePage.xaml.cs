using Sweeper.Infrastructure;
using Sweeper.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Sweeper.Resources;
using System.Reflection;

namespace Sweeper.Views
{
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LayoutItems();   
        }

        private void LayoutItems()
        {
            Grid parent = (Grid)buttons.Parent;
            var bc = parent.BindingContext as GamePageViewModel;
            var itemWidth = (parent.Width - ((bc.Board.Columns - 1) * bc.PieceSeparator)) / bc.Board.Columns;
            var itemHeight = (parent.Height - ((bc.Board.Rows - 1) * bc.PieceSeparator)) / bc.Board.Rows;

            buttons.Spacing = 2;
            for (int i = 0; i < bc.Board.Rows; i++)
            {
                StackLayout row = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 2 };
                for (int j = 0; j < bc.Board.Columns; j++)
                {
                    var btn = new ImageButton() { };
                    btn.BackgroundColor = Color.Blue;
                    btn.HeightRequest = itemHeight;
                    btn.WidthRequest = itemWidth;
                    btn.BindingContext = bc.Board[i, j];
                    btn.Source = ImageSource.FromResource("Sweeper.button.png");
                    btn.Aspect = Aspect.AspectFill;
                    row.Children.Add(btn);
                }
                buttons.Children.Add(row);
            }
        }

        private double width = 0;
        private double height = 0;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
                //reconfigure layout
            }
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
             base.LayoutChildren(x, y, width, height);

        }


    }
}