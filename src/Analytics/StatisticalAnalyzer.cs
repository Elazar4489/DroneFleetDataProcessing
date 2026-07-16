using DroneSystem.Models;
using DroneSystem.Reporting;
using System;
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





    }
}
