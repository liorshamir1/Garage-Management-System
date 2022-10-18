using System;

namespace Ex03.GarageLogic
{
    public class VehicleOwner
    {
        private readonly string r_VehicleOwnerName;
        private readonly string r_VehicleOwnerPhone;
        private readonly Vehicle r_VehicleOfOwner;

        public string VehicleOwnerName
        {
            get
            {
                return r_VehicleOwnerName;
            }
        }

        public string VehicleOwnerPhone
        {
            get
            {
                return r_VehicleOwnerPhone;
            }
        }

        public Vehicle VehicleOfOwner
        {
            get
            {
                return r_VehicleOfOwner;
            }
        }

        public VehicleOwner(string i_VehicleOwnerName, string i_VehicleOwnerPhone, Vehicle i_VehicleOfOwner)
        {
            r_VehicleOwnerName = i_VehicleOwnerName;
            r_VehicleOwnerPhone = i_VehicleOwnerPhone;
            r_VehicleOfOwner = i_VehicleOfOwner;
        }
    }
}
