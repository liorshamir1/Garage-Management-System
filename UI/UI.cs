using System;
using System.Linq;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    // $G$ CSS-999 (-7) private method name shouldn't start with capital letter.
    public class UI
    {
        private const string k_messageLicenseNotExist = "This license doesn't exist in the garage";
        private static readonly Garage m_Garage = new Garage();

        public Garage Garage
        {
            get
            {
                return m_Garage;
            }
        }

        public static void GarageMainMenu()
        {
            int userChoice = -1;

            while (userChoice != 0)
            {
                ShowMainMenu();
                userChoice = InputBetweenRange(0, 7);
                Console.Clear();
                switch (userChoice)
                {
                    case 0:
                        break;
                    case 1:
                        AddNewVehicleToGarage();
                        break;
                    case 2:
                        ShowVehiclesInGarage();
                        break;
                    case 3:
                        ChangeVehicleStatus();
                        break;
                    case 4:
                        InflateVehicleWheelsToMaximum();
                        break;
                    case 5:
                        RefuelingVehicle();
                        break;
                    case 6:
                        ChargingVehicle();
                        break;
                    case 7:
                        ShowVehicleDetails();
                        break;
                }

                if (userChoice > 0)
                {
                    userChoice = -1;
                    Console.WriteLine("Press any key to return to the main menu");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static void ShowMainMenu()
        {
            Console.WriteLine(
@"Garage Managing Systems Main Menu
Please select an action from the list:
1. Add new vehicle to the garage.
2. Show a list of all the vehicles in the garage.
3. Change vehicle status in the garage.
4. Inflating vehicle's wheels to maximum.
5. Fueling vehicle.
6. Charging vehicle.
7. Show vehicle details.
For exit press 0.");
        }

        private static void AddNewVehicleToGarage()
        {
            int vehicleType;
            string licenseNumber, modelName, manufacturerName, ownerName, ownerPhoneNumber;
            float precentOfEnergyLeft, currentAirPressure;
            Engine.eEnergyType energyType;
            Vehicle newVehicle;

            GetVehicleTypeAndEnergyType(out vehicleType, out energyType);
            Console.Clear();
            licenseNumber = GetLicenseNumber();
            Console.Clear();
            modelName = GetModelName();
            Console.Clear();
            precentOfEnergyLeft = GetPrecentOfEnergyLeft();
            Console.Clear();
            manufacturerName = GetManufacturerName();
            Console.Clear();
            currentAirPressure = getCurrentAirPressure(vehicleType);
            try
            {
                newVehicle = GenericVehicle.VehicleCreator(licenseNumber, modelName, energyType, manufacturerName, precentOfEnergyLeft, currentAirPressure, vehicleType);
                Console.Clear();
                SetOtherDetailsInVehicle(ref newVehicle);
                Console.Clear();
                ownerName = getOwnerName();
                Console.Clear();
                ownerPhoneNumber = GetOwnerPhoneNumber();
                Console.Clear();
                m_Garage.AddNewVehicleToGarage(newVehicle, ownerName, ownerPhoneNumber);
            }
            catch (ValueOutOfRangeException i_ValueOutOfRangeException)
            {
                Console.WriteLine(i_ValueOutOfRangeException.Message);
            }
        }

        private static void ShowVehiclesInGarage()
        {
            int userChoice = 0;

            Console.WriteLine(
@"Which vehicles would you like to see?
1. All the vehicles.
2. Vehicles in repair status.
3. Vehicles that have been repaired.
4. Vehicles that have been paid.");
            userChoice = InputBetweenRange(1, 4);
            Console.WriteLine(m_Garage.ShowVehiclesInGarage(userChoice));
        }

        private static void ChangeVehicleStatus()
        {
            string licenseNumber;
            Garage.eVehicleStatusInTheGarage newStatus;

            licenseNumber = GetLicenseNumber();
            Console.Clear();
            newStatus = GetVehicleStatus();

            try
            {
                m_Garage.ChangeVehicleStatus(licenseNumber, newStatus);
                Console.WriteLine("The status of the vehicle has been changed successfully.");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(k_messageLicenseNotExist);
            }
        }

        private static void InflateVehicleWheelsToMaximum()
        {
            string licenseNumber;

            licenseNumber = GetLicenseNumber();
            Console.Clear();
            try
            {
                m_Garage.InflateWheelsToMaximum(licenseNumber);
                Console.WriteLine("The wheels were inflated successfully");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(k_messageLicenseNotExist);
            }
        }

        private static void RefuelingVehicle()
        {
            string licenseNumber;
            FuelEngine.eFuelType fuelType;
            float amountOfFuelToAdd;

            licenseNumber = GetLicenseNumber();
            if (m_Garage.VehiclesInTheGarage.ContainsKey(licenseNumber))
            {
                if (m_Garage.VehiclesInTheGarage[licenseNumber].VehicleOfOwner.VehicleEngine is FuelEngine)
                {
                    Console.Clear();
                    fuelType = GetFuelType();
                    Console.Clear();
                    amountOfFuelToAdd = GetAmountOfFuelToAdd();
                    Console.Clear();
                    try
                    {
                        m_Garage.RefuelFuelEngine(licenseNumber, fuelType, amountOfFuelToAdd);
                        Console.WriteLine("The vehicle was successfully refueled");
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine(string.Format(@"Wrong fuel type!

Please choose the correct fuel type next time:
for car choose fuel type : Octan95
for motorcycle choose fuel type : Octan98
for truck choose fuel type : Soler"));
                    }
                    catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                    {
                        Console.WriteLine(i_ValueOutOfRangeException.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Please make sure this type of engine is Fuel");
                }
            }
            else
            {
                Console.WriteLine(k_messageLicenseNotExist);
            }
        }

        private static void ChargingVehicle()
        {
            string licenseNumber;
            int amountOfTimeToCharge;

            licenseNumber = GetLicenseNumber();
            if (m_Garage.VehiclesInTheGarage.ContainsKey(licenseNumber))
            {
                if (m_Garage.VehiclesInTheGarage[licenseNumber].VehicleOfOwner.VehicleEngine is ElectricEngine)
                {
                    Console.Clear();
                    amountOfTimeToCharge = GetAmountOfTimelToCharge();
                    Console.Clear();
                    try
                    {
                        m_Garage.ChargeElectricVehicle(licenseNumber, amountOfTimeToCharge);
                        Console.WriteLine("The vehicle successfully charged");
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Something wrong");
                    }
                    catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                    {
                        Console.WriteLine(i_ValueOutOfRangeException.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Please make sure this type of engine is Electric");
                }
            }
            else
            {
                Console.WriteLine(k_messageLicenseNotExist);
            }
        }

        // $G$ CSS-027 (-3) Spaces are not kept as required after defying variables.
        private static void ShowVehicleDetails()
        {
            string licenseNumber;
            licenseNumber = GetLicenseNumber();
            Console.Clear();
            try
            {
                Console.WriteLine(m_Garage.ShowVehicleDetails(licenseNumber));
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(k_messageLicenseNotExist);
            }
        }

        private static int InputBetweenRange(int i_MinimumValue, int i_MaximumValue)
        {
            int userChoice = -1;
            bool isValidInput = true;

            Console.Write(@"
Your choice is: ");
            isValidInput = int.TryParse(Console.ReadLine(), out userChoice);
            while (!isValidInput || userChoice < i_MinimumValue || userChoice > i_MaximumValue)
            {
                Console.WriteLine("Invalid input, please enter a choice between {0} to {1}", i_MinimumValue, i_MaximumValue);
                isValidInput = int.TryParse(Console.ReadLine(), out userChoice);
            }

            return userChoice;
        }

        // $G$ CSS-027 (-3) Spaces are not kept as required before return statements.
        private static string GetModelName()
        {
            Console.Write("Please enter vehicle model name: ");
            return GetAndCheckNotEmptyInput();
        }

        // $G$ CSS-027 (-3) Spaces are not kept as required after defying variables.
        private static string GetAndCheckNotEmptyInput()
        {
            string userInput = Console.ReadLine();
            while (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Invalid input, Please enter again.");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private static string GetLicenseNumber()
        {
            string userInput;
            bool isCorrectInput;

            Console.Write("Please enter vehicle license number with 7/8 digits: ");
            userInput = Console.ReadLine();
            isCorrectInput = userInput.All(char.IsDigit);
            while (string.IsNullOrEmpty(userInput) || !isCorrectInput || (userInput.Length != 7 && userInput.Length != 8))
            {
                Console.WriteLine("Invalid input, Please enter license number with 7/8 digits.");
                userInput = Console.ReadLine();
                isCorrectInput = userInput.All(char.IsDigit);
            }

            return userInput;
        }

        private static void GetVehicleTypeAndEnergyType(out int o_VehicleType, out Engine.eEnergyType o_EnergyType)
        {
            Console.WriteLine(
    @"Please choose vehicle type:
1. Car
2. Motorcycle
3. Truck");
            o_VehicleType = InputBetweenRange(1, 3);
            if (o_VehicleType == 1 || o_VehicleType == 2)
            {
                Console.Clear();
                o_EnergyType = GetEnergyType(o_VehicleType);
            }
            else
            {
                o_EnergyType = Engine.eEnergyType.Fuel;
            }
        }

        private static Engine.eEnergyType GetEnergyType(int i_UserChoice)
        {
            Engine.eEnergyType energyType = new Engine.eEnergyType();

            Console.WriteLine(@"Please enter vehicle energy type:
1. Fuel
2. Electricity");
            i_UserChoice = InputBetweenRange(1, 2);
            switch (i_UserChoice)
            {
                case 1:
                    energyType = Engine.eEnergyType.Fuel;
                    break;
                case 2:
                    energyType = Engine.eEnergyType.Electric;
                    break;
            }

            return energyType;
        }

        private static float GetPrecentOfEnergyLeft()
        {
            float percentOfEnergyLeft;

            Console.Write("Please enter current energy in percent: ");
            percentOfEnergyLeft = CheckPrecentOfEnergyLeft();
            while (percentOfEnergyLeft < 0 || percentOfEnergyLeft > 100)
            {
                Console.WriteLine("Invalid input, the input must be between 0 to 100");
                percentOfEnergyLeft = CheckPrecentOfEnergyLeft();
            }

            return percentOfEnergyLeft;
        }

        private static float CheckPrecentOfEnergyLeft()
        {
            bool checkInput = true;
            float userInput = 0;

            checkInput = float.TryParse(Console.ReadLine(), out userInput);
            while (checkInput != true)
            {
                Console.WriteLine(string.Format(@"Invalid input, please enter again.
Enter percent between 0-100"));
                checkInput = float.TryParse(Console.ReadLine(), out userInput);
            }

            return userInput;
        }

        private static float GetAndCheckCurrentAitPressure(int i_VehicleKind)
        {
            bool checkInput = true;
            float userInput = 0;

            checkInput = float.TryParse(Console.ReadLine(), out userInput);
            while (!checkInput || !IsInTheRange(i_VehicleKind, userInput))
            {
                Console.WriteLine(string.Format(
@"Invalid input, please enter again
if it's Car the valid range is: 0-29
if it's Motorcycle the valid range is: 0-30
if it's Truck the valid range is: 0-25"));
                checkInput = float.TryParse(Console.ReadLine(), out userInput);
            }

            return userInput;
        }

        // $G$ CSS-027 (-3) Spaces are not kept as required before return statements.
        private static string GetManufacturerName()
        {
            Console.Write("Please enter wheels manufacturer name: ");
            return GetAndCheckNotEmptyInput();
        }

        // $G$ CSS-027 (-3) Spaces are not kept as required before return statements.
        private static float getCurrentAirPressure(int i_VehicleKind)
        {
            Console.Write("Please enter current air pressure in the wheels: ");
            return GetAndCheckCurrentAitPressure(i_VehicleKind);
        }

        private static void SetOtherDetailsInVehicle(ref Vehicle i_NewVehicle)
        {
            Car.eCarColor carColor;
            Car.eNumOfDoors numberOfDoors;
            Motorcycle.eLicenseType licenseType;
            int engineCapacity;
            bool isDriveingDangerousThings;
            float o_CargoVolume;

            if (i_NewVehicle is Car)
            {
                InputCarDetails(out carColor, out numberOfDoors);
                i_NewVehicle.ChangeDetails(carColor, numberOfDoors);
            }
            else if (i_NewVehicle is Motorcycle)
            {
                InputMotorcycleDetails(out licenseType, out engineCapacity);
                i_NewVehicle.ChangeDetails(licenseType, engineCapacity);
            }
            else if (i_NewVehicle is Truck)
            {
                InputTruckDetails(out isDriveingDangerousThings, out o_CargoVolume);
                i_NewVehicle.ChangeDetails(isDriveingDangerousThings, o_CargoVolume);
            }
        }

        private static void InputCarDetails(out Car.eCarColor o_CarColor, out Car.eNumOfDoors o_NumberOfDoors)
        {
            int userChoice = 0;

            o_CarColor = 0;
            o_NumberOfDoors = 0;
            Console.WriteLine(@"Please enter the color of the car:
1. Black,
2. Blue,
3. Red,
4. White");
            userChoice = InputBetweenRange(1, 4);
            switch (userChoice)
            {
                case 1:
                    o_CarColor = Car.eCarColor.Black;
                    break;
                case 2:
                    o_CarColor = Car.eCarColor.Blue;
                    break;
                case 3:
                    o_CarColor = Car.eCarColor.Red;
                    break;
                case 4:
                    o_CarColor = Car.eCarColor.White;
                    break;
            }

            Console.Clear();
            Console.Write("Please enter number of doors from 2 to 5");
            userChoice = InputBetweenRange(2, 5);
            switch (userChoice)
            {
                case 2:
                    o_NumberOfDoors = Car.eNumOfDoors.Two;
                    break;
                case 3:
                    o_NumberOfDoors = Car.eNumOfDoors.Three;
                    break;
                case 4:
                    o_NumberOfDoors = Car.eNumOfDoors.Four;
                    break;
                case 5:
                    o_NumberOfDoors = Car.eNumOfDoors.Five;
                    break;
            }
        }

        private static void InputMotorcycleDetails(out Motorcycle.eLicenseType o_LicenseType, out int o_EngineCapacity)
        {
            int userChoice = 0;

            o_LicenseType = 0;
            o_EngineCapacity = 0;
            Console.WriteLine(@"Please enter license type:
1. A
2. AA
3. A2
4. B");
            userChoice = InputBetweenRange(1, 4);
            switch (userChoice)
            {
                case 1:
                    o_LicenseType = Motorcycle.eLicenseType.A;
                    break;
                case 2:
                    o_LicenseType = Motorcycle.eLicenseType.AA;
                    break;
                case 3:
                    o_LicenseType = Motorcycle.eLicenseType.A2;
                    break;
                case 4:
                    o_LicenseType = Motorcycle.eLicenseType.B;
                    break;
            }

            Console.Clear();
            o_EngineCapacity = GetEngineCapacity();
        }

        private static void InputTruckDetails(out bool o_IsDrivingRefrigeratedContents, out float o_CargoVolume)
        {
            int userChoice = 0;

            o_IsDrivingRefrigeratedContents = false;
            o_CargoVolume = 0;
            Console.WriteLine(string.Format(
@"Does the truck drive refrigerated contents?
1. yes
2. no"));
            userChoice = InputBetweenRange(1, 2);
            switch (userChoice)
            {
                case 1:
                    o_IsDrivingRefrigeratedContents = true;
                    break;
                case 2:
                    o_IsDrivingRefrigeratedContents = false;
                    break;
            }

            Console.Clear();
            o_CargoVolume = GetVolumeOfCargo();
        }

        private static string getOwnerName()
        {
            string ownerName;

            Console.Write("Please enter your first name: ");
            ownerName = Console.ReadLine();
            while (!CheckOwnerNameInput(ownerName))
            {
                Console.WriteLine("Wrong input, Please enter only letters.");
                ownerName = Console.ReadLine();
            }

            return ownerName;
        }

        private static bool CheckOwnerNameInput(string i_OwnerName)
        {
            bool isCorrectInput = false;

            if (!string.IsNullOrEmpty(i_OwnerName))
            {
                isCorrectInput = i_OwnerName.All(char.IsLetter);
            }

            return isCorrectInput;
        }

        private static string GetOwnerPhoneNumber()
        {
            string ownerPhoneNumber;

            Console.WriteLine("Please enter your phone number in a format of 10 digits:");
            ownerPhoneNumber = Console.ReadLine();
            while (!CheckOwnerPhoneNumberInput(ownerPhoneNumber))
            {
                Console.WriteLine("Wrong input, Please enter a number 10 digits long.");
                ownerPhoneNumber = Console.ReadLine();
            }

            return ownerPhoneNumber;
        }

        private static bool CheckOwnerPhoneNumberInput(string i_PhoneNumber)
        {
            bool isCorrectInput = i_PhoneNumber.All(char.IsDigit);

            if (i_PhoneNumber.Length != 10)
            {
                isCorrectInput = false;
            }

            return isCorrectInput;
        }

        private static bool IsInTheRange(int i_VehicleKind, float i_AirPressureToCheck)
        {
            bool inRange = false;

            switch (i_VehicleKind)
            {
                case 1:
                    inRange = i_AirPressureToCheck > 29f || i_AirPressureToCheck < 0 ? false : true;
                    break;

                case 2:
                    inRange = i_AirPressureToCheck > 30f || i_AirPressureToCheck < 0 ? false : true;
                    break;

                case 3:
                    inRange = i_AirPressureToCheck > 25f || i_AirPressureToCheck < 0 ? false : true;
                    break;
            }

            return inRange;
        }

        // $G$ CSS-027 (-3) Spaces are not kept as required after defying variables.
        private static Garage.eVehicleStatusInTheGarage GetVehicleStatus()
        {
            int userChoice = 0;
            Garage.eVehicleStatusInTheGarage newStatus = 0;

            Console.WriteLine(
                @"Now choose the new status:
1. In Repair
2. Fixed
3. Paid");
            userChoice = InputBetweenRange(1, 3);
            switch (userChoice)
            {
                case 1:
                    newStatus = Garage.eVehicleStatusInTheGarage.InRepair;
                    break;
                case 2:
                    newStatus = Garage.eVehicleStatusInTheGarage.Fixed;
                    break;
                case 3:
                    newStatus = Garage.eVehicleStatusInTheGarage.Paid;
                    break;
            }

            return newStatus;
        }

        private static FuelEngine.eFuelType GetFuelType()
        {
            int userChoice = 0;
            FuelEngine.eFuelType fuelType = 0;

            Console.WriteLine(string.Format(
@"Now please choose the fuel type:
1. Soler
2. Octan95
3. Octan96
4. Octan98"));
            userChoice = InputBetweenRange(1, 4);
            switch (userChoice)
            {
                case 1:
                    fuelType = FuelEngine.eFuelType.Soler;
                    break;
                case 2:
                    fuelType = FuelEngine.eFuelType.Octan95;
                    break;
                case 3:
                    fuelType = FuelEngine.eFuelType.Octan96;
                    break;
                case 4:
                    fuelType = FuelEngine.eFuelType.Octan98;
                    break;
            }

            return fuelType;
        }

        private static float GetAmountOfFuelToAdd()
        {
            float amountOfFuelToAdd;
            bool isValidInput = true;

            Console.Write("Please enter how many liters you wish to refuel: ");
            isValidInput = float.TryParse(Console.ReadLine(), out amountOfFuelToAdd);
            while (amountOfFuelToAdd < 0)
            {
                Console.WriteLine("Invalid input, the input must be bigger than 0");
                isValidInput = float.TryParse(Console.ReadLine(), out amountOfFuelToAdd);
            }

            return amountOfFuelToAdd;
        }

        private static int GetAmountOfTimelToCharge()
        {
            int amountOftimeToCharge;
            bool isValidInput = true;

            Console.Write("Please enter how many minutes you wish to charge: ");
            isValidInput = int.TryParse(Console.ReadLine(), out amountOftimeToCharge);
            while (amountOftimeToCharge < 0)
            {
                Console.WriteLine("Invalid input, the input must be bigger than 0");
                isValidInput = int.TryParse(Console.ReadLine(), out amountOftimeToCharge);
            }

            return amountOftimeToCharge;
        }

        private static float GetVolumeOfCargo()
        {
            bool isValidInput;
            float cargoVolume;

            Console.Write("Please enter the volume of the cargo: ");
            isValidInput = float.TryParse(Console.ReadLine(), out cargoVolume);
            while (!isValidInput)
            {
                Console.WriteLine("Invalid input, the input must be a number");
                isValidInput = float.TryParse(Console.ReadLine(), out cargoVolume);
            }

            return cargoVolume;
        }

        private static int GetEngineCapacity()
        {
            bool isValidInput;
            int engineCapacity = 0;

            Console.Write("Please enter engine capacity: ");
            isValidInput = int.TryParse(Console.ReadLine(), out engineCapacity);
            while (!isValidInput)
            {
                Console.WriteLine("Invalid input, the input must be an integer");
                isValidInput = int.TryParse(Console.ReadLine(), out engineCapacity);
            }

            return engineCapacity;
        }
    }
}
