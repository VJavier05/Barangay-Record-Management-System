using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace record_management_system
{
    public partial class dashboard : Form
    {

        //fields
        private Button currentbtn;
        private Form activeform;
       

        //constructor
        public dashboard()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd,int wMsg, int wParam, int lParam);


        //methods
        private Color SelectThemeColor()
        {
            string color = "#2980b9";

            //ColorTranslator.fromHtml convert to System.drawing.color para ma set ung color
            return ColorTranslator.FromHtml(color);

        }
        private void ActiveButton(object btnSender)
        {
            if (btnSender != null)
            {
                DisableButton();

 
                    //NOTE: kunin ung nakuhang color sa SelectThemeColor METHOD
                    Color color = SelectThemeColor();

                    //assign currentbtn equal to btnSender
                    currentbtn = (Button)btnSender;
                    currentbtn.BackColor = color;
                    currentbtn.ForeColor = Color.White;
                    currentbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                   
                
            }
        }
        private void OpenForm(Form childform, object btnsender)
        {
            if (activeform != null)
            {
                activeform.Close();
            }
            ActiveButton(btnsender);
            activeform = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            this.panelmain.Controls.Add(childform);
            this.panelmain.Tag = childform;
            childform.BringToFront();
            childform.Show();

            string formNameWithSpaces = childform.Text.Replace("_", " ");
            lbltitle.Text = formNameWithSpaces;
        }
        private void OpenForm2(Form childform)
        {
            if (activeform != null)
            {
                activeform.Close();
            }
            activeform = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            this.panelmain.Controls.Add(childform);
            this.panelmain.Tag = childform;
            childform.BringToFront();
            childform.Show();

            string formNameWithSpaces = childform.Text.Replace("_", " ");
            lbltitle.Text = formNameWithSpaces;
        }


        private void DisableButton()
        {

            //bibilang kung ilan ung control sa panelmenu which are ung buttons
            foreach (Control previousBtn in pnlmenu.Controls)
            {
                //if statememt para i check kung ung control is buttons only
                if (previousBtn.GetType() ==  typeof(Button))
                {
                    //change property ng button
                    previousBtn.BackColor = Color.FromArgb(37, 41, 88);
                    previousBtn.ForeColor = Color.White;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void btndash_Click(object sender, EventArgs e)
        {
            //using ung sender as parameter, thinks of sender as ung kung ano ung button na na click

            ActiveButton(sender);
            if (activeform != null)
            {
                activeform.Close();
                
            }
            
            Reset();
            
        }
        private void Reset()
        {
            
            lbltitle.Text = "DASHBOARD";
            currentbtn = null;
            
        }
       

        private void btnsk_Click(object sender, EventArgs e)
        {
            OpenForm(new FORMS.SK_BENEFICIARIES(),sender);
        }

        private void btnfile_Click(object sender, EventArgs e)
        {
            OpenForm(new FORMS.FILE_MANAGER(), sender);
        }

        private void btnuser_Click(object sender, EventArgs e)
        {
            OpenForm(new FORMS.USERS(), sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //OpenForm2(new FORMS.SK_BENEFICIARIES());
            OpenForm(new FORMS.SK_BENEFICIARIES(), btnsk);
        }

        private void btndbfile_Click(object sender, EventArgs e)
        {
            //OpenForm2(new FORMS.FILE_MANAGER());
            OpenForm(new FORMS.FILE_MANAGER(), btnfile);
        }

        private void btndbusers_Click(object sender, EventArgs e)
        {
            //OpenForm2(new FORMS.USERS());
            OpenForm(new FORMS.USERS(), btnuser);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Logout?","LOGOUT CONFIRMATION",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Login.loadform(new Login());
                this.Hide();
            }
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(Login.connectstring))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM users";
                    string querysk = "SELECT COUNT(*) FROM sk_member";
                    string querytrans = "SELECT COUNT(*) FROM transaction";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlCommand command2 = new MySqlCommand(querysk, connection);
                    MySqlCommand command3 = new MySqlCommand(querytrans, connection);


                    int userCount = Convert.ToInt32(command.ExecuteScalar());
                    int userCount2 = Convert.ToInt32(command2.ExecuteScalar());
                    int userCount3 = Convert.ToInt32(command3.ExecuteScalar());


                    lbluser.Text = Convert.ToString(userCount);
                    lblskbene.Text = Convert.ToString(userCount2);
                    label4.Text = Convert.ToString(userCount3);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

                if (Login.userole == "1")
            {
                btnuser.Visible = true;
                panel1.Visible = true;
            }
            else
            {
                panel1.Visible = false;
                btnuser.Visible = false;
                btndbusers.Visible = false;
            }
        }

        private void pnltitlebar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle,0x112,0xf012,0 );
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
           
           this.WindowState = FormWindowState.Minimized;
            
        }

        private void btnexit_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to Exit?","Exit Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenForm(new FORMS.OFFICIALS(), sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenForm(new FORMS.TRANSACTION(), sender);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            OpenForm(new FORMS.TRANSACTION(), btntrans);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            OpenForm(new FORMS.TRANSACTION(), button2);
        }
    }
}
