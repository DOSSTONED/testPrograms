using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using ARSoft.Tools.Net.Dns;

namespace DNSServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                using (DnsServer server = new DnsServer(IPAddress.Any, 10, 10, ProcessQuery))
                {
                    server.ExceptionThrown += server_ExceptionThrown;
                    server.Start();
                }
            }
            catch (Exception ex)
            {
            }

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run();
        }

        static void server_ExceptionThrown(object sender, ExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }


        static DnsMessageBase ProcessQuery(DnsMessageBase message, IPAddress clientAddress, ProtocolType protocol)
        {
            message.IsQuery = false;

            DnsMessage query = message as DnsMessage;

            if ((query != null) && (query.Questions.Count == 1))
            {
                // send query to upstream server
                DnsQuestion question = query.Questions[0];
                DnsMessage answer = null;
                if (question.RecordType == RecordType.A)  // IPv4, so try IPv6 first then IPv4
                {
                    DnsClient dc = new DnsClient(IPAddress.Parse("2001:470:20::2"), 1000);
                    
                    answer = dc.Resolve(question.Name, RecordType.A6, question.RecordClass);
                    // if got an answer, copy it to the message sent to the client
                    if (answer != null)
                    {
                        foreach (DnsRecordBase record in (answer.AnswerRecords))
                        {
                            query.AnswerRecords.Add(record);
                        }
                        foreach (DnsRecordBase record in (answer.AdditionalRecords))
                        {
                            query.AnswerRecords.Add(record);
                        }

                        query.ReturnCode = ReturnCode.NoError;
                        return query;
                    }
                }

                answer = DnsClient.Default.Resolve(question.Name, question.RecordType, question.RecordClass);
                if (answer != null)
                {
                    foreach (DnsRecordBase record in (answer.AnswerRecords))
                    {
                        query.AnswerRecords.Add(record);
                    }
                    foreach (DnsRecordBase record in (answer.AdditionalRecords))
                    {
                        query.AnswerRecords.Add(record);
                    }

                    query.ReturnCode = ReturnCode.NoError;
                    return query;
                }

            }

            // Not a valid query or upstream server did not answer correct
            message.ReturnCode = ReturnCode.ServerFailure;
            return message;
        }
    }
}
