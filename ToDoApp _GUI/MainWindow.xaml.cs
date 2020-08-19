using System;
using System.Collections.Generic;
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
using System.Configuration;
using MySql.Data.MySqlClient;
using Ubiety.Dns.Core;

namespace ToDoApp__GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection mySqlConnection;
        public MainWindow()
        {
            InitializeComponent();


            //string ConnectionString = ConfigurationManager.ConnectionStrings["ToDoApp_GUI.Properties.Settings." +
            //    "todoapp_dbConnectionString"].ConnectionString;             //Connection String For DataBase 
            //mySqlConnection = new MySqlConnection(ConnectionString);
        }

        private void LogIn_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(UserNameBox.Text) && !string.IsNullOrEmpty(PassWordBox.Password))
            {
                Login_InfoBar.Text = "Successful";
            }
            else
            { 
                Login_InfoBar.Foreground = Brushes.Red;
                Login_InfoBar.Text = "Please enter your username and password";
            }
        }
        private void SignUp_Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Content = new SignUpPage();
            MainPage.Height = 650;
            MainPage.Width = 500;
        }
    }
}
