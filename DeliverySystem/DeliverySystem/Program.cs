using System;

namespace DeliverySystem
{
    public class Vehicle
    {
        protected string brand;
        protected int year;
        protected double mileage;
        protected double maxSpeed;

        public Vehicle(string brand, int year, double mileage, double maxSpeed)
        {
            this.brand = brand;
            this.year = year;
            this.mileage = mileage;
            this.maxSpeed = maxSpeed;
        }

        public virtual string GetInfo()
        {
            return $"{brand} ({year}), Mileage: {mileage} km";
        }

        public virtual double GetMaxSpeed()
        {
            return maxSpeed;
        }

        public virtual void Move(double distance)
        {
            mileage += distance;
            Console.WriteLine($"{brand} drove {distance} km.");
        }
    }

    public class Scooter : Vehicle
    {
        private int batteryCapacity;
        private double batteryLevel;

        public Scooter(string brand, int year, double mileage, int batteryCapacity)
            : base(brand, year, mileage, 45.0)
        {
            this.batteryCapacity = batteryCapacity;
            batteryLevel = 100;
        }

        public override string GetInfo()
        {
            return $"Scooter: {brand} ({year}), Battery: {batteryLevel}% of {batteryCapacity}Ah";
        }

        public override void Move(double distance)
        {
            base.Move(distance);

            batteryLevel -= distance * 0.5;
            if (batteryLevel < 0)
                batteryLevel = 0;
        }

        public void Charge()
        {
            batteryLevel = 100;
            Console.WriteLine($"{brand} has been fully charged.");
        }
    }

    public class Car : Vehicle
    {
        protected int doors;
        protected double fuelLevel;

        public Car(string brand, int year, double mileage, int doors)
            : base(brand, year, mileage, 180.0)
        {
            this.doors = doors;
            fuelLevel = 50;
        }

        public override string GetInfo()
        {
            return $"Car: {brand} ({year}), Doors: {doors}, Fuel: {fuelLevel}L";
        }

        public override void Move(double distance)
        {
            base.Move(distance);

            fuelLevel -= distance * 0.1;
            if (fuelLevel < 0)
                fuelLevel = 0;
        }

        public void Refuel(double liters)
        {
            fuelLevel += liters;
            if (fuelLevel > 50)
                fuelLevel = 50;
        }
    }

    public class Van : Car
    {
        private double loadCapacity;
        private double currentLoad;

        public Van(string brand, int year, double mileage, int doors, double loadCapacity)
            : base(brand, year, mileage, doors)
        {
            this.loadCapacity = loadCapacity;
            currentLoad = 0;

            this.maxSpeed = 140.0;
        }

        public override string GetInfo()
        {
            return $"Van: {brand} ({year}), Doors: {doors}, Load: {currentLoad}/{loadCapacity}kg, Fuel: {fuelLevel}L";
        }

        public void LoadCargo(double weight)
        {
            if (currentLoad + weight <= loadCapacity)
            {
                currentLoad += weight;
                Console.WriteLine($"{weight} kg loaded into the van.");
            }
            else
            {
                Console.WriteLine("Too heavy! Cannot load more cargo.");
            }
        }

        public void UnloadCargo()
        {
            currentLoad = 0;
            Console.WriteLine("Van unloaded.");
        }
    }
}
