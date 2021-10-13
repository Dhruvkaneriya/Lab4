using System;

namespace Lab4.Particles
{
	public enum Polarization
	{
		LA,
		TA
	}

	public class Phonon : Particle
	{
		public int Sign { get; private set; }
		public double Frequency { get; private set; }
		public Polarization Polarization { get; private set; }

		public bool Active { get; set; }
		public double DriftTime { get; set; }

		public Phonon(int sign)
		{
			SetSign(sign);
			Active = true;
			DriftTime = 0;
		}

		public Phonon(Phonon p) : base(p)
		{
			Sign = p.Sign;
			Frequency = p.Frequency;
			Polarization = p.Polarization;
			Active = p.Active;
			DriftTime = p.DriftTime;
		}

		public void SetSign(int sign)
		{
			if (sign == -1 || sign == 1)
				Sign = sign;
			else
				throw new ArgumentOutOfRangeException("Phonon Sign must be either -1 or 1.");
		}

		public override void Update(double frequency, double speed, Polarization pol)
		{
			Frequency = frequency;
			Speed = speed;
			Polarization = pol;
		}
		public override string ToString()
		{
			return $"Frequency: {Frequency}\n" +
				   $"Speed: {Speed}\n" +
				   $"Polarization: {Polarization}\n" +
				   base.ToString();
		}
	}
}