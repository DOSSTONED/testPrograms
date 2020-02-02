

using System.Net;
using System;
using System.Threading;
using System.Text;
using System.IO;
public class HttpAdapter
{
    HttpWebRequest request;
    byte[] bSendingFile;          //发送信息
    String strResponese;          //返回字符串
    AutoResetEvent resetEvent;    //信号量

    public string Destination;
    public CookieContainer cc = new CookieContainer();
    public HttpAdapter()
    {
        strResponese = String.Empty;
        resetEvent = new AutoResetEvent(false);
    }

    /// <summary>
    /// 执行Http连接操作
    /// </summary>
    /// <param name="xmlRequest">要发送的xml字符串</param>
    /// <returns>返回字符串</returns>
    public string ProcessHttp(String xmlRequest)
    {
        if (request != null)
            request.Abort();
        bSendingFile = Encoding.UTF8.GetBytes(xmlRequest);
        resetEvent.Reset();
        Thread t = new Thread(new ThreadStart(ProcessHttpWithAsyn));
        t.Start();
        resetEvent.WaitOne(30000);
        if (strResponese == String.Empty)
        {
            if (request != null)
                request.Abort();
            strResponese = "TimeOut";

        }
        return strResponese;
    }
    /// <summary>
    /// 执行Http连接操作
    /// </summary>
    /// <param name="xmlRequest">要发送的xml字符串</param>
    /// <param name="timeOut">设置超时时间(秒)</param>
    /// <returns></returns>
    public string ProcessHttp(String xmlRequest, int timeOut)
    {

        bSendingFile = Encoding.UTF8.GetBytes(xmlRequest);
        resetEvent.Reset();
        Thread t = new Thread(new ThreadStart(ProcessHttpWithAsyn));
        t.Start();
        resetEvent.WaitOne(timeOut);
        if (strResponese == String.Empty)
        {
            if (request != null)
                request.Abort();
            strResponese = "TimeOut";  //timeout 连接超时
        }
        return strResponese;
    }

    /// <summary>
    /// 异步方式调用Http协议
    /// </summary>
    public void ProcessHttpWithAsyn()
    {
        request = (HttpWebRequest)WebRequest.Create(Destination);
        request.ContentType = "text/xml;charset=UTF-8";
        request.Method = "POST";
        request.CookieContainer = cc;
        request.BeginGetRequestStream(RequestStreamCallback, request);
    }

    /// <summary>
    /// 返回用于将数据写入Stream，写入数据，并发送
    /// </summary>
    /// <param name="result"></param>
    private void RequestStreamCallback(IAsyncResult result)
    {
        HttpWebRequest request = (HttpWebRequest)(result.AsyncState);
        Stream requestStream = request.EndGetRequestStream(result);
        requestStream.Write(bSendingFile, 0, bSendingFile.Length);
        requestStream.Flush();
        requestStream.Close();
        request.BeginGetResponse(WebResponseCallback, request);
    }

    /// <summary>
    /// Http请求结束后调用的回调方法
    /// </summary>
    /// <param name="result"></param>
    private void WebResponseCallback(IAsyncResult result)
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)(result.AsyncState);
            WebResponse response = request.EndGetResponse(result) as HttpWebResponse;
            if (response != null)
            {
                Stream responseStream = response.GetResponseStream();
                using (StreamReader streamReader = new StreamReader(responseStream))
                {
                    strResponese = streamReader.ReadToEnd();
                }
                resetEvent.Set();
            }
        }
        catch (Exception e)
        {
            strResponese = "NetEx";
            resetEvent.Set();
        }
    }
}