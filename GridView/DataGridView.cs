using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace GridView
{
    public partial class DataGridView : Form
    {
        private SqlConnection cn = null;
        public DataGridView()
        {
            InitializeComponent();
        }

        private void DataGridView_Load(object sender, EventArgs e)
        {
            string cnStr = "Server = .; Database = QLBanHang; Integrated security = true";
            cn = new SqlConnection(cnStr);
            DGVProduct.DataSource = GetData();
        }
        private void Connect()
        {
            try
            {
                if (cn != null && cn.State != ConnectionState.Open)
                {
                    cn.Open();
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show("Cannot open a connection without specifying a data source or server \n\n" + ex.Message);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("A connection-level error occurred while opening the connection \n\n" + ex.Message);
            }
            catch (ConfigurationException ex)
            {
                MessageBox.Show("There are two entries with the same name in the <localdbinstances> section \n\n" + ex.Message);
            }
        }
        private void DisConnect() 
        {
            if (cn != null && cn.State != ConnectionState.Closed)
            {
                cn.Close();
            }
        }
        private List<object> GetData()
        {
            Connect();
            string sql = "SELECT * FROM SanPham";
            List<object> list = new List<object>();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                string MaSP, TenSP;
                int SoLuong;             
                double GiaSP;
                int CategoryID;
                while (dr.Read())
                {
                    MaSP = dr.GetString(0);
                    TenSP = dr.GetString(1);
                    SoLuong = dr.GetInt32(2);
                    GiaSP = dr.GetDouble(3);
                    CategoryID = dr.GetInt32(4);
                    var poor = new
                    {
                        ID = MaSP,
                        Name = TenSP,
                        Unit = SoLuong,
                        GiaSP = GiaSP,
                        CategoryID = CategoryID
                    };
                    list.Add(poor);
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DisConnect();
            }
            return list;
        }
    }
}
