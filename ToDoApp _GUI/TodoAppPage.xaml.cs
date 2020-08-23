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


namespace ToDoApp__GUI
{
    /// <summary>
    /// Interaction logic for TodoAppPage.xaml
    /// </summary>
    public partial class TodoAppPage : Page
    {
        private MySqlConnection signInForm_MySqlConection;
        public string userName { get; set; }

        public TodoAppPage(MySqlConnection mySqlConnection, string username)
        {
            InitializeComponent();
            signInForm_MySqlConection = mySqlConnection;
            userName = username;
            UserName_Tag.Text = "Username: " + userName;

            showingtask_inlistbox(userName);
        }


        private void showingtask_inlistbox(string username)
        {
            try
            {
                string query = "SElECT CONCAT(user_task,' - ', task_datetime) AS TaskWithDate FROM users " +
                    "INNER JOIN user_tasks " +
                    "ON user_tasks.user_id = users.id WHERE username = '"+ username +"'; ";                
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, signInForm_MySqlConection);
                using (mySqlDataAdapter)
                {                    
                    DataTable dataTable = new DataTable();
                    mySqlDataAdapter.Fill(dataTable);


                    UserTask_listBox.DisplayMemberPath = "TaskWithDate" ;
                    UserTask_listBox.SelectedValuePath = "user_id";
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
            try
            {
                string query = "INSERT INTO user_tasks (user_task, task_datetime, user_id)" +
                    "VALUES ('" + username + "','"+ +"',);";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, signInForm_MySqlConection);
                using (mySqlDataAdapter)
                {
                    DataTable dataTable = new DataTable();
                    mySqlDataAdapter.Fill(dataTable);


                    UserTask_listBox.DisplayMemberPath = "TaskWithDate";
                    UserTask_listBox.SelectedValuePath = "user_id";
                    UserTask_listBox.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message.ToString());
            }
        }
    }
}
