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
    public partial class SignUpPage : Page
    {
         private MySqlConnection signInForm_MySqlConection;
        public SignUpPage(MySqlConnection mySqlConnection)
        {
            InitializeComponent();
            signInForm_MySqlConection = mySqlConnection;        //requesting mysql connection from main class/window
        }
        private void SignUp_Page_SignUp_Button (object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(UserNameBox.Text)         //Checking whether text boxes is empty 
                && !string.IsNullOrEmpty(EmailBox.Text)
                && !string.IsNullOrEmpty(SignUp_PasswordBox.Password) 
                && !string.IsNullOrEmpty(SignUp_ConfirmPasswordBox.Password))
            {
                if (SignUp_PasswordBox.Password.ToLower() == SignUp_ConfirmPasswordBox.Password.ToLower())      //Checking both password box is same or not
                {
                    try
                    {
                        String mySqlQuery = "INSERT INTO users (username, email, user_password)" +          //Mysql Insert Query with variable
                                    " VALUES ('" + UserNameBox.Text + "', '" + EmailBox.Text + "', SHA1" +
                                    "('" + UserNameBox.Text + SignUp_PasswordBox.Password + "'));";             
                        MySqlCommand mySqlCommand = new MySqlCommand(mySqlQuery, signInForm_MySqlConection);        //Saving query and connection on mysql command
                        signInForm_MySqlConection.Open();       //Opening Mysql conection                                         
                        mySqlCommand.ExecuteScalar();           //this command only execute and return first column of first row
                        
                        SignUp_InfoBar.Foreground = Brushes.Blue;       //decorations
                        SignUp_InfoBar.Text = "Account Successfully created";
                        
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message.ToString());
                    }
                    finally
                    {
                        signInForm_MySqlConection.Close();          //closing connection
                        UserNameBox.Text = "";          //emptying all feilds
                        EmailBox.Text = "";
                        SignUp_PasswordBox.Password = "";
                        SignUp_ConfirmPasswordBox.Password = "";
                    }

                }
                else
                {
                    SignUp_InfoBar.Foreground = Brushes.Red;
                    SignUp_InfoBar.Text = "Password doesn't match!";
                }
            }
            else
            {
                MessageBox.Show("Please fill full form!");
            }
        }
        private void SignUp_PageClose_Button(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();         //closing whole application
        }
    }
}
