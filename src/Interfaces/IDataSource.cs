using System;
using DroneSystem.src.Models;

namespace DroneSystem.src.Interfaces
{
    public interface IDataSource
    {
        IEnumerable<Drone> LoadData();
    }
}