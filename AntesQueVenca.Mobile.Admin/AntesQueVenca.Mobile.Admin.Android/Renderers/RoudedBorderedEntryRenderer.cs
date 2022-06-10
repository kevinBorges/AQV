﻿using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using AntesQueVenca.Mobile.Admin.CustomControls;
using AntesQueVenca.Mobile.Admin.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoudedBorderedCustomEntry), typeof(RoudedBorderedEntryRenderer))]
namespace AntesQueVenca.Mobile.Admin.Droid.Renderers
{
    public class RoudedBorderedEntryRenderer : Xamarin.Forms.Platform.Android.EntryRenderer
    {
        public RoudedBorderedEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var roundedBorderCustomEntry = (RoudedBorderedCustomEntry)Element;
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