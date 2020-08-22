using MySql.Data.MySqlClient;
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
using Ubiety.Dns.Core;



namespace ToDoApp__GUI
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
         private MySqlConnection signInForm_MySqlConection;
        public SignUpPage(MySqlConnection mySqlConnection)
        {
            InitializeComponent();
            signInForm_MySqlConection = mySqlConnection;                                                    //requesting mysql connection from main class/window
        }
        private void SignUp_Page_SignUp_Button (object sender, RoutedEventArgs e)
        {
            if (SignUp_PasswordBox.Password.ToLower() == SignUp_ConfirmPassWordBox.Password.ToLower())
            {
                try
                {
                    String mySqlQuery = "INSERT INTO users (username, email, user_password)" +
                                    " VALUES ('" + UserNameBox.Text + "', '" + EmailBox.Text + "', SHA1" +
                                    "('" + UserNameBox.Text + SignUp_PasswordBox.Password + "'));";         //Mysql Insert Query with variable
                    signInForm_MySqlConection.Open();                                                       //Opening mysql connection
                    MySqlCommand mySqlCommand = new MySqlCommand(mySqlQuery, signInForm_MySqlConection);    //Saving query and connection on mysql command
                    mySqlCommand.Prepare();                                                                 //Preparing to run query
                    mySqlCommand.ExecuteNonQuery();                                                         //executereader() function only execute query and return nothing                   
                    signInForm_MySqlConection.Close();
                    SignUp_InfoBar.Foreground = Brushes.Blue;
                    SignUp_InfoBar.Text = "Account Successfully created";


                    
                    
                    
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message.ToString());
                }
                
            }
            else
            {
                SignUp_InfoBar.Foreground = Brushes.Red;
                SignUp_InfoBar.Text = "Password doesn't match!";
            }
        }
        private void SignUp_PageClose_Button(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }
    }
}
