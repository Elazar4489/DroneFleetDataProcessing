using DroneSystem.src.Models;
using DroneSystem.src.Reporting;
using System;
using System.Xml.Linq;
namespace DroneSystem.src.Analytics
{
    public class StatisticalAnalyzer
    {       
        public IEnumerable<Drone> GetNotOperationalDrones(IEnumerable<Drone> cleanDrones)
        {
            var NotOperationalDrones = cleanDrones.Where(r => r.status != "Operational");
            return NotOperationalDrones;
        }
        public IEnumerable<Drone> GetTopFlightHours(IEnumerable<Drone> cleanDrones)
        {
             var TopFlightHours = cleanDrones.OrderByDescending(r => r.flightHours).Take(5);
             return TopFlightHours;
        }
        public IEnumerable<string> GetUniqueModels(IEnumerable<Drone> cleanDrones)
        {
            return cleanDrones.Select(r => r.model).Distinct();
        }

        public IDictionary<string, int> GetNumDornesPerBase(IEnumerable<Drone> cleanDrones)
        {
            var NumDornesPerBase = cleanDrones.GroupBy(g => g.base_location).ToDictionary( g => g.Key, g => g.Count() );
            return NumDornesPerBase;
        }
        public IDictionary<string, double> GetBatteryAvgPerModel(IEnumerable<Drone> cleanDrones)
        {
            var BatteryAvgPerModel = cleanDrones.GroupBy(g => g.model).ToDictionary(g => g.Key, g => g.Average(r => r.batteryHealth));
            return BatteryAvgPerModel;
        }
        public IDictionary<string, int> GetTopModelMissionsCompleted(IEnumerable<Drone> cleanDrones)
        {
            var TopModelMissionsCompleted = cleanDrones.
                GroupBy(g => g.model).
                Select(g => new {Model = g.Key, TotalMissions = g.Sum(d => d.missionsCompleted)}).
                OrderByDescending(r=>r.TotalMissions).
                Take(1).
                ToDictionary(r=>r.Model, r => r.TotalMissions);
            return TopModelMissionsCompleted;
        }
        public IEnumerable<string> GetBasesWithOperationalAndBattery80(IEnumerable<Drone> cleanDrones)
        {
            var BasesWithOperationalAndBattery80 = cleanDrones.
                GroupBy(g => g.base_location).
                Where(g => g.Any(d => d.status == "Operational" && d.batteryHealth > 80)).Select(g => g.Key);
            return BasesWithOperationalAndBattery80;
        }
        public IDictionary<string, double> GetThreeHighModelsPerAvgFlightHours(IEnumerable<Drone> cleanDrones)
        {
            var ThreeHighModelsPerAvgFlightHours = cleanDrones.
                GroupBy(g => g.model).
                Select(g => new {Model = g.Key, AvgFlightHours = g.Average(r=> r.flightHours)}).
                OrderByDescending(r=>r.AvgFlightHours).
                Take(3).
                ToDictionary(r => r.Model, r => r.AvgFlightHours);
            return ThreeHighModelsPerAvgFlightHours;
        }

    }
}
