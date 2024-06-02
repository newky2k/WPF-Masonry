using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace MasonryWinUI
{
    public sealed class Tile : Button
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(Tile), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary> 
        /// HorizontalTitleAlignment Dependency Property.
        /// Default Value: HorizontalAlignment.Left
        /// </summary>
        public static readonly DependencyProperty HorizontalTitleAlignmentProperty =
            DependencyProperty.Register(
                nameof(HorizontalTitleAlignment),
                typeof(HorizontalAlignment),
                typeof(Tile),
                new PropertyMetadata(HorizontalAlignment.Left));

        /// <summary> 
        /// Gets/Sets the horizontal alignment of the title.
        /// </summary> 
        [System.ComponentModel.Bindable(true), Category("Layout")]
        public HorizontalAlignment HorizontalTitleAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalTitleAlignmentProperty); }
            set { SetValue(HorizontalTitleAlignmentProperty, value); }
        }

        /// <summary>
        /// VerticalTitleAlignment Dependency Property. 
        /// Default Value: VerticalAlignment.Bottom
        /// </summary> 
        public static readonly DependencyProperty VerticalTitleAlignmentProperty =
            DependencyProperty.Register(
                nameof(VerticalTitleAlignment),
                typeof(VerticalAlignment),
                typeof(Tile),
                new PropertyMetadata(VerticalAlignment.Bottom));

        /// <summary>
        /// Gets/Sets the vertical alignment of the title.
        /// </summary>
        [System.ComponentModel.Bindable(true), Category("Layout")]
        public VerticalAlignment VerticalTitleAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalTitleAlignmentProperty); }
            set { SetValue(VerticalTitleAlignmentProperty, value); }
        }

        public static readonly DependencyProperty CountProperty = DependencyProperty.Register(nameof(Count), typeof(string), typeof(Tile), new PropertyMetadata(default(string)));

        public string Count
        {
            get { return (string)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        public static readonly DependencyProperty TiltFactorProperty = DependencyProperty.Register(nameof(TiltFactor), typeof(int), typeof(Tile), new PropertyMetadata(5));

        public int TiltFactor
        {
            get { return (Int32)GetValue(TiltFactorProperty); }
            set { SetValue(TiltFactorProperty, value); }
        }

        public static readonly DependencyProperty TitleFontSizeProperty = DependencyProperty.Register(nameof(TitleFontSize), typeof(double), typeof(Tile), new PropertyMetadata(12d));

        public double TitleFontSize
        {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }

        public static readonly DependencyProperty CountFontSizeProperty = DependencyProperty.Register(nameof(CountFontSize), typeof(double), typeof(Tile), new PropertyMetadata(28d));

        public double CountFontSize
        {
            get { return (double)GetValue(CountFontSizeProperty); }
            set { SetValue(CountFontSizeProperty, value); }
        }
        public Tile()
        {
            this.DefaultStyleKey = typeof(Tile);
        }
    }
}
