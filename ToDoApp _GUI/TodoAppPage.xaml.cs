using MySql.Data.MySqlClient;
using Renci.SshNet.Messages.Connection;
using System;
using System.Collections.Generic;
using System.Data;
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
using Ubiety.Dns.Core.Records.NotUsed;

namespace ToDoApp__GUI
{
    public partial class TodoAppPage : Page
    {
        private MySqlConnection signInForm_MySqlConection;
        private string userName { get; set; }
        private int User_id { get; set; }

        public TodoAppPage(MySqlConnection mySqlConnection, string username)
        {
            InitializeComponent();
            signInForm_MySqlConection = mySqlConnection;
            userName = username;
            User_id = RequestUsernameId();
            UserName_Tag.Content = "Username: " + userName;

            showingtask_inlistbox();
        }


        private void showingtask_inlistbox()
        {
            try
            {
                string query = "SElECT CONCAT(user_task,' -- ',task_datetime) AS user_task, user_tasks.id AS id FROM users " +          //Mysql Query/Command
                    "INNER JOIN user_tasks " +
                    "ON user_tasks.user_id = users.id WHERE username = '"+ userName +"'; ";                
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, signInForm_MySqlConection);         //this is like a adapter which take query and connection and run it
                using (mySqlDataAdapter)        //running in like a loop
                {
                    DataTable dataTable = new DataTable();          //Creating table same as in Mysql
                    mySqlDataAdapter.Fill(dataTable);               //filling table with mysql data
                    mySqlDataAdapter.Dispose();                     //disposig adapter means closing connection

                    
                    UserTask_listBox.DisplayMemberPath = "user_task" ;      //showing mysql data with id 
                    UserTask_listBox.SelectedValuePath = "id";
                    UserTask_listBox.ItemsSource = dataTable.DefaultView;   //connecting list box source to data table
                }
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message.ToString());
            }                        
        }
        private void AddTask_Button (object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty (Task_TextBox.Text))
            {
                try
                {
                    string query = "INSERT INTO user_tasks (user_task, task_datetime, user_id)" +
                        "VALUES ('" + Task_TextBox.Text + "','" + Task_DateTime_TextBlock.Text + "', "+ User_id + ");";                    
                    MySqlCommand mySqlCommand = new MySqlCommand(query, signInForm_MySqlConection);
                    signInForm_MySqlConection.Open();
                    mySqlCommand.ExecuteScalar();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message.ToString());
                }
                finally
                {                    
                    signInForm_MySqlConection.Close();
                    showingtask_inlistbox();
                    Task_TextBox.Text = "";
                    Task_DateTime_TextBlock.Text = "";
                }
            }
            else
            {
                MessageBox.Show("First enter task first!");
            }            
        }
        /*
         * here i create requestuserid function 
         * the main working here is to extract the user id which is present in main table 
         * not in secomdary table which is connected to main table which is users(name)
         * so to achieve this i created this otherwise i may have to create the whole database again
         */
        public int RequestUsernameId()
        {
            var id = 0;            
            try
            {                
                string query = "SELECT id FROM users WHERE username = '"+ userName +"';";       
                MySqlCommand mySqlCommand = new MySqlCommand(query, signInForm_MySqlConection);
                using (mySqlCommand)
                {
                    signInForm_MySqlConection.Open();
                    using (MySqlDataReader reader = mySqlCommand.ExecuteReader())       //here mysqldatareader and executereader both work together such as 
                                                                                        //reader read the data from the mysql output and executer execute the datareader 
                    {
                        while (reader.Read())       //reading in lope that is one by one
                        {
                            id = Convert.ToInt32(reader[0]);        //saving in variable                     
                        }
                    }
                    signInForm_MySqlConection.Close();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message.ToString());
            }
            return id;      //finally return user id 
        }
        private void DeleteTask_Button (object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "DELETE FROM user_tasks WHERE user_tasks.id = " + UserTask_listBox.SelectedValue + " ;";
                MySqlCommand mySqlCommand = new MySqlCommand(query, signInForm_MySqlConection);
                signInForm_MySqlConection.Open();
                mySqlCommand.ExecuteScalar();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message.ToString());
            }
            finally
            {
                signInForm_MySqlConection.Close();
                showingtask_inlistbox();
            }
        }
        private void EditTask_Button (object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Task_TextBox.Text))
            {
                try
                {
                    string query = " UPDATE user_tasks SET user_task='" + Task_TextBox.Text + "', task_datetime='" + Task_DateTime_TextBlock.Text + "'" +
                        "WHERE user_tasks.id=" + UserTask_listBox.SelectedValue + ";";
                    MySqlCommand mySqlCommand = new MySqlCommand(query, signInForm_MySqlConection);
                    signInForm_MySqlConection.Open();
                    mySqlCommand.ExecuteScalar();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message.ToString());
                }
                finally
                {
                    signInForm_MySqlConection.Close();
                    showingtask_inlistbox();
                    Task_TextBox.Text = "";
                    Task_DateTime_TextBlock.Text = "";
                }
            }
            else
            {
                MessageBox.Show("First enter required things!");
            }
            
        }
        private void SignOut_Button (object sender, RoutedEventArgs e)
        {
            //this.Content = new MainWindow();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            //Application.Current.Run();

        }
    }
}
