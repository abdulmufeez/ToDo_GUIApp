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
        }


        private void showingtask_inlistbox()
        {
            string query = "SELECT user_task, user_tasks.created_at, task_datetime FROM users" +
                "INNER JOIN user_tasks" +
                "ON users.id = user_tasks.user_id ;";
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, signInForm_MySqlConection);
            using (mySqlDataAdapter)
            {
                DataTable dataTable = new DataTable();
                mySqlDataAdapter.Fill(dataTable);

                UserTask_listBox.ItemsSource = dataTable.DefaultView;
            }
        }
    }
}
