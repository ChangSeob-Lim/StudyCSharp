using static System.Console;

namespace _30_MultiInfInheritance
{
    interface IRunnable
    {
        void Run();
    }
    interface IFlyable
    {
        void Fly();
    }
    interface IWalkable
    {
        void Run();
    }
    public class Vehicle
    {
        public string Year;
        public string Company;
        public float HorsePower;
    }
    class FlyingCar : Vehicle, IRunnable, IFlyable
    {
        public void Fly()
        {
            WriteLine("Fly!");
        }

        public void Run()
        {
            WriteLine("Run!");
        }
    }
    class TestClass : IRunnable, IWalkable
    {
        public void Run()
        {
            WriteLine("Duplicated Interface!");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            FlyingCar car = new FlyingCar();
            car.Run();
            car.Fly();
            car.Company = "현대";

            IRunnable runnable = car as IRunnable;
            runnable.Run();

            IFlyable flyable = car as IFlyable;
            flyable.Fly();

            TestClass t = new TestClass();
            t.Run();

            IWalkable w = t as IWalkable;
            w.Run();

            IRunnable r = t as IRunnable;
            r.Run();
        }
    }
}
