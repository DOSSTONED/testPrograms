using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOSSTONED_For_Personal_Use_
{
    class Lesson
    {

        internal string LName;
        private string LTeacher;
        private string LRoom;
        private int LBegin;
        private int LEnd;
        private string LType;

        public Lesson()
        {
            LName = "N/A";
            LTeacher = "N/A";
            LRoom = "N/A";
            LBegin = 0; 
            LEnd = 0;
            throw new System.NotImplementedException();
        }
        public Lesson(string LessonName,string LessonRoom,string LessonTeacher,int LessonBegin,int LessonEnd,string LessonType)
        {
            LName = LessonName;
            LTeacher = LessonTeacher;
            LRoom = LessonRoom;
            LBegin = LessonBegin;
            LEnd = LessonEnd;
            LType=LessonType;
            throw new System.NotImplementedException();
        }

        public Lesson(string LessonName, string LessonRoom, string LessonTeacher, int LessonBegin, int LessonEnd)
        {
            LName = LessonName;
            LTeacher = LessonTeacher;
            LRoom = LessonRoom;
            LBegin = LessonBegin;
            LEnd = LessonEnd;
            LType = "主讲";
            //throw new System.NotImplementedException();
        }

        public void LessonDetail()
        {
            System.Windows.Forms.MessageBox.Show("课程名称："+LName +"\n开始节次：第"+LBegin.ToString()+ "节\n结束节次：第"+ LEnd.ToString()+"节\n教室："+LRoom+ "\n上课类型："+LType+ "\n教师姓名："+LTeacher, LName);
            //throw new System.NotImplementedException();
        }
    }
}
