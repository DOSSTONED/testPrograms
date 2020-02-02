using SBarHook;
using System.Windows.Forms;
using System;
using CoreAudioApi;


namespace SliderBar_NoDisplay
{
    class Program
    {
        static SlideBar sb = null;
        static private MMDevice device = null;
        static private bool isLightBreathOn = true;
        //const int totalVolumeNumber = 3;
        //static float[] volumes = new float[totalVolumeNumber];


        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            sb = new SlideBar(true);
            sb.sbArriveEvent += new SlideBarEventHandler(sb_sbArriveEvent);
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
            device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            //sb.SetSlideBarStatus(1, 0);
            //for (int i = 0; i < volumes.Length;i++ )
            //{
            //    volumes[i] = device.AudioEndpointVolume.MasterVolumeLevelScalar;
            //}
            Application.Run();
        }

        static void sb_sbArriveEvent(SlideBar.SlideBarData sbData)
        {

            
            if (sbData.bPosition < 10)
            {
                if (sbData.bAction == 5)
                {
                    if (isLightBreathOn)
                    {
                        sb.SetSlideBarStatus(1, 1);
                        isLightBreathOn = !isLightBreathOn;
                    }
                    else
                    {
                        sb.SetSlideBarStatus(1, 0);
                        isLightBreathOn = !isLightBreathOn;
                    }
                }
                return;
            }
            if (device != null)
            {
                float inVolume = 0;
                if (sbData.bPosition < 30)
                    inVolume = 0;
                else
                    inVolume = (float)(sbData.bPosition - 30) / 225;

                device.AudioEndpointVolume.MasterVolumeLevelScalar = inVolume;
            }
             

        }

        //static float inputVolume(float input)
        //{
        //    float sum = 0;
        //    for (int i = 1; i < volumes.Length; i++)
        //    {
                
        //        volumes[i] = volumes[i - 1];
        //        sum += volumes[i];
        //    }
        //    volumes[0] = input;
        //    sum += input;
        //    return sum / volumes.Length;
        //}

    }
}
