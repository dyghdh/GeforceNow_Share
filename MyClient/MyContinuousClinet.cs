using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MyClient
{
    class MyContinuousClient
    {
        TcpClient client = null;
        public void Run()
        {
            
            Process.Start("C:/Users/dyghd/AppData/Local/NVIDIA Corporation/GeForceNOW/CEF/GeForceNOW.exe");

            while (true) {
            Console.WriteLine("아이피 : " + loadSet());

            int pass = 0;
            while (pass == 0)
            {
                pass = Connect();
                findProcess();
                Thread.Sleep(1000);
            }

            pass = 0;
            while (pass == 0)
            {
                pass=SendMessage();

                Thread.Sleep(1000);
            }
            }

        }

        private String loadSet()
        {
            IniFile ini = new IniFile();

            ini.Load("Setting.ini");
            string IPAddress = ini["Setting"]["IPAddress"].ToString();

            return IPAddress;
        }


        private String findProcess()
        {
            String find;
            Process[] processList = Process.GetProcessesByName("GeForceNow");

            if (processList.Length < 1)
            {
                find = "OFF\n";
                Console.WriteLine("내 상태 : 지포스나우OFF \n");
                Environment.Exit(0);
            }
            else
            {
                find = "ON\n";
                Console.WriteLine("내 상태 : 지포스나우ON \n");
                
            }

            return find;

        }

        private int SendMessage()
        {
            int pass = 0;
            string message = "상대 : 지포스나우" + findProcess();
            byte[] byteData = new byte[message.Length];
            byteData = Encoding.Default.GetBytes(message);

            try
            {
                client.GetStream().Write(byteData, 0, byteData.Length);
            }
            catch (Exception ex)
            {
                pass = 1;
            }

            return pass;
            Console.WriteLine("전송완료");
            
        }
        private int Connect()
        {
            
            int pass = 1;
            client = new TcpClient();
            try
            {
                client.Connect(loadSet(), 9999);
            }
            catch (Exception ex)
            {
                pass = 0;
            }
            
            Console.WriteLine("상대 : 미접속");

            return pass;
        }
    }
}
