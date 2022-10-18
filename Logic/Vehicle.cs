using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private string m_LicenseNumber;
        private float m_PercentageOfEnergyRemaining;
        private List<Wheel> m_CollectionOfWheels;
        private Engine m_VehicleEngine;
        private Garage.eVehicleStatusInTheGarage m_VehicleStatusInTheGarage;

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }

            set
            {
                m_LicenseNumber = value;
            }
        }

        public float PercentageOfEnergyRemaining
        {
            get
            {
                return m_PercentageOfEnergyRemaining;
            }

            set
            {
                m_PercentageOfEnergyRemaining = value;
            }
        }

        public List<Wheel> CollectionOfWheels
        {
            get
            {
                return m_CollectionOfWheels;
            }

            set
            {
                m_CollectionOfWheels = value;
            }
        }

        public Engine VehicleEngine
        {
            get
            {
                return m_VehicleEngine;
            }

            set
            {
                m_VehicleEngine = value;
            }
        }

        public Garage.eVehicleStatusInTheGarage VehicleStatusInTheGarage
        {
            get
            {
                return m_VehicleStatusInTheGarage;
            }

            set
            {
                m_VehicleStatusInTheGarage = value;
            }
        }

        public Vehicle(string i_ModelName, string i_LicenseNumber, int i_NumOfWheels, float i_MaximumAirPressure, float i_CurrentAirPressure, string i_ManufacturerName)
        {
            m_ModelName = i_ModelName;
            m_LicenseNumber = i_LicenseNumber;
            CollectionOfWheels = new List<Wheel>();
            for (int i = 0; i < i_NumOfWheels; i++)
            {
                CollectionOfWheels.Add(new Wheel(i_MaximumAirPressure));
            }

            foreach (Wheel wheel in CollectionOfWheels)
            {
                wheel.ManufacturerName = i_ManufacturerName;
                wheel.CurrentAirPressure = i_CurrentAirPressure;
            }
        }

        public abstract void ChangeDetails(object i_Param1, object i_Param2);
    }
}
