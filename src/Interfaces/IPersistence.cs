using System;
using DroneSystem.Models;

namespace DroneSystem.Interfaces
{
    public interface IPersistence
    {
        void SaveData(IEnumerable<Drone> drones, string fileName);
    }
}
