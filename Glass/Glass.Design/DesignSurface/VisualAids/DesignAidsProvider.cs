﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Design.Interfaces;
using Glass.Basics.Presentation;
using Glass.Design.CanvasItem;

namespace Glass.Design.DesignSurface.VisualAids
{
    internal class DesignAidsProvider
    {

        public DesignAidsProvider(FrameworkElement visual)
        {
            Visual = visual;
            Visual.Loaded += VisualOnLoaded;
     

            SelectionAdorners = new Dictionary<ICanvasItem, SelectionAdorner>();
            DesignOperation = DesignOperation.Resize;
            DragOperationHost = new DragOperationHost(Visual);
        }

        public DragOperationHost DragOperationHost { get; set; }


       private CanvasItemGroup groupedItems;

        private CanvasItemGroup GroupedItems
        {
            get { return groupedItems; }
            set
            {
                if (groupedItems != null)
                {
                    AdornerLayer.Remove(ResizingAdorner);
                    AdornerLayer.Remove(MovingAdorner);
                }

                groupedItems = value;

                if (groupedItems != null)
                {
                    var movingControl = new MovingControl();
                    
                    DragOperationHost.SetDragTarget(movingControl, GroupedItems);

                    MovingAdorner = new WrappingAdorner(Visual, movingControl, GroupedItems);
                    ResizingAdorner = new WrappingAdorner(Visual, new SizingControl { CanvasItem = GroupedItems }, GroupedItems);
                    AdornerLayer.Add(ResizingAdorner);
                    AdornerLayer.Add(MovingAdorner);
                }
            }
        }


        private Dictionary<ICanvasItem, SelectionAdorner> SelectionAdorners { get; set; }

        private AdornerLayer AdornerLayer { get; set; }

        private FrameworkElement Visual
        {
            get;
            set;
        }

        private Adorner ResizingAdorner { get; set; }
        private Adorner MovingAdorner { get; set; }

        private void VisualOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            AdornerLayer = AdornerLayer.GetAdornerLayer(Visual);
        }

        public void AddItemToSelection(ICanvasItem item)
        {
            AddSelectionAdorner(item);
            UpdateGroup();
        }

        public void RemoveItemFromSelection(ICanvasItem item)
        {
            RemoveSelectionAdorner(item);
            UpdateGroup();
        }

        private void AddSelectionAdorner(ICanvasItem container)
        {
            var selectionAdorner = new SelectionAdorner(Visual, container) { IsHitTestVisible = false };
            AdornerLayer.Add(selectionAdorner);
            SelectionAdorners.Add(container, selectionAdorner);
        }

        private void UpdateGroup()
        {
            var items = SelectionAdorners.Keys.ToList();
            if (items.Any())
            {
                GroupedItems = new CanvasItemGroup(items);
            }
            else
            {
                GroupedItems = null;
            }
        }

        private void RemoveSelectionAdorner(ICanvasItem container)
        {
            var adorner = SelectionAdorners[container];
            SelectionAdorners.Remove(container);
            AdornerLayer.Remove(adorner);
        }

        public DesignOperation DesignOperation { get; set; }
    }
}