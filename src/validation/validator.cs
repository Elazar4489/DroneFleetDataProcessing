using DroneSystem.src.Models;
using DroneSystem.src.Validation.validatorExaption;
using DroneSystem.src.Validation.validateorAbstractClass;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DroneSystem.src.Validation.validator
{


    public class ValidatorDrone
    {
        private readonly List<IDroneValidationRule> _allRules;
        public List<Drone> Drones { get; set; }
        public ValidatorDrone(List<Drone> drones)
        {
            Drones = drones;
            _allRules = new List<IDroneValidationRule>
            {
                new IdValidator(),
                new serialNumberValidator(),
                new modelValidator(),
                new categoryValidator(),
                new base_locationValidator(),
                new flightHoursValidator(),
                new batteryHealthValidator(),
                new maxRangeKmValidator(),
                new missionsCompletedValidator(),
                new statusValidator(),
                new correctnessStatusValidator()
            };
        }
        public bool IsValid(Drone drone)
        {
            foreach (var rule in _allRules)
            {
                if (!rule.IValidate(drone))
                {
                    return false;
                }
            }
            return true;
        }
        public List<Drone> Validate()
        {
            List<Drone> ValidDrones = new List<Drone>();
            foreach (Drone drone in Drones)
            {
                try
                {
                    if (IsValid(drone))
                    {
                        ValidDrones.Add(drone);
                    }
                }
                catch (IdException ex) { Console.WriteLine(ex.Message + drone.id);}
                catch (SerialNumberException ex) { Console.WriteLine(ex.Message + drone.id); }
                catch (ModelException ex) { Console.WriteLine(ex.Message + drone.id); }
                catch (CategoryException ex) { Console.WriteLine(ex.Message + drone.id); }
                catch (BaseLocationException ex) { Console.WriteLine(ex.Message + drone.id); }
                catch (FlightHoursException ex) { Console.WriteLine(ex.Message + drone.id); }
                catch (BatteryHealthException ex) { Console.WriteLine(ex.Message + drone.id); }
                catch (MaxRangeKmException ex) { Console.WriteLine(ex.Message + drone.id); }
                catch (MissionsCompletedException ex) { Console.WriteLine(ex.Message + drone.id); }
                catch (StatusException ex) { Console.WriteLine(ex.Message + drone.id); }
            }
            return ValidDrones;
        }
    }
}