using MySql.Data.MySqlClient;
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
    /// <summary>
    /// Interaction logic for TodoAppPage.xaml
    /// </summary>
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
                string query = "SElECT CONCAT(user_task,' -- ',task_datetime) AS user_task, user_tasks.id AS id FROM users " +
                    "INNER JOIN user_tasks " +
                    "ON user_tasks.user_id = users.id WHERE username = '"+ userName +"'; ";                
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, signInForm_MySqlConection);
                using (mySqlDataAdapter)
                {
                    DataTable dataTable = new DataTable();
                    mySqlDataAdapter.Fill(dataTable);
                    mySqlDataAdapter.Dispose();

                    
                    UserTask_listBox.DisplayMemberPath = "user_task" ;
                    UserTask_listBox.SelectedValuePath = "id";
                    UserTask_listBox.ItemsSource = dataTable.DefaultView;
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
        public int RequestUsernameId()
        {
            var id = 0;            
            try
            {                
                string query = "SELECT id FROM users WHERE username = '"+ userName +"';";
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, signInForm_MySqlConection))
                {
                    signInForm_MySqlConection.Open();
                    using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader[0]);                            
                        }
                    }
                    signInForm_MySqlConection.Close();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message.ToString());
            }
            return id;
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
    }
}
