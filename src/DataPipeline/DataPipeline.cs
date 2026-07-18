using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using DroneSystem.src.Models;
using DroneSystem.src.Reception;
using DroneSystem.src.Persistence;
using DroneSystem.src.Reporting;
using DroneSystem.src.Validation.validator;


namespace DroneSystem.src.Pipeline
{
    public class DataPipeline
    {
        public void Run()
        {
            string baseDirectory = Directory.GetCurrentDirectory();
            string myFilePath = Path.Combine(baseDirectory, "input", "raw", "drones_raw.json");

            var loader = new ReceptionDrone(myFilePath);
            var drones = loader.LoadData().ToList();

            var validator = new ValidatorDrone(drones);
            validator.Validate();

            var report = new CreateReport(validator.ValidDrones, validator.RejectedDrones);

            report.SaveData(validator.ValidDrones, "report.txt");

            var saver = new CreateNewJson();
            saver.SaveData(validator.ValidDrones, "drones_clean.json");

            Console.WriteLine("Pipeline completed successfully!");
        }
    }
}