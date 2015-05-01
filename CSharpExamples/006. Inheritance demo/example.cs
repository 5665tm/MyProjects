using System;

// General Info
class Transport
{
	protected string Name; // Name
	double pri_width; // Width
	double pri_weight; // Weight
	double pri_horse; // HorsePower

	//constructor
	protected Transport (string n, double wid, double wei, double hp)
	{
		Width      = wid;
		Weight     = wei;
		HorsePower = hp;
		Name       = n;
	}

	//properties
	protected double Width
	{
		get {return pri_width; }
		private set {pri_width = value < 0 ? -value: value; }
	}

	protected double Weight
	{
		get {return pri_weight; }
		private set {pri_weight = value < 0 ? -value: value; }
	}

	protected double HorsePower
	{
		get {return pri_horse; }
		private set {pri_horse = value < 0 ? -value: value; }
	}

	//Show Info
	public virtual void ShowDim()
	{
		Console.WriteLine("Name: {0}\nWidth: {1} meter\nHorse Power: {2} hp\nWeight: {3} kg", Name, Width, HorsePower, Weight);
	}
}

// Transport => Auto
class Auto : Transport
{
	string DriveType; // FWD, RWD, 4WD

	// constructor
	protected Auto (string n, double wei, double wid, double hp, string dt) : base (n, wid, wei, hp)
	{
		DriveType = dt;
	}

	// Return Power Density
	public double HpPerTon() // HorsePower per (kg / 1000)
	{
		return HorsePower / (Weight / 1000);
	}

	// Show Tech Info
	public void ShowDrive()
	{
		Console.WriteLine("DriveType: {0}", DriveType);
	}
}

// Transport => Auto => SportCar
sealed class SportCar : Auto
{
	string RaceType;

	// constructor
	public SportCar (string n, double wei, double wid, double hp, string dt, string rt) : base (n, wei, wid, hp, dt)
	{
		RaceType = rt;
	}

	public void ShowRaceType()
	{
		Console.WriteLine("Race Type: {0}", RaceType);
	}
}

// Transport => Plane
sealed class Plane : Transport
{
	double SpanWings;

	public Plane (string n, double wei, double wid, double hp, double win) : base (n, wid, wei, hp)
	{
		SpanWings = win;
	}

	public void ShowWings()
	{
		Console.WriteLine("Span Wings: {0}", SpanWings);
	}

	//Show Info
	public override void ShowDim()
	{
		Console.WriteLine("PLANE!\nName: {0}\nWidth: {1} meter\nHorse Power: {2} hp\nWeight: {3} kg", Name, Width, HorsePower, Weight);
	}
}

class Demo
{
	static void Main()
	{
		SportCar sc1  = new SportCar("Silvia", -1230, 4.25, 440, "RWD", "Drift");
		SportCar sc2  = new SportCar("Impreza", 1380, 4.43, 506, "4WD", "Drag Racing");
		Plane p1 = new Plane("Boeing", 290000, 73.9, 9000, 60.9);

		Console.WriteLine("Information about the car \"sc1\"");
		sc1.ShowDim();
		sc1.ShowDrive();
		sc1.ShowRaceType();
		Console.WriteLine("HorsePower per (kg / 1000): {0}", sc1.HpPerTon());

		Console.WriteLine();

		Console.WriteLine("Information about the car \"sc2\"");
		sc2.ShowDim();
		sc2.ShowDrive();
		sc2.ShowRaceType();
		Console.WriteLine("HorsePower per (kg / 1000): {0}", sc2.HpPerTon());

		Console.WriteLine();

		Console.WriteLine("Information about the plane \"p1\"");
		p1.ShowDim();
		p1.ShowWings();

		Console.ReadLine();
	}
}
