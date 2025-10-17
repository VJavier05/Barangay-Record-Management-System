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
using System.IO;
using System.Drawing.Imaging;

namespace record_management_system.FORMS
{
    public partial class OFFICIALS : Form
    {
        public OFFICIALS()
        {
            InitializeComponent();
        }

        private void OFFICIALS_Load(object sender, EventArgs e)
        {

            if (Login.userole == "1")
            {


                using (MySqlConnection connection = new MySqlConnection(Login.connectstring))
                {
                    connection.Open();

                    string query = "SELECT image FROM sk_officials";
                    string queryname = "SELECT name FROM sk_officials";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlCommand command2 = new MySqlCommand(queryname, connection);
                    List<PictureBox> pictureBoxes = new List<PictureBox>();
                    pictureBoxes.Add(PBCHAIR);
                    pictureBoxes.Add(PBINGAT);
                    pictureBoxes.Add(PBKALIHIM);
                    pictureBoxes.Add(PBKAGAWAD1);
                    pictureBoxes.Add(PBKAGAWAD2);
                    pictureBoxes.Add(PBKAGAWAD3);
                    pictureBoxes.Add(PBKAGAWAD4);
                    pictureBoxes.Add(PBKAGAWAD5);


                    List<Label> labelList = new List<Label>();
                    labelList.Add(lblchair);
                    labelList.Add(lblingat);
                    labelList.Add(lblkalihim);
                    labelList.Add(lblkagawad1);
                    labelList.Add(lblkagawad2);
                    labelList.Add(lblkagawad3);
                    labelList.Add(lblkagawad4);
                    labelList.Add(lblkagawad5);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        int pictureBoxIndex = 0;

                        while (reader.Read())
                        {
                            if (pictureBoxIndex < pictureBoxes.Count) // pictureBoxes is a List<PictureBox> containing all the PictureBox controls
                            {
                                byte[] photoBytes = (byte[])reader["image"];

                                using (MemoryStream memoryStream = new MemoryStream(photoBytes))
                                {
                                    Image image = Image.FromStream(memoryStream);
                                    pictureBoxes[pictureBoxIndex].Image = image;
                                }
                            }
                            pictureBoxIndex++;

                        }
                    }
                    using (MySqlDataReader reader = command2.ExecuteReader())
                    {
                        int labelIndex = 0;

                        while (reader.Read())
                        {
                            if (labelIndex < labelList.Count)
                            {
                                string name = reader["name"].ToString();
                                labelList[labelIndex].Text = name;
                            }

                            labelIndex++;
                        }
                    }

                }
            }
            else
            {
                foreach (Control control in this.Controls)
                {
                    if (control is Button button)
                    {
                        button.Visible = false;
                    }
                }

                using (MySqlConnection connection = new MySqlConnection(Login.connectstring))
                {
                    connection.Open();

                    string query = "SELECT image FROM sk_officials";
                    string queryname = "SELECT name FROM sk_officials";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlCommand command2 = new MySqlCommand(queryname, connection);
                    List<PictureBox> pictureBoxes = new List<PictureBox>();
                    pictureBoxes.Add(PBCHAIR);
                    pictureBoxes.Add(PBINGAT);
                    pictureBoxes.Add(PBKALIHIM);
                    pictureBoxes.Add(PBKAGAWAD1);
                    pictureBoxes.Add(PBKAGAWAD2);
                    pictureBoxes.Add(PBKAGAWAD3);
                    pictureBoxes.Add(PBKAGAWAD4);
                    pictureBoxes.Add(PBKAGAWAD5);


                    List<Label> labelList = new List<Label>();
                    labelList.Add(lblchair);
                    labelList.Add(lblingat);
                    labelList.Add(lblkalihim);
                    labelList.Add(lblkagawad1);
                    labelList.Add(lblkagawad2);
                    labelList.Add(lblkagawad3);
                    labelList.Add(lblkagawad4);
                    labelList.Add(lblkagawad5);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        int pictureBoxIndex = 0;

                        while (reader.Read())
                        {
                            if (pictureBoxIndex < pictureBoxes.Count) // pictureBoxes is a List<PictureBox> containing all the PictureBox controls
                            {
                                byte[] photoBytes = (byte[])reader["image"];

                                using (MemoryStream memoryStream = new MemoryStream(photoBytes))
                                {
                                    Image image = Image.FromStream(memoryStream);
                                    pictureBoxes[pictureBoxIndex].Image = image;
                                }
                            }
                            pictureBoxIndex++;

                        }
                    }
                    using (MySqlDataReader reader = command2.ExecuteReader())
                    {
                        int labelIndex = 0;

                        while (reader.Read())
                        {
                            if (labelIndex < labelList.Count)
                            {
                                string name = reader["name"].ToString();
                                labelList[labelIndex].Text = name;
                            }

                            labelIndex++;
                        }
                    }

                }


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            RD_FORM.officialsadd form1 = new RD_FORM.officialsadd(this);

       

            form1.txtbxFname.Text = lblchair.Text;
            form1.comboBox1.Text = label2.Text;

            form1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RD_FORM.officialsadd form1 = new RD_FORM.officialsadd(this);

            form1.txtbxFname.Text = lblingat.Text;
            form1.comboBox1.Text = label4.Text;

            form1.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RD_FORM.officialsadd form1 = new RD_FORM.officialsadd(this);

            form1.txtbxFname.Text = lblkagawad2.Text;
            form1.comboBox1.Text = label9.Text;

            form1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RD_FORM.officialsadd form1 = new RD_FORM.officialsadd(this);

            form1.txtbxFname.Text = lblkalihim.Text;
            form1.comboBox1.Text = label6.Text;

            form1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RD_FORM.officialsadd form1 = new RD_FORM.officialsadd(this);

            form1.txtbxFname.Text = lblkagawad1.Text;
            form1.comboBox1.Text = label7.Text;

            form1.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RD_FORM.officialsadd form1 = new RD_FORM.officialsadd(this);

            form1.txtbxFname.Text = lblkagawad3
                .Text;
            form1.comboBox1.Text = label11.Text;

            form1.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            RD_FORM.officialsadd form1 = new RD_FORM.officialsadd(this);

            form1.txtbxFname.Text = lblkagawad4.Text;
            form1.comboBox1.Text = label15.Text;

            form1.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RD_FORM.officialsadd form1 = new RD_FORM.officialsadd(this);

            form1.txtbxFname.Text = lblkagawad5.Text;
            form1.comboBox1.Text = label13.Text;

            form1.ShowDialog();
        }

        private void OFFICIALS_Resize(object sender, EventArgs e)
        {
           
        }
    }
}
