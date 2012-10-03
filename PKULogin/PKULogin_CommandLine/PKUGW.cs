using System;
using System.Net;
using System.IO;

class PKUGW
{
    /// <summary>
    /// 将各种参数组合起来，变为待POST的参数
    /// </summary>
    /// <param name="username">用户名</param>
    /// <param name="password">密码（明文）</param>
    /// <param name="seperater">分隔内容，是一个很长很奇怪的字符串</param>
    /// <param name="lt">登陆类型</param>
    /// <returns>组合好的参数</returns>
    string paraCombination(string username, string password, string seperater, LoginType lt)
    {
        string ret = string.Empty;
        ret =
            "username1=" + username +    //[19]
            "&password=" + password +    //[37]
            "&pwd_t=密码" + //%E5%AF%86%E7%A0%81" + // “密码”的编码    //[50]
            "&fwrd=" + lt.ToString() +   // Cernet Free    //[60]
            "&username=" + username +    //[80]
             seperater +  //[108]
             password +   //[116]
             seperater +  //[144]
             (int)lt;   //[146]
        return ret;
    }

    /// <summary>
    /// 参见 https://its.pku.edu.cn/netportal/funcs_UTF-8.js
    /// 原函数主要是加了时间判断，防止频繁刷页面。
    /// 功能为添加一个随机的sid。
    /// </summary>
    /// <param name="_c">原地址</param>
    /// <returns>添加过随机sid的地址</returns>
    string doaction(string _c)
    {
        Random ran = new Random();
        if (_c.IndexOf("?") != -1)
        {
            return _c + "&sid=" + ran.Next(1000);
        }
        else
        {
            return _c + "?sid=" + ran.Next(1000);
        }
    }

    /// <summary>
    /// 登陆主函数，用于登陆北大网关
    /// </summary>
    /// <param name="username">用户名</param>
    /// <param name="password">密码（明文）</param>
    /// <param name="lt">登陆类型</param>
    /// <returns>HTML文本，其内注释包含官方Client的数据内容。</returns>
    public string Login(string username, string password, LoginType lt)
    {
        return Login(username, password, lt, null);
    }


    public string Login(string username, string password, LoginType lt, IWebProxy proxy)
    {
        string ret = string.Empty;
        var cookies = new CookieContainer();

        ///
        /// Step1：登陆到窗口内
        /// 因为要保存Cookie，该部分没有用WebClient来实现
        /// 
        string dest = "https://its.pku.edu.cn/cas/login";

        var request = WebRequest.Create(dest) as HttpWebRequest;
        if (proxy != null)
            request.Proxy = proxy;
        request.CookieContainer = cookies;
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";

        using (var requestStream = request.GetRequestStream())
        using (var writer = new StreamWriter(requestStream))
        {
            writer.Write(paraCombination(username, password, 莫名其妙的字符串, lt));
        }

        using (var responseStream = request.GetResponse().GetResponseStream())
        using (var reader = new StreamReader(responseStream))
        {
            var result = reader.ReadToEnd();
            ret = result;
            /// 测试用
            //textBox1.Text = result;
            //Console.WriteLine(result);
        }

        ///
        /// Step2：打开网络连接
        /// 
        var response = WebRequest.Create(doaction("https://its.pku.edu.cn/netportal/" + LoginTypeToLink(lt))) as HttpWebRequest;
        if (proxy != null)
            response.Proxy = proxy;
        response.CookieContainer = cookies;
        response.Method = "GET";
        response.ContentType = "application/x-www-form-urlencoded";

        using (var responseStream = response.GetResponse().GetResponseStream())
        using (var reader = new StreamReader(responseStream))
        {
            var result = reader.ReadToEnd();
            ret = result;
            /// 测试用
            //textBox1.Text = result;
            //Console.WriteLine(result);
        }

        return ret;
    }

    public class IPGWCLIENT_INFO
    {
        public IPGWCLIENT_INFO(string key, string val)
        {
            Key = key;
            Value = val;
        }
        string _key;
        string _value;
        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }

