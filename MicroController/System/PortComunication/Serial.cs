﻿using System;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace microController.system
{
    public class Serial
    {
        private Thread ReadThread;
        public SerialPort Port;
        public string Info = "";

        public Serial(string portName, int baudRate) 
        { 
            Port = new SerialPort(portName, baudRate);
            ReadThread = new Thread(Read);
            ReadThread.IsBackground = true;
        }

        public void StartReading()
        {
            try
            {
                Port.Open();
            }
            catch(IOException Exception) 
            {
                Console.WriteLine(Exception);
                Console.WriteLine("Failed to open the serial port." + Port.PortName);
                return;
            }
            if (ReadThread.ThreadState == (ThreadState.Unstarted | ThreadState.Background))
            {
                ReadThread.Start();
            }
            else
            {
                ReadThread.Resume();
            }
        }

        public void StopReading()
        {
            Port.Close();
            ReadThread.Suspend();
        }

        private void Read()
        {
            while(true)
            {
                try
                {
                    Info = Port.ReadLine();
                    Info = Info.Trim();
                }
                catch
                {
                    Console.WriteLine("Failed reading the serial info");
                }
            }
        }
    }
}
