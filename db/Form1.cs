using System;
using System.Data;
using System.Data.SqlClient;

namespace db
{
    public partial class Form1 : Form
    {
        string a;
        public SqlConnection cnn;
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=laboras;User ID=dominykas;Password=1234";
        public Form1()
        {
            InitializeComponent();
        }

        private void prisijungti_Click(object sender, EventArgs e)
        {
            cnn = new SqlConnection(connectionString);
            a = "a";
            try
            {
                cnn.Open();
                MessageBox.Show("Sekmingai prisijungta, prasome uzkrauti duomenis");
                cnn.Close();
            }
            catch
            {
                MessageBox.Show("Kazkas negerai " + e, "DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tikrinadb_Click(object sender, EventArgs e)
        {
            string sql, query;
            sql = "SELECT * FROM duomenys";
            SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "duomenys");
            koziuri.DataSource = ds.Tables["duomenys"].DefaultView;
        }

        private void submit1_Click(object sender, EventArgs e)
        {
            try
            {
                string query;

                query = "INSERT INTO dbo.duomenys (vardas, pavarde, telefonas, miestas) VALUES (@vardas, @pavarde, @telefonas, @miestas)";

                SqlCommand insert = new SqlCommand(query, cnn);
                insert.Parameters.Add("@vardas", SqlDbType.NChar).Value = vardas.Text;
                insert.Parameters.Add("@pavarde", SqlDbType.NChar).Value = pavarde.Text;
                insert.Parameters.Add("@telefonas", SqlDbType.NChar).Value = telefonas.Text;
                insert.Parameters.Add("@miestas", SqlDbType.NChar).Value = miestas.Text;
                insert.Connection.Open();
                insert.ExecuteNonQuery();
                cnn.Close();
                tikrinadb.PerformClick();
            }
            catch
            {
                MessageBox.Show("Kaþkas negerai " + e, "DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void submit2_Click(object sender, EventArgs e)
        {
            try
            {
                string query;
                if (!String.IsNullOrEmpty(vardas.Text))
                {
                    query = "UPDATE duomenys SET vardas=@vardas WHERE id=@id";
                    SqlCommand update = new SqlCommand(query, cnn);
                    update.Parameters.Add("@vardas", SqlDbType.NChar).Value = vardas.Text;
                    update.Parameters.Add("@id", SqlDbType.Int).Value = idtaisyti.Text;
                    update.Connection.Open();
                    update.ExecuteNonQuery();
                    cnn.Close();
                }
                if (!String.IsNullOrEmpty(pavarde.Text))
                {
                    query = "UPDATE duomenys SET pavarde=@pavarde WHERE id=@id";
                    SqlCommand update = new SqlCommand(query, cnn);
                    update.Parameters.Add("@pavarde", SqlDbType.NChar).Value = pavarde.Text;
                    update.Parameters.Add("@id", SqlDbType.Int).Value = idtaisyti.Text;
                    update.Connection.Open();
                    update.ExecuteNonQuery();
                    cnn.Close();
                }
                if (!String.IsNullOrEmpty(telefonas.Text))
                {
                    query = "UPDATE duomenys SET telefonas=@telefonas WHERE id=@id";
                    SqlCommand update = new SqlCommand(query, cnn);
                    update.Parameters.Add("@telefonas", SqlDbType.NChar).Value = telefonas.Text;
                    update.Parameters.Add("@id", SqlDbType.Int).Value = idtaisyti.Text;
                    update.Connection.Open();
                    update.ExecuteNonQuery();
                    cnn.Close();
                }
                if (!String.IsNullOrEmpty(miestas.Text))
                {
                    query = "UPDATE duomenys SET miestas=@miestas WHERE id=@id";
                    SqlCommand update = new SqlCommand(query, cnn);
                    update.Parameters.Add("@miestas", SqlDbType.NChar).Value = miestas.Text;
                    update.Parameters.Add("@id", SqlDbType.Int).Value = idtaisyti.Text;
                    update.Connection.Open();
                    update.ExecuteNonQuery();
                    cnn.Close();
                }
                tikrinadb.PerformClick();
                vardas.Clear();
                pavarde.Clear();
                telefonas.Clear();
                miestas.Clear();
                idtaisyti.Clear();
            }
            catch
            {
                MessageBox.Show("Kazkas negerai " + e, "DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void submit3_Click(object sender, EventArgs e)
        {
            try
            {
                string query;

                query = "DELETE FROM duomenys WHERE id=@id";

                SqlCommand delSQL = new SqlCommand(query, cnn);
                delSQL.Parameters.Add("@id", SqlDbType.NChar).Value = idtrinti.Text;
                delSQL.Connection.Open();
                delSQL.ExecuteNonQuery();
                cnn.Close();
                tikrinadb.PerformClick();
            }
            catch
            {
                MessageBox.Show("Kazkas negerai " + e, "DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void submit4_Click(object sender, EventArgs e)
        {

            cnn.Open();
            string sql = "SELECT * FROM duomenys WHERE vardas=@vardas OR pavarde=@pavarde";
            SqlCommand com = new SqlCommand(sql, cnn);
            com.Parameters.AddWithValue("@vardas", searchvar.Text);
            com.Parameters.AddWithValue("@pavarde", searchpav.Text);

            using (SqlDataAdapter adapter = new SqlDataAdapter(com))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                koziuri.DataSource = dt.DefaultView;
            }
            cnn.Close();
        }
    }
}