using DroneSystem.src.Enums;
using DroneSystem.src.Exceptions.Validation;
using DroneSystem.src.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace DroneSystem.src.Validation
{
    public interface IDroneValidationRule
    {
        bool IValidate(Drone drone);
    }
    abstract class Validateor<TValue> : IDroneValidationRule
    {
        public DroneExaption TheException { get; private set; }
        private readonly Func<Drone, TValue> _selector;
        protected Validateor(Func<Drone, TValue> selector, DroneExaption droneExaption)
        {
            TheException = droneExaption; 
            _selector = selector;
        }
        public abstract bool Validate(TValue value);
        public bool IValidate(Drone drone)
        {
            TValue specificValue = _selector(drone);
            return Validate(specificValue);
        }
        protected bool UniquenessCheck<TItem, SItem>(TItem item, SItem seen) where SItem : HashSet<TItem>
        {
            return seen.Add(item);
        }
        protected bool RangeCheck(int min, int max, int num)
        {
            if (num > max || num < min)
            {
                return false;
            }
            return true;
        }
        protected bool RangeDoubleCheck(double min, double max, double num)
        {
            return num <= max && num >= min;
        }
        protected bool EnumCheck<TEnum>(string item, out TEnum theEnum) where TEnum : struct, Enum
        {
            return Enum.TryParse(item, out theEnum);
        }
    }
    class IdValidator : Validateor<int>
    {
        public IdValidator() : base(drone => drone.id, new IdExaption()) { }
        public override bool Validate(int value)
        {
            var seenIds = new HashSet<int>();
            if (value < 0 || !UniquenessCheck(value, seenIds))
            {
                throw TheException;
            }
            return true;
        }
    }
    class serialNumberValidator : Validateor<string>
    {
        public serialNumberValidator() : base(drone => drone.serialNumber, new serialNumberExaption()) { }
        public override bool Validate(string value)
        {
            string pattern = @"^DR-\d{4}$";
            var seenserialNumber = new HashSet<string>();
            if (string.IsNullOrEmpty(value)) throw TheException;
            if (!UniquenessCheck(value, seenserialNumber)) throw TheException;
            if (!Regex.IsMatch(value, pattern)) throw TheException;
            return true;
        }
    }
    class modelValidator : Validateor<string>
    {
        public modelValidator() : base(drone => drone.model, new modelExaption()) { }
        public override bool Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value)){throw TheException;}
            string normalizedValue = value.Replace("-", "_");
            if (!EnumCheck<ModelEnum>(normalizedValue, out _))
            {
                throw TheException;
            }
            return true;
        }
    }
    class categoryValidator : Validateor<string>
    {
        public categoryValidator() : base(drone => drone.category, new categoryExaption()) { }
        public override bool Validate(string value)
        {
            if (!EnumCheck< EnumsCategory>(value, out _))
            {
                throw TheException;
            }
            return true;
        }
    }
    class base_locationValidator : Validateor<string>
    {
        public base_locationValidator() : base(drone => drone.base_location, new base_locationExaption()) { }
        public override bool Validate(string value)
        {
            if (!EnumCheck< EnumsBase_location>(value, out _))
            {
                throw TheException;
            }
            return true;
        }
    }
    class flightHoursValidator : Validateor<double>
    {
        public flightHoursValidator() : base(drone => drone.flightHours, new flightHoursExaption()) { }
        public override bool Validate(double value)
        {
            if (!RangeDoubleCheck(0, 2500, value))
            {
                throw TheException;
            }
            return true;
        }
    }
    class batteryHealthValidator : Validateor<int>
    {
        public batteryHealthValidator() : base(drone => drone.batteryHealth, new batteryHealthExaption()) { }
        public override bool Validate(int value)
        {
            if (!RangeCheck(0, 100, value))
            {
                throw TheException;
            }
            return true;
        }
    }
    class maxRangeKmValidator : Validateor<double>
    {
        public maxRangeKmValidator() : base(drone => drone.maxRangeKm, new maxRangeKmExaption()) { }
        public override bool Validate(double value)
        {
            if (!RangeDoubleCheck(1, 150, value))
            {
                throw TheException;
            }
            return true;
        }
    }
    class missionsCompletedValidator : Validateor<int>
    {
        public missionsCompletedValidator() : base(drone => drone.missionsCompleted, new missionsCompletedExaption()) { }
        public override bool Validate(int value)
        {
            if (!RangeCheck(0, 5000, value))
            {
                throw TheException;
            }
            return true;
        }
    }
    class statusValidator : Validateor<string>
    {
        public statusValidator() : base(drone => drone.status, new statusExaption()) { }
        public override bool Validate(string value)
        {
            if (!EnumCheck<DroneStatus>(value, out _))
            {
                throw TheException;
            }
            return true;
        }
    }
    class correctnessStatusValidator: Validateor<Drone>
    {
        public correctnessStatusValidator():base(drone => drone, new statusExaption()) { }
        public override bool Validate(Drone value)
        {
            if (value.batteryHealth < 20 && Enum.Parse<DroneStatus>(value.status, true) == DroneStatus.Operational)
            {
                throw TheException;
            }
            return true;
        }
    }
}
