using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    public class ConfigJson
    {

        public int utf8Port = 2515;
        public int unicodePort = 2514;
    }
    static void Main(string[] args)
    {

                //Read "Config.json" file and import it as object
        
        if(!File.Exists("Config.json"))
        {
            File.WriteAllText("Config.json", JsonConvert.SerializeObject(new ConfigJson()));
        }
        string json = System.IO.File.ReadAllText("Config.json");

        ConfigJson config = JsonConvert.DeserializeObject<ConfigJson>(json);//Read "Config.json" file and import it as object


        int utf8Port = config.utf8Port;
        int unicodePort = config.unicodePort;

        Console.WriteLine($"Listening for Unicode messages on port {unicodePort}");
        Thread unicodeThread = new Thread(() => ReceiveUdpMessage(unicodePort, Encoding.Unicode));
        unicodeThread.Start();
        Console.WriteLine($"Listening for UTF8 messages on port {utf8Port}");
        Thread utf8Thread = new Thread(() => ReceiveUdpMessage(utf8Port, Encoding.UTF8));
        utf8Thread.Start();



        // Wait for the threads to complete
         utf8Thread.Join();
        unicodeThread.Join();

        Console.WriteLine($"Press Key to quit...");
        Console.ReadKey();
    }

    static void ReceiveUdpMessage(int serverPort, Encoding encoding)
    {
        using (UdpClient udpClient = new UdpClient(serverPort))
        {
            Console.WriteLine($"UDP server listening on port {serverPort} with {encoding.EncodingName}");

            while (true)
            {
                IPEndPoint remoteEndPoint = null;
                byte[] data = udpClient.Receive(ref remoteEndPoint);

                string message = encoding.GetString(data);

                Console.WriteLine($"Received message from {remoteEndPoint}: {message}");
            }
        }
    }
}