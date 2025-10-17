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


namespace record_management_system.RD_FORM
{
    public partial class officialsadd : Form
    {
        MySqlCommand cm;
        MySqlDataReader dr;
        private FORMS.OFFICIALS offificialform;
        public officialsadd(FORMS.OFFICIALS form1)
        {
            InitializeComponent();
            this.offificialform = form1;
        }

        public void load()
        {
            using (MySqlConnection connection = new MySqlConnection(Login.connectstring))
            {
                connection.Open();

                string query = "SELECT image FROM sk_officials";
                string queryname = "SELECT name FROM sk_officials";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlCommand command2 = new MySqlCommand(queryname, connection);
                List<PictureBox> pictureBoxes = new List<PictureBox>();
                pictureBoxes.Add(offificialform.PBCHAIR);
                pictureBoxes.Add(offificialform.PBINGAT);
                pictureBoxes.Add(offificialform.PBKALIHIM);
                pictureBoxes.Add(offificialform.PBKAGAWAD1);
                pictureBoxes.Add(offificialform.PBKAGAWAD2);
                pictureBoxes.Add(offificialform.PBKAGAWAD3);
                pictureBoxes.Add(offificialform.PBKAGAWAD4);
                pictureBoxes.Add(offificialform.PBKAGAWAD5);


                List<Label> labelList = new List<Label>();
                labelList.Add(offificialform.lblchair);
                labelList.Add(offificialform.lblingat);
                labelList.Add(offificialform.lblkalihim);
                labelList.Add(offificialform.lblkagawad1);
                labelList.Add(offificialform.lblkagawad2);
                labelList.Add(offificialform.lblkagawad3);
                labelList.Add(offificialform.lblkagawad4);
                labelList.Add(offificialform.lblkagawad5);

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
        public byte[] ImageBytesToShow { get; set; }
        private void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                if (photoBytes == null)
                {
                    MessageBox.Show("Please select an image first.", "Image Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {



                    if (comboBox1.Text == "SK CHAIRMAN")
                    {
                        using (MySqlConnection connection = new MySqlConnection(Login.connectstring))
                        {
                            connection.Open();

                            string query = "UPDATE sk_officials SET name= @name,image= @PhotoValue WHERE ID = 1";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@name", txtbxFname.Text);
                            command.Parameters.AddWithValue("@PhotoValue", photoBytes);

                            command.ExecuteNonQuery();
                            MessageBox.Show("Chairman Updated");

                            offificialform.lblchair.Text = txtbxFname.Text;

                            this.Close();
                            connection.Close();
                        }


                    }
                    else if (comboBox1.Text == "SK INGAT-YAMAN")
                    {
                        using (MySqlConnection connection = new MySqlConnection(Login.connectstring))
                        {
                            connection.Open();

                            string query = "UPDATE sk_officials SET name= @name,image= @PhotoValue WHERE ID = 2";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@name", txtbxFname.Text);
                            command.Parameters.AddWithValue("@PhotoValue", photoBytes);

                            command.ExecuteNonQuery();
                            MessageBox.Show("Ingat-Yaman Updated");
                            offificialform.lblingat.Text = txtbxFname.Text;

                            this.Close();
                            connection.Close();
                        }
                    }
                    else if (comboBox1.Text == "SK KALIHIM")
                    {
                        using (MySqlConnection connection = new MySqlConnection(Login.connectstring))
                        {
                            connection.Open();

                            string query = "UPDATE sk_officials SET name= @name,image= @PhotoValue WHERE ID = 3";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@name", txtbxFname.Text);
                            command.Parameters.AddWithValue("@PhotoValue", photoBytes);

                            command.ExecuteNonQuery();
                            MessageBox.Show("Kalihim Updated");

                            offificialform.lblkalihim.Text = txtbxFname.Text;
                            this.Close();
                            connection.Close();
                        }
                    }
                    else if (comboBox1.Text == "SK KAGAWAD 1")
                    {
                        using (MySqlConnection connection = new MySqlConnection(Login.connectstring))
                        {
                            connection.Open();

                            string query = "UPDATE sk_officials SET name= @name,image= @PhotoValue WHERE ID = 4";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@name", txtbxFname.Text);
                            command.Parameters.AddWithValue("@PhotoValue", photoBytes);

                            command.ExecuteNonQuery();
                            MessageBox.Show("Kagawad 1 Updated");
                            offificialform.lblkagawad1.Text = txtbxFname.Text;
                            this.Close();
                            connection.Close();
                        }
                    }
                    else if (comboBox1.Text == "SK KAGAWAD 2")
                    {
                        using (MySqlConnection connection = new MySqlConnection(Login.connectstring))
                        {
                            connection.Open();

                            string query = "UPDATE sk_officials SET name= @name,image= @PhotoValue WHERE ID = 5";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@name", txtbxFname.Text);
                            command.Parameters.AddWithValue("@PhotoValue", photoBytes);

                            command.ExecuteNonQuery();
                            MessageBox.Show("Kagawad 2 Updated");
                            offificialform.lblkagawad2.Text = txtbxFname.Text;
                            this.Close();
                            connection.Close();
                        }
                    }
                    else if (comboBox1.Text == "SK KAGAWAD 3")
                    {
                        using (MySqlConnection connection = new MySqlConnection(Login.connectstring))
                        {
                            connection.Open();

                            string query = "UPDATE sk_officials SET name= @name,image= @PhotoValue WHERE ID = 6";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@name", txtbxFname.Text);
                            command.Parameters.AddWithValue("@PhotoValue", photoBytes);

                            command.ExecuteNonQuery();
                            MessageBox.Show("Kagawad 3 Updated");
                            offificialform.lblkagawad3.Text = txtbxFname.Text;
                            this.Close();
                            connection.Close();
                        }
                    }
                    else if (comboBox1.Text == "SK KAGAWAD 4")
                    {
                        using (MySqlConnection connection = new MySqlConnection(Login.connectstring))
                        {
                            connection.Open();

                            string query = "UPDATE sk_officials SET name= @name,image= @PhotoValue WHERE ID = 7";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@name", txtbxFname.Text);
                            command.Parameters.AddWithValue("@PhotoValue", photoBytes);

                            command.ExecuteNonQuery();
                            MessageBox.Show("Kagawad 4 Updated");
                            offificialform.lblkagawad4.Text = txtbxFname.Text;
                            this.Close();
                            connection.Close();
                        }
                    }
                    else if (comboBox1.Text == "SK KAGAWAD 5")
                    {
                        using (MySqlConnection connection = new MySqlConnection(Login.connectstring))
                        {
                            connection.Open();

                            string query = "UPDATE sk_officials SET name= @name,image= @PhotoValue WHERE ID = 8";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@name", txtbxFname.Text);
                            command.Parameters.AddWithValue("@PhotoValue", photoBytes);

                            command.ExecuteNonQuery();
                            MessageBox.Show("Kagawad 5 Updated");
                            offificialform.lblkagawad5.Text = txtbxFname.Text;
                            this.Close();
                            connection.Close();
                        }

                    }
                    load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        byte[] photoBytes;
        private void btnbrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Load the image into a PictureBox
                pictureBox1.Image = Image.FromFile(imagePath);

               
                photoBytes = File.ReadAllBytes(imagePath);
            }
        }

        private void officialsadd_Load(object sender, EventArgs e)
        {

        }
    }
}
