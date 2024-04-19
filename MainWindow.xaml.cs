using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CmpSc472_WindowsApp_FedTaxReturn
	{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
		{
		public MainWindow()
			{
			InitializeComponent();
			}

		private void RadioButton_Checked(object sender, RoutedEventArgs e)
			{

			}

        private void txtLastName1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void clearOnFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Background = null;
            if (textBox != null)
            {
                if (textBox.Text == "i.e. John")
                {
                    textBox.Text = ""; // Remove the prefilled text
                }
            }
        }

        private void clearLastNameOnFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Background = null;
            if (textBox != null)
            {
                if (textBox.Text == "i.e. Doe")
                {
                    textBox.Text = ""; // Remove the prefilled text
                }
            }
        }

        private void clearSSNOnFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Background = null;
            if (textBox != null)
            {
                if (textBox.Text == "XXXXXXXXX")
                {
                    textBox.Text = ""; // Remove the prefilled text
                }
            }
        }

        private void txtSSN_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox.Text == "0.00")
                {
                    textBox.Text = ""; // Remove the prefilled text
                }
            }
        }

        private void clearCashOnFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox.Text == "0.00")
                {
                    textBox.Text = ""; 
                }
            }
        }

        private void txtDepend_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                try
                {
                    int number = int.Parse(textBox.Text);
                    if (number < 0)
                    {
                        textBox.Text = "0";
                    }
                    else if (number > 5)
                    {
                        textBox.Text = "5";
                    }
                }
                catch (FormatException)
                {

                }

            }
            
        }

        private void clearIntOnFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox.Text == "0")
                {
                    textBox.Text = ""; // Remove the prefilled text
                }
            }
        }
        private bool containsNumber(string text)
        {
            bool hasDigit = false;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsDigit(text[i]))
                {
                    return true;
                }
            }
            return false;
        }
        private bool containsLetter(string text)
        {
            bool hasChar = false;
            for(int i = 0;  i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    return true;
                }
            }
            return false;
        }

        private void OnTextboxExit(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string name = textBox.Name;
                string text = textBox.Text;
                switch (name)
                {
                    case "txtMorgInt":
                    case "txtCharDon":
                    case "txtTaxesPayed":
                    case "txtIncome":
                        bool invalid = false;
                        bool dotDetected = false;
                        int dotIndex = -1;
                        Console.WriteLine("I am here");
                        //textBox.Text = "123.00";
                        for (int i = 0; i < text.Length; i++)
                        {
                            //textBox.Text = ""+i;
                            
                            if (text[i] == '.')
                            {
                                if (!dotDetected)
                                {  
                                    dotDetected = true;
                                    dotIndex = i;
                                }
                                else
                                {
                                    invalid = true;
                                }
                            }else if (!char.IsDigit(text[i]))
                            {
                                invalid = true;
                            }
                        }
                        if (invalid)
                        {
                            textBox.Text = "";
                        }
                        else if(dotIndex == 0)
                        {
                            textBox.Text = "0"+text;
                        }
                        else
                        {
                            if (dotIndex == -1)
                            {
                                if(text.Length == 0)
                                {
                                    textBox.Text = "0.00";
                                }
                                else
                                {
                                    textBox.Text = text + ".00";
                                }
                            }
                            else if (dotIndex < text.Length-3)
                            {
                                textBox.Text = text.Substring(0, dotIndex+3);
                            }
                            else if (dotIndex > text.Length - 3)
                            {
                                textBox.Text = text + "0";
                            }
                        }
                        break;
                    case "txtSSN":
                        if (containsLetter(text))
                        {
                            textBox.Text = "";
                        }
                        if(text.Length != 9)
                        {
                            SolidColorBrush fillColor = new SolidColorBrush(Colors.Red);
                            fillColor.Opacity = .5;
                            textBox.Background = fillColor;
                        }
                        else
                        {
                            textBox.Background = null;
                        }
                        break;
                    case "txtLastName":
                    case "txtFirstName":
                        if (containsNumber(text))
                        {
                            textBox.Text = "";
                        }
                        break;
                }
            }
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            double income = 0;
            double adjustedIncome = 0;
            double taxPct = 0;
            int dependents = 0;
            double morgageIntrest = 0;
            double charDonate = 0;
            double taxesPaid = 0;
            try
            {
                taxesPaid = double.Parse(txtTaxesPayed.Text);
                //Deductions
                dependents = int.Parse(txtDepend.Text);
                morgageIntrest = double.Parse(txtMorgInt.Text);
                charDonate = double.Parse(txtCharDon.Text);
                //Filing Status
                
                
                
                if (radSingle.IsChecked == true)
                {
                    
                    taxPct = 0.25;
                }else if (radMarriedJoint.IsChecked == true)
                {
                    taxPct = 0.18;
                    
                }
                else if(radMarriedSep.IsChecked == true)
                {
                    taxPct = 0.22;
                    
                }
                else if (radHeadOfHousehold.IsChecked == true)
                {
                    taxPct = 0.20;
                    
                }
                else //no buttons pressed
                {
                    valid = false;
                    
                }
                //verify name + ssn
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                string ssn = txtSSN.Text;
                SolidColorBrush fillColor = new SolidColorBrush(Colors.Red);
                fillColor.Opacity = .5;
                if (firstName == "i.e. John" || firstName.Replace(" ", "") == "")
                {
                    valid = false;
                    txtFirstName.Background = fillColor;
                }
                if (lastName == "i.e. Doe" || lastName.Replace(" ", "") == "")
                {
                    valid = false;
                    txtLastName.Background = fillColor;
                }
                if (ssn == "XXXXXXXXX" || ssn == "" || ssn.Length != 9)
                {
                    valid = false;
                    txtSSN.Background = fillColor;
                }
                income = double.Parse(txtIncome.Text);
                

            }
            catch (FormatException){
                valid = false;
            }
            
            if (valid)
            {
                double taxPercent = 0;
                
                adjustedIncome = income - (5000 * dependents) - charDonate - morgageIntrest;
                adjustedIncome = adjustedIncome - (adjustedIncome * taxPct);
                if (radMarriedJoint.IsChecked == true)
                {
                    if(adjustedIncome <= 20550)
                    {
                        taxPercent = 0.10;
                    }
                    else if(adjustedIncome <=  83550)
                    {
                        taxPercent = 0.12;
                    }
                    else if (adjustedIncome <= 178150)
                    {
                        taxPercent = 0.22;
                    }
                    else if (adjustedIncome <= 340100)
                    {
                        taxPercent = 0.24;
                    }
                    else if (adjustedIncome <= 431900)
                    {
                        taxPercent = 0.32;
                    }
                    else if (adjustedIncome <= 647850)
                    {
                        taxPercent = 0.35;
                    }
                    else if (adjustedIncome > 647850)
                    {
                        taxPercent = 0.37;
                    }
                }
                else if(radHeadOfHousehold.IsChecked == true)
                {
                    if (adjustedIncome <= 14650)
                    {
                        taxPercent = 0.10;
                    }
                    else if (adjustedIncome <= 55900)
                    {
                        taxPercent = 0.12;
                    }
                    else if (adjustedIncome <= 89050)
                    {
                        taxPercent = 0.22;
                    }
                    else if (adjustedIncome <= 178151)
                    {
                        taxPercent = 0.24;
                    }
                    else if (adjustedIncome <= 340100)
                    {
                        taxPercent = 0.32;
                    }
                    else if (adjustedIncome <= 431900)
                    {
                        taxPercent = 0.35;
                    }
                    else if (adjustedIncome > 539901)
                    {
                        taxPercent = 0.37;
                    }
                }
                else if(radSingle.IsChecked == true || radMarriedSep.IsChecked == true)
                {
                    //single or filing single
                    if (adjustedIncome <= 10275)
                    {
                        taxPercent = 0.10;
                    } 
                    else if (adjustedIncome <= 41775)
                    {
                        taxPercent = 0.12;
                    }
                    else if (adjustedIncome <= 89076)
                    {
                        taxPercent = 0.22;
                    }
                    else if (adjustedIncome <= 170050)
                    {
                        taxPercent = 0.24;
                    }
                    else if (adjustedIncome <= 215950)
                    {
                        taxPercent = 0.32;
                    }
                    else if ((radSingle.IsChecked == true && adjustedIncome <= 539001) || (radMarriedSep.IsChecked == true && adjustedIncome <= 323925))
                    {
                        taxPercent = 0.35;
                    }
                    else if ((radSingle.IsChecked == true && adjustedIncome > 539001) || (radMarriedSep.IsChecked == true && adjustedIncome > 323925))
                    {
                        taxPercent = 0.37;
                    }
                }

                double taxesOwed = adjustedIncome * taxPercent;
                
                
                if(taxesOwed-taxesPaid < 0)
                {
                    string refundTxt = (taxesPaid - taxesOwed).ToString("F2");
                    txtExpectedRefund.Text = "" + refundTxt;
                    txtTaxesOwed.Text = "0.00";
                }
                else
                {
                    txtTaxesOwed.Text = taxesOwed.ToString("F2");
                    txtExpectedRefund.Text = "0.00";
                }

                
                

            }

            
        }

        private void txtTaxesOwed_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void clearAllFields(object sender, RoutedEventArgs e)
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtSSN.Text = string.Empty;
            txtIncome.Text = "0.00";
            txtTaxesPayed.Text = "0.00";
            txtCharDon.Text = "0.00";
            txtMorgInt.Text = "0.00";
            txtDepend.Text = "0";

            radHeadOfHousehold.IsChecked = false;
            radSingle.IsChecked = false;
            radMarriedJoint.IsChecked = false;
            radMarriedSep.IsChecked = false;

            txtTaxesOwed.Text = "--------";
            txtExpectedRefund.Text = "--------";

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
	}
