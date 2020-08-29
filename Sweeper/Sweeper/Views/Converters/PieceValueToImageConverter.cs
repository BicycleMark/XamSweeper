using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Sweeper.Models;
using Sweeper.Infrastructure;

namespace Sweeper.Views.Converters
{
    public class PieceValueToImageConverter : IValueConverter
    {
        private static Dictionary<PieceValues, ImageSource> imageDictionary;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return imageDictionary[(PieceValues)value] as ImageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        static PieceValueToImageConverter()
        {
            imageDictionary = new Dictionary<PieceValues, ImageSource>
            {
                /*NOMINE,
                ONEMINE,
                TWOMINE,
                THREEMINE,
                FOURMINE,
                FIVEMINE,
                SIXMINE,
                SEVENMINE,
                EIGHTMINE,
                WRONGCHOICE,
                MINE,

                // Following Values indicate the Item is not yet played

                BLANK,
                BUTTON,
                PRESSED,
                FLAGGED
                */

                { PieceValues.NOMINE, null },
                { PieceValues.ONEMINE, ImageSource.FromResource("Sweeper.Resources.One_TP.png") },
                { PieceValues.TWOMINE, ImageSource.FromResource("Sweeper.Resources.Two_TP.png") },
                { PieceValues.THREEMINE, ImageSource.FromResource("Sweeper.Resources.Three_TP.png") },
                { PieceValues.FOURMINE, ImageSource.FromResource("Sweeper.Resources.Four_TP.png") },
                { PieceValues.FIVEMINE, ImageSource.FromResource("Sweeper.Resources.Five_TP.png") },
                { PieceValues.SIXMINE, ImageSource.FromResource("Sweeper.Resources.Six_TP.png") },
                { PieceValues.SEVENMINE, ImageSource.FromResource("Sweeper.Resources.Seven_TP.png") },
                { PieceValues.EIGHTMINE, ImageSource.FromResource("Sweeper.Resources.Eight_TP.png") },
                { PieceValues.WRONGCHOICE, ImageSource.FromResource("Sweeper.Resources.WrongChoice_TP.png") },
                { PieceValues.MINE, ImageSource.FromResource("Sweeper.Resources.mine.png") },

                { PieceValues.BLANK, null  },
                { PieceValues.BUTTON, ImageSource.FromResource("Sweeper.Resources.button.png") },
                { PieceValues.PRESSED, ImageSource.FromResource("Sweeper.Resources.Pressed.png") },
                { PieceValues.FLAGGED, ImageSource.FromResource("Sweeper.Resources.Flagged.png") }
            };
        }
    }
}
