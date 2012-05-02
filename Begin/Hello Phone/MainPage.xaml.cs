using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Hello_Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region Math Functions




        private List<String> OperatorsList = new List<string>();
        string[] OperatorsArray = new string[] {    // This must be the same with f(x,y)
                "/",
                "*",
                "-",
                "+",
        };


        /// <summary>
        /// Gives f(x,y)
        /// </summary>
        /// <param name="x">1st parameter</param>
        /// <param name="y">2nd parameter</param>
        /// <param name="f">The calculation function</param>
        /// <returns>f(x,y)</returns>
        private double BiFunction(double x, double y, string f)
        {
            switch (f.ToUpper())
            {
                case "/": return x / y;
                case "*": return x * y;
                case "-": return x - y;
                case "+": return x + y;

                default:
                    throw new ArgumentException("Operator error.");
            }
        }

        /// <summary>
        /// Calculate with the formula
        /// </summary>
        /// <param name="Formula">Input of math formula</param>
        /// <returns>The result</returns>
        public double Calculate(string Formula)
        {
            try
            {
                Formula = Formula.ToUpper().Replace(" ", "");
                string[] arr = Formula.Split(OperatorsArray, StringSplitOptions.RemoveEmptyEntries);

                while (Formula.LastIndexOf("(") > -1)
                {
                    int lastOpenPhrantesisIndex = Formula.LastIndexOf("(");
                    int firstClosePhrantesisIndexAfterLastOpened = Formula.IndexOf(")", lastOpenPhrantesisIndex);
                    double result = ProcessOperation(Formula.Substring(lastOpenPhrantesisIndex + 1, firstClosePhrantesisIndexAfterLastOpened - lastOpenPhrantesisIndex - 1));
                    bool AppendAsterix = false;
                    if (lastOpenPhrantesisIndex > 0)
                    {
                        if (Formula.Substring(lastOpenPhrantesisIndex - 1, 1) != "(" && !OperatorsList.Contains(Formula.Substring(lastOpenPhrantesisIndex - 1, 1)))
                        {
                            AppendAsterix = true;
                        }
                    }

                    Formula = Formula.Substring(0, lastOpenPhrantesisIndex) + (AppendAsterix ? "*" : "") + result.ToString() + Formula.Substring(firstClosePhrantesisIndexAfterLastOpened + 1);

                }
                return ProcessOperation(Formula);
            }
            catch (Exception ex)
            {
                throw new Exception("Syntax error.", ex);
            }
        }


        /// <summary>
        /// Process the operations
        /// </summary>
        /// <param name="operation">One part from given formula</param>
        /// <returns>The result of this part.</returns>
        private double ProcessOperation(string operation)
        {

            List<string> arr = new List<string>();
            string s = "";
            for (int i = 0; i < operation.Length; i++)
            {
                string currentCharacter = operation.Substring(i, 1);
                if (OperatorsList.IndexOf(currentCharacter) > -1)
                {
                    if (s != "")
                    {
                        arr.Add(s);
                    }
                    else
                    {
                        arr.Add("0");
                    }
                    arr.Add(currentCharacter);
                    s = "";
                }
                else
                {
                    s += currentCharacter;
                }
            }
            arr.Add(s);
            s = "";
            foreach (string op in OperatorsList)
            {
                while (arr.IndexOf(op) > -1)
                {
                    int operatorIndex = arr.IndexOf(op);
                    double digitBeforeOperator = Convert.ToDouble(arr[operatorIndex - 1]);
                    double digitAfterOperator = 0;
                    if (arr[operatorIndex + 1].ToString() == "-")
                    {
                        arr.RemoveAt(operatorIndex + 1);
                        digitAfterOperator = Convert.ToDouble(arr[operatorIndex + 1]) * -1;
                    }
                    else
                    {
                        digitAfterOperator = Convert.ToDouble(arr[operatorIndex + 1]);
                    }
                    arr[operatorIndex] = BiFunction(digitBeforeOperator, digitAfterOperator, op).ToString();
                    arr.RemoveAt(operatorIndex - 1);
                    arr.RemoveAt(operatorIndex);
                }
            }
            return Convert.ToDouble(arr[0]);
        }

        #endregion

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            OperatorsList.AddRange(OperatorsArray);
            // Avoid screen locks while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
        }

        private void ApplicationHint_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a test program made by DC.S.\nAlso thanks Tamer Oz.", "Hint!", MessageBoxButton.OK);
        }



        private void textBoxFormula_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                textBoxRes.Text = Calculate(textBoxFormula.Text).ToString();
                textBoxRes.Focus();   
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error.", MessageBoxButton.OK);
                textBoxFormula.Focus();
            }
        }

        private void textBoxFormula_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                textBoxFormula_LostFocus(null, null);
        }





    }
}