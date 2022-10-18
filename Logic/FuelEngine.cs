using System;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private readonly eFuelType r_FuelType;

        public enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        public FuelEngine(eFuelType i_FuelType)
        {
            this.r_FuelType = i_FuelType;
        }

        public void RefuelingVehicle(float i_LitersToAdd, eFuelType i_FuelType)
        {
            if (i_FuelType == FuelType)
            {
                if (MaxCapacity - CurrentCapacity <= i_LitersToAdd)
                {
                    CurrentCapacity += i_LitersToAdd;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, MaxCapacity - CurrentCapacity);
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
