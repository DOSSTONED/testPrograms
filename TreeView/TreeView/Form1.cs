////////////////////////////////////////////
//
//   Code From : http://tieba.baidu.com/f?kz=307058692
//
////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
namespace TreeView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /*
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.treeView.Nodes.Add("123");
            TreeNode tRoot = new TreeNode("我的电脑", 0, 0);
            this.treeView.Nodes.Add(tRoot);
            string[] drivers = null;
            drivers = System.IO.Directory.GetLogicalDrives();
            foreach (string s in drivers)
            {
                TreeNode NodeTemp = new TreeNode(s);
                tRoot.Nodes.Add(NodeTemp);
            }
        }
        */
        
        
        private void getSubNode(TreeNode PathName, bool isEnd)
        {
            if (!isEnd)
                return; //exit this 
            TreeNode curNode;
            DirectoryInfo[] subDir;
            DirectoryInfo curDir = new DirectoryInfo(PathName.FullPath);
            try
            {
                subDir = curDir.GetDirectories();
                foreach (DirectoryInfo d in subDir)
                {
                    curNode = new TreeNode(d.Name);
                    PathName.Nodes.Add(curNode);
                    getSubNode(curNode, true);//由原来的FALSE改为TRUE之后没有出现死循环，反而成功实现了所有符合条件的目录列出的要求
                    
                }
            }
            catch { }

        }

        /*
        private void treeView_AfterExpand_1(object sender, TreeViewEventArgs e)
        {
            MessageBox.Show("hi");
            try
            {
                foreach (TreeNode tn in e.Node.Nodes)
                {
                    if (!tn.IsExpanded)
                        getSubNode(tn, true);
                }
            }
            catch { ;}
        }

        */
        private void Form1_Load(object sender, EventArgs e)
        {
            //获取逻辑驱动器 
            string[] LogicDrives = System.IO.Directory.GetLogicalDrives();
            TreeNode[] cRoot = new TreeNode[LogicDrives.Length];
            for (int i = 0; i < LogicDrives.Length; i++)
            {
                if (Directory.Exists(LogicDrives[i] + "DOSSTONED\\"))
                {
                    TreeNode drivesNode = new TreeNode(LogicDrives[i] + "DOSSTONED\\");
                    treeView.Nodes.Add(drivesNode);
                    if (LogicDrives[i] != "A:\\" && LogicDrives[i] != "B:\\")
                        getSubNode(drivesNode, true);
                }
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listView.Items.Clear();
            DirectoryInfo selDir = new DirectoryInfo(e.Node.FullPath);
            DirectoryInfo[] listDir;
            FileInfo[] listFile;
            try
            {
                listDir = selDir.GetDirectories();
                listFile = selDir.GetFiles();
                foreach (DirectoryInfo d in listDir)
                    listView.Items.Add(d.Name,32);
                foreach (FileInfo d in listFile)
                    listView.Items.Add(d.Name);
            }
            catch { }

        }


    }
}
