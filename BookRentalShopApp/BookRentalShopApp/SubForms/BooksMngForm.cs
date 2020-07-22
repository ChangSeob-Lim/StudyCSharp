using MetroFramework;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace BookRentalShopApp.SubForms
{
    public partial class BooksMngForm : MetroForm
    {
        readonly string strTblName = "booksTbl";

        BtnMode myMode = BtnMode.NONE; // 기본상태
        public BooksMngForm()
        {
            InitializeComponent();
            TxtIdx.ReadOnly = true;
        }

        private void DivMngForm_Load(object sender, EventArgs e)
        {
            UpdateData();

            using (MySqlConnection conn = new MySqlConnection(Commons.CONNSTR))
            {
                string strQuery = $"SELECT Division, Names FROM divTbl";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(strQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                Dictionary<string, string> temps = new Dictionary<string, string>();
                temps.Add("선택", "");
                while (reader.Read())
                {
                    temps.Add(reader[1].ToString(), reader[0].ToString());
                }

                CboDivision.DataSource = new BindingSource(temps, null);
                CboDivision.DisplayMember = "Key";
                CboDivision.ValueMember = "Value";
                //CboDivision.SelectedIndex = -1;
            }

            InitControls();
        }

        /// <summary>
        /// DB에서 데이터 불러오기
        /// </summary>
        private void UpdateData()
        {
            using (MySqlConnection conn = new MySqlConnection(Commons.CONNSTR))
            {
                string strQuery = $"SELECT   b.Idx, " +
                                   "         b.Author, " +
                                   "         b.Division," +
                                   "         d.Names As DivisionName, " +
                                   "         b.Names, " +
                                   "         b.ReleaseDate, " +
                                   "         b.ISBN, " +
                                   "         b.Price " +
                                   "     FROM bookstbl AS b " +
                                   "     INNER JOIN divtbl AS d " +
                                   "     ON b.Division = d.Division " + 
                                   " ORDER BY b.Idx ASC"; // 정렬추가 200716 12:11

                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(strQuery, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, strTblName);

                GrdBooksTbl.DataSource = ds;
                GrdBooksTbl.DataMember = strTblName;
            }

            SetColumnHeaders();
        }

        private void SetColumnHeaders()
        {
            DataGridViewColumn column;

            column = GrdBooksTbl.Columns[0];
            column.Width = 60;
            column.HeaderText = "번호";

            column = GrdBooksTbl.Columns[1];
            column.Width = 120;
            column.HeaderText = "저자명";

            column = GrdBooksTbl.Columns[2];
            column.Visible = false;

            column = GrdBooksTbl.Columns[3];
            column.Width = 100;
            column.HeaderText = "장르";

            column = GrdBooksTbl.Columns[4];
            column.Width = 200;
            column.HeaderText = "이름";

            column = GrdBooksTbl.Columns[5];
            column.Width = 100;
            column.HeaderText = "출간일";

            column = GrdBooksTbl.Columns[6];
            column.Width = 100;
            column.HeaderText = "ISBN";

            column = GrdBooksTbl.Columns[7];
            column.Width = 100;
            column.HeaderText = "가격";
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            InitControls();

            myMode = BtnMode.INSERT; // 신규입력 모드

            TxtAuthor.Focus();
        }

        /// <summary>
        /// DB 업데이트 및 입력 처리
        /// </summary>
        private void SaveData()
        {
            // 빈값비교 NULL 체크
            if (string.IsNullOrEmpty(TxtAuthor.Text) ||
                CboDivision.SelectedIndex < 1 ||
                string.IsNullOrEmpty(TxtNames.Text) ||
                string.IsNullOrEmpty(TxtISBN.Text))
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
                        cmd.CommandText = "UPDATE bookstbl " +
                                          "   SET Author      = @Author, " +
                                          "       Division    = @Division, " +
                                          "       Names       = @Names, " +
                                          "       ReleaseDate = @ReleaseDate, " +
                                          "       ISBN        = @ISBN, " +
                                          "       Price       = @Price " +
                                          " WHERE Idx = @Idx";
                    }
                    else if (myMode == BtnMode.INSERT)
                    {
                        cmd.CommandText = "INSERT INTO bookstbl " +
                                          "(    Author, " +
                                          "     Division, " +
                                          "     Names, " +
                                          "     ReleaseDate, " +
                                          "     ISBN, " +
                                          "     Price) " +
                                          "VALUES " +
                                          "     (@Author, " +
                                          "     @Division, " +
                                          "     @Names, " +
                                          "     @ReleaseDate, " +
                                          "     @ISBN, " +
                                          "     @Price)";
                    }

                    // 저자명
                    MySqlParameter paramAuthor = new MySqlParameter("@Author", MySqlDbType.VarChar, 45)
                    {
                        Value = TxtAuthor.Text.Trim()
                    };
                    cmd.Parameters.Add(paramAuthor);
                    // 장르
                    MySqlParameter paramDivision = new MySqlParameter("@Division", MySqlDbType.VarChar, 4)
                    {
                        Value = CboDivision.SelectedValue // B001, B002;
                    };
                    cmd.Parameters.Add(paramDivision);
                    // 책이름
                    MySqlParameter paramNames = new MySqlParameter("@Names", MySqlDbType.VarChar, 100)
                    {
                        Value = TxtNames.Text.Trim()
                    };
                    cmd.Parameters.Add(paramNames);
                    // 출간일
                    MySqlParameter paramReleaseDate = new MySqlParameter("@ReleaseDate", MySqlDbType.Date)
                    {
                        Value = DtpReleaseDate.Value
                    };
                    cmd.Parameters.Add(paramReleaseDate);
                    // ISBN
                    MySqlParameter paramISBN = new MySqlParameter("@ISBN", MySqlDbType.VarChar, 13)
                    {
                        Value = TxtISBN.Text.Trim()
                    };
                    cmd.Parameters.Add(paramISBN);
                    // 가격
                    MySqlParameter paramPrice = new MySqlParameter("@Price", MySqlDbType.Decimal)
                    {
                        Value = TxtPrice.Text.Trim()
                    };
                    cmd.Parameters.Add(paramPrice);

                    if (myMode == BtnMode.UPDATE)
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
                        MetroMessageBox.Show(this, $"일련번호 {TxtIdx.Text.Trim()}인 {TxtNames.Text.Trim()}이 수정되었습니다.", "항목 수정");
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

            myMode = BtnMode.DELETE;
            //DeleteData();
            SaveData();
            InitControls();

            GrdBooksTbl.Focus();
        }

        private void InitControls()
        {
            TxtIdx.Text = string.Empty;
            TxtAuthor.Text = string.Empty;
            TxtISBN.Text = TxtNames.Text = TxtPrice.Text = string.Empty;
            CboDivision.SelectedIndex = 0;

            DtpReleaseDate.CustomFormat = "yyyy-MM-dd";
            DtpReleaseDate.Format = DateTimePickerFormat.Custom;
            DtpReleaseDate.Value = DateTime.Now;

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

            GrdBooksTbl.Focus();
        }

        private void GrdDivTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow data = GrdBooksTbl.Rows[e.RowIndex];

                // TODO : 클릭시 입력컨트롤에 데이터 할당
                TxtIdx.Text = data.Cells[0].Value.ToString();
                TxtAuthor.Text = data.Cells[1].Value.ToString();

                // 로맨스, 추리 등 디스플레이 되는 글자로 인덱스 찾기
                //CboDivision.SelectedIndex = CboDivision.FindString(data.Cells[3].Value.ToString());

                // 코드값 그대로 할당 B001, B002 등
                CboDivision.SelectedValue = data.Cells[2].Value;

                TxtNames.Text = data.Cells[4].Value.ToString();

                DtpReleaseDate.CustomFormat = "yyyy-MM-dd";
                DtpReleaseDate.Format = DateTimePickerFormat.Custom;
                DtpReleaseDate.Value = DateTime.Parse(data.Cells[5].Value.ToString());

                TxtISBN.Text = data.Cells[6].Value.ToString();
                TxtPrice.Text = data.Cells[7].Value.ToString();

                myMode = BtnMode.UPDATE; // 수정모드로 변경
                TxtIdx.ReadOnly = true;
            }
        }
    }
}
