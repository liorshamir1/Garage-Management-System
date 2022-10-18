using System;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenceType;
        private int m_EngineCapacity;

        public enum eLicenseType
        {
            A,
            AA,
            A2,
            B
        }

        public eLicenseType LicenceType
        {
            get
            {
                return m_LicenceType;
            }

            set
            {
                m_LicenceType = value;
            }
        }

        public int EngineCapacity
        {
            get
            {
                return m_EngineCapacity;
            }

            set
            {
                m_EngineCapacity = value;
            }
        }

        public Motorcycle(string i_ModelName, string i_LicenseNumber, float i_CurrentAirPressure, string i_ManufacturerName, Engine.eEnergyType i_EnergyType, float i_PrecentOfEnergyLeft) : base(i_ModelName, i_LicenseNumber, 2, 30f, i_CurrentAirPressure, i_ManufacturerName)
        {
            CollectionOfWheels.Capacity = 2;
            for (int i = 0; i < CollectionOfWheels.Capacity; i++)
            {
                if (CollectionOfWheels[i].CurrentAirPressure > 30f)
                {
                    throw new ValueOutOfRangeException(0, 30);
                }
            }

            if (Engine.eEnergyType.Fuel == i_EnergyType)
            {
                VehicleEngine = new FuelEngine(FuelEngine.eFuelType.Octan98)
                {
                    MaxCapacity = 5.8f
                };
            }
            else if (Engine.eEnergyType.Electric == i_EnergyType)
            {
                VehicleEngine = new ElectricEngine
                {
                    MaxCapacity = 2.3f
                };
            }

            PercentageOfEnergyRemaining = i_PrecentOfEnergyLeft;
            VehicleEngine.CurrentCapacity = (i_PrecentOfEnergyLeft * VehicleEngine.MaxCapacity) / 100;
        }

        public override void ChangeDetails(object i_LicenceType, object i_EngineCapacity)
        {
            LicenceType = (eLicenseType)i_LicenceType;
            EngineCapacity = (int)i_EngineCapacity;
        }

        public override string ToString()
        {
            string motorcycleDetails = string.Format(
@"Licence type: {0}
Engine capacity: {1}",
                LicenceType,
                EngineCapacity);

            return motorcycleDetails;
        }
    }
}
