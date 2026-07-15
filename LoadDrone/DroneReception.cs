using DroneSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace DroneSystem.Reception
{
    public class ReceptionDrone
    {
        public List<Drone> drones = new List<Drone>();

        public string path;

        public ReceptionDrone(string Path)
        {
            path = Path;

        }
        public List<Drone> loadJson()
        {
            try
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine($"Not in path: {path}");
                    return new List<Drone>();
                }
                Console.WriteLine("tride: " + Path.GetFullPath(path));
                using (StreamReader r = new StreamReader(path))
                {
                    string jsonString = r.ReadToEnd();
                    var data = JsonSerializer.Deserialize<List<Drone>>(jsonString);
                    if (data != null)
                    {
                        drones = data;
                        return drones;
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Eror in lodJeson UnauthorizedAccessException :{ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Eror in loadJeson JsonException :{ex.Message}");
            }
            catch (FieldAccessException ex)
            {
                Console.WriteLine($"Eror in loadJeson FieldAccessException:{ex.Message}");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error in loadJsonException: {ex.Message}");
            }
            return new List<Drone>();

        }
   
    }

}