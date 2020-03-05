using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Dapper;
using ErrorInPrograms;
using SecurityModule;
using System.IO;
using System.Threading;

namespace SqlQueryProcessing
{
    public class Sql
    {
        Security SECURITY = new Security();
        Security.Backup SECURITY_BACKUP = new Security.Backup();
        //
        //  SQLite
        //
        //private SQLiteConnection _connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);


        private SQLiteConnection _connect = new SQLiteConnection($"Data Source=.\\data.db; Version=3");

        public void Connect() => _connect.Open();
        // Масимальная длина отправляемого текста в ячейке
        private const int _maxLengthTextInCell = 254;

        private int UnknowErrorReturnInteger()
        {
            _connect.Close();
            Error.ServerError.UnknowError();
            SECURITY_BACKUP.LoadBackUpDb();
            return -1;
        }

        private List<string> UnknowErrorReturnListString()
        {
            _connect.Close();
            Error.ServerError.UnknowError();
            
            SECURITY_BACKUP.LoadBackUpDb();
            return new List<string> { };
        }

        #region SQL select operations
        public List<string> LoadByDay(string login, int weekday)
        {

            try
            {
                using (SQLiteCommand sQLiteCommand = new SQLiteCommand("SELECT TimeStart, TimeEnd FROM ByDay WHERE Login=@login AND Weekday=@weekday", _connect))
                {
                    _connect.Open();
                    List<string> data = new List<string>();
                    /*
                                 switch (weekday)
            {
                case 1:
                    week = "monday_";
                    break;
                case 2:
                    week = "tuesday_";
                    break;
                // Среда
                case 3:
                    week = "environs_";
                    break;
                // Четверг
                case 4:
                    week = "thursday_";
                    break;
                // Пятница
                case 5:
                    week = "friday_";
                    break;
                // Суббота
                case 6:
                    week = "saturday_";
                    break;
                // Воскресенье
                case 7:
                    week = "sunday_";
                    break;
            };
                     */
                    switch (weekday)
                    {
                        case 1:
                            // monday
                            sQLiteCommand.Parameters.Add("@weekday", DbType.String, _maxLengthTextInCell).Value = "monday";
                            break;
                        case 2:
                            // monday
                            sQLiteCommand.Parameters.Add("@weekday", DbType.String, _maxLengthTextInCell).Value = "tuesday";
                            break;
                        case 3:
                            // monday
                            sQLiteCommand.Parameters.Add("@weekday", DbType.String, _maxLengthTextInCell).Value = "environs";
                            break;
                        case 4:
                            // monday
                            sQLiteCommand.Parameters.Add("@weekday", DbType.String, _maxLengthTextInCell).Value = "thursday";
                            break;
                        case 5:
                            // monday
                            sQLiteCommand.Parameters.Add("@weekday", DbType.String, _maxLengthTextInCell).Value = "friday";
                            break;
                        case 6:
                            // monday
                            sQLiteCommand.Parameters.Add("@weekday", DbType.String, _maxLengthTextInCell).Value = "saturday";
                            break;
                        case 7:
                            // monday
                            sQLiteCommand.Parameters.Add("@weekday", DbType.String, _maxLengthTextInCell).Value = "sunday";
                            break;
                    };
                    sQLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;

                    using (SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader())
                    {

                        if (sQLiteDataReader.HasRows)
                        {
                            while (sQLiteDataReader.Read())
                            {
                                data.Add(sQLiteDataReader["TimeStart"].ToString());
                                data.Add(sQLiteDataReader["TimeEnd"].ToString());
                            }
                            /*
                            for (int track = 0; track < sQLiteDataReader.FieldCount; track++)
                            {
                                MessageBox.Show(sQLiteDataReader[track].ToString());
                                data.Add(sQLiteDataReader[track].ToString());
                            }*/
                        }
                    }
                    _connect.Close();
                    return data;
                }
            }
            catch
            {
                return UnknowErrorReturnListString();
            }
        }
        public List<string> UserAuthorization(string login, string password)
        {
            
            try
            {
                using (SQLiteCommand sQLiteCommand = new SQLiteCommand("SELECT * FROM Users WHERE Login=@login AND Password=@password", _connect))
                {

                    //_connect.ChangePassword("");
                    _connect.Open();
                    //MessageBox.Show(sQLiteCommand.CommandText);
                    sQLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    sQLiteCommand.Parameters.Add("@password", DbType.String, _maxLengthTextInCell).Value = password;
                    List<string> data = new List<string>();

                    using (SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader())
                    {

                        if (sQLiteDataReader.HasRows)
                        {
                            sQLiteDataReader.Read();
                            for (int track = 0; track < sQLiteDataReader.FieldCount; track++)
                            {
                                data.Add(sQLiteDataReader[track].ToString());
                            }
                        }
                    }
                    //_connect.ChangePassword(_passwordDb);
                    _connect.Close();
                return data;

                }
            }
            catch
            {
                _connect.Close();
                SECURITY_BACKUP.LoadBackUpDb();
                Error.ServerError.UnknowError();
                return new List<string> { "*ERROR*" };
            }
        }

