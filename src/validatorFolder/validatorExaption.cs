using System;
using System.Collections.Generic;
using System.Text;

namespace DroneSystem.src.Exceptions.Validation
{
    public class DroneException : Exception
    {
        public DroneException(string message) : base(message) { }
    }

    public class IdException : DroneException
    {
        public IdException(string message) : base(message) { }
    }

    public class SerialNumberException : DroneException
    {
        public SerialNumberException(string message) : base(message) { }
    }

    public class ModelException : DroneException
    {
        public ModelException(string message) : base(message) { }
    }

    public class CategoryException : DroneException
    {
        public CategoryException(string message) : base(message) { }
    }

    public class BaseLocationException : DroneException
    {
        public BaseLocationException(string message) : base(message) { }
    }

    public class FlightHoursException : DroneException
    {
        public FlightHoursException(string message) : base(message) { }
    }

    public class BatteryHealthException : DroneException
    {
        public BatteryHealthException(string message) : base(message) { }
    }

    public class MaxRangeKmException : DroneException
    {
        public MaxRangeKmException(string message) : base(message) { }
    }

    public class MissionsCompletedException : DroneException
    {
        public MissionsCompletedException(string message) : base(message) { }
    }

    public class StatusException : DroneException
    {
        public StatusException(string message) : base(message) { }
    }
}