using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace record_management_system.RD_FORM
{
    public partial class transactionadd : Form
    {
        private FORMS.TRANSACTION transform;
        MySqlCommand cm;
        MySqlDataReader dr;
        public transactionadd(FORMS.TRANSACTION form1)
        {
            InitializeComponent();
            this.transform = form1;
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtbxname.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Please fill up the form", "System Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                errormsg.Text = "Please Fill up form!";
            }
            else
            {
                DateTime currentDateTime = DateTime.Now;
                string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                string addtransaction = "INSERT INTO transaction(name,dateadded,purok,certificate,assisted_by) VALUES('" + txtbxname.Text + "','" + formattedDateTime + "','" + comboBox2.Text + "','" + comboBox1.Text +"','"+Login.username+"')";
                executeQuery(addtransaction, "Transaction Added", "Sk Beneficaries not Added");
                this.Close();
            }
            loadrecord();
        }
        MySqlConnection databaseconnection = new MySqlConnection(Login.connectstring);
        public void executeQuery(string query, string Mess1, string mess2)
        {
            try
            {

                MySqlCommand Commanddatabase = new MySqlCommand(query, databaseconnection);

                databaseconnection.Open();

                if (Commanddatabase.ExecuteNonQuery() == 1)
                {
                    //MessageBox.Show("Query Executed");
                    MessageBox.Show(Mess1, "Transaction", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    //MessageBox.Show("Query Not Executed");
                    MessageBox.Show("Transaction Added", "Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                databaseconnection.Close();
            }
        }
        public void loadrecord()
        {
            MySqlConnection dbconnect = new MySqlConnection(Login.connectstring);
            try
            {

                transform.dataGridView1.Rows.Clear();
                dbconnect.Open();
                cm = new MySqlCommand("SELECT * FROM transaction", dbconnect);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    transform.dataGridView1.Rows.Add(dr["ID"].ToString(), dr["name"].ToString(), dr["dateadded"].ToString(), dr["purok"].ToString(), dr["certificate"].ToString(), dr["assisted_by"].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dbconnect.Close();
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
