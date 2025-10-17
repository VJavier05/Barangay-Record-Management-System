using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace record_management_system.FORMS
{
    public partial class FILE_MANAGER : Form
    {
        MySqlCommand cm;
        MySqlDataReader dr;
        public string primaryid;

        public FILE_MANAGER()
        {
            InitializeComponent();
        }
        
        private void btnadd_Click(object sender, EventArgs e)
        {

            RD_FORM.fileadd form1 = new RD_FORM.fileadd(this);
            form1.ShowDialog();
            
        }
       
        public void loadrecord()
        {
            MySqlConnection dbconnect = new MySqlConnection(Login.connectstring);
            try
            {
                if (Login.rolecheck == "1")
                {
                    
                    dataGridView1.Rows.Clear();
                    dbconnect.Open();
                    cm = new MySqlCommand("SELECT * FROM file", dbconnect);
                    dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr["ID"].ToString(), dr["filename"].ToString(), dr["filetype"].ToString(), DateTime.Parse(dr["dateadded"].ToString()).ToShortDateString(), dr["filedescription"].ToString());
                    }

                }
                else
                {

                    dataGridView1.Rows.Clear();
                    dbconnect.Open();
                    cm = new MySqlCommand("SELECT * FROM file", dbconnect);
                    dr = cm.ExecuteReader();
                    while (dr.Read())   
                    {
                        dataGridView1.Rows.Add(dr["ID"].ToString(), dr["filename"].ToString(), dr["filetype"].ToString(), DateTime.Parse(dr["dateadded"].ToString()).ToShortDateString(), dr["filedescription"].ToString());
                    }
                    this.dataGridView1.Columns["btndel"].Visible = false;

                }
                /*dataGridView1.Rows.Clear();
                dbconnect.Open();
                cm = new MySqlCommand("SELECT * FROM file", dbconnect);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr["ID"].ToString(), dr["filename"].ToString(), dr["filetype"].ToString(),DateTime.Parse(dr["dateadded"].ToString()).ToShortDateString(), dr["filedescription"].ToString());
                }*/

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

        private void FILE_MANAGER_Load(object sender, EventArgs e)
        {
            loadrecord();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colname = dataGridView1.Columns[e.ColumnIndex].Name;
             
                if (colname == "btndl")
                {

                    if (MessageBox.Show("Download this File?", "Download Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        string fileName = dataGridView1.Rows[e.RowIndex].Cells["Column2"].Value.ToString();
                        MySqlConnection connection = new MySqlConnection(Login.connectstring);
                        try
                        {

                            connection.Open();
                            string sql = "select * from file where filename=@FileName";
                            MySqlCommand cmd = new MySqlCommand(sql, connection);
                            cmd.Parameters.AddWithValue("@FileName", fileName);
                            MySqlDataReader dr = cmd.ExecuteReader();
                            byte[] data = null;
                            while (dr.Read())
                            {
                                data = (byte[])dr["data"];
                            }
                            dr.Close();
                            connection.Close();

                            if (data != null)
                            {
                                using (var fs = new FileStream(Path.Combine("C:\\Users\\pc-3\\Downloads", fileName), FileMode.Create, FileAccess.Write))
                                {
                                    fs.Write(data, 0, data.Length);
                                }

                                MessageBox.Show("File downloaded successfully.");
                            }
                            else
                            {
                                MessageBox.Show("File not found.");
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                    

                }
                else if (colname == "btnuseredit")
                {
                   

                    int selectedID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    RD_FORM.fileadd editfile = new RD_FORM.fileadd(this);

                    editfile.txtbxfilename.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    editfile.dateTimePicker1.Value = DateTime.Parse(this.dataGridView1.CurrentRow.Cells[3].Value.ToString());
                    editfile.txtbxdes.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    editfile.btnlogin.Visible = false;
                    editfile.dateTimePicker1.Enabled = false;
                    editfile.btnbrowse.Enabled = false;
                    //editfile.btnbrowse.ForeColor = System.Drawing.Color.Red;
                    editfile.lbltitle.Text = "EDIT FILE";
                    primaryid = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    editfile.btnedit.Visible = true;
                    editfile.ShowDialog();

                }else if (colname == "btndel")
                {
                    if (MessageBox.Show("Are you sure You want to Delete this File?", "Delete File", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        MySqlConnection dbconnect = new MySqlConnection(Login.connectstring);
                        try
                        {
                            dbconnect.Open();
                            string deletecmd = "DELETE FROM file WHERE ID = " + this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                            cm = new MySqlCommand(deletecmd, dbconnect);
                           
                            dr = cm.ExecuteReader();
                            MessageBox.Show("File Deleted", "File Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            finally
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
