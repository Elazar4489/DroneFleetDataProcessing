using System;
using DroneSystem.Models;

namespace DroneSystem.src.Interfaces
{
    public interface IDataSource
    {
        IEnumerable<Drone> LoadData();
    }
}