        public int CheckForUniqueLogin(string login)
        {
            try
            {
                using (SQLiteCommand sQLiteCommand = new SQLiteCommand("SELECT COUNT (*) FROM Users WHERE Login=@login", _connect))
                {
                    _connect.Open();
                    int codeOperation = 0;
                    sQLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    using (SQLiteDataReader sQLiteData = sQLiteCommand.ExecuteReader())
                    {
                        codeOperation = (sQLiteData.HasRows) ? 0 : 1;
                    }
                    _connect.Close();
                    return codeOperation;
                }
            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }

        public List<string> SelectTimeUsers(string login)
        {
            List<string> data = new List<string>();
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("SELECT TimeWorkPc, ByDay, Block, LastEnter, AllTime FROM Users WHERE Login=@login", _connect))
                {
                    _connect.Open();

                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    using (SQLiteDataReader sQLiteData = qLiteCommand.ExecuteReader())
                    {

                        if (sQLiteData.HasRows)
                        {
                            sQLiteData.Read();
                            for (int track = 0; track < sQLiteData.FieldCount; track++)
                                data.Add(sQLiteData[track].ToString());
                        }
                    }

                    _connect.Close();
                    return data;
                }
            }
            catch
            {
                return UnknowErrorReturnListString();
            }
        }

