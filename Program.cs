using DroneSystem.Data.Persistence;
using DroneSystem.Interfaces;
using DroneSystem.Reception;
using DroneSystem.Data.Persistence;
using System;
namespace DroneSystem.program
{
    public class program
    {
         static void Main()
        {
           
            string baseDirectory = Directory.GetCurrentDirectory();
            string myFilePath = Path.Combine(baseDirectory, "input", "raw", "drones_raw.json");


            IDataSource n = new ReceptionDrone(myFilePath);
            var h = n.LoadData(); ;
            Console.WriteLine(h.Count());
            foreach (var r in h)
            {
                Console.WriteLine($"Found Drone: {r.id} - Model: {r.model} mnmn {r.base_location}");
            }

            var validDrones = 



            IPersistence saver = new CreateNewJson();
            saver.SaveData(validDrones, "drones_clean.json");

        }
    }
}