using System;
using System.Collections.Generic;
using System.Text;

namespace DroneSystem.Models
{
    internal class Drone
    {
        public int id { get; set; }
        public string serialNumber { get; set; }
        public string model { get; set; }
        public string category { get; set; }
        public string base_location { get; set; }
        public double flightHours { get; set; }
        public int batteryHealth { get; set; }
        public double maxRangeKm { get; set; }
        public int missionsCompleted { get; set; }
        public string status { get; set; }
        public Drone(int Iid, string IserialNumber, string Imodel, string Icategory, string Ibase_location, 
            double IflightHours, int IbatteryHealth, double ImaxRangeKm, int ImissionsCompleted, string Istatus)
        {
            id = Iid;
            serialNumber = IserialNumber;
            model = Imodel;
            category = Icategory;
            base_location = Ibase_location;
            flightHours = IflightHours;
            batteryHealth = IbatteryHealth;
            maxRangeKm = ImaxRangeKm;
            missionsCompleted = ImissionsCompleted;
            status = Istatus;
        }
    }
}
