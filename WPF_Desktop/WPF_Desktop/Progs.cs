using System;
using System.Threading;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Windows;

public class ExtractIcon
{
    [DllImport("Shell32.dll")]
    private static extern int SHGetFileInfo(
        string pszPath,
        uint dwFileAttributes,
        out     SHFILEINFO psfi,
        uint cbfileInfo,
        SHGFI uFlags
        );

    [StructLayout(LayoutKind.Sequential)]
    private struct SHFILEINFO
    {
        public SHFILEINFO(bool b)
        {
            hIcon = IntPtr.Zero;
            iIcon = 0;
            dwAttributes = 0;
            szDisplayName = "";
            szTypeName = "";
        }
        public IntPtr hIcon;
        public int iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.LPStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.LPStr, SizeConst = 80)]
        public string szTypeName;
    };

    private ExtractIcon()
    {
    }

    private enum SHGFI
    {
        SmallIcon = 0x00000001,
        LargeIcon = 0x00000000,
        Icon = 0x00000100,
        DisplayName = 0x00000200,
        Typename = 0x00000400,
        SysIconIndex = 0x00004000,
        UseFileAttributes = 0x00000010
    }

    public static Icon GetIcon(string strPath, bool bSmall)
    {
        SHFILEINFO info = new SHFILEINFO(true);
        int cbFileInfo = Marshal.SizeOf(info);
        SHGFI flags;
        if (bSmall)
        {
            flags = SHGFI.Icon | SHGFI.SmallIcon | SHGFI.UseFileAttributes;
        }
        else
        {
            flags = SHGFI.Icon | SHGFI.LargeIcon | SHGFI.UseFileAttributes;
        }
        SHGetFileInfo(strPath, 256, out   info, (uint)cbFileInfo, flags);
        return Icon.FromHandle(info.hIcon);
    }

}



//[Serializable]
class Progs : System.Windows.Controls.Image
{
    public Progs(string path, string name)
    {

        //Name
        //Content = Image.FromFile(path);

        // Image i = 
        IntPtr hBitmap = ExtractIcon.GetIcon(path, false).ToBitmap().GetHbitmap();

        ImageSource wpfBitmap =
            Imaging.CreateBitmapSourceFromHBitmap(
                 hBitmap, IntPtr.Zero, Int32Rect.Empty,
                 BitmapSizeOptions.FromEmptyOptions());
        // i.Width = 48;
        // i.Height = 48;

        // BitmapImage bi = new BitmapImage();
        // bi.BeginInit();
        // bi = ExtractIcon.GetIcon(path, false);
        // bi.UriSource = new Uri("pack://application:,,,/Resources/close_small_black.ico");
        // bi.EndInit();
        // i.Source = bi;

        this.Source = wpfBitmap;

        //Content = ExtractIcon.GetIcon(path, false).ToBitmap();
        ImagePath = path;
        
        Width = 72;
        Height = 64;
        // Background = System.Windows.Media.Brushes.Transparent;
        ToolTip = name;
        Margin = new System.Windows.Thickness(10);
        Opacity = 0.8;
        this.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(Progs_ClickThread);
        this.MouseEnter += new System.Windows.Input.MouseEventHandler(Progs_MouseEnter);
        this.MouseLeave += new System.Windows.Input.MouseEventHandler(Progs_MouseLeave);
    }

    void Progs_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        Opacity = 0.8;
    }

    void Progs_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        Opacity = 1;
    }

    void Progs_ClickThread(object sender, System.Windows.RoutedEventArgs e)
    {
        Thread t = new Thread(new ThreadStart(Progs_Click));
        t.Start();
    }

    void Progs_Click()
    {
        try
        {

            System.Diagnostics.Process.Start(ImagePath);
            ClickedTimes++;
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message);
        }
    }

    private string _path;
    private string _name;
    private uint _clicktimes;


    public uint ClickedTimes
    {
        get
        {
            return _clicktimes;
        }
        set
        {
            _clicktimes = value;
        }
    }

    public string ImagePath
    {
        get
        {
            return _path;
        }
        set
        {
            _path = value;
        }
    }

    public string DisplayName
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

}
