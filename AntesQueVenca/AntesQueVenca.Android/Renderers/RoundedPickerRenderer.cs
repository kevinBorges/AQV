using System;
using Android.Content;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Android.Util;
using Xamarin.Forms;
using AntesQueVenca.CustomControls;
using AntesQueVenca.Droid.Renderers;

[assembly: ExportRenderer(typeof(RoundedCustomPicker), typeof(RoundedPickerRenderer))]
namespace AntesQueVenca.Droid.Renderers
{
    public class RoundedPickerRenderer : PickerRenderer
    {
        public RoundedPickerRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var roundedBorderCustomEntry = (RoundedCustomPicker)Element;
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