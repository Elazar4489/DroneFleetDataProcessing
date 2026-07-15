using DroneSystem.Models;
using DroneSystem.Reception;
using DroneSystem.Validation;
using System;
namespace DroneSystem.program
{
    public class program
    {
         static void Main()
        {
           
            string baseDirectory = Directory.GetCurrentDirectory();
            string myFilePath = Path.Combine(baseDirectory, "input", "raw", "drones_raw.json");

      
            var n = new ReceptionDrone(myFilePath);
            var h = n.loadJson();
            Console.WriteLine(h.Count);
            var V = new ValidatorDrone(h);
            List<Drone> newList = V.Validate();
            Console.WriteLine(newList.Count);
           

        }
    }
}