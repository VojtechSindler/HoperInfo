using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace Projekt_kraj
{
    class Save
    {
        string Path;
        /// <summary>
        /// Konstruct
        /// </summary>
        /// <param name="save">path to save XML file</param>
        public Save()
        {
            
        }
      
        /// <summary>
        /// Method for save xml document
        /// </summary>
        public void SaveXML()
        {
            System_information sys = new System_information();

            XmlDocument xml = new XmlDocument();

            XmlNode rootNode = xml.CreateElement("PC");
            xml.AppendChild(rootNode);


            #region Definicion XML ELEMENT
            XmlElement programs = xml.CreateElement("Programs");
            XmlElement CPU = xml.CreateElement("CPUs");
            XmlElement SystemInfo = xml.CreateElement("SystemInfo");
            XmlElement WindowsInfo = xml.CreateElement("WindowsInfo");
            XmlElement GPUInfo = xml.CreateElement("GPUs");
            XmlElement HDDInfo = xml.CreateElement("HDDs");
            XmlElement CDROMInfo = xml.CreateElement("CD-DVD_ROMs");
            XmlElement RAM = xml.CreateElement("RAM");
            XmlElement NIC = xml.CreateElement("NICs");
            XmlElement SoundCard = xml.CreateElement("SoundCards");
            XmlElement Printer = xml.CreateElement("Printers");
            
            #endregion


            Stopwatch ProgramRunning2 = new Stopwatch();
            Stopwatch ProgramRunning3 = new Stopwatch();
            Stopwatch ProgramRunning4 = new Stopwatch();
            
            #region SystemInfo
            Dictionary<string, string> SystemInfo_ = new Dictionary<string, string>();
            ProgramRunning2.Start();
            SystemInfo_ = sys.Hardware_info_select("Win32_ComputerSystem"); // TODO parametr hodit do metody a tady smaza
            this.Path = SystemInfo_["ComputerName"] + "@" + SystemInfo_["Domain"] + ".xml";
            rootNode.AppendChild(SystemInfo);

            foreach (KeyValuePair<string, string> pair in SystemInfo_)
            {                               
                 XmlElement systInfo = xml.CreateElement(pair.Key);
                 systInfo.InnerText = pair.Value;
                 SystemInfo.AppendChild(systInfo);                 
            }
            ProgramRunning2.Stop();
            Console.WriteLine("Time elapsed SystemInfo: {0}", ProgramRunning2.Elapsed);
            #endregion

            #region CPUInfo
            rootNode.AppendChild(CPU);
            List<PCU> CPUInfo = new List<PCU>();
            ProgramRunning3.Start();
            CPUInfo = sys.CPU_info();
            
            foreach (PCU cpu in CPUInfo)
            {             

                XmlElement CPUInfo_ = xml.CreateElement("CPU");
                XmlElement CPUName = xml.CreateElement("Name");
                XmlElement CPUCores = xml.CreateElement("Cores");
                XmlElement CPULocialCores = xml.CreateElement("LocicalCores");
                XmlElement MaxFreq = xml.CreateElement("MaximalFrequenci");

                CPUName.InnerText = cpu.Name;
                CPUCores.InnerText = cpu.NumberOfCores1;
                CPULocialCores.InnerText = cpu.NumberOfLogicalProcessors1;
                MaxFreq.InnerText = cpu.MaxFreq;
                
                CPU.AppendChild(CPUInfo_);
                CPUInfo_.AppendChild(CPUName);
                CPUInfo_.AppendChild(CPUCores);
                CPUInfo_.AppendChild(CPULocialCores);
                CPUInfo_.AppendChild(MaxFreq);

            }
            ProgramRunning3.Stop();
            Console.WriteLine("Time elapsed CPUInfo: {0}", ProgramRunning3.Elapsed);
            #endregion

            #region WindowsInfo

            rootNode.AppendChild(WindowsInfo);
            Dictionary<string, string> WindowsInfo_ = new Dictionary<string, string>();
            ProgramRunning4.Start();
            WindowsInfo_ = sys.Windows_info();

            foreach (KeyValuePair<string, string> pair in WindowsInfo_)
            {               
                XmlElement WinInfo = xml.CreateElement(pair.Key);
                WinInfo.InnerText = pair.Value;
                WindowsInfo.AppendChild(WinInfo);
            }
            ProgramRunning4.Stop();
            Console.WriteLine("Time elapsed WindowsInfo: {0}", ProgramRunning4.Elapsed);
            #endregion

            #region GPUInfo
            rootNode.AppendChild(GPUInfo);

            List<GraphicCard> GPUInfo_ = new List<GraphicCard>();
            GPUInfo_ = sys.GPU_info();

            foreach (GraphicCard g in GPUInfo_)
            {
                XmlElement GPUinf = xml.CreateElement("GPU");
                GPUinf.InnerText = g.Name;
                GPUInfo.AppendChild(GPUinf);
            }

            #endregion

            #region HDDInfo
            rootNode.AppendChild(HDDInfo);

            List<HDD> HDDInfo_ = new List<HDD>();
            HDDInfo_ = sys.Disk_info();

            foreach ( HDD disk in HDDInfo_)
            {
                XmlElement hdd = xml.CreateElement("HDD");
                XmlElement hddName = xml.CreateElement("Name");
                XmlElement hddSize = xml.CreateElement("Size");
                
                if(disk.SizeDouble > 20)
                {
                    hddName.InnerText = disk.Name;
                    hddSize.InnerText = disk.Size;

                    
                    hdd.AppendChild(hddName);
                    hdd.AppendChild(hddSize);
                    HDDInfo.AppendChild(hdd);
                }


               
                
            }
            #endregion

            #region CDROMInfo
            rootNode.AppendChild(CDROMInfo);

            List<CDROM> CDROMInfo_ = new List<CDROM>();
            CDROMInfo_= sys.CDrom_info();

            foreach (CDROM cd in CDROMInfo_)
            {
                XmlElement cdRom = xml.CreateElement("CD-DVD_ROM");
                XmlElement cdRomName = xml.CreateElement("Name");
                cdRomName.InnerText = cd.Name;

                CDROMInfo.AppendChild(cdRom);
                cdRom.AppendChild(cdRomName);
            }


            #endregion

            #region RAM
            rootNode.AppendChild(RAM);

            List<Memory> RamInfo_ = new List<Memory>();
            RamInfo_ = sys.Memory_Info();

            foreach (Memory memory in RamInfo_)
            {
                XmlElement Ram = xml.CreateElement("Memory");
                XmlElement capacityRam = xml.CreateElement("Capacity");
                XmlElement speedRam = xml.CreateElement("Speed");
                capacityRam.InnerText = memory.Capacity;
                speedRam.InnerText = memory.Speed;

                RAM.AppendChild(Ram);
                Ram.AppendChild(capacityRam);
                Ram.AppendChild(speedRam);
            }
            #endregion RAM

            #region NIC
            rootNode.AppendChild(NIC);
            List<NetworkAdapter> NIC_ = new List<NetworkAdapter>();
            NIC_ = sys.NetworkAdapter_Info();

            foreach (NetworkAdapter nic in NIC_)
            {  
                    XmlElement productName = xml.CreateElement("Name");
                    XmlElement macAdress = xml.CreateElement("MACAddress");
                    XmlElement speed = xml.CreateElement("Speed");
                    XmlElement physicalAdapter = xml.CreateElement("PhysicalAdapter");
                    XmlElement virtualAdapter = xml.CreateElement("VirtualAdapter");

                    if (nic.PhysicalAdapter.Equals("True"))
                    {
                        productName.InnerText = nic.ProductName;
                        macAdress.InnerText = nic.MacAddress;
                       

                       
                        NIC.AppendChild(physicalAdapter);
                        physicalAdapter.AppendChild(productName);
                        physicalAdapter.AppendChild(macAdress);

                    }
                    else
                    {
                        productName.InnerText = nic.ProductName;
                        macAdress.InnerText = nic.MacAddress;
                        
                        NIC.AppendChild(virtualAdapter);
                        virtualAdapter.AppendChild(productName);
                        
                    }
                    
                    
                  
                    //Nic.AppendChild(speed);
              


            }
            #endregion NIC

            #region soundCard
            rootNode.AppendChild(SoundCard);
            List<SoundCard> sCard_ = new List<SoundCard>();
            sCard_ = sys.soundCard_Info();

            foreach (SoundCard scard in sCard_)
            {
                    XmlElement soundCard = xml.CreateElement("SoundCard");
                    XmlElement soundCardName = xml.CreateElement("Name");
                    soundCardName.InnerText = scard.Caption;
                    SoundCard.AppendChild(soundCard);
                    soundCard.AppendChild(soundCardName);
                    //Nic.AppendChild(speed);                
            }


            #endregion

            #region Printer
            rootNode.AppendChild(Printer);
            List<Printer> printer_ = new List<Printer>();
            printer_ = sys.printer_Info();

            foreach (Printer p in printer_)
            {
                XmlElement printer = xml.CreateElement("Printer");
                XmlElement PrinterName = xml.CreateElement("Caption");
                XmlElement isNetwork = xml.CreateElement("Network");
                if (!p.Caption.Contains("Fax") && !p.Caption.Contains("Microsoft") && !p.Caption.Contains("Odeslat"))
                {
                    PrinterName.InnerText = p.Caption;
                    isNetwork.InnerText = p.IsNetwork;
                    Printer.AppendChild(printer);
                    printer.AppendChild(PrinterName);
                    printer.AppendChild(isNetwork);
                }
            }

            #endregion

            #region ProgramsInfo
            List<string> programList = new List<string>();
            programList = sys.Program_print();
            rootNode.AppendChild(programs);

            for (int i = 0; i < programList.Count - 1; i++)
            {
                XmlElement instaledPrograms = xml.CreateElement("Program");
                instaledPrograms.InnerText = programList[i];
                programs.AppendChild(instaledPrograms);
            }
            
            #endregion

            xml.Save(this.Path);
            
            Console.WriteLine("Hotovo");
        }

    }
}
