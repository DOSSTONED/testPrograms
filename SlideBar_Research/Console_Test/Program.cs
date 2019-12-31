using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SBarHook;


namespace Console_Test
{
    class Program
    {
        static SlideBar sbController = new SlideBar(true);
        static bool directionFromLeftToRight = true;
        static byte formerPosition = 0;
        static Int64 totalPosition = 0;

        static void Main(string[] args)
        {
            sbController.sbArriveEvent += new SlideBarEventHandler(sbController_sbArriveEvent);



            Console.ReadKey();
        }

        static void sbController_sbArriveEvent(SlideBar.SlideBarData sbData)
        {
            //if (sbData.bCurrentSpeed < 4) return;
            directionFromLeftToRight = sbData.bPosition >= formerPosition;
            formerPosition = sbData.bPosition;
            if (sbData.bAction == 1) return;
            if (sbData.bAction == 2)
            {
                //speedAnimation(sbData.bCurrentSpeed * 100);
                //return;
            }
            
            
            int speed = sbData.bCurrentSpeed * 100;
            int position = sbData.bPosition;
            if (directionFromLeftToRight)
            {
                totalPosition += speed+position;
            }
            else
            {
                totalPosition += -speed - position;
            }
            Console.WriteLine("Current Position: {0}", totalPosition);
        }

        //static void speedAnimation(int speed_in)
        //{
        //    int position = 0;
        //    int curSpeed = speed_in;
        //    int a = -10;
        //    //int t = speed_in / -a + 1;
        //    while (curSpeed > 0)
        //    {
        //        curSpeed = curSpeed + a;
        //        position += curSpeed;
        //        Console.WriteLine(position);
        //    }
        //}
    }
}
