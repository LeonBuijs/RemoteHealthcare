namespace FietsDemo
{
    public class BikeData
    {
        // private string Data;
        private int speed;
        private int distance;
        private int time;
        private int watt;
        private int rpm;
        private int heartRate;

        public BikeData(byte[] data)
        {
            if (data[4] == 16)
            {
                
            }
            else if (data[4] == 25)
            {
                
            }

            this.speed = speed;
            this.distance = distance;
            this.time = time;
            this.watt = watt;
            this.rpm = rpm;
            this.heartRate = heartRate;
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