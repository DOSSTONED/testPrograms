using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace ClassSlideBar
{
    public partial class UserControlSB : UserControl
    {
        static Int16 TouchInPlace = 0;
        static Int16 TouchOutPlace = 0;

        private void PressMediaButton(byte place_byte, byte action_byte)
        {
            /// 基本不动，在中部点击表示暂停/播放
            /// 
            if (action_byte == 5 && Math.Abs(place_byte - 127) < 25)
            {
                WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.MEDIA_PLAY_PAUSE);
                TouchInPlace = 0;
                TouchOutPlace = 0;
                return;
            }

            if (action_byte == 1)    // 触摸起点
                TouchInPlace = place_byte;
            if (action_byte == 2)   //  触摸离开
            {
                TouchOutPlace = place_byte;




                /// 从左向右划
                /// 下一首歌
                if (TouchOutPlace - TouchInPlace > 60)
                {
                    WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.MEDIA_NEXT_TRACK);
                    TouchInPlace = 0;
                    TouchOutPlace = 0;
                }

                /// 从右向左划
                /// 前一首歌
                if (TouchOutPlace - TouchInPlace < -60)
                {
                    WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.MEDIA_NEXT_TRACK);
                    TouchInPlace = 0;
                    TouchOutPlace = 0;
                }
            }


        }
    }
}
