using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleOwner> m_VehiclesInTheGarage;

        public enum eVehicleStatusInTheGarage
        {
            InRepair,
            Fixed,
            Paid
        }

        public Garage()
        {
            m_VehiclesInTheGarage = new Dictionary<string, VehicleOwner>();
        }

        public Dictionary<string, VehicleOwner> VehiclesInTheGarage
        {
            get
            {
                return m_VehiclesInTheGarage;
            }
        }

        public void AddNewVehicleToGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            if (!isLicenseExist(i_Vehicle.LicenseNumber))
            {
                VehiclesInTheGarage.Add(i_Vehicle.LicenseNumber, new VehicleOwner(i_OwnerName, i_OwnerPhoneNumber, i_Vehicle));
            }
        }

        private bool isLicenseExist(string i_LicenseNumber)
        {
            bool isExist = true;
            if (VehiclesInTheGarage.ContainsKey(i_LicenseNumber))
            {
                VehiclesInTheGarage[i_LicenseNumber].VehicleOfOwner.VehicleStatusInTheGarage = eVehicleStatusInTheGarage.InRepair;
                Console.WriteLine(string.Format(@"
A vehicle with such a license number is already in the garage.
The vehicle status became InFix."));
            }
            else
            {
                isExist = false;
                Console.WriteLine("The vehicle was successfully added");
            }

            return isExist;
        }

        public StringBuilder ShowVehiclesInGarage(int i_DesiredStatusToShow)
        {
            string openingLine;
            StringBuilder listOfAllLicenses = new StringBuilder();

            switch (i_DesiredStatusToShow)
            {
                case 1:
                    foreach (string license in VehiclesInTheGarage.Keys)
                    {
                        listOfAllLicenses.AppendFormat(
@"{0}
",
license);
                    }

                    break;
                case 2:
                    foreach (string license in VehiclesInTheGarage.Keys)
                    {
                        if (VehiclesInTheGarage[license].VehicleOfOwner.VehicleStatusInTheGarage == eVehicleStatusInTheGarage.InRepair)
                        {
                            listOfAllLicenses.AppendFormat(
@"{0}
",
license);
                        }
                    }

                    break;
                case 3:
                    foreach (string license in VehiclesInTheGarage.Keys)
                    {
                        if (VehiclesInTheGarage[license].VehicleOfOwner.VehicleStatusInTheGarage == eVehicleStatusInTheGarage.Fixed)
                        {
                            listOfAllLicenses.AppendFormat(
@"{0}
",
license);
                        }
                    }

                    break;
                case 4:
                    foreach (string license in VehiclesInTheGarage.Keys)
                    {
                        if (VehiclesInTheGarage[license].VehicleOfOwner.VehicleStatusInTheGarage == eVehicleStatusInTheGarage.Paid)
                        {
                            listOfAllLicenses.AppendFormat(
@"{0}
",
license);
                        }
                    }

                    break;
            }

            if (listOfAllLicenses.Length == 0)
            {
                listOfAllLicenses.AppendFormat(@"
There are no vehicles in this status.
");
            }
            else
            {
                openingLine = string.Format(@"
All the vehicles in this status:
");
                listOfAllLicenses.Insert(0, openingLine);
            }

            return listOfAllLicenses;
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatusInTheGarage i_NewStatus)
        {
            if (VehiclesInTheGarage.ContainsKey(i_LicenseNumber))
            {
                VehiclesInTheGarage[i_LicenseNumber].VehicleOfOwner.VehicleStatusInTheGarage = i_NewStatus;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void InflateWheelsToMaximum(string i_LicenseNumber)
        {
            if (VehiclesInTheGarage.ContainsKey(i_LicenseNumber))
            {
                foreach (Wheel wheel in VehiclesInTheGarage[i_LicenseNumber].VehicleOfOwner.CollectionOfWheels)
                {
                    wheel.InflationAction(wheel.MaximumAirPressure - wheel.CurrentAirPressure);
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void RefuelFuelEngine(string i_LicenseNumber, FuelEngine.eFuelType i_FuelType, float i_AmountToFuel)
        {
            if (VehiclesInTheGarage.ContainsKey(i_LicenseNumber))
            {
                Vehicle VehicleFuel = VehiclesInTheGarage[i_LicenseNumber].VehicleOfOwner;
                if (VehicleFuel.VehicleEngine is FuelEngine)
                {
                    (VehicleFuel.VehicleEngine as FuelEngine).RefuelingVehicle(i_AmountToFuel, i_FuelType);
                    VehicleFuel.PercentageOfEnergyRemaining = (VehicleFuel.VehicleEngine.CurrentCapacity * VehicleFuel.VehicleEngine.MaxCapacity) / 100;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void ChargeElectricVehicle(string i_LicenseNumber, float i_MinutesToAdd)
        {
            i_MinutesToAdd /= 60;
            if (VehiclesInTheGarage.ContainsKey(i_LicenseNumber))
            {
                Vehicle VehicleElectric = VehiclesInTheGarage[i_LicenseNumber].VehicleOfOwner;
                if (VehicleElectric.VehicleEngine is ElectricEngine)
                {
                    (VehicleElectric.VehicleEngine as ElectricEngine).ChargeVehicle(i_MinutesToAdd);
                    VehicleElectric.PercentageOfEnergyRemaining = (VehicleElectric.VehicleEngine.CurrentCapacity * VehicleElectric.VehicleEngine.MaxCapacity) / 100;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public string ShowVehicleDetails(string i_LicenseNumber)
        {
            string listOfAllLicense, kindOfVehicle = string.Empty;
            float maxAirPressure;

            if (VehiclesInTheGarage.ContainsKey(i_LicenseNumber))
            {
                getKindOfVehicle(out kindOfVehicle, i_LicenseNumber);
                maxAirPressure = getMaxAirPressure(kindOfVehicle);

                listOfAllLicense = string.Format(
@"This is a {0}
License Number: {1}
Model Name: {2}
Owner Name: {3}
Current Status: {4}
Wheel Manufacturer: {5}
Wheel Air Pressure: {6}/{7}
{8}
{9}
",
                kindOfVehicle,
                i_LicenseNumber,
                VehiclesInTheGarage[i_LicenseNumber].VehicleOfOwner.ModelName,
                VehiclesInTheGarage[i_LicenseNumber].VehicleOwnerName,
                VehiclesInTheGarage[i_LicenseNumber].VehicleOfOwner.VehicleStatusInTheGarage,
                VehiclesInTheGarage[i_LicenseNumber].VehicleOfOwner.CollectionOfWheels[0].ManufacturerName,
                VehiclesInTheGarage[i_LicenseNumber].VehicleOfOwner.CollectionOfWheels[0].CurrentAirPressure,
                maxAirPressure,
                VehiclesInTheGarage[i_LicenseNumber].VehicleOfOwner.VehicleEngine.ToString(),
                VehiclesInTheGarage[i_LicenseNumber].VehicleOfOwner.ToString());

                return listOfAllLicense;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        private void getKindOfVehicle(out string o_KindOfVehicle, string i_LicenseNumber)
        {
            o_KindOfVehicle = string.Empty;

            if (VehiclesInTheGarage[i_LicenseNumber].VehicleOfOwner is Car)
            {
                o_KindOfVehicle = "Car";
            }
            else if (VehiclesInTheGarage[i_LicenseNumber].VehicleOfOwner is Motorcycle)
            {
                o_KindOfVehicle = "Motorcycle";
            }
            else if (VehiclesInTheGarage[i_LicenseNumber].VehicleOfOwner is Truck)
            {
                o_KindOfVehicle = "Truck";
            }
        }

        private float getMaxAirPressure(string i_KindOfVehicle)
        {
            float maxAirPressure = 0;

            if (i_KindOfVehicle.Equals("Car"))
            {
                maxAirPressure = 29;
            }
            else if (i_KindOfVehicle.Equals("Motorcycle"))
            {
                maxAirPressure = 30;
            }
            else if (i_KindOfVehicle.Equals("Truck"))
            {
                maxAirPressure = 25;
            }

            return maxAirPressure;
        }
    }
}