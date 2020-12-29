using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PinBot
{
    public partial class ucManage : UserControl
    {
        public ucManage(string _Path, string Description, string strSourceURL, BindingSource bs, string bid, bool isNew, bool TRIAL, ManageQueue parent = null, bool repin = false)
        {
            InitializeComponent();
            this._Path = _Path;
            txtDescription.Text = Description;
            this.pic.ImageLocation = _Path;
            cboBoard.DataSource = bs;
            cboBoard.DisplayMember = "Value";
            cboBoard.ValueMember = "Key";
            if (bid != null)
                cboBoard.SelectedValue = bid;
            this.isNew = isNew;
            this.parent = parent;
            this.TRIAL = TRIAL;
            txtSourceURL.Text = strSourceURL;
            if (TRIAL)
            {
                txtSourceURL.Enabled = false;
            }

            repin = false;
        }

        public ucManage(RePin_Pin pin, BindingSource bs, bool isNew, bool TRIAL, ManageQueue parent = null)
        {
            InitializeComponent();

            btnEditImg.Visible = false;
            this.PIN = pin;

            this._Path = pin.img_url;
            this.pic.ImageLocation = _Path;
            
            txtDescription.Text = pin.description;
            
            cboBoard.DataSource = bs;
            cboBoard.DisplayMember = "Value";
            cboBoard.ValueMember = "Key";
            if (pin.boardID != null)
                cboBoard.SelectedValue = pin.boardID;

            this.isNew = isNew;
            this.parent = parent;
            this.TRIAL = TRIAL;

            txtSourceURL.Text = pin.link;
            if (TRIAL)
            {
                txtSourceURL.Enabled = false;
            }

            repin = true;
        }

        public string _Path;
        private bool isNew;
        private ManageQueue parent;
        private bool TRIAL;
        private bool repin;
        private RePin_Pin PIN;

        private void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }

        public void save()
        {
            if (repin)
                save_repin();
            else
                save_pin();
        }
        void save_pin()
        {
            try
            {
                if (!this.Visible) return;
                SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
                if (!isNew)
                {
                    if (txtDescription.Text.Length <= 0)
                        throw new Exception();
                    if (cboBoard.SelectedValue == null)
                        throw new Exception();
                    m_dbConnection.Open();
                    string sql = "UPDATE pins SET boardID='" + cboBoard.SelectedValue + "', description='" + txtDescription.Text.Replace("'", "''") + "', source_url='" + txtSourceURL.Text.Replace("'", "''") + "' where path='" + _Path + "'";
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                    m_dbConnection.Close();
                    visibleOK(true);
                    picOK.Image = PinBot.Properties.Resources.ok;
                }
                else
                {

                    if (txtDescription.Text.Length <= 0)
                        throw new Exception();
                    if (cboBoard.SelectedValue == null)
                        throw new Exception();

                    string d = ".\\imgs\\";

                    if (!System.IO.Directory.Exists(d))
                        System.IO.Directory.CreateDirectory(d);

                    string new_d = Path.GetFullPath(d + Path.GetFileName(_Path));
                    while (File.Exists(new_d))
                    {
                        Random r = new Random();
                        new_d = Path.GetFullPath(d + Path.GetFileNameWithoutExtension(_Path) + r.Next(0, 10000) + Path.GetExtension(_Path));
                    }
                    File.Copy(_Path, new_d);
                    _Path = new_d;


                    m_dbConnection.Open();
                    string sql = "INSERT INTO pins (boardID, description, path, source_url) VALUES ('" + cboBoard.SelectedValue + "', '" + txtDescription.Text.Replace("'", "''") + "', '" + _Path + "', '" + txtSourceURL.Text.Replace("'", "''") + "')";
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                    m_dbConnection.Close();
                    //this.Visible = false;
                    isNew = false;
                    visibleOK(true);
                    picOK.Image = PinBot.Properties.Resources.ok;
                }
            }
            catch
            {
                picOK.Image = PinBot.Properties.Resources.nok;
                visibleOK(true);
            }
        }
        void save_repin()
        {
            try
            {
                if (!this.Visible) return;
                SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");

                if (txtDescription.Text.Length <= 0)
                    throw new Exception();
                if (cboBoard.SelectedValue == null)
                    throw new Exception();

                RePin_Pin p = PIN;
                p.link = txtSourceURL.Text;
                p.description = txtDescription.Text;
                p.boardID = cboBoard.SelectedValue.ToString();
                p.img_url = pic.ImageLocation;

                MemoryStream ms = new MemoryStream();
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(ms, p);
                string obj = Convert.ToBase64String(ms.ToArray());

                m_dbConnection.Open();
                string sql = "UPDATE repins SET pin='" + obj + "' where ID='" + PIN.DB_ID + "'";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                m_dbConnection.Close();
                visibleOK(true);
                picOK.Image = PinBot.Properties.Resources.ok;
                
               
            }
            catch
            {
                picOK.Image = PinBot.Properties.Resources.nok;
                visibleOK(true);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
                m_dbConnection.Open();
                string sql = "";
                if (repin)
                    sql = "DELETE from repins where ID=" + PIN.DB_ID;
                else
                    sql = "DELETE from pins where path='" + _Path + "'";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                m_dbConnection.Close();

                this.Visible = false;

                if (parent != null)
                    parent.checkDB();
            }
            catch { MessageBox.Show("Error occured: #Del_ucM", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }



        public void visibleOK(bool v)
        {
            picOK.Visible = v;
        }

        private void btnEditImg_Click(object sender, EventArgs e)
        {
            if (TRIAL)
            {
                MessageBox.Show("Sorry, this feature is for Premium only.", "Premium only", MessageBoxButtons.OK);
                return;
            }

            ImageProcessing ip = new ImageProcessing(pic.Image);
            ip.ShowDialog();
            pic.Image = (Image)ip.BITMAP;
            pic.Image.Save(pic.ImageLocation);
        }

    }
}