        public List<string> SelectBlockUrl(string login)
        {
            List<string> data = new List<string>();
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("SELECT Url FROM BlockUrl WHERE Login=@login", _connect))
                {
                    _connect.Open();

                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    using (SQLiteDataReader sQLiteData = qLiteCommand.ExecuteReader())
                    {

                        if (sQLiteData.HasRows)
                        {
                            while (sQLiteData.Read())
                            {
                                data.Add(sQLiteData[0].ToString());
                            }
                        }
                    }
                    _connect.Close();
                    return data;
                }
            }
            catch
            {
                return UnknowErrorReturnListString();
            }
        }

        public List<string> ListChild()
        {
            List<string> data = new List<string>();
            try
            {
                using (SQLiteCommand sQLiteCommand = new SQLiteCommand("SELECT Login FROM Users WHERE Access=0", _connect))
                {
                    _connect.Open();

                    using (SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader())
                    {

                        if (sQLiteDataReader.HasRows)
                        {
                            while (sQLiteDataReader.Read())
                            {
                                data.Add(sQLiteDataReader[0].ToString());
                            }
                        }
                    }
                    _connect.Close();
                    return data;
                }
            }
            catch
            {
                return UnknowErrorReturnListString();
            }
        }

        public int CheckingLoginForUniqueness(string login)
        {
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("SELECT Login FROM Users WHERE Login='@login'", _connect))
                {
                    _connect.Open();
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    using (SQLiteDataReader sqlDataReader = qLiteCommand.ExecuteReader())
                    {

                        if (sqlDataReader.HasRows)
                        {
                            sqlDataReader.Read();
                            _connect.Close();

                            return 0;
                        }
                        else
                        {
                            _connect.Close();
                            return 1;
                        }
                    }
                }
            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }

        public System.Drawing.Image image(string login)
        {
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("SELECT Avatar FROM Users WHERE Login=@login", _connect))
                {
                    System.Drawing.Image img;
                    _connect.Open();

                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    using (SQLiteDataReader sqlDataReader = qLiteCommand.ExecuteReader())
                    {

                        sqlDataReader.Read();

                        MemoryStream image_stream = new MemoryStream((byte[])sqlDataReader[0]);
                        img = System.Drawing.Image.FromStream(image_stream);

                        _connect.Close();
                        return img;
                    }
                }
            }
            catch
            {
                _connect.Close();
                SECURITY_BACKUP.LoadBackUpDb();
                Error.ServerError.UnknowError();
                System.Drawing.Image image = Properties.Resources.errorPicture;
                return image;
            }
        }
        public System.Drawing.Image BackGround(string login)
        {
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("SELECT BackImage FROM Users WHERE Login=@login", _connect))
                {
                    System.Drawing.Image img;
                    _connect.Open();

                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    using (SQLiteDataReader sqlDataReader = qLiteCommand.ExecuteReader())
                    {

                        sqlDataReader.Read();

                        MemoryStream image_stream = new MemoryStream((byte[])sqlDataReader[0]);
                        img = System.Drawing.Image.FromStream(image_stream);

                        _connect.Close();
                        return img;
                    }

                }
            }
            catch
            {
                _connect.Close();
                SECURITY_BACKUP.LoadBackUpDb();
                Error.ServerError.UnknowError();
                System.Drawing.Image image = Properties.Resources.errorPicture;
                return image;
            }
        }
        public List<string> SelectBlockUrlChild(string login)
        {
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("SELECT Url FROM BlockUrl WHERE Login=@login", _connect))
                {
                    _connect.Open();
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    using (SQLiteDataReader sqlDataReader = qLiteCommand.ExecuteReader())
                    {
                        List<string> url = new List<string>();
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                url.Add(sqlDataReader[0].ToString());
                            }
                        }
                        _connect.Close();
                        return url;
                    }
                }
            }
            catch
            {
                return UnknowErrorReturnListString();

            }
        }

        public List<byte[]> SelectScreenShotsUser(string login, string date)
        {
            List<byte[]> data = new List<byte[]>();

            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("SELECT Time, Picture, Id FROM ScreenShots WHERE Login=@login AND Day=@day", _connect))
                {
                    _connect.Open();
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    qLiteCommand.Parameters.Add("@day", DbType.String, _maxLengthTextInCell).Value = date;
                    using (SQLiteDataReader sqlDataReader = qLiteCommand.ExecuteReader())
                    {

                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                data.Add(Security.ConvertInByteArray(sqlDataReader[0].ToString()));
                                data.Add((byte[])sqlDataReader[1]);
                                data.Add(Security.ConvertInByteArray(sqlDataReader[2].ToString()));
                            }
                        }
                        _connect.Close();
                        return data;
                    }

                }
            }
            catch
            {
                _connect.Close();
                SECURITY_BACKUP.LoadBackUpDb();
                Error.ServerError.UnknowError();
                return new List<byte[]> { };
            }
        }

        public List<string> SelectBlockPrograms(string login)
        {
            List<string> data = new List<string>();
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("SELECT Program FROM BlockProgram WHERE Login=@login", _connect))
                {
                    _connect.Open();
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    using (SQLiteDataReader sqlDataReader = qLiteCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                data.Add(sqlDataReader[0].ToString());
                            }
                        }
                        _connect.Close();
                        return data;
                    }
                }
            }
            catch
            {
                return UnknowErrorReturnListString();
            }
        }

        public List<int> SelectPositionScreenShot(string login)
        {
            List<int> data = new List<int>();
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("SELECT ScreenShot, TimeOutScreenShot FROM Users WHERE Login=@login", _connect))
                {
                    _connect.Open();
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    using (SQLiteDataReader sqlDataReader = qLiteCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            sqlDataReader.Read();
                            data.Add(Convert.ToInt32(sqlDataReader[0]));
                            data.Add(Convert.ToInt32(sqlDataReader[1]));
                        }
                    }
                    _connect.Close();
                    return data;
                }
            }
            catch
            {
                _connect.Close();
                SECURITY_BACKUP.LoadBackUpDb();
                Error.ServerError.UnknowError();
                return data;
            }
        }

        

        public int SelectVideoPlayUser(string login)
        {
            int data = -1;
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("SELECT RecordVideo FROM Users WHERE Login=@login", _connect))
                {
                    _connect.Open();
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    using (SQLiteDataReader sqlDataReader = qLiteCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            sqlDataReader.Read();
                            data = Convert.ToInt32(sqlDataReader[0]);
                        }
                        sqlDataReader.Close();
                    }
                    _connect.Close();
                    return data;
                }
            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }

        public List<string> LoadDataChild(string login)
        {
            List<string> data = new List<string>();
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("SELECT Password, Name, Surname, Block, SoundEffect, Inaction, ByDay FROM Users WHERE Login=@login", _connect))
                {
                    _connect.Open();
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    using (SQLiteDataReader sqlDataReader = qLiteCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            sqlDataReader.Read();

                            for (int track = 0; track < sqlDataReader.FieldCount; track++)
                                data.Add(sqlDataReader[track].ToString());
                        }
                        sqlDataReader.Close();
                    }
                    _connect.Close();
                    return data;
                }
            }
            catch
            {
                return UnknowErrorReturnListString();
            }
        }

        public List<string> LoadBlockProgramUser(string login)
        {
            List<string> data = new List<string>();
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("SELECT Program, Way FROM BlockProgram WHERE Login=@login", _connect))
                {
                    _connect.Open();
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    using (SQLiteDataReader sqlDataReader = qLiteCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {

                            while (sqlDataReader.Read())
                            {

                                for (int track = 0; track < sqlDataReader.FieldCount; track++)
                                    data.Add(sqlDataReader[track].ToString());
                            }
                        }
                    }
                    _connect.Close();
                    return data;
                }
            }
            catch
            {
                return UnknowErrorReturnListString();
            }
        }

        
        public List<string> SelectSecretTextAndWord(string login)
        {
            List<string> data = new List<string>();
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("SELECT SecretText, SecretWord FROM Users WHERE Login=@login", _connect))
                {
                    _connect.Open();
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    using (SQLiteDataReader sqlDataReader = qLiteCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {

                            while (sqlDataReader.Read())
                            {
                                data.Add(sqlDataReader[0].ToString());
                                data.Add(sqlDataReader[1].ToString());
                            }
                        }
                        _connect.Close();
                    }

                    _connect.Close();
                    return data;
                }
            }
            catch
            {
                return UnknowErrorReturnListString();
            }
        }

       
        public List<string> SelectMessageChild(string login)
        {
            List<string> data = new List<string>();
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("SELECT LoginFrom, Date, Message FROM Message WHERE LoginIn=@login", _connect))
                {
                    _connect.Open();
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    using (SQLiteDataReader sqlDataReader = qLiteCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {

                            while (sqlDataReader.Read())
                            {
                                data.Add(sqlDataReader[0].ToString());
                                data.Add(sqlDataReader[1].ToString());
                                data.Add(sqlDataReader[2].ToString());
                            }
                        }
                        _connect.Close();
                    }

                    _connect.Close();
                    return data;
                }
            }
            catch
            {
                return UnknowErrorReturnListString();
            }
        }

        public List<int> SelectTimeWeekdayChild(string login, string weekday)
        {
            List<int> data = new List<int>();
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("SELECT TimeStart, TimeEnd FROM ByDay WHERE Login=@login AND Weekday=@weekday", _connect))
                {
                    _connect.Open();
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    qLiteCommand.Parameters.Add("@weekday", DbType.String, _maxLengthTextInCell).Value = weekday;
                    using (SQLiteDataReader sqlDataReader = qLiteCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {

                            while (sqlDataReader.Read())
                            {
                                data.Add(Convert.ToInt32(sqlDataReader[0]));
                                data.Add(Convert.ToInt32(sqlDataReader[1]));

                            }
                        }
                        _connect.Close();
                    }

                    _connect.Close();
                    return data;
                }
            }
            catch
            {
                _connect.Close();
                SECURITY_BACKUP.LoadBackUpDb();
                Error.ServerError.UnknowError();
                return data;
            }
        }
        #endregion

        #region SQL update operations

        public int SaveNewSettingUser(string login, string newLogin, string fontFamily, string fontSize, string colorProgram, int inaction, int alertExpirationTime,
            int maxVolume, int soundEffects, int eyeProblems)
        {
            try
            {
                using (SQLiteCommand sQLiteCommand = new SQLiteCommand("UPDATE Users SET Login=@newLogin, FontFamily=@fontFamily, " +
                        "FontSize=@fontFize, ColorProgram=@colorProgram, Inaction=@inaction, AlertExpirationTime=@alertExpirationTime, " +
                        "MaxVolume=@maxVolume, SoundEffect=@soundEffect, EyeProblems=@eyeProblems WHERE Login=@login", _connect))
                {
                    _connect.Open();

                    sQLiteCommand.Parameters.Add("@newLogin", DbType.String, _maxLengthTextInCell).Value = newLogin;
                    sQLiteCommand.Parameters.Add("@fonFamily", DbType.String, _maxLengthTextInCell).Value = fontFamily;
                    sQLiteCommand.Parameters.Add("@fontSize", DbType.Int32, _maxLengthTextInCell).Value = fontSize;
                    sQLiteCommand.Parameters.Add("@colorProgram", DbType.String, _maxLengthTextInCell).Value = colorProgram;
                    sQLiteCommand.Parameters.Add("@inaction", DbType.Int32, _maxLengthTextInCell).Value = inaction;
                    sQLiteCommand.Parameters.Add("@alertExpirationTime", DbType.Int32, _maxLengthTextInCell).Value = alertExpirationTime;
                    sQLiteCommand.Parameters.Add("@maxVolume", DbType.Int32, _maxLengthTextInCell).Value = maxVolume;
                    sQLiteCommand.Parameters.Add("@soundEffect", DbType.Int32, _maxLengthTextInCell).Value = soundEffects;
                    sQLiteCommand.Parameters.Add("@eyeProblems", DbType.Int32, _maxLengthTextInCell).Value = eyeProblems;
                    sQLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    int codeOperation = sQLiteCommand.ExecuteNonQuery();

                    _connect.Close();
                    return codeOperation;
                }

            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }

        public int UpdateLastEnter(string login, string lastEnter)
        {
            try
            {
                using (SQLiteCommand sQLiteCommand = new SQLiteCommand("UPDATE Users SET TimeWorkPc=AllTime, LastEnter=@lastEnter WHERE Login=@login", _connect))
                {
                    _connect.Open();

                    sQLiteCommand.Parameters.Add("@lastEnter", DbType.String, _maxLengthTextInCell).Value = lastEnter;
                    sQLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    int codeOperation = sQLiteCommand.ExecuteNonQuery();

                    _connect.Close();
                    if (codeOperation == 1)
                    {
                        var data = SelectTimeUsers(login);
                        if (data.Count != 0)
                        {
                            return Convert.ToInt32(data[0]);
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -2;
                    }

                }
            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }

        public int UpdateTimeWorkPc(string login, int timeWorkPc)
        {
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("UPDATE Users SET TimeWorkPc=@timeWorkPc WHERE Login=@login", _connect))
                {
                    _connect.Open();

                    qLiteCommand.Parameters.Add("@timeWorkPc", DbType.Int32, _maxLengthTextInCell).Value = timeWorkPc;
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;

                    int codeOperation = qLiteCommand.ExecuteNonQuery();

                    _connect.Close();
                    return codeOperation;
                }
            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }

        public int SaveDataUser(string login, string newLogin, string password, string secretText, string secretWord, string fontFamily, int fontSize, string fontStyle, string colorProgram, int inaction, int soundEffect,
    int maxVolume, int eyeProblems, byte[] avatar, string avatarFormat, byte[] backImage, string backImageFormat, bool isParance)
        {
            try
            {
                //string avaByte = Encoding.ASCII.GetString(avatar);
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("UPDATE Users SET Login=@newLogin, Password=@password, SecretText=@secretText, SecretWord=@secretWord, FontFamily=@fontFamily, FontSize=@fontSize, FontStyle=@fontStyle, " +
                    "ColorProgram=@colorProgram, Inaction=@inaction, SoundEffect=@soundEffect, MaxVolume=@maxVolume, EyeProblems=@eyeProblems,  Avatar=@avatar, AvatarFormat=@avatarFormat, BackImage=@backImage, BackImageFormat=@backImageFormat WHERE Login=@login\n", _connect))
                {
                    _connect.Open();
                    qLiteCommand.Parameters.Add("@newLogin", DbType.String, _maxLengthTextInCell).Value = newLogin;
                    qLiteCommand.Parameters.Add("@password", DbType.String, _maxLengthTextInCell).Value = password;
                    qLiteCommand.Parameters.Add("@secretText", DbType.String, _maxLengthTextInCell).Value = secretText;
                    qLiteCommand.Parameters.Add("@secretWord", DbType.String, _maxLengthTextInCell).Value = secretWord;
                    qLiteCommand.Parameters.Add("@fontFamily", DbType.String, _maxLengthTextInCell).Value = fontFamily;
                    qLiteCommand.Parameters.Add("@FontStyle", DbType.String, _maxLengthTextInCell).Value = fontStyle;
                    qLiteCommand.Parameters.Add("@fontSize", DbType.Int32, _maxLengthTextInCell).Value = fontSize;
                    qLiteCommand.Parameters.Add("@colorProgram", DbType.String, _maxLengthTextInCell).Value = colorProgram;
                    qLiteCommand.Parameters.Add("@inaction", DbType.Int32, _maxLengthTextInCell).Value = inaction;
                    qLiteCommand.Parameters.Add("@soundEffect", DbType.Int32, _maxLengthTextInCell).Value = soundEffect;
                    qLiteCommand.Parameters.Add("@maxVolume", DbType.Int32, _maxLengthTextInCell).Value = maxVolume;
                    qLiteCommand.Parameters.Add("@eyeProblems", DbType.Int32, _maxLengthTextInCell).Value = eyeProblems;
                    qLiteCommand.Parameters.Add("@avatar", DbType.Binary, _maxLengthTextInCell).Value = avatar;
                    qLiteCommand.Parameters.Add("@avatarFormat", DbType.String, _maxLengthTextInCell).Value = avatarFormat;
                    qLiteCommand.Parameters.Add("@backImage", DbType.Binary, _maxLengthTextInCell).Value = backImage;
                    qLiteCommand.Parameters.Add("@backImageFormat", DbType.String, _maxLengthTextInCell).Value = backImageFormat;
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;

                    int codeOperation = qLiteCommand.ExecuteNonQuery();
                    if (codeOperation == 1)
                    {
                        if (isParance)
                        {
                            string updateTables = "UPDATE Message SET LoginFrom='" + newLogin + "' WHERE LoginFrom='" + login + "'\n";
                            qLiteCommand.CommandText = updateTables;
                            qLiteCommand.Connection = _connect;
                        }
                        else
                        {
                            // Бд
                            string updateTables = "UPDATE Message SET LoginIn='" + newLogin + "' WHERE LoginIn='" + login + "'\n";
                            updateTables += "UPDATE ByDay SET Login='" + newLogin + "' WHERE Login='" + login + "'\n";
                            updateTables += "UPDATE BlockUrl SET Login='" + newLogin + "' WHERE Login='" + login + "'\n";
                            updateTables += "UPDATE ScreenShots SET Login='" + newLogin + "' WHERE Login='" + login + "'\n";
                            // Обновление названия видео
                            Security.UpdateNameVideo(login, newLogin);
                        }
                        codeOperation = qLiteCommand.ExecuteNonQuery();
                    }
                    _connect.Close();
                    return codeOperation;
                }
            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }

        public int SaveDataScreenShot(string login, bool screenShot, int timeOutScreenShot)
        {
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("UPDATE Users SET ScreenShot=@screenShot, TimeOutScreenShot=@timeOutScreenShot WHERE Login=@login", _connect))
                {
                    _connect.Open();

                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    qLiteCommand.Parameters.Add("@screenShot", DbType.Int32, _maxLengthTextInCell).Value = (screenShot) ? 1 : 0;
                    qLiteCommand.Parameters.Add("@timeOutScreenShot", DbType.Int32, _maxLengthTextInCell).Value = timeOutScreenShot;

                    int codeOperation = qLiteCommand.ExecuteNonQuery();

                    _connect.Close();
                    return codeOperation;
                }
            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }

        public int UpdatePassword(string login, string password)
        {
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("UPDATE Users SET Password=@password WHERE Login=@login", _connect))
                {
                    _connect.Open();

                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    qLiteCommand.Parameters.Add("@password", DbType.String, _maxLengthTextInCell).Value = password;

                    int codeOperation = qLiteCommand.ExecuteNonQuery();

                    _connect.Close();
                    return codeOperation;
                }
            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }

        public int SaveDataRecordVideo(string login, int recordVideo)
        {
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("UPDATE Users SET RecordVideo=@recordVideo WHERE Login=@login", _connect))
                {
                    _connect.Open();

                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    qLiteCommand.Parameters.Add("@recordVideo", DbType.String, _maxLengthTextInCell).Value = recordVideo;

                    int codeOperation = qLiteCommand.ExecuteNonQuery();

                    _connect.Close();
                    return codeOperation;
                }
            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }

        public async Task<int> UpdateTimeChild(string login, int timeWorkPc)
        {
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("UPDATE Users SET TimeWorkPc=@timeWorckPc WHERE Login=@login", _connect))
                {

                    await _connect.OpenAsync();
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    qLiteCommand.Parameters.Add("@timeWorkPc", DbType.Int32, _maxLengthTextInCell).Value = timeWorkPc;
                    var codeOperation = qLiteCommand.ExecuteNonQueryAsync();
                    await codeOperation;
                    _connect.Close();
                    return Convert.ToInt32(codeOperation.Result);
                }
            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }

        #endregion

        #region SQL insert operations
        public int CreateMessage(string loginFrom, string LoginTo, string msg)
        {
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("INSERT INTO Message (LoginIn, LoginFrom, Message, Date) VALUES (@to, @from, @msg, @date)", _connect))
                {
                    _connect.Open();

                    qLiteCommand.Parameters.Add("@to", DbType.String, _maxLengthTextInCell).Value = LoginTo;
                    qLiteCommand.Parameters.Add("@day", DbType.String, _maxLengthTextInCell).Value = DateTime.UtcNow.ToShortDateString();
                    qLiteCommand.Parameters.Add("@form", DbType.String, _maxLengthTextInCell).Value = loginFrom;
                    qLiteCommand.Parameters.Add("@msg", DbType.String, _maxLengthTextInCell).Value = msg;

                    int codeOperation = qLiteCommand.ExecuteNonQuery();
                    _connect.Close();
                    return codeOperation;
                }
            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }
        public int SaveScreenShots(string login, byte[] picture)
        {
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("INSERT INTO ScreenShots (Login, Day, Time, Picture) VALUES(@login, @day, @time, @picture)", _connect))
                {
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    qLiteCommand.Parameters.Add("@day", DbType.String, _maxLengthTextInCell).Value = DateTime.UtcNow.ToShortDateString();
                    qLiteCommand.Parameters.Add("@time", DbType.String, _maxLengthTextInCell).Value = DateTime.UtcNow.ToShortTimeString();
                    qLiteCommand.Parameters.Add("@picture", DbType.Binary, _maxLengthTextInCell).Value = picture;


                    _connect.Open();

                    int codeOperation = qLiteCommand.ExecuteNonQuery();
                    _connect.Close();
                    return codeOperation;
                }
            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }

        public int CreateNewUserInProgram(string login, string password, string name, string surname, string secretText, string secretWord, string fontFamily,
           int fontSize, string fontStyle, string colorPrograme, int inaction, int alertExpirationTime, int maxVolume, int soundEffect, int recordVideo,
           int recordAudio, int screenShot, int timeOutScreenShot, int timeWorkPc, int byDay, int block, int blockProgram, List<string> program, int blockUrl, List<string> url, string lastEnter,
           int access, int allTime, int eyeProblems, List<string> date, byte[] avatar, string formatAvatar, byte[] background, string formatBackground)
        {
            //
            // Блокировка сайтов
            //
            string sqlCommand = "";
            foreach (string value in url)
            {
                sqlCommand += @"INSERT INTO BlockUrl (Login, Url) VALUES('" + login + "','" + value + "');\n";
            }
            //
            //  Блокировка приложений
            //
            foreach (string value in program)
            {
                sqlCommand += @"INSERT INTO BlockProgram (Login, Program) VALUES('" + login + "','" + value + "');\n";
            }
            //
            //  Блокировка по дням недели
            //
            if (byDay == 1)
            {
                string weekday = "";
                string timeStart = "";
                string timeEnd = "";
                bool isTimeStart = true;
                bool isNameWeekday = true;
                foreach (string value in date)
                {
                    for (int item = 0; item < value.Length; item++)
                    {
                        if (isNameWeekday)
                        {
                            if (value[item] == '_') isNameWeekday = false;
                            else
                            {
                                weekday += value[item];
                            }
                        }
                        else
                        {
                            if (isTimeStart)
                            {
                                if (value[item] == '-') isTimeStart = false;
                                else
                                {
                                    timeStart += value[item];
                                }
                            }
                            else
                            {
                                timeEnd += value[item];

                            }
                        }

                    }
                    sqlCommand += @"INSERT INTO ByDay ( WeekDay, Login, TimeStart, TimeEnd) VALUES('" + weekday + "','" + login + "','" + timeStart + "','" + timeEnd + "');\n";
                    weekday = "";
                    timeStart = "";
                    timeEnd = "";
                    isTimeStart = true;
                    isNameWeekday = true;
                }
            }

            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("INSERT INTO Users VALUES(@login, @password, @name, @surname, @secretText, " +
                    "@secretWord, @fontFamily, @fontSize, @fontStyle, @colorprogram, @inaction, @alertExpirationTime, " +
                    "@maxVolume, @soundEffect, @recordVideo, @recordAudio, @screenShot, @timeOutScreenShot, " +
                    "@timeWorkPc, @byDay, @block, @blockProgram, @blockUrl, @lastEnter, @access, @allTime, @eyeProblems, @avatar, @avatarFormat, @backImage, @backImageFormat);\n" + sqlCommand, _connect))
                {
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    qLiteCommand.Parameters.Add("@password", DbType.String, _maxLengthTextInCell).Value = password;
                    qLiteCommand.Parameters.Add("@name", DbType.String, _maxLengthTextInCell).Value = name;
                    qLiteCommand.Parameters.Add("@surname", DbType.String, _maxLengthTextInCell).Value = surname;
                    qLiteCommand.Parameters.Add("@secretText", DbType.String, _maxLengthTextInCell).Value = secretText;
                    qLiteCommand.Parameters.Add("@secretWord", DbType.String, _maxLengthTextInCell).Value = secretWord;
                    qLiteCommand.Parameters.Add("@fontFamily", DbType.String, _maxLengthTextInCell).Value = fontFamily;
                    qLiteCommand.Parameters.Add("@fontSize", DbType.Int32, _maxLengthTextInCell).Value = fontSize;
                    qLiteCommand.Parameters.Add("@fontStyle", DbType.String, _maxLengthTextInCell).Value = fontStyle;
                    qLiteCommand.Parameters.Add("@colorprogram", DbType.String, _maxLengthTextInCell).Value = colorPrograme;
                    qLiteCommand.Parameters.Add("@inaction", DbType.Int32, _maxLengthTextInCell).Value = inaction;
                    qLiteCommand.Parameters.Add("@alertExpirationTime", DbType.Int32, _maxLengthTextInCell).Value = alertExpirationTime;
                    qLiteCommand.Parameters.Add("@maxVolume", DbType.Int32, _maxLengthTextInCell).Value = maxVolume;
                    qLiteCommand.Parameters.Add("@soundEffect", DbType.Int32, _maxLengthTextInCell).Value = soundEffect;
                    qLiteCommand.Parameters.Add("@recordVideo", DbType.Int32, _maxLengthTextInCell).Value = recordVideo;
                    qLiteCommand.Parameters.Add("@recordAudio", DbType.Int32, _maxLengthTextInCell).Value = recordAudio;
                    qLiteCommand.Parameters.Add("@screenShot", DbType.Int32, _maxLengthTextInCell).Value = screenShot;
                    qLiteCommand.Parameters.Add("@timeOutScreenShot", DbType.Int32, _maxLengthTextInCell).Value = timeOutScreenShot;
                    qLiteCommand.Parameters.Add("@timeWorkPc", DbType.Int32, _maxLengthTextInCell).Value = timeWorkPc;
                    qLiteCommand.Parameters.Add("@byDay", DbType.Int32, _maxLengthTextInCell).Value = byDay;
                    qLiteCommand.Parameters.Add("@block", DbType.Int32, _maxLengthTextInCell).Value = block;
                    qLiteCommand.Parameters.Add("@blockProgram", DbType.Int32, _maxLengthTextInCell).Value = blockProgram;
                    qLiteCommand.Parameters.Add("@blockUrl", DbType.Int32, _maxLengthTextInCell).Value = blockUrl;
                    qLiteCommand.Parameters.Add("@lastEnter", DbType.String, _maxLengthTextInCell).Value = lastEnter;
                    qLiteCommand.Parameters.Add("@access", DbType.Int32, _maxLengthTextInCell).Value = access;
                    qLiteCommand.Parameters.Add("@allTime", DbType.Int32, _maxLengthTextInCell).Value = allTime;
                    qLiteCommand.Parameters.Add("@eyeProblems", DbType.Int32, _maxLengthTextInCell).Value = eyeProblems;
                    qLiteCommand.Parameters.Add("@avatar", DbType.Binary, _maxLengthTextInCell).Value = avatar;
                    qLiteCommand.Parameters.Add("@avatarFormat", DbType.String, _maxLengthTextInCell).Value = formatAvatar;
                    qLiteCommand.Parameters.Add("@backImage", DbType.Binary, _maxLengthTextInCell).Value = background;
                    qLiteCommand.Parameters.Add("@backImageFormat", DbType.String, _maxLengthTextInCell).Value = formatBackground;

                    _connect.Open();

                    int codeOperation = qLiteCommand.ExecuteNonQuery();
                    _connect.Close();
                    return codeOperation;
                }
            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }
        #endregion

        #region SQL delete operations
        public int DeleteInvalidProgram(string login, string way)
        {
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("DELETE FROM BlockProgram WHERE Login=@login AND Way=@way", _connect))
                {
                    _connect.Open();
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    qLiteCommand.Parameters.Add("@way", DbType.String, _maxLengthTextInCell).Value = way;
                    qLiteCommand.ExecuteNonQuery();

                    int codeOperation = qLiteCommand.ExecuteNonQuery();
                    _connect.Close();
                    return codeOperation;
                }
            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }

        public int DeleteScreenShot(string login, int id)
        {
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("DELETE FROM ScreenShots WHERE Id=@id", _connect))
                {
                    _connect.Open();
                    qLiteCommand.Parameters.Add("@id", DbType.Int32, _maxLengthTextInCell).Value = id;
                    int codeOperation = qLiteCommand.ExecuteNonQuery();

                    _connect.Close();
                    return codeOperation;
                }
            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }
        public int SaveMoreFunctionAccount(string login, List<string> data, int codeFunction)
        {
            string nameTable = (codeFunction == 0) ? "BlockUrl" : "BlockProgram";
            string command = "";
            try
            {
                using (SQLiteCommand qLiteCommand = new SQLiteCommand("DELETE FROM " + nameTable + " WHERE Login=@login", _connect))
                {
                    _connect.Open();
                    qLiteCommand.Parameters.Add("@login", DbType.String, _maxLengthTextInCell).Value = login;
                    qLiteCommand.ExecuteNonQuery();
                    if (codeFunction == 0)
                    {
                        foreach (string value in data)
                        {
                            command += "INSERT INTO BlockUrl (Login, Url) VALUES('" + login + "', '" + value + "');\n";
                        }
                    }
                    else
                    {
                        for (int track = 0; track < data.Count; track += 2)
                        {
                            command += "INSERT INTO BlockProgram (Login, Program, Way) VALUES('" + login + "', '" + data[track] + "','" + data[track + 1] + "');\n";
                        }
                    }
                    qLiteCommand.CommandText = command;
                    int codeOperation = qLiteCommand.ExecuteNonQuery();
                    _connect.Close();
                    return codeOperation;
                }
            }
            catch
            {
                return UnknowErrorReturnInteger();
            }
        }
        #endregion
    }
}