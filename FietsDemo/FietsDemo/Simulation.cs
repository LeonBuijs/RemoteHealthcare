using System.Threading;

namespace FietsDemo
{
    public class Simulation
    {
        public int speed { get; set; }
        public double time { get; set; }
        public int watt { get; set; }
        public int rpm { get; set; }
        public int heartRate { get; set; }
        public int distance { get; set; }
        public int counter { get; set; }

        public Simulation(int speed, int watt, int rpm, int heartRate)
        {
            this.speed = speed;
            this.time = 0;
            this.watt = watt;
            this.rpm = rpm;
            this.heartRate = heartRate;
            this.distance = 0;
            this.counter = 0;
            Thread thread = new Thread(UpdateTime);
            thread.Start();
        }

        public byte[] GenerateData()
        {
            byte[] data = null;
            switch (counter)
            {
                case 0:
                    // Bikedata 25
                    data = new byte[]
                    {
                        0xA4, 0x09, 0x4E, 0x05, 0x19, 0x00, (byte)this.rpm, 0x00, 0x00, (byte)this.watt, 0x00, 0x00,
                        0x00
                    };
                    break;
                case 1:
                    // Bikedata 16
                    CalculateDistance();
                    data = new byte[]
                    {
                        0xA4, 0x09, 0x4E, 0x05, 0x10, 0x00, (byte)this.time, (byte)this.distance, 0x00,
                        (byte)this.speed, 0x00,
                        0x00, 0x00
                    };
                    break;
                case 2:
                    // Hartslag
                    data = new byte[] { 16, (byte)this.heartRate, 0x00, 0x00 };
                    counter = -1;
                    break;
                default:
                    counter = -1;
                    break;
            }

            counter++;
            return data;
        }

        private void CalculateDistance()
        {
            this.distance = ((int)((this.speed / 3.6) * this.time) % 255);
        }

        private void UpdateTime()
        {
            while (true)
            {
                Thread.Sleep(250);
                this.time = (this.time + 1) % 255;
            }
        }
    }
}