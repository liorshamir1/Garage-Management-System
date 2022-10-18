using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaximumAirPressure;
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;

		public string ManufacturerName
		{
			get
			{
				return m_ManufacturerName;
			}

			set
			{
				m_ManufacturerName = value;
			}
		}

		public float CurrentAirPressure
		{
			get
			{
				return m_CurrentAirPressure;
			}

			set
			{
				m_CurrentAirPressure = value;
			}
		}

		public float MaximumAirPressure
		{
			get
			{
				return r_MaximumAirPressure;
			}
		}

        public Wheel(float i_MaximumAirPressure)
        {
            r_MaximumAirPressure = i_MaximumAirPressure;
        }

        public void InflationAction(float i_AirPressureToAdd)
        {
            if (MaximumAirPressure - CurrentAirPressure <= i_AirPressureToAdd)
			{
				CurrentAirPressure += i_AirPressureToAdd;
            }
            else
            {
				throw new ValueOutOfRangeException(0, MaximumAirPressure - CurrentAirPressure);
            }
        }
	}
}