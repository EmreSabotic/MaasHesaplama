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

namespace maaşprojesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=LAPTOP-KIFERO8A;Integrated security=SSPI;database=MaasTakip";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT Ad,Soyad FROM tblMaas ORDER BY ID"; // İstediğiniz sıralamaya göre ORDER BY ekleyin
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // İlgili sütunlardan veriyi al
                    string Ad = reader["Ad"].ToString();
                    string Soyad = reader["Soyad"].ToString();

                    // ComboBox'a ekleyin
                    comboBoxEdit1.Properties.Items.Add($"{Ad} {Soyad}");
                }

                con.Close();
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedName = comboBoxEdit1.SelectedItem.ToString(); // ComboBox'tan seçili kişiyi al

            // Seçilen kişinin bilgilerini almak için SQL sorgusunu hazırla
            string query = "SELECT ID, Telefon, Adres, Garanti, bonus FROM tblMaas WHERE CONCAT(Ad, ' ', Soyad) = @SelectedName";

            using (SqlConnection con = new SqlConnection("Server=LAPTOP-KIFERO8A;Integrated security=SSPI;database=MaasTakip"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SelectedName", selectedName);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Veritabanından gelen bilgileri TextBox'lara yaz
                    txtID.Text = reader["ID"].ToString();
                    txtTelefon.Text = reader["Telefon"].ToString();
                    txtAdres.Text = reader["Adres"].ToString();
                    txtGaranti.Text = reader["Garanti"].ToString();
                    txtBonus.Text = reader["Bonus"].ToString();
                }

                con.Close();
            }
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            int garanti = Convert.ToInt32(txtGaranti.Text);
            int bonus= Convert.ToInt32(txtBonus.Text);
            double intoran = 0.10; // yüzdelik dilimi bu 0.15 = %15  == 1 de %100 eşittir 
            double bonussonuc = bonus * intoran;
            MessageBox.Show(Convert.ToString("Bonusunuz : " + bonussonuc));


            double msonuc = bonussonuc + garanti;

            MessageBox.Show(Convert.ToString("Sonuç : " + msonuc));
            txtMaas.Text = msonuc.ToString();


            //   Bonus gelen paranın % 10'u ve garanti paranın toplamı çalışanın son maaşıdır..



        }
    }
    }


