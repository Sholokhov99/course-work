using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using ErrorInPrograms;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.Xml;
using System.Text;

namespace SecurityModule
{
    public class Security
    {
        private const string _disallowRun = @"DisallowRun";
        public string _hostsName { get; } = "hosts";

        public static List<string> _listProtocols = new List<string>()
        {
            "http://",
            "https://",
            "ftp://",
            "pop3://",
            "smtp://",
            "telnet://",

        };
        public static List<string> _listPrefixUrl = new List<string>()
        {
            "www.",
            "m.",
        };

        public System.Windows.Forms.Timer killExplorer = new System.Windows.Forms.Timer();
        public Security()
        {
            System.Windows.Forms.Timer autoRun = new System.Windows.Forms.Timer();
            autoRun.Interval = 18000;
            autoRun.Enabled = true;
            autoRun.Tick += (s, e) => { CheckAutoRun(); };
        }

        public static void UpdateNameVideo(string login, string newLogin)
        {
            string way = Application.StartupPath + @"\Video\";
            string temp_login = "";
            string file = "";
            string temp_dateFile = "";
            bool isLogin = true;

            DirectoryInfo dir = new DirectoryInfo(way);

            foreach (var value in dir.GetFiles())
            {
                file = value.Name;
                for (int item = 0; item < file.Length; item++)
                {
                    if (isLogin)
                    {
                        if (file[item] != '_')
                        {
                            temp_login += file[item];
                        }
                        else
                        {
                            isLogin = false;
                        }
                    }
                    else
                    {
                        temp_dateFile += file[item];
                    }
                }

                if (temp_login == login)
                {
                    string moveFile = way + newLogin + '_' + temp_dateFile;
                    File.Move(value.FullName, moveFile);
                }

                temp_login = "";
                file = "";
                temp_dateFile = "";
                isLogin = true;
            }
        }

        public static void CommandCMD(string command)
        {

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();

        }

