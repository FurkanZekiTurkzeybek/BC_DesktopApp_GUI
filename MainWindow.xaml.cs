using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.JavaScript;
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
using Newtonsoft.Json;


namespace WpfApp1 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class MyPerson {
        public String latestState;
        public String name;
        public String surname;
        public String address;

        public MyPerson(string latestState, string name, string surname, string address) {
            this.latestState = latestState;
            this.name = name;
            this.surname = surname;
            this.address = address;
        }
    }

    public partial class MainWindow : Window {
        private bool _distress = false;
        private bool _aid = false;
        private bool _safe = false;

        public MainWindow() {
            InitializeComponent();
        }

        private void setDistress() {
            _distress = true;
            _aid = false;
            _safe = false;
        }

        private void setAid() {
            _distress = false;
            _aid = true;
            _safe = false;
        }

        private void setSafe() {
            _distress = false;
            _aid = false;
            _safe = true;
        }

        private int getStatus() {
            if (_distress == true && _aid == false && _safe == false) {
                return 1;
            }
            else if (_distress == false && _aid == true && _safe == false) {
                return 2;
            }
            else if (_distress == false && _aid == false && _safe == true) {
                return 3;
            }
            else {
                return 0;
            }
        }

        private void distressedSelected(object sender, RoutedEventArgs e) {
            setDistress();
        }

        private void aidSelected(object sender, RoutedEventArgs e) {
            setAid();
        }

        private void safeSelected(object sender, RoutedEventArgs e) {
            setSafe();
        }

        private HttpClient client = new HttpClient();

        private String url = "http://localhost:3000/myFunction?param1=value1&param2=value2";

        private void listClicked(object sender, RoutedEventArgs e) {
            outputList.Items.Clear();
            int statusVariable = getStatus();


            test(statusVariable);
        }

        private async void test(int pStatVariable) {
            HttpClient client = new HttpClient();
            String url = "http://localhost:3000/read";


            String statusStringToSend = pStatVariable.ToString();
            var data = new Dictionary<String, String> {
                { "statusType", statusStringToSend }
            };
            var queryString = new FormUrlEncodedContent(data);

            url += "?" + queryString.ReadAsStringAsync().Result; //?
            HttpResponseMessage response = await client.PostAsync(url, queryString);


            if (response.IsSuccessStatusCode) {
                String result = await response.Content.ReadAsStringAsync();
                // MessageBox.Show(result);
            }

            else {
                MessageBox.Show("Cannot fetch the data");
            }

            string filePath = @"D:\Programming\SourceCodes\_BC_read\DesktopApp\read\output.json";

            try {
                string json = File.ReadAllText(filePath);


                List<MyPerson> jsonDataList = JsonConvert.DeserializeObject<List<MyPerson>>(json);


                foreach (MyPerson person in jsonDataList) {
                    outputList.Items.Add(person.name + " || " + person.surname + " || " + person.address);
                }
            }
            catch (Exception exception) {
                MessageBox.Show("Error:" + exception.Message);
                throw;
            }
        }
    }
}