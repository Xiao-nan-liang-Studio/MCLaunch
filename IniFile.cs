using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Serilog;

namespace MCLaunch;

public class IniFile
{
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

    public static string ReadIniFile(string section, string key, string def, string filename)
    {
        StringBuilder sb = new StringBuilder(1024);
        _ = GetPrivateProfileString(section, key, def, sb, 1024, filename);
        return sb.ToString();
    }

    public static void WriteIniFile(string section, string key, string val, string filename)
    {
        _ = WritePrivateProfileString(section, key, val, filename);
    }
    public static void WriteLog(string msg)  
    {
        Log.Logger = new LoggerConfiguration()  
            .MinimumLevel.Debug()  
            .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", rollingInterval: RollingInterval.Day)  
            .CreateLogger();
            
        
        string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log";  //路径
        if (!Directory.Exists(filePath))  //不存在则创建
        {  
            Directory.CreateDirectory(filePath);  
        }  
        string logPath = AppDomain.CurrentDomain.BaseDirectory + "Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";  //文档路径
        try  
        {  
            using (StreamWriter sw = File.AppendText(logPath))  
            {  
                sw.Write(DateTime.Now.ToString("HH:mm:ss") + "<主线程>" + "消息：" + msg );  
                sw.WriteLine("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));  
                sw.WriteLine();  
                sw.Flush();  
                sw.Close();  
                sw.Dispose();  
            }  
        }  
        catch (IOException e)  
        {  
            using (StreamWriter sw = File.AppendText(logPath))  
            {  
                sw.WriteLine("异常：" + e.Message);  
                sw.WriteLine("时间：" + DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"));  
                sw.WriteLine("**************************************************");  
                sw.WriteLine();  
                sw.Flush();  
                sw.Close();  
                sw.Dispose();  
            }  
        }  
        
    }

    public static void WriteErrorLog(string msg)
    {
        string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log";  //路径
        if (!Directory.Exists(filePath))  //不存在则创建
        {  
            Directory.CreateDirectory(filePath);  
        }  
        string logPath = AppDomain.CurrentDomain.BaseDirectory + "Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + "error.txt";  //文档路径
        try  
        {  
            using (StreamWriter sw = File.AppendText(logPath))  
            {  
                sw.WriteLine("错误：" + msg);  
                sw.WriteLine("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));  
                sw.WriteLine("**************************************************");  
                sw.WriteLine();  
                sw.Flush();  
                sw.Close();  
                sw.Dispose();  
            }  
        }  
        catch (IOException e)  
        {  
            using (StreamWriter sw = File.AppendText(logPath))  
            {  
                sw.WriteLine("异常：" + e.Message);  
                sw.WriteLine("时间：" + DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"));  
                sw.WriteLine("**************************************************");  
                sw.WriteLine();  
                sw.Flush();  
                sw.Close();  
                sw.Dispose();  
            }  
        }  
    }

    public static void DumpLog(StreamReader r)
    {
        string line;
        while ((line = r.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
    }

}