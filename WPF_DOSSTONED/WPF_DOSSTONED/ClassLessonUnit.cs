using System.Windows;

namespace WPF_DOSSTONED
{
    class ClassLessonUnit : System.Windows.Controls.Button
    {

        private string LName;
        private string LTeacher;
        private string LRoom;
        private int LBegin;
        private int LEnd;
        private string LType;

        public ClassLessonUnit()
        {
            this.Content = "N/A";
            //DispBtn.Command.Execute(MessageBox.Show("hi"));
            LName = "N/A";
            LTeacher = "N/A";
            LRoom = "N/A";
            LBegin = 0;
            LEnd = 0;
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.Click += new System.Windows.RoutedEventHandler(this.BtnClick);

        }
        public void setClassLessonUnit(string LessonName, string LessonRoom, string LessonTeacher, int LessonBegin, int LessonEnd, string LessonType)
        {
            this.Content = LessonName;
            //DispBtn.Command.Execute(MessageBox.Show("hi"));
            LName = LessonName;
            LTeacher = LessonTeacher;
            LRoom = LessonRoom;
            LBegin = LessonBegin;
            LEnd = LessonEnd;
            LType = LessonType;
            this.HorizontalAlignment = HorizontalAlignment.Center;
        }

        public void setClassLessonUnit(string LessonName, string LessonRoom, string LessonTeacher, int LessonBegin, int LessonEnd)
        {
            setClassLessonUnit(LessonName, LessonRoom, LessonTeacher, LessonBegin, LessonEnd, "主讲");
            //this.Content = LessonName;
            //DispBtn.Command.Execute(MessageBox.Show("hi"));
            //LName = LessonName;
            //LTeacher = LessonTeacher;
            //LRoom = LessonRoom;
            //LBegin = LessonBegin;
            //LEnd = LessonEnd;
            //LType = "主讲";

            //throw new System.NotImplementedException();
        }
        public void BtnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("课程名称：\t" + LName + "\n开始节次：\t第" + LBegin.ToString() + "节\n结束节次：\t第" + LEnd.ToString() + "节\n教室：\t\t" + LRoom + "\n上课类型：\t" + LType + "\n教师姓名：\t" + LTeacher, LName);

        }
    }
}
