using System;

namespace FietsDemo
{
    public class BikeData
    {
        private bool initialized = false;
        
        private int speed;
        private int time;
        private int watt;
        private int rpm;
        private int heartRate;
        
        //attributen voor afstanden
        private int distance;
        private int lastDistance;
        private int distanceFlip = 0;
        private int distanceOffset = 0;

        public void UpdateData(string data)
        {
            var split = data.Split(' ');
            var length = split.Length;
            switch (length)
            {
                case 4:
                    // heartRate = data[2];
                    heartRate = int.Parse(split[1], System.Globalization.NumberStyles.HexNumber);
                    break;
                case 6:
                    //todo andere data van hartslagmeter, mogelijk nog extra data implementeren.
                    break;
                case 13:
                    changeBikeData(split);
                    break;
            }
        }

        private void changeBikeData(string[] split)
        {
            var PageNumber = int.Parse(split[4], System.Globalization.NumberStyles.HexNumber);
            
            switch (PageNumber)
            {
                case 16:
                    // speed = data[8];
                    // speed = BitConverter.ToInt32(data, 8);
                    speed = int.Parse(split[9], System.Globalization.NumberStyles.HexNumber);
                    //distance flipt na 597
                    distance = int.Parse(split[7], System.Globalization.NumberStyles.HexNumber);
                    
                    if (lastDistance > 200 && distance < 100)
                    {
                        distanceFlip++;
                    }
                    
                    lastDistance = distance;

                    distance += distanceFlip * 256 - distanceOffset;
                        
                    break;
                case 25:
                    rpm = int.Parse(split[6], System.Globalization.NumberStyles.HexNumber);
                    watt = int.Parse(split[9], System.Globalization.NumberStyles.HexNumber);
                    break;
            }

            if (!initialized)
            {
                distanceOffset = distance;
                initialized = true;
            }
        }

        public int Speed
        {
            get => speed;
            set => speed = value;
        }

        public int Distance
        {
            get => distance;
            set => distance = value;
        }

        public int Time
        {
            get => time;
            set => time = value;
        }

        public int Watt
        {
            get => watt;
            set => watt = value;
        }

        public int Rpm
        {
            get => rpm;
            set => rpm = value;
        }

        public int HeartRate
        {
            get => heartRate;
            set => heartRate = value;
        }
    }
}