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
using System.Windows.Navigation;

namespace ToDoApp__GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        protected MySqlConnection mySqlConnection;
        

        public MainWindow()
        {
            InitializeComponent();


            string ConnectionString ="server=localhost;user id=root;" +
                "password=manjan;persistsecurityinfo=True;database=todoapp_db";                             //Connection String For DataBase 
            mySqlConnection = new MySqlConnection(ConnectionString);

        }

        private void LogIn_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(UserNameBox.Text) && 
                !string.IsNullOrEmpty(PasswordBox.Password))                                                //Check if BothTextBox is not empty
            {
                try
                {
                    string mySqlQuery = "SELECT username, user_password FROM users " +
                                        "WHERE username='" + UserNameBox.Text + "" +
                                        "' AND user_password=SHA1('" + UserNameBox.Text 
                                        + PasswordBox.Password + "');";                                     //Storing mysql query in c# string with string concat
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlQuery, mySqlConnection);  //Basically adapter here act as a medium which first make connection and then run mysql query 
                    using (mySqlDataAdapter)                                                                //here we use defined dataadapter
                    {

                        DataTable dataTable = new DataTable();                                              //here we create a table for data to be fill
                        mySqlDataAdapter.Fill(dataTable);                                                   //store result of mysql in adapter is now filling in data table
                        if (dataTable.Rows.Count > 0 && dataTable.Rows.Count <= 1)                          //if result is perfect then there should be one row in data table
                        {
                            Login_InfoBar.Foreground = Brushes.Blue;                                        //changing label text colour
                            Login_InfoBar.Text = "Success";                                                 //showing result in textblock

                            MainPage.Content = new TodoAppPage(mySqlConnection, UserNameBox.Text);                            //initializing todoapp page class
                            MainPage.Height = 550;                                                          //changing main page height and width
                            MainPage.Width = 800;
                        }
                        else
                        {
                            Login_InfoBar.Foreground = Brushes.Red;
                            Login_InfoBar.Text = "Incorrect username and password!";
                        }
                    }
                }                
                catch (Exception error)
                {
                    MessageBox.Show(error.Message.ToString());
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
            MainPage.Content = new SignUpPage(mySqlConnection);             //Going to SignUp Page
            MainPage.Height = 650;                                          //Setting its height ans width
            MainPage.Width = 500;
        }
        private void ApplicationClose_Button(object sender, RoutedEventArgs e)
        {
            this.Close();                                                   //Closing Application or todo app
        }
    }
}
