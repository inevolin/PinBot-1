using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace PinBot
{
    public partial class ManageQueue : Form
    {
        private account acc;
        private bool debug = false;
        private bool TRIAL;
        private bool repin;
        private string table;

        public ManageQueue(account acc, bool TRIAL, bool repin = false)
        {
            InitializeComponent();
            this.acc = acc;
            this.TRIAL = TRIAL;
            this.repin = repin;
            if (repin)
                table = "repins";
            else
                table = "pins";
        }

        List<ucManage> ucm = new List<ucManage>();
        private void ManageQueue_Load(object sender, EventArgs e)
        {
            if (repin)
            {
                fill_repins();
                checkDB();
            }
            else
            { 
                fill_pins();
                checkDB();
            }
        }

        private void fill_pins(string limit = "")
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
            try
            {
                m_dbConnection.Open();
                string sql = "select path,description,boardID,source_url from " + table + " " + limit;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader sqlreader = command.ExecuteReader();
                while (sqlreader.Read())
                {

                    string p = sqlreader["path"].ToString();
                    string d = sqlreader["description"].ToString();
                    string bid = sqlreader["boardID"].ToString();
                    string src = sqlreader["source_url"].ToString();

                    BindingSource bs = new BindingSource(acc.boards, null);

                    ucManage um = new ucManage(p, d, src, bs, bid, false, TRIAL, this);
                    ucm.Add(um);
                    flp.Controls.Add(um);
                }

                m_dbConnection.Close();
            }
            catch { MessageBox.Show("Error occured: #Init_MQ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { m_dbConnection.Close(); }
        }
        private void fill_repins(string limit = "")
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
            try
            {
                m_dbConnection.Open();
                string sql = "select pin, ID from " + table + " " + limit;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader sqlreader = command.ExecuteReader();
                while (sqlreader.Read())
                {
                    BinaryFormatter b = new BinaryFormatter();
                    var bytes = Convert.FromBase64String(sqlreader["pin"].ToString());
                    MemoryStream mss = new MemoryStream(bytes);
                    RePin_Pin p = (RePin_Pin)b.Deserialize(mss);
                    p.DB_ID = int.Parse(sqlreader["ID"].ToString());

                    BindingSource bs = new BindingSource(acc.boards, null);

                    ucManage um = new ucManage(p, bs, false, TRIAL, this);
                    ucm.Add(um);
                    flp.Controls.Add(um);
                }
                sqlreader.Close();
            }
            catch { MessageBox.Show("Error occured: #Init_MQ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { m_dbConnection.Close();  }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < flp.Controls.Count; i++)
                {
                    Control c = flp.Controls[i];
                    if (c.GetType() == typeof(ucManage))
                        ((ucManage)c).save();

                }
            }
            catch { MessageBox.Show("Error occured: #SaveAll_MQ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnClearQueue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Emptying queue", MessageBoxButtons.OKCancel) == DialogResult.OK)
                clearDB();
            ucm.Clear();
            flp.Controls.Clear();

            if (repin)
                fill_repins();
            else
                fill_pins();
            
        }

        void clearDB()
        {
            try
            {
                SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
                m_dbConnection.Open();
                string sql = "delete from " + table + "";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                m_dbConnection.Close();
                btnClearQueue.Text = "empty queue";

            }
            catch (Exception ex) { if (debug) MessageBox.Show(ex.StackTrace); }
            clearImgs();
        }
        void clearImgs()
        {
            try
            {
                List<string> files = Directory.GetFiles("./imgs").ToList();
                foreach (string file in files)
                    File.Delete(file);
            }
            catch (Exception ex) { if (debug) MessageBox.Show(ex.StackTrace); }
        }

        public int checkDB()
        {
            int count = 0;
            try
            {
                SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
                m_dbConnection.Open();
                string sql = "select count(*) as c from " + table + "";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader sqlreader = command.ExecuteReader();
                string i = "";
                while (sqlreader.Read())
                {
                    i = sqlreader["c"].ToString();
                }
                m_dbConnection.Close();
                if (int.Parse(i) > 0)
                {
                    btnClearQueue.Text = "empty queue (" + i + ")";
                    count = int.Parse(i);
                }
                else
                    btnClearQueue.Text = "empty queue";
                return count;
            }
            catch (Exception ex) { if (debug) MessageBox.Show(ex.StackTrace); btnClearQueue.Text = "empty queue"; return count; }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            flp.Controls.Clear();

            if (repin)
                fill_repins();
            else
                fill_pins();

            checkDB();
        }
    }
}
