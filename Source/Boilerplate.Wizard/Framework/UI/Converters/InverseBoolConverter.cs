namespace Framework.UI.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public sealed class InverseBoolConverter : IValueConverter
    {
        #region Public Methods
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolean = (bool)value;

            if (boolean)
            {
                return false;
            }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolean = (bool)value;

            if (boolean)
            {
                return true;
            }

            return false;
        } 

        #endregion
    }
}
