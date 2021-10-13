using System;
using Lab4.Geometry2D;

namespace Lab4.Particles
{
	public abstract class Particle
	{
		private Point position = new Point();
		private Vector direction = new Vector();
		public double Speed { get; protected set; }

		public Point Position
		{
			get => new Point(position.X, position.Y);
			set => position = value;
		}

		public Vector Direction
		{
			get => new Vector(direction.DX, direction.DY);
			set => direction = value;
		}

		public Particle() { }

		public Particle(Point position, Vector direction, double speed)
		{
			Position = position;
			Direction = direction;
			Speed = speed;
		}

		public Particle(Particle p)
		{
			Position = p.Position;
			Direction = p.Direction;
			Speed = p.Speed;
		}

		public void SetCoords(double? px, double? py) => position.SetCoords(px, py);

		public void GetCoords(out double px, out double py) => position.GetCoords(out px, out py);

		public void SetDirection(double dx, double dy) => direction.Set(dx, dy);

		public void GetDirection(out double dx, out double dy)
		{
			dx = direction.DX;
			dy = direction.DY;
		}

		public void Drift(double time)
		{
			position.GetCoords(out double x, out double y);
			position.SetCoords(x + Speed * direction.DX * time, y + Speed * direction.DY * time);
		}

		public void SetRandomDirection(double r1, double r2)
		{
			double dx = 2 * r1 - 1;
			double dy = Math.Sqrt(1 - dx * dx) * Math.Cos(2 * Math.PI * r2);
			direction.Set(dx, dy);
		}

		public abstract void Update(double frequency, double speed, Polarization pol);
		public override string ToString()
		{
			return $"Position: {Position}\n" +
				   $"Direction: {Direction}\n" +
				   $"Speed: {Speed}";
		}
	}
}
