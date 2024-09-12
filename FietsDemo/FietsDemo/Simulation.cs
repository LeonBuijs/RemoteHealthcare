namespace FietsDemo
{
    public class Simulation
    {
        private int speed { get; set; }
        private int time { get; set; } //todo
        private int watt { get; set; }
        private int rpm { get; set; }
        private int heartRate { get; set; }
        private int distance { get; set; }
        private int counter {get; set;}

        public Simulation()
        {
            this.speed = 0;
            this.time = 0;
            this.watt = 0;
            this.rpm = 0;
            this.heartRate = 60;
            this.distance = 0;
            this.counter = 0;
        }

        public byte[] GenerateData()
        {
            byte[] data = null;
            switch (counter)
            {
                case 0:
                    // Bikedata 25
                    data = new byte[] {0xA4, 0x09, 0x4E, 0x05, 0x19, 0x00, (byte)this.rpm, 0x00, 0x00, (byte)this.watt, 0x00, 0x00, 0x00};
                    break;
                case 1:
                    // Bikedata 16
                    data = new byte[] {0xA4, 0x09, 0x4E, 0x05, 0x10, 0x00, 0x00, (byte)this.distance, 0x00, (byte)this.speed, 0x00, 0x00, 0x00};
                    break;
                case 2:
                    // Hartslag
                    data = new byte[] {16, (byte)this.heartRate, 0x00, 0x00};
                    break;
            }
             
            this.counter++;
            return data;
        }
    }
}