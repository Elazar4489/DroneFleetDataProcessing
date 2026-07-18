using System;
using System.Collections.Generic;
using System.Text; 
using System.IO;
using System.Linq;
using DroneSystem.src.Models;
using DroneSystem.src.Analytics;
using DroneSystem.src.Interfaces;
using DroneSystem.src.Models;



namespace DroneSystem.src.Reporting
{
    public class CreateReport : IPersistence
    {
        public int RejectedCount { get; set; }
        public IEnumerable<Drone> NotOperationalDrones { get; set; }
        public IEnumerable<Drone> TopFlightHours { get; set; }
        public IEnumerable<string> UniqueModels { get; set; }
        public IDictionary<string, int> NumDornesPerBase { get; set; }
        public IDictionary<string, double> BatteryAvgPerModel { get; set; }
        public IDictionary<string, int> TopModelMissionsCompleted { get; set; }
        public IEnumerable<string> BasesWithOperationalAndBattery80 { get; set; }
        public IDictionary<string, double> ThreeHighModelsPerAvgFlightHours { get; set; }

        public CreateReport(IEnumerable<Drone> cleanDrons,IEnumerable<Drone> rejectedDrones)
        {
            RejectedCount = rejectedDrones.Count();
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
                SaveData(drones.ToList(), drones.Count(), RejectedCount, fileName);
            
        }

        public void SaveData(List<Drone> allDrones, int validCount, int rejectedCount, string fileName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DRONE FLEET ANALYSIS REPORT");
            sb.AppendLine("===========================");
            sb.AppendLine("PROCESSING SUMMARY");
            sb.AppendLine($"Total raw records: {allDrones.Count + rejectedCount}"); 
            sb.AppendLine($"Valid records: {validCount}");
            sb.AppendLine($"Rejected records: {rejectedCount}");
            sb.AppendLine("===========================");
            sb.AppendLine();

            sb.AppendLine("NON-OPERATIONAL DRONES");
            if (!NotOperationalDrones.Any())
                sb.AppendLine("No results found");
            else
            {
                foreach (var d in NotOperationalDrones)
                    sb.AppendLine($"{d.serialNumber} | {d.model} | {d.base_location} | {d.status}");
            }
            sb.AppendLine();
            sb.AppendLine("TOP 5 DRONES BY FLIGHT HOURS");
            int i = 1;
            foreach (var d in TopFlightHours.Take(5))
            {
                sb.AppendLine($"{i}. {d.serialNumber} | {d.model} | {d.flightHours}");
                i++;
            }
            sb.AppendLine();

            sb.AppendLine("AVAILABLE DRONE MODELS");
            foreach (var model in UniqueModels)
                sb.AppendLine(model);
            sb.AppendLine();

            sb.AppendLine("DRONES BY BASE");
            foreach (var item in NumDornesPerBase)
                sb.AppendLine($"{item.Key}: {item.Value}");
            sb.AppendLine();

            sb.AppendLine("AVERAGE BATTERY HEALTH BY MODEL");
            foreach (var item in BatteryAvgPerModel)
                sb.AppendLine($"{item.Key}: {item.Value:F2}");
            sb.AppendLine();

            sb.AppendLine("MODEL WITH HIGHEST TOTAL COMPLETED MISSIONS");
            var topMissionModel = TopModelMissionsCompleted.OrderByDescending(x => x.Value).FirstOrDefault();
            if (topMissionModel.Key == null)
                sb.AppendLine("No results found");
            else
            {
                sb.AppendLine($"Model: {topMissionModel.Key}");
                sb.AppendLine($"Total completed missions: {topMissionModel.Value}");
            }

            string outPath = Path.Combine("output", fileName);
            Directory.CreateDirectory("output");
            File.WriteAllText(outPath, sb.ToString());
        
        }
    }
}
