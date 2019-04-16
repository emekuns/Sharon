using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SQLiteConnection m_dbConnection;
        public MainWindow()
        {
            InitializeComponent();
            SQLiteConnection.CreateFile("MyDatabase.sqlite");
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            string sql = "create table employee (employee_id int primary key, name varchar(20), position varchar(20), department varchar(20), hourly_pay_rate float)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            string sql2 = "select * from employee";
            SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);
            SQLiteDataAdapter sda = new SQLiteDataAdapter(command2);
            DataTable dt = new DataTable("employee");
            sda.Fill(dt);

            datagrid1.ItemsSource = dt.DefaultView;

            m_dbConnection.Close();
        }

        private void AddNewEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            m_dbConnection.Open();
            string sql = string.Format("insert into employee(employee_id, name, position, department, hourly_pay_rate) values ({0}, '{1}', '{2}', '{3}', {4})", idTxt.Text, nameTxt.Text, positionTxt.Text, departmentTxt.Text, payTxt.Text);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            idTxt.Text = "";
            nameTxt.Text = "";
            positionTxt.Text = "";
            departmentTxt.Text = "";
            payTxt.Text = "";
        }
    }
}
