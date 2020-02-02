using System;
using System.Xml;
using System.IO;
using System.Timers;


namespace DOSSTONED_BG_Service
{
    static class Record_Brightness
    {
        static Timer timer = null;
        /// <summary>
        /// The main entry point for the part.
        /// </summary>
        public static void Entrance()
        {
            timer = new Timer(1000 * 360);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int curBri = GetBrightness();
            if (curBri >= 0 && curBri < 256)
                写入Brightness((byte)curBri);
        }

        static int GetBrightness()
        {
            //define scope (namespace)
            System.Management.ManagementScope s = new System.Management.ManagementScope("root\\WMI");

            //define query
            System.Management.SelectQuery q = new System.Management.SelectQuery("WmiMonitorBrightness");

            //output current brightness
            System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher(s, q);

            System.Management.ManagementObjectCollection moc = mos.Get();

            //store result
            byte curBrightness = 0;


            ///
            /// 远程登陆的时候这会出错！！！
            try
            {
                foreach (System.Management.ManagementObject o in moc)
                {
                    curBrightness = (byte)o.GetPropertyValue("CurrentBrightness");
                    break; //only work on the first object
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.StackTrace, ex.Message);
                return -1;
            }
            finally
            {
                moc.Dispose();
                mos.Dispose();
            }

            return curBrightness;
        }

        static string profilePath = Path.GetTempPath() + "DOSSTONED_Brightness.xml";
        static bool 写入Brightness(byte brightness_in)
        {
            //XmlTextWriter writer = null;

            //AlternateDataStreamInfo dataStream = FileSystem.GetAlternateDataStream(profilePath, "profile");
            string curHour = DateTime.UtcNow.Hour.ToString();


            if (File.Exists(profilePath))
            {
                XmlDocument xmldoc = new XmlDocument();
                using (var fsRead = new StreamReader(profilePath))
                {
                    xmldoc.Load(fsRead);
                }
                XmlNodeList brightnessItems = xmldoc.ChildNodes[1].ChildNodes[0].ChildNodes;
                int i = 0;

                for (i = 0; i < brightnessItems.Count; i++)
                {
                    if (brightnessItems[i].Attributes["UTCHour"].Value.ToString() == curHour)
                        break;
                }
                if (i >= brightnessItems.Count)
                {
                    /// no record represented
                    /// 

                    xmldoc.AppendChild(xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", ""));
                    XmlElement root = xmldoc.CreateElement("DOSSTONED_RECORD_TEST");
                    XmlElement Brightness = xmldoc.CreateElement("Brightness");
                    //XmlElement users = xmldoc.CreateElement("USERS");
                    root.AppendChild(Brightness);
                    //root.AppendChild(users);

                    xmldoc.AppendChild(root);


                    // New XML Element Created
                    XmlElement curBrightnessItem = xmldoc.CreateElement("BrightnessItem");

                    // New Attribute Created
                    XmlAttribute curBrightnessHour = xmldoc.CreateAttribute("Hour");
                    XmlAttribute curAverageBrightness = xmldoc.CreateAttribute("AverageBrightness");
                    XmlAttribute curBrightnessCount = xmldoc.CreateAttribute("Count");
                    //XmlAttribute newUserV6 = xmldoc.CreateAttribute("v6");
                    // Value given for the new attribute
                    curBrightnessHour.Value = curHour;//资料.用户名;
                    curAverageBrightness.Value = brightness_in.ToString();//资料.密码;

                    curBrightnessCount.Value = "1";// 资料.v4登录.ToString();
                    //newUserV6.Value = 资料.v6登录.ToString();

                    // Attach the attribute to the xml element
                    curBrightnessItem.SetAttributeNode(curBrightnessHour);
                    curBrightnessItem.SetAttributeNode(curAverageBrightness);
                    curBrightnessItem.SetAttributeNode(curBrightnessCount);
                    //curBrightnessItem.SetAttributeNode(newUserV6);
                    Brightness.AppendChild(curBrightnessItem);

                    using (var fsWrite = new StreamWriter(profilePath))//profileInfo.GetAlternateDataStream(profileStream).OpenWrite())
                    {


                        //xmldoc = entity;

                        xmldoc.DocumentElement.ChildNodes[1].ChildNodes[0].AppendChild(curBrightnessItem);



                        xmldoc.Save(fsWrite);
                    }
                }
                else
                {
                    int oriCount = Convert.ToInt32(brightnessItems[i].Attributes["Count"].Value);
                    double oriAverge = Convert.ToDouble(brightnessItems[i].Attributes["AverageBrightness"].Value);
                    oriAverge = (oriAverge * oriCount + brightness_in) / (oriCount + 1);
                    oriCount++;

                    brightnessItems[i].Attributes["AverageBrightness"].Value = oriAverge.ToString();
                    brightnessItems[i].Attributes["Count"].Value = oriCount.ToString();
                    using (var fsWrite = new StreamWriter(profilePath))
                    {
                        xmldoc.Save(fsWrite);
                    }
                }




                //fsxml.Dispose();
                //fs.Close();
                //fsxml.Close();
            }
            else
            {
                XmlDocument xmldoc = new XmlDocument();

                xmldoc.AppendChild(xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", ""));
                XmlElement root = xmldoc.CreateElement("DOSSTONED_RECORD_TEST");
                XmlElement Brightness = xmldoc.CreateElement("Brightness");
                //XmlElement users = xmldoc.CreateElement("USERS");
                root.AppendChild(Brightness);
                //root.AppendChild(users);

                xmldoc.AppendChild(root);


                // New XML Element Created
                XmlElement curBrightnessItem = xmldoc.CreateElement("BrightnessItem");

                // New Attribute Created
                XmlAttribute curBrightnessHour = xmldoc.CreateAttribute("UTCHour");
                XmlAttribute curAverageBrightness = xmldoc.CreateAttribute("AverageBrightness");
                XmlAttribute curBrightnessCount = xmldoc.CreateAttribute("Count");
                //XmlAttribute newUserV6 = xmldoc.CreateAttribute("v6");
                // Value given for the new attribute
                curBrightnessHour.Value = curHour;//资料.用户名;
                curAverageBrightness.Value = brightness_in.ToString();//资料.密码;
                /*!!!!*/
                curBrightnessCount.Value = "1";
                //newUserV6.Value = 资料.v6登录.ToString();

                // Attach the attribute to the xml element
                curBrightnessItem.SetAttributeNode(curBrightnessHour);
                curBrightnessItem.SetAttributeNode(curAverageBrightness);
                curBrightnessItem.SetAttributeNode(curBrightnessCount);
                //curBrightnessItem.SetAttributeNode(newUserV6);

                using (var fsWrite = new StreamWriter(profilePath))// profileInfo.GetAlternateDataStream(profileStream).OpenWrite())
                {
                    xmldoc.DocumentElement.ChildNodes[1].ChildNodes[0].AppendChild(curBrightnessItem);
                    xmldoc.Save(fsWrite);
                }
            }
            return false;
        }
    }
}

