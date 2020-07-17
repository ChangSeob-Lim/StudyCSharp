using MetroFramework;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRentalShopApp.SubForms
{
    public partial class DivMngForm : MetroForm
    {
        readonly string strTblName = "divTbl";

        BtnMode myMode = BtnMode.NONE; // 기본상태
        public DivMngForm()
        {
            InitializeComponent();
        }

        private void DivMngForm_Load(object sender, EventArgs e)
        {
            UpdateData();
        }

        /// <summary>
        /// DB에서 데이터 불러오기
        /// </summary>
        private void UpdateData()
        {
            using (MySqlConnection conn = new MySqlConnection(Commons.CONNSTR))
            {
                string strQuery = $"SELECT Division, Names FROM {strTblName}";
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(strQuery, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, strTblName);

                GrdDivTbl.DataSource = ds;
                GrdDivTbl.DataMember = strTblName;
            }

            SetColumnHeaders();
        }

        private void SetColumnHeaders()
        {
            /*GrdDivTbl.Columns[0].Width = 100;
            GrdDivTbl.Columns[0].HeaderText = "구분코드";
            GrdDivTbl.Columns[1].Width = 150;
            GrdDivTbl.Columns[1].HeaderText = "이름";*/

            DataGridViewColumn column;

            column = GrdDivTbl.Columns[0];
            column.Width = 100;
            column.HeaderText = "구분코드";

            column = GrdDivTbl.Columns[1];
            column.Width = 150;
            column.HeaderText = "이름";
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            InitControls();

            TxtDivision.ReadOnly = false;

            myMode = BtnMode.INSERT; // 신규입력 모드

            TxtNames.Focus();
        }

        /// <summary>
        /// DB 업데이트 및 입력 처리
        /// </summary>
        private void SaveData()
        {
            if (string.IsNullOrEmpty(TxtDivision.Text) ||
                string.IsNullOrEmpty(TxtNames.Text))
            {
                MetroMessageBox.Show(this, "값을 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(myMode == BtnMode.NONE)
            {
                MetroMessageBox.Show(this, "신규 등록시 신규 버튼을 눌러주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(Commons.CONNSTR)) // using문 사용하면 conn.Close() 안해줘도 된다. -> 사용시 자동으로 일을 해줌
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;

                    if (myMode == BtnMode.UPDATE)
                    {
                        cmd.CommandText = "UPDATE divtbl " +
                                          "   SET Names = @Names " +
                                          " WHERE Division = @Division ";
                    }
                    else if (myMode == BtnMode.INSERT)
                    {
                        cmd.CommandText = "INSERT INTO divtbl (Division, Names) " +
                                          "VALUES (@Division, @Names)";
                    }
                    else if (myMode == BtnMode.DELETE)
                    {
                        cmd.CommandText = "DELETE FROM divtbl" +
                                      " WHERE Division = @Division ";
                    }

                    if (myMode == BtnMode.INSERT || myMode == BtnMode.UPDATE)
                    {
                        MySqlParameter paramNames = new MySqlParameter("@Names", MySqlDbType.VarChar, 45);
                        paramNames.Value = TxtNames.Text.Trim();
                        cmd.Parameters.Add(paramNames);
                    }

                    MySqlParameter paramDivision = new MySqlParameter("@Division", MySqlDbType.VarChar);
                    paramDivision.Value = TxtDivision.Text.Trim();
                    cmd.Parameters.Add(paramDivision);

                    var result = cmd.ExecuteNonQuery();

                    if(myMode == BtnMode.INSERT)
                    {
                        MetroMessageBox.Show(this, $"신규 입력되었습니다.", "신규 입력");
                        myMode = BtnMode.UPDATE;
                    }
                    else if(myMode == BtnMode.UPDATE)
                    {
                        MetroMessageBox.Show(this, $"구분코드 {TxtDivision.Text.Trim()}가 {TxtNames.Text.Trim()}로 수정되었습니다.", "항목 수정");
                    }
                    else if (myMode == BtnMode.DELETE)
                    {
                        MetroMessageBox.Show(this, $"구분코드 {TxtDivision.Text.Trim()}가 삭제되었습니다.", "삭제");
                        myMode = BtnMode.NONE;
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"에러발생 {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateData();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            TxtDivision.ReadOnly = true;
            SaveData();
            InitControls();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if(myMode != BtnMode.UPDATE)
            {
                MetroMessageBox.Show(this, $"삭제할 데이터를 선택하세요.", "알림");
                return;
            }

            myMode = BtnMode.DELETE;
            //DeleteData();
            SaveData();
            InitControls();

            GrdDivTbl.Focus();
        }

        private void InitControls()
        {
            TxtDivision.Text = string.Empty;
            TxtNames.Text = string.Empty;
        }

        #region 삭제메서드 주석처리
        /*
        private void DeleteData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Commons.CONNSTR))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM divtbl" +
                                      " WHERE Division = @Division ";
                    MySqlParameter paramDivision = new MySqlParameter("@Division", MySqlDbType.VarChar);
                    paramDivision.Value = TxtDivision.Text;
                    cmd.Parameters.Add(paramDivision);

                    cmd.ExecuteNonQuery();

                    if (myMode == BtnMode.INSERT)
                    {
                        MetroMessageBox.Show(this, $"구분코드 {paramDivision.Value}가 삭제되었습니다.", "삭제");
                        myMode = BtnMode.NONE;
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"에러발생 {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateData();
            }
        }
        */
        #endregion

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            TxtDivision.Text = string.Empty;
            TxtNames.Text = string.Empty;
            TxtDivision.ReadOnly = false;

            myMode = BtnMode.NONE; // 모드 초기화

            GrdDivTbl.Focus();
        }

        private void GrdDivTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow data = GrdDivTbl.Rows[e.RowIndex];

                TxtDivision.Text = data.Cells[0].Value.ToString();
                TxtNames.Text = data.Cells[1].Value.ToString();
                TxtDivision.ReadOnly = true;

                myMode = BtnMode.UPDATE; // 수정모드로 변경

                TxtNames.Focus();
            }
        }
    }
}
