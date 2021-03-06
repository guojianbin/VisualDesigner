﻿namespace Glass.Design.Pcl.Core
{
    public interface ICoreTypesFactory
    {
        IPoint CreatePoint(double x, double y);

        // ReSharper disable once TooManyArguments
        IRect CreateRect(double left, double top, double width, double height);
        ISize CreateSize(double width, double height);
        IVector CreateVector(double x, double y);
    }
}
