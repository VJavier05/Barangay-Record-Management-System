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
    public partial class skadd : Form
    {
        private FORMS.SK_BENEFICIARIES skform;
        MySqlCommand cm;
        MySqlDataReader dr;

        public skadd(FORMS.SK_BENEFICIARIES form1)
        {
            InitializeComponent();

            this.skform = form1;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void loadrecord()
        {
            MySqlConnection dbconnect = new MySqlConnection(Login.connectstring);
            try
            {

                skform.datagridviewsk.Rows.Clear();
                dbconnect.Open();
                cm = new MySqlCommand("SELECT * FROM sk_member", dbconnect);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    skform.datagridviewsk.Rows.Add(dr["ID"].ToString(), dr["fname"].ToString(), dr["mname"].ToString(), dr["lname"].ToString(), dr["age"].ToString(), DateTime.Parse(dr["birthdays"].ToString()).ToShortDateString(), dr["contactnumber"].ToString(), dr["gender"].ToString(), dr["address"].ToString(), dr["status"].ToString());
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
        public void reload()
        {
            string selectQuery = "SELECT * FROM sk_member";
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, Login.connectstring);
            adapter.Fill(table);
            skform.datagridviewsk.DataSource = table;
            loadrecord();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {

            if (txtbxFname.Text == "" || txtbxaddress.Text == "" || txtbxcontact.Text == "")
            {

                MessageBox.Show("Please fill up the form", "System Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                errormsg.Text = "Please Fill up form!";
            }
            else
            {
                if (radioButton1.Checked)
                {
                    gender = radioButton1.Text;
                }
                else
                {
                    gender = radioButton2.Text;
                }


                DateTime birthDate = dateTimePicker1.Value;
                string formattedBirthday = birthDate.ToString("yyyy-MM-dd");
                DateTime currentDate = DateTime.Now;
                int age = currentDate.Year - birthDate.Year;
                
                if (birthDate > currentDate.AddYears(-age))
                {
                    age--;

                }


              
                if (age <= 0)
                {
                    MessageBox.Show("Invalid Birthday!","Sk Beneficaries",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                //create condtion to check the age
                else if (age < 15 || age > 30)
                {
                    string adduser = "INSERT INTO sk_member(fname,mname,lname,age,birthdays,contactnumber,gender,address,status) VALUES('" + txtbxFname.Text + "','" + txtbxmname.Text + "','" + txtbxlname.Text + "','" + age + "','" + formattedBirthday + "','" + txtbxcontact.Text + "','" + gender + "','" + txtbxaddress.Text + "','" + "Inactive" + "')";


                    executeQuery(adduser, "Sk Beneficaries Added", "Sk Beneficaries not Added");
                    this.Close();
                }
                else
                {
                    string adduser = "INSERT INTO sk_member(fname,mname,lname,age,birthdays,contactnumber,gender,address,status) VALUES('" + txtbxFname.Text + "','" + txtbxmname.Text + "','" + txtbxlname.Text + "','" + age + "','" + formattedBirthday + "','" + txtbxcontact.Text + "','" + gender + "','" + txtbxaddress.Text + "','" + comboBox1.Text + "')";


                    executeQuery(adduser, "Sk Beneficaries Added", "Sk Beneficaries not Added");
                    this.Close();
                }


                loadrecord();

            }
        }
        MySqlConnection databaseconnection = new MySqlConnection(Login.connectstring);
        public void executeQuery(string query,string Mess1,string mess2)
        {
            try
            {

                MySqlCommand Commanddatabase = new MySqlCommand(query, databaseconnection);

                databaseconnection.Open();

                if (Commanddatabase.ExecuteNonQuery() == 1)
                {
                    //MessageBox.Show("Query Executed");
                    MessageBox.Show(Mess1, "Sk Beneficaries", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    //MessageBox.Show("Query Not Executed");
                    MessageBox.Show("Sk Beneficaries not Added", "Sk Beneficaries", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
        string gender;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void skadd_Load(object sender, EventArgs e)
        {
            loadrecord();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (txtbxFname.Text == "" || txtbxaddress.Text == "" || txtbxcontact.Text == "")
            {

                MessageBox.Show("Please fill up the form", "System Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                errormsg.Text = "Please Fill up form!";
            }
            else
            {
                if (radioButton1.Checked)
                {
                    gender = radioButton1.Text;
                }
                else
                {
                    gender = radioButton2.Text;
                }


                DateTime birthDate = dateTimePicker1.Value;
                string formattedBirthday = birthDate.ToString("yyyy-MM-dd");
                DateTime currentDate = DateTime.Now;
                int age = currentDate.Year - birthDate.Year;

                if (birthDate > currentDate.AddYears(-age))
                {
                    age--;

                }

                if (age <= 0)
                {
                    MessageBox.Show("Invalid Birthday!", "Sk Beneficaries", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (age < 15 || age > 30)
                {
                    string adduser = "UPDATE sk_member SET fname ='"+txtbxFname.Text+"',mname ='"+ txtbxmname.Text + "',lname = '" + txtbxlname.Text+"',age = '" + age + "',birthdays = '" + formattedBirthday + "',contactnumber = '" + txtbxcontact.Text +"',gender = '" + gender + "',address = '"+ txtbxaddress.Text + "', status = 'Inactive' WHERE ID = " + Convert.ToInt32(skform.primaryid);
                    //string adduser = "UPDATE sk_member SET fname = @fname, mname = @mname, lname = @lname, age = @age, birthdays = @birthdays, contactnumber = @contactnumber, gender = @gender, address = @address, status = @status WHERE ID = " + Convert.ToInt32(skform.primaryid);
                    
                    executeQuery(adduser, "Edit successful", "Edit not successful");
                    this.Close();
                }
                else
                {
                    string adduser = "UPDATE sk_member SET fname ='" + txtbxFname.Text + "',mname ='" + txtbxmname.Text + "',lname = '" + txtbxlname.Text + "',age = '" + age + "',birthdays = '" + formattedBirthday + "',contactnumber = '" + txtbxcontact.Text + "',gender = '" + gender + "',address = '" + txtbxaddress.Text + "', status ='"+ comboBox1.Text+"' WHERE ID = " + Convert.ToInt32(skform.primaryid);
                    
                    executeQuery(adduser, "Edit successful", "Edit not successful");
                    this.Close();
                }

                loadrecord();

            }

        }
    }
}
