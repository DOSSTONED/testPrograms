﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;


namespace NetWorkControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //NetWorkList();
        }
        private static void Disconnect()
        {

            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");

            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {

                objMO.InvokeMethod("ReleaseDHCPLease", null, null);

            }

        }

        private static void Reconnect()
        {

            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");

            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {

                objMO.InvokeMethod("RenewDHCPLease", null, null);

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        /*
         * 
        /// <summary>
        /// 网卡列表
        /// </summary>
        public void NetWorkList()
        {
            string manage = "SELECT * From Win32_NetworkAdapter";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(manage);
            ManagementObjectCollection collection = searcher.Get();
            List<string> netWorkList = new List<string>();

            foreach (ManagementObject obj in collection)
            {
                netWorkList.Add(obj["Name"].ToString());
                
            }
           this.cmbNetWork.DataSource = netWorkList;

        }

        /// <summary>
        /// 禁用网卡
        /// </summary>5
        /// <param name="netWorkName">网卡名</param>
        /// <returns></returns>
        public bool DisableNetWork(ManagementObject network)
        {
            try
            {
                network.InvokeMethod("Disable", null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 启用网卡
        /// </summary>
        /// <param name="netWorkName">网卡名</param>
        /// <returns></returns>
        public bool EnableNetWork(ManagementObject network)
        {
            try
            {
                network.InvokeMethod("Enable", null );
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        /// <summary>
        /// 网卡状态
        /// </summary>
        /// <param name="netWorkName">网卡名</param>
        /// <returns></returns>
        public bool NetWorkState(string netWorkName)
        {
            string netState = "SELECT * From Win32_NetworkAdapter";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(netState);
            ManagementObjectCollection collection = searcher.Get();
            foreach (ManagementObject manage in collection)
            {
                if (manage["Name"].ToString() == netWorkName )
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 得到指定网卡
        /// </summary>
        /// <param name="networkname">网卡名字</param>
        /// <returns></returns>
        public ManagementObject NetWork(string networkname)
        {
            string netState = "SELECT * From Win32_NetworkAdapter";

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(netState);
            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementObject manage in collection)
            {
                if (manage["Name"].ToString() == networkname)
                {
                    return manage;
                }
            }

            
            return null;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (NetWorkState(this.cmbNetWork.SelectedValue.ToString()))
            {
                if (!EnableNetWork(NetWork(this.cmbNetWork.SelectedValue.ToString())))
                {
                    MessageBox.Show("开启网卡失败!");
                }
                else
                {
                    MessageBox.Show("开启网卡成功!");
                }
            }
            else
            {
                MessageBox.Show("网卡己开启!");
            }

            NetWorkList();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

            if (NetWorkState(this.cmbNetWork.SelectedValue.ToString()))
            {
                if (!DisableNetWork(NetWork(this.cmbNetWork.SelectedValue.ToString())))
                {
                    MessageBox.Show("禁用网卡失败!");
                }
                else
                {
                    MessageBox.Show("禁用网卡成功!");
                }
            }
            else
            {
                MessageBox.Show("网卡己禁用!");
            }

            NetWorkList();
        }
        */
    }
}
