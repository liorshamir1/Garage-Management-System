namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public void ChargeVehicle(float i_HoursToAdd)
        {
            if (MaxCapacity - CurrentCapacity <= i_HoursToAdd)
            {
                CurrentCapacity += i_HoursToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, MaxCapacity - CurrentCapacity);
            }
        }
    }
}
