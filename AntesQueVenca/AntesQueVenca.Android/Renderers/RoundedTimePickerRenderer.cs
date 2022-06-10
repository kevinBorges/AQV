using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using AntesQueVenca.CustomControls;
using AntesQueVenca.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedCustomTimePicker), typeof(RoundedTimePickerRenderer))]
namespace AntesQueVenca.Droid.Renderers
{
    public class RoundedTimePickerRenderer : TimePickerRenderer
    {
        public RoundedTimePickerRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var roundedBorderCustomEntry = (RoundedCustomTimePicker)Element;
                if (roundedBorderCustomEntry.IsCurvedCornersEnabled == true)
                {
                    var gradientDrawable = new GradientDrawable();
                    gradientDrawable.SetShape(ShapeType.Rectangle);
                    gradientDrawable.SetColor(roundedBorderCustomEntry.BackgroundColor.ToAndroid());
                    gradientDrawable.SetStroke(roundedBorderCustomEntry.BorderWidth, roundedBorderCustomEntry.BorderColor.ToAndroid());
                    gradientDrawable.SetCornerRadius(DpToPixels(this.Context, Convert.ToSingle(roundedBorderCustomEntry.CornerRadius)));
                    Control.SetBackground(gradientDrawable);
                }

                Control.SetPadding((int)DpToPixels(this.Context, Convert.ToSingle(20)), (int)DpToPixels(this.Context, Convert.ToSingle(Control.TotalPaddingTop / 2)), (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingBottom);
            }
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}