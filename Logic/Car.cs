namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eCarColor m_CarColor;
        private eNumOfDoors m_NumOfDoors;

        public enum eCarColor
        {
            Black,
            Blue,
            Red,
            White
        }

        public enum eNumOfDoors
        {
            Two = 2,
            Three,
            Four,
            Five
        }

        public eCarColor CarColor
        {
            get
            {
                return m_CarColor;
            }

            set
            {
                m_CarColor = value;
            }
        }

        public eNumOfDoors NumOfDoors 
        {
            get
            {
                return m_NumOfDoors;
            } 

            set
            {
                m_NumOfDoors = value;
            }
        }

        public Car(string i_ModelName, string i_LicenseNumber, float i_CurrentAirPressure, string i_ManufacturerName, Engine.eEnergyType i_EnergyType, float i_PrecentOfEnergyLeft) : base(i_ModelName, i_LicenseNumber, 4, 29f, i_CurrentAirPressure, i_ManufacturerName)
        {
            CollectionOfWheels.Capacity = 4;
            for (int i = 0; i < CollectionOfWheels.Capacity; i++)
            {
                if (CollectionOfWheels[i].CurrentAirPressure > 29f)
                {
                    throw new ValueOutOfRangeException(0, 29);
                }
            }

            if (Engine.eEnergyType.Fuel == i_EnergyType)
            {
                VehicleEngine = new FuelEngine(FuelEngine.eFuelType.Octan95)
                {
                    MaxCapacity = 48f
                };
            }
            else if (Engine.eEnergyType.Electric == i_EnergyType)
            {
                VehicleEngine = new ElectricEngine
                {
                    MaxCapacity = 2.6f
                };
            }

            PercentageOfEnergyRemaining = i_PrecentOfEnergyLeft;
            VehicleEngine.CurrentCapacity = (i_PrecentOfEnergyLeft * VehicleEngine.MaxCapacity) / 100;
        }

        public override void ChangeDetails(object i_CarColor, object i_NumberOfDoors)
        {
            CarColor = (eCarColor)i_CarColor;
            NumOfDoors = (eNumOfDoors)i_NumberOfDoors;
        }

        // $G$ CSS-027 (-3) Spaces are not kept as required after defying variables and before return statements.
        public override string ToString()
        {
            string carDetails = string.Format(
@"Car's Color: {0}
Number Of Doors: {1}",
                CarColor,
                NumOfDoors);

            return carDetails;
        }
    }
}
