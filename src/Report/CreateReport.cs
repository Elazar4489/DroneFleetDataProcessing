using DroneSystem.src.Analytics;
using DroneSystem.src.Interfaces;
using DroneSystem.src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneSystem.src.Reporting
{
    public class CreateReport : IPersistence
    {
        public IEnumerable<Drone> NotOperationalDrones { get; set; }
        public IEnumerable<Drone> TopFlightHours { get; set; }
        public IEnumerable<string> UniqueModels { get; set; }
        public IDictionary<string, int> NumDornesPerBase { get; set; }
        public IDictionary<string, double> BatteryAvgPerModel { get; set; }
        public IDictionary<string, int> TopModelMissionsCompleted { get; set; }
        public IEnumerable<string> BasesWithOperationalAndBattery80 { get; set; }
        public IDictionary<string, double> ThreeHighModelsPerAvgFlightHours { get; set; }

        public CreateReport(IEnumerable<Drone> cleanDrons)
        {
            var statical = new StatisticalAnalyzer();
            NotOperationalDrones = statical.GetNotOperationalDrones(cleanDrons);
            TopFlightHours = statical.GetTopFlightHours(cleanDrons);
            UniqueModels = statical.GetUniqueModels(cleanDrons);
            NumDornesPerBase = statical.GetNumDornesPerBase(cleanDrons);
            BatteryAvgPerModel = statical.GetBatteryAvgPerModel(cleanDrons);
            TopModelMissionsCompleted = statical.GetTopModelMissionsCompleted(cleanDrons);
            BasesWithOperationalAndBattery80 = statical.GetBasesWithOperationalAndBattery80(cleanDrons);
            ThreeHighModelsPerAvgFlightHours = statical.GetThreeHighModelsPerAvgFlightHours(cleanDrons);
        }

        public void SaveData(IEnumerable<Drone> drones, string fileName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DRONE FLEET ANALYSIS REPORT");
            sb.AppendLine("PROCESSING SUMMARY");
            sb.AppendLine($"Total raw records: {drones.Count()}");
           
            sb.AppendLine();

            int count = 0;
            sb.AppendLine("TOP 5 DRONES BY FLIGHT HOURS");
            foreach (var fligh in TopFlightHours)
            {
                sb.AppendLine($"{count}: {fligh.serialNumber} | {fligh.model} | {fligh.flightHours}");
                count += 1;

            }
            sb.AppendLine("AVAILABLE DRONE MODELS");




            string outPath = Path.Combine("output", fileName);
            Directory.CreateDirectory("output");
            File.WriteAllText(outPath, sb.ToString());
        }
     
        }
    }
}