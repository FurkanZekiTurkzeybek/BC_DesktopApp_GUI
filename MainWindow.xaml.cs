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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _distress = false;
        private bool _aid = false;
        private bool _safe = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void setDistress()
        {
            _distress = true;
            _aid = false;
            _safe = false;
        }

        private void setAid()
        {
            _distress = false;
            _aid = true;
            _safe = false;
        }

        private void setSafe()
        {
            _distress = false;
            _aid = false;
            _safe = true;
        }

        private int getStatus()
        {
            if (_distress == true && _aid == false && _safe == false)
            {
                return 1;
            }
            else if (_distress == false && _aid == true && _safe == false)
            {
                return 2;
            }
            else if (_distress == false && _aid == false && _safe == true)
            {
                return 3;
            }
            else
            {
                return 0;
            }
               
        }

        private void distressedSelected(object sender, RoutedEventArgs e)
        {
            setDistress();
        }

        private void aidSelected(object sender, RoutedEventArgs e)
        {
            setAid();
        }

        private void safeSelected(object sender, RoutedEventArgs e)
        {
            setSafe();
        }

        private void listClicked(object sender, RoutedEventArgs e)
        {
            int statusVariable = getStatus();
            //send this command to the JS code and execute the searching according to that
            //after js search is done and print the resulting hashes and information to the window
            outputList.Items.Add(statusVariable);
        }
    }
}