using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace record_management_system.RD_FORM
{
    public partial class edit_username : Form
    {
        private FORMS.USERS form1;
        
        MySqlCommand cm;
        MySqlDataReader dr;

        //FORMS.USERS form1
        public edit_username(FORMS.USERS form1)
        {
            InitializeComponent();
          
            this.form1 = form1;
           
        
            
           
            
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtbxusername_TextChanged(object sender, EventArgs e)
        {
            
            if (txtbxusername.Text == "")
            {
                usererror.Visible = true;
                errormsg.Text = "Please Fill up form!";
            }
            else
            {
                usererror.Visible = false;
                errormsg.Text = "";
            }
        }

        private void edit_username_Load(object sender, EventArgs e)
        {
            
        }


        public void loadrecord()
        {
            MySqlConnection dbconnect = new MySqlConnection(Login.connectstring);
            try
            {
               
                form1.dataGridView1.Rows.Clear();
                dbconnect.Open();
                cm = new MySqlCommand("SELECT * FROM users WHERE user_role = 2", dbconnect);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    form1.dataGridView1.Rows.Add(dr["ID"].ToString(), dr["username"].ToString(), dr["password"].ToString());
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
        private void btnlogin_Click(object sender, EventArgs e)
        {

            string newUsername = txtbxusername.Text;

            try
                {
                //using (SqlConnection connection = new SqlConnection(Login.connectstring))
               
                databaseconnection.Open();
                //ERROR HERE
                string updateQuery = "UPDATE users SET username ='" + newUsername + "'WHERE ID = "+ Convert.ToInt32(form1.primaryid);
               
                
                MySqlCommand Commanddatabase = new MySqlCommand(updateQuery, databaseconnection);

                if (Commanddatabase.ExecuteNonQuery() == 1)
                {
                     //MessageBox.Show("Query Executed");
                     MessageBox.Show("Username Updated", "USERNAME UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadrecord();
                     this.Close();

                 }
                 else
                 {
                     //MessageBox.Show("Query Not Executed");
                     MessageBox.Show("Username Not Updated", "USERNAME UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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


                        public void executeQuery(string query)
                        {
                        try
                        {

                         MySqlCommand Commanddatabase = new MySqlCommand(query, databaseconnection);

                         databaseconnection.Open();

                         if (Commanddatabase.ExecuteNonQuery() == 1)
                         {
                             //MessageBox.Show("Query Executed");
                             MessageBox.Show("Username ", "Forget Password", MessageBoxButtons.OK, MessageBoxIcon.Information);

                         }
                         else
                         {
                             //MessageBox.Show("Query Not Executed");
                             MessageBox.Show("Username Not Updated", "Forget Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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


                        }
                        }
