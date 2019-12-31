/*
::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
:::: Author:            Aemon Wang :::::::::::::::::::::::::::::::::::::
:::: Date:              2010/06/20 :::::::::::::::::::::::::::::::::::::
:::: EMAIL:             gaeproxy.csharp.client@gmail.com::::::::::::::::
:::: License:           GPLv3 ::::::::::::::::::::::::::::::::::::::::::
:::: Personal HomePage: NO NO NO ::::::::::::::::::::::::::::::::
:::: Version:           Beta 1.1.0 :::::::::::::::::::::::::::::::::::::
:::: Code Bsae:         http://code.google.com/p/gaeproxycsharpclient/::
::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
::Because Of Using The Code(Modified) Of metalis.org.
::Add Their Declaration Here.
::    Copyright ?2002, The KPD-Team
::    All rights reserved.
::    http://www.mentalis.org/
::
::  Redistribution and use in source and binary forms, with or without
::  modification, are permitted provided that the following conditions
::  are met:
::
::    - Redistributions of source code must retain the above copyright
::       notice, this list of conditions and the following disclaimer. 
::
::    - Neither the name of the KPD-Team, nor the names of its contributors
::       may be used to endorse or promote products derived from this
::       software without specific prior written permission. 
::
::  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
::  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
::  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
::  FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL
::  THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
::  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
::  (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
::  SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
::  HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
::  STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
::  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
::  OF THE POSSIBILITY OF SUCH DAMAGE.
::
::initialize 
@echo off
set "workDIR=%TEMP%\aemonDIR
set "dotnetFramePath=%windir%\Microsoft.NET\FrameWork"
set "est=DO_NOT_ZT_WITHOUT_PERMISSION"
set "CSharpCompiler="
set "AddedPath="
::Get CSharp Compiler Path General csc.exe
for /F "delims=" %%v in ('dir /ad /b %dotnetFramePath%\v?.*') do (
if exist "%dotnetFramePath%\%%v\csc.exe" set "CSharpCompiler=%dotnetFramePath%\%%v\csc.exe"
if exist "%dotnetFramePath%\%%v\csc.exe" set "AddedPath=%dotnetFramePath%\%%v"
)
::Get CSharp Compiler V2.*, since only version 2.* and version 3.* are bug-free.
for /F "delims=" %%v in ('dir /ad /b %dotnetFramePath%\v2.*') do (
if exist "%dotnetFramePath%\%%v\csc.exe" set "CSharpCompiler=%dotnetFramePath%\%%v\csc.exe"
if exist "%dotnetFramePath%\%%v\csc.exe" set "AddedPath=%dotnetFramePath%\%%v"
)
::Get CSharp Compiler V3.*, since only version 2.* and version 3.* are bug-free.
for /F "delims=" %%v in ('dir /ad /b %dotnetFramePath%\v3.*') do (
if exist "%dotnetFramePath%\%%v\csc.exe" set "CSharpCompiler=%dotnetFramePath%\%%v\csc.exe"
if exist "%dotnetFramePath%\%%v\csc.exe" set "AddedPath=%dotnetFramePath%\%%v"
)
set PATH=%PATH%;%AddedPath%
if not exist %CSharpCompiler% @echo "You Should Install Dotnet FrameWork 2.* or 3.* First" && @pause && @exit
@echo USE COMPILER: %CSharpCompiler%  %AddedPath%
if exist %workDIR% rmdir /s /q %workDIR%
if not exist %workDIR% mkdir %workDIR%
< "%~f0" more +83 > "%workDIR%\aemonHTTPProxy.cs"
%CSharpCompiler% "/target:exe" "/warn:0" "/nologo" "/optimize+" "/out:%workDIR%\utility.exe" "%workDIR%\aemonHTTPProxy.cs"
if exist %workDIR%\utility.exe  %workDIR%\utility.exe prepareFiles
::Build the GUIProxy
set "PARA1=/noconfig /define:TRACE /target:winexe  /warn:0 /nologo /optimize+  "
set "REFER1=/r:System.Data.dll /r:System.dll"
set "REFER2=/r:System.Drawing.dll /r:System.Web.dll /r:System.Windows.Forms.dll /r:System.Xml.dll"
set "PARA2=/debug:pdbonly /filealign:512 /out:%workDIR%\GUIProxy.exe "
set "PARA3=%workDIR%\aemonHTTPProxy.cs  /win32icon:%workDIR%\gap.ico"
%CSharpCompiler% %PARA1% %REFER1% %REFER2% %PARA2% %PARA3%
if exist %workDIR%\utility.exe  %workDIR%\utility.exe createShortcut GUIProxy.exe
@del %workDIR%\*.cs >nul
@del %workDIR%\uti*.exe >nul
if exist "%workDIR%\GUIProxy.exe" start  %workDIR%\GUIProxy.exe
goto:eof
:CSharp Code Begin
 */

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security;
using Microsoft.Win32;

namespace GUIProxy
{
    delegate void DestroyDelegate(Client client);
    public class Program
    {
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {

                    if (process.MainModule.FileName == current.MainModule.FileName)
                    {

                        return process;
                    }
                }
            }
            return null;
        }
        public static void prepareFiles()
        {
            string exeDir = DataHelper.GetAppDirectory();
            if (File.Exists(exeDir + DataHelper.SSL_KEY) == false)
            {
                DataHelper.GenerateSSLFiles();
            }

            if (File.Exists(exeDir + DataHelper.PROXY_ICON) == false)
            {
                DataHelper.GenerateIconFile();
            }
            if (File.Exists(exeDir + DataHelper.PROXY_CONFIG) == false)
            {
                DataHelper.GenerateConfigFile();
            }
            return;
        }
        public static void createShortcut(string linkName, string exePath)
        {

            if (File.Exists(exePath) == false)
            {
                return;
            }
            DirectoryInfo info = new DirectoryInfo(exePath);
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            using (StreamWriter writer = new StreamWriter(deskDir + "\\" + linkName + ".url"))
            {
                string app = info.FullName;
                //encode whitespace
                app = app.Replace(" ", "%20");
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + app);
                writer.WriteLine("IconIndex=0");
                string icon = app.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);
                writer.Flush();
            }

            return;
        }

        public static System.Uri proxyUri;
        public static System.Uri fetchServerUri;
        static bool parse(string fetchServer)
        {

            return parse(fetchServer, "www.google.cn:80");
        }
        static bool parse(string fetchServer, string proxyToFetchServer)
        {
            if (fetchServer as object == null || proxyToFetchServer as object == null)
            {

                return false;
            }

            string gaeServer = fetchServer.Trim().ToLower();
            string proxyToGaeServer = proxyToFetchServer.Trim().ToLower();
            if (gaeServer.Length <= 0 || proxyToGaeServer.Length <= 0)
            {

                return false;
            }

            if (proxyToGaeServer.StartsWith("http://") == false)
            {
                proxyToGaeServer = "http://" + proxyToGaeServer;
            }

            if (gaeServer.StartsWith("http://") == false)
            {

                gaeServer = "http://" + gaeServer;
            }
            try
            {
                proxyUri = new Uri(proxyToGaeServer);
                fetchServerUri = new Uri(gaeServer);


            }
            catch
            {
                proxyUri = fetchServerUri = null;
                return false;
            }

            return true;
        }

        [STAThread]
        static void Main(string[] args)
        {

            //used as an utility here.
            if (args.Length == 1 || args.Length == 2)
            {
                if (args[0] == DataHelper.PROXY_COMMAND_PREPARE_FILES)
                {
                    prepareFiles();
                    return;
                }
                if (args[0] == DataHelper.PROXY_COMMAND_CREATE_SHORTCUT)
                {
                    if (File.Exists(args[1]) == false)
                    {
                        args[1] = DataHelper.GetAppDirectory() + DataHelper.PROXY_EXE_NAME;
                    }
                    createShortcut("GUIProxy", args[1]);

                    return;
                }
            }
            if (args.Length != 0)//it should not be here if used as a utility.
            {
                return;
            }
            //make sure single instance running
            if (RunningInstance() as object != null)
            {
                return;

            }
            //check for files, if not exist, regenerate it.
            Program.prepareFiles();

            //parse config file
            string[] config = DataHelper.ParseConfig();

            bool configError = false;
            int listenPort = Int32.Parse(DataHelper.PROXY_CONFIG_DFAULT_LISTEN_PORT);
            try
            {
                listenPort = Int32.Parse(config[0]);
            }
            catch
            {
                configError = true;
            }

            string fetchServer = config[1];
            if (fetchServer as object == null || fetchServer.Trim().Length <= 0)
            {
                fetchServer = DataHelper.PROXY_CONFIG_DEFAULT_FETCH_SERVER;
                configError = true;
            }
            string proxy = config[2];
            if (proxy as object == null || proxy.Trim().Length <= 0)
            {
                proxy = DataHelper.PROXY_CONFIG_DEFAULT_PROXY_SERVER;
                configError = true;
            }
            if (configError == true)
            {
                string[] temp = new string[]
                {
                    listenPort.ToString(),
                    fetchServer,
                    proxy
                };
                DataHelper.WriteConfig(temp);
            }

            bool parseCorrect = Program.parse(fetchServer, proxy);
            if (parseCorrect == false) //not valid config we restore to default
            {
                DataHelper.GenerateConfigFile();
                listenPort = Int32.Parse(DataHelper.PROXY_CONFIG_DFAULT_LISTEN_PORT);
                fetchServer = DataHelper.PROXY_CONFIG_DEFAULT_FETCH_SERVER;
                proxy = DataHelper.PROXY_CONFIG_DEFAULT_PROXY_SERVER;
                parseCorrect = Program.parse(fetchServer, proxy);//parse again
            }

            if (parseCorrect)
            {
                HTTPProxy httpProxy = new HTTPProxy(listenPort);
                HTTPProxy.urlFetchServer = fetchServerUri.AbsoluteUri;
                HTTPProxy.proxyHostToFetchServer = proxyUri.Host;
                HTTPProxy.proxyPortToFetchServer = proxyUri.Port;
                HTTPProxy.httpProxyListenPort = listenPort;
                //change IE setting here;
                BrowserProxySetting.SetProxy("127.0.0.1:" + listenPort.ToString());
                httpProxy.Start();

                //enter the GUI Loop Until To Exit
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
                BrowserProxySetting.UnsetProxy();
                return;
            }
        }

    }
    class HTTPProxy //used to store the proxy properties
    {
        public HTTPProxy()
        {
            listener = new HttpListener(this.defaultListenPort);
        }
        public HTTPProxy(int listenPort)
        {
            listener = new HttpListener(listenPort);
        }

        public void Start()
        {
            this.listener.Start();

        }
        public int defaultListenPort = Int32.Parse(DataHelper.PROXY_CONFIG_DFAULT_LISTEN_PORT);
        HttpListener listener;
        static string defaultProxyToFetchServer = DataHelper.PROXY_CONFIG_DEFAULT_PROXY_SERVER;
        public static string urlFetchServer;
        public static string proxyHostToFetchServer;
        public static int proxyPortToFetchServer;
        public static int httpProxyListenPort;
    }

    abstract class Listener : IDisposable
    {
        ///<summary>Initializes a new instance of the Listener class.</summary>
        ///<param name="Port">The port to listen on.</param>
        ///<param name="Address">The address to listen on. You can specify IPAddress.Any to listen on all installed network cards.</param>
        ///<remarks>For the security of your server, try to avoid to listen on every network card (IPAddress.Any). Listening on a local IP address is usually sufficient and much more secure.</remarks>
        public Listener(int Port, IPAddress Address)
        {
            this.Port = Port;
            this.Address = Address;
        }
        ///<summary>Gets or sets the port number on which to listen on.</summary>
        ///<value>An integer defining the port number to listen on.</value>
        ///<seealso cref ="Address"/>
        ///<exception cref="ArgumentException">The specified value is less than or equal to zero.</exception>
        protected int Port
        {
            get
            {
                return m_Port;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentException();
                m_Port = value;
                Restart();
            }
        }
        ///<summary>Gets or sets the address on which to listen on.</summary>
        ///<value>An IPAddress instance defining the IP address to listen on.</value>
        ///<seealso cref ="Port"/>
        ///<exception cref="ArgumentNullException">The specified value is null.</exception>
        protected IPAddress Address
        {
            get
            {
                return m_Address;
            }
            set
            {
                if (value as object == null)
                    throw new ArgumentNullException();
                m_Address = value;
                Restart();
            }
        }
        ///<summary>Gets or sets the listening Socket.</summary>
        ///<value>An instance of the Socket class that's used to listen for incoming connections.</value>
        ///<exception cref="ArgumentNullException">The specified value is null.</exception>
        protected Socket ListenSocket
        {
            get
            {
                return m_ListenSocket;
            }
            set
            {
                if (value as object == null)
                    throw new ArgumentNullException();
                m_ListenSocket = value;
            }
        }
        ///<summary>Gets the list of connected clients.</summary>
        ///<value>An instance of the ArrayList class that's used to store all the connections.</value>
        protected ArrayList Clients
        {
            get
            {
                return m_Clients;
            }
        }
        ///<summary>Gets a value indicating whether the Listener has been disposed or not.</summary>
        ///<value>An boolean that specifies whether the object has been disposed or not.</value>
        public bool IsDisposed
        {
            get
            {
                return m_IsDisposed;
            }
        }
        ///<summary>Starts listening on the selected IP address and port.</summary>
        ///<exception cref="SocketException">There was an error while creating the listening socket.</exception>
        public void Start()
        {
            try
            {
                ListenSocket = new Socket(Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                ListenSocket.Bind(new IPEndPoint(Address, Port));
                ListenSocket.Listen(50);
                ListenSocket.BeginAccept(new AsyncCallback(this.OnAccept), ListenSocket);
            }
            catch
            {
                ListenSocket = null;
                throw new SocketException();
            }
        }
        ///<summary>Restarts listening on the selected IP address and port.</summary>
        ///<remarks>This method is automatically called when the listening port or the listening IP address are changed.</remarks>
        ///<exception cref="SocketException">There was an error while creating the listening socket.</exception>
        protected void Restart()
        {
            //If we weren't listening, do nothing
            if (ListenSocket as object == null)
                return;
            ListenSocket.Close();
            Start();
        }
        ///<summary>Adds the specified Client to the client list.</summary>
        ///<remarks>A client will never be added twice to the list.</remarks>
        ///<param name="client">The client to add to the client list.</param>
        protected void AddClient(Client client)
        {
            if (Clients.IndexOf(client) == -1)
                Clients.Add(client);
        }
        ///<summary>Removes the specified Client from the client list.</summary>
        ///<param name="client">The client to remove from the client list.</param>
        protected void RemoveClient(Client client)
        {
            Clients.Remove(client);
        }
        ///<summary>Returns the number of clients in the client list.</summary>
        ///<returns>The number of connected clients.</returns>
        public int GetClientCount()
        {
            return Clients.Count;
        }
        ///<summary>Returns the requested client from the client list.</summary>
        ///<param name="Index">The index of the requested client.</param>
        ///<returns>The requested client.</returns>
        ///<remarks>If the specified index is invalid, the GetClientAt method returns null.</remarks>
        public Client GetClientAt(int Index)
        {
            if (Index < 0 || Index >= GetClientCount())
                return null;
            return (Client)Clients[Index];
        }
        ///<summary>Gets a value indicating whether the Listener is currently listening or not.</summary>
        ///<value>A boolean that indicates whether the Listener is currently listening or not.</value>
        public bool Listening
        {
            get
            {
                return ListenSocket as object != null;
            }
        }
        ///<summary>Disposes of the resources (other than memory) used by the Listener.</summary>
        ///<remarks>Stops listening and disposes <em>all</em> the client objects. Once disposed, this object should not be used anymore.</remarks>
        ///<seealso cref ="System.IDisposable"/>
        public void Dispose()
        {
            if (IsDisposed)
                return;
            while (Clients.Count > 0)
            {
                ((Client)Clients[0]).Dispose();
            }
            try
            {
                ListenSocket.Shutdown(SocketShutdown.Both);
            }
            catch { }
            if (ListenSocket as object != null)
                ListenSocket.Close();
            m_IsDisposed = true;
        }
        ///<summary>Finalizes the Listener.</summary>
        ///<remarks>The destructor calls the Dispose method.</remarks>
        ~Listener()
        {
            Dispose();
        }
        ///<summary>Returns an external IP address of this computer, if present.</summary>
        ///<returns>Returns an external IP address of this computer; if this computer does not have an external IP address, it returns the first local IP address it can find.</returns>
        ///<remarks>If this computer does not have any configured IP address, this method returns the IP address 0.0.0.0.</remarks>
        public static IPAddress GetLocalExternalIP()
        {
            try
            {
                IPHostEntry he = Dns.Resolve(Dns.GetHostName());
                for (int Cnt = 0; Cnt < he.AddressList.Length; Cnt++)
                {
                    if (IsRemoteIP(he.AddressList[Cnt]))
                        return he.AddressList[Cnt];
                }
                return he.AddressList[0];
            }
            catch
            {
                return IPAddress.Any;
            }
        }
        ///<summary>Checks whether the specified IP address is a remote IP address or not.</summary>
        ///<param name="IP">The IP address to check.</param>
        ///<returns>True if the specified IP address is a remote address, false otherwise.</returns>
        protected static bool IsRemoteIP(IPAddress IP)
        {
            byte First = (byte)Math.Floor((double)IP.Address % 256);
            byte Second = (byte)Math.Floor((double)(IP.Address % 65536) / 256);
            //Not 10.x.x.x And Not 172.16.x.x <-> 172.31.x.x And Not 192.168.x.x
            //And Not Any And Not Loopback And Not Broadcast
            return (First != 10) &&
                (First != 172 || (Second < 16 || Second > 31)) &&
                (First != 192 || Second != 168) &&
                (!IP.Equals(IPAddress.Any)) &&
                (!IP.Equals(IPAddress.Loopback)) &&
                (!IP.Equals(IPAddress.Broadcast));
        }
        ///<summary>Checks whether the specified IP address is a local IP address or not.</summary>
        ///<param name="IP">The IP address to check.</param>
        ///<returns>True if the specified IP address is a local address, false otherwise.</returns>
        protected static bool IsLocalIP(IPAddress IP)
        {
            byte First = (byte)Math.Floor((double)IP.Address % 256);
            byte Second = (byte)Math.Floor((double)(IP.Address % 65536) / 256);
            //10.x.x.x Or 172.16.x.x <-> 172.31.x.x Or 192.168.x.x
            return (First == 10) ||
                (First == 172 && (Second >= 16 && Second <= 31)) ||
                (First == 192 && Second == 168);
        }
        ///<summary>Returns an internal IP address of this computer, if present.</summary>
        ///<returns>Returns an internal IP address of this computer; if this computer does not have an internal IP address, it returns the first local IP address it can find.</returns>
        ///<remarks>If this computer does not have any configured IP address, this method returns the IP address 0.0.0.0.</remarks>
        public static IPAddress GetLocalInternalIP()
        {
            try
            {
                IPHostEntry he = Dns.Resolve(Dns.GetHostName());
                for (int Cnt = 0; Cnt < he.AddressList.Length; Cnt++)
                {
                    if (IsLocalIP(he.AddressList[Cnt]))
                        return he.AddressList[Cnt];
                }
                return he.AddressList[0];
            }
            catch
            {
                return IPAddress.Any;
            }
        }
        ///<summary>Called when there's an incoming client connection waiting to be accepted.</summary>
        ///<param name="ar">The result of the asynchronous operation.</param>
        public abstract void OnAccept(IAsyncResult ar);
        ///<summary>Returns a string representation of this object.</summary>
        ///<returns>A string with information about this object.</returns>
        public override abstract string ToString();
        ///<summary>Returns a string that holds all the construction information for this object.</summary>
        ///<value>A string that holds all the construction information for this object.</value>
        public abstract string ConstructString { get; }
        // private variables
        /// <summary>Holds the value of the Port property.</summary>
        private int m_Port;
        /// <summary>Holds the value of the Address property.</summary>
        private IPAddress m_Address;
        /// <summary>Holds the value of the ListenSocket property.</summary>
        private Socket m_ListenSocket;
        /// <summary>Holds the value of the Clients property.</summary>
        private ArrayList m_Clients = new ArrayList();
        /// <summary>Holds the value of the IsDisposed property.</summary>
        private bool m_IsDisposed = false;
    }
    abstract class Client : IDisposable
    {
        ///<summary>Initializes a new instance of the Client class.</summary>
        ///<param name="ClientSocket">The <see cref ="Socket">Socket</see> connection between this proxy server and the local client.</param>
        ///<param name="Destroyer">The callback method to be called when this Client object disconnects from the local client and the remote server.</param>
        public Client(Socket ClientSocket, DestroyDelegate Destroyer)
        {
            this.ClientSocket = ClientSocket;
            this.Destroyer = Destroyer;
        }
        ///<summary>Initializes a new instance of the Client object.</summary>
        ///<remarks>Both the ClientSocket property and the DestroyDelegate are initialized to null.</remarks>
        public Client()
        {
            this.ClientSocket = null;
            this.Destroyer = null;
        }
        ///<summary>Gets or sets the Socket connection between the proxy server and the local client.</summary>
        ///<value>A Socket instance defining the connection between the proxy server and the local client.</value>
        ///<seealso cref ="DestinationSocket"/>
        internal Socket ClientSocket
        {
            get
            {
                return m_ClientSocket;
            }
            set
            {
                if (m_ClientSocket as object != null)
                    m_ClientSocket.Close();
                m_ClientSocket = value;
            }
        }
        ///<summary>Gets or sets the Socket connection between the proxy server and the remote host.</summary>
        ///<value>A Socket instance defining the connection between the proxy server and the remote host.</value>
        ///<seealso cref ="ClientSocket"/>
        internal Socket DestinationSocket
        {
            get
            {
                return m_DestinationSocket;
            }
            set
            {
                if (m_DestinationSocket as object != null)
                    m_DestinationSocket.Close();
                m_DestinationSocket = value;
            }
        }
        ///<summary>Gets the buffer to store all the incoming data from the local client.</summary>
        ///<value>An array of bytes that can be used to store all the incoming data from the local client.</value>
        ///<seealso cref ="RemoteBuffer"/>
        protected byte[] Buffer
        {
            get
            {
                return m_Buffer;
            }
        }
        ///<summary>Gets the buffer to store all the incoming data from the remote host.</summary>
        ///<value>An array of bytes that can be used to store all the incoming data from the remote host.</value>
        ///<seealso cref ="Buffer"/>
        protected byte[] RemoteBuffer
        {
            get
            {
                return m_RemoteBuffer;
            }
        }
        ///<summary>Disposes of the resources (other than memory) used by the Client.</summary>
        ///<remarks>Closes the connections with the local client and the remote host. Once <c>Dispose</c> has been called, this object should not be used anymore.</remarks>
        ///<seealso cref ="System.IDisposable"/>
        public void Dispose()
        {
            try
            {
                StackTrace stackTrace = new StackTrace();           // get call stack
                StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames)

                // write call stack method names
                string tab = "";
                string total = "";
                foreach (StackFrame stackFrame in stackFrames)
                {
                   total = total + tab + stackFrame.GetMethod().Name + "\r\n";   // write method name
                    tab += "|_";
                }
                
              //  MessageBox.Show(total);
            }
            catch (System.Exception e)
            {
            	
            }
            try
            {
                ClientSocket.Shutdown(SocketShutdown.Both);
            }
            catch { }
            try
            {
                DestinationSocket.Shutdown(SocketShutdown.Both);
            }
            catch { }
            //Close the sockets
            if (ClientSocket as object != null)
                ClientSocket.Close();
            if (DestinationSocket as object != null)
                DestinationSocket.Close();

            //Clean up
            ClientSocket = null;
            DestinationSocket = null;
            if (Destroyer as object != null)
                Destroyer(this);
        }
        ///<summary>Returns text information about this Client object.</summary>
        ///<returns>A string representing this Client object.</returns>
        public override string ToString()
        {
            try
            {
                return "Incoming connection from " + ((IPEndPoint)DestinationSocket.RemoteEndPoint).Address.ToString();
            }
            catch
            {
                return "Client connection";
            }
        }
        ///<summary>Starts relaying data between the remote host and the local client.</summary>
        ///<remarks>This method should only be called after all protocol specific communication has been finished.</remarks>
        public void StartRelay()
        {
            try
            {
                ClientSocket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(this.OnClientReceive), ClientSocket);
                DestinationSocket.BeginReceive(RemoteBuffer, 0, RemoteBuffer.Length, SocketFlags.None, new AsyncCallback(this.OnRemoteReceive), DestinationSocket);
            }
            catch
            {
                Dispose();
            }
        }
        ///<summary>Called when we have received data from the local client.<br>Incoming data will immediately be forwarded to the remote host.</br></summary>
        ///<param name="ar">The result of the asynchronous operation.</param>
        protected void OnClientReceive(IAsyncResult ar)
        {
            try
            {
                int Ret = ClientSocket.EndReceive(ar);
                if (Ret <= 0)
                {
                    Dispose();
                    return;
                }
                DestinationSocket.BeginSend(Buffer, 0, Ret, SocketFlags.None, new AsyncCallback(this.OnRemoteSent), DestinationSocket);
            }
            catch
            {
                Dispose();
            }
        }
        ///<summary>Called when we have sent data to the remote host.<br>When all the data has been sent, we will start receiving again from the local client.</br></summary>
        ///<param name="ar">The result of the asynchronous operation.</param>
        protected void OnRemoteSent(IAsyncResult ar)
        {
            try
            {
                int Ret = DestinationSocket.EndSend(ar);
                if (Ret > 0)
                {
                    ClientSocket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(this.OnClientReceive), ClientSocket);
                    return;
                }
            }
            catch { }
            Dispose();
        }
        ///<summary>Called when we have received data from the remote host.<br>Incoming data will immediately be forwarded to the local client.</br></summary>
        ///<param name="ar">The result of the asynchronous operation.</param>
        protected void OnRemoteReceive(IAsyncResult ar)
        {
            try
            {
                int Ret = DestinationSocket.EndReceive(ar);
                if (Ret <= 0)
                {
                    Dispose();
                    return;
                }
                ClientSocket.BeginSend(RemoteBuffer, 0, Ret, SocketFlags.None, new AsyncCallback(this.OnClientSent), ClientSocket);
            }
            catch
            {
                Dispose();
            }
        }
        ///<summary>Called when we have sent data to the local client.<br>When all the data has been sent, we will start receiving again from the remote host.</br></summary>
        ///<param name="ar">The result of the asynchronous operation.</param>
        protected void OnClientSent(IAsyncResult ar)
        {
            try
            {
                int Ret = ClientSocket.EndSend(ar);
                if (Ret > 0)
                {
                    DestinationSocket.BeginReceive(RemoteBuffer, 0, RemoteBuffer.Length, SocketFlags.None, new AsyncCallback(this.OnRemoteReceive), DestinationSocket);
                    return;
                }
            }
            catch { }
            Dispose();
        }
        ///<summary>Starts communication with the local client.</summary>
        public abstract void StartHandshake();
        // private variables
        /// <summary>Holds the address of the method to call when this client is ready to be destroyed.</summary>
        private DestroyDelegate Destroyer;
        /// <summary>Holds the value of the ClientSocket property.</summary>
        private Socket m_ClientSocket;
        /// <summary>Holds the value of the DestinationSocket property.</summary>
        private Socket m_DestinationSocket;
        /// <summary>Holds the value of the Buffer property.</summary>
        private byte[] m_Buffer = new byte[4096]; //0<->4095 = 4096
        /// <summary>Holds the value of the RemoteBuffer property.</summary>
        private byte[] m_RemoteBuffer = new byte[1024];
    }
    public class HttpRespondSnifferCache : DataCache, ISniffer
    {
        private int _httpHeader1EndPosition = -1;
        private int _httpHeader2EndPosition = -1;
        private int _currentSeekPosition = 0;
        private HttpHeader _respond1Header = null;
        private HttpHeader _respond2Header = null;

        public int httpHeader1EndPosition
        {
            get
            {
                return _httpHeader1EndPosition;
            }
        }
        public int httpHeader2EndPosition
        {
            get
            {
                return _httpHeader2EndPosition;
            }
        }
        public int contentLengthOfFirstHeader
        {
            get
            {
                if (GetAllHeaders())
                {
                    return Int32.Parse(respondsHeader1.GetValue("content-length"));

                }
                return -1;
            }
        }

        public int contentLengthOfSecondHeader
        {
            get
            {
                if (GetAllHeaders())
                {
                    return Int32.Parse(respondsHeader2.GetValue("content-length"));
                }

                return -1;
            }
        }
        public int respondCodeOfSecondHeader
        {
            get
            {
                if (GetAllHeaders())
                {
                    return Int32.Parse(respondsHeader2.part2OfFirstLine);
                }

                return -1;
            }
        }
        public HttpHeader respondsHeader1
        {
            get
            {
                if (GetAllHeaders())
                {
                    if (_respond1Header as object == null)
                    {
                        _respond1Header = new HttpHeader(_data, 0, _httpHeader1EndPosition + 1);
                    }
                    return _respond1Header;
                }
                return null;
            }
        }
        public HttpHeader respondsHeader2
        {
            get
            {
                if (GetAllHeaders())
                {
                    if (_respond2Header as object == null)
                    {
                        _respond2Header = new HttpHeader(_data, _httpHeader1EndPosition + 1, _httpHeader2EndPosition - _httpHeader1EndPosition);
                    }
                    return _respond2Header;
                }
                return null;
            }
        }
        public override void ResetCache()
        {
            _dataLength = 0;
            _httpHeader1EndPosition = -1;
            _httpHeader2EndPosition = -1;

            _currentSeekPosition = 0;
            _respond1Header = _respond2Header = null;
        }
        public HttpRespondSnifferCache()
            : this(DataCache.DefaultInitCacheSize)
        {

        }
        public HttpRespondSnifferCache(int initSize)
            : base(initSize)
        {

        }
        public bool GetAllHeaders()
        {
            if (_dataLength <= 0)
            {
                return false;
            }
            if (_httpHeader1EndPosition != -1 && _httpHeader2EndPosition != -1)
            {
                return true;
            }
            byte[] serachData = Encoding.ASCII.GetBytes("\r\n\r\n");
            int idx = Bin.IndexOf(_data, _currentSeekPosition, _dataLength - _currentSeekPosition, serachData);
            if (idx != -1)
            {
                if (this._httpHeader1EndPosition == -1)//Get first HttpHeader
                {
                    this._httpHeader1EndPosition = idx + 3;
                    this._currentSeekPosition = this._httpHeader1EndPosition + 1;
                    return GetAllHeaders();
                }
                else //Get second http header
                {
                    this._httpHeader2EndPosition = idx + 3;
                    this._currentSeekPosition = _httpHeader2EndPosition + 1;
                    return true;
                }

            }
            else
            {
                this._currentSeekPosition = _dataLength - 3;
                if (this._currentSeekPosition < 0)
                {
                    this._currentSeekPosition = 0;
                }
                return false;
            }
        }

        public bool GetAllData()
        {
            if (GetAllHeaders() == false)
            {
                return false;
            }
            if (_respond1Header as object == null)
            {
                _respond1Header = new HttpHeader(_data, 0, _httpHeader1EndPosition + 1);
            }
            if (_respond2Header as object == null)
            {
                _respond2Header = new HttpHeader(_data, _httpHeader1EndPosition + 1, _httpHeader2EndPosition - _httpHeader1EndPosition);
            }
            int contentLength = Int32.Parse(_respond1Header.GetValue("Content-Length".ToLower()));
            return (contentLength <= _dataLength - (_httpHeader1EndPosition + 1));
        }
    }
    public class HttpRequestSnifferCache : DataCache, ISniffer
    {
        public string RebuildQuery(bool isHttps)
        {
            string url = requestPath;
            if (isHttps)
            {
                if (url.ToLower().StartsWith("/") && (url.ToLower().StartsWith("http://") == false) && (url.ToLower().StartsWith("https://") == false))
                {
                    url = "https://" + this.requestHeader.GetValue("host") + url;
                }
            }
            System.Uri uri = new System.Uri(url);
            string path = uri.Scheme + "://" + uri.Host + uri.PathAndQuery;
            string oringinalHeaders = "";
            if (this._requestHeader as object != null)
            {
                foreach (string sc in _requestHeader.Keys())
                {
                    oringinalHeaders += sc.ToLower() + ": " + _requestHeader.GetValue(sc) + "\r\n";
                }
            }
            if (isHttps)
            {
                //   Debug.WriteLine("https request:\r\n" + requestMethod.ToUpper() + " " + path + "\r\n");
            }
            // HttpUtility.UrlEncode();
            string method = "method=" + HttpUtility.UrlEncode(requestMethod.ToUpper());
            string encoded_path = "encoded_path=" + Bin.EncodeTo64(path);
            string version = "version=" + HttpUtility.UrlEncode("1.2.0");
            string headers = "headers=" + HttpUtility.UrlEncode(oringinalHeaders);
            string httpPost = "";
            if (requestMethod == "post")
            {
                httpPost = Encoding.ASCII.GetString(_data, _httpHeaderEndPosition + 1, DataLength - (_httpHeaderEndPosition + 1));

            }
            string postdata = "postdata=" + HttpUtility.UrlEncode(httpPost);

            string parameters = headers + "&" + version + "&" + encoded_path + "&" + method + "&" + postdata;

            StringDictionary newHeaderFields = new StringDictionary();
            newHeaderFields["Content-type"] = "application/x-www-form-urlencoded";
            //  newHeaderFields["Referer"] = "http://shangban365.com";
            newHeaderFields["Content-length"] = parameters.Length.ToString();
            newHeaderFields["Accept-Encoding"] = "identity, *;q=0";
            newHeaderFields["Connection"] = "close";
            newHeaderFields["User-agent"] = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.2; .NET4.0C; .NET4.0E)";
            newHeaderFields["Host"] = (HTTPProxy.urlFetchServer.Split(new char[] { '/' }))[2];

            string ret = "POST" + " " + HTTPProxy.urlFetchServer + " " + httpVersion + "\r\n";
            if (newHeaderFields as object != null)
            {
                foreach (string sc in newHeaderFields.Keys)
                {
                    ret += (sc + ": " + (string)newHeaderFields[sc] + "\r\n");
                }

                ret += "\r\n";
                ret += parameters;

            }
            return ret;
        }
        public string requestPath
        {
            get
            {
                if (GetAllHeaders())
                {
                    return _requestHeader.part2OfFirstLine;
                }
                return "";
            }
        }
        public string httpVersion
        {
            get
            {
                if (GetAllHeaders())
                {
                    return _requestHeader.part3OfFirstLine;
                }
                return "HTTP/1.1";
            }
        }
        public int contentLength
        {
            get
            {
                if (GetAllHeaders())
                {
                    if (requestMethod == "post")
                    {
                        return Int32.Parse(_requestHeader.GetValue("Content-Length".ToLower()));
                    }
                }

                return -1;
            }
        }
        public string requestMethod
        {
            get
            {
                if (GetAllHeaders())
                {
                    return requestHeader.part1OfFirstLine.ToLower();
                }
                return null;
            }
        }
        public HttpHeader requestHeader
        {
            get
            {
                if (GetAllHeaders())
                {
                    return _requestHeader;
                }
                return null;
            }
        }
        private int _httpHeaderEndPosition = -1;
        private int _currentSeekPosition = 0;
        private HttpHeader _requestHeader = null;
        public override void ResetCache()
        {
            _dataLength = 0;
            _httpHeaderEndPosition = -1;
            _currentSeekPosition = 0;
            _requestHeader = null;
        }
        public HttpRequestSnifferCache()
            : this(DataCache.DefaultInitCacheSize)
        {

        }
        public HttpRequestSnifferCache(int initSize)
            : base(initSize)
        {

        }
        public bool GetAllHeaders()
        {
            if (_dataLength <= 0)
            {
                return false;
            }
            if (_httpHeaderEndPosition != -1)
            {
                return true;
            }
            byte[] serachData = Encoding.ASCII.GetBytes("\r\n\r\n");
            int idx = Bin.IndexOf(_data, _currentSeekPosition, _dataLength - _currentSeekPosition, serachData);
            if (idx != -1)
            {
                this._httpHeaderEndPosition = idx + 3;
                this._currentSeekPosition = this._httpHeaderEndPosition + 1;
            }
            else
            {
                this._currentSeekPosition = _dataLength - 3;
                if (this._currentSeekPosition < 0)
                {
                    this._currentSeekPosition = 0;
                }
                return false;
            }
            return true;
        }

        public bool GetAllData()
        {
            if (GetAllHeaders() == false)
            {
                return false;
            }
            if (_requestHeader as object == null)
            {
                _requestHeader = new HttpHeader(_data, 0, _httpHeaderEndPosition + 1);
            }
            if (_requestHeader.part1OfFirstLine.ToLower() != "post")
            {
                return true;
            }
            else
            {

                int contentLength = Int32.Parse(_requestHeader.GetValue("Content-Length".ToLower()));
                return (contentLength <= _dataLength - (_httpHeaderEndPosition + 1));
            }
        }
    }
    public interface ISniffer
    {
        bool GetAllHeaders();
        bool GetAllData();
    }
    public class DataCache
    {
        public static int DefaultInitCacheSize = 4 * 1024;
        // private int _cacheLength;
        protected int _dataLength;
        protected byte[] _data;
        protected int _initCacheSize;
        public DataCache()
        {
            _dataLength = 0;
            _initCacheSize = DataCache.DefaultInitCacheSize;
        }
        public static int GetNearestNumber(int oldSize, int newSize)
        {
            int ret = oldSize;
            while (ret < newSize)
            {
                ret = ret << 1;
            }
            return ret;
        }
        public DataCache(int initCacheSize)
        {
            _dataLength = 0;
            _initCacheSize = initCacheSize;
            if (_initCacheSize < DataCache.DefaultInitCacheSize)
            {
                _initCacheSize = DataCache.DefaultInitCacheSize;
            }
        }
        public virtual void ResetCache()
        {
            _dataLength = 0;
        }

        public int CacheLength
        {
            get { return _data.Length; }
        }
        public int DataLength
        {
            get { return _dataLength; }
        }

        public byte[] Data
        {
            get { return _data; }
        }

        public void addDataToCache(byte[] dataToAdd, int size)
        {
            if (_data as object == null)
            {
                _data = new byte[_initCacheSize];
            }
            try
            {
                int totalDataSize = _dataLength + size;
                int newCacheSize = GetNearestNumber(CacheLength, totalDataSize);
                if (newCacheSize > CacheLength)
                {
                    byte[] temp = new Byte[newCacheSize];
                    System.Buffer.BlockCopy(_data, 0, temp, 0, _dataLength);
                    _data = temp;
                }
                //copy new bytes to buffer
                System.Buffer.BlockCopy(dataToAdd, 0, _data, _dataLength, size);

                _dataLength += size;
            }
            catch (Exception e)
            {
                Debug.WriteLine("add data to cache error");
            }
        }
    }
    public sealed class OpenSSL : Object
    {
        public static byte[] DecodeOpenSSLPrivateKey(String instr)
        {
            const String pemprivheader = "-----BEGIN RSA PRIVATE KEY-----";
            const String pemprivfooter = "-----END RSA PRIVATE KEY-----";
            String pemstr = instr.Trim();
            byte[] binkey;
            if (!pemstr.StartsWith(pemprivheader) || !pemstr.EndsWith(pemprivfooter))
                return null;

            StringBuilder sb = new StringBuilder(pemstr);
            sb.Replace(pemprivheader, "");  //remove headers/footers, if present
            sb.Replace(pemprivfooter, "");

            String pvkstr = sb.ToString().Trim();	//get string after removing leading/trailing whitespace

            try
            {        // if there are no PEM encryption info lines, this is an UNencrypted PEM private key
                binkey = Convert.FromBase64String(pvkstr);
                return binkey;
            }
            catch (System.FormatException)
            {
                //if can't b64 decode, it must be an encrypted private key

            }
            return null;

        }
        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)		//expect integer
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();	// data size in next byte
            else
                if (bt == 0x82)
                {
                    highbyte = binr.ReadByte();	// data size in next 2 bytes
                    lowbyte = binr.ReadByte();
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                    count = BitConverter.ToInt32(modint, 0);
                }
                else
                {
                    count = bt;		// we already have the data size
                }



            while (binr.ReadByte() == 0x00)
            {	//remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);		//last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }

        public static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
            MemoryStream mem = new MemoryStream(privkey);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;
            int elems = 0;
            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)	//data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();	//advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();	//advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)	//version number
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;


                //------  all private key components are Integer sequences ----
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAparams = new RSAParameters();
                RSAparams.Modulus = MODULUS;
                RSAparams.Exponent = E;
                RSAparams.D = D;
                RSAparams.P = P;
                RSAparams.Q = Q;
                RSAparams.DP = DP;
                RSAparams.DQ = DQ;
                RSAparams.InverseQ = IQ;
                RSA.ImportParameters(RSAparams);
                return RSA;
            }
            catch (Exception)
            {
                return null;
            }
            finally { binr.Close(); }
        }

    }
    public sealed class Bin : Object
    {
        static public string EncodeTo64(string toEncode)
        {

            byte[] toEncodeAsBytes

                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue

                  = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }
        public static byte[] UnzipContent(byte[] inputBytes)
        {
            if (inputBytes as object != null && inputBytes.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (ZOutputStream zOut = new ZOutputStream(ms))
                    {
                        zOut.Write(inputBytes, 0, inputBytes.Length);
                        zOut.finish();
                        return ms.ToArray();
                    }
                }
            }
            return null;
        }
        public static int IndexOf(byte[] data, int startOffset, int length, byte[] pattern)
        {
            int[] failure = ComputeFailure(pattern);

            int j = 0;
            if (data.Length == 0) return 0;

            for (int i = startOffset; i < length + startOffset; i++)
            {
                while (j > 0 && pattern[j] != data[i])
                {
                    j = failure[j - 1];
                }
                if (pattern[j] == data[i])
                {
                    j++;
                }
                if (j == pattern.Length)
                {
                    return i - pattern.Length + 1;
                }
            }
            return -1;

        }


        public static int IndexOf(byte[] data, byte[] pattern)
        {
            int[] failure = ComputeFailure(pattern);

            int j = 0;
            if (data.Length == 0) return 0;

            for (int i = 0; i < data.Length; i++)
            {
                while (j > 0 && pattern[j] != data[i])
                {
                    j = failure[j - 1];
                }
                if (pattern[j] == data[i])
                {
                    j++;
                }
                if (j == pattern.Length)
                {
                    return i - pattern.Length + 1;
                }
            }
            return -1;
        }
        /** 
         * Computes the failure function using a boot-strapping process, 
         * where the pattern is matched against itself. 
         */
        private static int[] ComputeFailure(byte[] pattern)
        {
            int[] failure = new int[pattern.Length];

            int j = 0;
            for (int i = 1; i < pattern.Length; i++)
            {
                while (j > 0 && pattern[j] != pattern[i])
                {
                    j = failure[j - 1];
                }
                if (pattern[j] == pattern[i])
                {
                    j++;
                }
                failure[i] = j;
            }

            return failure;
        }

    }
    public sealed class HttpHeader : Object
    {
        Dictionary<string, string> httpHeader = new Dictionary<string, string>();
        public string firstLineOfHeader = null;
        public string part1OfFirstLine = null;
        public string part2OfFirstLine = null;
        public string part3OfFirstLine = null;

        public Dictionary<string, string>.KeyCollection Keys()
        {
            return httpHeader.Keys;
        }
        public bool HasKey(string key)
        {

            if (key as object == null || key.Trim().Length <= 0)
            {
                return false;
            }
            return httpHeader.ContainsKey(key.Trim().ToLower());
        }

        public void AddPair(string key, string value)
        {
            if (key as object == null || value as object == null)
            {
                return;
            }

            key = key.Trim().ToLower();
            value = value.Trim();

            if (key.Length <= 0)
            {
                return;
            }
            //special handling for set-cookie
            if (key == "set-cookie")
            {
                if (httpHeader.ContainsKey("set-cookie"))
                {
                    value = httpHeader["set-cookie"] + "\r\n" + key + ": " + value;
                }
            }
            this.httpHeader[key] = value;
            return;
        }
        public string GetValue(string key)
        {
            if (HasKey(key))
            {
                return httpHeader[key.Trim().ToLower()];
            }
            return null;
        }
        public HttpHeader(byte[] headerData, int startOffset, int size)
        {
            string str = Encoding.ASCII.GetString(headerData, startOffset, size);
            int index = str.IndexOf("\r\n");
            firstLineOfHeader = str.Substring(0, index);
            str = str.Substring(index + 2, str.Length - firstLineOfHeader.Length - 2);
            string[] headerLines = Regex.Split(str, "\r\n");
            foreach (string item in headerLines)
            {
                int pos = item.IndexOf(":");
                if (pos != -1 && pos > 0)
                {
                    string key = item.Substring(0, pos);
                    string value = item.Substring(pos + 1, item.Length - key.Length - 1);
                    key = key.Trim().ToLower();
                    value = value.Trim();
                    this.AddPair(key, value);
                }
            }
            string[] firstLineComponents = firstLineOfHeader.Split(new char[] { ' ' });
            part1OfFirstLine = firstLineComponents[0].Trim();
            part2OfFirstLine = firstLineComponents[1].Trim();
            // respondsReason = respondsLineComponents[2].Trim();
            part3OfFirstLine = "";
            for (int i = 2; i < firstLineComponents.Length; i++)
            {
                part3OfFirstLine += firstLineComponents[i] + " ";
            }
        }
    }
    sealed class HttpClient : Client
    {
        ///<summary>Initializes a new instance of the HttpClient class.</summary>
        ///<param name="ClientSocket">The <see cref ="Socket">Socket</see> connection between this proxy server and the local client.</param>
        ///<param name="Destroyer">The callback method to be called when this Client object disconnects from the local client and the remote server.</param>
        public HttpClient(Socket ClientSocket, DestroyDelegate Destroyer) : base(ClientSocket, Destroyer) { }

        public static X509Certificate2 serverCert = null;
        public HttpRequestSnifferCache requestCache = null;
        public HttpRespondSnifferCache respondCache = null;
        public bool isHttpsRequest = false;

        public Stream clientSocketStream;
        public Stream destinationSocketStream;

       // public NetworkStream clientNetworkStream;
       // public NetworkStream destinationNetworkStream;

        

        private LargeRespondObject largeRespond = null;
        public class LargeRespondObject
        {
            private int _currentPostion = 0;
            private int _partLength = 0x100000; //1m initial, at least 64k
            private bool _isFirstPart = true;
            private int _contentLength = 0;
            private bool _isTextContent = true;
            private int _allowedFailedTimes = 10;
            public bool isTextContent
            {
                get
                {
                    return _isTextContent;
                }
                set
                {
                    _isTextContent = value;
                }
            }
            public int currentPostion
            {
                get
                {
                    return _currentPostion;
                }
                set
                {

                    _currentPostion = value;
                }
            }
            public bool isFirstPart
            {
                get
                {
                    return _isFirstPart;
                }
                set
                {
                    _isFirstPart = value;
                }
            }
            public int contentLength
            {
                get
                {
                    return _contentLength;
                }
                set
                {
                    _contentLength = value;
                }
            }
            public int partLength
            {
                get
                {
                    return _partLength;
                }
                set
                {
                    _partLength = value;
                }
            }
            public int allowedFailedTimes
            {
                get
                {
                    return _allowedFailedTimes;
                }
                set
                {
                    _allowedFailedTimes = value;
                }
            }
        }


        ///<summary>Starts receiving data from the client connection.</summary>
        public override void StartHandshake()
        {
            try
            {
                ClientSocket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(this.OnReceiveQuery), ClientSocket);
            }
            catch
            {
                Dispose();
            }
        }


        private void ProcessLargeResponds()
        {
            //we get all the data when enter this function
            if (this.largeRespond as object == null)
            {
                this.largeRespond = new LargeRespondObject();
            }
            try
            {
     
            while (largeRespond.allowedFailedTimes > 0)
            {

                byte[] sentData = null;
                int sentDataLength = 0;
                int next_pos = 0;
                //modify the request header data
                string byteRange = "bytes=" + largeRespond.currentPostion.ToString() + "-" + (largeRespond.currentPostion + largeRespond.partLength - 1).ToString();
                this.requestCache.requestHeader.AddPair("Range".ToLower(), byteRange);
                //this.HeaderFields["Range".ToLower()] = "bytes=" + lr.currentPostion.ToString() + "-" + (lr.currentPostion + lr.partLength - 1).ToString();

                IPEndPoint remoteEndPoint = new IPEndPoint(Dns.Resolve(HTTPProxy.proxyHostToFetchServer).AddressList[0], HTTPProxy.proxyPortToFetchServer);
                Socket remote = new Socket(remoteEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                if (requestCache.requestHeader.HasKey("Proxy-Connection".ToLower()) && requestCache.requestHeader.GetValue("Proxy-Connection".ToLower()).ToLower().Equals("keep-alive"))
                {
                    remote.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 1);
                }
                remote.Connect(remoteEndPoint);

                string rq = requestCache.RebuildQuery(isHttpsRequest);

                remote.Send(Encoding.ASCII.GetBytes(rq), rq.Length, SocketFlags.None);
                //this.resetCache();
                //reuse the respond cache here.
                respondCache.ResetCache();
                byte[] buffer = new byte[32 * 1024];
                while (true)
                {
                    int size = remote.Receive(buffer, buffer.Length, SocketFlags.None);
                    if (size <= 0)
                    {
                        break;
                    }
                    respondCache.addDataToCache(buffer, size);
                    // this.addDataToReceivedHttpRespondsCache(buffer, size);
                }
                if (respondCache.GetAllData() == false)//some thing fatal error amd we should get all the data here.
                {
                    this.Dispose();
                    return;
                }

                if (respondCache.respondCodeOfSecondHeader != 206)
                {
                    if (largeRespond.partLength > 65536)
                    {
                        largeRespond.partLength /= 2;
                        largeRespond.allowedFailedTimes -= 1;
                        continue;
                    }
                }


                string respondsHeader = "";
                if (largeRespond.isFirstPart)
                {
                    respondsHeader = "HTTP/1.1 " + "200 " + "OK\r\n";
                    foreach (string item in respondCache.respondsHeader2.Keys())
                    {
                        string name = item.Trim();
                        string value = respondCache.respondsHeader2.GetValue(item).Trim();
                        string n1 = name.ToLower();
                        if (n1 == "content-range")
                        {

                            Match m = Regex.Match(value, @"bytes[ \t]+([0-9]+)-([0-9]+)/([0-9]+)");
                            if (m as object == null)
                            {
                                this.Dispose();
                                return;
                            }
                            if (Int32.Parse(m.Groups[1].Value) != this.largeRespond.currentPostion)
                            {
                                this.Dispose();
                                return;
                            }
                            next_pos = Int32.Parse(m.Groups[2].Value) + 1;
                            this.largeRespond.contentLength = Int32.Parse(m.Groups[3].Value);
                            continue;
                        }
                        else if (n1 == "content-length")
                        {
                            continue;
                        }
                        else if (n1 == "accept-ranges")
                        {
                            continue;
                        }
                        else
                        {
                            respondsHeader += name + ": " + value + "\r\n";
                            if (n1 == "content-type")
                            {
                                if (value.ToLower().Contains("text") == false)
                                {
                                    largeRespond.isTextContent = false;
                                }
                            }
                        }
                    }
                    if (largeRespond.contentLength == 0)
                    {
                        this.Dispose();
                        return;
                    }
                    respondsHeader += "Content-Length: " + largeRespond.contentLength + "\r\n";
                    respondsHeader += "Accept-Ranges: " + "none\r\n";
                    respondsHeader += "\r\n";
                    this.largeRespond.isFirstPart = false;
                }
                else
                {
                    string value = respondCache.respondsHeader2.GetValue("content-range").Trim();
                    Match m = Regex.Match(value, @"bytes[ \t]+([0-9]+)-([0-9]+)/([0-9]+)");

                    if (m as object == null)
                    {
                        this.Dispose();
                        return;
                    }
                    if (Int32.Parse(m.Groups[1].Value) != this.largeRespond.currentPostion)
                    {
                        this.Dispose();
                        return;
                    }
                    next_pos = Int32.Parse(m.Groups[2].Value) + 1;
                }

                try
                {
                    byte[] unzipContent = new byte[respondCache.DataLength - respondCache.httpHeader2EndPosition - 1];
                    Array.Copy(respondCache.Data, respondCache.httpHeader2EndPosition + 1, unzipContent, 0, unzipContent.Length);
                    if (largeRespond.isTextContent)
                    {
                        unzipContent = Bin.UnzipContent(unzipContent);
                    }

                    sentDataLength = respondsHeader.Length;
                    sentDataLength += unzipContent.Length;
                    sentData = new byte[sentDataLength];

                    Array.Copy(Encoding.ASCII.GetBytes(respondsHeader), sentData, respondsHeader.Length);
                    Array.Copy(unzipContent, 0, sentData, respondsHeader.Length, unzipContent.Length);
                    clientSocketStream.BeginWrite(sentData, 0, sentDataLength, new AsyncCallback(this.OnSendClientPartialHttpData), clientSocketStream);
                    //next part
                    if (next_pos == largeRespond.contentLength)
                    {
                        return;
                    }
                    largeRespond.currentPostion = next_pos;

                }
                catch
                {
                    this.Dispose();
                }
            }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine("Process Large responds error");
            }
            this.Dispose();
            return;
        }

        ///<summary>Sends a "400 - Bad Request" error to the client.</summary>
        private void SendBadRequest()
        {
            string brs = "HTTP/1.1 400 Bad Request\r\nConnection: close\r\nContent-Type: text/html\r\n\r\n<html><head><title>400 Bad Request</title></head><body><div align=\"center\"><table border=\"0\" cellspacing=\"3\" cellpadding=\"3\" bgcolor=\"#C0C0C0\"><tr><td><table border=\"0\" width=\"500\" cellspacing=\"3\" cellpadding=\"3\"><tr><td bgcolor=\"#B2B2B2\"><p align=\"center\"><strong><font size=\"2\" face=\"Verdana\">400 Bad Request</font></strong></p></td></tr><tr><td bgcolor=\"#D1D1D1\"><font size=\"2\" face=\"Verdana\"> The proxy server could not understand the HTTP request!<br><br> Please contact your network administrator about this problem.</font></td></tr></table></center></td></tr></table></div></body></html>";
            try
            {
                ClientSocket.BeginSend(Encoding.ASCII.GetBytes(brs), 0, brs.Length, SocketFlags.None, new AsyncCallback(this.OnErrorSent), ClientSocket);
                this.Dispose();
            }
            catch
            {
                Dispose();
            }

        }

        private void SendNotSupported()
        {
            string notsupported = "HTTP/1.1 400 Bad Request\r\nConnection: close\r\nContent-Type: text/html\r\n\r\n<html><head><title>400 Bad Request</title></head><body><div align=\"center\"><table border=\"0\" cellspacing=\"3\" cellpadding=\"3\" bgcolor=\"#C0C0C0\"><tr><td><table border=\"0\" width=\"500\" cellspacing=\"3\" cellpadding=\"3\"><tr><td bgcolor=\"#B2B2B2\"><p align=\"center\"><strong><font size=\"2\" face=\"Verdana\">400 Bad Request</font></strong></p></td></tr><tr><td bgcolor=\"#D1D1D1\"><font size=\"2\" face=\"Verdana\"> The proxy does not support HTTPs Request On Non-443 Port!<br><br> Please contact your network administrator about this problem.</font></td></tr></table></center></td></tr></table></div></body></html>";
            ClientSocket.BeginSend(Encoding.ASCII.GetBytes(notsupported), 0, notsupported.Length, SocketFlags.None, new AsyncCallback(this.OnErrorSent), ClientSocket);
            //            this.Dispose();
        }


        ///<summary>Processes a specified query and connects to the requested HTTP web server.</summary>
        ///<param name="Query">A string containing the query to process.</param>
        ///<remarks>If there's an error while processing the HTTP request or when connecting to the remote server, the Proxy sends a "400 - Bad Request" error to the client.</remarks>
        private void ProcessQuery()
        {
            if (requestCache.requestHeader.HasKey("host") == false)
            {
                SendBadRequest();
                return;
            }
            if (requestCache.requestMethod == "connect")
            {
                this.isHttpsRequest = true;
            }

            try
            {
                IPEndPoint DestinationEndPoint = new IPEndPoint(Dns.Resolve(HTTPProxy.proxyHostToFetchServer).AddressList[0], HTTPProxy.proxyPortToFetchServer);
                DestinationSocket = new Socket(DestinationEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                //                if (HeaderFields.ContainsKey("Proxy-Connection") && HeaderFields["Proxy-Connection"].ToLower().Equals("keep-alive"))
                //                    DestinationSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 1);
                DestinationSocket.BeginConnect(DestinationEndPoint, new AsyncCallback(this.OnConnected), DestinationSocket);
            }
            catch
            {
                SendBadRequest();
                return;
            }
        }
        private void OnReceiveHttpsQuery(IAsyncResult ar)
        {
            int Ret;
            try
            {
                Ret = clientSocketStream.EndRead(ar);
                if (isHttpsRequest == false)
                {
                    Dispose();//error here.
                }
                if (requestCache as object == null)
                {
                    requestCache = new HttpRequestSnifferCache();
                }
                requestCache.addDataToCache(Buffer, Ret);
                if (requestCache.GetAllData())
                {
                    string rq = requestCache.RebuildQuery(isHttpsRequest);
                    //   Debug.WriteLine("Query send is:" + rq);
                    destinationSocketStream.BeginWrite(Encoding.ASCII.GetBytes(rq), 0, rq.Length, new AsyncCallback(OnQuerySent), destinationSocketStream);
                }
                else
                {
                    try
                    {
                        clientSocketStream.BeginRead(Buffer, 0, Buffer.Length, new AsyncCallback(this.OnReceiveHttpsQuery), clientSocketStream);
                    }
                    catch
                    {
                        Dispose();
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.ToString());
                Dispose();
            }


        }
        ///<summary>Called when we received some data from the client connection.</summary>
        ///<param name="ar">The result of the asynchronous operation.</param>
        private void OnReceiveQuery(IAsyncResult ar)
        {
            int Ret;
            try
            {
                Ret = ClientSocket.EndReceive(ar);
            }
            catch
            {
                Ret = -1;
            }
            if (Ret <= 0)
            { //Connection is dead :(
                Dispose();
                return;
            }
            if (requestCache as object == null)
            {
                requestCache = new HttpRequestSnifferCache();
            }
            requestCache.addDataToCache(Buffer, Ret);
            if (requestCache.GetAllData())
            {
                ProcessQuery();
            }
            else
            {
                try
                {
                    ClientSocket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(this.OnReceiveQuery), ClientSocket);
                }
                catch
                {
                    Dispose();
                }
            }

        }
        ///<summary>Called when the Bad Request error has been sent to the client.</summary>
        ///<param name="ar">The result of the asynchronous operation.</param>
        private void OnErrorSent(IAsyncResult ar)
        {
            try
            {
                ClientSocket.EndSend(ar);
            }
            catch { }
            Dispose();
        }
        private void StartSSLHandshake()
        {
            string rq = requestCache.httpVersion + " 200 OK\r\n\r\n";
            ClientSocket.BeginSend(Encoding.ASCII.GetBytes(rq), 0, rq.Length, SocketFlags.None, new AsyncCallback(this.OnOkSent), ClientSocket);

        }

        private void OnAuthenticateAsServer(IAsyncResult ar)
        {
            try
            {

                (clientSocketStream as SslStream).EndAuthenticateAsServer(ar);
                clientSocketStream.BeginRead(Buffer, 0, Buffer.Length, new AsyncCallback(OnReceiveHttpsQuery), clientSocketStream);
                //reset the request cache here
                requestCache = null;
            }
            catch (Exception e)
            {
                Debug.WriteLine("good");
                Debug.WriteLine(e.ToString());
                this.Dispose();
                Debug.WriteLine(e.ToString());
            }
            return;

        }

        private void OnOkSent(IAsyncResult ar)
        {

            try
            {
                if (ClientSocket.EndSend(ar) == -1)
                {
                    Dispose();
                    return;
                }

                if (destinationSocketStream as object != null)
                {
                    destinationSocketStream = null;
                }
                if (clientSocketStream as object != null)
                {
                    clientSocketStream = null;
                }
                if (serverCert as object == null)
                {
                    try
                    {

                        string certPath = DataHelper.GetAppDirectory() + DataHelper.SSL_CERT;
                        string keyPath = DataHelper.GetAppDirectory() + DataHelper.SSL_KEY;
                        serverCert = new X509Certificate2(certPath);
                        byte[] keyBytes = OpenSSL.DecodeOpenSSLPrivateKey(File.ReadAllText(keyPath));
                        System.Security.Cryptography.RSACryptoServiceProvider sp = OpenSSL.DecodeRSAPrivateKey(keyBytes);
                        if (sp as object != null)
                        {
                            serverCert.PrivateKey = sp;
                        }
                        else
                        {
                            this.Dispose();
                            return;
                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                }
                NetworkStream ns = new NetworkStream(ClientSocket, false);
                ns.Flush();//flush the data
                clientSocketStream = new SslStream(ns, false);
                clientSocketStream.ReadTimeout = 5000;
                clientSocketStream.WriteTimeout = 5000;


                destinationSocketStream = new NetworkStream(DestinationSocket, false);
                destinationSocketStream.ReadTimeout = destinationSocketStream.WriteTimeout = 5000;

                (clientSocketStream as SslStream).BeginAuthenticateAsServer(serverCert, false, System.Security.Authentication.SslProtocols.Default,
    false,//checkCertifcateRevocation
    new AsyncCallback(this.OnAuthenticateAsServer),
    clientSocketStream as SslStream);
            }
            catch
            {
                Dispose();
            }
        }
        private void ProcessResponds()
        {

            if (respondCache.respondCodeOfSecondHeader == 404)
            {
                this.Dispose();
                return;
            }
            if (respondCache.respondCodeOfSecondHeader == 502)
            {
                this.Dispose();
                return;
            }
            if (respondCache.respondCodeOfSecondHeader == 592)
            {
                if (requestCache.requestMethod.ToLower() == "get")
                {
                    // processingLargeResponds here.
                    if (respondCache.GetAllData())
                    {
                        Debug.WriteLine("large responds");
                        ProcessLargeResponds();
                    }
                    else //get all data first
                    {
                        //Go On Get The Responds data
                        try
                        {
                            destinationSocketStream.BeginRead(RemoteBuffer, 0, RemoteBuffer.Length, new AsyncCallback(OnReceiveRemoteHttpResponds), destinationSocketStream);
                        }
                        catch
                        {
                            Dispose();
                            return;
                        }
                    }
                }
            }
            else
            {
                if (respondCache.GetAllData())
                {
                    // Debug.WriteLine(clientSocketStream.get)
                    if (isHttpsRequest)
                    {
                       Debug.WriteLine("\r\nhttps responds:\r\n"
                            + requestCache.requestHeader.part1OfFirstLine + " https://"
                            + requestCache.requestHeader.GetValue("host")
                            + requestCache.requestHeader.part2OfFirstLine + " " + requestCache.requestHeader.part3OfFirstLine + " " + respondCache.respondCodeOfSecondHeader
                            + "\r\n");
                    }
                    else
                    {
                        Debug.WriteLine("\r\nhttp responds:\r\n"
                            + requestCache.requestHeader.part1OfFirstLine + " " + requestCache.requestHeader.part2OfFirstLine 
                            + " " + requestCache.requestHeader.part3OfFirstLine + " " + respondCache.respondCodeOfSecondHeader
                            + "\r\n");
                    }

                    byte[] sentData = null;
                    int dataLength = 0;
                    respondCache.respondsHeader2.AddPair("accept-ranges", "none");
                    bool isTextContent = true;//default is text/* and so on
                    if (respondCache.respondsHeader2.HasKey("content-type".ToLower()))
                    {
                        string type = respondCache.respondsHeader2.GetValue("content-type".ToLower());
                        if (type.ToLower().Contains("text") == false)
                        {
                            isTextContent = false;
                        }
                    }

                    int zipContentLength = respondCache.contentLengthOfFirstHeader - (respondCache.httpHeader2EndPosition - respondCache.httpHeader1EndPosition);
                    int zipContentOffset = respondCache.httpHeader2EndPosition + 1;

                    byte[] zipContent = new byte[zipContentLength];
                    Array.Copy(respondCache.Data, zipContentOffset, zipContent, 0, zipContentLength);
                    byte[] unzipContent = null;
                    if (isTextContent)
                    {
                        unzipContent = Bin.UnzipContent(zipContent);//decompress text content
                    }
                    else
                    {
                        unzipContent = zipContent;//no need to decompress non-text content since they never get compressed
                    }

                    string newHeader = respondCache.respondsHeader2.firstLineOfHeader + "\r\n";
                    respondCache.respondsHeader2.AddPair("Content-Length", unzipContent.Length.ToString());
                    foreach (string item in respondCache.respondsHeader2.Keys())
                    {
                        newHeader += item + ": " + respondCache.respondsHeader2.GetValue(item) + "\r\n";
                    }

                    newHeader += "\r\n";
                    byte[] newHeaderBytes = Encoding.ASCII.GetBytes(newHeader);
                    byte[] sendBuffer = new byte[newHeaderBytes.Length + unzipContent.Length];

                    Array.Copy(newHeaderBytes, sendBuffer, newHeaderBytes.Length);
                    Array.Copy(unzipContent, 0, sendBuffer, newHeaderBytes.Length, unzipContent.Length);
                    //string str = Encoding.ASCII.GetString(sendBuffer, 0, sendBuffer.Length);
                    //int len = 200;
                    //  if(str.Length < len)
                    //{
                    //    len = str.Length;
                    // }
                    //  Debug.WriteLine("\r\nData Sent To: \r\n" + str.Substring(0, len) + "\r\n");
                    sentData = sendBuffer;
                    dataLength = sendBuffer.Length;
                    try
                    {
                        clientSocketStream.BeginWrite(sentData, 0, dataLength, new AsyncCallback(this.OnSendClientHttpData), clientSocketStream);

                    }
                    catch
                    {
                        this.Dispose();
                    }
                    return;
                }
                else
                {
                    //Go On Get The Responds data
                    try
                    {
                        destinationSocketStream.BeginRead(RemoteBuffer, 0, RemoteBuffer.Length, new AsyncCallback(OnReceiveRemoteHttpResponds), destinationSocketStream);
                    }
                    catch
                    {
                        Dispose();
                        return;
                    }
                }
            }

            return;
        }
        private void OnReceiveRemoteHttpResponds(IAsyncResult ar)
        {
            int Ret;
            try
            {
                Ret = destinationSocketStream.EndRead(ar);
            }
            catch
            {
                Ret = -1;
            }
            if (Ret <= 0)
            { //Connection is dead :(
                Dispose();
                return;
            }
            if (respondCache as object == null)
            {
                respondCache = new HttpRespondSnifferCache(32 * 1024);
            }
            respondCache.addDataToCache(RemoteBuffer, Ret);

            if (respondCache.GetAllHeaders())
            {
                ProcessResponds();
                //else, keep listening
            }
            else
            {
                try
                {
                    destinationSocketStream.BeginRead(RemoteBuffer, 0, RemoteBuffer.Length, new AsyncCallback(OnReceiveRemoteHttpResponds), destinationSocketStream);
                }
                catch
                {
                    Dispose();
                }
            }
        }
        private void OnQuerySent(IAsyncResult ar)
        {
            try
            {
                destinationSocketStream.EndWrite(ar);
                destinationSocketStream.BeginRead(RemoteBuffer, 0, RemoteBuffer.Length, new AsyncCallback(OnReceiveRemoteHttpResponds), destinationSocketStream);
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.ToString());
                Dispose();
            }

        }
        private void OnSendClientPartialHttpData(IAsyncResult ar)
        {

            try
            {
                clientSocketStream.EndWrite(ar);
            }
            catch
            {
                Dispose();
            }
            //do not close the connection since we have sent partial data to client
            //this.Dispose();
        }
        private void OnSendClientHttpData(IAsyncResult ar)
        {

            try
            {
                clientSocketStream.EndWrite(ar);
            }
            catch
            {
                Dispose();
            }
            //close the connection since we have sent all the data to client
            this.Dispose();
        }
        ///<summary>Called when we're connected to the requested remote host.</summary>
        ///<param name="ar">The result of the asynchronous operation.</param>
        private void OnConnected(IAsyncResult ar)
        {
            try
            {
                DestinationSocket.EndConnect(ar);
                if (isHttpsRequest)
                {
                    StartSSLHandshake();
                }
                else
                {
                    string rq = requestCache.RebuildQuery(isHttpsRequest);
                    if (destinationSocketStream as object != null)
                    {
                        destinationSocketStream = null;
                        //destinationNetworkStream = null;
                    }
                    if (clientSocketStream as object != null)
                    {
                        clientSocketStream = null;
                    }

                    destinationSocketStream = new NetworkStream(DestinationSocket, false);
                    clientSocketStream = new NetworkStream(ClientSocket, false);
                    destinationSocketStream.BeginWrite(Encoding.ASCII.GetBytes(rq), 0, rq.Length, new AsyncCallback(OnQuerySent), destinationSocketStream);

                }
            }
            catch
            {
                Dispose();
            }
        }
    }


    sealed class HttpListener : Listener
    {
        static int cnt = 0;
        ///<summary>Initializes a new instance of the HttpListener class.</summary>
        ///<param name="Port">The port to listen on.</param>
        ///<remarks>The HttpListener will start listening on all installed network cards.</remarks>
        public HttpListener(int Port) : this(IPAddress.Any, Port) { }
        ///<summary>Initializes a new instance of the HttpListener class.</summary>
        ///<param name="Port">The port to listen on.</param>
        ///<param name="Address">The address to listen on. You can specify IPAddress.Any to listen on all installed network cards.</param>
        ///<remarks>For the security of your server, try to avoid to listen on every network card (IPAddress.Any). Listening on a local IP address is usually sufficient and much more secure.</remarks>
        public HttpListener(IPAddress Address, int Port) : base(Port, Address) { }
        ///<summary>Called when there's an incoming client connection waiting to be accepted.</summary>
        ///<param name="ar">The result of the asynchronous operation.</param>
        public override void OnAccept(IAsyncResult ar)
        {

            try
            {
                Socket NewSocket = ListenSocket.EndAccept(ar);
                if (NewSocket as object != null)
                {
                    HttpClient NewClient = new HttpClient(NewSocket, new DestroyDelegate(this.RemoveClient));
                    AddClient(NewClient);
                    NewClient.StartHandshake();
                    HttpListener.cnt += 1;
                    //                    Debug.WriteLine("Connected :" + cnt.ToString());
                }
            }
            catch { }
            try
            {
                //Restart Listening
                ListenSocket.BeginAccept(new AsyncCallback(this.OnAccept), ListenSocket);
            }
            catch
            {
                Dispose();
            }
        }
        ///<summary>Returns a string representation of this object.</summary>
        ///<returns>A string with information about this object.</returns>
        public override string ToString()
        {
            return "HTTP service on " + Address.ToString() + ":" + Port.ToString();
        }
        ///<summary>Returns a string that holds all the construction information for this object.</summary>
        ///<value>A string that holds all the construction information for this object.</value>
        public override string ConstructString
        {
            get
            {
                return "host:" + Address.ToString() + ";int:" + Port.ToString();
            }
        }
    }
    public partial class MainForm : Form
    {
        private ContextMenu notifyiconMnu;
        public MainForm()
        {
            InitializeComponent();
            Initializenotifyicon();
            this.Icon = new Icon(DataHelper.GetAppDirectory() + DataHelper.PROXY_ICON);
            this.notifyIcon.Icon = new Icon(DataHelper.GetAppDirectory() + DataHelper.PROXY_ICON);
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeControlStatus();
        }

        private void InitializeControlStatus()
        {
            string[] configs = DataHelper.ParseConfig();
            string fetchServer = configs[1];
            string proxy = configs[2];

            if (fetchServer as object != null)
            {
                this.fetchserverEdit.Text = string.Copy(fetchServer);
                this.fetchserverCheckBox.Checked = true;
            }
            if (proxy as object != null)
            {
                this.proxyEdit.Text = string.Copy(proxy);
                this.proxyCheckBox.Checked = true;
            }
            //disable status button and Service button
            this.statusButton.Enabled = this.serviceButton.Enabled = false;
        }

        private void Initializenotifyicon()
        {
            this.notifyIcon.Visible = false;

            MenuItem[] mnuItms = new MenuItem[7];
            mnuItms[0] = new MenuItem();
            mnuItms[0].Text = "Restore";
            mnuItms[0].Click += new System.EventHandler(this.OnRestore);


            mnuItms[1] = new MenuItem("-");

            mnuItms[2] = new MenuItem();
            mnuItms[2].Text = "Exit";
            mnuItms[2].Click += new System.EventHandler(this.OnExit);
            // mnuItms[2].DefaultItem = true;

            mnuItms[3] = new MenuItem("-");

            mnuItms[4] = new MenuItem();
            mnuItms[4].Text = "Restart";
            mnuItms[4].Click += new System.EventHandler(this.OnRestart);
			
			mnuItms[5] = new MenuItem("-");

            mnuItms[6] = new MenuItem();
            mnuItms[6].Text = "ProxyEnabled";
			mnuItms[6].Checked = true;
            mnuItms[6].Click += new System.EventHandler(this.OnEnableProxy);
			
            notifyiconMnu = new ContextMenu(mnuItms);
            this.notifyIcon.ContextMenu = notifyiconMnu;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (new CenterWinDialog(this))
            {
                if (this.weAreRestarting == true)
                {
                    this.weAreRestarting = false;// no warning pop up here
                }
                else
                {
                    if (MessageBox.Show("Do you really want to quit?", "Confirm",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }

            }

        }
        public void OnRestore(object sender, System.EventArgs e)
        {
            showMainWindow();
        }
		
        public void DisableProxy()
        {
            BrowserProxySetting.UnsetProxy();
        }

        public void EnableProxy()
        {
            BrowserProxySetting.SetProxy("127.0.0.1:" + HTTPProxy.httpProxyListenPort.ToString());
        }
        public void OnEnableProxy(object sender, System.EventArgs e)
        {
            MenuItem item = sender as MenuItem;
            if(item == null)
            {

            this.notifyIcon.Visible = false;
            //we exit the application directly because some one (like me) hates the confirmation dialog.
            //it is very stupid, but ... bla bla bla...
            PostQuitMessage(0);			
                return;
            }
            if(item.Checked)
            {
                item.Checked = false;
                DisableProxy();
            }
            else
            {
                item.Checked = true;
                EnableProxy();
            }
         //   showMainWindow();
        }
        public void OnRestart(object sender, System.EventArgs e)
        {
            this.notifyIcon.Visible = false;
            restartButton_Click(sender, e);
        }
        public void OnExit(object sender, System.EventArgs e)
        {

            this.notifyIcon.Visible = false;
            //we exit the application directly because some one (like me) hates the confirmation dialog.
            //it is very stupid, but ... bla bla bla...
            PostQuitMessage(0);
            //            this.Close();
        }
        private void hideButton_Click(object sender, EventArgs e)
        {
            hideMainWindow();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            showMainWindow();
        }

        private void showMainWindow()
        {
            Show();
            WindowState = FormWindowState.Normal;
            this.notifyIcon.Visible = false;
        }

        private void hideMainWindow()
        {
            Hide();
            this.notifyIcon.Visible = true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string[] configs = DataHelper.ParseConfig();
            string proxy = this.proxyEdit.Text.ToLower().Trim();
            string server = this.fetchserverEdit.Text.ToLower().Trim();

            if (this.proxyCheckBox.Checked == false)
            {
                proxy = "";
            }
            if (this.fetchserverCheckBox.Checked == false)
            {
                server = "";
            }

            if (proxy != configs[2].ToLower().Trim()
                ||
                server != configs[1].ToLower().Trim())
            {
                string[] temp = new String[]
                {
                    configs[0],
                    server,
                    proxy
                };
                DataHelper.WriteConfig(temp);
            }

        }

        private void useProxy()
        {
            this.proxyEdit.Enabled = this.proxyCheckBox.Checked;
        }

        private void useFetchServer()
        {
            this.fetchserverEdit.Enabled = this.fetchserverCheckBox.Checked;
        }

        private void applyChange()
        {
            string localProxy = this.proxyEdit.Text.Trim();
        }

        private void proxyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            useProxy();
        }

        private void fetchserverCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            useFetchServer();
        }


        [DllImport("user32.dll")]
        static extern void PostQuitMessage(int nExitCode);
        private void restartButton_Click(object sender, EventArgs e)
        {
            // PostQuitMessage(0);
            this.weAreRestarting = true;
            Application.Restart();
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            using (new CenterWinDialog(this))
            {
                string help = "Save:\t保存你的设置到配置文件.\n" +
                            "Restart:\t重新启动Proxy程序.\n" +
                            "Hide:\t隐藏主窗口.\n";
                MessageBox.Show(help, "Help",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
            }

        }

        private void statusButton_Click(object sender, EventArgs e)
        {

        }

        private void serviceButton_Click(object sender, EventArgs e)
        {

        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            using (new CenterWinDialog(this))
            {
                string help = "GAppProxy CSharp Client.\n" +
                            "A free HTTP proxy based on Google App Engine.\n" +
                            "Version: 1.0.0 beta.\n" +
                            "License: GPLv3.\n" +
                            "Maintained By:Aemon.\n" +
                            "Contact:gaeproxy.csharp.client@gmail.com\n";
                MessageBox.Show(help, "Help",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
            }

        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components as object != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            //            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.restartButton = new System.Windows.Forms.Button();
            this.fetchserverEdit = new System.Windows.Forms.TextBox();
            this.proxyEdit = new System.Windows.Forms.TextBox();
            this.fetchserverCheckBox = new System.Windows.Forms.CheckBox();
            this.proxyCheckBox = new System.Windows.Forms.CheckBox();
            this.statusButton = new System.Windows.Forms.Button();
            this.serviceButton = new System.Windows.Forms.Button();
            this.hideButton = new System.Windows.Forms.Button();
            this.aboutButton = new System.Windows.Forms.Button();
            this.quitButton = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.saveButton);
            this.groupBox.Controls.Add(this.helpButton);
            this.groupBox.Controls.Add(this.restartButton);
            this.groupBox.Controls.Add(this.fetchserverEdit);
            this.groupBox.Controls.Add(this.proxyEdit);
            this.groupBox.Controls.Add(this.fetchserverCheckBox);
            this.groupBox.Controls.Add(this.proxyCheckBox);
            this.groupBox.Location = new System.Drawing.Point(10, 10);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(381, 111);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Setup";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(270, 80);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // helpButton
            // 
            this.helpButton.Location = new System.Drawing.Point(170, 80);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(75, 23);
            this.helpButton.TabIndex = 5;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // restartButton
            // 
            this.restartButton.Location = new System.Drawing.Point(70, 80);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(75, 23);
            this.restartButton.TabIndex = 4;
            this.restartButton.Text = "Restart";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // fetchserverEdit
            // 
            this.fetchserverEdit.Location = new System.Drawing.Point(130, 50);
            this.fetchserverEdit.Name = "fetchserverEdit";
            this.fetchserverEdit.Size = new System.Drawing.Size(240, 20);
            this.fetchserverEdit.TabIndex = 3;
            // 
            // proxyEdit
            // 
            this.proxyEdit.Location = new System.Drawing.Point(130, 20);
            this.proxyEdit.Name = "proxyEdit";
            this.proxyEdit.Size = new System.Drawing.Size(240, 20);
            this.proxyEdit.TabIndex = 2;
            // 
            // fetchserverCheckBox
            // 
            this.fetchserverCheckBox.AutoSize = true;
            this.fetchserverCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fetchserverCheckBox.Location = new System.Drawing.Point(10, 50);
            this.fetchserverCheckBox.Name = "fetchserverCheckBox";
            this.fetchserverCheckBox.Size = new System.Drawing.Size(109, 17);
            this.fetchserverCheckBox.TabIndex = 1;
            this.fetchserverCheckBox.Text = "Use FetchServer:";
            this.fetchserverCheckBox.UseVisualStyleBackColor = true;
            this.fetchserverCheckBox.CheckedChanged += new System.EventHandler(this.fetchserverCheckBox_CheckedChanged);
            // 
            // proxyCheckBox
            // 
            this.proxyCheckBox.AutoSize = true;
            this.proxyCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.proxyCheckBox.Location = new System.Drawing.Point(10, 20);
            this.proxyCheckBox.Name = "proxyCheckBox";
            this.proxyCheckBox.Size = new System.Drawing.Size(106, 17);
            this.proxyCheckBox.TabIndex = 0;
            this.proxyCheckBox.Text = "Use Local Proxy:";
            this.proxyCheckBox.UseVisualStyleBackColor = true;
            this.proxyCheckBox.CheckedChanged += new System.EventHandler(this.proxyCheckBox_CheckedChanged);
            // 
            // statusButton
            // 
            this.statusButton.Location = new System.Drawing.Point(30, 130);
            this.statusButton.Name = "statusButton";
            this.statusButton.Size = new System.Drawing.Size(60, 23);
            this.statusButton.TabIndex = 1;
            this.statusButton.Text = "Status";
            this.statusButton.UseVisualStyleBackColor = true;
            this.statusButton.Click += new System.EventHandler(this.statusButton_Click);
            // 
            // serviceButton
            // 
            this.serviceButton.Location = new System.Drawing.Point(100, 130);
            this.serviceButton.Name = "serviceButton";
            this.serviceButton.Size = new System.Drawing.Size(60, 23);
            this.serviceButton.TabIndex = 2;
            this.serviceButton.Text = "Service";
            this.serviceButton.UseVisualStyleBackColor = true;
            this.serviceButton.Click += new System.EventHandler(this.serviceButton_Click);
            // 
            // hideButton
            // 
            this.hideButton.Location = new System.Drawing.Point(170, 130);
            this.hideButton.Name = "hideButton";
            this.hideButton.Size = new System.Drawing.Size(60, 23);
            this.hideButton.TabIndex = 3;
            this.hideButton.Text = "Hide";
            this.hideButton.UseVisualStyleBackColor = true;
            this.hideButton.Click += new System.EventHandler(this.hideButton_Click);
            // 
            // aboutButton
            // 
            this.aboutButton.Location = new System.Drawing.Point(240, 130);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(60, 23);
            this.aboutButton.TabIndex = 4;
            this.aboutButton.Text = "About";
            this.aboutButton.UseVisualStyleBackColor = true;
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // quitButton
            // 
            this.quitButton.Location = new System.Drawing.Point(310, 130);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(60, 23);
            this.quitButton.TabIndex = 5;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // notifyIcon
            // 
            //this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "GAE Proxy CSharp Client";
            this.notifyIcon.Visible = false;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 177);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.aboutButton);
            this.Controls.Add(this.hideButton);
            this.Controls.Add(this.serviceButton);
            this.Controls.Add(this.statusButton);
            this.Controls.Add(this.groupBox);
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "GAppProxy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            //            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.CheckBox fetchserverCheckBox;
        private System.Windows.Forms.CheckBox proxyCheckBox;
        private System.Windows.Forms.TextBox fetchserverEdit;
        private System.Windows.Forms.TextBox proxyEdit;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.Button statusButton;
        private System.Windows.Forms.Button serviceButton;
        private System.Windows.Forms.Button hideButton;
        private System.Windows.Forms.Button aboutButton;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private bool weAreRestarting = false;
    }
    /*used  to center the dialog of MainForm*/
    class CenterWinDialog : IDisposable
    {
        private int mTries = 0;
        private Form mOwner;

        public CenterWinDialog(Form owner)
        {
            mOwner = owner;
            owner.BeginInvoke(new MethodInvoker(findDialog));
        }

        private void findDialog()
        {
            // Enumerate windows to find the message box 
            if (mTries < 0) return;
            EnumThreadWndProc callback = new EnumThreadWndProc(checkWindow);
            if (EnumThreadWindows(GetCurrentThreadId(), callback, IntPtr.Zero))
            {
                if (++mTries < 10) mOwner.BeginInvoke(new MethodInvoker(findDialog));
            }
        }
        private bool checkWindow(IntPtr hWnd, IntPtr lp)
        {
            // Checks if <hWnd> is a dialog 
            StringBuilder sb = new StringBuilder(260);
            GetClassName(hWnd, sb, sb.Capacity);
            if (sb.ToString() != "#32770") return true;
            // Got it 
            Rectangle frmRect = new Rectangle(mOwner.Location, mOwner.Size);
            RECT dlgRect;
            GetWindowRect(hWnd, out dlgRect);
            MoveWindow(hWnd,
                frmRect.Left + (frmRect.Width - dlgRect.Right + dlgRect.Left) / 2,
                frmRect.Top + (frmRect.Height - dlgRect.Bottom + dlgRect.Top) / 2,
                dlgRect.Right - dlgRect.Left,
                dlgRect.Bottom - dlgRect.Top, true);
            return false;
        }
        public void Dispose()
        {
            mTries = -1;
        }

        // P/Invoke declarations 
        private delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lp);
        [DllImport("user32.dll")]
        private static extern bool EnumThreadWindows(int tid, EnumThreadWndProc callback, IntPtr lp);
        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();
        [DllImport("user32.dll")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder buffer, int buflen);
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT rc);
        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);
        private struct RECT { public int Left; public int Top; public int Right; public int Bottom; }
    }

    //the following is the zlib source code copied from zlib.net which is open sourced and free for non-commerical used.
    // Copyright (c) 2006, ComponentAce
    // http://www.componentace.com
    // All rights reserved.

    // Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

    // Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer. 
    // Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution. 
    // Neither the name of ComponentAce nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission. 
    // THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

    /*
     * the following is used to change IE proxy setting.
     */
    public class BrowserProxySetting
    {
	    [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;
        public static void UnsetProxy()
        {
            SetProxy(null, null);
        }
        public static void SetProxy(string strProxy)
        {
            SetProxy(strProxy, null);
        }

        public static void  SetProxy(string strProxy, string exceptions)
        {
			// open registry key needed
			RegistryKey InternetSettings = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
			
			// write to registry
			if(strProxy == null)
			{
				InternetSettings.SetValue("ProxyEnable", 0);
			}
			else
			{
				InternetSettings.SetValue("ProxyEnable", 1);
				InternetSettings.SetValue("ProxyServer", strProxy);
			}
			
			// close key
			InternetSettings.Close();

			// refresh internet settings
			InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
			InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }
    }

    /*
    Copyright (c) 2000,2001,2002,2003 ymnk, JCraft,Inc. All rights reserved.

    Redistribution and use in source and binary forms, with or without
    modification, are permitted provided that the following conditions are met:

    1. Redistributions of source code must retain the above copyright notice,
    this list of conditions and the following disclaimer.

    2. Redistributions in binary form must reproduce the above copyright 
    notice, this list of conditions and the following disclaimer in 
    the documentation and/or other materials provided with the distribution.

    3. The names of the authors may not be used to endorse or promote products
    derived from this software without specific prior written permission.

    THIS SOFTWARE IS PROVIDED ``AS IS'' AND ANY EXPRESSED OR IMPLIED WARRANTIES,
    INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
    FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL JCRAFT,
    INC. OR ANY CONTRIBUTORS TO THIS SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT,
    INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
    LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA,
    OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
    LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
    NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
    EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
    */
    /*
    * This program is based on zlib-1.1.3, so all credit should go authors
    * Jean-loup Gailly(jloup@gzip.org) and Mark Adler(madler@alumni.caltech.edu)
    * and contributors of zlib.
    */

    public class ZStreamException : System.IO.IOException
    {
        public ZStreamException()
            : base()
        {
        }
        public ZStreamException(System.String s)
            : base(s)
        {
        }
    }
    public class ZOutputStream : System.IO.Stream
    {
        private void InitBlock()
        {
            flush_Renamed_Field = zlibConst.Z_NO_FLUSH;
            buf = new byte[bufsize];
        }
        virtual public int FlushMode
        {
            get
            {
                return (flush_Renamed_Field);
            }

            set
            {
                this.flush_Renamed_Field = value;
            }

        }
        /// <summary> Returns the total number of bytes input so far.</summary>
        virtual public long TotalIn
        {
            get
            {
                return z.total_in;
            }

        }
        /// <summary> Returns the total number of bytes output so far.</summary>
        virtual public long TotalOut
        {
            get
            {
                return z.total_out;
            }

        }

        protected internal ZStream z = new ZStream();
        protected internal int bufsize = 4096;
        protected internal int flush_Renamed_Field;
        protected internal byte[] buf, buf1 = new byte[1];
        protected internal bool compress;

        private System.IO.Stream out_Renamed;

        public ZOutputStream(System.IO.Stream out_Renamed)
            : base()
        {
            InitBlock();
            this.out_Renamed = out_Renamed;
            z.inflateInit();
            compress = false;
        }

        public ZOutputStream(System.IO.Stream out_Renamed, int level)
            : base()
        {
            InitBlock();
            this.out_Renamed = out_Renamed;
            z.deflateInit(level);
            compress = true;
        }

        public void WriteByte(int b)
        {
            buf1[0] = (byte)b;
            Write(buf1, 0, 1);
        }
        //UPGRADE_TODO: The differences in the Expected value  of parameters for method 'WriteByte'  may cause compilation errors.  'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1092_3"'
        public override void WriteByte(byte b)
        {
            WriteByte((int)b);
        }

        public override void Write(System.Byte[] b1, int off, int len)
        {
            if (len == 0)
                return;
            int err;
            byte[] b = new byte[b1.Length];
            System.Array.Copy(b1, 0, b, 0, b1.Length);
            z.next_in = b;
            z.next_in_index = off;
            z.avail_in = len;
            do
            {
                z.next_out = buf;
                z.next_out_index = 0;
                z.avail_out = bufsize;
                if (compress)
                    err = z.deflate(flush_Renamed_Field);
                else
                    err = z.inflate(flush_Renamed_Field);
                if (err != zlibConst.Z_OK && err != zlibConst.Z_STREAM_END)
                    throw new ZStreamException((compress ? "de" : "in") + "flating: " + z.msg);
                out_Renamed.Write(buf, 0, bufsize - z.avail_out);
            }
            while (z.avail_in > 0 || z.avail_out == 0);
        }

        public virtual void finish()
        {
            int err;
            do
            {
                z.next_out = buf;
                z.next_out_index = 0;
                z.avail_out = bufsize;
                if (compress)
                {
                    err = z.deflate(zlibConst.Z_FINISH);
                }
                else
                {
                    err = z.inflate(zlibConst.Z_FINISH);
                }
                if (err != zlibConst.Z_STREAM_END && err != zlibConst.Z_OK)
                    throw new ZStreamException((compress ? "de" : "in") + "flating: " + z.msg);
                if (bufsize - z.avail_out > 0)
                {
                    out_Renamed.Write(buf, 0, bufsize - z.avail_out);
                }
            }
            while (z.avail_in > 0 || z.avail_out == 0);
            try
            {
                Flush();
            }
            catch
            {
            }
        }
        public virtual void end()
        {
            if (compress)
            {
                z.deflateEnd();
            }
            else
            {
                z.inflateEnd();
            }
            z.free();
            z = null;
        }
        public override void Close()
        {
            try
            {
                try
                {
                    finish();
                }
                catch
                {
                }
            }
            finally
            {
                end();
                out_Renamed.Close();
                out_Renamed = null;
            }
        }

        public override void Flush()
        {
            out_Renamed.Flush();
        }
        //UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
        public override System.Int32 Read(System.Byte[] buffer, System.Int32 offset, System.Int32 count)
        {
            return 0;
        }
        //UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
        public override void SetLength(System.Int64 value)
        {
        }
        //UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
        public override System.Int64 Seek(System.Int64 offset, System.IO.SeekOrigin origin)
        {
            return 0;
        }
        //UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
        public override System.Boolean CanRead
        {
            get
            {
                return false;
            }

        }
        //UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
        public override System.Boolean CanSeek
        {
            get
            {
                return false;
            }

        }
        //UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
        public override System.Boolean CanWrite
        {
            get
            {
                return false;
            }

        }
        //UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
        public override System.Int64 Length
        {
            get
            {
                return 0;
            }

        }
        //UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1232_3"'
        public override System.Int64 Position
        {
            get
            {
                return 0;
            }

            set
            {
            }

        }
    }
    sealed public class zlibConst
    {
        private const System.String version_Renamed_Field = "1.0.2";
        public static System.String version()
        {
            return version_Renamed_Field;
        }

        // compression levels
        public const int Z_NO_COMPRESSION = 0;
        public const int Z_BEST_SPEED = 1;
        public const int Z_BEST_COMPRESSION = 9;
        public const int Z_DEFAULT_COMPRESSION = (-1);

        // compression strategy
        public const int Z_FILTERED = 1;
        public const int Z_HUFFMAN_ONLY = 2;
        public const int Z_DEFAULT_STRATEGY = 0;

        public const int Z_NO_FLUSH = 0;
        public const int Z_PARTIAL_FLUSH = 1;
        public const int Z_SYNC_FLUSH = 2;
        public const int Z_FULL_FLUSH = 3;
        public const int Z_FINISH = 4;

        public const int Z_OK = 0;
        public const int Z_STREAM_END = 1;
        public const int Z_NEED_DICT = 2;
        public const int Z_ERRNO = -1;
        public const int Z_STREAM_ERROR = -2;
        public const int Z_DATA_ERROR = -3;
        public const int Z_MEM_ERROR = -4;
        public const int Z_BUF_ERROR = -5;
        public const int Z_VERSION_ERROR = -6;
    }
    sealed class Tree
    {
        private const int MAX_BITS = 15;
        private const int BL_CODES = 19;
        private const int D_CODES = 30;
        private const int LITERALS = 256;
        private const int LENGTH_CODES = 29;
        private static readonly int L_CODES = (LITERALS + 1 + LENGTH_CODES);
        private static readonly int HEAP_SIZE = (2 * L_CODES + 1);

        // Bit length codes must not exceed MAX_BL_BITS bits
        internal const int MAX_BL_BITS = 7;

        // end of block literal code
        internal const int END_BLOCK = 256;

        // repeat previous bit length 3-6 times (2 bits of repeat count)
        internal const int REP_3_6 = 16;

        // repeat a zero length 3-10 times  (3 bits of repeat count)
        internal const int REPZ_3_10 = 17;

        // repeat a zero length 11-138 times  (7 bits of repeat count)
        internal const int REPZ_11_138 = 18;

        // extra bits for each length code		
        internal static readonly int[] extra_lbits = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0 };

        // extra bits for each distance code		
        internal static readonly int[] extra_dbits = new int[] { 0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13 };

        // extra bits for each bit length code		
        internal static readonly int[] extra_blbits = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 7 };

        internal static readonly byte[] bl_order = new byte[] { 16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15 };


        // The lengths of the bit length codes are sent in order of decreasing
        // probability, to avoid transmitting the lengths for unused bit
        // length codes.

        internal const int Buf_size = 8 * 2;

        // see definition of array dist_code below
        internal const int DIST_CODE_LEN = 512;

        internal static readonly byte[] _dist_code = new byte[]{0, 1, 2, 3, 4, 4, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 8, 8, 8, 8, 9, 9, 9, 9, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 0, 0, 16, 17, 18, 18, 19, 19, 20, 20, 20, 20, 21, 21, 21, 21, 22, 22, 22, 22, 22, 22, 22, 22, 23, 23, 23, 23, 23, 23, 23, 23, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 
			29, 29, 29, 29, 29, 29, 29, 29, 29};

        internal static readonly byte[] _length_code = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 12, 12, 13, 13, 13, 13, 14, 14, 14, 14, 15, 15, 15, 15, 16, 16, 16, 16, 16, 16, 16, 16, 17, 17, 17, 17, 17, 17, 17, 17, 18, 18, 18, 18, 18, 18, 18, 18, 19, 19, 19, 19, 19, 19, 19, 19, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 28 };

        internal static readonly int[] base_length = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 12, 14, 16, 20, 24, 28, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 0 };

        internal static readonly int[] base_dist = new int[] { 0, 1, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 64, 96, 128, 192, 256, 384, 512, 768, 1024, 1536, 2048, 3072, 4096, 6144, 8192, 12288, 16384, 24576 };

        // Mapping from a distance to a distance code. dist is the distance - 1 and
        // must not have side effects. _dist_code[256] and _dist_code[257] are never
        // used.
        internal static int d_code(int dist)
        {
            return ((dist) < 256 ? _dist_code[dist] : _dist_code[256 + (SupportClass.URShift((dist), 7))]);
        }

        internal short[] dyn_tree; // the dynamic tree
        internal int max_code; // largest code with non zero frequency
        internal StaticTree stat_desc; // the corresponding static tree

        // Compute the optimal bit lengths for a tree and update the total bit length
        // for the current block.
        // IN assertion: the fields freq and dad are set, heap[heap_max] and
        //    above are the tree nodes sorted by increasing frequency.
        // OUT assertions: the field len is set to the optimal bit length, the
        //     array bl_count contains the frequencies for each bit length.
        //     The length opt_len is updated; static_len is also updated if stree is
        //     not null.
        internal void gen_bitlen(Deflate s)
        {
            short[] tree = dyn_tree;
            short[] stree = stat_desc.static_tree;
            int[] extra = stat_desc.extra_bits;
            int base_Renamed = stat_desc.extra_base;
            int max_length = stat_desc.max_length;
            int h; // heap index
            int n, m; // iterate over the tree elements
            int bits; // bit length
            int xbits; // extra bits
            short f; // frequency
            int overflow = 0; // number of elements with bit length too large

            for (bits = 0; bits <= MAX_BITS; bits++)
                s.bl_count[bits] = 0;

            // In a first pass, compute the optimal bit lengths (which may
            // overflow in the case of the bit length tree).
            tree[s.heap[s.heap_max] * 2 + 1] = 0; // root of the heap

            for (h = s.heap_max + 1; h < HEAP_SIZE; h++)
            {
                n = s.heap[h];
                bits = tree[tree[n * 2 + 1] * 2 + 1] + 1;
                if (bits > max_length)
                {
                    bits = max_length; overflow++;
                }
                tree[n * 2 + 1] = (short)bits;
                // We overwrite tree[n*2+1] which is no longer needed

                if (n > max_code)
                    continue; // not a leaf node

                s.bl_count[bits]++;
                xbits = 0;
                if (n >= base_Renamed)
                    xbits = extra[n - base_Renamed];
                f = tree[n * 2];
                s.opt_len += f * (bits + xbits);
                if (stree != null)
                    s.static_len += f * (stree[n * 2 + 1] + xbits);
            }
            if (overflow == 0)
                return;

            // This happens for example on obj2 and pic of the Calgary corpus
            // Find the first bit length which could increase:
            do
            {
                bits = max_length - 1;
                while (s.bl_count[bits] == 0)
                    bits--;
                s.bl_count[bits]--; // move one leaf down the tree
                s.bl_count[bits + 1] = (short)(s.bl_count[bits + 1] + 2); // move one overflow item as its brother
                s.bl_count[max_length]--;
                // The brother of the overflow item also moves one step up,
                // but this does not affect bl_count[max_length]
                overflow -= 2;
            }
            while (overflow > 0);

            for (bits = max_length; bits != 0; bits--)
            {
                n = s.bl_count[bits];
                while (n != 0)
                {
                    m = s.heap[--h];
                    if (m > max_code)
                        continue;
                    if (tree[m * 2 + 1] != bits)
                    {
                        s.opt_len = (int)(s.opt_len + ((long)bits - (long)tree[m * 2 + 1]) * (long)tree[m * 2]);
                        tree[m * 2 + 1] = (short)bits;
                    }
                    n--;
                }
            }
        }

        // Construct one Huffman tree and assigns the code bit strings and lengths.
        // Update the total bit length for the current block.
        // IN assertion: the field freq is set for all tree elements.
        // OUT assertions: the fields len and code are set to the optimal bit length
        //     and corresponding code. The length opt_len is updated; static_len is
        //     also updated if stree is not null. The field max_code is set.
        internal void build_tree(Deflate s)
        {
            short[] tree = dyn_tree;
            short[] stree = stat_desc.static_tree;
            int elems = stat_desc.elems;
            int n, m; // iterate over heap elements
            int max_code = -1; // largest code with non zero frequency
            int node; // new node being created

            // Construct the initial heap, with least frequent element in
            // heap[1]. The sons of heap[n] are heap[2*n] and heap[2*n+1].
            // heap[0] is not used.
            s.heap_len = 0;
            s.heap_max = HEAP_SIZE;

            for (n = 0; n < elems; n++)
            {
                if (tree[n * 2] != 0)
                {
                    s.heap[++s.heap_len] = max_code = n;
                    s.depth[n] = 0;
                }
                else
                {
                    tree[n * 2 + 1] = 0;
                }
            }

            // The pkzip format requires that at least one distance code exists,
            // and that at least one bit should be sent even if there is only one
            // possible code. So to avoid special checks later on we force at least
            // two codes of non zero frequency.
            while (s.heap_len < 2)
            {
                node = s.heap[++s.heap_len] = (max_code < 2 ? ++max_code : 0);
                tree[node * 2] = 1;
                s.depth[node] = 0;
                s.opt_len--;
                if (stree != null)
                    s.static_len -= stree[node * 2 + 1];
                // node is 0 or 1 so it does not have extra bits
            }
            this.max_code = max_code;

            // The elements heap[heap_len/2+1 .. heap_len] are leaves of the tree,
            // establish sub-heaps of increasing lengths:

            for (n = s.heap_len / 2; n >= 1; n--)
                s.pqdownheap(tree, n);

            // Construct the Huffman tree by repeatedly combining the least two
            // frequent nodes.

            node = elems; // next internal node of the tree
            do
            {
                // n = node of least frequency
                n = s.heap[1];
                s.heap[1] = s.heap[s.heap_len--];
                s.pqdownheap(tree, 1);
                m = s.heap[1]; // m = node of next least frequency

                s.heap[--s.heap_max] = n; // keep the nodes sorted by frequency
                s.heap[--s.heap_max] = m;

                // Create a new node father of n and m
                tree[node * 2] = (short)(tree[n * 2] + tree[m * 2]);
                s.depth[node] = (byte)(System.Math.Max((byte)s.depth[n], (byte)s.depth[m]) + 1);
                tree[n * 2 + 1] = tree[m * 2 + 1] = (short)node;

                // and insert the new node in the heap
                s.heap[1] = node++;
                s.pqdownheap(tree, 1);
            }
            while (s.heap_len >= 2);

            s.heap[--s.heap_max] = s.heap[1];

            // At this point, the fields freq and dad are set. We can now
            // generate the bit lengths.

            gen_bitlen(s);

            // The field len is now set, we can generate the bit codes
            gen_codes(tree, max_code, s.bl_count);
        }

        // Generate the codes for a given tree and bit counts (which need not be
        // optimal).
        // IN assertion: the array bl_count contains the bit length statistics for
        // the given tree and the field len is set for all tree elements.
        // OUT assertion: the field code is set for all tree elements of non
        //     zero code length.
        internal static void gen_codes(short[] tree, int max_code, short[] bl_count)
        {
            short[] next_code = new short[MAX_BITS + 1]; // next code value for each bit length
            short code = 0; // running code value
            int bits; // bit index
            int n; // code index

            // The distribution counts are first used to generate the code values
            // without bit reversal.
            for (bits = 1; bits <= MAX_BITS; bits++)
            {
                next_code[bits] = code = (short)((code + bl_count[bits - 1]) << 1);
            }

            // Check that the bit counts in bl_count are consistent. The last code
            // must be all ones.
            //Assert (code + bl_count[MAX_BITS]-1 == (1<<MAX_BITS)-1,
            //        "inconsistent bit counts");
            //Tracev((stderr,"\ngen_codes: max_code %d ", max_code));

            for (n = 0; n <= max_code; n++)
            {
                int len = tree[n * 2 + 1];
                if (len == 0)
                    continue;
                // Now reverse the bits
                tree[n * 2] = (short)(bi_reverse(next_code[len]++, len));
            }
        }

        // Reverse the first len bits of a code, using straightforward code (a faster
        // method would use a table)
        // IN assertion: 1 <= len <= 15
        internal static int bi_reverse(int code, int len)
        {
            int res = 0;
            do
            {
                res |= code & 1;
                code = SupportClass.URShift(code, 1);
                res <<= 1;
            }
            while (--len > 0);
            return SupportClass.URShift(res, 1);
        }
    }
    sealed public class ZStream
    {

        private const int MAX_WBITS = 15; // 32K LZ77 window		
        private static readonly int DEF_WBITS = MAX_WBITS;

        private const int Z_NO_FLUSH = 0;
        private const int Z_PARTIAL_FLUSH = 1;
        private const int Z_SYNC_FLUSH = 2;
        private const int Z_FULL_FLUSH = 3;
        private const int Z_FINISH = 4;

        private const int MAX_MEM_LEVEL = 9;

        private const int Z_OK = 0;
        private const int Z_STREAM_END = 1;
        private const int Z_NEED_DICT = 2;
        private const int Z_ERRNO = -1;
        private const int Z_STREAM_ERROR = -2;
        private const int Z_DATA_ERROR = -3;
        private const int Z_MEM_ERROR = -4;
        private const int Z_BUF_ERROR = -5;
        private const int Z_VERSION_ERROR = -6;

        public byte[] next_in; // next input byte
        public int next_in_index;
        public int avail_in; // number of bytes available at next_in
        public long total_in; // total nb of input bytes read so far

        public byte[] next_out; // next output byte should be put there
        public int next_out_index;
        public int avail_out; // remaining free space at next_out
        public long total_out; // total nb of bytes output so far

        public System.String msg;

        internal Deflate dstate;
        internal Inflate istate;

        internal int data_type; // best guess about the data type: ascii or binary

        public long adler;
        internal Adler32 _adler = new Adler32();

        public int inflateInit()
        {
            return inflateInit(DEF_WBITS);
        }
        public int inflateInit(int w)
        {
            istate = new Inflate();
            return istate.inflateInit(this, w);
        }

        public int inflate(int f)
        {
            if (istate == null)
                return Z_STREAM_ERROR;
            return istate.inflate(this, f);
        }
        public int inflateEnd()
        {
            if (istate == null)
                return Z_STREAM_ERROR;
            int ret = istate.inflateEnd(this);
            istate = null;
            return ret;
        }
        public int inflateSync()
        {
            if (istate == null)
                return Z_STREAM_ERROR;
            return istate.inflateSync(this);
        }
        public int inflateSetDictionary(byte[] dictionary, int dictLength)
        {
            if (istate == null)
                return Z_STREAM_ERROR;
            return istate.inflateSetDictionary(this, dictionary, dictLength);
        }

        public int deflateInit(int level)
        {
            return deflateInit(level, MAX_WBITS);
        }
        public int deflateInit(int level, int bits)
        {
            dstate = new Deflate();
            return dstate.deflateInit(this, level, bits);
        }
        public int deflate(int flush)
        {
            if (dstate == null)
            {
                return Z_STREAM_ERROR;
            }
            return dstate.deflate(this, flush);
        }
        public int deflateEnd()
        {
            if (dstate == null)
                return Z_STREAM_ERROR;
            int ret = dstate.deflateEnd();
            dstate = null;
            return ret;
        }
        public int deflateParams(int level, int strategy)
        {
            if (dstate == null)
                return Z_STREAM_ERROR;
            return dstate.deflateParams(this, level, strategy);
        }
        public int deflateSetDictionary(byte[] dictionary, int dictLength)
        {
            if (dstate == null)
                return Z_STREAM_ERROR;
            return dstate.deflateSetDictionary(this, dictionary, dictLength);
        }

        // Flush as much pending output as possible. All deflate() output goes
        // through this function so some applications may wish to modify it
        // to avoid allocating a large strm->next_out buffer and copying into it.
        // (See also read_buf()).
        internal void flush_pending()
        {
            int len = dstate.pending;

            if (len > avail_out)
                len = avail_out;
            if (len == 0)
                return;

            if (dstate.pending_buf.Length <= dstate.pending_out || next_out.Length <= next_out_index || dstate.pending_buf.Length < (dstate.pending_out + len) || next_out.Length < (next_out_index + len))
            {
                //System.Console.Out.WriteLine(dstate.pending_buf.Length + ", " + dstate.pending_out + ", " + next_out.Length + ", " + next_out_index + ", " + len);
                //System.Console.Out.WriteLine("avail_out=" + avail_out);
            }

            Array.Copy(dstate.pending_buf, dstate.pending_out, next_out, next_out_index, len);

            next_out_index += len;
            dstate.pending_out += len;
            total_out += len;
            avail_out -= len;
            dstate.pending -= len;
            if (dstate.pending == 0)
            {
                dstate.pending_out = 0;
            }
        }

        // Read a new buffer from the current input stream, update the adler32
        // and total number of bytes read.  All deflate() input goes through
        // this function so some applications may wish to modify it to avoid
        // allocating a large strm->next_in buffer and copying from it.
        // (See also flush_pending()).
        internal int read_buf(byte[] buf, int start, int size)
        {
            int len = avail_in;

            if (len > size)
                len = size;
            if (len == 0)
                return 0;

            avail_in -= len;

            if (dstate.noheader == 0)
            {
                adler = _adler.adler32(adler, next_in, next_in_index, len);
            }
            Array.Copy(next_in, next_in_index, buf, start, len);
            next_in_index += len;
            total_in += len;
            return len;
        }

        public void free()
        {
            next_in = null;
            next_out = null;
            msg = null;
            _adler = null;
        }
    }
    public class ZInputStream : System.IO.BinaryReader
    {
        internal void InitBlock()
        {
            flush = zlibConst.Z_NO_FLUSH;
            buf = new byte[bufsize];
        }
        virtual public int FlushMode
        {
            get
            {
                return (flush);
            }

            set
            {
                this.flush = value;
            }

        }
        /// <summary> Returns the total number of bytes input so far.</summary>
        virtual public long TotalIn
        {
            get
            {
                return z.total_in;
            }

        }
        /// <summary> Returns the total number of bytes output so far.</summary>
        virtual public long TotalOut
        {
            get
            {
                return z.total_out;
            }

        }

        protected ZStream z = new ZStream();
        protected int bufsize = 512;
        protected int flush;
        protected byte[] buf, buf1 = new byte[1];
        protected bool compress;

        internal System.IO.Stream in_Renamed = null;

        public ZInputStream(System.IO.Stream in_Renamed)
            : base(in_Renamed)
        {
            InitBlock();
            this.in_Renamed = in_Renamed;
            z.inflateInit();
            compress = false;
            z.next_in = buf;
            z.next_in_index = 0;
            z.avail_in = 0;
        }

        public ZInputStream(System.IO.Stream in_Renamed, int level)
            : base(in_Renamed)
        {
            InitBlock();
            this.in_Renamed = in_Renamed;
            z.deflateInit(level);
            compress = true;
            z.next_in = buf;
            z.next_in_index = 0;
            z.avail_in = 0;
        }

        /*public int available() throws IOException {
        return inf.finished() ? 0 : 1;
        }*/

        public override int Read()
        {
            if (read(buf1, 0, 1) == -1)
                return (-1);
            return (buf1[0] & 0xFF);
        }

        internal bool nomoreinput = false;

        public int read(byte[] b, int off, int len)
        {
            if (len == 0)
                return (0);
            int err;
            z.next_out = b;
            z.next_out_index = off;
            z.avail_out = len;
            do
            {
                if ((z.avail_in == 0) && (!nomoreinput))
                {
                    // if buffer is empty and more input is avaiable, refill it
                    z.next_in_index = 0;
                    z.avail_in = SupportClass.ReadInput(in_Renamed, buf, 0, bufsize); //(bufsize<z.avail_out ? bufsize : z.avail_out));
                    if (z.avail_in == -1)
                    {
                        z.avail_in = 0;
                        nomoreinput = true;
                    }
                }
                if (compress)
                    err = z.deflate(flush);
                else
                    err = z.inflate(flush);
                if (nomoreinput && (err == zlibConst.Z_BUF_ERROR))
                    return (-1);
                if (err != zlibConst.Z_OK && err != zlibConst.Z_STREAM_END)
                    throw new ZStreamException((compress ? "de" : "in") + "flating: " + z.msg);
                if (nomoreinput && (z.avail_out == len))
                    return (-1);
            }
            while (z.avail_out == len && err == zlibConst.Z_OK);
            //System.err.print("("+(len-z.avail_out)+")");
            return (len - z.avail_out);
        }

        public long skip(long n)
        {
            int len = 512;
            if (n < len)
                len = (int)n;
            byte[] tmp = new byte[len];
            return ((long)SupportClass.ReadInput(BaseStream, tmp, 0, tmp.Length));
        }

        public override void Close()
        {
            in_Renamed.Close();
        }
    }
    public class SupportClass
    {
        /// <summary>
        /// This method returns the literal value received
        /// </summary>
        /// <param name="literal">The literal to return</param>
        /// <returns>The received value</returns>
        public static long Identity(long literal)
        {
            return literal;
        }

        /// <summary>
        /// This method returns the literal value received
        /// </summary>
        /// <param name="literal">The literal to return</param>
        /// <returns>The received value</returns>
        public static ulong Identity(ulong literal)
        {
            return literal;
        }

        /// <summary>
        /// This method returns the literal value received
        /// </summary>
        /// <param name="literal">The literal to return</param>
        /// <returns>The received value</returns>
        public static float Identity(float literal)
        {
            return literal;
        }

        /// <summary>
        /// This method returns the literal value received
        /// </summary>
        /// <param name="literal">The literal to return</param>
        /// <returns>The received value</returns>
        public static double Identity(double literal)
        {
            return literal;
        }

        /*******************************/
        /// <summary>
        /// Performs an unsigned bitwise right shift with the specified number
        /// </summary>
        /// <param name="number">Number to operate on</param>
        /// <param name="bits">Ammount of bits to shift</param>
        /// <returns>The resulting number from the shift operation</returns>
        public static int URShift(int number, int bits)
        {
            if (number >= 0)
                return number >> bits;
            else
                return (number >> bits) + (2 << ~bits);
        }

        /// <summary>
        /// Performs an unsigned bitwise right shift with the specified number
        /// </summary>
        /// <param name="number">Number to operate on</param>
        /// <param name="bits">Ammount of bits to shift</param>
        /// <returns>The resulting number from the shift operation</returns>
        public static int URShift(int number, long bits)
        {
            return URShift(number, (int)bits);
        }

        /// <summary>
        /// Performs an unsigned bitwise right shift with the specified number
        /// </summary>
        /// <param name="number">Number to operate on</param>
        /// <param name="bits">Ammount of bits to shift</param>
        /// <returns>The resulting number from the shift operation</returns>
        public static long URShift(long number, int bits)
        {
            if (number >= 0)
                return number >> bits;
            else
                return (number >> bits) + (2L << ~bits);
        }

        /// <summary>
        /// Performs an unsigned bitwise right shift with the specified number
        /// </summary>
        /// <param name="number">Number to operate on</param>
        /// <param name="bits">Ammount of bits to shift</param>
        /// <returns>The resulting number from the shift operation</returns>
        public static long URShift(long number, long bits)
        {
            return URShift(number, (int)bits);
        }

        /*******************************/
        /// <summary>Reads a number of characters from the current source Stream and writes the data to the target array at the specified index.</summary>
        /// <param name="sourceStream">The source Stream to read from.</param>
        /// <param name="target">Contains the array of characteres read from the source Stream.</param>
        /// <param name="start">The starting index of the target array.</param>
        /// <param name="count">The maximum number of characters to read from the source Stream.</param>
        /// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source Stream. Returns -1 if the end of the stream is reached.</returns>
        public static System.Int32 ReadInput(System.IO.Stream sourceStream, byte[] target, int start, int count)
        {
            // Returns 0 bytes if not enough space in target
            if (target.Length == 0)
                return 0;

            byte[] receiver = new byte[target.Length];
            int bytesRead = sourceStream.Read(receiver, start, count);

            // Returns -1 if EOF
            if (bytesRead == 0)
                return -1;

            for (int i = start; i < start + bytesRead; i++)
                target[i] = (byte)receiver[i];

            return bytesRead;
        }

        /// <summary>Reads a number of characters from the current source TextReader and writes the data to the target array at the specified index.</summary>
        /// <param name="sourceTextReader">The source TextReader to read from</param>
        /// <param name="target">Contains the array of characteres read from the source TextReader.</param>
        /// <param name="start">The starting index of the target array.</param>
        /// <param name="count">The maximum number of characters to read from the source TextReader.</param>
        /// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source TextReader. Returns -1 if the end of the stream is reached.</returns>
        public static System.Int32 ReadInput(System.IO.TextReader sourceTextReader, byte[] target, int start, int count)
        {
            // Returns 0 bytes if not enough space in target
            if (target.Length == 0) return 0;

            char[] charArray = new char[target.Length];
            int bytesRead = sourceTextReader.Read(charArray, start, count);

            // Returns -1 if EOF
            if (bytesRead == 0) return -1;

            for (int index = start; index < start + bytesRead; index++)
                target[index] = (byte)charArray[index];

            return bytesRead;
        }

        /// <summary>
        /// Converts a string to an array of bytes
        /// </summary>
        /// <param name="sourceString">The string to be converted</param>
        /// <returns>The new array of bytes</returns>
        public static byte[] ToByteArray(System.String sourceString)
        {
            return System.Text.UTF8Encoding.UTF8.GetBytes(sourceString);
        }

        /// <summary>
        /// Converts an array of bytes to an array of chars
        /// </summary>
        /// <param name="byteArray">The array of bytes to convert</param>
        /// <returns>The new array of chars</returns>
        public static char[] ToCharArray(byte[] byteArray)
        {
            return System.Text.UTF8Encoding.UTF8.GetChars(byteArray);
        }


    }
    sealed class StaticTree
    {
        private const int MAX_BITS = 15;

        private const int BL_CODES = 19;
        private const int D_CODES = 30;
        private const int LITERALS = 256;
        private const int LENGTH_CODES = 29;
        private static readonly int L_CODES = (LITERALS + 1 + LENGTH_CODES);

        // Bit length codes must not exceed MAX_BL_BITS bits
        internal const int MAX_BL_BITS = 7;

        internal static readonly short[] static_ltree = new short[]{12, 8, 140, 8, 76, 8, 204, 8, 44, 8, 172, 8, 108, 8, 236, 8, 28, 8, 156, 8, 92, 8, 220, 8, 60, 8, 188, 8, 124, 8, 252, 8, 2, 8, 130, 8, 66, 8, 194, 8, 34, 8, 162, 8, 98, 8, 226, 8, 18, 8, 146, 8, 82, 8, 210, 8, 50, 8, 178, 8, 114, 8, 242, 8, 10, 8, 138, 8, 74, 8, 202, 8, 42, 8, 170, 8, 106, 8, 234, 8, 26, 8, 154, 8, 90, 8, 218, 8, 58, 8, 186, 8, 122, 8, 250, 8, 6, 8, 134, 8, 70, 8, 198, 8, 38, 8, 166, 8, 102, 8, 230, 8, 22, 8, 150, 8, 86, 8, 214, 8, 54, 8, 182, 8, 118, 8, 246, 8, 14, 8, 142, 8, 78, 8, 206, 8, 46, 8, 174, 8, 110, 8, 238, 8, 30, 8, 158, 8, 94, 8, 222, 8, 62, 8, 190, 8, 126, 8, 254, 8, 1, 8, 129, 8, 65, 8, 193, 8, 33, 8, 161, 8, 97, 8, 225, 8, 17, 8, 145, 8, 81, 8, 209, 8, 49, 8, 177, 8, 113, 8, 241, 8, 9, 8, 137, 8, 73, 8, 201, 8, 41, 8, 169, 8, 105, 8, 233, 8, 25, 8, 153, 8, 89, 8, 217, 8, 57, 8, 185, 8, 121, 8, 249, 8, 5, 8, 133, 8, 69, 8, 197, 8, 37, 8, 165, 8, 101, 8, 229, 8, 21, 8, 149, 8, 85, 8, 213, 8, 53, 8, 181, 8, 117, 8, 245, 8, 13, 8, 141, 8, 77, 8, 205, 8, 45, 8, 173, 8, 109, 8, 237, 8, 29, 8, 157, 8, 93, 8, 221, 8, 61, 8, 189, 8, 125, 8, 253, 8, 19, 9, 275, 9, 147, 9, 403, 9, 83, 9, 339, 9, 211, 9, 467, 9, 51, 9, 307, 9, 179, 9, 435, 9, 115, 9, 371, 9, 243, 9, 499, 9, 11, 9, 267, 9, 139, 9, 395, 9, 75, 9, 331, 9, 203, 9, 459, 9, 43, 9, 299, 9, 171, 9, 427, 9, 107, 9, 363, 9, 235, 9, 491, 9, 27, 9, 283, 9, 155, 9, 411, 9, 91, 9, 347, 9, 219, 9, 475, 9, 59, 9, 315, 9, 187, 9, 443, 9, 123, 9, 379, 9, 251, 9, 507, 9, 7, 9, 263, 9, 135, 9, 391, 9, 71, 9, 327, 9, 199, 9, 455, 9, 39, 9, 295, 9, 167, 9, 423, 9, 103, 9, 359, 9, 231, 9, 487, 9, 23, 9, 279, 9, 151, 9, 407, 9, 87, 9, 343, 9, 215, 9, 471, 9, 55, 9, 311, 9, 183, 9, 439, 9, 119, 9, 375, 9, 247, 9, 503, 9, 15, 9, 271, 9, 143, 9, 399, 9, 79, 9, 335, 9, 207, 9, 463, 9, 47, 9, 303, 9, 175, 9, 431, 9, 111, 9, 367, 9, 239, 9, 495, 9, 31, 9, 287, 9, 159, 9, 415, 9, 95, 9, 351, 9, 223, 9, 479, 9, 63, 9, 319, 9, 191, 9, 447, 9, 127, 9, 383, 9, 255, 9, 511, 9, 0, 7, 64, 7
			, 32, 7, 96, 7, 16, 7, 80, 7, 48, 7, 112, 7, 8, 7, 72, 7, 40, 7, 104, 7, 24, 7, 88, 7, 56, 7, 120, 7, 4, 7, 68, 7, 36, 7, 100, 7, 20, 7, 84, 7, 52, 7, 116, 7, 3, 8, 131, 8, 67, 8, 195, 8, 35, 8, 163, 8, 99, 8, 227, 8};

        internal static readonly short[] static_dtree = new short[] { 0, 5, 16, 5, 8, 5, 24, 5, 4, 5, 20, 5, 12, 5, 28, 5, 2, 5, 18, 5, 10, 5, 26, 5, 6, 5, 22, 5, 14, 5, 30, 5, 1, 5, 17, 5, 9, 5, 25, 5, 5, 5, 21, 5, 13, 5, 29, 5, 3, 5, 19, 5, 11, 5, 27, 5, 7, 5, 23, 5 };

        internal static StaticTree static_l_desc;

        internal static StaticTree static_d_desc;

        internal static StaticTree static_bl_desc;

        internal short[] static_tree; // static tree or null
        internal int[] extra_bits; // extra bits for each code or null
        internal int extra_base; // base index for extra_bits
        internal int elems; // max number of elements in the tree
        internal int max_length; // max bit length for the codes

        internal StaticTree(short[] static_tree, int[] extra_bits, int extra_base, int elems, int max_length)
        {
            this.static_tree = static_tree;
            this.extra_bits = extra_bits;
            this.extra_base = extra_base;
            this.elems = elems;
            this.max_length = max_length;
        }
        static StaticTree()
        {
            static_l_desc = new StaticTree(static_ltree, Tree.extra_lbits, LITERALS + 1, L_CODES, MAX_BITS);
            static_d_desc = new StaticTree(static_dtree, Tree.extra_dbits, 0, D_CODES, MAX_BITS);
            static_bl_desc = new StaticTree(null, Tree.extra_blbits, 0, BL_CODES, MAX_BL_BITS);
        }
    }
    sealed class InfTree
    {

        private const int MANY = 1440;

        private const int Z_OK = 0;
        private const int Z_STREAM_END = 1;
        private const int Z_NEED_DICT = 2;
        private const int Z_ERRNO = -1;
        private const int Z_STREAM_ERROR = -2;
        private const int Z_DATA_ERROR = -3;
        private const int Z_MEM_ERROR = -4;
        private const int Z_BUF_ERROR = -5;
        private const int Z_VERSION_ERROR = -6;

        internal const int fixed_bl = 9;
        internal const int fixed_bd = 5;


        internal static readonly int[] fixed_tl = new int[]{96, 7, 256, 0, 8, 80, 0, 8, 16, 84, 8, 115, 82, 7, 31, 0, 8, 112, 0, 8, 48, 0, 9, 192, 80, 7, 10, 0, 8, 96, 0, 8, 32, 0, 9, 160, 0, 8, 0, 0, 8, 128, 0, 8, 64, 0, 9, 224, 80, 7, 6, 0, 8, 88, 0, 8, 24, 0, 9, 144, 83, 7, 59, 0, 8, 120, 0, 8, 56, 0, 9, 208, 81, 7, 17, 0, 8, 104, 0, 8, 40, 0, 9, 176, 0, 8, 8, 0, 8, 136, 0, 8, 72, 0, 9, 240, 80, 7, 4, 0, 8, 84, 0, 8, 20, 85, 8, 227, 83, 7, 43, 0, 8, 116, 0, 8, 52, 0, 9, 200, 81, 7, 13, 0, 8, 100, 0, 8, 36, 0, 9, 168, 0, 8, 4, 0, 8, 132, 0, 8, 68, 0, 9, 232, 80, 7, 8, 0, 8, 92, 0, 8, 28, 0, 9, 152, 84, 7, 83, 0, 8, 124, 0, 8, 60, 0, 9, 216, 82, 7, 23, 0, 8, 108, 0, 8, 44, 0, 9, 184, 0, 8, 12, 0, 8, 140, 0, 8, 76, 0, 9, 248, 80, 7, 3, 0, 8, 82, 0, 8, 18, 85, 8, 163, 83, 7, 35, 0, 8, 114, 0, 8, 50, 0, 9, 196, 81, 7, 11, 0, 8, 98, 0, 8, 34, 0, 9, 164, 0, 8, 2, 0, 8, 130, 0, 8, 66, 0, 9, 228, 80, 7, 7, 0, 8, 90, 0, 8, 26, 0, 9, 148, 84, 7, 67, 0, 8, 122, 0, 8, 58, 0, 9, 212, 82, 7, 19, 0, 8, 106, 0, 8, 42, 0, 9, 180, 0, 8, 10, 0, 8, 138, 0, 8, 74, 0, 9, 244, 80, 7, 5, 0, 8, 86, 0, 8, 22, 192, 8, 0, 83, 7, 51, 0, 8, 118, 0, 8, 54, 0, 9, 204, 81, 7, 15, 0, 8, 102, 0, 8, 38, 0, 9, 172, 0, 8, 6, 0, 8, 134, 0, 8, 70, 0, 9, 236, 80, 7, 9, 0, 8, 94, 0, 8, 30, 0, 9, 156, 84, 7, 99, 0, 8, 126, 0, 8, 62, 0, 9, 220, 82, 7, 27, 0, 8, 110, 0, 8, 46, 0, 9, 188, 0, 8, 14, 0, 8, 142, 0, 8, 78, 0, 9, 252, 96, 7, 256, 0, 8, 81, 0, 8, 17, 85, 8, 131, 82, 7, 31, 0, 8, 113, 0, 8, 49, 0, 9, 194, 80, 7, 10, 0, 8, 97, 0, 8, 33, 0, 9, 162, 0, 8, 1, 0, 8, 129, 0, 8, 65, 0, 9, 226, 80, 7, 6, 0, 8, 89, 0, 8, 25, 0, 9, 146, 83, 7, 59, 0, 8, 121, 0, 8, 57, 0, 9, 210, 81, 7, 17, 0, 8, 105, 0, 8, 41, 0, 9, 178, 0, 8, 9, 0, 8, 137, 0, 8, 73, 0, 9, 242, 80, 7, 4, 0, 8, 85, 0, 8, 21, 80, 8, 258, 83, 7, 43, 0, 8, 117, 0, 8, 53, 0, 9, 202, 81, 7, 13, 0, 8, 101, 0, 8, 37, 0, 9, 170, 0, 8, 5, 0, 8, 133, 0, 8, 69, 0, 9, 234, 80, 7, 8, 0, 8, 93, 0, 8, 29, 0, 9, 154, 84, 7, 83, 0, 8, 125, 0, 8, 61, 0, 9, 218, 82, 7, 23, 0, 8, 109, 0, 8, 45, 0, 9, 186, 
			0, 8, 13, 0, 8, 141, 0, 8, 77, 0, 9, 250, 80, 7, 3, 0, 8, 83, 0, 8, 19, 85, 8, 195, 83, 7, 35, 0, 8, 115, 0, 8, 51, 0, 9, 198, 81, 7, 11, 0, 8, 99, 0, 8, 35, 0, 9, 166, 0, 8, 3, 0, 8, 131, 0, 8, 67, 0, 9, 230, 80, 7, 7, 0, 8, 91, 0, 8, 27, 0, 9, 150, 84, 7, 67, 0, 8, 123, 0, 8, 59, 0, 9, 214, 82, 7, 19, 0, 8, 107, 0, 8, 43, 0, 9, 182, 0, 8, 11, 0, 8, 139, 0, 8, 75, 0, 9, 246, 80, 7, 5, 0, 8, 87, 0, 8, 23, 192, 8, 0, 83, 7, 51, 0, 8, 119, 0, 8, 55, 0, 9, 206, 81, 7, 15, 0, 8, 103, 0, 8, 39, 0, 9, 174, 0, 8, 7, 0, 8, 135, 0, 8, 71, 0, 9, 238, 80, 7, 9, 0, 8, 95, 0, 8, 31, 0, 9, 158, 84, 7, 99, 0, 8, 127, 0, 8, 63, 0, 9, 222, 82, 7, 27, 0, 8, 111, 0, 8, 47, 0, 9, 190, 0, 8, 15, 0, 8, 143, 0, 8, 79, 0, 9, 254, 96, 7, 256, 0, 8, 80, 0, 8, 16, 84, 8, 115, 82, 7, 31, 0, 8, 112, 0, 8, 48, 0, 9, 193, 80, 7, 10, 0, 8, 96, 0, 8, 32, 0, 9, 161, 0, 8, 0, 0, 8, 128, 0, 8, 64, 0, 9, 225, 80, 7, 6, 0, 8, 88, 0, 8, 24, 0, 9, 145, 83, 7, 59, 0, 8, 120, 0, 8, 56, 0, 9, 209, 81, 7, 17, 0, 8, 104, 0, 8, 40, 0, 9, 177, 0, 8, 8, 0, 8, 136, 0, 8, 72, 0, 9, 241, 80, 7, 4, 0, 8, 84, 0, 8, 20, 85, 8, 227, 83, 7, 43, 0, 8, 116, 0, 8, 52, 0, 9, 201, 81, 7, 13, 0, 8, 100, 0, 8, 36, 0, 9, 169, 0, 8, 4, 0, 8, 132, 0, 8, 68, 0, 9, 233, 80, 7, 8, 0, 8, 92, 0, 8, 28, 0, 9, 153, 84, 7, 83, 0, 8, 124, 0, 8, 60, 0, 9, 217, 82, 7, 23, 0, 8, 108, 0, 8, 44, 0, 9, 185, 0, 8, 12, 0, 8, 140, 0, 8, 76, 0, 9, 249, 80, 7, 3, 0, 8, 82, 0, 8, 18, 85, 8, 163, 83, 7, 35, 0, 8, 114, 0, 8, 50, 0, 9, 197, 81, 7, 11, 0, 8, 98, 0, 8, 34, 0, 9, 165, 0, 8, 2, 0, 8, 130, 0, 8, 66, 0, 9, 229, 80, 7, 7, 0, 8, 90, 0, 8, 26, 0, 9, 149, 84, 7, 67, 0, 8, 122, 0, 8, 58, 0, 9, 213, 82, 7, 19, 0, 8, 106, 0, 8, 42, 0, 9, 181, 0, 8, 10, 0, 8, 138, 0, 8, 74, 0, 9, 245, 80, 7, 5, 0, 8, 86, 0, 8, 22, 192, 8, 0, 83, 7, 51, 0, 8, 118, 0, 8, 54, 0, 9, 205, 81, 7, 15, 0, 8, 102, 0, 8, 38, 0, 9, 173, 0, 8, 6, 0, 8, 134, 0, 8, 70, 0, 9, 237, 80, 7, 9, 0, 8, 94, 0, 8, 30, 0, 9, 157, 84, 7, 99, 0, 8, 126, 0, 8, 62, 0, 9, 221, 82, 7, 27, 0, 8, 110, 0, 8, 46, 0, 9, 189, 0, 8, 
			14, 0, 8, 142, 0, 8, 78, 0, 9, 253, 96, 7, 256, 0, 8, 81, 0, 8, 17, 85, 8, 131, 82, 7, 31, 0, 8, 113, 0, 8, 49, 0, 9, 195, 80, 7, 10, 0, 8, 97, 0, 8, 33, 0, 9, 163, 0, 8, 1, 0, 8, 129, 0, 8, 65, 0, 9, 227, 80, 7, 6, 0, 8, 89, 0, 8, 25, 0, 9, 147, 83, 7, 59, 0, 8, 121, 0, 8, 57, 0, 9, 211, 81, 7, 17, 0, 8, 105, 0, 8, 41, 0, 9, 179, 0, 8, 9, 0, 8, 137, 0, 8, 73, 0, 9, 243, 80, 7, 4, 0, 8, 85, 0, 8, 21, 80, 8, 258, 83, 7, 43, 0, 8, 117, 0, 8, 53, 0, 9, 203, 81, 7, 13, 0, 8, 101, 0, 8, 37, 0, 9, 171, 0, 8, 5, 0, 8, 133, 0, 8, 69, 0, 9, 235, 80, 7, 8, 0, 8, 93, 0, 8, 29, 0, 9, 155, 84, 7, 83, 0, 8, 125, 0, 8, 61, 0, 9, 219, 82, 7, 23, 0, 8, 109, 0, 8, 45, 0, 9, 187, 0, 8, 13, 0, 8, 141, 0, 8, 77, 0, 9, 251, 80, 7, 3, 0, 8, 83, 0, 8, 19, 85, 8, 195, 83, 7, 35, 0, 8, 115, 0, 8, 51, 0, 9, 199, 81, 7, 11, 0, 8, 99, 0, 8, 35, 0, 9, 167, 0, 8, 3, 0, 8, 131, 0, 8, 67, 0, 9, 231, 80, 7, 7, 0, 8, 91, 0, 8, 27, 0, 9, 151, 84, 7, 67, 0, 8, 123, 0, 8, 59, 0, 9, 215, 82, 7, 19, 0, 8, 107, 0, 8, 43, 0, 9, 183, 0, 8, 11, 0, 8, 139, 0, 8, 75, 0, 9, 247, 80, 7, 5, 0, 8, 87, 0, 8, 23, 192, 8, 0, 83, 7, 51, 0, 8, 119, 0, 8, 55, 0, 9, 207, 81, 7, 15, 0, 8, 103, 0, 8, 39, 0, 9, 175, 0, 8, 7, 0, 8, 135, 0, 8, 71, 0, 9, 239, 80, 7, 9, 0, 8, 95, 0, 8, 31, 0, 9, 159, 84, 7, 99, 0, 8, 127, 0, 8, 63, 0, 9, 223, 82, 7, 27, 0, 8, 111, 0, 8, 47, 0, 9, 191, 0, 8, 15, 0, 8, 143, 0, 8, 79, 0, 9, 255};

        internal static readonly int[] fixed_td = new int[] { 80, 5, 1, 87, 5, 257, 83, 5, 17, 91, 5, 4097, 81, 5, 5, 89, 5, 1025, 85, 5, 65, 93, 5, 16385, 80, 5, 3, 88, 5, 513, 84, 5, 33, 92, 5, 8193, 82, 5, 9, 90, 5, 2049, 86, 5, 129, 192, 5, 24577, 80, 5, 2, 87, 5, 385, 83, 5, 25, 91, 5, 6145, 81, 5, 7, 89, 5, 1537, 85, 5, 97, 93, 5, 24577, 80, 5, 4, 88, 5, 769, 84, 5, 49, 92, 5, 12289, 82, 5, 13, 90, 5, 3073, 86, 5, 193, 192, 5, 24577 };

        // Tables for deflate from PKZIP's appnote.txt.		
        internal static readonly int[] cplens = new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15, 17, 19, 23, 27, 31, 35, 43, 51, 59, 67, 83, 99, 115, 131, 163, 195, 227, 258, 0, 0 };

        internal static readonly int[] cplext = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0, 112, 112 };

        internal static readonly int[] cpdist = new int[] { 1, 2, 3, 4, 5, 7, 9, 13, 17, 25, 33, 49, 65, 97, 129, 193, 257, 385, 513, 769, 1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577 };

        internal static readonly int[] cpdext = new int[] { 0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13 };

        // If BMAX needs to be larger than 16, then h and x[] should be uLong.
        internal const int BMAX = 15; // maximum bit length of any code

        internal static int huft_build(int[] b, int bindex, int n, int s, int[] d, int[] e, int[] t, int[] m, int[] hp, int[] hn, int[] v)
        {
            // Given a list of code lengths and a maximum table size, make a set of
            // tables to decode that set of codes.  Return Z_OK on success, Z_BUF_ERROR
            // if the given code set is incomplete (the tables are still built in this
            // case), Z_DATA_ERROR if the input is invalid (an over-subscribed set of
            // lengths), or Z_MEM_ERROR if not enough memory.

            int a; // counter for codes of length k
            int[] c = new int[BMAX + 1]; // bit length count table
            int f; // i repeats in table every f entries
            int g; // maximum code length
            int h; // table level
            int i; // counter, current code
            int j; // counter
            int k; // number of bits in current code
            int l; // bits per table (returned in m)
            int mask; // (1 << w) - 1, to avoid cc -O bug on HP
            int p; // pointer into c[], b[], or v[]
            int q; // points to current table
            int[] r = new int[3]; // table entry for structure assignment
            int[] u = new int[BMAX]; // table stack
            int w; // bits before this table == (l * h)
            int[] x = new int[BMAX + 1]; // bit offsets, then code stack
            int xp; // pointer into x
            int y; // number of dummy codes added
            int z; // number of entries in current table

            // Generate counts for each bit length

            p = 0; i = n;
            do
            {
                c[b[bindex + p]]++; p++; i--; // assume all entries <= BMAX
            }
            while (i != 0);

            if (c[0] == n)
            {
                // null input--all zero length codes
                t[0] = -1;
                m[0] = 0;
                return Z_OK;
            }

            // Find minimum and maximum length, bound *m by those
            l = m[0];
            for (j = 1; j <= BMAX; j++)
                if (c[j] != 0)
                    break;
            k = j; // minimum code length
            if (l < j)
            {
                l = j;
            }
            for (i = BMAX; i != 0; i--)
            {
                if (c[i] != 0)
                    break;
            }
            g = i; // maximum code length
            if (l > i)
            {
                l = i;
            }
            m[0] = l;

            // Adjust last length count to fill out codes, if needed
            for (y = 1 << j; j < i; j++, y <<= 1)
            {
                if ((y -= c[j]) < 0)
                {
                    return Z_DATA_ERROR;
                }
            }
            if ((y -= c[i]) < 0)
            {
                return Z_DATA_ERROR;
            }
            c[i] += y;

            // Generate starting offsets into the value table for each length
            x[1] = j = 0;
            p = 1; xp = 2;
            while (--i != 0)
            {
                // note that i == g from above
                x[xp] = (j += c[p]);
                xp++;
                p++;
            }

            // Make a table of values in order of bit lengths
            i = 0; p = 0;
            do
            {
                if ((j = b[bindex + p]) != 0)
                {
                    v[x[j]++] = i;
                }
                p++;
            }
            while (++i < n);
            n = x[g]; // set n to length of v

            // Generate the Huffman codes and for each, make the table entries
            x[0] = i = 0; // first Huffman code is zero
            p = 0; // grab values in bit order
            h = -1; // no tables yet--level -1
            w = -l; // bits decoded == (l * h)
            u[0] = 0; // just to keep compilers happy
            q = 0; // ditto
            z = 0; // ditto

            // go through the bit lengths (k already is bits in shortest code)
            for (; k <= g; k++)
            {
                a = c[k];
                while (a-- != 0)
                {
                    // here i is the Huffman code of length k bits for value *p
                    // make tables up to required level
                    while (k > w + l)
                    {
                        h++;
                        w += l; // previous table always l bits
                        // compute minimum size table less than or equal to l bits
                        z = g - w;
                        z = (z > l) ? l : z; // table size upper limit
                        if ((f = 1 << (j = k - w)) > a + 1)
                        {
                            // try a k-w bit table
                            // too few codes for k-w bit table
                            f -= (a + 1); // deduct codes from patterns left
                            xp = k;
                            if (j < z)
                            {
                                while (++j < z)
                                {
                                    // try smaller tables up to z bits
                                    if ((f <<= 1) <= c[++xp])
                                        break; // enough codes to use up j bits
                                    f -= c[xp]; // else deduct codes from patterns
                                }
                            }
                        }
                        z = 1 << j; // table entries for j-bit table

                        // allocate new table
                        if (hn[0] + z > MANY)
                            // (note: doesn't matter for fixed)
                            return Z_DATA_ERROR; // overflow of MANY
                        u[h] = q = hn[0]; // DEBUG
                        hn[0] += z;

                        // connect to last table, if there is one
                        if (h != 0)
                        {
                            x[h] = i; // save pattern for backing up
                            r[0] = (byte)j; // bits in this table
                            r[1] = (byte)l; // bits to dump before this table
                            j = SupportClass.URShift(i, (w - l));
                            r[2] = (int)(q - u[h - 1] - j); // offset to this table
                            Array.Copy(r, 0, hp, (u[h - 1] + j) * 3, 3); // connect to last table
                        }
                        else
                        {
                            t[0] = q; // first table is returned result
                        }
                    }

                    // set up table entry in r
                    r[1] = (byte)(k - w);
                    if (p >= n)
                    {
                        r[0] = 128 + 64; // out of values--invalid code
                    }
                    else if (v[p] < s)
                    {
                        r[0] = (byte)(v[p] < 256 ? 0 : 32 + 64); // 256 is end-of-block
                        r[2] = v[p++]; // simple code is just the value
                    }
                    else
                    {
                        r[0] = (byte)(e[v[p] - s] + 16 + 64); // non-simple--look up in lists
                        r[2] = d[v[p++] - s];
                    }

                    // fill code-like entries with r
                    f = 1 << (k - w);
                    for (j = SupportClass.URShift(i, w); j < z; j += f)
                    {
                        Array.Copy(r, 0, hp, (q + j) * 3, 3);
                    }

                    // backwards increment the k-bit code i
                    for (j = 1 << (k - 1); (i & j) != 0; j = SupportClass.URShift(j, 1))
                    {
                        i ^= j;
                    }
                    i ^= j;

                    // backup over finished tables
                    mask = (1 << w) - 1; // needed on HP, cc -O bug
                    while ((i & mask) != x[h])
                    {
                        h--; // don't need to update q
                        w -= l;
                        mask = (1 << w) - 1;
                    }
                }
            }
            // Return Z_BUF_ERROR if we were given an incomplete table
            return y != 0 && g != 1 ? Z_BUF_ERROR : Z_OK;
        }

        internal static int inflate_trees_bits(int[] c, int[] bb, int[] tb, int[] hp, ZStream z)
        {
            int r;
            int[] hn = new int[1]; // hufts used in space
            int[] v = new int[19]; // work area for huft_build 

            r = huft_build(c, 0, 19, 19, null, null, tb, bb, hp, hn, v);

            if (r == Z_DATA_ERROR)
            {
                z.msg = "oversubscribed dynamic bit lengths tree";
            }
            else if (r == Z_BUF_ERROR || bb[0] == 0)
            {
                z.msg = "incomplete dynamic bit lengths tree";
                r = Z_DATA_ERROR;
            }
            return r;
        }

        internal static int inflate_trees_dynamic(int nl, int nd, int[] c, int[] bl, int[] bd, int[] tl, int[] td, int[] hp, ZStream z)
        {
            int r;
            int[] hn = new int[1]; // hufts used in space
            int[] v = new int[288]; // work area for huft_build

            // build literal/length tree
            r = huft_build(c, 0, nl, 257, cplens, cplext, tl, bl, hp, hn, v);
            if (r != Z_OK || bl[0] == 0)
            {
                if (r == Z_DATA_ERROR)
                {
                    z.msg = "oversubscribed literal/length tree";
                }
                else if (r != Z_MEM_ERROR)
                {
                    z.msg = "incomplete literal/length tree";
                    r = Z_DATA_ERROR;
                }
                return r;
            }

            // build distance tree
            r = huft_build(c, nl, nd, 0, cpdist, cpdext, td, bd, hp, hn, v);

            if (r != Z_OK || (bd[0] == 0 && nl > 257))
            {
                if (r == Z_DATA_ERROR)
                {
                    z.msg = "oversubscribed distance tree";
                }
                else if (r == Z_BUF_ERROR)
                {
                    z.msg = "incomplete distance tree";
                    r = Z_DATA_ERROR;
                }
                else if (r != Z_MEM_ERROR)
                {
                    z.msg = "empty distance tree with lengths";
                    r = Z_DATA_ERROR;
                }
                return r;
            }

            return Z_OK;
        }

        internal static int inflate_trees_fixed(int[] bl, int[] bd, int[][] tl, int[][] td, ZStream z)
        {
            bl[0] = fixed_bl;
            bd[0] = fixed_bd;
            tl[0] = fixed_tl;
            td[0] = fixed_td;
            return Z_OK;
        }
    }
    sealed class InfBlocks
    {
        private const int MANY = 1440;

        // And'ing with mask[n] masks the lower n bits		
        private static readonly int[] inflate_mask = new int[] { 0x00000000, 0x00000001, 0x00000003, 0x00000007, 0x0000000f, 0x0000001f, 0x0000003f, 0x0000007f, 0x000000ff, 0x000001ff, 0x000003ff, 0x000007ff, 0x00000fff, 0x00001fff, 0x00003fff, 0x00007fff, 0x0000ffff };

        // Table for deflate from PKZIP's appnote.txt.		
        internal static readonly int[] border = new int[] { 16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15 };

        private const int Z_OK = 0;
        private const int Z_STREAM_END = 1;
        private const int Z_NEED_DICT = 2;
        private const int Z_ERRNO = -1;
        private const int Z_STREAM_ERROR = -2;
        private const int Z_DATA_ERROR = -3;
        private const int Z_MEM_ERROR = -4;
        private const int Z_BUF_ERROR = -5;
        private const int Z_VERSION_ERROR = -6;

        private const int TYPE = 0; // get type bits (3, including end bit)
        private const int LENS = 1; // get lengths for stored
        private const int STORED = 2; // processing stored block
        private const int TABLE = 3; // get table lengths
        private const int BTREE = 4; // get bit lengths tree for a dynamic block
        private const int DTREE = 5; // get length, distance trees for a dynamic block
        private const int CODES = 6; // processing fixed or dynamic block
        private const int DRY = 7; // output remaining window bytes
        private const int DONE = 8; // finished last block, done
        private const int BAD = 9; // ot a data error--stuck here

        internal int mode; // current inflate_block mode 

        internal int left; // if STORED, bytes left to copy 

        internal int table; // table lengths (14 bits) 
        internal int index; // index into blens (or border) 
        internal int[] blens; // bit lengths of codes 
        internal int[] bb = new int[1]; // bit length tree depth 
        internal int[] tb = new int[1]; // bit length decoding tree 

        internal InfCodes codes; // if CODES, current state 

        internal int last; // true if this block is the last block 

        // mode independent information 
        internal int bitk; // bits in bit buffer 
        internal int bitb; // bit buffer 
        internal int[] hufts; // single malloc for tree space 
        internal byte[] window; // sliding window 
        internal int end; // one byte after sliding window 
        internal int read; // window read pointer 
        internal int write; // window write pointer 
        internal System.Object checkfn; // check function 
        internal long check; // check on output 

        internal InfBlocks(ZStream z, System.Object checkfn, int w)
        {
            hufts = new int[MANY * 3];
            window = new byte[w];
            end = w;
            this.checkfn = checkfn;
            mode = TYPE;
            reset(z, null);
        }

        internal void reset(ZStream z, long[] c)
        {
            if (c != null)
                c[0] = check;
            if (mode == BTREE || mode == DTREE)
            {
                blens = null;
            }
            if (mode == CODES)
            {
                codes.free(z);
            }
            mode = TYPE;
            bitk = 0;
            bitb = 0;
            read = write = 0;

            if (checkfn != null)
                z.adler = check = z._adler.adler32(0L, null, 0, 0);
        }

        internal int proc(ZStream z, int r)
        {
            int t; // temporary storage
            int b; // bit buffer
            int k; // bits in bit buffer
            int p; // input data pointer
            int n; // bytes available there
            int q; // output window write pointer
            int m; // bytes to end of window or read pointer

            // copy input/output information to locals (UPDATE macro restores)
            {
                p = z.next_in_index; n = z.avail_in; b = bitb; k = bitk;
            }
            {
                q = write; m = (int)(q < read ? read - q - 1 : end - q);
            }

            // process input based on current state
            while (true)
            {
                switch (mode)
                {

                    case TYPE:

                        while (k < (3))
                        {
                            if (n != 0)
                            {
                                r = Z_OK;
                            }
                            else
                            {
                                bitb = b; bitk = k;
                                z.avail_in = n;
                                z.total_in += p - z.next_in_index; z.next_in_index = p;
                                write = q;
                                return inflate_flush(z, r);
                            }
                            ;
                            n--;
                            b |= (z.next_in[p++] & 0xff) << k;
                            k += 8;
                        }
                        t = (int)(b & 7);
                        last = t & 1;

                        switch (SupportClass.URShift(t, 1))
                        {

                            case 0:  // stored 
                                {
                                    b = SupportClass.URShift(b, (3)); k -= (3);
                                }
                                t = k & 7; // go to byte boundary
                                {
                                    b = SupportClass.URShift(b, (t)); k -= (t);
                                }
                                mode = LENS; // get length of stored block
                                break;

                            case 1:  // fixed
                                {
                                    int[] bl = new int[1];
                                    int[] bd = new int[1];
                                    int[][] tl = new int[1][];
                                    int[][] td = new int[1][];

                                    InfTree.inflate_trees_fixed(bl, bd, tl, td, z);
                                    codes = new InfCodes(bl[0], bd[0], tl[0], td[0], z);
                                }
                                {
                                    b = SupportClass.URShift(b, (3)); k -= (3);
                                }

                                mode = CODES;
                                break;

                            case 2:  // dynamic
                                {
                                    b = SupportClass.URShift(b, (3)); k -= (3);
                                }

                                mode = TABLE;
                                break;

                            case 3:  // illegal
                                {
                                    b = SupportClass.URShift(b, (3)); k -= (3);
                                }
                                mode = BAD;
                                z.msg = "invalid block type";
                                r = Z_DATA_ERROR;

                                bitb = b; bitk = k;
                                z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                                write = q;
                                return inflate_flush(z, r);
                        }
                        break;

                    case LENS:

                        while (k < (32))
                        {
                            if (n != 0)
                            {
                                r = Z_OK;
                            }
                            else
                            {
                                bitb = b; bitk = k;
                                z.avail_in = n;
                                z.total_in += p - z.next_in_index; z.next_in_index = p;
                                write = q;
                                return inflate_flush(z, r);
                            }
                            ;
                            n--;
                            b |= (z.next_in[p++] & 0xff) << k;
                            k += 8;
                        }

                        if (((SupportClass.URShift((~b), 16)) & 0xffff) != (b & 0xffff))
                        {
                            mode = BAD;
                            z.msg = "invalid stored block lengths";
                            r = Z_DATA_ERROR;

                            bitb = b; bitk = k;
                            z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                            write = q;
                            return inflate_flush(z, r);
                        }
                        left = (b & 0xffff);
                        b = k = 0; // dump bits
                        mode = left != 0 ? STORED : (last != 0 ? DRY : TYPE);
                        break;

                    case STORED:
                        if (n == 0)
                        {
                            bitb = b; bitk = k;
                            z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                            write = q;
                            return inflate_flush(z, r);
                        }

                        if (m == 0)
                        {
                            if (q == end && read != 0)
                            {
                                q = 0; m = (int)(q < read ? read - q - 1 : end - q);
                            }
                            if (m == 0)
                            {
                                write = q;
                                r = inflate_flush(z, r);
                                q = write; m = (int)(q < read ? read - q - 1 : end - q);
                                if (q == end && read != 0)
                                {
                                    q = 0; m = (int)(q < read ? read - q - 1 : end - q);
                                }
                                if (m == 0)
                                {
                                    bitb = b; bitk = k;
                                    z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                                    write = q;
                                    return inflate_flush(z, r);
                                }
                            }
                        }
                        r = Z_OK;

                        t = left;
                        if (t > n)
                            t = n;
                        if (t > m)
                            t = m;
                        Array.Copy(z.next_in, p, window, q, t);
                        p += t; n -= t;
                        q += t; m -= t;
                        if ((left -= t) != 0)
                            break;
                        mode = last != 0 ? DRY : TYPE;
                        break;

                    case TABLE:

                        while (k < (14))
                        {
                            if (n != 0)
                            {
                                r = Z_OK;
                            }
                            else
                            {
                                bitb = b; bitk = k;
                                z.avail_in = n;
                                z.total_in += p - z.next_in_index; z.next_in_index = p;
                                write = q;
                                return inflate_flush(z, r);
                            }
                            ;
                            n--;
                            b |= (z.next_in[p++] & 0xff) << k;
                            k += 8;
                        }

                        table = t = (b & 0x3fff);
                        if ((t & 0x1f) > 29 || ((t >> 5) & 0x1f) > 29)
                        {
                            mode = BAD;
                            z.msg = "too many length or distance symbols";
                            r = Z_DATA_ERROR;

                            bitb = b; bitk = k;
                            z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                            write = q;
                            return inflate_flush(z, r);
                        }
                        t = 258 + (t & 0x1f) + ((t >> 5) & 0x1f);
                        blens = new int[t];
                        {
                            b = SupportClass.URShift(b, (14)); k -= (14);
                        }

                        index = 0;
                        mode = BTREE;
                        goto case BTREE;

                    case BTREE:
                        while (index < 4 + (SupportClass.URShift(table, 10)))
                        {
                            while (k < (3))
                            {
                                if (n != 0)
                                {
                                    r = Z_OK;
                                }
                                else
                                {
                                    bitb = b; bitk = k;
                                    z.avail_in = n;
                                    z.total_in += p - z.next_in_index; z.next_in_index = p;
                                    write = q;
                                    return inflate_flush(z, r);
                                }
                                ;
                                n--;
                                b |= (z.next_in[p++] & 0xff) << k;
                                k += 8;
                            }

                            blens[border[index++]] = b & 7;

                            {
                                b = SupportClass.URShift(b, (3)); k -= (3);
                            }
                        }

                        while (index < 19)
                        {
                            blens[border[index++]] = 0;
                        }

                        bb[0] = 7;
                        t = InfTree.inflate_trees_bits(blens, bb, tb, hufts, z);
                        if (t != Z_OK)
                        {
                            r = t;
                            if (r == Z_DATA_ERROR)
                            {
                                blens = null;
                                mode = BAD;
                            }

                            bitb = b; bitk = k;
                            z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                            write = q;
                            return inflate_flush(z, r);
                        }

                        index = 0;
                        mode = DTREE;
                        goto case DTREE;

                    case DTREE:
                        while (true)
                        {
                            t = table;
                            if (!(index < 258 + (t & 0x1f) + ((t >> 5) & 0x1f)))
                            {
                                break;
                            }


                            int i, j, c;

                            t = bb[0];

                            while (k < (t))
                            {
                                if (n != 0)
                                {
                                    r = Z_OK;
                                }
                                else
                                {
                                    bitb = b; bitk = k;
                                    z.avail_in = n;
                                    z.total_in += p - z.next_in_index; z.next_in_index = p;
                                    write = q;
                                    return inflate_flush(z, r);
                                }
                                ;
                                n--;
                                b |= (z.next_in[p++] & 0xff) << k;
                                k += 8;
                            }

                            if (tb[0] == -1)
                            {
                                //System.err.println("null...");
                            }

                            t = hufts[(tb[0] + (b & inflate_mask[t])) * 3 + 1];
                            c = hufts[(tb[0] + (b & inflate_mask[t])) * 3 + 2];

                            if (c < 16)
                            {
                                b = SupportClass.URShift(b, (t)); k -= (t);
                                blens[index++] = c;
                            }
                            else
                            {
                                // c == 16..18
                                i = c == 18 ? 7 : c - 14;
                                j = c == 18 ? 11 : 3;

                                while (k < (t + i))
                                {
                                    if (n != 0)
                                    {
                                        r = Z_OK;
                                    }
                                    else
                                    {
                                        bitb = b; bitk = k;
                                        z.avail_in = n;
                                        z.total_in += p - z.next_in_index; z.next_in_index = p;
                                        write = q;
                                        return inflate_flush(z, r);
                                    }
                                    ;
                                    n--;
                                    b |= (z.next_in[p++] & 0xff) << k;
                                    k += 8;
                                }

                                b = SupportClass.URShift(b, (t)); k -= (t);

                                j += (b & inflate_mask[i]);

                                b = SupportClass.URShift(b, (i)); k -= (i);

                                i = index;
                                t = table;
                                if (i + j > 258 + (t & 0x1f) + ((t >> 5) & 0x1f) || (c == 16 && i < 1))
                                {
                                    blens = null;
                                    mode = BAD;
                                    z.msg = "invalid bit length repeat";
                                    r = Z_DATA_ERROR;

                                    bitb = b; bitk = k;
                                    z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                                    write = q;
                                    return inflate_flush(z, r);
                                }

                                c = c == 16 ? blens[i - 1] : 0;
                                do
                                {
                                    blens[i++] = c;
                                }
                                while (--j != 0);
                                index = i;
                            }
                        }

                        tb[0] = -1;
                        {
                            int[] bl = new int[1];
                            int[] bd = new int[1];
                            int[] tl = new int[1];
                            int[] td = new int[1];


                            bl[0] = 9; // must be <= 9 for lookahead assumptions
                            bd[0] = 6; // must be <= 9 for lookahead assumptions
                            t = table;
                            t = InfTree.inflate_trees_dynamic(257 + (t & 0x1f), 1 + ((t >> 5) & 0x1f), blens, bl, bd, tl, td, hufts, z);
                            if (t != Z_OK)
                            {
                                if (t == Z_DATA_ERROR)
                                {
                                    blens = null;
                                    mode = BAD;
                                }
                                r = t;

                                bitb = b; bitk = k;
                                z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                                write = q;
                                return inflate_flush(z, r);
                            }

                            codes = new InfCodes(bl[0], bd[0], hufts, tl[0], hufts, td[0], z);
                        }
                        blens = null;
                        mode = CODES;
                        goto case CODES;

                    case CODES:
                        bitb = b; bitk = k;
                        z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                        write = q;

                        if ((r = codes.proc(this, z, r)) != Z_STREAM_END)
                        {
                            return inflate_flush(z, r);
                        }
                        r = Z_OK;
                        codes.free(z);

                        p = z.next_in_index; n = z.avail_in; b = bitb; k = bitk;
                        q = write; m = (int)(q < read ? read - q - 1 : end - q);

                        if (last == 0)
                        {
                            mode = TYPE;
                            break;
                        }
                        mode = DRY;
                        goto case DRY;

                    case DRY:
                        write = q;
                        r = inflate_flush(z, r);
                        q = write; m = (int)(q < read ? read - q - 1 : end - q);
                        if (read != write)
                        {
                            bitb = b; bitk = k;
                            z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                            write = q;
                            return inflate_flush(z, r);
                        }
                        mode = DONE;
                        goto case DONE;

                    case DONE:
                        r = Z_STREAM_END;

                        bitb = b; bitk = k;
                        z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                        write = q;
                        return inflate_flush(z, r);

                    case BAD:
                        r = Z_DATA_ERROR;

                        bitb = b; bitk = k;
                        z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                        write = q;
                        return inflate_flush(z, r);


                    default:
                        r = Z_STREAM_ERROR;

                        bitb = b; bitk = k;
                        z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                        write = q;
                        return inflate_flush(z, r);

                }
            }
        }

        internal void free(ZStream z)
        {
            reset(z, null);
            window = null;
            hufts = null;
            //ZFREE(z, s);
        }

        internal void set_dictionary(byte[] d, int start, int n)
        {
            Array.Copy(d, start, window, 0, n);
            read = write = n;
        }

        // Returns true if inflate is currently at the end of a block generated
        // by Z_SYNC_FLUSH or Z_FULL_FLUSH. 
        internal int sync_point()
        {
            return mode == LENS ? 1 : 0;
        }

        // copy as much as possible from the sliding window to the output area
        internal int inflate_flush(ZStream z, int r)
        {
            int n;
            int p;
            int q;

            // local copies of source and destination pointers
            p = z.next_out_index;
            q = read;

            // compute number of bytes to copy as far as end of window
            n = (int)((q <= write ? write : end) - q);
            if (n > z.avail_out)
                n = z.avail_out;
            if (n != 0 && r == Z_BUF_ERROR)
                r = Z_OK;

            // update counters
            z.avail_out -= n;
            z.total_out += n;

            // update check information
            if (checkfn != null)
                z.adler = check = z._adler.adler32(check, window, q, n);

            // copy as far as end of window
            Array.Copy(window, q, z.next_out, p, n);
            p += n;
            q += n;

            // see if more to copy at beginning of window
            if (q == end)
            {
                // wrap pointers
                q = 0;
                if (write == end)
                    write = 0;

                // compute bytes to copy
                n = write - q;
                if (n > z.avail_out)
                    n = z.avail_out;
                if (n != 0 && r == Z_BUF_ERROR)
                    r = Z_OK;

                // update counters
                z.avail_out -= n;
                z.total_out += n;

                // update check information
                if (checkfn != null)
                    z.adler = check = z._adler.adler32(check, window, q, n);

                // copy
                Array.Copy(window, q, z.next_out, p, n);
                p += n;
                q += n;
            }

            // update pointers
            z.next_out_index = p;
            read = q;

            // done
            return r;
        }
    }
    sealed class Adler32
    {

        // largest prime smaller than 65536
        private const int BASE = 65521;
        // NMAX is the largest n such that 255n(n+1)/2 + (n+1)(BASE-1) <= 2^32-1
        private const int NMAX = 5552;

        internal long adler32(long adler, byte[] buf, int index, int len)
        {
            if (buf == null)
            {
                return 1L;
            }

            long s1 = adler & 0xffff;
            long s2 = (adler >> 16) & 0xffff;
            int k;

            while (len > 0)
            {
                k = len < NMAX ? len : NMAX;
                len -= k;
                while (k >= 16)
                {
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    k -= 16;
                }
                if (k != 0)
                {
                    do
                    {
                        s1 += (buf[index++] & 0xff); s2 += s1;
                    }
                    while (--k != 0);
                }
                s1 %= BASE;
                s2 %= BASE;
            }
            return (s2 << 16) | s1;
        }

    }
    sealed class InfCodes
    {

        private static readonly int[] inflate_mask = new int[] { 0x00000000, 0x00000001, 0x00000003, 0x00000007, 0x0000000f, 0x0000001f, 0x0000003f, 0x0000007f, 0x000000ff, 0x000001ff, 0x000003ff, 0x000007ff, 0x00000fff, 0x00001fff, 0x00003fff, 0x00007fff, 0x0000ffff };

        private const int Z_OK = 0;
        private const int Z_STREAM_END = 1;
        private const int Z_NEED_DICT = 2;
        private const int Z_ERRNO = -1;
        private const int Z_STREAM_ERROR = -2;
        private const int Z_DATA_ERROR = -3;
        private const int Z_MEM_ERROR = -4;
        private const int Z_BUF_ERROR = -5;
        private const int Z_VERSION_ERROR = -6;

        // waiting for "i:"=input,
        //             "o:"=output,
        //             "x:"=nothing
        private const int START = 0; // x: set up for LEN
        private const int LEN = 1; // i: get length/literal/eob next
        private const int LENEXT = 2; // i: getting length extra (have base)
        private const int DIST = 3; // i: get distance next
        private const int DISTEXT = 4; // i: getting distance extra
        private const int COPY = 5; // o: copying bytes in window, waiting for space
        private const int LIT = 6; // o: got literal, waiting for output space
        private const int WASH = 7; // o: got eob, possibly still output waiting
        private const int END = 8; // x: got eob and all data flushed
        private const int BADCODE = 9; // x: got error

        internal int mode; // current inflate_codes mode

        // mode dependent information
        internal int len;

        internal int[] tree; // pointer into tree
        internal int tree_index = 0;
        internal int need; // bits needed

        internal int lit;

        // if EXT or COPY, where and how much
        internal int get_Renamed; // bits to get for extra
        internal int dist; // distance back to copy from

        internal byte lbits; // ltree bits decoded per branch
        internal byte dbits; // dtree bits decoder per branch
        internal int[] ltree; // literal/length/eob tree
        internal int ltree_index; // literal/length/eob tree
        internal int[] dtree; // distance tree
        internal int dtree_index; // distance tree

        internal InfCodes(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index, ZStream z)
        {
            mode = START;
            lbits = (byte)bl;
            dbits = (byte)bd;
            ltree = tl;
            ltree_index = tl_index;
            dtree = td;
            dtree_index = td_index;
        }

        internal InfCodes(int bl, int bd, int[] tl, int[] td, ZStream z)
        {
            mode = START;
            lbits = (byte)bl;
            dbits = (byte)bd;
            ltree = tl;
            ltree_index = 0;
            dtree = td;
            dtree_index = 0;
        }

        internal int proc(InfBlocks s, ZStream z, int r)
        {
            int j; // temporary storage
            //int[] t; // temporary pointer
            int tindex; // temporary pointer
            int e; // extra bits or operation
            int b = 0; // bit buffer
            int k = 0; // bits in bit buffer
            int p = 0; // input data pointer
            int n; // bytes available there
            int q; // output window write pointer
            int m; // bytes to end of window or read pointer
            int f; // pointer to copy strings from

            // copy input/output information to locals (UPDATE macro restores)
            p = z.next_in_index; n = z.avail_in; b = s.bitb; k = s.bitk;
            q = s.write; m = q < s.read ? s.read - q - 1 : s.end - q;

            // process input and output based on current state
            while (true)
            {
                switch (mode)
                {

                    // waiting for "i:"=input, "o:"=output, "x:"=nothing
                    case START:  // x: set up for LEN
                        if (m >= 258 && n >= 10)
                        {

                            s.bitb = b; s.bitk = k;
                            z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                            s.write = q;
                            r = inflate_fast(lbits, dbits, ltree, ltree_index, dtree, dtree_index, s, z);

                            p = z.next_in_index; n = z.avail_in; b = s.bitb; k = s.bitk;
                            q = s.write; m = q < s.read ? s.read - q - 1 : s.end - q;

                            if (r != Z_OK)
                            {
                                mode = r == Z_STREAM_END ? WASH : BADCODE;
                                break;
                            }
                        }
                        need = lbits;
                        tree = ltree;
                        tree_index = ltree_index;

                        mode = LEN;
                        goto case LEN;

                    case LEN:  // i: get length/literal/eob next
                        j = need;

                        while (k < (j))
                        {
                            if (n != 0)
                                r = Z_OK;
                            else
                            {

                                s.bitb = b; s.bitk = k;
                                z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                                s.write = q;
                                return s.inflate_flush(z, r);
                            }
                            n--;
                            b |= (z.next_in[p++] & 0xff) << k;
                            k += 8;
                        }

                        tindex = (tree_index + (b & inflate_mask[j])) * 3;

                        b = SupportClass.URShift(b, (tree[tindex + 1]));
                        k -= (tree[tindex + 1]);

                        e = tree[tindex];

                        if (e == 0)
                        {
                            // literal
                            lit = tree[tindex + 2];
                            mode = LIT;
                            break;
                        }
                        if ((e & 16) != 0)
                        {
                            // length
                            get_Renamed = e & 15;
                            len = tree[tindex + 2];
                            mode = LENEXT;
                            break;
                        }
                        if ((e & 64) == 0)
                        {
                            // next table
                            need = e;
                            tree_index = tindex / 3 + tree[tindex + 2];
                            break;
                        }
                        if ((e & 32) != 0)
                        {
                            // end of block
                            mode = WASH;
                            break;
                        }
                        mode = BADCODE; // invalid code
                        z.msg = "invalid literal/length code";
                        r = Z_DATA_ERROR;

                        s.bitb = b; s.bitk = k;
                        z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                        s.write = q;
                        return s.inflate_flush(z, r);


                    case LENEXT:  // i: getting length extra (have base)
                        j = get_Renamed;

                        while (k < (j))
                        {
                            if (n != 0)
                                r = Z_OK;
                            else
                            {

                                s.bitb = b; s.bitk = k;
                                z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                                s.write = q;
                                return s.inflate_flush(z, r);
                            }
                            n--; b |= (z.next_in[p++] & 0xff) << k;
                            k += 8;
                        }

                        len += (b & inflate_mask[j]);

                        b >>= j;
                        k -= j;

                        need = dbits;
                        tree = dtree;
                        tree_index = dtree_index;
                        mode = DIST;
                        goto case DIST;

                    case DIST:  // i: get distance next
                        j = need;

                        while (k < (j))
                        {
                            if (n != 0)
                                r = Z_OK;
                            else
                            {

                                s.bitb = b; s.bitk = k;
                                z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                                s.write = q;
                                return s.inflate_flush(z, r);
                            }
                            n--; b |= (z.next_in[p++] & 0xff) << k;
                            k += 8;
                        }

                        tindex = (tree_index + (b & inflate_mask[j])) * 3;

                        b >>= tree[tindex + 1];
                        k -= tree[tindex + 1];

                        e = (tree[tindex]);
                        if ((e & 16) != 0)
                        {
                            // distance
                            get_Renamed = e & 15;
                            dist = tree[tindex + 2];
                            mode = DISTEXT;
                            break;
                        }
                        if ((e & 64) == 0)
                        {
                            // next table
                            need = e;
                            tree_index = tindex / 3 + tree[tindex + 2];
                            break;
                        }
                        mode = BADCODE; // invalid code
                        z.msg = "invalid distance code";
                        r = Z_DATA_ERROR;

                        s.bitb = b; s.bitk = k;
                        z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                        s.write = q;
                        return s.inflate_flush(z, r);


                    case DISTEXT:  // i: getting distance extra
                        j = get_Renamed;

                        while (k < (j))
                        {
                            if (n != 0)
                                r = Z_OK;
                            else
                            {

                                s.bitb = b; s.bitk = k;
                                z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                                s.write = q;
                                return s.inflate_flush(z, r);
                            }
                            n--; b |= (z.next_in[p++] & 0xff) << k;
                            k += 8;
                        }

                        dist += (b & inflate_mask[j]);

                        b >>= j;
                        k -= j;

                        mode = COPY;
                        goto case COPY;

                    case COPY:  // o: copying bytes in window, waiting for space
                        f = q - dist;
                        while (f < 0)
                        {
                            // modulo window size-"while" instead
                            f += s.end; // of "if" handles invalid distances
                        }
                        while (len != 0)
                        {

                            if (m == 0)
                            {
                                if (q == s.end && s.read != 0)
                                {
                                    q = 0; m = q < s.read ? s.read - q - 1 : s.end - q;
                                }
                                if (m == 0)
                                {
                                    s.write = q; r = s.inflate_flush(z, r);
                                    q = s.write; m = q < s.read ? s.read - q - 1 : s.end - q;

                                    if (q == s.end && s.read != 0)
                                    {
                                        q = 0; m = q < s.read ? s.read - q - 1 : s.end - q;
                                    }

                                    if (m == 0)
                                    {
                                        s.bitb = b; s.bitk = k;
                                        z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                                        s.write = q;
                                        return s.inflate_flush(z, r);
                                    }
                                }
                            }

                            s.window[q++] = s.window[f++]; m--;

                            if (f == s.end)
                                f = 0;
                            len--;
                        }
                        mode = START;
                        break;

                    case LIT:  // o: got literal, waiting for output space
                        if (m == 0)
                        {
                            if (q == s.end && s.read != 0)
                            {
                                q = 0; m = q < s.read ? s.read - q - 1 : s.end - q;
                            }
                            if (m == 0)
                            {
                                s.write = q; r = s.inflate_flush(z, r);
                                q = s.write; m = q < s.read ? s.read - q - 1 : s.end - q;

                                if (q == s.end && s.read != 0)
                                {
                                    q = 0; m = q < s.read ? s.read - q - 1 : s.end - q;
                                }
                                if (m == 0)
                                {
                                    s.bitb = b; s.bitk = k;
                                    z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                                    s.write = q;
                                    return s.inflate_flush(z, r);
                                }
                            }
                        }
                        r = Z_OK;

                        s.window[q++] = (byte)lit; m--;

                        mode = START;
                        break;

                    case WASH:  // o: got eob, possibly more output
                        if (k > 7)
                        {
                            // return unused byte, if any
                            k -= 8;
                            n++;
                            p--; // can always return one
                        }

                        s.write = q; r = s.inflate_flush(z, r);
                        q = s.write; m = q < s.read ? s.read - q - 1 : s.end - q;

                        if (s.read != s.write)
                        {
                            s.bitb = b; s.bitk = k;
                            z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                            s.write = q;
                            return s.inflate_flush(z, r);
                        }
                        mode = END;
                        goto case END;

                    case END:
                        r = Z_STREAM_END;
                        s.bitb = b; s.bitk = k;
                        z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                        s.write = q;
                        return s.inflate_flush(z, r);


                    case BADCODE:  // x: got error

                        r = Z_DATA_ERROR;

                        s.bitb = b; s.bitk = k;
                        z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                        s.write = q;
                        return s.inflate_flush(z, r);


                    default:
                        r = Z_STREAM_ERROR;

                        s.bitb = b; s.bitk = k;
                        z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                        s.write = q;
                        return s.inflate_flush(z, r);

                }
            }
        }

        internal void free(ZStream z)
        {
            //  ZFREE(z, c);
        }

        // Called with number of bytes left to write in window at least 258
        // (the maximum string length) and number of input bytes available
        // at least ten.  The ten bytes are six bytes for the longest length/
        // distance pair plus four bytes for overloading the bit buffer.

        internal int inflate_fast(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index, InfBlocks s, ZStream z)
        {
            int t; // temporary pointer
            int[] tp; // temporary pointer
            int tp_index; // temporary pointer
            int e; // extra bits or operation
            int b; // bit buffer
            int k; // bits in bit buffer
            int p; // input data pointer
            int n; // bytes available there
            int q; // output window write pointer
            int m; // bytes to end of window or read pointer
            int ml; // mask for literal/length tree
            int md; // mask for distance tree
            int c; // bytes to copy
            int d; // distance back to copy from
            int r; // copy source pointer

            // load input, output, bit values
            p = z.next_in_index; n = z.avail_in; b = s.bitb; k = s.bitk;
            q = s.write; m = q < s.read ? s.read - q - 1 : s.end - q;

            // initialize masks
            ml = inflate_mask[bl];
            md = inflate_mask[bd];

            // do until not enough input or output space for fast loop
            do
            {
                // assume called with m >= 258 && n >= 10
                // get literal/length code
                while (k < (20))
                {
                    // max bits for literal/length code
                    n--;
                    b |= (z.next_in[p++] & 0xff) << k; k += 8;
                }

                t = b & ml;
                tp = tl;
                tp_index = tl_index;
                if ((e = tp[(tp_index + t) * 3]) == 0)
                {
                    b >>= (tp[(tp_index + t) * 3 + 1]); k -= (tp[(tp_index + t) * 3 + 1]);

                    s.window[q++] = (byte)tp[(tp_index + t) * 3 + 2];
                    m--;
                    continue;
                }
                do
                {

                    b >>= (tp[(tp_index + t) * 3 + 1]); k -= (tp[(tp_index + t) * 3 + 1]);

                    if ((e & 16) != 0)
                    {
                        e &= 15;
                        c = tp[(tp_index + t) * 3 + 2] + ((int)b & inflate_mask[e]);

                        b >>= e; k -= e;

                        // decode distance base of block to copy
                        while (k < (15))
                        {
                            // max bits for distance code
                            n--;
                            b |= (z.next_in[p++] & 0xff) << k; k += 8;
                        }

                        t = b & md;
                        tp = td;
                        tp_index = td_index;
                        e = tp[(tp_index + t) * 3];

                        do
                        {

                            b >>= (tp[(tp_index + t) * 3 + 1]); k -= (tp[(tp_index + t) * 3 + 1]);

                            if ((e & 16) != 0)
                            {
                                // get extra bits to add to distance base
                                e &= 15;
                                while (k < (e))
                                {
                                    // get extra bits (up to 13)
                                    n--;
                                    b |= (z.next_in[p++] & 0xff) << k; k += 8;
                                }

                                d = tp[(tp_index + t) * 3 + 2] + (b & inflate_mask[e]);

                                b >>= (e); k -= (e);

                                // do the copy
                                m -= c;
                                if (q >= d)
                                {
                                    // offset before dest
                                    //  just copy
                                    r = q - d;
                                    if (q - r > 0 && 2 > (q - r))
                                    {
                                        s.window[q++] = s.window[r++]; c--; // minimum count is three,
                                        s.window[q++] = s.window[r++]; c--; // so unroll loop a little
                                    }
                                    else
                                    {
                                        Array.Copy(s.window, r, s.window, q, 2);
                                        q += 2; r += 2; c -= 2;
                                    }
                                }
                                else
                                {
                                    // else offset after destination
                                    r = q - d;
                                    do
                                    {
                                        r += s.end; // force pointer in window
                                    }
                                    while (r < 0); // covers invalid distances
                                    e = s.end - r;
                                    if (c > e)
                                    {
                                        // if source crosses,
                                        c -= e; // wrapped copy
                                        if (q - r > 0 && e > (q - r))
                                        {
                                            do
                                            {
                                                s.window[q++] = s.window[r++];
                                            }
                                            while (--e != 0);
                                        }
                                        else
                                        {
                                            Array.Copy(s.window, r, s.window, q, e);
                                            q += e; r += e; e = 0;
                                        }
                                        r = 0; // copy rest from start of window
                                    }
                                }

                                // copy all or what's left
                                if (q - r > 0 && c > (q - r))
                                {
                                    do
                                    {
                                        s.window[q++] = s.window[r++];
                                    }
                                    while (--c != 0);
                                }
                                else
                                {
                                    Array.Copy(s.window, r, s.window, q, c);
                                    q += c; r += c; c = 0;
                                }
                                break;
                            }
                            else if ((e & 64) == 0)
                            {
                                t += tp[(tp_index + t) * 3 + 2];
                                t += (b & inflate_mask[e]);
                                e = tp[(tp_index + t) * 3];
                            }
                            else
                            {
                                z.msg = "invalid distance code";

                                c = z.avail_in - n; c = (k >> 3) < c ? k >> 3 : c; n += c; p -= c; k -= (c << 3);

                                s.bitb = b; s.bitk = k;
                                z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                                s.write = q;

                                return Z_DATA_ERROR;
                            }
                        }
                        while (true);
                        break;
                    }

                    if ((e & 64) == 0)
                    {
                        t += tp[(tp_index + t) * 3 + 2];
                        t += (b & inflate_mask[e]);
                        if ((e = tp[(tp_index + t) * 3]) == 0)
                        {

                            b >>= (tp[(tp_index + t) * 3 + 1]); k -= (tp[(tp_index + t) * 3 + 1]);

                            s.window[q++] = (byte)tp[(tp_index + t) * 3 + 2];
                            m--;
                            break;
                        }
                    }
                    else if ((e & 32) != 0)
                    {

                        c = z.avail_in - n; c = (k >> 3) < c ? k >> 3 : c; n += c; p -= c; k -= (c << 3);

                        s.bitb = b; s.bitk = k;
                        z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                        s.write = q;

                        return Z_STREAM_END;
                    }
                    else
                    {
                        z.msg = "invalid literal/length code";

                        c = z.avail_in - n; c = (k >> 3) < c ? k >> 3 : c; n += c; p -= c; k -= (c << 3);

                        s.bitb = b; s.bitk = k;
                        z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
                        s.write = q;

                        return Z_DATA_ERROR;
                    }
                }
                while (true);
            }
            while (m >= 258 && n >= 10);

            // not enough input or output--restore pointers and return
            c = z.avail_in - n; c = (k >> 3) < c ? k >> 3 : c; n += c; p -= c; k -= (c << 3);

            s.bitb = b; s.bitk = k;
            z.avail_in = n; z.total_in += p - z.next_in_index; z.next_in_index = p;
            s.write = q;

            return Z_OK;
        }
    }
    sealed class Inflate
    {

        private const int MAX_WBITS = 15; // 32K LZ77 window

        // preset dictionary flag in zlib header
        private const int PRESET_DICT = 0x20;

        internal const int Z_NO_FLUSH = 0;
        internal const int Z_PARTIAL_FLUSH = 1;
        internal const int Z_SYNC_FLUSH = 2;
        internal const int Z_FULL_FLUSH = 3;
        internal const int Z_FINISH = 4;

        private const int Z_DEFLATED = 8;

        private const int Z_OK = 0;
        private const int Z_STREAM_END = 1;
        private const int Z_NEED_DICT = 2;
        private const int Z_ERRNO = -1;
        private const int Z_STREAM_ERROR = -2;
        private const int Z_DATA_ERROR = -3;
        private const int Z_MEM_ERROR = -4;
        private const int Z_BUF_ERROR = -5;
        private const int Z_VERSION_ERROR = -6;

        private const int METHOD = 0; // waiting for method byte
        private const int FLAG = 1; // waiting for flag byte
        private const int DICT4 = 2; // four dictionary check bytes to go
        private const int DICT3 = 3; // three dictionary check bytes to go
        private const int DICT2 = 4; // two dictionary check bytes to go
        private const int DICT1 = 5; // one dictionary check byte to go
        private const int DICT0 = 6; // waiting for inflateSetDictionary
        private const int BLOCKS = 7; // decompressing blocks
        private const int CHECK4 = 8; // four check bytes to go
        private const int CHECK3 = 9; // three check bytes to go
        private const int CHECK2 = 10; // two check bytes to go
        private const int CHECK1 = 11; // one check byte to go
        private const int DONE = 12; // finished check, done
        private const int BAD = 13; // got an error--stay here

        internal int mode; // current inflate mode

        // mode dependent information
        internal int method; // if FLAGS, method byte

        // if CHECK, check values to compare
        internal long[] was = new long[1]; // computed check value
        internal long need; // stream check value

        // if BAD, inflateSync's marker bytes count
        internal int marker;

        // mode independent information
        internal int nowrap; // flag for no wrapper
        internal int wbits; // log2(window size)  (8..15, defaults to 15)

        internal InfBlocks blocks; // current inflate_blocks state

        internal int inflateReset(ZStream z)
        {
            if (z == null || z.istate == null)
                return Z_STREAM_ERROR;

            z.total_in = z.total_out = 0;
            z.msg = null;
            z.istate.mode = z.istate.nowrap != 0 ? BLOCKS : METHOD;
            z.istate.blocks.reset(z, null);
            return Z_OK;
        }

        internal int inflateEnd(ZStream z)
        {
            if (blocks != null)
                blocks.free(z);
            blocks = null;
            //    ZFREE(z, z->state);
            return Z_OK;
        }

        internal int inflateInit(ZStream z, int w)
        {
            z.msg = null;
            blocks = null;

            // handle undocumented nowrap option (no zlib header or check)
            nowrap = 0;
            if (w < 0)
            {
                w = -w;
                nowrap = 1;
            }

            // set window size
            if (w < 8 || w > 15)
            {
                inflateEnd(z);
                return Z_STREAM_ERROR;
            }
            wbits = w;

            z.istate.blocks = new InfBlocks(z, z.istate.nowrap != 0 ? null : this, 1 << w);

            // reset state
            inflateReset(z);
            return Z_OK;
        }

        internal int inflate(ZStream z, int f)
        {
            int r;
            int b;

            if (z == null || z.istate == null || z.next_in == null)
                return Z_STREAM_ERROR;
            f = f == Z_FINISH ? Z_BUF_ERROR : Z_OK;
            r = Z_BUF_ERROR;
            while (true)
            {
                //System.out.println("mode: "+z.istate.mode);
                switch (z.istate.mode)
                {

                    case METHOD:

                        if (z.avail_in == 0)
                            return r; r = f;

                        z.avail_in--; z.total_in++;
                        if (((z.istate.method = z.next_in[z.next_in_index++]) & 0xf) != Z_DEFLATED)
                        {
                            z.istate.mode = BAD;
                            z.msg = "unknown compression method";
                            z.istate.marker = 5; // can't try inflateSync
                            break;
                        }
                        if ((z.istate.method >> 4) + 8 > z.istate.wbits)
                        {
                            z.istate.mode = BAD;
                            z.msg = "invalid window size";
                            z.istate.marker = 5; // can't try inflateSync
                            break;
                        }
                        z.istate.mode = FLAG;
                        goto case FLAG;

                    case FLAG:

                        if (z.avail_in == 0)
                            return r; r = f;

                        z.avail_in--; z.total_in++;
                        b = (z.next_in[z.next_in_index++]) & 0xff;

                        if ((((z.istate.method << 8) + b) % 31) != 0)
                        {
                            z.istate.mode = BAD;
                            z.msg = "incorrect header check";
                            z.istate.marker = 5; // can't try inflateSync
                            break;
                        }

                        if ((b & PRESET_DICT) == 0)
                        {
                            z.istate.mode = BLOCKS;
                            break;
                        }
                        z.istate.mode = DICT4;
                        goto case DICT4;

                    case DICT4:

                        if (z.avail_in == 0)
                            return r; r = f;

                        z.avail_in--; z.total_in++;
                        z.istate.need = ((z.next_in[z.next_in_index++] & 0xff) << 24) & unchecked((int)0xff000000L);
                        z.istate.mode = DICT3;
                        goto case DICT3;

                    case DICT3:

                        if (z.avail_in == 0)
                            return r; r = f;

                        z.avail_in--; z.total_in++;
                        z.istate.need += (((z.next_in[z.next_in_index++] & 0xff) << 16) & 0xff0000L);
                        z.istate.mode = DICT2;
                        goto case DICT2;

                    case DICT2:

                        if (z.avail_in == 0)
                            return r; r = f;

                        z.avail_in--; z.total_in++;
                        z.istate.need += (((z.next_in[z.next_in_index++] & 0xff) << 8) & 0xff00L);
                        z.istate.mode = DICT1;
                        goto case DICT1;

                    case DICT1:

                        if (z.avail_in == 0)
                            return r; r = f;

                        z.avail_in--; z.total_in++;
                        z.istate.need += (z.next_in[z.next_in_index++] & 0xffL);
                        z.adler = z.istate.need;
                        z.istate.mode = DICT0;
                        return Z_NEED_DICT;

                    case DICT0:
                        z.istate.mode = BAD;
                        z.msg = "need dictionary";
                        z.istate.marker = 0; // can try inflateSync
                        return Z_STREAM_ERROR;

                    case BLOCKS:

                        r = z.istate.blocks.proc(z, r);
                        if (r == Z_DATA_ERROR)
                        {
                            z.istate.mode = BAD;
                            z.istate.marker = 0; // can try inflateSync
                            break;
                        }
                        if (r == Z_OK)
                        {
                            r = f;
                        }
                        if (r != Z_STREAM_END)
                        {
                            return r;
                        }
                        r = f;
                        z.istate.blocks.reset(z, z.istate.was);
                        if (z.istate.nowrap != 0)
                        {
                            z.istate.mode = DONE;
                            break;
                        }
                        z.istate.mode = CHECK4;
                        goto case CHECK4;

                    case CHECK4:

                        if (z.avail_in == 0)
                            return r; r = f;

                        z.avail_in--; z.total_in++;
                        z.istate.need = ((z.next_in[z.next_in_index++] & 0xff) << 24) & unchecked((int)0xff000000L);
                        z.istate.mode = CHECK3;
                        goto case CHECK3;

                    case CHECK3:

                        if (z.avail_in == 0)
                            return r; r = f;

                        z.avail_in--; z.total_in++;
                        z.istate.need += (((z.next_in[z.next_in_index++] & 0xff) << 16) & 0xff0000L);
                        z.istate.mode = CHECK2;
                        goto case CHECK2;

                    case CHECK2:

                        if (z.avail_in == 0)
                            return r; r = f;

                        z.avail_in--; z.total_in++;
                        z.istate.need += (((z.next_in[z.next_in_index++] & 0xff) << 8) & 0xff00L);
                        z.istate.mode = CHECK1;
                        goto case CHECK1;

                    case CHECK1:

                        if (z.avail_in == 0)
                            return r; r = f;

                        z.avail_in--; z.total_in++;
                        z.istate.need += (z.next_in[z.next_in_index++] & 0xffL);

                        if (((int)(z.istate.was[0])) != ((int)(z.istate.need)))
                        {
                            z.istate.mode = BAD;
                            z.msg = "incorrect data check";
                            z.istate.marker = 5; // can't try inflateSync
                            break;
                        }

                        z.istate.mode = DONE;
                        goto case DONE;

                    case DONE:
                        return Z_STREAM_END;

                    case BAD:
                        return Z_DATA_ERROR;

                    default:
                        return Z_STREAM_ERROR;

                }
            }
        }


        internal int inflateSetDictionary(ZStream z, byte[] dictionary, int dictLength)
        {
            int index = 0;
            int length = dictLength;
            if (z == null || z.istate == null || z.istate.mode != DICT0)
                return Z_STREAM_ERROR;

            if (z._adler.adler32(1L, dictionary, 0, dictLength) != z.adler)
            {
                return Z_DATA_ERROR;
            }

            z.adler = z._adler.adler32(0, null, 0, 0);

            if (length >= (1 << z.istate.wbits))
            {
                length = (1 << z.istate.wbits) - 1;
                index = dictLength - length;
            }
            z.istate.blocks.set_dictionary(dictionary, index, length);
            z.istate.mode = BLOCKS;
            return Z_OK;
        }

        private static byte[] mark = new byte[] { (byte)0, (byte)0, (byte)SupportClass.Identity(0xff), (byte)SupportClass.Identity(0xff) };

        internal int inflateSync(ZStream z)
        {
            int n; // number of bytes to look at
            int p; // pointer to bytes
            int m; // number of marker bytes found in a row
            long r, w; // temporaries to save total_in and total_out

            // set up
            if (z == null || z.istate == null)
                return Z_STREAM_ERROR;
            if (z.istate.mode != BAD)
            {
                z.istate.mode = BAD;
                z.istate.marker = 0;
            }
            if ((n = z.avail_in) == 0)
                return Z_BUF_ERROR;
            p = z.next_in_index;
            m = z.istate.marker;

            // search
            while (n != 0 && m < 4)
            {
                if (z.next_in[p] == mark[m])
                {
                    m++;
                }
                else if (z.next_in[p] != 0)
                {
                    m = 0;
                }
                else
                {
                    m = 4 - m;
                }
                p++; n--;
            }

            // restore
            z.total_in += p - z.next_in_index;
            z.next_in_index = p;
            z.avail_in = n;
            z.istate.marker = m;

            // return no joy or set up to restart on a new block
            if (m != 4)
            {
                return Z_DATA_ERROR;
            }
            r = z.total_in; w = z.total_out;
            inflateReset(z);
            z.total_in = r; z.total_out = w;
            z.istate.mode = BLOCKS;
            return Z_OK;
        }

        // Returns true if inflate is currently at the end of a block generated
        // by Z_SYNC_FLUSH or Z_FULL_FLUSH. This function is used by one PPP
        // implementation to provide an additional safety check. PPP uses Z_SYNC_FLUSH
        // but removes the length bytes of the resulting empty stored block. When
        // decompressing, PPP checks that at the end of input packet, inflate is
        // waiting for these length bytes.
        internal int inflateSyncPoint(ZStream z)
        {
            if (z == null || z.istate == null || z.istate.blocks == null)
                return Z_STREAM_ERROR;
            return z.istate.blocks.sync_point();
        }
    }
    public sealed class Deflate
    {

        private const int MAX_MEM_LEVEL = 9;

        private const int Z_DEFAULT_COMPRESSION = -1;

        private const int MAX_WBITS = 15; // 32K LZ77 window
        private const int DEF_MEM_LEVEL = 8;

        internal class Config
        {
            internal int good_length; // reduce lazy search above this match length
            internal int max_lazy; // do not perform lazy search above this match length
            internal int nice_length; // quit search above this match length
            internal int max_chain;
            internal int func;
            internal Config(int good_length, int max_lazy, int nice_length, int max_chain, int func)
            {
                this.good_length = good_length;
                this.max_lazy = max_lazy;
                this.nice_length = nice_length;
                this.max_chain = max_chain;
                this.func = func;
            }
        }

        private const int STORED = 0;
        private const int FAST = 1;
        private const int SLOW = 2;
        private static Config[] config_table;

        private static readonly System.String[] z_errmsg = new System.String[] { "need dictionary", "stream end", "", "file error", "stream error", "data error", "insufficient memory", "buffer error", "incompatible version", "" };

        // block not completed, need more input or more output
        private const int NeedMore = 0;

        // block flush performed
        private const int BlockDone = 1;

        // finish started, need only more output at next deflate
        private const int FinishStarted = 2;

        // finish done, accept no more input or output
        private const int FinishDone = 3;

        // preset dictionary flag in zlib header
        private const int PRESET_DICT = 0x20;

        private const int Z_FILTERED = 1;
        private const int Z_HUFFMAN_ONLY = 2;
        private const int Z_DEFAULT_STRATEGY = 0;

        private const int Z_NO_FLUSH = 0;
        private const int Z_PARTIAL_FLUSH = 1;
        private const int Z_SYNC_FLUSH = 2;
        private const int Z_FULL_FLUSH = 3;
        private const int Z_FINISH = 4;

        private const int Z_OK = 0;
        private const int Z_STREAM_END = 1;
        private const int Z_NEED_DICT = 2;
        private const int Z_ERRNO = -1;
        private const int Z_STREAM_ERROR = -2;
        private const int Z_DATA_ERROR = -3;
        private const int Z_MEM_ERROR = -4;
        private const int Z_BUF_ERROR = -5;
        private const int Z_VERSION_ERROR = -6;

        private const int INIT_STATE = 42;
        private const int BUSY_STATE = 113;
        private const int FINISH_STATE = 666;

        // The deflate compression method
        private const int Z_DEFLATED = 8;

        private const int STORED_BLOCK = 0;
        private const int STATIC_TREES = 1;
        private const int DYN_TREES = 2;

        // The three kinds of block type
        private const int Z_BINARY = 0;
        private const int Z_ASCII = 1;
        private const int Z_UNKNOWN = 2;

        private const int Buf_size = 8 * 2;

        // repeat previous bit length 3-6 times (2 bits of repeat count)
        private const int REP_3_6 = 16;

        // repeat a zero length 3-10 times  (3 bits of repeat count)
        private const int REPZ_3_10 = 17;

        // repeat a zero length 11-138 times  (7 bits of repeat count)
        private const int REPZ_11_138 = 18;

        private const int MIN_MATCH = 3;
        private const int MAX_MATCH = 258;
        private static readonly int MIN_LOOKAHEAD = (MAX_MATCH + MIN_MATCH + 1);

        private const int MAX_BITS = 15;
        private const int D_CODES = 30;
        private const int BL_CODES = 19;
        private const int LENGTH_CODES = 29;
        private const int LITERALS = 256;
        private static readonly int L_CODES = (LITERALS + 1 + LENGTH_CODES);
        private static readonly int HEAP_SIZE = (2 * L_CODES + 1);

        private const int END_BLOCK = 256;

        internal ZStream strm; // pointer back to this zlib stream
        internal int status; // as the name implies
        internal byte[] pending_buf; // output still pending
        internal int pending_buf_size; // size of pending_buf
        internal int pending_out; // next pending byte to output to the stream
        internal int pending; // nb of bytes in the pending buffer
        internal int noheader; // suppress zlib header and adler32
        internal byte data_type; // UNKNOWN, BINARY or ASCII
        internal byte method; // STORED (for zip only) or DEFLATED
        internal int last_flush; // value of flush param for previous deflate call

        internal int w_size; // LZ77 window size (32K by default)
        internal int w_bits; // log2(w_size)  (8..16)
        internal int w_mask; // w_size - 1

        internal byte[] window;
        // Sliding window. Input bytes are read into the second half of the window,
        // and move to the first half later to keep a dictionary of at least wSize
        // bytes. With this organization, matches are limited to a distance of
        // wSize-MAX_MATCH bytes, but this ensures that IO is always
        // performed with a length multiple of the block size. Also, it limits
        // the window size to 64K, which is quite useful on MSDOS.
        // To do: use the user input buffer as sliding window.

        internal int window_size;
        // Actual size of window: 2*wSize, except when the user input buffer
        // is directly used as sliding window.

        internal short[] prev;
        // Link to older string with same hash index. To limit the size of this
        // array to 64K, this link is maintained only for the last 32K strings.
        // An index in this array is thus a window index modulo 32K.

        internal short[] head; // Heads of the hash chains or NIL.

        internal int ins_h; // hash index of string to be inserted
        internal int hash_size; // number of elements in hash table
        internal int hash_bits; // log2(hash_size)
        internal int hash_mask; // hash_size-1

        // Number of bits by which ins_h must be shifted at each input
        // step. It must be such that after MIN_MATCH steps, the oldest
        // byte no longer takes part in the hash key, that is:
        // hash_shift * MIN_MATCH >= hash_bits
        internal int hash_shift;

        // Window position at the beginning of the current output block. Gets
        // negative when the window is moved backwards.

        internal int block_start;

        internal int match_length; // length of best match
        internal int prev_match; // previous match
        internal int match_available; // set if previous match exists
        internal int strstart; // start of string to insert
        internal int match_start; // start of matching string
        internal int lookahead; // number of valid bytes ahead in window

        // Length of the best match at previous step. Matches not greater than this
        // are discarded. This is used in the lazy match evaluation.
        internal int prev_length;

        // To speed up deflation, hash chains are never searched beyond this
        // length.  A higher limit improves compression ratio but degrades the speed.
        internal int max_chain_length;

        // Attempt to find a better match only when the current match is strictly
        // smaller than this value. This mechanism is used only for compression
        // levels >= 4.
        internal int max_lazy_match;

        // Insert new strings in the hash table only if the match length is not
        // greater than this length. This saves time but degrades compression.
        // max_insert_length is used only for compression levels <= 3.

        internal int level; // compression level (1..9)
        internal int strategy; // favor or force Huffman coding

        // Use a faster search when the previous match is longer than this
        internal int good_match;

        // Stop searching when current match exceeds this
        internal int nice_match;

        internal short[] dyn_ltree; // literal and length tree
        internal short[] dyn_dtree; // distance tree
        internal short[] bl_tree; // Huffman tree for bit lengths

        internal Tree l_desc = new Tree(); // desc for literal tree
        internal Tree d_desc = new Tree(); // desc for distance tree
        internal Tree bl_desc = new Tree(); // desc for bit length tree

        // number of codes at each bit length for an optimal tree
        internal short[] bl_count = new short[MAX_BITS + 1];

        // heap used to build the Huffman trees
        internal int[] heap = new int[2 * L_CODES + 1];

        internal int heap_len; // number of elements in the heap
        internal int heap_max; // element of largest frequency
        // The sons of heap[n] are heap[2*n] and heap[2*n+1]. heap[0] is not used.
        // The same heap array is used to build all trees.

        // Depth of each subtree used as tie breaker for trees of equal frequency
        internal byte[] depth = new byte[2 * L_CODES + 1];

        internal int l_buf; // index for literals or lengths */

        // Size of match buffer for literals/lengths.  There are 4 reasons for
        // limiting lit_bufsize to 64K:
        //   - frequencies can be kept in 16 bit counters
        //   - if compression is not successful for the first block, all input
        //     data is still in the window so we can still emit a stored block even
        //     when input comes from standard input.  (This can also be done for
        //     all blocks if lit_bufsize is not greater than 32K.)
        //   - if compression is not successful for a file smaller than 64K, we can
        //     even emit a stored file instead of a stored block (saving 5 bytes).
        //     This is applicable only for zip (not gzip or zlib).
        //   - creating new Huffman trees less frequently may not provide fast
        //     adaptation to changes in the input data statistics. (Take for
        //     example a binary file with poorly compressible code followed by
        //     a highly compressible string table.) Smaller buffer sizes give
        //     fast adaptation but have of course the overhead of transmitting
        //     trees more frequently.
        //   - I can't count above 4
        internal int lit_bufsize;

        internal int last_lit; // running index in l_buf

        // Buffer for distances. To simplify the code, d_buf and l_buf have
        // the same number of elements. To use different lengths, an extra flag
        // array would be necessary.

        internal int d_buf; // index of pendig_buf

        internal int opt_len; // bit length of current block with optimal trees
        internal int static_len; // bit length of current block with static trees
        internal int matches; // number of string matches in current block
        internal int last_eob_len; // bit length of EOB code for last block

        // Output buffer. bits are inserted starting at the bottom (least
        // significant bits).
        internal short bi_buf;

        // Number of valid bits in bi_buf.  All bits above the last valid bit
        // are always zero.
        internal int bi_valid;

        internal Deflate()
        {
            dyn_ltree = new short[HEAP_SIZE * 2];
            dyn_dtree = new short[(2 * D_CODES + 1) * 2]; // distance tree
            bl_tree = new short[(2 * BL_CODES + 1) * 2]; // Huffman tree for bit lengths
        }

        internal void lm_init()
        {
            window_size = 2 * w_size;

            head[hash_size - 1] = 0;
            for (int i = 0; i < hash_size - 1; i++)
            {
                head[i] = 0;
            }

            // Set the default configuration parameters:
            max_lazy_match = Deflate.config_table[level].max_lazy;
            good_match = Deflate.config_table[level].good_length;
            nice_match = Deflate.config_table[level].nice_length;
            max_chain_length = Deflate.config_table[level].max_chain;

            strstart = 0;
            block_start = 0;
            lookahead = 0;
            match_length = prev_length = MIN_MATCH - 1;
            match_available = 0;
            ins_h = 0;
        }

        // Initialize the tree data structures for a new zlib stream.
        internal void tr_init()
        {

            l_desc.dyn_tree = dyn_ltree;
            l_desc.stat_desc = StaticTree.static_l_desc;

            d_desc.dyn_tree = dyn_dtree;
            d_desc.stat_desc = StaticTree.static_d_desc;

            bl_desc.dyn_tree = bl_tree;
            bl_desc.stat_desc = StaticTree.static_bl_desc;

            bi_buf = 0;
            bi_valid = 0;
            last_eob_len = 8; // enough lookahead for inflate

            // Initialize the first block of the first file:
            init_block();
        }

        internal void init_block()
        {
            // Initialize the trees.
            for (int i = 0; i < L_CODES; i++)
                dyn_ltree[i * 2] = 0;
            for (int i = 0; i < D_CODES; i++)
                dyn_dtree[i * 2] = 0;
            for (int i = 0; i < BL_CODES; i++)
                bl_tree[i * 2] = 0;

            dyn_ltree[END_BLOCK * 2] = 1;
            opt_len = static_len = 0;
            last_lit = matches = 0;
        }

        // Restore the heap property by moving down the tree starting at node k,
        // exchanging a node with the smallest of its two sons if necessary, stopping
        // when the heap property is re-established (each father smaller than its
        // two sons).
        internal void pqdownheap(short[] tree, int k)
        {
            int v = heap[k];
            int j = k << 1; // left son of k
            while (j <= heap_len)
            {
                // Set j to the smallest of the two sons:
                if (j < heap_len && smaller(tree, heap[j + 1], heap[j], depth))
                {
                    j++;
                }
                // Exit if v is smaller than both sons
                if (smaller(tree, v, heap[j], depth))
                    break;

                // Exchange v with the smallest son
                heap[k] = heap[j]; k = j;
                // And continue down the tree, setting j to the left son of k
                j <<= 1;
            }
            heap[k] = v;
        }

        internal static bool smaller(short[] tree, int n, int m, byte[] depth)
        {
            return (tree[n * 2] < tree[m * 2] || (tree[n * 2] == tree[m * 2] && depth[n] <= depth[m]));
        }

        // Scan a literal or distance tree to determine the frequencies of the codes
        // in the bit length tree.
        internal void scan_tree(short[] tree, int max_code)
        {
            int n; // iterates over all tree elements
            int prevlen = -1; // last emitted length
            int curlen; // length of current code
            int nextlen = tree[0 * 2 + 1]; // length of next code
            int count = 0; // repeat count of the current code
            int max_count = 7; // max repeat count
            int min_count = 4; // min repeat count

            if (nextlen == 0)
            {
                max_count = 138; min_count = 3;
            }
            tree[(max_code + 1) * 2 + 1] = (short)SupportClass.Identity(0xffff); // guard

            for (n = 0; n <= max_code; n++)
            {
                curlen = nextlen; nextlen = tree[(n + 1) * 2 + 1];
                if (++count < max_count && curlen == nextlen)
                {
                    continue;
                }
                else if (count < min_count)
                {
                    bl_tree[curlen * 2] = (short)(bl_tree[curlen * 2] + count);
                }
                else if (curlen != 0)
                {
                    if (curlen != prevlen)
                        bl_tree[curlen * 2]++;
                    bl_tree[REP_3_6 * 2]++;
                }
                else if (count <= 10)
                {
                    bl_tree[REPZ_3_10 * 2]++;
                }
                else
                {
                    bl_tree[REPZ_11_138 * 2]++;
                }
                count = 0; prevlen = curlen;
                if (nextlen == 0)
                {
                    max_count = 138; min_count = 3;
                }
                else if (curlen == nextlen)
                {
                    max_count = 6; min_count = 3;
                }
                else
                {
                    max_count = 7; min_count = 4;
                }
            }
        }

        // Construct the Huffman tree for the bit lengths and return the index in
        // bl_order of the last bit length code to send.
        internal int build_bl_tree()
        {
            int max_blindex; // index of last bit length code of non zero freq

            // Determine the bit length frequencies for literal and distance trees
            scan_tree(dyn_ltree, l_desc.max_code);
            scan_tree(dyn_dtree, d_desc.max_code);

            // Build the bit length tree:
            bl_desc.build_tree(this);
            // opt_len now includes the length of the tree representations, except
            // the lengths of the bit lengths codes and the 5+5+4 bits for the counts.

            // Determine the number of bit length codes to send. The pkzip format
            // requires that at least 4 bit length codes be sent. (appnote.txt says
            // 3 but the actual value used is 4.)
            for (max_blindex = BL_CODES - 1; max_blindex >= 3; max_blindex--)
            {
                if (bl_tree[Tree.bl_order[max_blindex] * 2 + 1] != 0)
                    break;
            }
            // Update opt_len to include the bit length tree and counts
            opt_len += 3 * (max_blindex + 1) + 5 + 5 + 4;

            return max_blindex;
        }


        // Send the header for a block using dynamic Huffman trees: the counts, the
        // lengths of the bit length codes, the literal tree and the distance tree.
        // IN assertion: lcodes >= 257, dcodes >= 1, blcodes >= 4.
        internal void send_all_trees(int lcodes, int dcodes, int blcodes)
        {
            int rank; // index in bl_order

            send_bits(lcodes - 257, 5); // not +255 as stated in appnote.txt
            send_bits(dcodes - 1, 5);
            send_bits(blcodes - 4, 4); // not -3 as stated in appnote.txt
            for (rank = 0; rank < blcodes; rank++)
            {
                send_bits(bl_tree[Tree.bl_order[rank] * 2 + 1], 3);
            }
            send_tree(dyn_ltree, lcodes - 1); // literal tree
            send_tree(dyn_dtree, dcodes - 1); // distance tree
        }

        // Send a literal or distance tree in compressed form, using the codes in
        // bl_tree.
        internal void send_tree(short[] tree, int max_code)
        {
            int n; // iterates over all tree elements
            int prevlen = -1; // last emitted length
            int curlen; // length of current code
            int nextlen = tree[0 * 2 + 1]; // length of next code
            int count = 0; // repeat count of the current code
            int max_count = 7; // max repeat count
            int min_count = 4; // min repeat count

            if (nextlen == 0)
            {
                max_count = 138; min_count = 3;
            }

            for (n = 0; n <= max_code; n++)
            {
                curlen = nextlen; nextlen = tree[(n + 1) * 2 + 1];
                if (++count < max_count && curlen == nextlen)
                {
                    continue;
                }
                else if (count < min_count)
                {
                    do
                    {
                        send_code(curlen, bl_tree);
                    }
                    while (--count != 0);
                }
                else if (curlen != 0)
                {
                    if (curlen != prevlen)
                    {
                        send_code(curlen, bl_tree); count--;
                    }
                    send_code(REP_3_6, bl_tree);
                    send_bits(count - 3, 2);
                }
                else if (count <= 10)
                {
                    send_code(REPZ_3_10, bl_tree);
                    send_bits(count - 3, 3);
                }
                else
                {
                    send_code(REPZ_11_138, bl_tree);
                    send_bits(count - 11, 7);
                }
                count = 0; prevlen = curlen;
                if (nextlen == 0)
                {
                    max_count = 138; min_count = 3;
                }
                else if (curlen == nextlen)
                {
                    max_count = 6; min_count = 3;
                }
                else
                {
                    max_count = 7; min_count = 4;
                }
            }
        }

        // Output a byte on the stream.
        // IN assertion: there is enough room in pending_buf.
        internal void put_byte(byte[] p, int start, int len)
        {
            Array.Copy(p, start, pending_buf, pending, len);
            pending += len;
        }

        internal void put_byte(byte c)
        {
            pending_buf[pending++] = c;
        }
        internal void put_short(int w)
        {
            put_byte((byte)(w));
            put_byte((byte)(SupportClass.URShift(w, 8)));
        }
        internal void putShortMSB(int b)
        {
            put_byte((byte)(b >> 8));
            put_byte((byte)(b));
        }

        internal void send_code(int c, short[] tree)
        {
            send_bits((tree[c * 2] & 0xffff), (tree[c * 2 + 1] & 0xffff));
        }

        internal void send_bits(int value_Renamed, int length)
        {
            int len = length;
            if (bi_valid > (int)Buf_size - len)
            {
                int val = value_Renamed;
                //      bi_buf |= (val << bi_valid);
                bi_buf = (short)((ushort)bi_buf | (ushort)(((val << bi_valid) & 0xffff)));
                put_short(bi_buf);
                bi_buf = (short)(SupportClass.URShift(val, (Buf_size - bi_valid)));
                bi_valid += len - Buf_size;
            }
            else
            {
                //      bi_buf |= (value) << bi_valid;
                bi_buf = (short)((ushort)bi_buf | (ushort)((((value_Renamed) << bi_valid) & 0xffff)));
                bi_valid += len;
            }
        }

        // Send one empty static block to give enough lookahead for inflate.
        // This takes 10 bits, of which 7 may remain in the bit buffer.
        // The current inflate code requires 9 bits of lookahead. If the
        // last two codes for the previous block (real code plus EOB) were coded
        // on 5 bits or less, inflate may have only 5+3 bits of lookahead to decode
        // the last real code. In this case we send two empty static blocks instead
        // of one. (There are no problems if the previous block is stored or fixed.)
        // To simplify the code, we assume the worst case of last real code encoded
        // on one bit only.
        internal void _tr_align()
        {
            send_bits(STATIC_TREES << 1, 3);
            send_code(END_BLOCK, StaticTree.static_ltree);

            bi_flush();

            // Of the 10 bits for the empty block, we have already sent
            // (10 - bi_valid) bits. The lookahead for the last real code (before
            // the EOB of the previous block) was thus at least one plus the length
            // of the EOB plus what we have just sent of the empty static block.
            if (1 + last_eob_len + 10 - bi_valid < 9)
            {
                send_bits(STATIC_TREES << 1, 3);
                send_code(END_BLOCK, StaticTree.static_ltree);
                bi_flush();
            }
            last_eob_len = 7;
        }


        // Save the match info and tally the frequency counts. Return true if
        // the current block must be flushed.
        internal bool _tr_tally(int dist, int lc)
        {

            pending_buf[d_buf + last_lit * 2] = (byte)(SupportClass.URShift(dist, 8));
            pending_buf[d_buf + last_lit * 2 + 1] = (byte)dist;

            pending_buf[l_buf + last_lit] = (byte)lc; last_lit++;

            if (dist == 0)
            {
                // lc is the unmatched char
                dyn_ltree[lc * 2]++;
            }
            else
            {
                matches++;
                // Here, lc is the match length - MIN_MATCH
                dist--; // dist = match distance - 1
                dyn_ltree[(Tree._length_code[lc] + LITERALS + 1) * 2]++;
                dyn_dtree[Tree.d_code(dist) * 2]++;
            }

            if ((last_lit & 0x1fff) == 0 && level > 2)
            {
                // Compute an upper bound for the compressed length
                int out_length = last_lit * 8;
                int in_length = strstart - block_start;
                int dcode;
                for (dcode = 0; dcode < D_CODES; dcode++)
                {
                    out_length = (int)(out_length + (int)dyn_dtree[dcode * 2] * (5L + Tree.extra_dbits[dcode]));
                }
                out_length = SupportClass.URShift(out_length, 3);
                if ((matches < (last_lit / 2)) && out_length < in_length / 2)
                    return true;
            }

            return (last_lit == lit_bufsize - 1);
            // We avoid equality with lit_bufsize because of wraparound at 64K
            // on 16 bit machines and because stored blocks are restricted to
            // 64K-1 bytes.
        }

        // Send the block data compressed using the given Huffman trees
        internal void compress_block(short[] ltree, short[] dtree)
        {
            int dist; // distance of matched string
            int lc; // match length or unmatched char (if dist == 0)
            int lx = 0; // running index in l_buf
            int code; // the code to send
            int extra; // number of extra bits to send

            if (last_lit != 0)
            {
                do
                {
                    dist = ((pending_buf[d_buf + lx * 2] << 8) & 0xff00) | (pending_buf[d_buf + lx * 2 + 1] & 0xff);
                    lc = (pending_buf[l_buf + lx]) & 0xff; lx++;

                    if (dist == 0)
                    {
                        send_code(lc, ltree); // send a literal byte
                    }
                    else
                    {
                        // Here, lc is the match length - MIN_MATCH
                        code = Tree._length_code[lc];

                        send_code(code + LITERALS + 1, ltree); // send the length code
                        extra = Tree.extra_lbits[code];
                        if (extra != 0)
                        {
                            lc -= Tree.base_length[code];
                            send_bits(lc, extra); // send the extra length bits
                        }
                        dist--; // dist is now the match distance - 1
                        code = Tree.d_code(dist);

                        send_code(code, dtree); // send the distance code
                        extra = Tree.extra_dbits[code];
                        if (extra != 0)
                        {
                            dist -= Tree.base_dist[code];
                            send_bits(dist, extra); // send the extra distance bits
                        }
                    } // literal or match pair ?

                    // Check that the overlay between pending_buf and d_buf+l_buf is ok:
                }
                while (lx < last_lit);
            }

            send_code(END_BLOCK, ltree);
            last_eob_len = ltree[END_BLOCK * 2 + 1];
        }

        // Set the data type to ASCII or BINARY, using a crude approximation:
        // binary if more than 20% of the bytes are <= 6 or >= 128, ascii otherwise.
        // IN assertion: the fields freq of dyn_ltree are set and the total of all
        // frequencies does not exceed 64K (to fit in an int on 16 bit machines).
        internal void set_data_type()
        {
            int n = 0;
            int ascii_freq = 0;
            int bin_freq = 0;
            while (n < 7)
            {
                bin_freq += dyn_ltree[n * 2]; n++;
            }
            while (n < 128)
            {
                ascii_freq += dyn_ltree[n * 2]; n++;
            }
            while (n < LITERALS)
            {
                bin_freq += dyn_ltree[n * 2]; n++;
            }
            data_type = (byte)(bin_freq > (SupportClass.URShift(ascii_freq, 2)) ? Z_BINARY : Z_ASCII);
        }

        // Flush the bit buffer, keeping at most 7 bits in it.
        internal void bi_flush()
        {
            if (bi_valid == 16)
            {
                put_short(bi_buf);
                bi_buf = 0;
                bi_valid = 0;
            }
            else if (bi_valid >= 8)
            {
                put_byte((byte)bi_buf);
                bi_buf = (short)(SupportClass.URShift(bi_buf, 8));
                bi_valid -= 8;
            }
        }

        // Flush the bit buffer and align the output on a byte boundary
        internal void bi_windup()
        {
            if (bi_valid > 8)
            {
                put_short(bi_buf);
            }
            else if (bi_valid > 0)
            {
                put_byte((byte)bi_buf);
            }
            bi_buf = 0;
            bi_valid = 0;
        }

        // Copy a stored block, storing first the length and its
        // one's complement if requested.
        internal void copy_block(int buf, int len, bool header)
        {

            bi_windup(); // align on byte boundary
            last_eob_len = 8; // enough lookahead for inflate

            if (header)
            {
                put_short((short)len);
                put_short((short)~len);
            }

            //  while(len--!=0) {
            //    put_byte(window[buf+index]);
            //    index++;
            //  }
            put_byte(window, buf, len);
        }

        internal void flush_block_only(bool eof)
        {
            _tr_flush_block(block_start >= 0 ? block_start : -1, strstart - block_start, eof);
            block_start = strstart;
            strm.flush_pending();
        }

        // Copy without compression as much as possible from the input stream, return
        // the current block state.
        // This function does not insert new strings in the dictionary since
        // uncompressible data is probably not useful. This function is used
        // only for the level=0 compression option.
        // NOTE: this function should be optimized to avoid extra copying from
        // window to pending_buf.
        internal int deflate_stored(int flush)
        {
            // Stored blocks are limited to 0xffff bytes, pending_buf is limited
            // to pending_buf_size, and each stored block has a 5 byte header:

            int max_block_size = 0xffff;
            int max_start;

            if (max_block_size > pending_buf_size - 5)
            {
                max_block_size = pending_buf_size - 5;
            }

            // Copy as much as possible from input to output:
            while (true)
            {
                // Fill the window as much as possible:
                if (lookahead <= 1)
                {
                    fill_window();
                    if (lookahead == 0 && flush == Z_NO_FLUSH)
                        return NeedMore;
                    if (lookahead == 0)
                        break; // flush the current block
                }

                strstart += lookahead;
                lookahead = 0;

                // Emit a stored block if pending_buf will be full:
                max_start = block_start + max_block_size;
                if (strstart == 0 || strstart >= max_start)
                {
                    // strstart == 0 is possible when wraparound on 16-bit machine
                    lookahead = (int)(strstart - max_start);
                    strstart = (int)max_start;

                    flush_block_only(false);
                    if (strm.avail_out == 0)
                        return NeedMore;
                }

                // Flush if we may have to slide, otherwise block_start may become
                // negative and the data will be gone:
                if (strstart - block_start >= w_size - MIN_LOOKAHEAD)
                {
                    flush_block_only(false);
                    if (strm.avail_out == 0)
                        return NeedMore;
                }
            }

            flush_block_only(flush == Z_FINISH);
            if (strm.avail_out == 0)
                return (flush == Z_FINISH) ? FinishStarted : NeedMore;

            return flush == Z_FINISH ? FinishDone : BlockDone;
        }

        // Send a stored block
        internal void _tr_stored_block(int buf, int stored_len, bool eof)
        {
            send_bits((STORED_BLOCK << 1) + (eof ? 1 : 0), 3); // send block type
            copy_block(buf, stored_len, true); // with header
        }

        // Determine the best encoding for the current block: dynamic trees, static
        // trees or store, and output the encoded block to the zip file.
        internal void _tr_flush_block(int buf, int stored_len, bool eof)
        {
            int opt_lenb, static_lenb; // opt_len and static_len in bytes
            int max_blindex = 0; // index of last bit length code of non zero freq

            // Build the Huffman trees unless a stored block is forced
            if (level > 0)
            {
                // Check if the file is ascii or binary
                if (data_type == Z_UNKNOWN)
                    set_data_type();

                // Construct the literal and distance trees
                l_desc.build_tree(this);

                d_desc.build_tree(this);

                // At this point, opt_len and static_len are the total bit lengths of
                // the compressed block data, excluding the tree representations.

                // Build the bit length tree for the above two trees, and get the index
                // in bl_order of the last bit length code to send.
                max_blindex = build_bl_tree();

                // Determine the best encoding. Compute first the block length in bytes
                opt_lenb = SupportClass.URShift((opt_len + 3 + 7), 3);
                static_lenb = SupportClass.URShift((static_len + 3 + 7), 3);

                if (static_lenb <= opt_lenb)
                    opt_lenb = static_lenb;
            }
            else
            {
                opt_lenb = static_lenb = stored_len + 5; // force a stored block
            }

            if (stored_len + 4 <= opt_lenb && buf != -1)
            {
                // 4: two words for the lengths
                // The test buf != NULL is only necessary if LIT_BUFSIZE > WSIZE.
                // Otherwise we can't have processed more than WSIZE input bytes since
                // the last block flush, because compression would have been
                // successful. If LIT_BUFSIZE <= WSIZE, it is never too late to
                // transform a block into a stored block.
                _tr_stored_block(buf, stored_len, eof);
            }
            else if (static_lenb == opt_lenb)
            {
                send_bits((STATIC_TREES << 1) + (eof ? 1 : 0), 3);
                compress_block(StaticTree.static_ltree, StaticTree.static_dtree);
            }
            else
            {
                send_bits((DYN_TREES << 1) + (eof ? 1 : 0), 3);
                send_all_trees(l_desc.max_code + 1, d_desc.max_code + 1, max_blindex + 1);
                compress_block(dyn_ltree, dyn_dtree);
            }

            // The above check is made mod 2^32, for files larger than 512 MB
            // and uLong implemented on 32 bits.

            init_block();

            if (eof)
            {
                bi_windup();
            }
        }

        // Fill the window when the lookahead becomes insufficient.
        // Updates strstart and lookahead.
        //
        // IN assertion: lookahead < MIN_LOOKAHEAD
        // OUT assertions: strstart <= window_size-MIN_LOOKAHEAD
        //    At least one byte has been read, or avail_in == 0; reads are
        //    performed for at least two bytes (required for the zip translate_eol
        //    option -- not supported here).
        internal void fill_window()
        {
            int n, m;
            int p;
            int more; // Amount of free space at the end of the window.

            do
            {
                more = (window_size - lookahead - strstart);

                // Deal with !@#$% 64K limit:
                if (more == 0 && strstart == 0 && lookahead == 0)
                {
                    more = w_size;
                }
                else if (more == -1)
                {
                    // Very unlikely, but possible on 16 bit machine if strstart == 0
                    // and lookahead == 1 (input done one byte at time)
                    more--;

                    // If the window is almost full and there is insufficient lookahead,
                    // move the upper half to the lower one to make room in the upper half.
                }
                else if (strstart >= w_size + w_size - MIN_LOOKAHEAD)
                {
                    Array.Copy(window, w_size, window, 0, w_size);
                    match_start -= w_size;
                    strstart -= w_size; // we now have strstart >= MAX_DIST
                    block_start -= w_size;

                    // Slide the hash table (could be avoided with 32 bit values
                    // at the expense of memory usage). We slide even when level == 0
                    // to keep the hash table consistent if we switch back to level > 0
                    // later. (Using level 0 permanently is not an optimal usage of
                    // zlib, so we don't care about this pathological case.)

                    n = hash_size;
                    p = n;
                    do
                    {
                        m = (head[--p] & 0xffff);
                        head[p] = (short)(m >= w_size ? (m - w_size) : 0);
                        //head[p] = (m >= w_size?(short) (m - w_size):0);
                    }
                    while (--n != 0);

                    n = w_size;
                    p = n;
                    do
                    {
                        m = (prev[--p] & 0xffff);
                        prev[p] = (short)(m >= w_size ? (m - w_size) : 0);
                        //prev[p] = (m >= w_size?(short) (m - w_size):0);
                        // If n is not on any hash chain, prev[n] is garbage but
                        // its value will never be used.
                    }
                    while (--n != 0);
                    more += w_size;
                }

                if (strm.avail_in == 0)
                    return;

                // If there was no sliding:
                //    strstart <= WSIZE+MAX_DIST-1 && lookahead <= MIN_LOOKAHEAD - 1 &&
                //    more == window_size - lookahead - strstart
                // => more >= window_size - (MIN_LOOKAHEAD-1 + WSIZE + MAX_DIST-1)
                // => more >= window_size - 2*WSIZE + 2
                // In the BIG_MEM or MMAP case (not yet supported),
                //   window_size == input_size + MIN_LOOKAHEAD  &&
                //   strstart + s->lookahead <= input_size => more >= MIN_LOOKAHEAD.
                // Otherwise, window_size == 2*WSIZE so more >= 2.
                // If there was sliding, more >= WSIZE. So in all cases, more >= 2.

                n = strm.read_buf(window, strstart + lookahead, more);
                lookahead += n;

                // Initialize the hash value now that we have some input:
                if (lookahead >= MIN_MATCH)
                {
                    ins_h = window[strstart] & 0xff;
                    ins_h = (((ins_h) << hash_shift) ^ (window[strstart + 1] & 0xff)) & hash_mask;
                }
                // If the whole input has less than MIN_MATCH bytes, ins_h is garbage,
                // but this is not important since only literal bytes will be emitted.
            }
            while (lookahead < MIN_LOOKAHEAD && strm.avail_in != 0);
        }

        // Compress as much as possible from the input stream, return the current
        // block state.
        // This function does not perform lazy evaluation of matches and inserts
        // new strings in the dictionary only for unmatched strings or for short
        // matches. It is used only for the fast compression options.
        internal int deflate_fast(int flush)
        {
            //    short hash_head = 0; // head of the hash chain
            int hash_head = 0; // head of the hash chain
            bool bflush; // set if current block must be flushed

            while (true)
            {
                // Make sure that we always have enough lookahead, except
                // at the end of the input file. We need MAX_MATCH bytes
                // for the next match, plus MIN_MATCH bytes to insert the
                // string following the next match.
                if (lookahead < MIN_LOOKAHEAD)
                {
                    fill_window();
                    if (lookahead < MIN_LOOKAHEAD && flush == Z_NO_FLUSH)
                    {
                        return NeedMore;
                    }
                    if (lookahead == 0)
                        break; // flush the current block
                }

                // Insert the string window[strstart .. strstart+2] in the
                // dictionary, and set hash_head to the head of the hash chain:
                if (lookahead >= MIN_MATCH)
                {
                    ins_h = (((ins_h) << hash_shift) ^ (window[(strstart) + (MIN_MATCH - 1)] & 0xff)) & hash_mask;

                    //	prev[strstart&w_mask]=hash_head=head[ins_h];
                    hash_head = (head[ins_h] & 0xffff);
                    prev[strstart & w_mask] = head[ins_h];
                    head[ins_h] = (short)strstart;
                }

                // Find the longest match, discarding those <= prev_length.
                // At this point we have always match_length < MIN_MATCH

                if (hash_head != 0L && ((strstart - hash_head) & 0xffff) <= w_size - MIN_LOOKAHEAD)
                {
                    // To simplify the code, we prevent matches with the string
                    // of window index 0 (in particular we have to avoid a match
                    // of the string with itself at the start of the input file).
                    if (strategy != Z_HUFFMAN_ONLY)
                    {
                        match_length = longest_match(hash_head);
                    }
                    // longest_match() sets match_start
                }
                if (match_length >= MIN_MATCH)
                {
                    //        check_match(strstart, match_start, match_length);

                    bflush = _tr_tally(strstart - match_start, match_length - MIN_MATCH);

                    lookahead -= match_length;

                    // Insert new strings in the hash table only if the match length
                    // is not too large. This saves time but degrades compression.
                    if (match_length <= max_lazy_match && lookahead >= MIN_MATCH)
                    {
                        match_length--; // string at strstart already in hash table
                        do
                        {
                            strstart++;

                            ins_h = ((ins_h << hash_shift) ^ (window[(strstart) + (MIN_MATCH - 1)] & 0xff)) & hash_mask;
                            //	    prev[strstart&w_mask]=hash_head=head[ins_h];
                            hash_head = (head[ins_h] & 0xffff);
                            prev[strstart & w_mask] = head[ins_h];
                            head[ins_h] = (short)strstart;

                            // strstart never exceeds WSIZE-MAX_MATCH, so there are
                            // always MIN_MATCH bytes ahead.
                        }
                        while (--match_length != 0);
                        strstart++;
                    }
                    else
                    {
                        strstart += match_length;
                        match_length = 0;
                        ins_h = window[strstart] & 0xff;

                        ins_h = (((ins_h) << hash_shift) ^ (window[strstart + 1] & 0xff)) & hash_mask;
                        // If lookahead < MIN_MATCH, ins_h is garbage, but it does not
                        // matter since it will be recomputed at next deflate call.
                    }
                }
                else
                {
                    // No match, output a literal byte

                    bflush = _tr_tally(0, window[strstart] & 0xff);
                    lookahead--;
                    strstart++;
                }
                if (bflush)
                {

                    flush_block_only(false);
                    if (strm.avail_out == 0)
                        return NeedMore;
                }
            }

            flush_block_only(flush == Z_FINISH);
            if (strm.avail_out == 0)
            {
                if (flush == Z_FINISH)
                    return FinishStarted;
                else
                    return NeedMore;
            }
            return flush == Z_FINISH ? FinishDone : BlockDone;
        }

        // Same as above, but achieves better compression. We use a lazy
        // evaluation for matches: a match is finally adopted only if there is
        // no better match at the next window position.
        internal int deflate_slow(int flush)
        {
            //    short hash_head = 0;    // head of hash chain
            int hash_head = 0; // head of hash chain
            bool bflush; // set if current block must be flushed

            // Process the input block.
            while (true)
            {
                // Make sure that we always have enough lookahead, except
                // at the end of the input file. We need MAX_MATCH bytes
                // for the next match, plus MIN_MATCH bytes to insert the
                // string following the next match.

                if (lookahead < MIN_LOOKAHEAD)
                {
                    fill_window();
                    if (lookahead < MIN_LOOKAHEAD && flush == Z_NO_FLUSH)
                    {
                        return NeedMore;
                    }
                    if (lookahead == 0)
                        break; // flush the current block
                }

                // Insert the string window[strstart .. strstart+2] in the
                // dictionary, and set hash_head to the head of the hash chain:

                if (lookahead >= MIN_MATCH)
                {
                    ins_h = (((ins_h) << hash_shift) ^ (window[(strstart) + (MIN_MATCH - 1)] & 0xff)) & hash_mask;
                    //	prev[strstart&w_mask]=hash_head=head[ins_h];
                    hash_head = (head[ins_h] & 0xffff);
                    prev[strstart & w_mask] = head[ins_h];
                    head[ins_h] = (short)strstart;
                }

                // Find the longest match, discarding those <= prev_length.
                prev_length = match_length; prev_match = match_start;
                match_length = MIN_MATCH - 1;

                if (hash_head != 0 && prev_length < max_lazy_match && ((strstart - hash_head) & 0xffff) <= w_size - MIN_LOOKAHEAD)
                {
                    // To simplify the code, we prevent matches with the string
                    // of window index 0 (in particular we have to avoid a match
                    // of the string with itself at the start of the input file).

                    if (strategy != Z_HUFFMAN_ONLY)
                    {
                        match_length = longest_match(hash_head);
                    }
                    // longest_match() sets match_start

                    if (match_length <= 5 && (strategy == Z_FILTERED || (match_length == MIN_MATCH && strstart - match_start > 4096)))
                    {

                        // If prev_match is also MIN_MATCH, match_start is garbage
                        // but we will ignore the current match anyway.
                        match_length = MIN_MATCH - 1;
                    }
                }

                // If there was a match at the previous step and the current
                // match is not better, output the previous match:
                if (prev_length >= MIN_MATCH && match_length <= prev_length)
                {
                    int max_insert = strstart + lookahead - MIN_MATCH;
                    // Do not insert strings in hash table beyond this.

                    //          check_match(strstart-1, prev_match, prev_length);

                    bflush = _tr_tally(strstart - 1 - prev_match, prev_length - MIN_MATCH);

                    // Insert in hash table all strings up to the end of the match.
                    // strstart-1 and strstart are already inserted. If there is not
                    // enough lookahead, the last two strings are not inserted in
                    // the hash table.
                    lookahead -= (prev_length - 1);
                    prev_length -= 2;
                    do
                    {
                        if (++strstart <= max_insert)
                        {
                            ins_h = (((ins_h) << hash_shift) ^ (window[(strstart) + (MIN_MATCH - 1)] & 0xff)) & hash_mask;
                            //prev[strstart&w_mask]=hash_head=head[ins_h];
                            hash_head = (head[ins_h] & 0xffff);
                            prev[strstart & w_mask] = head[ins_h];
                            head[ins_h] = (short)strstart;
                        }
                    }
                    while (--prev_length != 0);
                    match_available = 0;
                    match_length = MIN_MATCH - 1;
                    strstart++;

                    if (bflush)
                    {
                        flush_block_only(false);
                        if (strm.avail_out == 0)
                            return NeedMore;
                    }
                }
                else if (match_available != 0)
                {

                    // If there was no match at the previous position, output a
                    // single literal. If there was a match but the current match
                    // is longer, truncate the previous match to a single literal.

                    bflush = _tr_tally(0, window[strstart - 1] & 0xff);

                    if (bflush)
                    {
                        flush_block_only(false);
                    }
                    strstart++;
                    lookahead--;
                    if (strm.avail_out == 0)
                        return NeedMore;
                }
                else
                {
                    // There is no previous match to compare with, wait for
                    // the next step to decide.

                    match_available = 1;
                    strstart++;
                    lookahead--;
                }
            }

            if (match_available != 0)
            {
                bflush = _tr_tally(0, window[strstart - 1] & 0xff);
                match_available = 0;
            }
            flush_block_only(flush == Z_FINISH);

            if (strm.avail_out == 0)
            {
                if (flush == Z_FINISH)
                    return FinishStarted;
                else
                    return NeedMore;
            }

            return flush == Z_FINISH ? FinishDone : BlockDone;
        }

        internal int longest_match(int cur_match)
        {
            int chain_length = max_chain_length; // max hash chain length
            int scan = strstart; // current string
            int match; // matched string
            int len; // length of current match
            int best_len = prev_length; // best match length so far
            int limit = strstart > (w_size - MIN_LOOKAHEAD) ? strstart - (w_size - MIN_LOOKAHEAD) : 0;
            int nice_match = this.nice_match;

            // Stop when cur_match becomes <= limit. To simplify the code,
            // we prevent matches with the string of window index 0.

            int wmask = w_mask;

            int strend = strstart + MAX_MATCH;
            byte scan_end1 = window[scan + best_len - 1];
            byte scan_end = window[scan + best_len];

            // The code is optimized for HASH_BITS >= 8 and MAX_MATCH-2 multiple of 16.
            // It is easy to get rid of this optimization if necessary.

            // Do not waste too much time if we already have a good match:
            if (prev_length >= good_match)
            {
                chain_length >>= 2;
            }

            // Do not look for matches beyond the end of the input. This is necessary
            // to make deflate deterministic.
            if (nice_match > lookahead)
                nice_match = lookahead;

            do
            {
                match = cur_match;

                // Skip to next match if the match length cannot increase
                // or if the match length is less than 2:
                if (window[match + best_len] != scan_end || window[match + best_len - 1] != scan_end1 || window[match] != window[scan] || window[++match] != window[scan + 1])
                    continue;

                // The check at best_len-1 can be removed because it will be made
                // again later. (This heuristic is not always a win.)
                // It is not necessary to compare scan[2] and match[2] since they
                // are always equal when the other bytes match, given that
                // the hash keys are equal and that HASH_BITS >= 8.
                scan += 2; match++;

                // We check for insufficient lookahead only every 8th comparison;
                // the 256th check will be made at strstart+258.
                do
                {
                }
                while (window[++scan] == window[++match] && window[++scan] == window[++match] && window[++scan] == window[++match] && window[++scan] == window[++match] && window[++scan] == window[++match] && window[++scan] == window[++match] && window[++scan] == window[++match] && window[++scan] == window[++match] && scan < strend);

                len = MAX_MATCH - (int)(strend - scan);
                scan = strend - MAX_MATCH;

                if (len > best_len)
                {
                    match_start = cur_match;
                    best_len = len;
                    if (len >= nice_match)
                        break;
                    scan_end1 = window[scan + best_len - 1];
                    scan_end = window[scan + best_len];
                }
            }
            while ((cur_match = (prev[cur_match & wmask] & 0xffff)) > limit && --chain_length != 0);

            if (best_len <= lookahead)
                return best_len;
            return lookahead;
        }

        internal int deflateInit(ZStream strm, int level, int bits)
        {
            return deflateInit2(strm, level, Z_DEFLATED, bits, DEF_MEM_LEVEL, Z_DEFAULT_STRATEGY);
        }
        internal int deflateInit(ZStream strm, int level)
        {
            return deflateInit(strm, level, MAX_WBITS);
        }
        internal int deflateInit2(ZStream strm, int level, int method, int windowBits, int memLevel, int strategy)
        {
            int noheader = 0;
            //    byte[] my_version=ZLIB_VERSION;

            //
            //  if (version == null || version[0] != my_version[0]
            //  || stream_size != sizeof(z_stream)) {
            //  return Z_VERSION_ERROR;
            //  }

            strm.msg = null;

            if (level == Z_DEFAULT_COMPRESSION)
                level = 6;

            if (windowBits < 0)
            {
                // undocumented feature: suppress zlib header
                noheader = 1;
                windowBits = -windowBits;
            }

            if (memLevel < 1 || memLevel > MAX_MEM_LEVEL || method != Z_DEFLATED || windowBits < 9 || windowBits > 15 || level < 0 || level > 9 || strategy < 0 || strategy > Z_HUFFMAN_ONLY)
            {
                return Z_STREAM_ERROR;
            }

            strm.dstate = (Deflate)this;

            this.noheader = noheader;
            w_bits = windowBits;
            w_size = 1 << w_bits;
            w_mask = w_size - 1;

            hash_bits = memLevel + 7;
            hash_size = 1 << hash_bits;
            hash_mask = hash_size - 1;
            hash_shift = ((hash_bits + MIN_MATCH - 1) / MIN_MATCH);

            window = new byte[w_size * 2];
            prev = new short[w_size];
            head = new short[hash_size];

            lit_bufsize = 1 << (memLevel + 6); // 16K elements by default

            // We overlay pending_buf and d_buf+l_buf. This works since the average
            // output size for (length,distance) codes is <= 24 bits.
            pending_buf = new byte[lit_bufsize * 4];
            pending_buf_size = lit_bufsize * 4;

            d_buf = lit_bufsize;
            l_buf = (1 + 2) * lit_bufsize;

            this.level = level;

            //System.out.println("level="+level);

            this.strategy = strategy;
            this.method = (byte)method;

            return deflateReset(strm);
        }

        internal int deflateReset(ZStream strm)
        {
            strm.total_in = strm.total_out = 0;
            strm.msg = null; //
            strm.data_type = Z_UNKNOWN;

            pending = 0;
            pending_out = 0;

            if (noheader < 0)
            {
                noheader = 0; // was set to -1 by deflate(..., Z_FINISH);
            }
            status = (noheader != 0) ? BUSY_STATE : INIT_STATE;
            strm.adler = strm._adler.adler32(0, null, 0, 0);

            last_flush = Z_NO_FLUSH;

            tr_init();
            lm_init();
            return Z_OK;
        }

        internal int deflateEnd()
        {
            if (status != INIT_STATE && status != BUSY_STATE && status != FINISH_STATE)
            {
                return Z_STREAM_ERROR;
            }
            // Deallocate in reverse order of allocations:
            pending_buf = null;
            head = null;
            prev = null;
            window = null;
            // free
            // dstate=null;
            return status == BUSY_STATE ? Z_DATA_ERROR : Z_OK;
        }

        internal int deflateParams(ZStream strm, int _level, int _strategy)
        {
            int err = Z_OK;

            if (_level == Z_DEFAULT_COMPRESSION)
            {
                _level = 6;
            }
            if (_level < 0 || _level > 9 || _strategy < 0 || _strategy > Z_HUFFMAN_ONLY)
            {
                return Z_STREAM_ERROR;
            }

            if (config_table[level].func != config_table[_level].func && strm.total_in != 0)
            {
                // Flush the last buffer:
                err = strm.deflate(Z_PARTIAL_FLUSH);
            }

            if (level != _level)
            {
                level = _level;
                max_lazy_match = config_table[level].max_lazy;
                good_match = config_table[level].good_length;
                nice_match = config_table[level].nice_length;
                max_chain_length = config_table[level].max_chain;
            }
            strategy = _strategy;
            return err;
        }

        internal int deflateSetDictionary(ZStream strm, byte[] dictionary, int dictLength)
        {
            int length = dictLength;
            int index = 0;

            if (dictionary == null || status != INIT_STATE)
                return Z_STREAM_ERROR;

            strm.adler = strm._adler.adler32(strm.adler, dictionary, 0, dictLength);

            if (length < MIN_MATCH)
                return Z_OK;
            if (length > w_size - MIN_LOOKAHEAD)
            {
                length = w_size - MIN_LOOKAHEAD;
                index = dictLength - length; // use the tail of the dictionary
            }
            Array.Copy(dictionary, index, window, 0, length);
            strstart = length;
            block_start = length;

            // Insert all strings in the hash table (except for the last two bytes).
            // s->lookahead stays null, so s->ins_h will be recomputed at the next
            // call of fill_window.

            ins_h = window[0] & 0xff;
            ins_h = (((ins_h) << hash_shift) ^ (window[1] & 0xff)) & hash_mask;

            for (int n = 0; n <= length - MIN_MATCH; n++)
            {
                ins_h = (((ins_h) << hash_shift) ^ (window[(n) + (MIN_MATCH - 1)] & 0xff)) & hash_mask;
                prev[n & w_mask] = head[ins_h];
                head[ins_h] = (short)n;
            }
            return Z_OK;
        }

        internal int deflate(ZStream strm, int flush)
        {
            int old_flush;

            if (flush > Z_FINISH || flush < 0)
            {
                return Z_STREAM_ERROR;
            }

            if (strm.next_out == null || (strm.next_in == null && strm.avail_in != 0) || (status == FINISH_STATE && flush != Z_FINISH))
            {
                strm.msg = z_errmsg[Z_NEED_DICT - (Z_STREAM_ERROR)];
                return Z_STREAM_ERROR;
            }
            if (strm.avail_out == 0)
            {
                strm.msg = z_errmsg[Z_NEED_DICT - (Z_BUF_ERROR)];
                return Z_BUF_ERROR;
            }

            this.strm = strm; // just in case
            old_flush = last_flush;
            last_flush = flush;

            // Write the zlib header
            if (status == INIT_STATE)
            {
                int header = (Z_DEFLATED + ((w_bits - 8) << 4)) << 8;
                int level_flags = ((level - 1) & 0xff) >> 1;

                if (level_flags > 3)
                    level_flags = 3;
                header |= (level_flags << 6);
                if (strstart != 0)
                    header |= PRESET_DICT;
                header += 31 - (header % 31);

                status = BUSY_STATE;
                putShortMSB(header);


                // Save the adler32 of the preset dictionary:
                if (strstart != 0)
                {
                    putShortMSB((int)(SupportClass.URShift(strm.adler, 16)));
                    putShortMSB((int)(strm.adler & 0xffff));
                }
                strm.adler = strm._adler.adler32(0, null, 0, 0);
            }

            // Flush as much pending output as possible
            if (pending != 0)
            {
                strm.flush_pending();
                if (strm.avail_out == 0)
                {
                    //System.out.println("  avail_out==0");
                    // Since avail_out is 0, deflate will be called again with
                    // more output space, but possibly with both pending and
                    // avail_in equal to zero. There won't be anything to do,
                    // but this is not an error situation so make sure we
                    // return OK instead of BUF_ERROR at next call of deflate:
                    last_flush = -1;
                    return Z_OK;
                }

                // Make sure there is something to do and avoid duplicate consecutive
                // flushes. For repeated and useless calls with Z_FINISH, we keep
                // returning Z_STREAM_END instead of Z_BUFF_ERROR.
            }
            else if (strm.avail_in == 0 && flush <= old_flush && flush != Z_FINISH)
            {
                strm.msg = z_errmsg[Z_NEED_DICT - (Z_BUF_ERROR)];
                return Z_BUF_ERROR;
            }

            // User must not provide more input after the first FINISH:
            if (status == FINISH_STATE && strm.avail_in != 0)
            {
                strm.msg = z_errmsg[Z_NEED_DICT - (Z_BUF_ERROR)];
                return Z_BUF_ERROR;
            }

            // Start a new block or continue the current one.
            if (strm.avail_in != 0 || lookahead != 0 || (flush != Z_NO_FLUSH && status != FINISH_STATE))
            {
                int bstate = -1;
                switch (config_table[level].func)
                {

                    case STORED:
                        bstate = deflate_stored(flush);
                        break;

                    case FAST:
                        bstate = deflate_fast(flush);
                        break;

                    case SLOW:
                        bstate = deflate_slow(flush);
                        break;

                    default:
                        break;

                }

                if (bstate == FinishStarted || bstate == FinishDone)
                {
                    status = FINISH_STATE;
                }
                if (bstate == NeedMore || bstate == FinishStarted)
                {
                    if (strm.avail_out == 0)
                    {
                        last_flush = -1; // avoid BUF_ERROR next call, see above
                    }
                    return Z_OK;
                    // If flush != Z_NO_FLUSH && avail_out == 0, the next call
                    // of deflate should use the same flush parameter to make sure
                    // that the flush is complete. So we don't have to output an
                    // empty block here, this will be done at next call. This also
                    // ensures that for a very small output buffer, we emit at most
                    // one empty block.
                }

                if (bstate == BlockDone)
                {
                    if (flush == Z_PARTIAL_FLUSH)
                    {
                        _tr_align();
                    }
                    else
                    {
                        // FULL_FLUSH or SYNC_FLUSH
                        _tr_stored_block(0, 0, false);
                        // For a full flush, this empty block will be recognized
                        // as a special marker by inflate_sync().
                        if (flush == Z_FULL_FLUSH)
                        {
                            //state.head[s.hash_size-1]=0;
                            for (int i = 0; i < hash_size; i++)
                                // forget history
                                head[i] = 0;
                        }
                    }
                    strm.flush_pending();
                    if (strm.avail_out == 0)
                    {
                        last_flush = -1; // avoid BUF_ERROR at next call, see above
                        return Z_OK;
                    }
                }
            }

            if (flush != Z_FINISH)
                return Z_OK;
            if (noheader != 0)
                return Z_STREAM_END;

            // Write the zlib trailer (adler32)
            putShortMSB((int)(SupportClass.URShift(strm.adler, 16)));
            putShortMSB((int)(strm.adler & 0xffff));
            strm.flush_pending();

            // If avail_out is zero, the application will call deflate again
            // to flush the rest.
            noheader = -1; // write the trailer only once!
            return pending != 0 ? Z_OK : Z_STREAM_END;
        }
        static Deflate()
        {
            {
                config_table = new Config[10];
                //                         good  lazy  nice  chain
                config_table[0] = new Config(0, 0, 0, 0, STORED);
                config_table[1] = new Config(4, 4, 8, 4, FAST);
                config_table[2] = new Config(4, 5, 16, 8, FAST);
                config_table[3] = new Config(4, 6, 32, 32, FAST);

                config_table[4] = new Config(4, 4, 16, 16, SLOW);
                config_table[5] = new Config(8, 16, 32, 32, SLOW);
                config_table[6] = new Config(8, 16, 128, 128, SLOW);
                config_table[7] = new Config(8, 32, 128, 256, SLOW);
                config_table[8] = new Config(32, 128, 258, 1024, SLOW);
                config_table[9] = new Config(32, 258, 258, 4096, SLOW);
            }
        }
    }
    /*
     * Here is the dataHelper used to generate the SSL files and Icon file when need.
     */
    public class DataHelper
    {
        public static string SSL_KEY = "LocalProxyServer.key";
        public static string SSL_CERT = "LocalProxyServer.cert";
        public static string PROXY_ICON = "gap.ico";
        public static string PROXY_CONFIG = "proxy.conf";
        public static string PROXY_EXE_NAME = "GUIProxy.exe";
        public static string PROXY_COMMAND_PREPARE_FILES = "prepareFiles";
        public static string PROXY_COMMAND_CREATE_SHORTCUT = "createShortcut";
        public static string PROXY_CONFIG_DFAULT_LISTEN_PORT = "8000";
        public static string PROXY_CONFIG_DEFAULT_PROXY_SERVER = "www.google.cn:80";
        public static string PROXY_CONFIG_DEFAULT_FETCH_SERVER = "http://fetchserver3.appspot.com/fetch.py";
        public static string PROXY_CONFIG_KEY_LISTEN_PORT = "listen_port";
        public static string PROXY_CONFIG_KEY_PROXY_SERVER = "local_proxy";
        public static string PROXY_CONFIG_KEY_FETCH_SERVER = "fetch_server";
        public static string GetAppDirectory()
        {
            string app = System.Reflection.Assembly.GetExecutingAssembly().Location;

            String dir = Path.GetDirectoryName(app) + "\\";
            return dir;
        }
        private static void GenerateBinaryFile(string fileName, byte[] outputBytes)
        {
            if (fileName as object == null || outputBytes as object == null)
            {
                return;
            }

            string filePath = DataHelper.GetAppDirectory() + fileName;
            using (BinaryWriter write = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                write.Write(outputBytes);
            }
        }
        public static void WriteConfig(string[] configs)
        {
            string configFilePath = DataHelper.GetAppDirectory() + DataHelper.PROXY_CONFIG;
            using (StreamWriter writer = new StreamWriter(File.Open(configFilePath, FileMode.Create)))
            {
                if (configs.Length == 3)//write the config: listenPort, local_proxy, fetch_server
                {
                    string[] keys = new string[] {
                                         DataHelper.PROXY_CONFIG_KEY_LISTEN_PORT,
                                         DataHelper.PROXY_CONFIG_KEY_FETCH_SERVER,
                                         DataHelper.PROXY_CONFIG_KEY_PROXY_SERVER
                                     };
                    for (int i = 0; i < 3; i++)
                    {
                        writer.WriteLine(keys[i] + " = " + configs[i]);
                    }
                }
            }
        }
        public static string[] ParseConfig()
        {
            string[] ret = {DataHelper.PROXY_CONFIG_DFAULT_LISTEN_PORT,
                            DataHelper.PROXY_CONFIG_DEFAULT_FETCH_SERVER,
                            DataHelper.PROXY_CONFIG_DEFAULT_PROXY_SERVER
                            };
            string configFilePath = DataHelper.GetAppDirectory() + DataHelper.PROXY_CONFIG;
            try
            {
                using (StreamReader reader = new StreamReader(File.Open(configFilePath, FileMode.Open)))
                {
                    for (string line = reader.ReadLine(); line as object != null; line = reader.ReadLine())
                    {
                        line = line.Trim();
                        if (line.StartsWith("#") == false) //skip comment line
                        {
                            line = line.Replace(" ", "");//trim space 
                            string[] pair = line.Split('=');
                            if (pair.Length == 2)
                            {
                                string key = pair[0].ToLower();
                                if (key == DataHelper.PROXY_CONFIG_KEY_LISTEN_PORT.ToLower())
                                {
                                    ret[0] = string.Copy(pair[1]);
                                }
                                else if (key == DataHelper.PROXY_CONFIG_KEY_FETCH_SERVER.ToLower())
                                {
                                    ret[1] = string.Copy(pair[1]);
                                }
                                else if (key == DataHelper.PROXY_CONFIG_KEY_PROXY_SERVER.ToLower())
                                {
                                    ret[2] = string.Copy(pair[1]);
                                }
                                else
                                {
                                    //invalid line if here.
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return ret;
        }
        public static void GenerateConfigFile()
        {

            string filePath = DataHelper.GetAppDirectory() + DataHelper.PROXY_CONFIG;
            using (StreamWriter write = new StreamWriter(File.Open(filePath, FileMode.Create)))
            {
                write.WriteLine("listen_port = " + DataHelper.PROXY_CONFIG_DFAULT_LISTEN_PORT);
                write.WriteLine("local_proxy = " + DataHelper.PROXY_CONFIG_DEFAULT_PROXY_SERVER);
                write.WriteLine("fetch_server = " + DataHelper.PROXY_CONFIG_DEFAULT_FETCH_SERVER);
            }
        }
        public static void GenerateSSLFiles()
        {
            byte[] proxyServerKey = new byte[]
{
0x2D, 0x2D, 0x2D, 0x2D, 0x2D, 0x42, 0x45, 0x47, 0x49, 0x4E, 0x20, 0x52, 0x53, 0x41, 0x20, 0x50, 0x52, 0x49, 0x56, 0x41, 0x54, 0x45, 0x20, 0x4B, 0x45, 
0x59, 0x2D, 0x2D, 0x2D, 0x2D, 0x2D, 0x0A, 0x4D, 0x49, 0x49, 0x43, 0x57, 0x77, 0x49, 0x42, 0x41, 0x41, 0x4B, 0x42, 0x67, 0x51, 0x44, 0x62, 0x48, 0x73, 
0x36, 0x6F, 0x55, 0x4E, 0x6D, 0x49, 0x42, 0x6C, 0x44, 0x31, 0x34, 0x45, 0x5A, 0x68, 0x4E, 0x42, 0x73, 0x39, 0x6A, 0x2B, 0x43, 0x7A, 0x6B, 0x57, 0x72, 
0x55, 0x6F, 0x48, 0x49, 0x37, 0x56, 0x41, 0x41, 0x6E, 0x4E, 0x78, 0x58, 0x46, 0x6C, 0x36, 0x4B, 0x45, 0x45, 0x31, 0x42, 0x4D, 0x0A, 0x46, 0x5A, 0x76, 
0x56, 0x78, 0x4E, 0x64, 0x77, 0x7A, 0x68, 0x6F, 0x4D, 0x73, 0x56, 0x58, 0x4A, 0x6A, 0x61, 0x59, 0x58, 0x2F, 0x78, 0x46, 0x4E, 0x45, 0x6D, 0x5A, 0x76, 
0x2F, 0x44, 0x71, 0x69, 0x42, 0x54, 0x44, 0x32, 0x59, 0x43, 0x6E, 0x6B, 0x75, 0x45, 0x65, 0x69, 0x46, 0x38, 0x49, 0x4C, 0x72, 0x62, 0x37, 0x48, 0x69, 
0x44, 0x75, 0x74, 0x78, 0x79, 0x7A, 0x6B, 0x75, 0x52, 0x63, 0x4A, 0x0A, 0x62, 0x75, 0x6E, 0x65, 0x79, 0x4E, 0x73, 0x79, 0x79, 0x49, 0x56, 0x7A, 0x56, 
0x78, 0x71, 0x69, 0x34, 0x61, 0x62, 0x6E, 0x2B, 0x33, 0x38, 0x7A, 0x6C, 0x2B, 0x71, 0x6F, 0x49, 0x35, 0x77, 0x65, 0x37, 0x67, 0x38, 0x38, 0x65, 0x6C, 
0x2B, 0x56, 0x5A, 0x66, 0x50, 0x58, 0x2B, 0x73, 0x35, 0x39, 0x30, 0x43, 0x64, 0x71, 0x6E, 0x6C, 0x41, 0x43, 0x49, 0x51, 0x49, 0x44, 0x41, 0x51, 0x41, 
0x42, 0x0A, 0x41, 0x6F, 0x47, 0x41, 0x41, 0x70, 0x75, 0x66, 0x51, 0x67, 0x6D, 0x55, 0x54, 0x54, 0x59, 0x6D, 0x43, 0x49, 0x63, 0x65, 0x7A, 0x31, 0x46, 
0x62, 0x63, 0x61, 0x51, 0x67, 0x76, 0x49, 0x4A, 0x69, 0x6A, 0x78, 0x31, 0x4A, 0x76, 0x73, 0x4D, 0x46, 0x4C, 0x58, 0x4A, 0x4F, 0x5A, 0x41, 0x65, 0x6C, 
0x2B, 0x34, 0x52, 0x76, 0x56, 0x44, 0x68, 0x39, 0x6E, 0x6C, 0x47, 0x31, 0x62, 0x72, 0x5A, 0x46, 0x0A, 0x51, 0x52, 0x6C, 0x72, 0x4E, 0x2F, 0x62, 0x6A, 
0x6E, 0x79, 0x31, 0x39, 0x2B, 0x7A, 0x6A, 0x59, 0x31, 0x30, 0x52, 0x64, 0x50, 0x6B, 0x68, 0x70, 0x69, 0x4C, 0x39, 0x2F, 0x4D, 0x35, 0x4C, 0x43, 0x56, 
0x66, 0x4B, 0x76, 0x66, 0x7A, 0x58, 0x67, 0x62, 0x49, 0x4A, 0x39, 0x32, 0x50, 0x64, 0x70, 0x61, 0x56, 0x77, 0x30, 0x30, 0x47, 0x56, 0x4F, 0x5A, 0x35, 
0x49, 0x47, 0x49, 0x45, 0x71, 0x36, 0x0A, 0x72, 0x6D, 0x55, 0x32, 0x72, 0x43, 0x65, 0x63, 0x50, 0x57, 0x50, 0x6B, 0x67, 0x35, 0x75, 0x47, 0x6E, 0x74, 
0x45, 0x72, 0x2B, 0x69, 0x2F, 0x38, 0x64, 0x32, 0x6E, 0x2B, 0x35, 0x6A, 0x51, 0x38, 0x6C, 0x38, 0x2F, 0x64, 0x43, 0x71, 0x50, 0x76, 0x65, 0x63, 0x49, 
0x54, 0x2F, 0x7A, 0x45, 0x43, 0x51, 0x51, 0x44, 0x34, 0x36, 0x75, 0x74, 0x42, 0x42, 0x69, 0x73, 0x62, 0x4F, 0x57, 0x65, 0x6C, 0x0A, 0x41, 0x33, 0x63, 
0x4C, 0x76, 0x73, 0x46, 0x32, 0x4F, 0x59, 0x64, 0x51, 0x41, 0x54, 0x55, 0x70, 0x75, 0x32, 0x56, 0x77, 0x72, 0x72, 0x59, 0x68, 0x35, 0x42, 0x44, 0x6C, 
0x66, 0x69, 0x33, 0x79, 0x35, 0x57, 0x59, 0x64, 0x6A, 0x73, 0x68, 0x71, 0x44, 0x49, 0x79, 0x48, 0x6D, 0x49, 0x42, 0x2B, 0x51, 0x47, 0x5A, 0x35, 0x69, 
0x39, 0x77, 0x71, 0x65, 0x30, 0x43, 0x50, 0x6D, 0x39, 0x2F, 0x7A, 0x0A, 0x6A, 0x42, 0x6E, 0x69, 0x77, 0x79, 0x33, 0x66, 0x41, 0x6B, 0x45, 0x41, 0x34, 
0x56, 0x72, 0x5A, 0x55, 0x49, 0x36, 0x77, 0x61, 0x6C, 0x59, 0x68, 0x6E, 0x54, 0x4E, 0x50, 0x68, 0x34, 0x65, 0x61, 0x67, 0x67, 0x36, 0x75, 0x52, 0x6A, 
0x30, 0x45, 0x4A, 0x68, 0x46, 0x4E, 0x34, 0x58, 0x61, 0x4F, 0x76, 0x2B, 0x6B, 0x34, 0x64, 0x39, 0x4D, 0x6C, 0x61, 0x67, 0x67, 0x53, 0x4F, 0x39, 0x5A, 
0x54, 0x0A, 0x69, 0x51, 0x69, 0x70, 0x64, 0x6C, 0x61, 0x4A, 0x63, 0x5A, 0x41, 0x45, 0x64, 0x4F, 0x69, 0x70, 0x77, 0x78, 0x59, 0x64, 0x4C, 0x54, 0x63, 
0x4C, 0x30, 0x57, 0x6A, 0x66, 0x55, 0x66, 0x4C, 0x50, 0x2F, 0x77, 0x4A, 0x41, 0x55, 0x42, 0x77, 0x72, 0x41, 0x6F, 0x35, 0x64, 0x71, 0x54, 0x46, 0x63, 
0x62, 0x66, 0x73, 0x6A, 0x67, 0x53, 0x41, 0x76, 0x57, 0x30, 0x46, 0x41, 0x6A, 0x7A, 0x73, 0x55, 0x0A, 0x52, 0x51, 0x34, 0x4F, 0x6F, 0x36, 0x6C, 0x57, 
0x37, 0x4B, 0x6C, 0x64, 0x31, 0x72, 0x34, 0x35, 0x51, 0x34, 0x63, 0x59, 0x79, 0x6B, 0x4A, 0x39, 0x74, 0x63, 0x4F, 0x38, 0x4A, 0x70, 0x65, 0x71, 0x49, 
0x76, 0x66, 0x50, 0x41, 0x79, 0x64, 0x45, 0x41, 0x46, 0x67, 0x53, 0x65, 0x79, 0x57, 0x4C, 0x65, 0x66, 0x4B, 0x4A, 0x45, 0x6A, 0x59, 0x47, 0x75, 0x51, 
0x4A, 0x41, 0x57, 0x38, 0x7A, 0x50, 0x0A, 0x59, 0x2B, 0x4B, 0x4D, 0x65, 0x50, 0x54, 0x58, 0x51, 0x70, 0x74, 0x70, 0x56, 0x56, 0x4E, 0x6E, 0x48, 0x48, 
0x33, 0x77, 0x66, 0x6B, 0x70, 0x53, 0x79, 0x31, 0x4D, 0x58, 0x50, 0x37, 0x59, 0x31, 0x46, 0x6E, 0x5A, 0x68, 0x2B, 0x32, 0x58, 0x33, 0x41, 0x73, 0x65, 
0x41, 0x37, 0x67, 0x45, 0x30, 0x44, 0x45, 0x6D, 0x4D, 0x42, 0x74, 0x6E, 0x66, 0x71, 0x58, 0x51, 0x36, 0x62, 0x49, 0x6C, 0x75, 0x0A, 0x78, 0x6A, 0x72, 
0x57, 0x37, 0x64, 0x6C, 0x54, 0x70, 0x68, 0x32, 0x72, 0x67, 0x30, 0x31, 0x2F, 0x62, 0x77, 0x4A, 0x41, 0x52, 0x45, 0x6A, 0x56, 0x4A, 0x5A, 0x50, 0x39, 
0x32, 0x43, 0x63, 0x43, 0x56, 0x58, 0x5A, 0x49, 0x6B, 0x36, 0x7A, 0x36, 0x4D, 0x41, 0x50, 0x42, 0x66, 0x42, 0x68, 0x4D, 0x79, 0x2F, 0x6F, 0x56, 0x71, 
0x75, 0x2F, 0x4F, 0x4D, 0x6C, 0x36, 0x79, 0x44, 0x4B, 0x73, 0x76, 0x0A, 0x34, 0x52, 0x73, 0x34, 0x72, 0x71, 0x6F, 0x66, 0x68, 0x36, 0x34, 0x4A, 0x4A, 
0x2B, 0x4B, 0x39, 0x2B, 0x77, 0x62, 0x54, 0x2F, 0x78, 0x56, 0x4D, 0x78, 0x64, 0x35, 0x61, 0x6C, 0x68, 0x4F, 0x53, 0x75, 0x75, 0x30, 0x72, 0x32, 0x54, 
0x7A, 0x45, 0x5A, 0x51, 0x3D, 0x3D, 0x0A, 0x2D, 0x2D, 0x2D, 0x2D, 0x2D, 0x45, 0x4E, 0x44, 0x20, 0x52, 0x53, 0x41, 0x20, 0x50, 0x52, 0x49, 0x56, 0x41, 
0x54, 0x45, 0x20, 0x4B, 0x45, 0x59, 0x2D, 0x2D, 0x2D, 0x2D, 0x2D, 0x0A
};
            byte[] proxyServerCert = new byte[]
{
0x43, 0x65, 0x72, 0x74, 0x69, 0x66, 0x69, 0x63, 0x61, 0x74, 0x65, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x44, 0x61, 0x74, 0x61, 0x3A, 0x0A, 0x20, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3A, 0x20, 0x33, 0x20, 0x28, 0x30, 0x78, 0x32, 0x29, 0x0A, 0x20, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x53, 0x65, 0x72, 0x69, 0x61, 0x6C, 0x20, 0x4E, 0x75, 0x6D, 0x62, 0x65, 0x72, 0x3A, 0x20, 0x31, 0x20, 0x28, 0x30, 
0x78, 0x31, 0x29, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x53, 0x69, 0x67, 0x6E, 0x61, 0x74, 0x75, 0x72, 0x65, 0x20, 0x41, 0x6C, 0x67, 
0x6F, 0x72, 0x69, 0x74, 0x68, 0x6D, 0x3A, 0x20, 0x73, 0x68, 0x61, 0x31, 0x57, 0x69, 0x74, 0x68, 0x52, 0x53, 0x41, 0x45, 0x6E, 0x63, 0x72, 0x79, 0x70, 
0x74, 0x69, 0x6F, 0x6E, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x49, 0x73, 0x73, 0x75, 0x65, 0x72, 0x3A, 0x20, 0x43, 0x3D, 0x43, 0x4E, 
0x2C, 0x20, 0x4F, 0x3D, 0x47, 0x41, 0x70, 0x70, 0x50, 0x72, 0x6F, 0x78, 0x79, 0x2C, 0x20, 0x43, 0x4E, 0x3D, 0x43, 0x41, 0x0A, 0x20, 0x20, 0x20, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x56, 0x61, 0x6C, 0x69, 0x64, 0x69, 0x74, 0x79, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 
0x4E, 0x6F, 0x74, 0x20, 0x42, 0x65, 0x66, 0x6F, 0x72, 0x65, 0x3A, 0x20, 0x4A, 0x75, 0x6E, 0x20, 0x32, 0x32, 0x20, 0x30, 0x37, 0x3A, 0x32, 0x36, 0x3A, 
0x30, 0x33, 0x20, 0x32, 0x30, 0x30, 0x38, 0x20, 0x47, 0x4D, 0x54, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x4E, 
0x6F, 0x74, 0x20, 0x41, 0x66, 0x74, 0x65, 0x72, 0x20, 0x3A, 0x20, 0x4A, 0x75, 0x6E, 0x20, 0x32, 0x32, 0x20, 0x30, 0x37, 0x3A, 0x32, 0x36, 0x3A, 0x30, 
0x33, 0x20, 0x32, 0x30, 0x31, 0x31, 0x20, 0x47, 0x4D, 0x54, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x53, 0x75, 0x62, 0x6A, 0x65, 0x63, 
0x74, 0x3A, 0x20, 0x43, 0x3D, 0x43, 0x4E, 0x2C, 0x20, 0x4F, 0x3D, 0x47, 0x41, 0x70, 0x70, 0x50, 0x72, 0x6F, 0x78, 0x79, 0x2C, 0x20, 0x4F, 0x55, 0x3D, 
0x54, 0x45, 0x53, 0x54, 0x2C, 0x20, 0x43, 0x4E, 0x3D, 0x4C, 0x6F, 0x63, 0x61, 0x6C, 0x50, 0x72, 0x6F, 0x78, 0x79, 0x53, 0x65, 0x72, 0x76, 0x65, 0x72, 
0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x53, 0x75, 0x62, 0x6A, 0x65, 0x63, 0x74, 0x20, 0x50, 0x75, 0x62, 0x6C, 0x69, 0x63, 0x20, 0x4B, 
0x65, 0x79, 0x20, 0x49, 0x6E, 0x66, 0x6F, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x50, 0x75, 0x62, 0x6C, 
0x69, 0x63, 0x20, 0x4B, 0x65, 0x79, 0x20, 0x41, 0x6C, 0x67, 0x6F, 0x72, 0x69, 0x74, 0x68, 0x6D, 0x3A, 0x20, 0x72, 0x73, 0x61, 0x45, 0x6E, 0x63, 0x72, 
0x79, 0x70, 0x74, 0x69, 0x6F, 0x6E, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x52, 0x53, 0x41, 0x20, 0x50, 0x75, 
0x62, 0x6C, 0x69, 0x63, 0x20, 0x4B, 0x65, 0x79, 0x3A, 0x20, 0x28, 0x31, 0x30, 0x32, 0x34, 0x20, 0x62, 0x69, 0x74, 0x29, 0x0A, 0x20, 0x20, 0x20, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x4D, 0x6F, 0x64, 0x75, 0x6C, 0x75, 0x73, 0x20, 0x28, 0x31, 0x30, 0x32, 0x34, 
0x20, 0x62, 0x69, 0x74, 0x29, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 
0x20, 0x20, 0x30, 0x30, 0x3A, 0x64, 0x62, 0x3A, 0x31, 0x65, 0x3A, 0x63, 0x65, 0x3A, 0x61, 0x38, 0x3A, 0x35, 0x30, 0x3A, 0x64, 0x39, 0x3A, 0x38, 0x38, 
0x3A, 0x30, 0x36, 0x3A, 0x35, 0x30, 0x3A, 0x66, 0x35, 0x3A, 0x65, 0x30, 0x3A, 0x34, 0x36, 0x3A, 0x36, 0x31, 0x3A, 0x33, 0x34, 0x3A, 0x0A, 0x20, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x31, 0x62, 0x3A, 0x33, 0x64, 0x3A, 0x38, 
0x66, 0x3A, 0x65, 0x30, 0x3A, 0x62, 0x33, 0x3A, 0x39, 0x31, 0x3A, 0x36, 0x61, 0x3A, 0x64, 0x34, 0x3A, 0x61, 0x30, 0x3A, 0x37, 0x32, 0x3A, 0x33, 0x62, 
0x3A, 0x35, 0x34, 0x3A, 0x30, 0x30, 0x3A, 0x32, 0x37, 0x3A, 0x33, 0x37, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x31, 0x35, 0x3A, 0x63, 0x35, 0x3A, 0x39, 0x37, 0x3A, 0x61, 0x32, 0x3A, 0x38, 0x34, 0x3A, 0x31, 
0x33, 0x3A, 0x35, 0x30, 0x3A, 0x34, 0x63, 0x3A, 0x31, 0x35, 0x3A, 0x39, 0x62, 0x3A, 0x64, 0x35, 0x3A, 0x63, 0x34, 0x3A, 0x64, 0x37, 0x3A, 0x37, 0x30, 
0x3A, 0x63, 0x65, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 
0x31, 0x61, 0x3A, 0x30, 0x63, 0x3A, 0x62, 0x31, 0x3A, 0x35, 0x35, 0x3A, 0x63, 0x39, 0x3A, 0x38, 0x64, 0x3A, 0x61, 0x36, 0x3A, 0x31, 0x37, 0x3A, 0x66, 
0x66, 0x3A, 0x31, 0x31, 0x3A, 0x34, 0x64, 0x3A, 0x31, 0x32, 0x3A, 0x36, 0x36, 0x3A, 0x36, 0x66, 0x3A, 0x66, 0x63, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x33, 0x61, 0x3A, 0x61, 0x32, 0x3A, 0x30, 0x35, 0x3A, 
0x33, 0x30, 0x3A, 0x66, 0x36, 0x3A, 0x36, 0x30, 0x3A, 0x32, 0x39, 0x3A, 0x65, 0x34, 0x3A, 0x62, 0x38, 0x3A, 0x34, 0x37, 0x3A, 0x61, 0x32, 0x3A, 0x31, 
0x37, 0x3A, 0x63, 0x32, 0x3A, 0x30, 0x62, 0x3A, 0x61, 0x64, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x62, 0x65, 0x3A, 0x63, 0x37, 0x3A, 0x38, 0x38, 0x3A, 0x33, 0x62, 0x3A, 0x61, 0x64, 0x3A, 0x63, 0x37, 0x3A, 
0x32, 0x63, 0x3A, 0x65, 0x34, 0x3A, 0x62, 0x39, 0x3A, 0x31, 0x37, 0x3A, 0x30, 0x39, 0x3A, 0x36, 0x65, 0x3A, 0x65, 0x39, 0x3A, 0x64, 0x65, 0x3A, 0x63, 
0x38, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x64, 0x62, 
0x3A, 0x33, 0x32, 0x3A, 0x63, 0x38, 0x3A, 0x38, 0x35, 0x3A, 0x37, 0x33, 0x3A, 0x35, 0x37, 0x3A, 0x31, 0x61, 0x3A, 0x61, 0x32, 0x3A, 0x65, 0x31, 0x3A, 
0x61, 0x36, 0x3A, 0x65, 0x37, 0x3A, 0x66, 0x62, 0x3A, 0x37, 0x66, 0x3A, 0x33, 0x33, 0x3A, 0x39, 0x37, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x65, 0x61, 0x3A, 0x61, 0x38, 0x3A, 0x32, 0x33, 0x3A, 0x39, 0x63, 
0x3A, 0x31, 0x65, 0x3A, 0x65, 0x65, 0x3A, 0x30, 0x66, 0x3A, 0x33, 0x63, 0x3A, 0x37, 0x61, 0x3A, 0x35, 0x66, 0x3A, 0x39, 0x35, 0x3A, 0x36, 0x35, 0x3A, 
0x66, 0x33, 0x3A, 0x64, 0x37, 0x3A, 0x66, 0x61, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x20, 0x63, 0x65, 0x3A, 0x37, 0x64, 0x3A, 0x64, 0x30, 0x3A, 0x32, 0x37, 0x3A, 0x36, 0x61, 0x3A, 0x39, 0x65, 0x3A, 0x35, 0x30, 
0x3A, 0x30, 0x32, 0x3A, 0x32, 0x31, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x45, 0x78, 
0x70, 0x6F, 0x6E, 0x65, 0x6E, 0x74, 0x3A, 0x20, 0x36, 0x35, 0x35, 0x33, 0x37, 0x20, 0x28, 0x30, 0x78, 0x31, 0x30, 0x30, 0x30, 0x31, 0x29, 0x0A, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x58, 0x35, 0x30, 0x39, 0x76, 0x33, 0x20, 0x65, 0x78, 0x74, 0x65, 0x6E, 0x73, 0x69, 0x6F, 0x6E, 0x73, 0x3A, 
0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x58, 0x35, 0x30, 0x39, 0x76, 0x33, 0x20, 0x42, 0x61, 0x73, 0x69, 0x63, 
0x20, 0x43, 0x6F, 0x6E, 0x73, 0x74, 0x72, 0x61, 0x69, 0x6E, 0x74, 0x73, 0x3A, 0x20, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x43, 0x41, 0x3A, 0x46, 0x41, 0x4C, 0x53, 0x45, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 
0x20, 0x20, 0x58, 0x35, 0x30, 0x39, 0x76, 0x33, 0x20, 0x53, 0x75, 0x62, 0x6A, 0x65, 0x63, 0x74, 0x20, 0x4B, 0x65, 0x79, 0x20, 0x49, 0x64, 0x65, 0x6E, 
0x74, 0x69, 0x66, 0x69, 0x65, 0x72, 0x3A, 0x20, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 
0x36, 0x30, 0x3A, 0x42, 0x30, 0x3A, 0x42, 0x43, 0x3A, 0x36, 0x38, 0x3A, 0x39, 0x44, 0x3A, 0x42, 0x43, 0x3A, 0x37, 0x31, 0x3A, 0x34, 0x37, 0x3A, 0x45, 
0x41, 0x3A, 0x38, 0x37, 0x3A, 0x46, 0x30, 0x3A, 0x44, 0x43, 0x3A, 0x42, 0x36, 0x3A, 0x36, 0x46, 0x3A, 0x33, 0x30, 0x3A, 0x33, 0x31, 0x3A, 0x30, 0x41, 
0x3A, 0x38, 0x36, 0x3A, 0x31, 0x31, 0x3A, 0x33, 0x37, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x58, 0x35, 0x30, 
0x39, 0x76, 0x33, 0x20, 0x41, 0x75, 0x74, 0x68, 0x6F, 0x72, 0x69, 0x74, 0x79, 0x20, 0x4B, 0x65, 0x79, 0x20, 0x49, 0x64, 0x65, 0x6E, 0x74, 0x69, 0x66, 
0x69, 0x65, 0x72, 0x3A, 0x20, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x6B, 0x65, 0x79, 
0x69, 0x64, 0x3A, 0x42, 0x33, 0x3A, 0x35, 0x44, 0x3A, 0x43, 0x45, 0x3A, 0x42, 0x42, 0x3A, 0x45, 0x37, 0x3A, 0x41, 0x42, 0x3A, 0x35, 0x45, 0x3A, 0x31, 
0x36, 0x3A, 0x46, 0x46, 0x3A, 0x41, 0x35, 0x3A, 0x31, 0x41, 0x3A, 0x43, 0x36, 0x3A, 0x33, 0x41, 0x3A, 0x44, 0x38, 0x3A, 0x36, 0x42, 0x3A, 0x34, 0x46, 
0x3A, 0x35, 0x34, 0x3A, 0x34, 0x35, 0x3A, 0x43, 0x36, 0x3A, 0x43, 0x37, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x44, 0x69, 0x72, 0x4E, 0x61, 0x6D, 0x65, 0x3A, 0x2F, 0x43, 0x3D, 0x43, 0x4E, 0x2F, 0x4F, 0x3D, 0x47, 0x41, 0x70, 0x70, 0x50, 
0x72, 0x6F, 0x78, 0x79, 0x2F, 0x43, 0x4E, 0x3D, 0x43, 0x41, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 
0x20, 0x20, 0x73, 0x65, 0x72, 0x69, 0x61, 0x6C, 0x3A, 0x39, 0x32, 0x3A, 0x32, 0x46, 0x3A, 0x35, 0x33, 0x3A, 0x31, 0x43, 0x3A, 0x41, 0x45, 0x3A, 0x46, 
0x37, 0x3A, 0x46, 0x34, 0x3A, 0x38, 0x35, 0x0A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x53, 0x69, 0x67, 0x6E, 0x61, 0x74, 0x75, 0x72, 0x65, 0x20, 0x41, 0x6C, 
0x67, 0x6F, 0x72, 0x69, 0x74, 0x68, 0x6D, 0x3A, 0x20, 0x73, 0x68, 0x61, 0x31, 0x57, 0x69, 0x74, 0x68, 0x52, 0x53, 0x41, 0x45, 0x6E, 0x63, 0x72, 0x79, 
0x70, 0x74, 0x69, 0x6F, 0x6E, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x32, 0x65, 0x3A, 0x61, 0x64, 0x3A, 0x32, 0x64, 0x3A, 0x65, 0x33, 
0x3A, 0x32, 0x63, 0x3A, 0x30, 0x37, 0x3A, 0x63, 0x38, 0x3A, 0x35, 0x34, 0x3A, 0x39, 0x65, 0x3A, 0x62, 0x34, 0x3A, 0x30, 0x38, 0x3A, 0x63, 0x61, 0x3A, 
0x61, 0x37, 0x3A, 0x34, 0x31, 0x3A, 0x38, 0x65, 0x3A, 0x32, 0x30, 0x3A, 0x61, 0x62, 0x3A, 0x32, 0x32, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 
0x20, 0x20, 0x36, 0x61, 0x3A, 0x32, 0x39, 0x3A, 0x64, 0x36, 0x3A, 0x34, 0x36, 0x3A, 0x33, 0x32, 0x3A, 0x34, 0x36, 0x3A, 0x30, 0x39, 0x3A, 0x36, 0x39, 
0x3A, 0x35, 0x32, 0x3A, 0x32, 0x66, 0x3A, 0x35, 0x38, 0x3A, 0x35, 0x39, 0x3A, 0x39, 0x35, 0x3A, 0x33, 0x66, 0x3A, 0x61, 0x63, 0x3A, 0x35, 0x36, 0x3A, 
0x62, 0x34, 0x3A, 0x30, 0x37, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x66, 0x65, 0x3A, 0x63, 0x34, 0x3A, 0x34, 0x32, 0x3A, 0x32, 
0x33, 0x3A, 0x31, 0x36, 0x3A, 0x37, 0x63, 0x3A, 0x66, 0x31, 0x3A, 0x33, 0x30, 0x3A, 0x36, 0x64, 0x3A, 0x38, 0x64, 0x3A, 0x39, 0x38, 0x3A, 0x37, 0x39, 
0x3A, 0x34, 0x30, 0x3A, 0x30, 0x33, 0x3A, 0x30, 0x35, 0x3A, 0x38, 0x36, 0x3A, 0x33, 0x38, 0x3A, 0x61, 0x66, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 
0x20, 0x20, 0x20, 0x35, 0x33, 0x3A, 0x34, 0x35, 0x3A, 0x65, 0x65, 0x3A, 0x33, 0x35, 0x3A, 0x37, 0x64, 0x3A, 0x62, 0x38, 0x3A, 0x64, 0x65, 0x3A, 0x61, 
0x34, 0x3A, 0x62, 0x64, 0x3A, 0x36, 0x63, 0x3A, 0x35, 0x63, 0x3A, 0x64, 0x38, 0x3A, 0x35, 0x66, 0x3A, 0x31, 0x61, 0x3A, 0x33, 0x66, 0x3A, 0x33, 0x38, 
0x3A, 0x36, 0x30, 0x3A, 0x66, 0x32, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x30, 0x36, 0x3A, 0x64, 0x64, 0x3A, 0x35, 0x36, 0x3A, 
0x62, 0x30, 0x3A, 0x35, 0x65, 0x3A, 0x33, 0x38, 0x3A, 0x39, 0x39, 0x3A, 0x34, 0x35, 0x3A, 0x33, 0x63, 0x3A, 0x30, 0x37, 0x3A, 0x64, 0x32, 0x3A, 0x63, 
0x39, 0x3A, 0x35, 0x32, 0x3A, 0x64, 0x36, 0x3A, 0x36, 0x34, 0x3A, 0x31, 0x31, 0x3A, 0x39, 0x37, 0x3A, 0x31, 0x39, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x65, 0x35, 0x3A, 0x64, 0x36, 0x3A, 0x34, 0x62, 0x3A, 0x34, 0x38, 0x3A, 0x63, 0x32, 0x3A, 0x38, 0x33, 0x3A, 0x32, 0x61, 0x3A, 
0x30, 0x34, 0x3A, 0x32, 0x66, 0x3A, 0x32, 0x62, 0x3A, 0x61, 0x33, 0x3A, 0x32, 0x32, 0x3A, 0x35, 0x61, 0x3A, 0x38, 0x64, 0x3A, 0x63, 0x33, 0x3A, 0x35, 
0x31, 0x3A, 0x31, 0x61, 0x3A, 0x62, 0x39, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x31, 0x34, 0x3A, 0x39, 0x36, 0x3A, 0x32, 0x65, 
0x3A, 0x63, 0x64, 0x3A, 0x31, 0x39, 0x3A, 0x31, 0x32, 0x3A, 0x64, 0x66, 0x3A, 0x31, 0x65, 0x3A, 0x61, 0x63, 0x3A, 0x39, 0x62, 0x3A, 0x30, 0x61, 0x3A, 
0x31, 0x32, 0x3A, 0x34, 0x61, 0x3A, 0x64, 0x64, 0x3A, 0x36, 0x63, 0x3A, 0x34, 0x39, 0x3A, 0x30, 0x39, 0x3A, 0x36, 0x36, 0x3A, 0x0A, 0x20, 0x20, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x20, 0x61, 0x31, 0x3A, 0x34, 0x31, 0x3A, 0x65, 0x35, 0x3A, 0x35, 0x64, 0x3A, 0x36, 0x38, 0x3A, 0x36, 0x62, 0x3A, 0x63, 0x35, 
0x3A, 0x34, 0x63, 0x3A, 0x34, 0x35, 0x3A, 0x32, 0x63, 0x3A, 0x31, 0x31, 0x3A, 0x36, 0x31, 0x3A, 0x35, 0x31, 0x3A, 0x33, 0x35, 0x3A, 0x65, 0x39, 0x3A, 
0x38, 0x37, 0x3A, 0x31, 0x32, 0x3A, 0x61, 0x63, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x38, 0x61, 0x3A, 0x31, 0x30, 0x3A, 0x65, 
0x66, 0x3A, 0x62, 0x61, 0x3A, 0x63, 0x32, 0x3A, 0x37, 0x61, 0x3A, 0x30, 0x34, 0x3A, 0x37, 0x32, 0x3A, 0x63, 0x33, 0x3A, 0x63, 0x66, 0x3A, 0x31, 0x30, 
0x3A, 0x62, 0x64, 0x3A, 0x38, 0x36, 0x3A, 0x38, 0x30, 0x3A, 0x34, 0x63, 0x3A, 0x66, 0x35, 0x3A, 0x62, 0x36, 0x3A, 0x62, 0x38, 0x3A, 0x0A, 0x20, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x31, 0x64, 0x3A, 0x32, 0x66, 0x3A, 0x34, 0x61, 0x3A, 0x34, 0x39, 0x3A, 0x36, 0x32, 0x3A, 0x61, 0x39, 0x3A, 0x32, 
0x39, 0x3A, 0x65, 0x35, 0x3A, 0x38, 0x36, 0x3A, 0x62, 0x34, 0x3A, 0x63, 0x34, 0x3A, 0x38, 0x63, 0x3A, 0x30, 0x65, 0x3A, 0x38, 0x32, 0x3A, 0x66, 0x37, 
0x3A, 0x31, 0x31, 0x3A, 0x31, 0x37, 0x3A, 0x34, 0x33, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x64, 0x63, 0x3A, 0x34, 0x63, 0x3A, 
0x33, 0x61, 0x3A, 0x30, 0x62, 0x3A, 0x35, 0x38, 0x3A, 0x37, 0x64, 0x3A, 0x34, 0x65, 0x3A, 0x34, 0x36, 0x3A, 0x66, 0x38, 0x3A, 0x33, 0x38, 0x3A, 0x39, 
0x36, 0x3A, 0x33, 0x66, 0x3A, 0x34, 0x62, 0x3A, 0x35, 0x34, 0x3A, 0x36, 0x39, 0x3A, 0x34, 0x66, 0x3A, 0x37, 0x38, 0x3A, 0x35, 0x65, 0x3A, 0x0A, 0x20, 
0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x35, 0x61, 0x3A, 0x36, 0x30, 0x3A, 0x33, 0x65, 0x3A, 0x36, 0x35, 0x3A, 0x38, 0x36, 0x3A, 0x31, 0x36, 0x3A, 
0x32, 0x65, 0x3A, 0x63, 0x33, 0x3A, 0x31, 0x65, 0x3A, 0x39, 0x65, 0x3A, 0x34, 0x33, 0x3A, 0x32, 0x64, 0x3A, 0x38, 0x61, 0x3A, 0x61, 0x31, 0x3A, 0x34, 
0x62, 0x3A, 0x63, 0x38, 0x3A, 0x36, 0x36, 0x3A, 0x63, 0x64, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x66, 0x34, 0x3A, 0x32, 0x66, 
0x3A, 0x30, 0x30, 0x3A, 0x64, 0x35, 0x3A, 0x39, 0x64, 0x3A, 0x62, 0x66, 0x3A, 0x66, 0x36, 0x3A, 0x61, 0x33, 0x3A, 0x61, 0x32, 0x3A, 0x65, 0x66, 0x3A, 
0x31, 0x62, 0x3A, 0x61, 0x38, 0x3A, 0x65, 0x35, 0x3A, 0x38, 0x36, 0x3A, 0x63, 0x33, 0x3A, 0x63, 0x36, 0x3A, 0x37, 0x32, 0x3A, 0x38, 0x32, 0x3A, 0x0A, 
0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x38, 0x30, 0x3A, 0x39, 0x31, 0x3A, 0x61, 0x30, 0x3A, 0x36, 0x31, 0x3A, 0x38, 0x32, 0x3A, 0x31, 0x62, 
0x3A, 0x62, 0x35, 0x3A, 0x63, 0x62, 0x3A, 0x30, 0x34, 0x3A, 0x32, 0x39, 0x3A, 0x66, 0x36, 0x3A, 0x35, 0x66, 0x3A, 0x35, 0x37, 0x3A, 0x39, 0x63, 0x3A, 
0x63, 0x38, 0x3A, 0x32, 0x65, 0x3A, 0x30, 0x31, 0x3A, 0x63, 0x36, 0x3A, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x30, 0x61, 0x3A, 0x31, 
0x39, 0x3A, 0x38, 0x33, 0x3A, 0x33, 0x32, 0x0A, 0x2D, 0x2D, 0x2D, 0x2D, 0x2D, 0x42, 0x45, 0x47, 0x49, 0x4E, 0x20, 0x43, 0x45, 0x52, 0x54, 0x49, 0x46, 
0x49, 0x43, 0x41, 0x54, 0x45, 0x2D, 0x2D, 0x2D, 0x2D, 0x2D, 0x0A, 0x4D, 0x49, 0x49, 0x43, 0x2F, 0x6A, 0x43, 0x43, 0x41, 0x65, 0x61, 0x67, 0x41, 0x77, 
0x49, 0x42, 0x41, 0x67, 0x49, 0x42, 0x41, 0x54, 0x41, 0x4E, 0x42, 0x67, 0x6B, 0x71, 0x68, 0x6B, 0x69, 0x47, 0x39, 0x77, 0x30, 0x42, 0x41, 0x51, 0x55, 
0x46, 0x41, 0x44, 0x41, 0x75, 0x4D, 0x51, 0x73, 0x77, 0x43, 0x51, 0x59, 0x44, 0x56, 0x51, 0x51, 0x47, 0x45, 0x77, 0x4A, 0x44, 0x54, 0x6A, 0x45, 0x53, 
0x0A, 0x4D, 0x42, 0x41, 0x47, 0x41, 0x31, 0x55, 0x45, 0x43, 0x67, 0x77, 0x4A, 0x52, 0x30, 0x46, 0x77, 0x63, 0x46, 0x42, 0x79, 0x62, 0x33, 0x68, 0x35, 
0x4D, 0x51, 0x73, 0x77, 0x43, 0x51, 0x59, 0x44, 0x56, 0x51, 0x51, 0x44, 0x44, 0x41, 0x4A, 0x44, 0x51, 0x54, 0x41, 0x65, 0x46, 0x77, 0x30, 0x77, 0x4F, 
0x44, 0x41, 0x32, 0x4D, 0x6A, 0x49, 0x77, 0x4E, 0x7A, 0x49, 0x32, 0x4D, 0x44, 0x4E, 0x61, 0x0A, 0x46, 0x77, 0x30, 0x78, 0x4D, 0x54, 0x41, 0x32, 0x4D, 
0x6A, 0x49, 0x77, 0x4E, 0x7A, 0x49, 0x32, 0x4D, 0x44, 0x4E, 0x61, 0x4D, 0x45, 0x73, 0x78, 0x43, 0x7A, 0x41, 0x4A, 0x42, 0x67, 0x4E, 0x56, 0x42, 0x41, 
0x59, 0x54, 0x41, 0x6B, 0x4E, 0x4F, 0x4D, 0x52, 0x49, 0x77, 0x45, 0x41, 0x59, 0x44, 0x56, 0x51, 0x51, 0x4B, 0x44, 0x41, 0x6C, 0x48, 0x51, 0x58, 0x42, 
0x77, 0x55, 0x48, 0x4A, 0x76, 0x0A, 0x65, 0x48, 0x6B, 0x78, 0x44, 0x54, 0x41, 0x4C, 0x42, 0x67, 0x4E, 0x56, 0x42, 0x41, 0x73, 0x4D, 0x42, 0x46, 0x52, 
0x46, 0x55, 0x31, 0x51, 0x78, 0x47, 0x54, 0x41, 0x58, 0x42, 0x67, 0x4E, 0x56, 0x42, 0x41, 0x4D, 0x4D, 0x45, 0x45, 0x78, 0x76, 0x59, 0x32, 0x46, 0x73, 
0x55, 0x48, 0x4A, 0x76, 0x65, 0x48, 0x6C, 0x54, 0x5A, 0x58, 0x4A, 0x32, 0x5A, 0x58, 0x49, 0x77, 0x67, 0x5A, 0x38, 0x77, 0x0A, 0x44, 0x51, 0x59, 0x4A, 
0x4B, 0x6F, 0x5A, 0x49, 0x68, 0x76, 0x63, 0x4E, 0x41, 0x51, 0x45, 0x42, 0x42, 0x51, 0x41, 0x44, 0x67, 0x59, 0x30, 0x41, 0x4D, 0x49, 0x47, 0x4A, 0x41, 
0x6F, 0x47, 0x42, 0x41, 0x4E, 0x73, 0x65, 0x7A, 0x71, 0x68, 0x51, 0x32, 0x59, 0x67, 0x47, 0x55, 0x50, 0x58, 0x67, 0x52, 0x6D, 0x45, 0x30, 0x47, 0x7A, 
0x32, 0x50, 0x34, 0x4C, 0x4F, 0x52, 0x61, 0x74, 0x53, 0x67, 0x0A, 0x63, 0x6A, 0x74, 0x55, 0x41, 0x43, 0x63, 0x33, 0x46, 0x63, 0x57, 0x58, 0x6F, 0x6F, 
0x51, 0x54, 0x55, 0x45, 0x77, 0x56, 0x6D, 0x39, 0x58, 0x45, 0x31, 0x33, 0x44, 0x4F, 0x47, 0x67, 0x79, 0x78, 0x56, 0x63, 0x6D, 0x4E, 0x70, 0x68, 0x66, 
0x2F, 0x45, 0x55, 0x30, 0x53, 0x5A, 0x6D, 0x2F, 0x38, 0x4F, 0x71, 0x49, 0x46, 0x4D, 0x50, 0x5A, 0x67, 0x4B, 0x65, 0x53, 0x34, 0x52, 0x36, 0x49, 0x58, 
0x0A, 0x77, 0x67, 0x75, 0x74, 0x76, 0x73, 0x65, 0x49, 0x4F, 0x36, 0x33, 0x48, 0x4C, 0x4F, 0x53, 0x35, 0x46, 0x77, 0x6C, 0x75, 0x36, 0x64, 0x37, 0x49, 
0x32, 0x7A, 0x4C, 0x49, 0x68, 0x58, 0x4E, 0x58, 0x47, 0x71, 0x4C, 0x68, 0x70, 0x75, 0x66, 0x37, 0x66, 0x7A, 0x4F, 0x58, 0x36, 0x71, 0x67, 0x6A, 0x6E, 
0x42, 0x37, 0x75, 0x44, 0x7A, 0x78, 0x36, 0x58, 0x35, 0x56, 0x6C, 0x38, 0x39, 0x66, 0x36, 0x0A, 0x7A, 0x6E, 0x33, 0x51, 0x4A, 0x32, 0x71, 0x65, 0x55, 
0x41, 0x49, 0x68, 0x41, 0x67, 0x4D, 0x42, 0x41, 0x41, 0x47, 0x6A, 0x67, 0x59, 0x30, 0x77, 0x67, 0x59, 0x6F, 0x77, 0x43, 0x51, 0x59, 0x44, 0x56, 0x52, 
0x30, 0x54, 0x42, 0x41, 0x49, 0x77, 0x41, 0x44, 0x41, 0x64, 0x42, 0x67, 0x4E, 0x56, 0x48, 0x51, 0x34, 0x45, 0x46, 0x67, 0x51, 0x55, 0x59, 0x4C, 0x43, 
0x38, 0x61, 0x4A, 0x32, 0x38, 0x0A, 0x63, 0x55, 0x66, 0x71, 0x68, 0x2F, 0x44, 0x63, 0x74, 0x6D, 0x38, 0x77, 0x4D, 0x51, 0x71, 0x47, 0x45, 0x54, 0x63, 
0x77, 0x58, 0x67, 0x59, 0x44, 0x56, 0x52, 0x30, 0x6A, 0x42, 0x46, 0x63, 0x77, 0x56, 0x59, 0x41, 0x55, 0x73, 0x31, 0x33, 0x4F, 0x75, 0x2B, 0x65, 0x72, 
0x58, 0x68, 0x62, 0x2F, 0x70, 0x52, 0x72, 0x47, 0x4F, 0x74, 0x68, 0x72, 0x54, 0x31, 0x52, 0x46, 0x78, 0x73, 0x65, 0x68, 0x0A, 0x4D, 0x71, 0x51, 0x77, 
0x4D, 0x43, 0x34, 0x78, 0x43, 0x7A, 0x41, 0x4A, 0x42, 0x67, 0x4E, 0x56, 0x42, 0x41, 0x59, 0x54, 0x41, 0x6B, 0x4E, 0x4F, 0x4D, 0x52, 0x49, 0x77, 0x45, 
0x41, 0x59, 0x44, 0x56, 0x51, 0x51, 0x4B, 0x44, 0x41, 0x6C, 0x48, 0x51, 0x58, 0x42, 0x77, 0x55, 0x48, 0x4A, 0x76, 0x65, 0x48, 0x6B, 0x78, 0x43, 0x7A, 
0x41, 0x4A, 0x42, 0x67, 0x4E, 0x56, 0x42, 0x41, 0x4D, 0x4D, 0x0A, 0x41, 0x6B, 0x4E, 0x42, 0x67, 0x67, 0x6B, 0x41, 0x6B, 0x69, 0x39, 0x54, 0x48, 0x4B, 
0x37, 0x33, 0x39, 0x49, 0x55, 0x77, 0x44, 0x51, 0x59, 0x4A, 0x4B, 0x6F, 0x5A, 0x49, 0x68, 0x76, 0x63, 0x4E, 0x41, 0x51, 0x45, 0x46, 0x42, 0x51, 0x41, 
0x44, 0x67, 0x67, 0x45, 0x42, 0x41, 0x43, 0x36, 0x74, 0x4C, 0x65, 0x4D, 0x73, 0x42, 0x38, 0x68, 0x55, 0x6E, 0x72, 0x51, 0x49, 0x79, 0x71, 0x64, 0x42, 
0x0A, 0x6A, 0x69, 0x43, 0x72, 0x49, 0x6D, 0x6F, 0x70, 0x31, 0x6B, 0x59, 0x79, 0x52, 0x67, 0x6C, 0x70, 0x55, 0x69, 0x39, 0x59, 0x57, 0x5A, 0x55, 0x2F, 
0x72, 0x46, 0x61, 0x30, 0x42, 0x2F, 0x37, 0x45, 0x51, 0x69, 0x4D, 0x57, 0x66, 0x50, 0x45, 0x77, 0x62, 0x59, 0x32, 0x59, 0x65, 0x55, 0x41, 0x44, 0x42, 
0x59, 0x59, 0x34, 0x72, 0x31, 0x4E, 0x46, 0x37, 0x6A, 0x56, 0x39, 0x75, 0x4E, 0x36, 0x6B, 0x0A, 0x76, 0x57, 0x78, 0x63, 0x32, 0x46, 0x38, 0x61, 0x50, 
0x7A, 0x68, 0x67, 0x38, 0x67, 0x62, 0x64, 0x56, 0x72, 0x42, 0x65, 0x4F, 0x4A, 0x6C, 0x46, 0x50, 0x41, 0x66, 0x53, 0x79, 0x56, 0x4C, 0x57, 0x5A, 0x42, 
0x47, 0x58, 0x47, 0x65, 0x58, 0x57, 0x53, 0x30, 0x6A, 0x43, 0x67, 0x79, 0x6F, 0x45, 0x4C, 0x79, 0x75, 0x6A, 0x49, 0x6C, 0x71, 0x4E, 0x77, 0x31, 0x45, 
0x61, 0x75, 0x52, 0x53, 0x57, 0x0A, 0x4C, 0x73, 0x30, 0x5A, 0x45, 0x74, 0x38, 0x65, 0x72, 0x4A, 0x73, 0x4B, 0x45, 0x6B, 0x72, 0x64, 0x62, 0x45, 0x6B, 
0x4A, 0x5A, 0x71, 0x46, 0x42, 0x35, 0x56, 0x31, 0x6F, 0x61, 0x38, 0x56, 0x4D, 0x52, 0x53, 0x77, 0x52, 0x59, 0x56, 0x45, 0x31, 0x36, 0x59, 0x63, 0x53, 
0x72, 0x49, 0x6F, 0x51, 0x37, 0x37, 0x72, 0x43, 0x65, 0x67, 0x52, 0x79, 0x77, 0x38, 0x38, 0x51, 0x76, 0x59, 0x61, 0x41, 0x0A, 0x54, 0x50, 0x57, 0x32, 
0x75, 0x42, 0x30, 0x76, 0x53, 0x6B, 0x6C, 0x69, 0x71, 0x53, 0x6E, 0x6C, 0x68, 0x72, 0x54, 0x45, 0x6A, 0x41, 0x36, 0x43, 0x39, 0x78, 0x45, 0x58, 0x51, 
0x39, 0x78, 0x4D, 0x4F, 0x67, 0x74, 0x59, 0x66, 0x55, 0x35, 0x47, 0x2B, 0x44, 0x69, 0x57, 0x50, 0x30, 0x74, 0x55, 0x61, 0x55, 0x39, 0x34, 0x58, 0x6C, 
0x70, 0x67, 0x50, 0x6D, 0x57, 0x47, 0x46, 0x69, 0x37, 0x44, 0x0A, 0x48, 0x70, 0x35, 0x44, 0x4C, 0x59, 0x71, 0x68, 0x53, 0x38, 0x68, 0x6D, 0x7A, 0x66, 
0x51, 0x76, 0x41, 0x4E, 0x57, 0x64, 0x76, 0x2F, 0x61, 0x6A, 0x6F, 0x75, 0x38, 0x62, 0x71, 0x4F, 0x57, 0x47, 0x77, 0x38, 0x5A, 0x79, 0x67, 0x6F, 0x43, 
0x52, 0x6F, 0x47, 0x47, 0x43, 0x47, 0x37, 0x58, 0x4C, 0x42, 0x43, 0x6E, 0x32, 0x58, 0x31, 0x65, 0x63, 0x79, 0x43, 0x34, 0x42, 0x78, 0x67, 0x6F, 0x5A, 
0x0A, 0x67, 0x7A, 0x49, 0x3D, 0x0A, 0x2D, 0x2D, 0x2D, 0x2D, 0x2D, 0x45, 0x4E, 0x44, 0x20, 0x43, 0x45, 0x52, 0x54, 0x49, 0x46, 0x49, 0x43, 0x41, 0x54, 
0x45, 0x2D, 0x2D, 0x2D, 0x2D, 0x2D, 0x0A
};
            GenerateBinaryFile(DataHelper.SSL_CERT, proxyServerCert);
            GenerateBinaryFile(DataHelper.SSL_KEY, proxyServerKey);
        }

        public static void GenerateIconFile()
        {
            byte[] gapIcon =
               {
0x00,0x00,0x01,0x00,0x01,0x00,0x20,0x20,0x00,0x00,0x00,0x00,0x20,0x00,0xA8,0x10,0x00,0x00,0x16,0x00,0x00,0x00,0x28,0x00,0x00,0x00,0x20,0x00,0x00,0x00,0x40,0x00,0x00,0x00,0x01,0x00,0x20,0x00,0x00,0x00,0x00,0x00,0x00,0x20,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x39,0x39,0x39,0x3F,0x99,0x99,0x99,0xCF,0xA5,0xA2,0xA2,0xFF,0xC2,0xBC,0xBC,0xFF,0xE6,0xDD,0xDD,0xFF,0xD3,0xCA,0xCA,0xFF,0xC1,0xB9,0xB9,0xFF,0xAC,0xA5,0xA6,0xFF,0xA5,0xA2,0xA2,0xFF,0xA1,0xA1,0xA1,0xFF,0x39,0x39,0x39,0x3F,0x33,0x33,0x33,0x0F,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x39,0x39,0x39,0x8F,0x39,0x39,0x39,0xBF,0x40,0x3C,0x3C,0xBF,0x4F,0x40,0x40,0xFF,0x69,0x46,0x45,0xFF,0x7B,0x4F,0x49,0xFF,0x90,0x5F,0x54,0xFF,0xA2,0x6F,0x5D,0xFF,0xA2,0x6F,0x57,0xFF,0x98,0x65,0x52,0xFF,0x88,0x58,0x4B,0xFF,0x7B,0x4F,0x49,0xFF,0x6B,0x48,0x45,0xFF,0x50,0x40,0x40,0xFF,0x40,0x3C,0x3C,0xCF,0x39,0x39,0x39,0xBF,0x36,0x36,0x36,0x2F,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x3C,0x39,0x39,0x6F,0x40,0x3C,0x3C,0xFF,0x56,0x42,0x41,0xFF,0x7B,0x4F,0x49,0xFF,0x9A,0x62,0x4E,0xFF,0xB3,0x7A,0x51,0xFF,0xC4,0x8D,0x52,0xFF,0xD1,0x9A,0x52,0xFF,0xD9,0xA1,0x52,0xFF,0xDA,0xA3,0x50,0xFF,0xDB,0xA5,0x50,0xFF,0xD7,0xA1,0x4F,0xFF,0xCC,0x96,0x4E,0xFF,0xBB,0x85,0x4D,0xFF,0x9F,0x69,
0x4C,0xFF,0x7D,0x52,0x49,0xFF,0x59,0x43,0x42,0xFF,0x41,0x3C,0x3C,0xFF,0x3D,0x39,0x39,0x3F,0x33,0x33,0x33,0x0F,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x3C,0x3C,0x3C,0x2F,0x3C,0x3C,0x3C,0xBF,0x4E,0x40,0x40,0xFF,0x77,0x4C,0x48,0xFF,0xA0,0x68,0x50,0xFF,0xC0,0x87,0x54,0xFF,0xCD,0x93,0x55,0xFF,0xD2,0x99,0x55,0xFF,0xD5,0x9C,0x54,0xFF,0xD6,0x9E,0x52,0xFF,0xD8,0xA0,0x52,0xFF,0xD9,0xA2,0x52,0xFF,0xDB,0xA4,0x50,0xFF,0xDC,0xA5,0x50,0xFF,0xDD,0xA7,0x4E,0xFF,0xDE,0xA8,0x4E,0xFF,0xDB,0xA6,0x4D,0xFF,0xCF,0x9A,0x4C,0xFF,0xAA,0x76,0x4B,0xFF,0x7A,0x4E,0x49,0xFF,0x4F,0x40,0x40,0xFF,0x3C,0x3C,0x3C,0xCF,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x3C,0x3C,0x3C,0x2F,0x3F,0x3D,0x3D,0xFF,0x54,0x43,0x42,0xFF,0x8A,0x55,
0x4C,0xFF,0xB3,0x78,0x53,0xFF,0xCB,0x90,0x57,0xFF,0xCF,0x93,0x56,0xFF,0xD0,0x96,0x55,0xFF,0xD2,0x99,0x58,0xFF,0xD6,0xA0,0x60,0xFF,0xDB,0xA8,0x6B,0xFF,0xDE,0xAF,0x74,0xFF,0xE1,0xB4,0x78,0xFF,0xE0,0xB2,0x72,0xFF,0xDF,0xB0,0x68,0xFF,0xDF,0xAC,0x5C,0xFF,0xDD,0xA8,0x51,0xFF,0xDE,0xA9,0x4E,0xFF,0xE0,0xAB,0x4D,0xFF,0xDF,0xAA,0x4B,0xFF,0xC3,0x8F,0x4B,0xFF,0x8F,0x5C,0x4A,0xFF,0x59,0x44,0x43,0xFF,0x3F,0x3E,0x3E,0xCF,0x33,0x33,0x33,0x0F,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x3C,0x3C,0x3C,0x2F,0x3E,0x3E,0x3F,0xEF,0x5A,0x44,0x44,0xFF,0x8F,0x59,0x4D,0xFF,0xBC,0x80,0x56,0xFF,0xCA,0x8D,0x58,0xFF,0xCC,0x90,0x57,0xFF,0xCD,0x92,0x57,0xFF,0xD4,0x9F,0x69,0xFF,0xE7,0xC8,0xA9,0xFF,0xF3,0xE4,0xD4,0xFF,0xFA,0xF2,0xEA,0xFF,0xFC,0xF7,0xF2,0xFF,0xFD,0xF9,0xF6,0xFF,0xFC,0xF7,0xF2,0xFF,0xFB,0xF3,0xEB,0xFF,0xF6,0xE8,0xD5,0xFF,0xEE,0xD2,0xAA,0xFF,0xE2,0xB0,0x65,0xFF,0xDF,0xA9,0x4E,0xFF,0xE0,0xAB,0x4C,0xFF,0xE2,0xAD,0x4A,0xFF,0xD2,0x9F,
0x4B,0xFF,0x99,0x64,0x4B,0xFF,0x5E,0x46,0x45,0xFF,0x40,0x3F,0x3F,0xCF,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x40,0x3E,0x40,0x6F,0x56,0x44,0x44,0xFF,0x8E,0x58,0x4E,0xFF,0xBC,0x7F,0x57,0xFF,0xC8,0x8A,0x59,0xFF,0xC9,0x8C,0x58,0xFF,0xCE,0x96,0x64,0xFF,0xE2,0xC0,0xA3,0xFF,0xFB,0xF5,0xF0,0xFF,0xFF,0xFF,0xFE,0xFF,0xFB,0xFD,0xFB,0xFF,0xED,0xF6,0xED,0xFF,0xEA,0xF6,0xEB,0xFF,0xEB,0xF6,0xEB,0xFF,0xEB,0xF7,0xEC,0xFF,0xF4,0xFA,0xF4,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFC,0xF6,0xF0,0xFF,0xED,0xCF,0xA2,0xFF,0xE2,0xB1,0x5F,0xFF,0xE0,0xAC,0x4D,0xFF,0xE2,0xAD,0x4A,0xFF,0xD9,0xA6,0x4A,0xFF,0x94,0x60,0x4B,0xFF,0x57,0x44,0x44,0xFF,0x3F,0x3F,0x3F,0xCF,0x44,0x44,0x44,0x0F,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x40,0x40,0x40,0x8F,0x4A,0x42,0x42,0xFF,0x81,0x51,0x4B,0xFF,0xB4,0x76,0x55,0xFF,0xC5,0x86,0x59,0xFF,0xC6,0x89,0x59,0xFF,0xCF,0x98,0x6E,0xFF,0xEE,0xDA,0xCA,0xFF,0xFE,0xFB,0xF9,0xFF,0xFF,0xFF,
0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xDE,0xEF,0xDD,0xFF,0x73,0xBF,0x66,0xFF,0x66,0xBB,0x59,0xFF,0x66,0xBE,0x5A,0xFF,0x66,0xC0,0x5D,0xFF,0xA7,0xDB,0xA4,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFE,0xFD,0xFB,0xFF,0xF6,0xE5,0xCE,0xFF,0xE3,0xB4,0x64,0xFF,0xE0,0xAC,0x4C,0xFF,0xE2,0xAE,0x4A,0xFF,0xCF,0x9A,0x4A,0xFF,0x88,0x57,0x4A,0xFF,0x4D,0x42,0x43,0xFF,0x41,0x41,0x41,0x3F,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x40,0x40,0x40,0x14,0x42,0x41,0x42,0xCF,0x6A,0x4A,0x48,0xFF,0xA7,0x6B,0x53,0xFF,0xC2,0x82,0x59,0xFF,0xC5,0x85,0x59,0xFF,0xCB,0x92,0x6B,0xFF,0xF0,0xDE,0xD3,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xD8,0xED,0xD6,0xFF,0x58,0xB2,0x41,0xFF,0x49,0xAE,0x32,0xFF,0x49,0xB1,0x34,0xFF,0x49,0xB3,0x35,0xFF,0x96,0xD4,0x8D,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFE,0xFE,0xFF,0xF6,0xE5,0xCE,0xFF,0xE3,0xB3,0x62,0xFF,0xE1,0xAC,0x4C,0xFF,0xE2,0xAE,0x49,0xFF,0xB8,0x83,0x4C,0xFF,0x71,0x4C,
0x49,0xFF,0x44,0x42,0x42,0xFF,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x42,0x42,0x42,0x7F,0x51,0x45,0x45,0xFF,0x8E,0x57,0x4E,0xFF,0xBB,0x7B,0x58,0xFF,0xC2,0x81,0x59,0xFF,0xC3,0x84,0x5A,0xFF,0xE8,0xCE,0xBE,0xFF,0xFE,0xFD,0xFC,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xD8,0xEC,0xD6,0xFF,0x58,0xB2,0x41,0xFF,0x49,0xAC,0x30,0xFF,0x49,0xAF,0x33,0xFF,0x49,0xB1,0x34,0xFF,0x96,0xD3,0x8B,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFE,0xFF,0xF4,0xE1,0xC4,0xFF,0xE2,0xAF,0x58,0xFF,0xE1,0xAC,0x4C,0xFF,0xDB,0xA7,0x4A,0xFF,0x99,0x65,0x4C,0xFF,0x52,0x45,0x45,0xFF,0x42,0x42,0x42,0x8F,0x00,0x00,0x00,0x00,0x45,0x43,0x43,0x94,0x6D,0x4A,0x49,0xFF,0xA6,0x68,0x54,0xFF,0xBE,0x7D,0x59,0xFF,0xC1,0x80,0x5A,0xFF,0xD4,0xA6,0x8B,0xFF,0xFE,0xFB,0xFB,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xD7,0xEB,0xD5,0xFF,0x58,0xB0,0x40,0xFF,0x49,0xAB,
0x2E,0xFF,0x49,0xAD,0x31,0xFF,0x49,0xAE,0x32,0xFF,0x96,0xD1,0x89,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFD,0xF9,0xF5,0xFF,0xE9,0xC4,0x89,0xFF,0xE0,0xAA,0x4D,0xFF,0xE2,0xAD,0x4B,0xFF,0xC0,0x8C,0x4B,0xFF,0x6C,0x4D,0x49,0xFF,0x44,0x43,0x43,0xBF,0x00,0x00,0x00,0x00,0x4F,0x46,0x46,0xFE,0x82,0x51,0x4D,0xFF,0xB6,0x75,0x58,0xFF,0xBE,0x7D,0x59,0xFF,0xC4,0x86,0x64,0xFF,0xEE,0xDC,0xD3,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xD7,0xEB,0xD5,0xFF,0x57,0xAD,0x3E,0xFF,0x48,0xA8,0x2C,0xFF,0x49,0xAA,0x2D,0xFF,0x49,0xAC,0x2F,0xFF,0x96,0xD0,0x88,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xF8,0xEB,0xDB,0xFF,0xDF,0xAA,0x50,0xFF,0xE0,0xAB,0x4D,0xFF,0xD7,0xA2,0x4C,0xFF,0x8B,0x5A,0x4C,0xFF,0x4D,0x45,0x45,0xBF,0x00,0x00,0x00,0x00,0x5E,0x48,
0x48,0xFE,0x93,0x5B,0x50,0xFF,0xBB,0x78,0x59,0xFF,0xBD,0x7A,0x59,0xFF,0xCA,0x95,0x79,0xFF,0xFA,0xF5,0xF3,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xD7,0xEA,0xD5,0xFF,0x57,0xAB,0x3D,0xFF,0x48,0xA6,0x2B,0xFF,0x48,0xA7,0x2C,0xFF,0x49,0xA9,0x2D,0xFF,0x96,0xCE,0x86,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFE,0xFC,0xFF,0xE5,0xBB,0x7C,0xFF,0xDF,0xA9,0x4D,0xFF,0xDC,0xA8,0x4D,0xFF,0xA2,0x6B,0x4C,0xFF,0x6A,0x5B,0x5A,0xFF,0x8E,0x8E,0x8E,0x3F,0x68,0x4A,0x4A,0xFE,0xA1,0x64,0x53,0xFF,0xBB,0x78,0x59,0xFF,0xBC,0x7A,0x5A,0xFF,0xD4,0xA9,0x95,0xFF,0xFF,0xFF,0xFE,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xD7,0xEA,0xD4,0xFF,0x57,0xAA,0x3A,0xFF,0x48,0xA3,0x28,0xFF,0x48,0xA5,0x2A,0xFF,0x48,0xA7,0x2B,0xFF,0x95,0xCC,0x85,0xFF,0xFF,0xFF,
0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xEE,0xD4,0xAD,0xFF,0xDE,0xA8,0x51,0xFF,0xDE,0xA8,0x4E,0xFF,0xB4,0x7D,0x4E,0xFF,0x7A,0x5D,0x5C,0xFF,0x8E,0x8E,0x8E,0x3F,0x71,0x4C,0x4A,0xFE,0xAA,0x69,0x55,0xFF,0xBA,0x76,0x59,0xFF,0xBB,0x78,0x59,0xFF,0xE1,0xC1,0xB5,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xD7,0xE9,0xD4,0xFF,0x56,0xA6,0x39,0xFF,0x48,0xA1,0x26,0xFF,0x48,0xA2,0x27,0xFF,0x48,0xA3,0x29,0xFF,0x95,0xCA,0x84,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xF2,0xDF,0xC4,0xFF,0xDE,0xA9,0x56,0xFF,0xDE,0xA8,0x4E,0xFF,0xC2,0x8B,0x4D,0xFF,0x84,0x5E,0x5D,0xFF,0x8E,0x8E,0x92,0x3F,0x77,0x4E,0x4C,0xFE,0xAB,0x6A,0x55,0xFF,0xB9,0x75,0x59,0xFF,0xBA,0x76,0x59,0xFF,0xE6,0xCC,
0xC3,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xD6,0xE8,0xD3,0xFF,0x56,0xA3,0x38,0xFF,0x47,0x9E,0x25,0xFF,0x47,0x9F,0x25,0xFF,0x47,0xA0,0x25,0xFF,0x95,0xC9,0x82,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xF5,0xE5,0xD0,0xFF,0xDE,0xAA,0x5A,0xFF,0xDD,0xA6,0x4F,0xFF,0xC4,0x8E,0x4E,0xFF,0x88,0x60,0x5D,0xFF,0x92,0x92,0x92,0x3F,0x79,0x4F,0x4C,0xFE,0xAB,0x69,0x56,0xFF,0xB8,0x74,0x59,0xFF,0xBA,0x75,0x59,0xFF,0xE7,0xCC,0xC4,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFB,0xFC,0xFC,0xFF,0xC5,0xDC,0xC4,0xFF,0xB5,0xD3,0xAD,0xFF,0x9D,0xC7,0x93,0xFF,0x4F,0x9E,0x30,0xFF,0x47,0x9B,0x24,0xFF,0x47,0x9C,0x24,0xFF,0x47,0x9D,0x25,0xFF,0x76,0xB6,0x60,0xFF,0xB6,0xD7,0xB0,0xFF,0xB7,0xD7,0xB2,0xFF,0xE5,0xF1,0xE6,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,
0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xF5,0xE5,0xD2,0xFF,0xDD,0xA9,0x5B,0xFF,0xDC,0xA5,0x50,0xFF,0xC4,0x8E,0x4E,0xFF,0x88,0x60,0x5D,0xFF,0x92,0x92,0x92,0x3F,0x75,0x4D,0x4C,0xFE,0xA9,0x67,0x55,0xFF,0xB8,0x72,0x59,0xFF,0xB8,0x74,0x58,0xFF,0xE1,0xC0,0xB6,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFE,0xFF,0xFE,0xFF,0xAC,0xCE,0xA4,0xFF,0x47,0x95,0x23,0xFF,0x46,0x95,0x1F,0xFF,0x46,0x96,0x1E,0xFF,0x46,0x98,0x21,0xFF,0x46,0x99,0x22,0xFF,0x47,0x9A,0x23,0xFF,0x47,0x9B,0x24,0xFF,0x48,0x9C,0x25,0xFF,0x5E,0xA7,0x46,0xFF,0xEB,0xF4,0xEC,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xF2,0xDF,0xC7,0xFF,0xDB,0xA6,0x5A,0xFF,0xDB,0xA4,0x50,0xFF,0xC0,0x88,0x4E,0xFF,0x86,0x5E,0x5D,0xFF,0x92,0x92,0x92,0x3F,0x6E,0x4C,0x4C,0xFE,0xA0,0x62,0x54,0xFF,0xB7,0x71,0x5A,0xFF,0xB7,0x72,0x59,0xFF,0xD3,0xA7,0x98,0xFF,0xFF,0xFF,0xFE,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,
0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xEB,0xF3,0xE9,0xFF,0x68,0xAB,0x50,0xFF,0x45,0x94,0x1F,0xFF,0x45,0x94,0x1E,0xFF,0x45,0x95,0x1D,0xFF,0x46,0x96,0x1F,0xFF,0x46,0x97,0x1F,0xFF,0x46,0x98,0x20,0xFF,0x4C,0x9B,0x2A,0xFF,0xB4,0xD5,0xAD,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xEC,0xD1,0xB0,0xFF,0xD9,0xA2,0x54,0xFF,0xD9,0xA1,0x52,0xFF,0xB3,0x7C,0x4F,0xFF,0x7C,0x5F,0x5E,0xFF,0x92,0x92,0x92,0x3F,0x66,0x4C,0x4C,0xFE,0x93,0x58,0x51,0xFF,0xB5,0x70,0x5A,0xFF,0xB6,0x71,0x59,0xFF,0xC6,0x8F,0x7C,0xFF,0xFB,0xF6,0xF5,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFD,0xFE,0xFD,0xFF,0xB6,0xD9,0xB3,0xFF,0x4A,0xA1,0x2F,0xFF,0x46,0x9A,0x24,0xFF,0x45,0x95,0x1F,0xFF,0x45,0x94,0x1E,0xFF,0x46,0x94,0x1E,0xFF,0x46,0x94,0x1E,0xFF,0x6E,0xAC,0x56,0xFF,0xEC,0xF4,0xEC,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFE,0xFD,0xFD,0xFF,0xE0,0xB6,
0x84,0xFF,0xD7,0x9F,0x52,0xFF,0xD6,0x9E,0x51,0xFF,0xA2,0x6A,0x4E,0xFF,0x70,0x5F,0x5F,0xFF,0x92,0x92,0x92,0x3F,0x5C,0x4D,0x4D,0xFE,0x83,0x51,0x4D,0xFF,0xAF,0x6B,0x58,0xFF,0xB6,0x70,0x5A,0xFF,0xBC,0x7C,0x66,0xFF,0xEE,0xDC,0xD7,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFC,0xFE,0xFC,0xFF,0x7C,0xC4,0x78,0xFF,0x48,0xAA,0x36,0xFF,0x47,0xA3,0x2D,0xFF,0x46,0x9E,0x27,0xFF,0x46,0x9A,0x24,0xFF,0x4B,0x9A,0x2A,0xFF,0xC3,0xDC,0xC0,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xF7,0xEB,0xE1,0xFF,0xD5,0x9D,0x59,0xFF,0xD6,0x9D,0x53,0xFF,0xCF,0x97,0x51,0xFF,0x8C,0x5A,0x4E,0xFF,0x55,0x4D,0x4D,0xBF,0x00,0x00,0x00,0x00,0x53,0x4F,0x4F,0xBF,0x74,0x4E,0x4D,0xFF,0xA1,0x60,0x54,0xFF,0xB5,0x6F,0x59,0xFF,0xB7,0x71,0x5B,0xFF,0xCF,0xA0,0x92,0xFF,0xFE,0xFD,0xFD,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xD2,0xED,
0xD6,0xFF,0x58,0xBD,0x5F,0xFF,0x48,0xB3,0x46,0xFF,0x48,0xB0,0x3E,0xFF,0x49,0xAC,0x3B,0xFF,0x8D,0xC8,0x87,0xFF,0xF9,0xFC,0xF9,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFD,0xFA,0xF7,0xFF,0xE2,0xBC,0x95,0xFF,0xD2,0x99,0x55,0xFF,0xD4,0x9B,0x54,0xFF,0xBC,0x84,0x51,0xFF,0x74,0x51,0x4E,0xFF,0x50,0x4F,0x4F,0xBF,0x00,0x00,0x00,0x00,0x50,0x50,0x4E,0x7F,0x5E,0x50,0x4F,0xFF,0x90,0x55,0x4F,0xFF,0xB1,0x6B,0x58,0xFF,0xB5,0x6F,0x5A,0xFF,0xB7,0x72,0x5C,0xFF,0xE7,0xD0,0xCA,0xFF,0xFE,0xFD,0xFC,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xF9,0xFD,0xFA,0xFF,0x90,0xD5,0xA4,0xFF,0x48,0xBB,0x63,0xFF,0x47,0xBA,0x5B,0xFF,0x5B,0xC0,0x67,0xFF,0xDB,0xF0,0xDE,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFE,0xFE,0xFF,0xF1,0xDE,0xCE,0xFF,0xD4,0x9D,0x63,0xFF,0xD1,0x97,0x55,0xFF,0xCE,0x95,0x55,0xFF,0x9A,0x64,0x4F,0xFF,0x5F,0x50,0x4F,0xFF,0x50,0x50,
0x4E,0x8F,0x00,0x00,0x00,0x00,0x50,0x50,0x50,0x40,0x52,0x50,0x50,0xCF,0x74,0x50,0x4F,0xFF,0xA3,0x61,0x55,0xFF,0xB4,0x6E,0x5A,0xFF,0xB5,0x6F,0x5A,0xFF,0xC0,0x83,0x6F,0xFF,0xEF,0xDF,0xDB,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xE5,0xF5,0xEB,0xFF,0x67,0xCA,0x8D,0xFF,0x50,0xC1,0x78,0xFF,0x9D,0xDB,0xB2,0xFF,0xFC,0xFE,0xFD,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFE,0xFE,0xFF,0xF3,0xE5,0xDB,0xFF,0xD4,0xA1,0x71,0xFF,0xCE,0x93,0x57,0xFF,0xD0,0x96,0x55,0xFF,0xB3,0x7A,0x52,0xFF,0x79,0x50,0x4E,0xFF,0x53,0x51,0x50,0xFF,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x52,0x52,0x52,0x8F,0x5B,0x51,0x51,0xFF,0x86,0x52,0x4F,0xFF,0xAA,0x66,0x57,0xFF,0xB5,0x6E,0x59,0xFF,0xB5,0x70,0x5A,0xFF,0xC2,0x87,0x75,0xFF,0xEC,0xD8,0xD3,0xFF,0xFE,0xFC,0xFC,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFE,0xFF,0xFF,0xFF,0xBA,0xE7,0xD0,0xFF,0x81,0xD5,0xAA,0xFF,0xEB,0xF8,
0xF1,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFE,0xFE,0xFD,0xFF,0xF2,0xE3,0xD8,0xFF,0xD3,0xA0,0x75,0xFF,0xCC,0x90,0x58,0xFF,0xCE,0x91,0x57,0xFF,0xC2,0x87,0x55,0xFF,0x8C,0x59,0x4F,0xFF,0x5E,0x51,0x51,0xFF,0x51,0x51,0x51,0x3F,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x53,0x53,0x53,0x6F,0x68,0x51,0x51,0xFF,0x8F,0x55,0x50,0xFF,0xAF,0x6A,0x59,0xFF,0xB5,0x6E,0x5A,0xFF,0xB5,0x70,0x59,0xFF,0xBE,0x7F,0x6B,0xFF,0xDE,0xBD,0xB4,0xFF,0xFB,0xF6,0xF5,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xF2,0xFB,0xF7,0xFF,0xE0,0xF5,0xEC,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF,0xFC,0xF9,0xF7,0xFF,0xE5,0xC7,0xB4,0xFF,0xCE,0x98,0x6E,0xFF,0xC9,0x8C,0x59,0xFF,0xCB,0x8E,0x58,0xFF,0xC8,0x8C,0x56,0xFF,0x97,0x60,0x50,0xFF,0x67,0x52,0x51,0xFF,0x52,0x52,0x54,0xCF,0x55,0x55,0x55,0x0F,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x51,0x51,
0x51,0x2F,0x54,0x54,0x54,0xEF,0x6C,0x52,0x51,0xFF,0x91,0x56,0x50,0xFF,0xAE,0x69,0x58,0xFF,0xB4,0x6E,0x59,0xFF,0xB5,0x70,0x5A,0xFF,0xB6,0x71,0x59,0xFF,0xC2,0x87,0x73,0xFF,0xE0,0xC1,0xB6,0xFF,0xF2,0xE4,0xE0,0xFF,0xF9,0xF2,0xF0,0xFF,0xFC,0xF7,0xF7,0xFF,0xFC,0xF8,0xF7,0xFF,0xFC,0xF8,0xF7,0xFF,0xF9,0xF3,0xF0,0xFF,0xF4,0xE7,0xE1,0xFF,0xE6,0xCB,0xBC,0xFF,0xCF,0x9B,0x7A,0xFF,0xC5,0x86,0x5B,0xFF,0xC6,0x87,0x59,0xFF,0xC8,0x89,0x58,0xFF,0xC0,0x84,0x57,0xFF,0x9A,0x62,0x50,0xFF,0x6F,0x51,0x50,0xFF,0x56,0x54,0x54,0xCF,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x51,0x51,0x51,0x2F,0x57,0x54,0x55,0xFF,0x6A,0x52,0x52,0xFF,0x8D,0x54,0x4F,0xFF,0xA8,0x64,0x56,0xFF,0xB5,0x6E,0x59,0xFF,0xB6,0x70,0x5A,0xFF,0xB7,0x72,0x5A,0xFF,0xB9,0x76,0x5D,0xFF,0xBF,0x81,0x68,0xFF,0xC6,0x8D,0x76,0xFF,0xCA,0x95,0x7E,0xFF,0xCB,0x96,0x7E,0xFF,0xCB,0x97,0x7E,0xFF,0xC9,0x93,0x76,0xFF,0xC6,0x8A,0x69,0xFF,0xC2,0x83,0x5E,0xFF,0xC2,0x82,
0x59,0xFF,0xC4,0x84,0x59,0xFF,0xC4,0x86,0x59,0xFF,0xB6,0x79,0x56,0xFF,0x91,0x5B,0x4F,0xFF,0x6C,0x53,0x52,0xFF,0x57,0x55,0x55,0xCF,0x55,0x55,0x55,0x0F,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x57,0x57,0x57,0x2F,0x55,0x55,0x55,0xBF,0x66,0x54,0x54,0xFF,0x81,0x51,0x4F,0xFF,0x9B,0x5D,0x53,0xFF,0xAE,0x6A,0x57,0xFF,0xB5,0x70,0x59,0xFF,0xB7,0x72,0x5A,0xFF,0xB8,0x73,0x59,0xFF,0xB9,0x74,0x59,0xFF,0xBA,0x77,0x59,0xFF,0xBB,0x78,0x59,0xFF,0xBC,0x79,0x59,0xFF,0xBE,0x7B,0x59,0xFF,0xBE,0x7D,0x59,0xFF,0xC0,0x7E,0x58,0xFF,0xC0,0x7F,0x58,0xFF,0xBA,0x7C,0x57,0xFF,0xA6,0x6B,0x53,0xFF,0x84,0x54,0x4F,0xFF,0x67,0x54,0x53,0xFF,0x57,0x56,0x56,0xCF,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x57,0x57,
0x57,0x6F,0x5C,0x56,0x56,0xFF,0x6D,0x54,0x53,0xFF,0x86,0x53,0x50,0xFF,0x98,0x59,0x50,0xFF,0xA7,0x65,0x55,0xFF,0xB1,0x6E,0x58,0xFF,0xB8,0x73,0x59,0xFF,0xB9,0x74,0x59,0xFF,0xBA,0x77,0x59,0xFF,0xBB,0x78,0x59,0xFF,0xBC,0x7A,0x5A,0xFF,0xB7,0x76,0x58,0xFF,0xAE,0x6F,0x55,0xFF,0x9D,0x61,0x51,0xFF,0x88,0x56,0x50,0xFF,0x70,0x54,0x52,0xFF,0x5D,0x56,0x57,0xFF,0x59,0x55,0x55,0x3F,0x55,0x55,0x55,0x0F,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x57,0x57,0x57,0x8F,0x58,0x58,0x58,0xBF,0x5F,0x57,0x57,0xBF,0x6A,0x55,0x55,0xFF,0x7B,0x52,0x50,0xFF,0x84,0x54,0x50,0xFF,0x8D,0x57,0x50,0xFF,0x93,0x5B,0x51,0xFF,0x97,0x5E,0x51,0xFF,0x94,0x5C,0x51,0xFF,0x8E,0x58,0x50,0xFF,0x85,0x54,0x50,0xFF,0x7C,0x52,0x50,0xFF,0x6B,0x55,0x54,0xFF,0x5F,0x57,0x57,0xCF,0x58,0x58,0x58,0xBF,0x57,0x57,0x57,0x2F,0x00,0x00,
0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x55,0x55,0x55,0x15,0x58,0x5A,0x5A,0x7F,0x8F,0x8C,0x8C,0xEA,0x9D,0x91,0x91,0xFE,0xA1,0x8F,0x8F,0xFE,0xA5,0x8F,0x8F,0xFE,0xA5,0x8F,0x8E,0xFE,0xA4,0x8F,0x8F,0xFE,0xA0,0x8F,0x8F,0xFE,0x9B,0x91,0x91,0xFE,0x8E,0x8C,0x8C,0xEA,0x58,0x58,0x58,0x7F,0x59,0x59,0x59,0x14,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xFF,0xE0,0x0F,0xFF,0xFE,0x00,0x01,0xFF,0xFE,0x00,0x00,0xFF,0xF8,0x00,0x00,0x3F,0xF0,0x00,0x00,0x1F,0xE0,0x00,0x00,0x0F,0xE0,0x00,0x00,0x07,0x80,0x00,0x00,0x07,0x80,0x00,0x00,0x03,0x80,0x00,0x00,0x01,0x00,0x00,
0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x80,0x00,0x00,0x01,0x80,0x00,0x00,0x03,0x80,0x00,0x00,0x07,0xE0,0x00,0x00,0x07,0xE0,0x00,0x00,0x0F,0xF0,0x00,0x00,0x1F,0xF8,0x00,0x00,0x3F,0xFE,0x00,0x00,0xFF,0xFE,0x00,0x01,0xFF,0xFF,0xE0,0x0F,0xFF
          };
            GenerateBinaryFile(DataHelper.PROXY_ICON, gapIcon);
        }


    }
}
