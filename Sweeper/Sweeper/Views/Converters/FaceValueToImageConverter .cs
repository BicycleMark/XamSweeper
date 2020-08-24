using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Sweeper.Models;
using Sweeper.Infrastructure;

namespace Sweeper.Views.Converters
{
    public class FaceValueToImageConverter : IValueConverter
    {
        private static Dictionary<GameStates, ImageSource> imageDictionary;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Resources.Sweeper.Button;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        static FaceValueToImageConverter()
        {
            imageDictionary = new Dictionary<GameStates, ImageSource>
            {
                
                { GameStates.NOT_STARTED, null },
                { GameStates.IN_PLAY, ImageSource.FromResource("Sweeper.Resources.One_TP.png") } ,
                { GameStates.WON, ImageSource.FromResource("Sweeper.Resources.Two_TP.png") } ,
                { GameStates.LOST, ImageSource.FromResource("Sweeper.Resources.Three_TP.png") }
            };
        }
    }
}
