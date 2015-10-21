using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;
using Microsoft.Win32;
using System.Data;


namespace Projekt_kraj
{
    class System_information
    {
        public System_information()
        {

        }

        /// <summary>
        /// Methods return System manufacture, model and Bios version
        /// </summary>
        /// <param name="info_select"></param>
        /// <returns></returns>
        public Dictionary<string, string> Hardware_info_select(string info_select)
        {
            Dictionary<string, string> SystemInfo = new Dictionary<string, string>();
            System.Management.SelectQuery query = new System.Management.SelectQuery(@"Select * from " + info_select);

            //initialize the searcher with the query it is supposed to execute
            using (System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(query))
            {
                try { 
                //execute the query
                    foreach (System.Management.ManagementObject process in searcher.Get())
                    {
                        //print system info
                        process.Get();
                        SystemInfo.Add("Manufacturer", process["Manufacturer"].ToString());
                        SystemInfo.Add("Model", process["Model"].ToString().Trim());
                        SystemInfo.Add("ComputerName", process["Caption"].ToString().Trim());
                        SystemInfo.Add("Domain", process["Domain"].ToString().Trim());
                        SystemInfo.Add("UserName", process["PrimaryOwnerName"].ToString().Trim());
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }

            }

            System.Management.ManagementObjectSearcher searcher1 = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            System.Management.ManagementObjectCollection collection = searcher1.Get();


            foreach (ManagementObject obj in collection)
            {
                obj.Get();
                if (((string[])obj["BIOSVersion"]).Length > 1)
                    SystemInfo.Add("BIOSVersion", ((string[])obj["BIOSVersion"])[0] + " - " + ((string[])obj["BIOSVersion"])[1]);
                else
                    SystemInfo.Add("BIOSVersion", ((string[])obj["BIOSVersion"])[0]);
            }
            return SystemInfo;
        }


        /// <summary>
        ///Method return instaled program in PC 
        /// </summary>
        /// <returns>Return list<string> isntalled program</returns>
        public List<string> Program_print()
        {
            Stopwatch pr = new Stopwatch();
            Stopwatch pr2 = new Stopwatch();
            List<string> installed_program = new List<string>();

            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        try
                        {
                            installed_program.Add(subkey.GetValue("DisplayName").ToString());
                        }
                        catch (Exception e)
                        {
                            ; // todo
                        }
                    }
                }
            }
            pr.Stop();
            Console.WriteLine("Time elapsed using: {0}", pr.Elapsed);
            Console.WriteLine("Time elapsed foreach: {0}", pr2.Elapsed);
            return installed_program;
        }

        public List<PCU> CPU_info()
        {
            List<PCU> CPUInfo = new List<PCU>();
            System.Management.SelectQuery query = new System.Management.SelectQuery(@"Select Name,NumberOfCores,NumberOfLogicalProcessors,MaxClockSpeed from Win32_Processor");
                //initialize the searcher with the query it is supposed to execute
                using (System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(query))
                {
                    try
                    {
                        //execute the query
                        foreach (System.Management.ManagementObject process in searcher.Get())
                        {
                            PCU cpu = new PCU(process["Name"].ToString(), process["NumberOfCores"].ToString(), process["NumberOfLogicalProcessors"].ToString(), process["MaxClockSpeed"].ToString());
                            CPUInfo.Add(cpu);

                        }
                       
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                return CPUInfo;
        }

        public Dictionary<string, string> Windows_info()
        {
            Dictionary<string, string> windows = new Dictionary<string, string>();
            System.Management.SelectQuery query = new System.Management.SelectQuery(@"Select Caption,OSArchitecture,SerialNumber from Win32_OperatingSystem ");

            //initialize the searcher with the query it is supposed to execute
            using (System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(query))
            {
                try
                {
                    //execute the query
                    foreach (System.Management.ManagementObject process in searcher.Get())
                    {
                        //print system info
                        process.Get();
                        windows.Add("SerialNumber", process["SerialNumber"].ToString());
                        windows.Add("SystemVersion", process["Caption"].ToString());
                        windows.Add("Architecture", process["OSArchitecture"].ToString());

                    }

                    windows.Add("CoreVersion", Environment.OSVersion.ToString());
                    windows.Add("ComputerName", Environment.MachineName.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return windows;
            }
        }

        public List<GraphicCard> GPU_info()
        {
            List<GraphicCard> GPUInfo = new List<GraphicCard>();

            System.Management.SelectQuery query = new System.Management.SelectQuery(@"Select Caption from Win32_VideoController");
            
            using (System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(query))
            {
                try
                {

                    foreach (System.Management.ManagementObject process in searcher.Get())
                    {
                        GraphicCard gpu = new GraphicCard(process["Caption"].ToString());
                        GPUInfo.Add(gpu);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }             
            }
            return GPUInfo;
        }

        public List<HDD> Disk_info()
        {
            List<HDD> DiskInfo = new List<HDD>();

            System.Management.SelectQuery query = new System.Management.SelectQuery(@"Select Model,Size from Win32_DiskDrive");

            using (System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(query))
            {
                foreach (System.Management.ManagementObject process in searcher.Get())
                {
                    try
                    {
                        HDD hardDisk = new HDD();
                        hardDisk.Name = process["Model"].ToString();
                        hardDisk.Size = process["Size"].ToString();
                        DiskInfo.Add(hardDisk);
                    }catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }                              
            }
            return DiskInfo;
        }

        public List<CDROM> CDrom_info()
        {
            List<CDROM> cdromInfo = new List<CDROM>();

            System.Management.SelectQuery query = new System.Management.SelectQuery(@"Select Name from Win32_CDROMDrive");

            using (System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(query))
            {
                try
                {
                    foreach (System.Management.ManagementObject process in searcher.Get())
                    {
                        CDROM cd = new CDROM(process["Name"].ToString());
                        cdromInfo.Add(cd);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }            
            return cdromInfo;
        }

        public List<Memory> Memory_Info()
        {
            List<Memory> memoryInfo = new List<Memory>();

            System.Management.SelectQuery query = new System.Management.SelectQuery(@"Select Capacity,Speed from Win32_PhysicalMemory");

            using (System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(query))
            {
                try
                {
                    foreach (System.Management.ManagementObject process in searcher.Get())
                    {
                        Memory mem = new Memory(process["Capacity"].ToString(), process["Speed"].ToString());
                        memoryInfo.Add(mem);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return memoryInfo;
        }

        public List<NetworkAdapter> NetworkAdapter_Info()
        {
            List<NetworkAdapter> NetworkAdapterInfo = new List<NetworkAdapter>();

            System.Management.SelectQuery query = new System.Management.SelectQuery(@"Select ProductName,Speed,MacAddress,PhysicalAdapter from Win32_NetworkAdapter");

            using (System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(query))
            {
                foreach (System.Management.ManagementObject process in searcher.Get())
                {
                    NetworkAdapter networkadapter = new NetworkAdapter(process["ProductName"].ToString(), process["PhysicalAdapter"].ToString());
                    try
                    {
                        networkadapter.Speed = process.GetPropertyValue("Speed").ToString();
                        NetworkAdapterInfo.Add(networkadapter);
                        networkadapter.MacAddress = process.GetPropertyValue("MacAddress").ToString();
                    }
                    catch (Exception e)
                    {
                        //Console.WriteLine(e);
                    }
                }
            }
            return NetworkAdapterInfo;
        }
        
        public List<SoundCard> soundCard_Info()
        {
            List<SoundCard> sCard = new List<SoundCard>();

            System.Management.SelectQuery query = new System.Management.SelectQuery(@"Select Caption from Win32_SoundDevice");

            using (System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(query))
            {
                try
                {
                    foreach (System.Management.ManagementObject process in searcher.Get())
                    {
                        SoundCard card = new SoundCard(process["Caption"].ToString());
                        sCard.Add(card);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return sCard;
        }

        public List<Printer> printer_Info()
        {
            List<Printer> printer = new List<Printer>();

            System.Management.SelectQuery query = new System.Management.SelectQuery(@"Select Caption,Network from Win32_Printer");

            using (System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(query))
            {
                try
                {
                    foreach (System.Management.ManagementObject process in searcher.Get())
                    {
                        Printer p = new Printer(process["Caption"].ToString(), process.GetPropertyValue("Network").ToString());
                        printer.Add(p);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return printer;
        }
        public void LastUpdateTime ()
        {
           // RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\");
            //object o = key.GetValue(@"WindowsUpdate\Auto Update\Result\Detect/LastSuccessTime");
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\",true);
            key.CreateSubKey("WindowsUpdate");
            key.OpenSubKey("WindowsUpdate",true);
            //
           key.Close();
       
            
        }





            /// <summary>
            /// Method for computer informatrion
            /// </summary>
            /// <returns>String contains Operating system, Operatin system version, System Directory, Core count, Page size, Computer name, User domain name, User name, Logical drives</returns>

            /*public string ComputerInformation()
            {
                StringBuilder StringBuilder1 = new StringBuilder(string.Empty);
                try
                {
                    StringBuilder1.AppendFormat("Operation System:  {0}\n", Environment.OSVersion);
                    if (Environment.Is64BitOperatingSystem)
                        StringBuilder1.AppendFormat("\t\t  64 Bit Operating System\n");
                    else
                        StringBuilder1.AppendFormat("\t\t  32 Bit Operating System\n");
                    StringBuilder1.AppendFormat("Version:  {0}", Environment.Version);
                    StringBuilder1.AppendFormat("SystemDirectory:  {0}\n", Environment.SystemDirectory);
                    StringBuilder1.AppendFormat("CoreCount:  {0}\n", Environment.ProcessorCount);
                    StringBuilder1.AppendFormat("ComputerName:  {0}\n", Environment.MachineName);
                    StringBuilder1.AppendFormat("UserDomainName:  {0}\n", Environment.UserDomainName);
                    StringBuilder1.AppendFormat("UserName: {0}\n", Environment.UserName);
                    //Drives
                    StringBuilder1.AppendFormat("LogicalDrives:\n");
                    foreach (System.IO.DriveInfo DriveInfo1 in System.IO.DriveInfo.GetDrives())
                    {
                        try
                        {
                            StringBuilder1.AppendFormat("\t Drive: {0}\n\t\t VolumeLabel: " +
                              "{1}\n\t\t DriveType: {2}\n\t\t DriveFormat: {3}\n\t\t " +
                              "TotalSize: {4}\n\t\t AvailableFreeSpace: {5}\n",
                              DriveInfo1.Name, DriveInfo1.VolumeLabel, DriveInfo1.DriveType,
                              DriveInfo1.DriveFormat, DriveInfo1.TotalSize, DriveInfo1.AvailableFreeSpace);
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }
                return StringBuilder1.ToString();
            }

    
        }*/
        }
    }

