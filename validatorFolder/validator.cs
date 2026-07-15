using DroneSystem.Models;
using DroneSystem.ValidationExaption;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DroneSystem.Validation
{

    class Validator
    {
        public List<Drone> Drones { get; set; }
        public Validator(List<Drone> drones)
        {
            Drones = drones;
        }

        public bool UniquenessCheck<T, S>(T item, S seen) where S : HashSet<T>
        {
            return !seen.Add(item);
        }

        public bool RangeCheck(int min, int max, int num)
        {
            if (num > max || num < min)
            {
                return false;
            }
            return true;
        }
        public bool RangeDoubleCheck(double min, double max, double num)
        {
            return num <= max || num >= min;
        }

        public bool EnumCheck<TEnum>(string item, out TEnum theEnum) where TEnum:struct,Enum
        {
            return Enum.TryParse(item, true, out theEnum);
        }



        //public bool ValidateId(Drone drone)
        //{
        //    var seenIds = new HashSet<int>();
        //    if (!seenIds.Add(drone.id))
        //    {
        //        throw new IdExaption();
        //    }
        //    else if (drone.id <= 0)
        //    {
        //        throw new IdExaption();
        //    }
        //    else return true;

        //}
        //public bool ValidateSerialNumber(Drone drone)
        //{
        //    var seenSerialNumber = new HashSet<string>();
        //    string pattern = @"^DR-\d{4}$";
        //    if (string.IsNullOrEmpty(drone.serialNumber)) throw new serialNumberExaption();
        //    if (!seenSerialNumber.Add(drone.serialNumber)) throw new serialNumberExaption();
        //    if (!Regex.IsMatch(drone.serialNumber, pattern)) throw new serialNumberExaption();

        //    else return true;
        //}
    }
}
//|| 
                    //|| drone.serialNumber[] != "D")