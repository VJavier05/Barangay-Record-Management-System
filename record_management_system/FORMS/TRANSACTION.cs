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

namespace record_management_system.FORMS
{
    public partial class TRANSACTION : Form
    {
        MySqlCommand cm;
        MySqlDataReader dr;
        public TRANSACTION()
        {
            InitializeComponent();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            RD_FORM.transactionadd form1 = new RD_FORM.transactionadd(this);
            form1.ShowDialog();
        }
        public void loadrecord()
        {
            MySqlConnection dbconnect = new MySqlConnection(Login.connectstring);
            try
            {

                dataGridView1.Rows.Clear();
                dbconnect.Open();
                cm = new MySqlCommand("SELECT * FROM transaction", dbconnect);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr["ID"].ToString(), dr["name"].ToString(), dr["dateadded"].ToString(), dr["purok"].ToString(), dr["certificate"].ToString(), dr["assisted_by"].ToString());
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

        private void TRANSACTION_Load(object sender, EventArgs e)
        {
            loadrecord();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.ToLower();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool isVisible = false;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().ToLower().Contains(searchQuery))
                    {
                        isVisible = true;
                        break;
                    }
                }
                row.Visible = isVisible;
            }
        }
    }
}
