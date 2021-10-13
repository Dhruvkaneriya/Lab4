using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
	namespace Geometry2D
	{
		public class Point
		{
			public double X { get; set; }
			public double Y { get; set; }
			public Point(double x = 0, double y = 0)
			{
				X = x;
				Y = y;
			}

			public void GetCoords(out double x, out double y)
			{
				x = X;
				y = Y;
			}

			public void SetCoords(double x, double y)
			{
				X = x;
				Y = y;
			}

			public void SetCoords(double? x, double? y)
			{
				X = x ?? X;
				Y = y ?? Y;
			}
			public override string ToString() => $"({X}, {Y})\n";
		}

		public class Vector
		{
			public double DX { get; private set; }
			public double DY { get; private set; }

			public Vector(double dx = 0, double dy = 0) => Set(dx, dy);

			public void Set(double dx, double dy)
			{
				bool InRange(double x) => x <= 1 && x >= -1;

				if (InRange(dx) && InRange(dy))
				{
					DX = dx;
					DY = dy;
				}
				else
				{
					throw new ArgumentOutOfRangeException("Direction components must be in the range [-1, 1]");
				}
			}
			public override string ToString() => $"({DX}, {DY})\n";
		}

		public class Rectangle
		{
			public double Length { get; }
			public double Width { get; }
			public double Area { get; }
			public Rectangle(double length, double width)
			{
				Length = length;
				Width = width;
				Area = Length * Width;
			}
		}
	}
}
