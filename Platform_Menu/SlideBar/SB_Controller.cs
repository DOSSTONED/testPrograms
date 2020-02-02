using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SBarHook;
using CoreAudioApi;

namespace SlideBar_Controller
{
    public partial class SB_Controller : Form
    {
        /*
        /// <summary>
        /// Slidebar working mode
        /// </summary>
        public enum SBMode
        {
            //[Description("Nothing to do.")]
            Nothing,
            //[Description("Volume Control.")]
            VolumeControl,
            //[Description("Change music to the previous or next track.")]
            ChangeMusicTrackControl,
            //[Description("Brightness Control.")]
            BrightnessControl
        }
        */

        /// <summary>
        /// Scale the volume (0-255) to (0-1)
        /// Map 153 to 0.3, other is linear transform.
        /// </summary>
        /// <param name="volume">Volume wanna set. 255 is max.</param>
        /// <returns>Scaled volume.</returns>
        public static float ScaleSlideBarPlaceForVolume(byte volume)
        {
            float ret = 0;
            ret = (float)volume / 255; // no scale

            if (ret < 0.6)
                ret = ret / 2;
            else
                ret = (ret - 0.6f) * 7 / 4 + 0.3f;

            return ret;
        }

        private SlideBar sb = null;
        private Label[] labelIOBytes = new Label[128];
        private Label[] labelEvtBytes = new Label[64];
        private Label[] labelInputBytes = new Label[128];
        private byte[] rawInputBytes = new byte[128];

        /// <summary>
        /// Initialise the slidebar.
        /// </summary>
        public SB_Controller()
        {
            InitializeComponent();
            
        }

        private void updateLabels(byte[] EvtBytes)
        {
            byte[] IObytes = sb.GetSlideBarStatus();
            for (int i = 0; i < IObytes.Length; i++)
            {
                labelIOBytes[i].Text = IObytes[i].ToString("X2");
            }


            for (int i = 0; i < labelInputBytes.Length; i++)
            {
                labelInputBytes[i].Text = rawInputBytes[i].ToString("X2");
            }
            
            if (EvtBytes == null)
            {
                return;
            }
            SlideBar.Event sbEvt = (SlideBar.Event)EvtBytes[1];
            for (int i = 0; i < EvtBytes.Length; i++)
            {
                labelEvtBytes[i].Text = EvtBytes[i].ToString("X2");
            }

        }

