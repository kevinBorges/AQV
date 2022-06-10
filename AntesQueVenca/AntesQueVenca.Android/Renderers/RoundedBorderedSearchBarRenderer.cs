using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Widget;
using AntesQueVenca.CustomControls;
using AntesQueVenca.Droid.Renderers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoudedBorderedCustomSearchBar), typeof(RoundedBorderedSearchBarRenderer))]
namespace AntesQueVenca.Droid.Renderers
{
    public class RoundedBorderedSearchBarRenderer : SearchBarRenderer
    {
        public RoundedBorderedSearchBarRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var roundedBorderCustomSearchBar = (RoudedBorderedCustomSearchBar)Element;
                if (roundedBorderCustomSearchBar.IsCurvedCornersEnabled == true)
                {
                    var gradientDrawable = new GradientDrawable();
                    gradientDrawable.SetShape(ShapeType.Rectangle);
                    gradientDrawable.SetColor(roundedBorderCustomSearchBar.BackgroundColor.ToAndroid());
                    gradientDrawable.SetStroke(roundedBorderCustomSearchBar.BorderWidth, roundedBorderCustomSearchBar.BorderColor.ToAndroid());
                    gradientDrawable.SetCornerRadius(DpToPixels(this.Context, Convert.ToSingle(roundedBorderCustomSearchBar.CornerRadius)));

                    SearchView searchView = (Control as SearchView);
                    searchView.Iconified = true;
                    searchView.SetIconifiedByDefault(false);
                    int searchIconId = Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                    var icon = searchView.FindViewById(searchIconId);
                    (icon as ImageView).SetImageResource(Resource.Drawable.searchIcon);

                    Control.SetBackground(gradientDrawable);
                }

                Control.SetPadding(Control.PaddingLeft, (int)DpToPixels(this.Context, Convert.ToSingle(Control.PaddingTop / 2)), (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingBottom);
            }
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}