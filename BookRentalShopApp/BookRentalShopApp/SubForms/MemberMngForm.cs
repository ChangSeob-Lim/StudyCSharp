using MetroFramework;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace BookRentalShopApp.SubForms
{
    public partial class MemberMngForm : MetroForm
    {
        readonly string strTblName = "memberTbl";

        BtnMode myMode = BtnMode.NONE; // 기본상태
        public MemberMngForm()
        {
            InitializeComponent();
            TxtIdx.ReadOnly = true;
        }

        private void DivMngForm_Load(object sender, EventArgs e)
        {
            UpdateData();

            #region 레벨 범위는 A~D이므로 불러오지 않아도 된다.
            /*using (MySqlConnection conn = new MySqlConnection(Commons.CONNSTR))
            {
                string strQuery = $"SELECT DISTINCT Levels FROM memberTbl";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(strQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                Dictionary<string, string> temps = new Dictionary<string, string>();
                temps.Add("선택", "");
                while (reader.Read())
                {
                    temps.Add(reader[0].ToString(), reader[0].ToString());
                }

                CboLevel.DataSource = new BindingSource(temps, null);
                CboLevel.DisplayMember = "Key";
                CboLevel.ValueMember = "Value";
                //CboDivision.SelectedIndex = -1;
            }*/
            #endregion

            /*
            Dictionary<string, string> temps = new Dictionary<string, string>();
            temps.Add("선택", "");
            temps.Add("A", "A");
            temps.Add("B", "B");
            temps.Add("C", "C");
            temps.Add("D", "D");

            CboLevel.DataSource = new BindingSource(temps, null);
            CboLevel.DisplayMember = "Key";
            CboLevel.ValueMember = "Value";
            */

            CboLevel.Items.Add("선택");
            CboLevel.Items.Add("A");
            CboLevel.Items.Add("B");
            CboLevel.Items.Add("C");
            CboLevel.Items.Add("D");

            InitControls();
        }

        /// <summary>
        /// DB에서 데이터 불러오기
        /// </summary>
        private void UpdateData()
        {
            using (MySqlConnection conn = new MySqlConnection(Commons.CONNSTR))
            {
                string strQuery = $"SELECT   Idx, " +
                                   "         Names, " +
                                   "         Levels," +
                                   "         Addr, " +
                                   "         Mobile, " +
                                   "         Email " +
                                   "  FROM membertbl " +
                                   " ORDER BY Idx ASC";

                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(strQuery, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, strTblName);

                GrdMemberTbl.DataSource = ds;
                GrdMemberTbl.DataMember = strTblName;
            }

            SetColumnHeaders();
        }

        private void SetColumnHeaders()
        {
            DataGridViewColumn column;

            column = GrdMemberTbl.Columns[0];
            column.Width = 60;
            column.HeaderText = "번호";

            column = GrdMemberTbl.Columns[1];
            column.Width = 80;
            column.HeaderText = "이름";

            column = GrdMemberTbl.Columns[2];
            column.Width = 60;
            column.HeaderText = "레벨";

            column = GrdMemberTbl.Columns[3];
            column.Width = 120;
            column.HeaderText = "주소";

            column = GrdMemberTbl.Columns[4];
            column.Width = 120;
            column.HeaderText = "전화번호";

            column = GrdMemberTbl.Columns[5];
            column.Width = 120;
            column.HeaderText = "이메일";
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            InitControls();

            myMode = BtnMode.INSERT; // 신규입력 모드

            TxtNames.Focus();
        }

        /// <summary>
        /// DB 업데이트 및 입력 처리
        /// </summary>
        private void SaveData()
        {
            // 빈값비교 NULL 체크
            if (string.IsNullOrEmpty(TxtNames.Text) ||
                CboLevel.SelectedIndex < 1 ||
                string.IsNullOrEmpty(TxtAddr.Text) ||
                string.IsNullOrEmpty(TxtEmail.Text) ||
                string.IsNullOrEmpty(TxtMobile.Text))
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
                        cmd.CommandText = "UPDATE membertbl " +
                                          "   SET Names       = @Names, " +
                                          "       Levels      = @Levels, " +
                                          "       Addr        = @Addr, " +
                                          "       Mobile      = @Mobile, " +
                                          "       Email       = @Email " +
                                          " WHERE Idx = @Idx";
                    }
                    else if (myMode == BtnMode.INSERT)
                    {
                        cmd.CommandText = "INSERT INTO membertbl " +
                                          "(    Names, " +
                                          "     Levels, " +
                                          "     Addr, " +
                                          "     Mobile, " +
                                          "     Email) " +
                                          "VALUES " +
                                          "     (@Names, " +
                                          "     @Levels, " +
                                          "     @Addr, " +
                                          "     @Mobile, " +
                                          "     @Email)";
                    }
                    else if (myMode == BtnMode.DELETE)
                    {
                        cmd.CommandText = "DELETE FROM membertbl " +
                                          " WHERE Idx = @idx";
                    }

                    if (myMode == BtnMode.INSERT || myMode == BtnMode.UPDATE)
                    {
                        // 이름
                        MySqlParameter paramNames = new MySqlParameter("@Names", MySqlDbType.VarChar, 45)
                        {
                            Value = TxtNames.Text.Trim()
                        };
                        cmd.Parameters.Add(paramNames);
                        // 레벨
                        MySqlParameter paramLevels = new MySqlParameter("@Levels", MySqlDbType.VarChar, 1)
                        {
                            Value = CboLevel.SelectedItem//Value = CboLevel.SelectedValue
                        };
                        cmd.Parameters.Add(paramLevels);
                        // 주소
                        MySqlParameter paramAddr = new MySqlParameter("@Addr", MySqlDbType.VarChar, 100)
                        {
                            Value = TxtAddr.Text.Trim()
                        };
                        cmd.Parameters.Add(paramAddr);
                        // 전화번호
                        MySqlParameter paramMobile = new MySqlParameter("@Mobile", MySqlDbType.VarChar, 13)
                        {
                            Value = TxtMobile.Text.Trim()
                        };
                        cmd.Parameters.Add(paramMobile);
                        // 이메일
                        MySqlParameter paramEmail = new MySqlParameter("@Email", MySqlDbType.VarChar, 50)
                        {
                            Value = TxtEmail.Text.Trim()
                        };
                        cmd.Parameters.Add(paramEmail);
                    }

                    if (myMode == BtnMode.UPDATE || myMode == BtnMode.DELETE)
                    {
                        // Idx : PK, AI
                        MySqlParameter paramIdx = new MySqlParameter("@Idx", MySqlDbType.Int32)
                        {
                            Value = TxtIdx.Text.Trim()
                        };
                        cmd.Parameters.Add(paramIdx);
                    }

                    var result = cmd.ExecuteNonQuery();

                    if(myMode == BtnMode.INSERT)
                    {
                        MetroMessageBox.Show(this, $"신규 입력되었습니다.", "신규 입력");
                        myMode = BtnMode.UPDATE;
                    }
                    else if(myMode == BtnMode.UPDATE)
                    {
                        MetroMessageBox.Show(this, $"일련번호 {TxtIdx.Text.Trim()}인 {TxtNames.Text.Trim()}이 수정되었습니다.", "회원 정보 수정");
                    }
                    else if(myMode == BtnMode.DELETE)
                    {
                        MetroMessageBox.Show(this, $"일련번호 {TxtIdx.Text.Trim()}인 {TxtNames.Text.Trim()}이 삭제되었습니다.", "회원 삭제");
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
            TxtIdx.ReadOnly = true;
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

            DialogResult choice = MetroMessageBox.Show(this, "삭제하시겠습니까?", "경고", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if(choice == DialogResult.No)
            {
                return;
            }

            myMode = BtnMode.DELETE;
            //DeleteData();
            SaveData();
            InitControls();

            GrdMemberTbl.Focus();
        }

        private void InitControls()
        {
            TxtIdx.Text = string.Empty;
            TxtNames.Text = string.Empty;
            TxtEmail.Text = TxtAddr.Text = TxtMobile.Text = string.Empty;
            CboLevel.SelectedIndex = 0;

            #region 콤보박스 연습
            // 콤보박스 데이터바인딩
            /*Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("선택", "00");
            dic.Add("서울특별시", "11");
            dic.Add("부산광역시", "21");
            dic.Add("대구광역시", "22");
            dic.Add("인천광역시", "23");
            dic.Add("광주광역시", "24");
            dic.Add("대전광역시", "25");

            CboDivision.DataSource = new BindingSource(dic, null);
            CboDivision.DisplayMember = "Key";
            CboDivision.ValueMember = "Value";*/
            #endregion
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            InitControls();

            myMode = BtnMode.NONE; // 모드 초기화

            GrdMemberTbl.Focus();
        }

        private void GrdDivTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow data = GrdMemberTbl.Rows[e.RowIndex];

                // TODO : 클릭시 입력컨트롤에 데이터 할당
                TxtIdx.Text = data.Cells[0].Value.ToString();
                TxtNames.Text = data.Cells[1].Value.ToString();
                CboLevel.SelectedItem = data.Cells[2].Value; // CboLevel.SelectedValue = data.Cells[2].Value;
                TxtAddr.Text = data.Cells[3].Value.ToString();
                TxtMobile.Text = data.Cells[4].Value.ToString();
                TxtEmail.Text = data.Cells[5].Value.ToString();

                myMode = BtnMode.UPDATE; // 수정모드로 변경
                TxtIdx.ReadOnly = true;
            }
        }
    }
}
