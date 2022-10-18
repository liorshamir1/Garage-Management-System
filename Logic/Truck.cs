using System;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsDrivingRefrigeratedContents;
        private float m_CargoVolume;

        public bool IsDrivingRefrigeratedContents
        {
            get
            {
                return m_IsDrivingRefrigeratedContents;
            }

            set
            {
                m_IsDrivingRefrigeratedContents = value;
            }
        }

        public float CargoVolume
        {
            get
            {
                return m_CargoVolume;
            }

            set
            {
                m_CargoVolume = value;
            }
        }


        // $G$ DSN-999 (-3) In this implementation it is possible to create an electric truck.
        public Truck(string i_ModelName, string i_LicenseNumber, float i_CurrentAirPressure, string i_ManufacturerName, Engine.eEnergyType i_EnergyType, float i_PrecentOfEnergyLeft) : base(i_ModelName, i_LicenseNumber, 16, 25f, i_CurrentAirPressure, i_ManufacturerName)
        {
            CollectionOfWheels.Capacity = 16;
            for (int i = 0; i < CollectionOfWheels.Capacity; i++)
            {
                if (CollectionOfWheels[i].CurrentAirPressure > 25f)
                {
                    throw new ValueOutOfRangeException(0, 25);
                }
            }

            VehicleEngine = new FuelEngine(FuelEngine.eFuelType.Soler)
            {
                MaxCapacity = 130f
            };

            PercentageOfEnergyRemaining = i_PrecentOfEnergyLeft;
            VehicleEngine.CurrentCapacity = (i_PrecentOfEnergyLeft * VehicleEngine.MaxCapacity) / 100;
        }

        public override void ChangeDetails(object i_IsDrivingRefrigeratedContents, object i_CargoVolume)
        {
            IsDrivingRefrigeratedContents = (bool)i_IsDrivingRefrigeratedContents;
            CargoVolume = (float)i_CargoVolume;
        }

        public override string ToString()
        {
            string truckDetails, isDrivingRefrigeratedContents;

            isDrivingRefrigeratedContents = IsDrivingRefrigeratedContents == true ? "is" : "isn't";
            truckDetails = string.Format(
@"The truck {0} driving dangerous things
Cargo Volume: {1}",
                isDrivingRefrigeratedContents,
                CargoVolume);

            return truckDetails;
        }
    }
}
