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
        public List<Drone> ValidDrones { get; private set; } = new List<Drone>();
        public List<Drone> RejectedDrones { get; private set; } = new List<Drone>();
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
            ValidDrones.Clear();
            RejectedDrones.Clear();

            foreach (Drone drone in Drones)
            {
                try
                {
                    if (IsValid(drone))
                    {
                        ValidDrones.Add(drone);
                    }
                    else
                    {
                        RejectedDrones.Add(drone);
                    }
                }
                catch (IdException ex) { Console.WriteLine(ex.Message + drone.id); RejectedDrones.Add(drone); }
                catch (SerialNumberException ex) { Console.WriteLine(ex.Message + drone.id); RejectedDrones.Add(drone); }
                catch (ModelException ex) { Console.WriteLine(ex.Message + drone.id); RejectedDrones.Add(drone); }
                catch (CategoryException ex) { Console.WriteLine(ex.Message + drone.id); RejectedDrones.Add(drone); }
                catch (BaseLocationException ex) { Console.WriteLine(ex.Message + drone.id); RejectedDrones.Add(drone); }
                catch (FlightHoursException ex) { Console.WriteLine(ex.Message + drone.id); RejectedDrones.Add(drone); }
                catch (BatteryHealthException ex) { Console.WriteLine(ex.Message + drone.id); RejectedDrones.Add(drone); }
                catch (MaxRangeKmException ex) { Console.WriteLine(ex.Message + drone.id); RejectedDrones.Add(drone); }
                catch (MissionsCompletedException ex) { Console.WriteLine(ex.Message + drone.id); RejectedDrones.Add(drone); }
                catch (StatusException ex) { Console.WriteLine(ex.Message + drone.id); RejectedDrones.Add(drone); }
            }
            return ValidDrones;
        }
    }
}