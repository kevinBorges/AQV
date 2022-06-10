using Xamarin.Forms;

namespace AntesQueVenca.CustomControls
{
    public class RoundedCustomTimePicker : TimePicker
    {
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create("BorderColor", typeof(Color), typeof(RoundedCustomTimePicker), Color.FromHex("#FFFFFF"));
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create("BorderWidth", typeof(int), typeof(RoundedCustomTimePicker), 1);
        public int BorderWidth
        {
            get => (int)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create("CornerRadius", typeof(double), typeof(RoundedCustomTimePicker), 6.0);
        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty IsCurvedCornersEnabledProperty = BindableProperty.Create("IsCurvedCornersEnabled", typeof(bool), typeof(RoundedCustomTimePicker), true);
        public bool IsCurvedCornersEnabled
        {
            get => (bool)GetValue(IsCurvedCornersEnabledProperty);
            set => SetValue(IsCurvedCornersEnabledProperty, value);
        }
    }
}
