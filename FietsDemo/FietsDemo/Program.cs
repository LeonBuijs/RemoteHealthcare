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
        static async Task Main(string[] args)
        {
            int errorCode = 0;
            BLE bleBike = new BLE();
            BLE bleHeart = new BLE();
            Thread.Sleep(1000); // We need some time to list available devices

            // List available devices
            List<String> bleBikeList = bleBike.ListDevices();
            Console.WriteLine("Devices found: ");
            foreach (var name in bleBikeList)
            {
                Console.WriteLine($"Device: {name}");
            }

            // Connecting
            errorCode = errorCode = await bleBike.OpenDevice("Tacx Flux 00438");
            // __TODO__ Error check

            var services = bleBike.GetServices;
            foreach (var service in services)
            {
                Console.WriteLine($"Service: {service}");
            }

            // Set service
            errorCode = await bleBike.SetService("6e40fec1-b5a3-f393-e0a9-e50e24dcca9e");
            // __TODO__ error check

            // Subscribe
            bleBike.SubscriptionValueChanged += BleBike_SubscriptionValueChanged;
            errorCode = await bleBike.SubscribeToCharacteristic("6e40fec2-b5a3-f393-e0a9-e50e24dcca9e");

            // Heart rate
            errorCode = await bleHeart.OpenDevice("Decathlon Dual HR");

            await bleHeart.SetService("HeartRate");

            bleHeart.SubscriptionValueChanged += BleBike_SubscriptionValueChanged;
            await bleHeart.SubscribeToCharacteristic("HeartRateMeasurement");


            Console.Read();
        }

        private static void BleBike_SubscriptionValueChanged(object Sender, BLESubscriptionValueChangedEventArgs E)
        {
            // Console.WriteLine("Received from {0}: {1}, {2}", e.ServiceName,
            //     BitConverter.ToString(e.Data).Replace("-", " "),
            //     Encoding.UTF8.GetString(e.Data));
            CalculateData(BitConverter.ToString(E.Data).Replace("-", " "));
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
            int ParseHex = int.Parse(HexSplit[9], System.Globalization.NumberStyles.HexNumber);
            string HexString = ParseHex.ToString();
            // builder.Append(hexString);
            // Console.WriteLine(builder.ToString());
            if (HexSplit[4] == "10")
            {
                Console.WriteLine($"Speed: {HexString} km/h");
            }
        }
    }
}