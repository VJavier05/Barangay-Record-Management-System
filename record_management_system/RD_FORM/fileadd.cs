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


namespace record_management_system.RD_FORM
{
    public partial class fileadd : Form
    {
        private FORMS.FILE_MANAGER filefrm;
       
        MySqlCommand cm;
        MySqlDataReader dr;

        public fileadd(FORMS.FILE_MANAGER fileform)
        {
            InitializeComponent();
            this.filefrm = fileform;
        }

   
        private void btnlogin_Click(object sender, EventArgs e)
        {

            string textvalue = txtbxfilename.Text;
            if (textvalue.EndsWith("doc") || textvalue.EndsWith("docx") || textvalue.EndsWith("png") || textvalue.EndsWith("jpeg") || textvalue.EndsWith("jpg") || textvalue.EndsWith("xls") || textvalue.EndsWith("xlsx") || textvalue.EndsWith("pdf"))
            {

                DateTime date = dateTimePicker1.Value;
                string dateadded = date.ToString("yyyy-MM-dd");

                //UPLOAD
                try
                {
                    //eto bigy mo
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        using (MySqlConnection connection = new MySqlConnection(con))
                        {
                            connection.Open();

                            string insertSql = "INSERT INTO file (filename, data, filetype,dateadded,filedescription,userid) VALUES (@FileName, @Data, @FileType, @Dateadded, @Filedescription, @Userid)";
                            MySqlCommand insertCommand = new MySqlCommand(insertSql, connection);
                            insertCommand.Parameters.AddWithValue("@FileName", txtbxfilename.Text);
                            insertCommand.Parameters.AddWithValue("@Data", File.ReadAllBytes(filePath));
                            insertCommand.Parameters.AddWithValue("@FileType", selectedFileType);
                            insertCommand.Parameters.AddWithValue("@Dateadded", dateadded);
                            insertCommand.Parameters.AddWithValue("@Filedescription", txtbxdes.Text);
                            insertCommand.Parameters.AddWithValue("@Userid", Login.userid);
                            insertCommand.ExecuteNonQuery();
                            connection.Close();
                        }

                        MessageBox.Show("File uploaded and saved successfully.");
                        userload();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No file selected","File Add Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
            else
            {
                MessageBox.Show("Please add the file type at the end of filename");
            }

            

        }
        public void userload()
        {
            MySqlConnection dbconnect = new MySqlConnection(Login.connectstring);
            try
            {
                if (Login.rolecheck == "1")
                {

                    filefrm.dataGridView1.Rows.Clear();
                    dbconnect.Open();
                    cm = new MySqlCommand("SELECT * FROM file", dbconnect);
                    dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        filefrm.dataGridView1.Rows.Add(dr["ID"].ToString(), dr["filename"].ToString(), dr["filetype"].ToString(), DateTime.Parse(dr["dateadded"].ToString()).ToShortDateString(), dr["filedescription"].ToString());
                    }
                    filefrm.dataGridView1.Columns["btndel"].Visible = true;
                }
                else
                {
                    filefrm.dataGridView1.Rows.Clear();
                    dbconnect.Open();
                    cm = new MySqlCommand("SELECT * FROM file", dbconnect);
                    dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        filefrm.dataGridView1.Rows.Add(dr["ID"].ToString(), dr["filename"].ToString(), dr["filetype"].ToString(), DateTime.Parse(dr["dateadded"].ToString()).ToShortDateString(), dr["filedescription"].ToString());
                    }
                    filefrm.dataGridView1.Columns["btndel"].Visible = false;
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

        MySqlConnection databaseconnection = new MySqlConnection(Login.connectstring);

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string filepath { get; set; }
        string filePath, selectedFilter, selectedFileType;

        private void txtbxfilename_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbxfilename_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con))
                {
                    connection.Open();
                    //string adduser = "UPDATE sk_member SET fname ='" + txtbxFname.Text + "',mname ='" + txtbxmname.Text + "',lname = '" + txtbxlname.Text + "',age = '" + age + "',birthdays = '" + formattedBirthday + "',contactnumber = '" + txtbxcontact.Text + "',gender = '" + gender + "',address = '" + txtbxaddress.Text + "', status = 'Inactive' WHERE ID = " + Convert.ToInt32(skform.primaryid);

                    string insertSql = "UPDATE file SET filename = @FileName ,filedescription = @Filedescription WHERE ID = @id";
                    MySqlCommand insertCommand = new MySqlCommand(insertSql, connection);
                    insertCommand.Parameters.AddWithValue("@FileName", txtbxfilename.Text);
               
                    insertCommand.Parameters.AddWithValue("@Filedescription", txtbxdes.Text);
                    insertCommand.Parameters.AddWithValue("@id", Convert.ToInt32(filefrm.primaryid));
                    insertCommand.ExecuteNonQuery();
                }

                MessageBox.Show("File edited and saved successfully.");
                userload();
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string con = "data source = localhost;port = 3307;username = root;password = 'pass';database = record_management";
        private void btnbrowse_Click(object sender, EventArgs e)
        {
         
           
            try
            {
              
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|Word Files (*.doc; *.docx)|*.doc;*.docx|Excel Files (*.xls; *.xlsx)|*.xls;*.xlsx|JPEG Files (*.jpg; *.jpeg)|*.jpg;*.jpeg|PNG Files (*.png)|*.png";
                    openFileDialog.FilterIndex = 1;  // Set the default filter index

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filePath = openFileDialog.FileName;
                        selectedFilter = openFileDialog.Filter;
                        selectedFileType = openFileDialog.SafeFileName.Substring(openFileDialog.SafeFileName.LastIndexOf('.') + 1);


                       
                        txtbxfilename.Text = Path.GetFileName(filePath);
                        /*using (MySqlConnection connection = new MySqlConnection(con))
                        {
                            connection.Open();

                            string insertSql = "INSERT INTO file (filename, data, filetype) VALUES (@FileName, @Data, @FileType)";
                            MySqlCommand insertCommand = new MySqlCommand(insertSql, connection);
                            insertCommand.Parameters.AddWithValue("@FileName", Path.GetFileName(filePath));
                            insertCommand.Parameters.AddWithValue("@Data", File.ReadAllBytes(filePath));
                            insertCommand.Parameters.AddWithValue("@FileType", selectedFileType);
                            insertCommand.ExecuteNonQuery();
                        }*/

  
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MySqlConnection connection = new MySqlConnection(con);
                connection.Close();
            }
        }



    }

      
}
