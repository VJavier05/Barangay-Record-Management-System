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


namespace record_management_system
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public static string connectstring = "data source = localhost;port = 3307;username = root;password = 'pass';database = record_management";



        public static void loadform(object Form)
        {
            Form load = Form as Form;

            load.Show();
      
        }


        public static int userid;
        public static string rolecheck, userole, username;
        public void login()
        {
            MySqlConnection dbconnect = new MySqlConnection(connectstring);

            try
            {

                string Password = "";
                userole = "";
                bool IsExist = false;
                dbconnect.Open();
                MySqlCommand cmd = new MySqlCommand("select * from users where username='" + txtbxusername.Text + "'", dbconnect);
                MySqlDataReader sdr = cmd.ExecuteReader();
                cmd.CommandTimeout = 60;

                if (sdr.Read())
                {
                    userid = sdr.GetInt32(0);
                    Password = sdr.GetString(2);//get the user password from db if the user name is exist in that.
                    userole = sdr.GetString(3);
                    rolecheck = sdr.GetString(3);
                    
                    IsExist = true;
                }
                
                
                if (IsExist)  //if record exis in db , it will return true, otherwise it will return false
                {
                    if (Cryptography.Decrypt(Password).Equals(txtbxpass.Text))
                    {
                        string userdes = txtbxusername.Text;
                        username = userdes.Substring(0, 1).ToUpper() + userdes.Substring(1);
                        
                        if (userole == "1")
                        {
                            MessageBox.Show("Welcome Admin: " + username, "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            loadform(new dashboard());
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Welcome User: " + username, "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadform(new dashboard());
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password is wrong!...", "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else  //showing the error message if user credential is wrong
                {
                    MessageBox.Show("Please enter the valid credentials", "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            //prepare connection
            /*string query = "SELECT * FROM users INNER JOIN role ON users.user_role=role.role_id WHERE username = '" + txtbxusername.Text + "' AND password = '" + txtbxpass.Text + "'";

            MySqlConnection databaseconnection = new MySqlConnection(connectstring);
            MySqlCommand Commanddatabase = new MySqlCommand(query, databaseconnection);
            Commanddatabase.CommandTimeout = 60;

            //mySqldataApdapter retrive the data sa "Commanddatabase" then create new table after non gamit ung "fill"
            //ipapasok ung na retrive na data sa "dttable"
            MySqlDataAdapter adapter = new MySqlDataAdapter(Commanddatabase);
            DataTable dttable = new DataTable();
            adapter.Fill(dttable);

            try
            {
                databaseconnection.Open();
                userid = Convert.ToInt32(dttable.Rows[0]["ID"]);
                string username = Convert.ToString(dttable.Rows[0]["username"]);


                if (dttable.Rows.Count > 0)
                {
                    string usertype = dttable.Rows[0][5].ToString();
                    if (usertype == "admin")
                    {
                        MessageBox.Show("Welcome Admin: " + username, "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadform(new dashboard());
                        this.Hide();
                    }
                    else 
                    {
                        MessageBox.Show("Welcome User: " + username, "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadform(new dashboard());
                        this.Hide();
                    }
                   
                }
                else
                {
                    MessageBox.Show("USER NOT FOUND","Login",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    
                    txtbxpass.Clear();

                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("USER NOT FOUND", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtbxpass.Clear();
            }
            finally
            {
                databaseconnection.Close();
            }*/

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static void colorcontrol(TextBox tx1,TextBox tx2)
        {
            tx1.BackColor = Color.White;

            tx2.BackColor = SystemColors.Control; ;
        }
        public static void colorcontrol2(Panel pnl1, Panel pnl2)
        {
            pnl1.BackColor = Color.White;
            pnl2.BackColor = SystemColors.Control;
            
        } 


        private void txtbxusername_Click(object sender, EventArgs e)
        {

            colorcontrol(txtbxusername, txtbxpass);
            colorcontrol2(panelusernm,panelpass);

            if (txtbxpass.Text == "")
            {
                passerror.Visible = true;
                errormsg.Text = "Please Fill up form!";
            }
           
        }

        private void txtbxpass_Click(object sender, EventArgs e)
        {

            colorcontrol(txtbxpass, txtbxusername);
            colorcontrol2(panelpass, panelusernm);

           

            if (txtbxusername.Text == "")
            {
                usererror.Visible = true;
                errormsg.Text = "Please Fill up form!";
            }
            
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            txtbxpass.UseSystemPasswordChar = false;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            txtbxpass.UseSystemPasswordChar = true;
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

                if (txtbxpass.Text == "")
                {
                    passerror.Visible = true;
                    errormsg.Text = "Please Fill up form!";
                }
            }
           
        }

        private void txtbxpass_TextChanged(object sender, EventArgs e)
        {
            if (txtbxpass.Text == "")
            {
                passerror.Visible = true;
                errormsg.Text = "Please Fill up form!";
            }
            else
            {
                passerror.Visible = false;
                errormsg.Text = "";

                if (txtbxusername.Text == "")
                {
                    usererror.Visible = true;
                    errormsg.Text = "Please Fill up form!";
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtbxusername.Text == "" && txtbxpass.Text == "")
            {
                MessageBox.Show("Please input username and Password","Login Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                passerror.Visible = true;
                usererror.Visible = true;
                errormsg.Text = "Please Fill up form!";
            }
            else
            {
                login();
            }
        }

        private void btnFP_Click(object sender, EventArgs e)
        {
            loadform(new forgetpass());
            this.Hide();
        }
    }
}
