﻿using System;
using Glass.Design.Converters;
using Glass.Design.DesignSurface;
using Glass.Design.Wpf.DesignSurface.VisualAids.Resize;

namespace Glass.Design.Wpf
{
    public static class ThumbCursorConverterFactory
    {
        public static IThumbCursorConverter Create(IResizeThumb resizeThumb)
        {
            if (resizeThumb is ResizeThumb)
            {
                return new WindowsSizeCursorsThumbCursorConverter();
            }

            throw new ArgumentException("There is no associated converter for this thumb", "resizeThumb");
        }
    }
}