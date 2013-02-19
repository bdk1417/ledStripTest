using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace ledStripTest
{
    public class Program
    {
        

        public static void Main()
        {

            int numLed = 32;
            var spi = new SPI(
                new SPI.Configuration(
                    Cpu.Pin.GPIO_NONE,
                    false, 
                    0, 
                    0, 
                    false, 
                    true, 
                    2000, 
                    SPI.SPI_module.SPI1));
            var colors = new byte[3 * numLed];
            var zeros = new byte[3 * ((numLed + 63) / 64)];

            while (true)
            {
                // all pixels off
                for (int i = 0; i < colors.Length; ++i) colors[i] = (byte)(0x80 | 0);
                // a progressive yellow/red blend
                for (byte i = 0; i < 32; ++i)
                {
                    colors[i * 3 + 1] = 0x80 | 32;
                    colors[i * 3 + 0] = (byte)(0x80 | (32 - i));
                    spi.Write(colors);
                    spi.Write(zeros);
                    Thread.Sleep(1000 / 32); // march at 32 pixels per second
                    
                }
            }

        }
    }
}
