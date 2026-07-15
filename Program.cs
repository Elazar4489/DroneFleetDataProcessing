using System;
using DroneSystem.Reception;
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
            foreach (var r in h)
            {
                // נדפיס רק את ה-ID ואת ה-Model כדי לוודא שזה עובד
                Console.WriteLine($"Found Drone: {r.id} - Model: {r.model} mnmn         {r.base_location}");
            }

        }
    }
}