        public static string LocalIp()
        {
            string localIP = "";
            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    localIP = endPoint.Address.ToString();
                }
                return localIP;
            }
            catch
            {
               return System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString();
            }
        }
        //
        //  Добавление программы в реестр
        //
        public static void BlockBrogramInRegistry(bool newListProgram, List<string> programs)
        {
            var HK_DisallowRun = VerifyExistenceAutoPlaySettings();
            if (newListProgram)
            {
                // Удаление ранее заблокированных приложений
                string[] delBlockPrograms = HK_DisallowRun.GetSubKeyNames();
                foreach (string value in delBlockPrograms)
                {
                    HK_DisallowRun.DeleteValue(value);
                }
                // Добавление программ в реестр
                foreach (string value in programs)
                    HK_DisallowRun.SetValue(value, value);
            }
        }
        public static byte[] ConvertInByteArray(string value)
        {
            byte[] bytes = new byte[value.Length];
            for (int track = 0; track < value.Length; track++)
            {
                bytes[track] = (byte)value[track];
            }
            return bytes;
        }
        public static void CheckAutoRun()
        {
            using (RegistryKey HKEY_CURRENT_USER = Registry.CurrentUser)
            {
                RegistryKey HK_Software = HKEY_CURRENT_USER.CreateSubKey("Software");
                RegistryKey HK_Microsoft = HK_Software.CreateSubKey("Microsoft");
                RegistryKey HK_Windows = HK_Microsoft.CreateSubKey("Windows");
                RegistryKey HK_CurrentVersion = HK_Windows.CreateSubKey("CurrentVersion");
                RegistryKey HK_Run = HK_CurrentVersion.CreateSubKey("Run");
                HK_Run.SetValue("Ksis", Application.ExecutablePath);
            }
        }
        //
        //  Проверка на существование программы в реестре
        //
        public static RegistryKey VerifyExistenceAutoPlaySettings()
        {
            using (RegistryKey HKEY_CURRENT_USER = Registry.CurrentUser)
            {
                RegistryKey HK_Software = HKEY_CURRENT_USER.CreateSubKey("Software");
                RegistryKey HK_Microsoft = HK_Software.CreateSubKey("Microsoft");
                RegistryKey HK_Windows = HK_Microsoft.CreateSubKey("Windows");
                RegistryKey HK_CurrentVersion = HK_Windows.CreateSubKey("CurrentVersion");
                RegistryKey HK_Policies = HK_CurrentVersion.CreateSubKey("Policies");
                RegistryKey HK_Explorer = HK_Policies.CreateSubKey("explorer");

                HK_Explorer.SetValue(_disallowRun, "1", RegistryValueKind.DWord);
                RegistryKey HK_DisallowRun = HK_Explorer.CreateSubKey(_disallowRun);

                return HK_DisallowRun;
            }

        }
        /*
            //fdgsdfgdfgdg
            //OpenSubKey
            try
            {
                RegistryKey HKEY_CURRENT_USER = Registry.CurrentUser;
                RegistryKey HK_Software = HKEY_CURRENT_USER.OpenSubKey("Software");
                RegistryKey HK_Microsoft = HK_Software.OpenSubKey("Microsoft");
                RegistryKey HK_Windows = HK_Microsoft.OpenSubKey("Windows");
                RegistryKey HK_CurrentVersion = HK_Windows.OpenSubKey("CurrentVersion");
                RegistryKey HK_Policies = HK_CurrentVersion.OpenSubKey("Policies");
                RegistryKey HK_Explorer = HK_Policies.OpenSubKey("explorer");

                RegistryKey HK_DisallowRun = HK_Explorer.OpenSubKey("DisallowRun");
                return true;
            }
            catch
            {
                return false;
            }
        }*/

        //
        //  Проверка кода ошибки
        //
        public static bool CheckCodeOperation(int value)
        {
            switch (value)
            {
                case 0:
                    Error.ClientError.Unauthorized();
                    return false;
                case 1:
                    return true;
                default:
                    Error.ServerError.UnknowError();
                    return false;
            }
        }

        /*
        private string PositionProgram()
        {
            string position = Application.ExecutablePath.ToString();
            position = position.Substring(position.IndexOf('\\'));
            return position;
        }
        */

        private static string GettingUserName(string value)
        {
            string temp = "";
            int index = 0;
            while (value[index] != '_')
            {
                temp += value[index];
                index++;
            }
            return temp;
        }
        public static List<string> SearchVideoFiles(string way, string login)
        {
            List<string> data = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(way);
            foreach (var value in dir.GetFiles("*.wmv"))
            {
                if (GettingUserName(value.Name) == login)
                {
                    data.Add(value.Name);
                }
            }
            return data;
        }

        public string PositionOS()
        {
            string system = Environment.GetFolderPath(Environment.SpecialFolder.System);
            return Path.GetPathRoot(system);
        }
        public string WayFileHostsWin() { return PositionOS() + @"Windows\System32\drivers\etc"; }

        public partial class Backup : Form
        {
            private byte[] _backupDB;
            private System.Windows.Forms.Timer _backUp_DataBase = new System.Windows.Forms.Timer();
            Security SECURITY = new Security();
            public Backup()
            {

                _backUp_DataBase.Interval = 600000;
                _backUp_DataBase.Enabled = true;
                _backUp_DataBase.Tick += (s, e) => { CreateBackUp(); };
            }


            public void CreateBackUp()
            {
                string way = Application.StartupPath + "\\data.db";
                if (File.Exists(way))
                {
                    new Thread(() =>
                    {
                        using (FileStream fileStream = File.OpenRead(way))
                        {
                            _backupDB = new byte[fileStream.Length];
                            fileStream.Read(_backupDB, 0, _backupDB.Length);
                        }
                    })
                    {
                        Priority = ThreadPriority.AboveNormal,
                        IsBackground = true,
                    }.Start();
                }
                else
                {
                    LoadBackUpDb();
                }
            }

            public void LoadBackUpDb()
            {
                if (_backupDB != null)
                {
                    new Thread(() =>
                    {
                        try
                        {
                            File.Delete("data.db");
                            File.WriteAllBytes("data.db", _backupDB);
                        }
                        catch
                        {
                            MessageBox.Show("В момент восстановления базы данных возникла ошибка", "Фатальная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    })
                    {
                        Priority = ThreadPriority.Normal,
                    }.Start();
                }
                else
                {
                    MessageBox.Show("Резервная копия базы данных отсутствует.", "Фатальная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }

        }
       

        
        public partial class WorkingWithDateSystem
        {
            
            private System.Windows.Forms.Timer _timeTIMER = new System.Windows.Forms.Timer();

            [StructLayout(LayoutKind.Sequential)]
            public struct SystemTime
            {
                public short Year;
                public short Month;
                public short DayOfWeek;
                public short Day;
                public short Hour;
                public short Minute;
                public short Second;
            }
            SystemTime sysTime = new SystemTime();

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool SetSystemTime([In] ref SystemTime sysTime);

            private int HourNotNull(int hour)
            {

                if (hour <= 6)
                {
                    hour = 17 + hour;
                }
                else if (hour == 0)
                {
                    hour = 17;
                }
                else
                {
                    hour = hour - 7;
                }

                return hour;
            }
            public WorkingWithDateSystem(int year, int month, int day, int hour, int minute, int second)
            {
                OputputNewSystemTineInFirstStartFunction(year, month, day, HourNotNull(hour), minute, second);

                _timeTIMER.Enabled = true;
                _timeTIMER.Interval = 1000;
                _timeTIMER.Tick += _timeTIMER_Tick;
            }


            private void _timeTIMER_Tick(object sender, EventArgs e)
            {
                OutputNewSystemTime(sysTime.Year, sysTime.Month, sysTime.Day, sysTime.Hour, sysTime.Minute, sysTime.Second);
            }

            private void OputputNewSystemTineInFirstStartFunction(int year, int month, int day, int hour, int minute, int second)
            {
                sysTime.Year = Convert.ToInt16(year);
                sysTime.Month = Convert.ToInt16(month);
                sysTime.Day = Convert.ToInt16(day);
                sysTime.Hour = Convert.ToInt16(hour);
                sysTime.Minute = Convert.ToInt16(minute);
                sysTime.Second = Convert.ToInt16(second);
                SetSystemTime(ref sysTime);
            }

            public void OutputNewSystemTime(int year, int month, int day, int hour, int minute, int second)
            {
                DateTime dt = new DateTime(year, month, day, hour, minute, second);

                dt = dt.AddSeconds(1);
                
                sysTime.Year = Convert.ToInt16(dt.Year);
                sysTime.Month = Convert.ToInt16(dt.Month); 
                sysTime.Day = Convert.ToInt16(dt.Day);
                sysTime.Hour = Convert.ToInt16(dt.Hour);
                sysTime.Minute = Convert.ToInt16(dt.Minute);
                sysTime.Second = Convert.ToInt16(dt.Second);
                SetSystemTime(ref sysTime);
                
            }
        }
    }
}
