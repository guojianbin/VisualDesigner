﻿using Glass.Design.Pcl.Canvas;
using Model;
using StyleMVVM.DependencyInjection;
using StyleMVVM.ViewModel;

namespace ComicDesigner
{
    [Export("MainViewModel")]
    public class MainViewModel : BaseViewModel
    {
        private CanvasItemCollection items;

        [ImportConstructor]
        public MainViewModel()
        {
            Items = new CanvasItemCollection();
            for (int i = 0; i < 5; i++)
            {
                Items.Add(new CanvasRectangle());
            }
        }

        public CanvasItemCollection Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }
    }
}