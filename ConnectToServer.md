# DataGridView








namespace ConnectionDatabase
{
    public partial class ConnectToServer : Form
    {
        public ConnectToServer()
        {
            InitializeComponent();
        }

        private SqlConnection cnn = null;
        private void ConnectToServer_Load(object sender, EventArgs e)
        {
            cnn = new SqlConnection();

        }

        private void Connect(string sql)
        {
            cnn.ConnectionString = sql;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    MessageBox.Show("Đã kết nối! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể thay đổi CSDL!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Không thể mở kết nối! " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DisConnect()
        {
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
        }

        private void btKetNoi_Click(object sender, EventArgs e)
        {
            string Server;
            Server = txtServerName.Text;
            string Database;
            Database = txtDatabaseName.Text;
            string cnnStr = "Server = " + Server + "; Database = " + Database;
            if (rbWindows.Checked == true)
            {
                cnnStr += "; Integrated Security = true";
            }
            if (rbSQL.Checked == true)
            {
                cnnStr += "; User id = " + txtUserName.Text + "; Password = " + txtPassword.Text;
            }
            Connect(cnnStr);
        }

        private void btNgatKetNoi_Click(object sender, EventArgs e)
        {
            DisConnect();
            MessageBox.Show("Đã ngắt kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void rbWindows_CheckedChanged(object sender, EventArgs e)
        {
            txtUserName.Enabled = txtPassword.Enabled = false;
        }

        private void rbSQL_CheckedChanged(object sender, EventArgs e)
        {
            txtUserName.Enabled = txtPassword.Enabled = true;
        }
    }
}