        void sb_sbArriveEvent(SlideBar.SlideBarData sbData)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    labelbnAction.Text = sbData.bAction.ToString();
                    labelbnBreathEffect.Text = sbData.bBreathEffect.ToString();
                    labelbnEnabled.Text = sbData.bEnabled.ToString();
                    labelbnEvent.Text = sbData.bEvent.ToString();
                    labelbnLightEffect.Text = sbData.bLightEffect.ToString();
                    labelbnPosition.Text = sbData.bPosition.ToString();
                    labelbnSpeed.Text = sbData.bCurrentSpeed.ToString();
                    // Update Labels
                    updateLabels(sbData.RawBytes);
                    
                });
            }
        }

        private void SB_Controller_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sb != null)
            {
                sb.sbArriveEvent -= sb_sbArriveEvent;
                sb.StopAllEventWatcher();
            }
        }

        private void SB_Controller_Load(object sender, EventArgs e)
        {
            sb = new SlideBar(true);
            sb.sbArriveEvent += new SlideBarEventHandler(sb_sbArriveEvent);
            byte[] IObytes = sb.GetSlideBarStatus();
            foreach (SlideBar.Event sbEvt in Enum.GetValues(typeof(SlideBar.Event)))
            {
                CheckBox cb = new CheckBox();
                cb.Text = sbEvt.ToString();
                /// add the checkstatus to current checkbox
                switch (sbEvt)
                {
                    case SlideBar.Event.SlideBarLightSwitch:
                    case SlideBar.Event.ServiceKey:
                        cb.Checked = (IObytes[64 + (int)sbEvt] != 0);
                        break;
                    case SlideBar.Event.AP00:
                    case SlideBar.Event.AP01:
                    case SlideBar.Event.AP02:
                    case SlideBar.Event.AP03:
                    case SlideBar.Event.AP04:
                    case SlideBar.Event.AP05:
                    case SlideBar.Event.AP06:
                    case SlideBar.Event.AP07:
                        cb.Checked = (((int)IObytes[66]) >> ((int)sbEvt - 8)) % 2 != 0;
                        break;

                }
                cb.CheckedChanged += new EventHandler(cb_CheckedChanged);
                flowLayoutPanelControls.Controls.Add(cb);
            }

            for (int i = 0; i < IObytes.Length; i++)
            {
                labelIOBytes[i] = new Label();
                labelIOBytes[i].Text = IObytes[i].ToString("X2");
                labelIOBytes[i].Width = 21;
                //labelIOBytes[i].Click += new EventHandler(SB_Controller_Click);
                labelIOBytes[i].TextChanged += new EventHandler(SB_Controller_TextChanged);
                flowLayoutPanelIOBytes.Controls.Add(labelIOBytes[i]);

                labelInputBytes[i] = new Label();
                labelInputBytes[i].Width = 21;
                labelInputBytes[i].Text = "00";
                labelInputBytes[i].Click += new EventHandler(SB_Controller_Click);
                labelInputBytes[i].TextChanged += new EventHandler(SB_Controller_TextChanged);
                flowLayoutPanelInputBytes.Controls.Add(labelInputBytes[i]);
            }
            for (int i = 0; i < labelEvtBytes.Length; i++)
            {
                labelEvtBytes[i] = new Label();
                labelEvtBytes[i].Width = 21;
                labelEvtBytes[i].TextChanged += new EventHandler(SB_Controller_TextChanged);
                flowLayoutPanelEventBytes.Controls.Add(labelEvtBytes[i]);
            }

            //labelInputBytes[8].Text = "09";
            //labelInputBytes[85].Text = "01";
            //labelInputBytes[86].Text = "01";
            //labelInputBytes[66].Text = "01";
            //labelInputBytes[16].Text = "01";
            //labelInputBytes[10].Text = "01";
            //labelInputBytes[9].Text = "03";
            //labelInputBytes[0].Text = "01";
            //labelInputBytes[1].Text = "10";



            //labelInputBytes[0].ForeColor = Color.Blue;
            //labelInputBytes[1].ForeColor = Color.Blue;
            //labelInputBytes[8].ForeColor = Color.Blue;
            //labelInputBytes[9].ForeColor = Color.Blue;
            //labelInputBytes[10].ForeColor = Color.Blue;
            //labelInputBytes[16].ForeColor = Color.Blue;
            //labelInputBytes[66].ForeColor = Color.Blue;
            //labelInputBytes[85].ForeColor = Color.Blue;
            //labelInputBytes[86].ForeColor = Color.Blue;
        }

        void SB_Controller_Click(object sender, EventArgs e)
        {
            MouseEventArgs mea = e as MouseEventArgs;
            if (mea.Button == MouseButtons.Right)
            {
                ((Label)sender).Text = ((Convert.ToByte(((Label)sender).Text, 16) + 255) % 256).ToString("X2");
            }
            else
            {
                ((Label)sender).Text = ((Convert.ToByte(((Label)sender).Text, 16) + 1) % 256).ToString("X2");
            }
            //byte[] put = new byte[128];
            for (int i = 0; i < rawInputBytes.Length; i++)
                rawInputBytes[i] = Convert.ToByte(labelInputBytes[i].Text, 16);
            sb.WriteWMIACPI_IO(rawInputBytes);
            updateLabels(null);
        }

        void SB_Controller_TextChanged(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.Red;
        }

        void cb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb == null) return;
            foreach (SlideBar.Event sbEvt in Enum.GetValues(typeof(SlideBar.Event)))
            {
                if (cb.Text == sbEvt.ToString())
                {
                    rawInputBytes = sb.SetSlideBarStatus(sbEvt, cb.Checked);
                    break;
                }
            }
            updateLabels(null);
        }

        private void buttonResetColors_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < labelEvtBytes.Length; i++)
                labelEvtBytes[i].ForeColor = Color.Black;
            for (int i = 0; i < labelIOBytes.Length; i++)
                labelIOBytes[i].ForeColor = Color.Black;
        }

        private void buttonRefreshIOBytes_Click(object sender, EventArgs e)
        {
            updateLabels(null);
        }

        
    }
}
