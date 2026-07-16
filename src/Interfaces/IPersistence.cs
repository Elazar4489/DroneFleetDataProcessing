using System;
using DroneSystem.Models;

namespace DroneSystem.src.Interfaces
{
    public interface IPersistence
    {
        void SaveData(IEnumerable<Drone> drones, string fileName);
    }
}
