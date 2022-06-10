using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using AntesQueVenca.CustomControls;
using AntesQueVenca.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedBorderedCustomEditor), typeof(RoundedBorderedEditorRenderer))]
namespace AntesQueVenca.Droid.Renderers
{
    public class RoundedBorderedEditorRenderer : Xamarin.Forms.Platform.Android.EditorRenderer
    {
        public RoundedBorderedEditorRenderer(Context context):base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var roundedBorderCustomEditor = (RoundedBorderedCustomEditor)Element;
                if (roundedBorderCustomEditor.IsCurvedCornersEnabled == true)
                {
                    var gradientDrawable = new GradientDrawable();
                    gradientDrawable.SetShape(ShapeType.Rectangle);
                    gradientDrawable.SetColor(roundedBorderCustomEditor.BackgroundColor.ToAndroid());
                    gradientDrawable.SetStroke(roundedBorderCustomEditor.BorderWidth, roundedBorderCustomEditor.BorderColor.ToAndroid());
                    gradientDrawable.SetCornerRadius(DpToPixels(this.Context, Convert.ToSingle(roundedBorderCustomEditor.CornerRadius)));
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