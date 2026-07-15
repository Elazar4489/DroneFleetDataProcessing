using System;
using System.Collections.Generic;
using System.Text;

namespace DroneSystem.Exceptions.Validation
{
     class DroneExaption : Exception
    {
        public DroneExaption() : base() { }
    }
    class IdExaption: DroneExaption
    {
    }
    class serialNumberExaption : DroneExaption
    {
    }
    class modelExaption : DroneExaption
    {
    }
    class categoryExaption : DroneExaption
    {
    }
    class base_locationExaption : DroneExaption
    {
    }
    class flightHoursExaption : DroneExaption
    {
    }
    class batteryHealthExaption : DroneExaption
    {
    }
    class maxRangeKmExaption : DroneExaption
    {
    }
    class missionsCompletedExaption : DroneExaption
    {
    }
    class statusExaption : DroneExaption
    {
    }
}
