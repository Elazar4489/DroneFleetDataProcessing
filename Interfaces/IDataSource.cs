using System;
using DroneSystem.Models;

namespace DroneSystem.Interfaces
{
    public interface IDataSource
    {
        IEnumerable<Drone> LoadData();
    }
}