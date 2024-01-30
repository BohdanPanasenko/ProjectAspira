using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Formats.Asn1;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Sources;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
//File.AppendText("..\\..\\..\\1.txt");
string pathToFileDRIVERS = "..\\..\\..\\drivers.txt";
//string pathToFileDRIVERS = "D:\\ProjectAspira\\drivers.txt";
List<Drivers> allDrivers = ReadDriversFromFile(pathToFileDRIVERS);

string pathToFileCLIENTS = "..\\..\\..\\clients.txt";
//string pathToFileCLIENTS = "D:\\ProjectAspira\\clients.txt";
List<Clients> allClients = ReadClientsFromFile(pathToFileCLIENTS);

string pathToFileRIDES = "..\\..\\..\\rides.txt";
//string pathToFileRIDES = "D:\\ProjectAspira\\rides.txt";
List<Rides> allRides = ReadRidesFromFile(pathToFileRIDES);

Console.WriteLine("Welcome!");
DateTime data = DateTime.Now;
string currentDate = Convert.ToString(data);
currentDate = currentDate.Substring(0, 10);
Console.WriteLine(currentDate);

bool shouldWork = true;
while (shouldWork)
{
    try
    {
        Console.Write("\nYour menu:\n" +
    "\t1. Drivers' data\n" +
    "\t2. Clients' data\n" +
    "\t3. Rides' data\n" +
    "\t4. Additional operations\n" +
    "\t5. End.\n" +
            "\nWrite down your option: "
    );
        int mainoption = Convert.ToInt32(Console.ReadLine());
        switch (mainoption)
        {
            case 1:
                bool menu1 = true;
                while (menu1)
                {
                    try
                    {

                        Console.Write("\n1. Print all drivers\n" +
                                      "2. Enter a new driver\n" +
                                      "3. Delete the driver\n" +
                                      "4. Update driver\n" +
                                      "5. Return to the original menu\n" +
                                      "6. End\n" +
                                            "\nWrite down your option: "
                                          );
                        int option1 = Convert.ToInt32(Console.ReadLine());
                        switch (option1)
                        {
                            case 1:
                                PrintDrivers(allDrivers);
                                break;
                            case 2:
                                allDrivers.Add(EnterDrivers());
                                SaveDriversToFile(pathToFileDRIVERS, allDrivers);
                                break;
                            case 3:
                                PrintDrivers(allDrivers);
                                DeleteDrives(allDrivers);
                                SaveDriversToFile(pathToFileDRIVERS, allDrivers);
                                break;
                            case 4:
                                PrintDrivers(allDrivers);
                                int id = 0; string newName; int newAge = 0; string newType = "";
                                Console.Write("\nEnter an ID of a driver to change: \n");
                                try
                                {
                                    id = Convert.ToInt32(Console.ReadLine());
                                    if (!driverMatch(id))
                                    {
                                        Console.WriteLine("There is no such a driver");
                                        break;
                                    }
                                }
                                catch (Exception e) { Console.WriteLine("Invalid input"); break; }

                                Console.Write("Enter a new name: ");
                                newName = Console.ReadLine();

                                Console.Write("Enter an age of a driver: ");
                                bool boolwork = true;
                                while (boolwork)
                                {
                                    try
                                    {
                                        newAge = Convert.ToInt32(Console.ReadLine());
                                        boolwork = false;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Incorrect input. Please enter a valid integer.");
                                    }
                                }


                                int typeoption = 0;
                                bool validInput = true;
                                while (validInput)
                                {
                                    Console.Write("Enter a type that this driver provides " +
                                        "\n(Enter '1' if it's UberX" +
                                        ". Enter '2' if it's Comfort" +
                                        ". Enter '3' if it's Van): ");
                                    try
                                    {
                                        typeoption = Convert.ToInt32(Console.ReadLine());
                                        switch (typeoption)
                                        {
                                            case 1:
                                                newType = "UberX";
                                                validInput = false;
                                                break;
                                            case 2:
                                                newType = "Comfort";
                                                validInput = false;
                                                break;
                                            case 3:
                                                newType = "Van";
                                                validInput = false;
                                                break;
                                            default:
                                                Console.WriteLine("Oops! Something has gone wrong. Probably, you typed a wrong number. Please, try again.\n");
                                                break;
                                        }
                                    }
                                    catch (Exception e) { Console.WriteLine("Invalid input"); }
                                }
                                UpdateDriver(id, newName, newAge, newType);
                                break;
                            case 5:
                                menu1 = false;
                                break;
                            case 6:
                                shouldWork = false;
                                menu1 = false;
                                Console.WriteLine("The end of the programm.");
                                break;
                            default:
                                Console.WriteLine("Oops! Somethis has gone wrong. Probably, you typed a wrong number. Please, try again.");
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("invalido inputo");
                    }
                }
                break;
            case 2:
                bool menu2 = true;
                while (menu2)
                {
                    try
                    {

                        Console.Write("\n1. Print all clients\n" +
                                      "2. Enter a new client\n" +
                                      "3. Delete the client\n" +
                                      "4. Update the client\n" +
                                      "5. Return to the original menu\n" +
                                      "6. End\n" +
                                            "\nWrite down your option: "
                                          );
                        int option2 = Convert.ToInt32(Console.ReadLine());
                        switch (option2)
                        {
                            case 1:
                                PrintClients(allClients);
                                break;
                            case 2:
                                allClients.Add(EnterClients());
                                SaveClientsToFile(pathToFileCLIENTS, allClients);
                                break;
                            case 3:
                                PrintClients(allClients);
                                DeleteClients(allClients);
                                SaveClientsToFile(pathToFileCLIENTS, allClients);
                                break;
                            case 4:
                                PrintClients(allClients);
                                int id = 0; string newName; int newAge = 0;
                                Console.Write("\nEnter an ID of a client to change: \n");
                                try
                                {
                                    id = Convert.ToInt32(Console.ReadLine());
                                    if (!clientMatch(id))
                                    {
                                        Console.WriteLine("There is no such a client");
                                        break;
                                    }
                                }
                                catch (Exception e) { Console.WriteLine("Invalid input"); break; }

                                Console.Write("Enter a new name: ");
                                newName = Console.ReadLine();

                                Console.Write("Enter an age of the client: ");
                                bool boolwork = true;
                                while (boolwork)
                                {
                                    try
                                    {
                                        newAge = Convert.ToInt32(Console.ReadLine());
                                        boolwork = false;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Incorrect input. Please enter a valid integer.");
                                    }
                                }

                                UpdateClient(id, newName, newAge);
                                break;
                            case 5:
                                menu2 = false;
                                break;
                            case 6:
                                shouldWork = false;
                                menu2 = false;
                                Console.WriteLine("The end of the programm.");
                                break;
                            default:
                                Console.WriteLine("Oops! Somethis has gone wrong. Probably, you typed a wrong number. Please, try again");
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("invalido inputo");
                    }
                }
                break;
            case 3:
                bool menu3 = true;
                while (menu3)
                {
                    try
                    {
                        Console.Write("\n1. Print all rides\n" +
                                      "2. Enter a new ride\n" +
                                      "3. Delete the ride\n" +
                                      "4. Update the ride\n" +
                                      "5. Return to the original menu\n" +
                                      "6. End\n" +
                                            "\nWrite down your option: "
                                          );
                        int option3 = Convert.ToInt32(Console.ReadLine());
                        switch (option3)
                        {
                            case 1:
                                PrintRides(allRides);
                                break;
                            case 2:
                                EnterRides(allDrivers, allClients);
                                SaveRidesToFile(pathToFileRIDES, allRides);
                                break;
                            case 3:
                                PrintRides(allRides);
                                DeleteRides(allRides, allDrivers, allClients);
                                SaveRidesToFile(pathToFileRIDES, allRides);
                                break;
                            case 4:
                                UpdateRide(allRides, allDrivers, allClients);
                                break;
                            case 5:
                                menu3 = false;
                                break;
                            case 6:
                                shouldWork = false;
                                menu3 = false;
                                Console.WriteLine("The end of the programm.");
                                break;
                            default:
                                Console.WriteLine("Oops! Somethis has gone wrong. Probably, you typed a wrong number. Please, try again.");
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("invalido inputo");
                    }
                }
                break;

            case 4:
                bool menu4 = true;
                while (menu4)
                {
                    try
                    {
                        Console.Write("\n1. Print all drivers with rating <2.0\n" +
                                      "2. Print all drivers and clients with rating more or equal to 4.5\n" +
                                      "3. Print the service category that used the most among all\n" +
                                      "4. Print driver(s) with the biggest number of rides\n" +
                                      "5. Average of ratings that drivers and clients got in rides\n" +
                                      "6. Find the youngest and the oldest drivers,\nhave a list of all drivers in a sequence and get an average age\n" +
                                      "7. Counter for using cities in Uber system\n" +
                                      "8. Return to the original menu\n" +
                                      "9. End\n" +
                                            "\nWrite down your option: "
                                          );
                        int option4 = Convert.ToInt32(Console.ReadLine());
                        switch (option4)
                        {
                            case 1:
                                LowRatedDrivers(allDrivers);
                                break;
                            case 2:
                                HighRatedDriversAndClients(allDrivers, allClients);
                                break;
                            case 3:
                                MostUsedCarType(allDrivers);
                                break;
                            case 4:
                                DriversWithMostRides(allRides, allDrivers);
                                break;
                            case 5:
                                CalculateAverageScoreOfDrivers(allRides);
                                CalculateAverageScoreOfClients(allRides);
                                break;
                            case 6:
                                YoungestDriver(allDrivers);
                                OldestDriver(allDrivers);
                                Console.WriteLine("\nAll drivers from the oldest one to the youngest one:");
                                YoungestToOldestDrivers(allDrivers);
                                break;
                            case 7:
                                CityCounter(allRides);
                                break;
                            case 8:
                                menu4 = false;
                                break;
                            case 9:
                                shouldWork = false;
                                menu4 = false;
                                Console.WriteLine("The end of the programm.");
                                break;
                            default:
                                Console.WriteLine("Oops! Somethis has gone wrong. Probably, you typed a wrong number. Please, try again.");
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("invalido inputo");
                    }
                }
                break;
            case 5:
                shouldWork = false;
                Console.WriteLine("The end of the programm.");
                break;

            default:
                Console.WriteLine("Oops! Somethis has gone wrong. Probably, you typed a wrong number. Please, try again.");
                break;
        }
    }
    catch (Exception e) { Console.WriteLine("invalido inputo"); }
}
void CityCounter(List<Rides> rides)
{
    Dictionary<string, int> cityCounts = new Dictionary<string, int>();

    foreach (Rides ride in rides)
    {
        if (!cityCounts.ContainsKey(ride.cityDeparture))
        {
            cityCounts[ride.cityDeparture] = 1;
        }
        else
        {
            cityCounts[ride.cityDeparture]++;
        }

        if (!cityCounts.ContainsKey(ride.cityArrival))
        {
            cityCounts[ride.cityArrival] = 1;
        }
        else
        {
            cityCounts[ride.cityArrival]++;
        }
    }

    Console.WriteLine("City Counts:");
    foreach (var cityCount in cityCounts)
    {
        Console.WriteLine("{0}: {1}", cityCount.Key, cityCount.Value);
    }
}

void YoungestToOldestDrivers(List<Drivers> drivers)
{
    int CompareDriversByAge(Drivers driver1, Drivers driver2)
    {
        return driver1.ageDriver.CompareTo(driver2.ageDriver);
    }
    Console.WriteLine("\nDrivers sorted from younger to the older:");
    drivers.Sort(CompareDriversByAge);

    foreach (Drivers driver in drivers)
        Console.WriteLine("Driver: {0} (ID: {1}); Age: {2}", driver.nameDriver, driver.id, driver.ageDriver);

    int counter = 0; int sum = 0;
    foreach (Drivers driver in drivers)
    {
        sum += driver.ageDriver;
        counter++;
    }
    float average = (float)sum / counter;
    decimal roundAverage = Math.Round((decimal)average);
    Console.WriteLine("\nAverage age (with rounding to a whole) is: " + roundAverage);
}
void YoungestDriver(List<Drivers> drivers)
{
    if (drivers.Count == 0)
    {
        Console.WriteLine("No drivers");
        return;
    }

    Drivers youngestDriver = drivers[0]; // we say the first driver is the youngest and then we start a loop where we look for the younger one

    foreach (Drivers driver in drivers)
    {
        if (driver.ageDriver < youngestDriver.ageDriver)
        {
            youngestDriver = driver;
        }
    }

    Console.WriteLine("Youngest driver: {0} (ID: {1}, Age: {2})", youngestDriver.nameDriver, youngestDriver.id, youngestDriver.ageDriver);
}

void OldestDriver(List<Drivers> drivers)
{
    if (drivers.Count == 0)
    {
        Console.WriteLine("No drivers");
        return;
    }

    Drivers oldestDriver = drivers[0];

    foreach (Drivers driver in drivers)
    {
        if (driver.ageDriver > oldestDriver.ageDriver)
        {
            oldestDriver = driver;
        }
    }
    Console.WriteLine("Oldest driver: {0} (ID: {1}, Age: {2})", oldestDriver.nameDriver, oldestDriver.id, oldestDriver.ageDriver);
}
float CalculateAverageScoreOfDrivers(List<Rides> rides)
{
    if (rides == null || rides.Count == 0)
    {
        Console.WriteLine("No rides bruh");
        return 0.0f;
    }
    float totalScore = 0;
    foreach (Rides ride in rides)
    {
        totalScore += ride.scoreDriver;
    }
    float averageScore = (float)totalScore / rides.Count;
    Console.WriteLine("The average for drivers is " + averageScore);
    return averageScore;
}
float CalculateAverageScoreOfClients(List<Rides> rides)
{
    if (rides == null || rides.Count == 0)
    {
        Console.WriteLine("No rides bruh");
        return 0.0f;
    }
    float totalScore = 0;
    foreach (Rides ride in rides)
    {
        totalScore += ride.scoreClient;
    }
    float averageScore = (float)totalScore / rides.Count;
    Console.WriteLine("The average for clients is " + averageScore);
    return averageScore;
}
void DriversWithMostRides(List<Rides> rides, List<Drivers> drivers)
{
    int maxRides = 0;
    List<String> driversNames = new List<String>();
    List<int> driversIDs = new List<int>();
    foreach (Drivers driver in drivers)
    {
        int ridesCount = 0;
        foreach (Rides ride in rides)
        {
            if (ride.idD == driver.id)
            {
                ridesCount++;
            }
        }

        if (ridesCount >= maxRides)
        {
            maxRides = ridesCount;
            driversNames.Add(driver.nameDriver);
            driversIDs.Add(driver.id);
        }
    }

    Console.WriteLine("\nDriver with the most rides ({0} ride[s]):", maxRides);
    for (int i = 0; i < driversNames.Count; i++)
    {
        if (maxRides > 0)
            Console.WriteLine("› {0} (ID: {1})", driversNames[i], driversIDs[i]);
    }
}

void MostUsedCarType(List<Drivers> drivers)
{
    int uberxCount = 0; int comfortCount = 0; int vanCount = 0;
    foreach (Drivers driver in drivers)
    {
        switch (driver.typeDriver)
        {
            case "UberX":
                uberxCount++;
                break;
            case "Comfort":
                comfortCount++;
                break;
            case "Van":
                vanCount++;
                break;
        }
    }
    string mostUsedCarType = "";

    if (uberxCount > comfortCount && uberxCount > vanCount)
        mostUsedCarType = "UberX";
    else if (comfortCount > vanCount && comfortCount > uberxCount)
        mostUsedCarType = "Comfort";
    else if (vanCount > uberxCount && vanCount > comfortCount)
        mostUsedCarType = "Van";
    else if (uberxCount == comfortCount && uberxCount > vanCount)
        mostUsedCarType = "uberX and Comfort";
    else if (uberxCount == vanCount && uberxCount > comfortCount)
        mostUsedCarType = "uberX and Van";
    else if (vanCount == comfortCount && vanCount > uberxCount)
        mostUsedCarType = "Van and Comfort";
    else if (vanCount == comfortCount || vanCount == uberxCount)
        mostUsedCarType = "All 3 types have equal counter";

    if (mostUsedCarType != null && mostUsedCarType != "")
        Console.WriteLine("\nMost Used Car Type: " + mostUsedCarType);
    else
        Console.WriteLine("\nNo rides");
}
void HighRatedDriversAndClients(List<Drivers> drivers, List<Clients> clients)
{
    Console.WriteLine("\nDrivers with rating more than/equal to 4.5:");

    bool anyHighRatedDriversMoreThan = false; //just in case if there are no drivers
    foreach (Drivers driver in drivers)
    {
        if (driver.ratingDriver >= 4.5f)
        {
            Console.WriteLine("— Driver: {0} (ID: {1}), Rating: {2}", driver.nameDriver, driver.id, driver.ratingDriver);
            anyHighRatedDriversMoreThan = true;
        }
    }
    if (anyHighRatedDriversMoreThan == false)
        Console.WriteLine("No drivers");

    Console.WriteLine("\nClients with rating more than/equal to 4.5:");

    bool anyHighRatedClientsMoreThan = false;
    foreach (Clients client in clients)
    {
        if (client.ratingClient >= 4.5f)
        {
            Console.WriteLine("— Client: {0} (ID: {1}), Rating: {2}", client.nameClient, client.id, client.ratingClient);
            anyHighRatedClientsMoreThan = true;
        }
    }
    if (anyHighRatedClientsMoreThan == false)
        Console.WriteLine("No clients");
}

void LowRatedDrivers(List<Drivers> drivers)
{
    Console.WriteLine("\nDrivers with rating less than 2.0:");
    bool allLowRatedDriversMoreThan = false;
    foreach (Drivers driver in drivers)
    {
        if (driver.ratingDriver < 2.0f)
        {
            Console.WriteLine("{0}, ID: {2}. Rating: {1}", driver.nameDriver, driver.ratingDriver, driver.id);
            allLowRatedDriversMoreThan = true;
        }
    }
    if (allLowRatedDriversMoreThan == false)
        Console.WriteLine("Bruh");
}
void UpdateRide(List<Rides> rides, List<Drivers> drivers, List<Clients> clients)
{
    PrintRides(rides);
    int idInput = 0;
    Console.Write("\n\nPlease, enter an ID of the ride that you would like to update: ");
    try
    {
        idInput = Convert.ToInt32(Console.ReadLine());
    }
    catch (Exception ex) { Console.WriteLine("No ride"); return; }
    foreach (Rides ride in rides)
    {
        if (ride.rideID == idInput)
        {
            Console.WriteLine("\nUpdating driver information:");
            PrintDrivers(drivers);
            int newDriverId = 0;
            Console.Write("Enter the ID of the (new) driver: ");
            bool whilework = true;
            while (whilework)
            {
                try
                {
                    newDriverId = Convert.ToInt32(Console.ReadLine());

                    if (driverFound(drivers, newDriverId, out string newDriverName))
                    {
                        ride.idD = newDriverId;
                        ride.nameDriver = newDriverName;
                        whilework = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid driver ID. Ride will not be updated.");
                    }
                }
                catch (Exception e) { Console.WriteLine("inputo invalido"); }
            }

            Console.WriteLine("\nUpdating client information:");
            PrintClients(clients);
            int newClientId = 0;
            Console.Write("Enter the ID of the (new) client: ");
            bool whilework2 = true;
            while (whilework2)
            {
                try
                {
                    newClientId = Convert.ToInt32(Console.ReadLine());

                    if (clientFound(clients, newClientId, out string newClientName))
                    {
                        ride.idC = newClientId;
                        ride.nameClient = newClientName;
                        whilework2 = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid client ID. Ride will not be updated.");

                    }
                }
                catch (Exception e) { Console.WriteLine("inputo invalido"); }
            }
            int scoreD = 0; int scoreC = 0;
            bool flag = true;
            while (flag)
            {
                try
                {
                    Console.Write("Enter a score that client gave to his/her DRIVER (0-5): ");
                    ride.scoreDriver = Convert.ToInt32(Console.ReadLine());

                    if (ride.scoreDriver < 0 || ride.scoreDriver > 5)
                        Console.WriteLine("Invalid score for the driver. Please enter a number between 0 and 5 (included).");

                    else
                    {
                        Console.Write("Enter a score that driver gave to his/her CLIENT (0-5): ");
                        ride.scoreClient = Convert.ToInt32(Console.ReadLine());

                        if (ride.scoreClient < 0 || ride.scoreClient > 5)
                            Console.WriteLine("Invalid score for the client. Please enter a number between 0 and 5 (included).");
                        else
                        {
                            flag = false;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Incorrect input. Try again.");
                }
            }
            Console.Write("Enter a city of departure: ");
            string cityD = Console.ReadLine();
            cityD = cityD.ToUpper();
            ride.cityDeparture = cityD;
            Console.Write("Enter a city of arrival: ");
            string cityA = Console.ReadLine();
            cityA = cityA.ToUpper();
            ride.cityArrival = cityA;

            UpdateDriverRating(rides, drivers, newDriverId);
            UpdateClientRating(rides, clients, newClientId);

            SaveRidesToFile(pathToFileRIDES, rides);
            SaveDriversToFile(pathToFileDRIVERS, drivers);
            SaveClientsToFile(pathToFileCLIENTS, clients);

            Console.WriteLine("Ride with ID#{0} has been successfully updated.", idInput);
            return;
        }
    }
    Console.WriteLine("Ride with ID#{0} not found.", idInput);
}

void UpdateDriver(int id, string newName, int newAge, string newType)
{
    List<Drivers> drivers = ReadDriversFromFile(pathToFileDRIVERS); //put drivers to a list
    bool b = false;
    for (int i = 0; i < drivers.Count; i++)
    {
        if (drivers[i].id == id)
        {
            string oldName = drivers[i].nameDriver; // when we change the name of the driver it should be changed in the file of Rides as well

            drivers[i].nameDriver = newName;
            drivers[i].ageDriver = newAge;
            drivers[i].typeDriver = newType;
            b = true;

            UpdateDriverNameInRides(allRides, oldName, newName);
            break;
        }
    }
    if (!b)
        return;
    SaveDriversToFile(pathToFileDRIVERS, drivers);
    SaveRidesToFile(pathToFileRIDES, allRides);
    allDrivers = ReadDriversFromFile(pathToFileDRIVERS); // Read the drivers from the file again and update the 'allDrivers' variable
}

void UpdateDriverNameInRides(List<Rides> rides, string oldName, string newName)
{
    foreach (Rides ride in rides)
        if (ride.nameDriver == oldName)
            ride.nameDriver = newName;
}

void UpdateClient(int id, string newName, int newAge)
{
    List<Clients> clients = ReadClientsFromFile(pathToFileCLIENTS);
    bool b = false;
    for (int i = 0; i < clients.Count; i++)
        if (clients[i].id == id)
        {
            clients[i].nameClient = newName;
            clients[i].ageClient = newAge;
            b = true;
            break;
        }
    if (!b)
        return;
    SaveClientsToFile(pathToFileCLIENTS, clients);
    allClients = ReadClientsFromFile(pathToFileCLIENTS);
    return;
}
void SaveDriversToFile(string destinationFilePath, List<Drivers> drivers)
{
    List<string> lines = new List<string>();
    foreach (Drivers driver in drivers)
    {
        string line = String.Format("{0};{1};{2};{3};{4}", driver.id, driver.nameDriver, driver.ageDriver, driver.ratingDriver, driver.typeDriver);
        lines.Add(line);
    }
    File.WriteAllLines(destinationFilePath, lines);
}
void SaveClientsToFile(string destinationFilePath, List<Clients> clients)
{
    List<string> lines = new List<string>();
    foreach (Clients client in clients)
    {
        string line = String.Format("{0};{1};{2};{3}", client.id, client.nameClient, client.ageClient, client.ratingClient);
        lines.Add(line);
    }
    File.WriteAllLines(destinationFilePath, lines);
}
void SaveRidesToFile(string destinationFilePath, List<Rides> rides)
{
    List<string> lines = new List<string>();
    foreach (Rides ride in rides)
    {
        string line = String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}", ride.rideID, ride.nameDriver, ride.idD, ride.nameClient, ride.idC, ride.scoreDriver, ride.scoreClient, ride.cityDeparture, ride.cityArrival);
        lines.Add(line);
    }
    File.WriteAllLines(destinationFilePath, lines);
}
void DeleteClients(List<Clients> clients)
{
    int idInput = 0;
    Console.Write("Please, enter an ID of the client that you would like to delete: ");
    try
    {
        idInput = Convert.ToInt32(Console.ReadLine());
        foreach (Clients client in clients)
        {
            if (client.id == idInput)
            {
                Console.WriteLine("We are deleting {0}#{1}", client.nameClient, client.id);
                clients.Remove(client);
                return;
            }
        }
        Console.WriteLine("The client with ID#{0} isn't found", idInput);
    }
    catch (Exception ex) { }
}
void DeleteDrives(List<Drivers> drivers)
{
    int idInput = 0;
    Console.Write("Please, enter an ID of the driver that you would like to delete: ");
    try
    {
        idInput = Convert.ToInt32(Console.ReadLine());

        foreach (Drivers driver in drivers)
        {
            if (driver.id == idInput)
            {
                Console.WriteLine("We are deleting {0}#{1}", driver.nameDriver, driver.id);
                drivers.Remove(driver);
                break;
            }
        }
        Console.WriteLine("Driver with ID#{0 isn't found}", idInput);
    }
    catch (Exception ex) { }
}

void DeleteRides(List<Rides> rides, List<Drivers> drivers, List<Clients> clients)
{
    int idInput = 0;
    Console.Write("Please, enter an ID of the ride that you would like to delete: ");
    try
    {
        idInput = Convert.ToInt32(Console.ReadLine());
        foreach (Rides ride in rides)
        {
            if (ride.rideID == idInput)
            {
                Console.WriteLine("We are deleting ride with ID#{0}...\n" +
                                  "The name of the driver: {1}.\n" +
                                  "The name of the client: {2}.", ride.rideID, ride.nameDriver, ride.nameClient);
                rides.Remove(ride);

                UpdateDriverRating(rides, drivers, ride.idD); //since we are deleting ride it is gonna directly affect data in drivers and clients files
                SaveDriversToFile("..\\..\\..\\drivers.txt", drivers);
                UpdateClientRating(rides, clients, ride.idC);
                SaveClientsToFile("..\\..\\..\\clients.txt", clients);
                SaveRidesToFile("..\\..\\..\\rides.txt", rides);
                return;
            }
        }
        Console.WriteLine("Ride with ID#{0} not found.", idInput);
    }
    catch (Exception e) { }
}
Drivers EnterDrivers()
{
    Console.Write("Enter a name of a driver: ");
    string name = Console.ReadLine();
    int year = 0;
    bool shouldwork = false;

    while (!shouldwork)
    {
        Console.Write("Enter an age of a driver: ");

        try
        {
            year = Convert.ToInt32(Console.ReadLine());
            shouldwork = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Incorrect input. Please enter a valid integer.");
        }
    }
    float rating = 0.0f;// we just use zero as at least smth, since when the driver is entered his rating is basically zero (bcz of no rides)
    string type = null;
    bool validInput = true;
    int typeoption;
    while (validInput)
    {
        Console.Write("Enter a type that this driver provides " +
            "\n(Enter '1' if it's UberX" +
            ". Enter '2' if it's Comfort" +
            ". Enter '3' if it's Van): ");
        try
        {
            typeoption = Convert.ToInt32(Console.ReadLine());
            switch (typeoption)
            {
                case 1:
                    type = "UberX";
                    validInput = false;
                    break;
                case 2:
                    type = "Comfort";
                    validInput = false;
                    break;
                case 3:
                    type = "Van";
                    validInput = false;
                    break;
                default:
                    Console.WriteLine("Oops! Something has gone wrong. Probably, you typed a wrong number. Please, try again.\n");
                    break;
            }
        }
        catch (Exception e) { Console.WriteLine("Incorrect input"); }
    }
    Console.WriteLine("You have added a new driver successfully.");
    return new Drivers(name, year, rating, type);
}
Clients EnterClients()
{
    Console.Write("Enter a name of a client: ");
    string name = Console.ReadLine();

    int year = 0;
    bool shouldwork = false;

    while (!shouldwork)
    {
        Console.Write("Enter an age of a driver: ");

        try
        {
            year = Convert.ToInt32(Console.ReadLine());
            shouldwork = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Incorrect input. Please enter a valid integer.");
        }
    }
    float rating = 0.0f;

    return new Clients(name, year, rating);
}
Rides EnterRides(List<Drivers> drivers, List<Clients> clients)
{
    int idR = 0;
    foreach (string line in File.ReadAllLines("..\\..\\..\\rides.txt"))
    {
        string[] parts = line.Split(";");
        var existingID = Convert.ToInt32(parts[0]);
        if (existingID > idR)
            idR = existingID;
    }
    idR++;

    string nameD = ""; string nameC = "";
    int idD = 0; int idC = 0;

    Console.WriteLine("\nHere are all drivers in UBER system:\n");
    PrintDrivers(allDrivers);

    bool work1 = true;
    while (work1)
    {
        try
        {
            Console.Write("\nPlease, enter an ID of the driver: ");
            idD = Convert.ToInt32(Console.ReadLine());
            if (driverFound(drivers, idD, out nameD))
                break;
            else
                Console.WriteLine("Invalid driver ID");
        }
        catch (Exception e)
        {
            Console.WriteLine("Invalid input");
        }
    }

    Console.WriteLine("\nHere are all clients in UBER system:\n");
    PrintClients(clients);
    bool work2 = true;
    while (work2)
    {
        try
        {
            Console.Write("\nPlease, enter an ID of the client: ");
            idC = Convert.ToInt32(Console.ReadLine());
            if (clientFound(clients, idC, out nameC))
                break;
            else
            {
                Console.WriteLine("Invalid client ID.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Invalid input.");
        }

    }

    Console.Write("\nEnter a city of DEPARTURE: ");
    string cityDeparture = Console.ReadLine();
    cityDeparture = cityDeparture.ToUpper();
    Console.Write("Enter a city of ARRIVAL: ");
    string cityArrival = Console.ReadLine();
    cityArrival = cityArrival.ToUpper();

    int scoreD = 0; int scoreC = 0;
    bool flag = true;
    while (flag)
    {
        try
        {
            Console.Write("Enter a score that client gave to his/her DRIVER (0-5): ");
            scoreD = Convert.ToInt32(Console.ReadLine());

            if (scoreD < 0 || scoreD > 5)
                Console.WriteLine("Invalid score for the driver. Please enter a number between 0 and 5 (included).");

            else
            {
                Console.Write("Enter a score that driver gave to his/her CLIENT (0-5): ");
                scoreC = Convert.ToInt32(Console.ReadLine());

                if (scoreC < 0 || scoreC > 5)
                    Console.WriteLine("Invalid score for the client. Please enter a number between 0 and 5 (included).");
                else
                {
                    flag = false;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Incorrect input. Try again.");
        }
    }

    Rides ride = new Rides(idR, nameD, idD, nameC, idC, scoreD, scoreC, cityDeparture, cityArrival);
    allRides.Add(ride);
    UpdateDriverRating(allRides, allDrivers, idD); //ride are directly affecting drivers and clients (to be precise, their rating)
    UpdateClientRating(allRides, allClients, idC);
    return ride;
}
bool driverMatch(int idD) //this is designed for cases when we input IDs so that we could understand if unputed ID is matched with existed one (in list of drivers)
{
    List<Drivers> drivers = ReadDriversFromFile(pathToFileDRIVERS);
    for (int i = 0; i < drivers.Count; i++)
        if (drivers[i].id == idD)
            return true;
    return false;
}
bool clientMatch(int idC)
{
    List<Clients> clients = ReadClientsFromFile(pathToFileCLIENTS);
    for (int i = 0; i < clients.Count; i++)
        if (clients[i].id == idC)
            return true;
    return false;
}
bool driverFound(List<Drivers> drivers, int idD, out string nameD) //this is function is only for giving names by IDs
{
    foreach (Drivers driver in drivers)
    {
        if (idD == driver.id)
        {
            nameD = driver.nameDriver;
            Console.WriteLine("The driver has been found");
            return true;
        }
    }
    nameD = null;
    return false;
}

bool clientFound(List<Clients> clients, int idC, out string nameC)
{
    foreach (Clients client in clients)
    {
        if (idC == client.id)
        {
            nameC = client.nameClient;
            Console.WriteLine("Client has been found");
            return true;
        }
    }
    nameC = null;
    return false;
}

void PrintClients(List<Clients> clients)
{
    foreach (Clients client in clients)
        Console.WriteLine(client.ToString());
}
void PrintDrivers(List<Drivers> drivers)
{
    foreach (Drivers driver in drivers)
    {
        Console.WriteLine(driver.ToString());
    }
}
void PrintRides(List<Rides> rides)
{
    foreach (Rides ride in rides)
        Console.WriteLine(ride.ToString());
}
List<Drivers> ReadDriversFromFile(string path)
{
    List<Drivers> drivers = new List<Drivers>();

    foreach (string line in File.ReadAllLines(path))
    {
        string[] parts = line.Split(";");
        drivers.Add(new Drivers(Convert.ToInt32(parts[0]), parts[1], Convert.ToInt32(parts[2]), Convert.ToSingle(parts[3]), parts[4]));
    }
    return drivers;
}
List<Clients> ReadClientsFromFile(string path)
{
    List<Clients> clients = new List<Clients>();

    foreach (string line in File.ReadAllLines(path))
    {
        string[] parts = line.Split(";");
        clients.Add(new Clients(Convert.ToInt32(parts[0]), parts[1], Convert.ToInt32(parts[2]), Convert.ToSingle(parts[3])));
    }
    return clients;
}
List<Rides> ReadRidesFromFile(string path)
{
    List<Rides> rides = new List<Rides>();
    foreach (string line in File.ReadAllLines(path))
    {
        string[] parts = line.Split(";");
        rides.Add(new Rides(Convert.ToInt32(parts[0]), parts[1], Convert.ToInt32(parts[2]), parts[3], Convert.ToInt32(parts[4]),
                            Convert.ToSingle(parts[5]), Convert.ToSingle(parts[6]),
                            parts[7], parts[8]));
    }
    return rides;
}

void UpdateDriverRating(List<Rides> allRides, List<Drivers> allDrivers, int idDriver)
{

    float totalScore = 0; int totalRides = 0; float averageRating = 0;
    foreach (Rides ride in allRides)
    {
        if (ride.idD == idDriver)
        {
            totalScore += ride.scoreDriver;
            totalRides++;
        }
    }
    averageRating = totalRides > 0 ? totalScore / totalRides : 0;
    UpdateDriverRatingInList(allDrivers, idDriver, averageRating);
    SaveDriversToFile("..\\..\\..\\drivers.txt", allDrivers);
    SaveRidesToFile("..\\..\\..\\rides.txt", allRides);
}
static void UpdateDriverRatingInList(List<Drivers> allDrivers, int driverId, float newRating)
{
    foreach (Drivers driver in allDrivers)
    {
        if (driver.id == driverId)
        {
            driver.ratingDriver = newRating;
            break;
        }
    }
}
void UpdateClientRating(List<Rides> allRides, List<Clients> allClients, int idClient)
{
    float totalScore = 0; int totalRides = 0;
    float averageRating = 0;
    foreach (Rides ride in allRides)
    {
        if (ride.idC == idClient)
        {
            totalScore += ride.scoreClient;
            totalRides++;
        }
    }
    if (totalRides > 0)
        averageRating = totalScore / totalRides;
    else
        averageRating = 0;

    UpdateClientRatingInList(allClients, idClient, averageRating);
    SaveClientsToFile("..\\..\\..\\clients.txt", allClients);
    SaveRidesToFile("..\\..\\..\\rides.txt", allRides);
}
static void UpdateClientRatingInList(List<Clients> allClients, int clientId, float newRating)
{
    foreach (Clients client in allClients)
    {
        if (client.id == clientId)
        {
            client.ratingClient = newRating;
            break;
        }
    }
}
public class Drivers
{
    public static int driverID = 0; //we use statci in order to create a one variable that is common for the whole list of drivers (in makes to possible to give each driver a totally unique ID)
    public int id;
    public string nameDriver;
    public int ageDriver;
    public float ratingDriver;
    public string typeDriver;

    public Drivers(string name, int age, float rating, string type)
    {
        driverID++;
        this.id = driverID;
        this.nameDriver = name;
        this.ageDriver = age;
        this.ratingDriver = rating;
        this.typeDriver = type;
    }

    public Drivers(int id, string name, int age, float rating, string type) //we use this class inside a class for reading drivers while the first one is created for entering a new driver
    { //this was created for the "ID issue"
        this.id = id;
        this.nameDriver = name;
        this.ageDriver = age;
        this.ratingDriver = rating;
        this.typeDriver = type;
        driverID = id;
    }

    public string ToString() => String.Format("{0} (ID:{1}) {2} years old. Rating: {3}, Type: {4}", nameDriver, id, ageDriver, ratingDriver, typeDriver);

}
public class Clients
{
    public string nameClient;
    public static int IdClient = 0;
    public int id;
    public int ageClient;
    public float ratingClient;
    public Clients(string name, int age, float rating)
    {
        this.nameClient = name;
        IdClient++;
        this.id = IdClient;
        this.ageClient = age;
        this.ratingClient = rating;
    }
    public Clients(int id, string name, int age, float rating)
    {
        this.nameClient = name;
        this.id = id;
        this.ageClient = age;
        this.ratingClient = rating;
        IdClient = id;
    }

    public string ToString()
    {
        return String.Format("{0} (ID:{1}), {2} years old. Rating: {3}", nameClient, id, ageClient, ratingClient);
    }
}

public class Rides
{
    public string nameDriver;
    public string nameClient;
    public float scoreDriver;
    public float scoreClient;
    public int rideID;
    public int idD;
    public int idC;
    public string cityDeparture;
    public string cityArrival;

    public Rides(int idR, string nameD, int idD, string nameC, int idC, float scoreD, float scoreC, string cityDeparture, string cityArrival)
    {
        this.nameDriver = nameD;
        this.nameClient = nameC;
        this.scoreDriver = scoreD;
        this.scoreClient = scoreC;
        this.rideID = idR;
        this.idD = idD;
        this.idC = idC;
        this.cityDeparture = cityDeparture;
        this.cityArrival = cityArrival;
    }
    public string ToString()
    {
        return String.Format("{0}^{1} (recieved: {2}) - {3}^{4} (recieved: {5}). ID of this ride: {6}. {7} — {8}", nameDriver, idD, scoreDriver, nameClient, idC, scoreClient, rideID, cityDeparture, cityArrival);
    }
}

