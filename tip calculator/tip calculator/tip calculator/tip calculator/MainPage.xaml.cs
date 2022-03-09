using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace tip_calculator
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        float billAmount = 52.80F, tipAmount, totalAmount;
        public MainPage()
        {
            InitializeComponent();
            PercentSlider.ValueChanged += PercentSlider_ValueChanged;
        }

        private void PercentSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            percentLabel.Text = String.Format("{0}", e.NewValue);
                 
        }

        

        private void updateUI ()
        {
            //-- Calculate the bill

            tipAmount = billAmount * (float)PercentSlider.Value  / 100;
            totalAmount = billAmount + tipAmount;

            //--Display the tip

            billAmountLabel.Text = String.Format("${0:F2}", billAmount);
            tipAmountLabel.Text = String.Format("${0:F2}", tipAmount);
            totalAmountLabel.Text = String.Format("${0:F2}", totalAmount);
            percentLabel.Text = String.Format("{0}%", PercentSlider.Value);



        }

        private void CalculaterButtonTapped(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Console.WriteLine(button.Text);
        }

        bool hasStarted = false, hasTypeDecimal = false;
        int numberOfDecimalDigits = 0;
         
        private void reset ()
        {
            hasStarted = false;
            hasTypeDecimal = false;
            numberOfDecimalDigits = 0;
            billAmount = 0.0F;
            tipAmount = 0.0F;
            totalAmount = 0.0F;


            updateUI();

        }
    
        private void CalculateButtonTapped(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            //-- Reset everything if "C" is tapped 

            if (button.Text == "C")
            {
                reset();
                return;

            }

            //-- Check if "0" is a valid keypress at this point

            if (button.Text == "0" && hasStarted == false)
            {
                return;
            }

            
            //-- if we've made it this far, we have officialy started getting a valid bill 

            if (!hasStarted)
            {
                hasStarted = true;
            }

            //--Handle Decuimal

            if(button.Text == ".")
            {
                _ = hasTypeDecimal == true;
            }

            //-- Handle Numbers

            else
            {
                 if (hasTypeDecimal)
                {
                    if(numberOfDecimalDigits < 2)
                    {
                        numberOfDecimalDigits++;
                    }
                }
            }

            //-- Update the bill amount.

            if(hasTypeDecimal)
            {
                //-- If the user is typing a decimal, calculate the amount to add

                float multiplier = numberOfDecimalDigits == 2 ? 0.01F : 0.1F;
                billAmount += multiplier * Int32.Parse(button.Text);
            }

            else
            {
                //-- If the user is typing a whole number, then shift the current
                //-- bill amount to make room for the new number

                billAmount *= 10;

                //-- Convert the button label to an integer and add it 

                billAmount += Int32.Parse(button.Text);

            }

            updateUI();
        }
    
    
    
    }   
}
