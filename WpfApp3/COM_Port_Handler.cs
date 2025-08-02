// <copyright file="COM_Port_Handler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WpfApp3
{
    using System;
    using System.Diagnostics;
    using System.IO.Ports;
    using IUOM_Lib = WpfApp3.SharedLib.Item_Units_Of_Measure_SharedLib;
    using MW_Lib = WpfApp3.SharedLib.MainWindow_SharedLib;

    internal class COM_Port_Handler
    {
        internal static SerialPort Initial_Serial_Port()
        {
            var userconfig = MW_Lib.User_Configuration_ViewModel_Proxy.User_Configuration;
            SerialPort virtual_COM_Port = new SerialPort();
            virtual_COM_Port.PortName = "COM" + userconfig.Com_Port_Number.ToString();
            virtual_COM_Port.BaudRate = userconfig.Baudrate;
            virtual_COM_Port.Parity = userconfig.Parity;
            virtual_COM_Port.ReadTimeout = 300;
            virtual_COM_Port.DataBits = userconfig.DataBit;
            virtual_COM_Port.StopBits = userconfig.StopBits;
            return virtual_COM_Port;
        }

        internal static Tuple<string, string> Check_Scale_Status(SerialPort _serialport)
        {
            if (_serialport.IsOpen == false)
            {
                try
                {
                    _serialport.Open();
                    return new Tuple<string, string>("Ok", "#5cff64");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("An exception occurred: " + ex.Message);
                    return new Tuple<string, string>("Error", "#ff5c5c");
                }
            }
            else
            {
                return new Tuple<string, string>("Ok", "#5cff64");
            }
        }

        internal static decimal Start_Weighing(SerialPort _serialport)
        {
            if (_serialport.IsOpen == false)
            {
                return decimal.Zero;
            }

            try
            {
                string raw_Data = _serialport.ReadLine()
                    .Replace("kg", string.Empty)
                    .Replace("KG", string.Empty)
                    .Replace("k", string.Empty)
                    .Replace("g", string.Empty)
                    .Replace("\n", string.Empty);
                string numeric_chars = string.Empty;

                for (int i = 0; i < raw_Data.Length; i++)
                {
                    if (Char.IsDigit(raw_Data[i]) || raw_Data[i] == '.')
                        numeric_chars += raw_Data[i];
                }

                return IUOM_Lib.CustomDecimal(numeric_chars);
            }
            catch (Exception ex)
            {

            }

            return decimal.Zero;
        }
    }
}
