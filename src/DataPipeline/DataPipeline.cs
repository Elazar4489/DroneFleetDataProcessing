using DroneSystem.Models;
using DroneSystem.Reception;
using DroneSystem.Validation;
using DroneSystem.Data.Persistence;
using DroneSystem.Interfaces;
using DroneSystem.Reception;
using DroneSystem.Data.Persistence;
using System;
namespace DroneSystem.src.Pipeline
{
    public class DataPipeline
    {
        public void Run()
        {
            string baseDirectory = Directory.GetCurrentDirectory();
            string myFilePath = Path.Combine(baseDirectory, "input", "raw", "drones_raw.json");

            var loader = new ReceptionDrone(myFilePath);
            var drones = loader.LoadData();

            var validator = new ValidatorDrone(drones);
            var validDrones = validator.Validate();

            var saver = new CreateNewJson();
            saver.SaveData(validDrones, "drones_clean.json");

            var report = new CreateReport(validDrones);
            report.SaveData(validDrones, "report.txt");


            Console.WriteLine("Pipeline completed successfully!");
        }
    }
}