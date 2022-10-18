using System;

namespace Ex03.GarageLogic
{
    public class GenericVehicle
    {
        public enum eVehicleType
        {
            Car,
            Motorcycle,
            Truck
        }

        // $G$ DSN-011 (-3) The creator should initiate uniqe memmbers of each type as well.
        public static Vehicle VehicleCreator(string i_LicenseNumber, string i_ModelName, Engine.eEnergyType i_EnergyType, string i_ManufacturerName, float i_PrecentOfEnergyLeft, float i_CurrentAirPressure, int i_VehicleType)
        {
            Vehicle newVehicle;

            switch (i_VehicleType)
            {
                case 1:
                    newVehicle = new Car(i_ModelName, i_LicenseNumber, i_CurrentAirPressure, i_ManufacturerName, i_EnergyType, i_PrecentOfEnergyLeft);
                    break;
                case 2:
                    newVehicle = new Motorcycle(i_ModelName, i_LicenseNumber, i_CurrentAirPressure, i_ManufacturerName, i_EnergyType, i_PrecentOfEnergyLeft);
                    break;
                case 3:
                    newVehicle = new Truck(i_ModelName, i_LicenseNumber, i_CurrentAirPressure, i_ManufacturerName, i_EnergyType, i_PrecentOfEnergyLeft);
                    break;
                default:
                    throw new ArgumentNullException();
            }

            return newVehicle;
        }
    }
}
