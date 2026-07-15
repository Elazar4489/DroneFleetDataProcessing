using DroneSystem.Interfaces;
using DroneSystem.Models;
using System;
using System.IO.Enumeration;
using System.IO;
using System.Text.Json;

namespace DroneSystem.Data.Persistence
{

    public class DroneJsonSaver : IPersistence
    {
        public void SaveData(IEnumerable<Drone> drones, string fileName)
        {
            try
            {
                string outputPath = Path.Combine("output", fileName);
                Directory.CreateDirectory("output");

                var options = new JsonSerializerOptions { WriteIndented = true };

                string jsonString = JsonSerializer.Serialize(drones, options);

                File.WriteAllText(outputPath, jsonString);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Eror in UnauthorizedAccessException : {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Eror in IOException : {ex.Message}");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Eror in Exception :{ex.Message}");

            }



        }

    }
}



