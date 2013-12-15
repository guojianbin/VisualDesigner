﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Glass.Design.CanvasItem;
using Glass.Design.DesignSurface;
using Glass.Design.Wpf.Properties;

namespace Glass.Design.Wpf.CanvasItem
{
    internal class CanvasItemGroupWpf : CanvasItemGroup, INotifyPropertyChanged
    {
        public CanvasItemGroupWpf(IEnumerable<ICanvasItem> items)
            : base(items)
        {
            
        }


        protected override void OnWidthChanged(SizeChangeEventArgs e)
        {
            base.OnWidthChanged(e);
            OnPropertyChanged("Width");
        }

        protected override void OnTopChanged(LocationChangedEventArgs e)
        {
            base.OnTopChanged(e);
            OnPropertyChanged("Top");
        }

        protected override void OnHeightChanged(SizeChangeEventArgs e)
        {
            base.OnHeightChanged(e);
            OnPropertyChanged("Height");
        }

        protected override void OnLeftChanged(LocationChangedEventArgs e)
        {
            base.OnLeftChanged(e);
            OnPropertyChanged("Left");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}