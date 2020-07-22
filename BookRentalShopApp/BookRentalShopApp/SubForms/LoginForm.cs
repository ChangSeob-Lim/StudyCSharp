using System;
using System.Security.Cryptography;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;

namespace BookRentalShopApp.SubForms
{
    public partial class LoginForm : MetroForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            LoginProcess();
        }

        private void LoginProcess()
        {
            // 빈값비교 처리
            if (string.IsNullOrEmpty(TxtUserID.Text) || string.IsNullOrEmpty(TxtPassword.Text))
            //if (TxtUserID.Text == "" || TxtPassword.Text == "" || TxtUserID.Text == null || TxtPassword.Text == null)
            {
                //MessageBox.Show("아이디나 패스워드를 입력하세요!", "로그인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MetroMessageBox.Show(this, "아이디나 패스워드를 입력하세요!", "로그인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtUserID.Focus();
                return;
            }

            // 실제 DB처리
            string resultID = string.Empty; // ""

            try
            {
                using (MySqlConnection conn = new MySqlConnection(Commons.CONNSTR))
                {
                    conn.Open();
                    //MetroMessageBox.Show(this, $"DB 접속성공!!");
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT userID FROM userTBL " +
                                      " WHERE userID = @userID " +
                                      "   AND password = @password "; // @userID -> "'" + TxtUserID.Text + "'"
                    MySqlParameter paramUserID = new MySqlParameter("@userID", MySqlDbType.VarChar, 12);
                    paramUserID.Value = TxtUserID.Text.Trim();
                    MySqlParameter paramPassward = new MySqlParameter("@password", MySqlDbType.VarChar);
                    var md5Hash = MD5.Create();
                    var cryptoPassword = Commons.GetMd5Hash(md5Hash, TxtPassword.Text.Trim());
                    paramPassward.Value = cryptoPassword;//TxtPassword.Text.Trim();

                    cmd.Parameters.Add(paramUserID);
                    cmd.Parameters.Add(paramPassward);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();

                    if (!reader.HasRows)
                    {
                        MetroMessageBox.Show(this, "아이디나 패스워드가 틀립니다.", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TxtUserID.Text = TxtPassword.Text = string.Empty;
                        TxtUserID.Focus();
                        return;
                    }
                    else
                    {
                        resultID = reader["userID"] != null ? reader["userID"].ToString() : string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"DB 접속에러 : {ex.Message}", "로그인에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            if(string.IsNullOrEmpty(resultID))
            {
                MetroMessageBox.Show(this, "아이디나 패스워드가 틀립니다.", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtUserID.Text = TxtPassword.Text = string.Empty;
                TxtUserID.Focus();
                return;
            }
            else
            {
                MetroMessageBox.Show(this, $"{resultID} 로그인 성공");
                Commons.USERID = resultID; // 200720 12:30 추가
                this.Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void TxtUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                TxtPassword.Focus();
            }
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                BtnOk_Click(sender, new EventArgs());
            }
        }
    }
}
