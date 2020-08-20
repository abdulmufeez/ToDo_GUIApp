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
using System.Data;

namespace ToDoApp__GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        MySqlConnection mySqlConnection;
        MySqlCommand sqlCommand;
        public MainWindow()
        {
            InitializeComponent();


            string ConnectionString = //ConfigurationManager.ConnectionStrings["@ToDoApp_GUI.Properties.Settings.todoapp_dbConnectionString;"].ConnectionString.ToString();
                "server=localhost;user id=root;password=manjan;persistsecurityinfo=True;database=todoapp_db";             //Connection String For DataBase 
            mySqlConnection = new MySqlConnection(ConnectionString);

        }

        private void LogIn_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(UserNameBox.Text) && !string.IsNullOrEmpty(PasswordBox.Password))     //Check if BothTextBox is not empty
            { 
                string mySqlQuery = "SELECT username, user_password FROM users " +
                    "WHERE username='" + UserNameBox.Text + "' AND user_password=SHA1('" + PasswordBox.Password +"');";      //Storing mysql query in variable and @userame,@assword are just variable
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlQuery, mySqlConnection);  
                using (mySqlDataAdapter)
                {
                    DataTable dataTable = new DataTable();
                    mySqlDataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Rows.Count <= 1)
                    {
                        Login_InfoBar.Foreground = Brushes.Blue;
                        Login_InfoBar.Text = "Success";
                        
                        MainPage.Content = new TodoAppPage();
                        MainPage.Height = 550;
                        MainPage.Width = 800;
                    }
                    else
                    {
                        Login_InfoBar.Foreground = Brushes.Red;
                        Login_InfoBar.Text = "Incorrect username and password!";
                    }
                }
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
