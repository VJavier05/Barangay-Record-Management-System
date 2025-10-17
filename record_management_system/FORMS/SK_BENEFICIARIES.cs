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
    public partial class SK_BENEFICIARIES : Form
    {

        public string primaryid;
        MySqlCommand cm;
        MySqlDataReader dr;

        public SK_BENEFICIARIES()
        {
            InitializeComponent();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            RD_FORM.skadd form1 = new RD_FORM.skadd(this);
            
            form1.ShowDialog();
        }

        private void SK_BENEFICIARIES_Load(object sender, EventArgs e)
        {
            loadrecord();
        }
        public void loadrecord()
        {
            MySqlConnection dbconnect = new MySqlConnection(Login.connectstring);
            try
            {
                foreach (DataGridViewRow row in datagridviewsk.Rows)
                {
                    DateTime birthday = Convert.ToDateTime(row.Cells["birthdays"].Value);
                    DateTime today = DateTime.Today;

                    int age = today.Year - birthday.Year;

                    // Check if the birthday for this row has already occurred this year
                    if (today < birthday.AddYears(age))
                    {
                        age--; // Adjust the age if the birthday hasn't occurred yet
                    }

                    // Update the age in the MySQL database
                    int userId = Convert.ToInt32(row.Cells["ID"].Value);
                    string updateQuery = "UPDATE sk_member SET age = @Age WHERE ID = @UserID";

                    using (MySqlConnection connection = new MySqlConnection(Login.connectstring))
                    {
                        using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Age", age);
                            command.Parameters.AddWithValue("@UserID", userId);

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    // Set the calculated age value in a specific column (e.g., "Age")
                    row.Cells["Age"].Value = age;
                }

                datagridviewsk.Rows.Clear();
                dbconnect.Open();
                cm = new MySqlCommand("SELECT * FROM sk_member", dbconnect);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    datagridviewsk.Rows.Add(dr["ID"].ToString(), dr["fname"].ToString(), dr["mname"].ToString(), dr["lname"].ToString(), dr["age"].ToString(), DateTime.Parse(dr["birthdays"].ToString()).ToShortDateString(), dr["contactnumber"].ToString(), dr["gender"].ToString(), dr["address"].ToString(), dr["status"].ToString());
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

        private void datagridviewsk_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                string colname = datagridviewsk.Columns[e.ColumnIndex].Name;
                if (colname == "btnuseredit")
                {
                    int selectedID = Convert.ToInt32(datagridviewsk.Rows[e.RowIndex].Cells[0].Value);
                    RD_FORM.skadd editsk = new RD_FORM.skadd(this);
                    editsk.txtbxFname.Text = this.datagridviewsk.CurrentRow.Cells[1].Value.ToString();
                    editsk.txtbxmname.Text = this.datagridviewsk.CurrentRow.Cells[2].Value.ToString();
                    editsk.txtbxlname.Text = this.datagridviewsk.CurrentRow.Cells[3].Value.ToString();


                    //HERE
                    editsk.dateTimePicker1.Enabled = false;


                    editsk.dateTimePicker1.Value = DateTime.Parse(this.datagridviewsk.CurrentRow.Cells[5].Value.ToString());
                    editsk.txtbxcontact.Text = this.datagridviewsk.CurrentRow.Cells[6].Value.ToString();

                    editsk.txtbxaddress.Text = this.datagridviewsk.CurrentRow.Cells[8].Value.ToString();
                    editsk.comboBox1.Text = this.datagridviewsk.CurrentRow.Cells[9].Value.ToString();


                    if (this.datagridviewsk.CurrentRow.Cells[7].Value.ToString() == "Male")
                    {
                        editsk.radioButton1.Checked = true;

                    }
                    else if (this.datagridviewsk.CurrentRow.Cells[7].Value.ToString() == "Female")
                    {
                        editsk.radioButton2.Checked = true;
                    }
                    editsk.btnedit.Visible = true;
                    editsk.btnlogin.Visible = false;
                    editsk.lbltitle.Text = "EDIT SK";
                    primaryid = this.datagridviewsk.CurrentRow.Cells[0].Value.ToString();
                    editsk.ShowDialog();
                }
                if (colname == "btnuserdel")
                {

                    if (MessageBox.Show("Are you sure You want to Delete this Sk?", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        MySqlConnection dbconnect = new MySqlConnection(Login.connectstring);
                        try
                        {
                            dbconnect.Open();
                            string deletecmd = "DELETE FROM sk_member WHERE ID = " + this.datagridviewsk.CurrentRow.Cells[0].Value.ToString();
                            cm = new MySqlCommand(deletecmd, dbconnect);
                            dr = cm.ExecuteReader();
                            loadrecord();
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void datagridviewsk_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            foreach (DataGridViewRow r in datagridviewsk.Rows)
            {
                string active = Convert.ToString(r.Cells[9].Value);


                if (active == "Inactive")
                {
                    r.DefaultCellStyle.BackColor = Color.Red;
                    r.DefaultCellStyle.ForeColor = Color.White;


                }
                else if (active == "Active")
                {

                    r.DefaultCellStyle.BackColor = Color.White;
                    r.DefaultCellStyle.ForeColor = Color.Black;

                }

            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.ToLower();
            foreach (DataGridViewRow row in datagridviewsk.Rows)
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
