using System;
using System.Collections.Generic;

using Lab4.Geometry2D;
using Lab4.Particles;
using Lab4.Materials;

namespace Lab4.ModelComponents
{

	public enum SurfaceLocation
	{
		left = 0,
		top = 1,
		right = 2,
		bot = 3
	}

	public class Cell : Rectangle
	{
		private const int NUM_SURFACES = 4;
		private List<Phonon> phonons = new() { };
		private List<Phonon> incomingPhonons = new() { };
		private ISurface[] surfaces = new ISurface[NUM_SURFACES];
		public List<Phonon> Phonons { get { return phonons; } }
		private Sensor sensor;

		public Cell(double length, double width, Sensor sensor) : base(length, width)
		{
			this.sensor = sensor;
			this.sensor.AddToArea(this.Area);

			for (int i = 0; i < NUM_SURFACES; ++i)
			{
				surfaces[i] = new BoundarySurface((SurfaceLocation)i, this);
			}
		}

		public void AddPhonon(Phonon p)
		{
			phonons.Add(p);
		}

		public void AddIncPhonon(Phonon p)
		{
			incomingPhonons.Add(p);
		}

		public void MergeIncPhonons()
		{
			phonons.AddRange(incomingPhonons);
			incomingPhonons.Clear();
		}

		public ISurface GetSurface(SurfaceLocation loc)
		{
			return surfaces[(int)loc];
		}

		public SurfaceLocation? MoveToNearestSurface(Phonon p)
		{
			// Returns the time taken to for a phonon to move back into the cell or 0 if the phonon did not exit the cell
			double GetTime(double dist, double pos, double vel)
			{
				if (pos <= 0) { return pos / vel; } // pos is negative therefore vel must be negative
				else if (pos >= dist) { return (pos - dist) / vel; } // pos is + therefore vel is + and len < pos
				else return 0; // No surface was reached
			}

			p.Drift(p.DriftTime);
			p.GetCoords(out double px, out double py);
			p.GetDirection(out double dx, out double dy);
			double vx = dx * p.Speed;
			double vy = dy * p.Speed;

			// The longer the time, the sooner the phonon impacted the corresponding surface
			double timeToSurfaceX = (vx != 0) ? GetTime(Length, px, vx) : 0;
			double timeToSurfaceY = (vy != 0) ? GetTime(Width, py, vy) : 0;

			// Time needed to backtrack the phonon to the first surface collision
			double backtrackTime = Math.Max(timeToSurfaceX, timeToSurfaceY);
			p.DriftTime = backtrackTime;
			if (backtrackTime == 0) { return null; } // The phonon did not collide with a surface
			p.Drift(-backtrackTime);

			// Miminize FP errors and determine impacted surface
			if (backtrackTime == timeToSurfaceX)
			{
				if (vx < 0)
				{
					p.SetCoords(0, null);
					return SurfaceLocation.left;
				}
				else
					p.SetCoords(Length, null);
				return SurfaceLocation.right;
			}
			else
			{
				if (vy < 0)
				{
					p.SetCoords(null, 0);
					return SurfaceLocation.bot;
				}
				else
				{
					p.SetCoords(null, Width);
					return SurfaceLocation.top;
				}
			}
		}

		public Sensor GetSensor()
		{
			return sensor;
		}

		public void TakeMeasurements(double effEnergy, double tEq)
		{
			sensor.TakeMeasurements(phonons, effEnergy, tEq);
		}

		public override string ToString()
		{
			return string.Format("{0,-20} {1,-7} {2,-7}", sensor.ToString(), phonons.Count, incomingPhonons.Count);
		}
	}
}
