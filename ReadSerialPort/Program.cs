using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;

namespace ReadSerialPort
{
    //Test
    class Program
    {
        static void Main(string[] args)
        {
            var names = System.IO.Ports.SerialPort.GetPortNames();
            string tmpNum;
            int numero = 0;
            do
            {
                Console.WriteLine("Puertos disponibles: ");
                for (int i = 0; i < names.Count(); i++)
                {
                    Console.WriteLine( i+1 + ". " + names[i]);
                }
                Console.Write("Ingrese el número del puerto: ");
                tmpNum = Console.ReadLine();
                int.TryParse(tmpNum, out numero);

            } while (numero <= 0 || numero > names.Count());

            System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort(names[numero-1], 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            sp.DataReceived += sp_DataReceived;
            sp.Open();
            Console.ReadKey();
            sp.Close();
        }

        static void sp_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //Console.WriteLine("Recibido!");
            try
            {
                System.IO.Ports.SerialPort sp = sender as System.IO.Ports.SerialPort;
                //char[] cad = new char[1000];
                var cad = sp.ReadLine();
                Console.WriteLine(cad);
            }
            catch (Exception ex)
            {

                Trace.WriteLine(ex.Message, "Error");
                Trace.WriteLine(ex.StackTrace);
            }
            
            
        }
    }
}
