using StudentGradeTracker.Models;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace StudentGradeTracker.Converters
{
    public class GradeToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Grade grade)
            {
                return grade switch
                {
                    Grade.Good => Brushes.Green,
                    Grade.Excellent => Brushes.DarkGreen,
                    Grade.Poor => Brushes.Yellow,
                    Grade.Fail => Brushes.Red,
                    _ => Brushes.Transparent
                };
            }

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}