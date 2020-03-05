using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using SqlQueryProcessing;
using Microsoft.Expression.Encoder;
using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.ScreenCapture;
using System.Collections.ObjectModel;

namespace VideoChannelProcessing
{
    public class RecordVideo
    {
        private static ScreenCaptureJob gotu;
        private static string _videoFormat { get; } = ".wmv";

        public static bool TestRecord(string way)
        {
            string login = "testrecord";
            try
            {
                Directory.CreateDirectory(way);
                RecordVideo REC = new RecordVideo();
                gotu = new ScreenCaptureJob();
                System.Drawing.Size workingArea = SystemInformation.WorkingArea.Size;
                Rectangle captureRect = new Rectangle(0, 0, workingArea.Width + (workingArea.Width % 2), workingArea.Height + (workingArea.Height % 2));
                gotu.CaptureRectangle = captureRect;
                gotu.ShowFlashingBoundary = true;
                gotu.ShowCountdown = true;
                gotu.CaptureMouseCursor = true;
                gotu.AddAudioDeviceSource(AudioDevices());
                way += NowDate(login);
                int trackCopy = 1;
                //
                //  Проверка на совпадение
                //
                if (File.Exists(way + _videoFormat))
                {
                    while (File.Exists(way + _videoFormat + '(' + trackCopy + ')' + _videoFormat))
                    {
                        trackCopy++;
                    }
                    way += '(' + trackCopy.ToString() + ')' + _videoFormat;
                }
                else
                {
                    way += _videoFormat;
                }
                gotu.OutputScreenCaptureFileName = string.Format(way);
                gotu.Start();
                gotu.Stop();
                return true;
            }
            catch
            {
                return false;
            }

        }
        #region Start record
        public static void StartRecord(string way, string login)
        {
            try
            {
                Directory.CreateDirectory(way);
                RecordVideo REC = new RecordVideo();
                gotu = new ScreenCaptureJob();
                System.Drawing.Size workingArea = SystemInformation.WorkingArea.Size;
                Rectangle captureRect = new Rectangle(0, 0, workingArea.Width + (workingArea.Width % 2), workingArea.Height + (workingArea.Height % 2)); ;
                gotu.CaptureRectangle = captureRect;
                gotu.ShowFlashingBoundary = true;
                gotu.ShowCountdown = true;
                gotu.CaptureMouseCursor = true;
                gotu.AddAudioDeviceSource(AudioDevices());
                way += NowDate(login);
                int trackCopy = 1;
                //
                //  Проверка на совпадение
                //
                if (File.Exists(way + _videoFormat))
                {
                    while (File.Exists(way + _videoFormat + '(' + trackCopy + ')' + _videoFormat))
                    {
                        trackCopy++;
                    }
                    way += '(' + trackCopy.ToString() + ')' + _videoFormat;
                }
                else
                {
                    way += _videoFormat;
                }
                gotu.OutputScreenCaptureFileName = string.Format(way);
                gotu.Start();
            }
            catch
            {
                MessageBox.Show("ee");
            }
        }

        private static string NowDate(string login)
        {
            string date = '\\' + login + '_' + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
            date += '_' + DateTime.UtcNow.Hour.ToString() + DateTime.UtcNow.Minute.ToString();
            return date;
        }

        private static EncoderDevice AudioDevices()
        {
            EncoderDevice encoderDevice = null;
            Collection<EncoderDevice> audioDevices = EncoderDevices.FindDevices(EncoderDeviceType.Audio);

            try
            {
                encoderDevice = audioDevices.First();
            }
            catch
            {
                //MessageBox.Show("Audio Devices Not Found " + audioDevices[0].Name + ex.Message);
            }
            return encoderDevice;
        }
        #endregion

        #region Stop record
        public static void StopRecord()
        {
            RecordVideo REC = new RecordVideo();
            if (gotu.Status == RecordStatus.Running)
            {
                    gotu.Stop();
            }
        }
        #endregion



    }

    public class ScreenShot
    {
        Sql SQL = new Sql();
        public int CreateScreenShot(string login)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                return SQL.SaveScreenShots(login, ConvertImageInBytes(bitmap));
            }
        }

        private byte[] ConvertImageInBytes(Bitmap bitmap)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    bitmap.Save(memoryStream, ImageFormat.Jpeg);
                    byte[] byteImage = memoryStream.ToArray();
                    return byteImage;
                }
            }
            catch
            {
                return new byte[] { 0x0000 };
            }
        }
    }
}