    /// <summary>
    /// 将登陆类型转化为对应的字符串。
    /// 我没有用反射来实现。
    /// </summary>
    /// <param name="lt">待转化的登陆类型</param>
    /// <returns>该登陆类型对应的地址字符串</returns>
    string LoginTypeToLink(LoginType lt)
    {
        /// 详细内容，参见下面的注释或者登陆过后查看左边Frame的源代码。
        switch (lt)
        {
            case LoginType.free:
                return "ipgwopen";
            case LoginType.fee:
                return "ipgwopenall";
            case LoginType.Disconnect:
                return "ipgwclose";
            case LoginType.DisconnectAll:
                return "ipgwcloseall";
            default:
                throw new NotSupportedException("This type does NOT support convert to link." + lt.ToString());
        }
        //  <tr>
        //  <td><img src="images/dir_line1.gif" width="13" height="20"></td>
        //  <td><img src="images/mynetwork.gif" width="19" height="18"></td>
        //  <td width="90%"><a href="javascript:doaction('ipgwstate0.jsp',true);" title="查看当前网关连接状态&#13IP gateway status"><strong><span class="newRomanFont">IP Gateway</span></strong></a></td>
        //</tr>
        //<tr>
        //  <td><img src="images/dir_line2.gif" width="13" height="20"></td>
        //  <td><img src="images/dir_line1.gif" width="19" height="20"></td>
        //  <td><a href="javascript:doaction('ipgwopen',true);" title="点击打开网关，访问免费地址&#13Connect to CERNET free IP"><div>&nbsp;Connect <strong>free</strong> IP</div></a></td>
        //</tr>
        //<tr>
        //  <td><img src="images/dir_line2.gif" width="13" height="20"></td>
        //  <td><img src="images/dir_line1.gif" width="19" height="20"></td>
        //  <td><a href="javascript:doaction('ipgwopenall',true);" title="点击打开网关，访问收费+免费地址&#13Connect to Global IP"><div>&nbsp;Connect <strong>Global</strong> IP</div></a></td>
        //</tr>
        //<tr>
        //  <td><img src="images/dir_line2.gif" width="13" height="20"></td>
        //  <td><img src="images/dir_line1.gif" width="19" height="20"></td>
        //  <td><a href="javascript:doaction('ipgwclose',true);" title="点击断开本机的网关&#13Disconnect current connection"><div>&nbsp;Disconnect</div></a></td>
        //</tr>
        //<tr>
        //  <td><img src="images/dir_line2.gif" width="13" height="20"></td>
        //  <td><img src="images/dir_line1.gif" width="19" height="20"></td>
        //  <td><a href="javascript:doaction('ipgwcloseall',true);" title="点击断开登陆帐户对应的所有网关连接&#xA;若曾在其他机上使用网关&#13并且没有断开，可以使用此功能&#13Disconnect all"><div>&nbsp;Disconnect all</div></a></td>
    }

    /// <summary>
    /// 我也不知道这是干什么的，总之 https://its.pku.edu.cn 上面这个就是硬编码上去了，而且传参数的时候添加
    /// </summary>
    static string 莫名其妙的字符串 = "|;kiDrqvfi7d$v0p5Fg72Vwbv2;|";//"%7C%3BkiDrqvfi7d%24v0p5Fg72Vwbv2%3B%7C";

    /// <summary>
    /// 登陆类型
    /// </summary>
    public enum LoginType : int
    {
        /// <summary>
        /// 免费地址
        /// CERNET Free
        /// </summary>
        free = 12,  //CernetFree，免费地址
        /// <summary>
        /// 收费地址
        /// Global
        /// </summary>
        fee = 11,//Global_CernetNonFree，收费地址
        /// <summary>
        /// 不打开（没有地方用过这个选项）
        /// noopen
        /// </summary>
        noopen = 15,
        /// <summary>
        /// 断开所有连接
        /// Disconnect all connections.
        /// </summary>
        DisconnectAll = 13,
        /// <summary>
        /// 邮件登陆（功能没有实现）
        /// Mail login (Not implemented)
        /// </summary>
        MailLogin = 16,
        /// <summary>
        /// 断开本机连接，另一个方法是直接打开 https://its.pku.edu.cn/disconnetipgw.do
        /// Disconnect current IP connection, another way to do this is open the site above.
        /// </summary>
        Disconnect = 0  // this is not used.
    }
}

