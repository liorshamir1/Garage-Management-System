namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        private float m_CurrentCapacity;
        private float m_MaxCapacity;

        public enum eEnergyType
        {
            Fuel,
            Electric
        }

        public float CurrentCapacity
        {
            get
            {
                return m_CurrentCapacity;
            }

            set
            {
                m_CurrentCapacity = value;
            }
        }

        public float MaxCapacity
        {
            get
            {
                return m_MaxCapacity;
            }

            set
            {
                m_MaxCapacity = value;
            }
        }

        public override string ToString()
        {
            string engineDetails, energyType;
            energyType = this is FuelEngine ? "Fuel" : "Electric";

            if (energyType == "Fuel")
            {
                engineDetails = string.Format(
@"Energy type: {0}
Current fuel capacity: {1}/{2}",
    energyType,
    CurrentCapacity,
    MaxCapacity);
            }
            else
            {
                engineDetails = string.Format(
@"Energy type: {0}
Current battery time capacity: {1}/{2}",
    energyType,
    CurrentCapacity,
    MaxCapacity);
            }

            return engineDetails;
        }
    }
}
