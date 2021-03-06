﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface;
using PostSharp.Patterns.Model;

namespace Glass.Design.WinRT
{
    [NotifyPropertyChanged]
    public sealed class CanvasItemControl : GridViewItem, ICanvasItem
    {
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register("Top", typeof (double), typeof (CanvasItemControl),
                new PropertyMetadata(double.NaN, OnTopChanged));

        static CanvasItemControl()
        {
            
        }

        public CanvasItemControl()
        {
            DefaultStyleKey = typeof(CanvasItemControl);

            SizeChanged += OnSizeChanged;
        }


        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height != e.PreviousSize.Height)
            {
                this.OnPropertyChanged("Width");
            }

            if (e.NewSize.Width != e.PreviousSize.Width)
            {
                this.OnPropertyChanged("Height");
            }
        }

        [IgnoreAutoChangeNotification]
        public double Top
        {
            get { return (double) GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

   
        public double Right
        {
            get { return Left + Width; }
        }

        public double Bottom
        {
            get { return Top + Height; }
        }

        public ICanvasItemContainer Parent { get; private set; }

        public CanvasItemCollection Children { get; set; }     

        private static void OnTopChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CanvasItemControl target = (CanvasItemControl) d;
            Canvas.SetTop(target, target.Top);
            target.OnPropertyChanged("Top");
        }

        #region Left

        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof (double), typeof (CanvasItemControl),
                new PropertyMetadata(double.NaN, OnLeftChanged));

        [IgnoreAutoChangeNotification]
        public double Left
        {
            get { return (double) GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        public event InputDeviceEventHandler PreviewMouseLeftButtonDown;

        private static void OnLeftChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CanvasItemControl target = (CanvasItemControl) d;
            Canvas.SetLeft(target, target.Left);
            target.OnPropertyChanged("Left");
        }


        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public double GetCoordinate(CoordinatePart part)
        {
            switch (part)
            {
                case CoordinatePart.None:
                    return double.NaN;
                case CoordinatePart.Left:
                    return this.Left;
                case CoordinatePart.Right:
                    return this.Right;
                case CoordinatePart.Top:
                    return this.Top;
                case CoordinatePart.Bottom:
                    return this.Bottom;
                case CoordinatePart.Width:
                    return this.Width;
                case CoordinatePart.Height:
                    return this.Height;
                default:
                    throw new ArgumentOutOfRangeException("part");
            }
        }

        public void SetCoordinate(CoordinatePart part, double value)
        {

            switch (part)
            {
                case CoordinatePart.None:
                    break;
                case CoordinatePart.Left:
                    this.Left = value;
                    break;
                case CoordinatePart.Top:
                    this.Top = value;
                    break;
                case CoordinatePart.Width:
                    this.Width = value;
                    break;
                case CoordinatePart.Height:
                    this.Height = value;
                    break;
                case CoordinatePart.Bottom:
                case CoordinatePart.Right:
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException("part");
            }
        }

        string INamed.GetName()
        {
            return this.GetType().Name;
        }
    }
}