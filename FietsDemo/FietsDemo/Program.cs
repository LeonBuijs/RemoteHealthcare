using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avans.TI.BLE;

namespace FietsDemo
{
    class Program
    {
        private static Simulation Simulation = new Simulation(10, 100, 20, 69);
        static async Task Main(string[] args)
        {
            int errorCode = 0;
            BLE bleBike = new BLE();
            BLE bleHeart = new BLE();
            Thread.Sleep(1000); // We need some time to list available devices
            
            // StartSimulation();
            
            // List available devices
            List<String> bleBikeList = bleBike.ListDevices();
            Console.WriteLine("Devices found: ");
            foreach (var name in bleBikeList)
            {
                Console.WriteLine($"Device: {name}");
            }

            // Connecting
            errorCode = await bleBike.OpenDevice("Tacx Flux 00438");
            // TODO Error check
            Console.WriteLine($"BikeOpen: {errorCode}");
            
            while (errorCode != 0)
            {
                errorCode = await bleBike.SetService("6e40fec1-b5a3-f393-e0a9-e50e24dcca9e");
                Thread.Sleep(1000);
                Console.WriteLine($"BikeOpen: {errorCode}");
            }
            
            

            var services = bleBike.GetServices;
            foreach (var service in services)
            {
                Console.WriteLine($"Service: {service}");
            }

            // Set service
            errorCode = await bleBike.SetService("6e40fec1-b5a3-f393-e0a9-e50e24dcca9e");
            // TODO error check
            while (errorCode != 0)
            {
                errorCode = await bleBike.SetService("6e40fec1-b5a3-f393-e0a9-e50e24dcca9e");
            }
            Console.WriteLine($"Bike: {errorCode}");

            // Subscribe
            bleBike.SubscriptionValueChanged += BleBike_SubscriptionValueChanged;
            errorCode = await bleBike.SubscribeToCharacteristic("6e40fec2-b5a3-f393-e0a9-e50e24dcca9e");
            Console.WriteLine($"BikeSubscription: {errorCode}");
            //todo byte array zien te maken
            // bleBike.WriteCharacteristic("6e40fec2-b5a3-f393-e0a9-e50e24dcca9e", new[] { });

            // Heart rate
            errorCode = await bleHeart.OpenDevice("Decathlon Dual HR");
            Console.WriteLine($"Heart: {errorCode}");
            while (errorCode != 0) 
            {
                errorCode = await bleHeart.OpenDevice("Decathlon Dual HR");
                Console.WriteLine($"Heart: {errorCode}");
                Thread.Sleep(1000);
            }
            
            errorCode = await bleHeart.SetService("HeartRate");
            Console.WriteLine($"HeartRate: {errorCode}");
            while (errorCode != 0) 
            {
                errorCode = await bleHeart.SetService("HeartRate");
                Console.WriteLine($"Heart: {errorCode}");
                Thread.Sleep(1000);
            }

            bleHeart.SubscriptionValueChanged += BleBike_SubscriptionValueChanged;
            errorCode = await bleHeart.SubscribeToCharacteristic("HeartRateMeasurement");
            Console.WriteLine($"HeartRateMeasurement: {errorCode}");

            while (errorCode != 0) 
            {
                errorCode = await bleHeart.SubscribeToCharacteristic("HeartRateMeasurement");
                Console.WriteLine($"Heart: {errorCode}");
                Thread.Sleep(1000);
            }
            
            Console.Read();
        }
        
        

        private static BikeData bikeData = new BikeData();
        private static void BleBike_SubscriptionValueChanged(object Sender, BLESubscriptionValueChangedEventArgs e)
        {
            // Console.WriteLine("Received from {0}: {1}, {2}", e.ServiceName,
            //     BitConverter.ToString(e.Data).Replace("-", " "),
            //     Encoding.UTF8.GetString(e.Data));
            // Console.WriteLine(e.Data);
            // if (e.Data.Length < 7)
            // {
                // Console.WriteLine(BitConverter.ToString(e.Data));
            // }
            // Console.WriteLine(e.Data[4]);
            // CalculateData(BitConverter.ToString(e.Data).Replace("-", " "));
            // bikeData.UpdateData(BitConverter.ToString(e.Data).Replace("-", " "));
            bikeData.UpdateData(BitConverter.ToString(Simulation.GenerateData()).Replace("-", " "));
            Console.WriteLine($"Speed: {bikeData.speed} RPM: {bikeData.rpm} Distance: {bikeData.distance} Watts: {bikeData.watt} Time: {bikeData.time} HeartRate: {bikeData.heartRate}");
        }
        
        //method to use simulated data
        private static void StartSimulation()
        {
            while (true)
            {
                bikeData.UpdateData(BitConverter.ToString(Simulation.GenerateData()).Replace("-", " "));
                Console.WriteLine($"Speed: {bikeData.speed} RPM: {bikeData.rpm} Distance: {bikeData.distance} Watts: {bikeData.watt} Time: {bikeData.time} HeartRate: {bikeData.heartRate}");
            }
        }
        
        /**
         * De methode CalculateData krijgt de data binnen als een string.
         * Deze data wordt omgerekend van hex naar een decimaal getal.
         * Verder worden er aan sommige getallen nullen aan toegevoegd om er voor te zorgen dat er 3 getallen staan,
         * dit is beter voor de leesbaarheid.
         */
        private static void CalculateData(String data)
        {
            StringBuilder Builder = new StringBuilder();
            String[] HexSplit = data.Split(' ');
            foreach (String hexDec in HexSplit)
            {
                int Number = int.Parse(hexDec, System.Globalization.NumberStyles.HexNumber);
                String NumberToAdd = Number.ToString();
                if (NumberToAdd.Length == 1)
                {
                    NumberToAdd = "00" + NumberToAdd;
                }

                if (NumberToAdd.Length == 2)
                {
                    NumberToAdd = "0" + NumberToAdd;
                }

                Builder.Append(NumberToAdd).Append("|").Append(' ');
            }

            Builder.Append("\n------------------------\n");
            // int ParseHex = int.Parse(HexSplit[9], System.Globalization.NumberStyles.HexNumber);
            // string HexString = ParseHex.ToString();
            // Builder.Append(HexString);
            Console.WriteLine(Builder.ToString());

            //check in hex typen
            //vanaf [4] data uitlezen
            if (HexSplit[4] == "10")
            {
                // Console.WriteLine($"Data: {ParseHex}");
            }
        }
    }
}