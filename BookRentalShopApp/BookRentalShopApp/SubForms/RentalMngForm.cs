using MetroFramework;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace BookRentalShopApp.SubForms
{
    public partial class RentalMngForm : MetroForm
    {
        readonly string strTblName = "memberTbl";

        BtnMode myMode = BtnMode.NONE; // 기본상태
        public RentalMngForm()
        {
            InitializeComponent();
            TxtIdx.ReadOnly = true;
        }

        private void RentalMngForm_Load(object sender, EventArgs e)
        {
            UpdateComboMember();
            UpdateComboBook();

            UpdateData();

            InitControls();
        }

        private void UpdateComboMember()
        {
            string strQuery = $"SELECT DISTINCT Idx, Names FROM membertbl";

            using (MySqlConnection conn = new MySqlConnection(Commons.CONNSTR))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(strQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                Dictionary<string, string> temps = new Dictionary<string, string>();
                temps.Add("선택", "");
                while (reader.Read())
                {
                    temps.Add(reader[1].ToString(), reader[0].ToString());
                }

                CboMember.DataSource = new BindingSource(temps, null);
                CboMember.DisplayMember = "Key";
                CboMember.ValueMember = "Value";
                //CboDivision.SelectedIndex = 0;
            }
        }

        private void UpdateComboBook()
        {
            string strQuery = $"SELECT Idx, Names, " +
                               "            (SELECT Names FROM divtbl WHERE Division = b.Division) AS Division " +
                               "  FROM bookstbl as b";

            using (MySqlConnection conn = new MySqlConnection(Commons.CONNSTR))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(strQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                Dictionary<string, string> temps = new Dictionary<string, string>();
                temps.Add("선택", "");
                while (reader.Read())
                {
                    temps.Add($"[{reader[2]}] { reader[1]}", $"{reader[0]}");
                }

                CboBook.DataSource = new BindingSource(temps, null);
                CboBook.DisplayMember = "Key";
                CboBook.ValueMember = "Value";
                //CboDivision.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// DB에서 데이터 불러오기
        /// </summary>
        private void UpdateData()
        {
            using (MySqlConnection conn = new MySqlConnection(Commons.CONNSTR))
            {
                string strQuery = $"SELECT r.idx AS '대여번호', " +
                                   "        m.Names AS '대여회원', " +
		                           "        b.Names AS '대여책제목', " +
                                   "        b.ISBN, " +
                                   "        r.rentalDate AS '대여일', " +
                                   "        r.returnDate AS '반납일', " +
                                   "        r.memberIdx, " +
                                   "        r.bookIdx " +
                                   "  FROM rentaltbl AS r " +
                                   " INNER JOIN membertbl AS m " +
                                   "    ON r.memberIdx = m.Idx " +
                                   " INNER JOIN bookstbl AS b " +
                                   "    ON r.bookIdx = b.Idx";

                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(strQuery, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, strTblName);

                GrdRentalTbl.DataSource = ds;
                GrdRentalTbl.DataMember = strTblName;
            }

            SetColumnHeaders();
        }

        private void SetColumnHeaders()
        {
            DataGridViewColumn column;

            column = GrdRentalTbl.Columns[0];
            column.Width = 60;
            column.HeaderText = "대여번호";

            column = GrdRentalTbl.Columns[1];
            column.Width = 80;
            column.HeaderText = "대여회원";

            column = GrdRentalTbl.Columns[2];
            column.Width = 60;
            column.HeaderText = "대여책제목";

            column = GrdRentalTbl.Columns[3];
            column.Width = 100;
            column.HeaderText = "ISBN";

            column = GrdRentalTbl.Columns[4];
            column.Width = 90;
            //column.HeaderText = "대여일";

            column = GrdRentalTbl.Columns[5];
            column.Width = 90;
            //column.HeaderText = "반납일";

            column = GrdRentalTbl.Columns[3];
            column.Visible = false;

            column = GrdRentalTbl.Columns[6]; // memberIdx
            column.Visible = false;
            
            column = GrdRentalTbl.Columns[7]; // booksIdx
            column.Visible = false;
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            InitControls();

            myMode = BtnMode.INSERT; // 신규입력 모드
        }

        /// <summary>
        /// DB 업데이트 및 입력 처리
        /// </summary>
        private void SaveData()
        {
            // 빈값비교 NULL 체크
            if (CboMember.SelectedIndex < 1 ||
                CboBook.SelectedIndex < 1)
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
                        cmd.CommandText = "UPDATE rentaltbl " +
                                          "   SET " +
                                          "         memberIdx  = memberIdx " +
                                          "       , bookIdx    = @bookIdx " +
                                          "       , rentalDate = @rentalDate " +
                                          "       , returnDate = @returnDate " +
                                          " WHERE   Idx        = @Idx";
                    }
                    else if (myMode == BtnMode.INSERT)
                    {
                        cmd.CommandText = "INSERT INTO rentaltbl " +
                                          "(    memberIdx, " +
                                          "     bookIdx, " +
                                          "     rentalDate, " +
                                          "     returnDate) " +
                                          "VALUES " +
                                          "(    @memberIdx, " +
                                          "     @bookIdx, " +
                                          "     @rentalDate, " +
                                          "     @returnDate)";
                    }
                    // memberIdx
                    MySqlParameter paramMemberIdx = new MySqlParameter("@memberIdx", MySqlDbType.Int32);
                    paramMemberIdx.Value = CboMember.SelectedValue;
                    cmd.Parameters.Add(paramMemberIdx);
                    // bookIdx
                    MySqlParameter paramBookIdx = new MySqlParameter("@bookIdx", MySqlDbType.Int32);
                    paramBookIdx.Value = CboBook.SelectedValue;
                    cmd.Parameters.Add(paramBookIdx);
                    // rentalDate
                    MySqlParameter paramRentalDate = new MySqlParameter("@rentalDate", MySqlDbType.Date);
                    paramRentalDate.Value = DtpRentalDate.Value;
                    cmd.Parameters.Add(paramRentalDate);
                    // returnDate
                    MySqlParameter paramReturnDate = new MySqlParameter("@returnDate", MySqlDbType.Date);
                    if (myMode == BtnMode.INSERT)
                        paramReturnDate.Value = null;
                    else
                        paramReturnDate.Value = DtpReturnDate.Value;
                    cmd.Parameters.Add(paramReturnDate);

                    var result = cmd.ExecuteNonQuery();

                    if(myMode == BtnMode.INSERT)
                    {
                        MetroMessageBox.Show(this, $"신규 입력되었습니다.", "신규 입력");
                        myMode = BtnMode.UPDATE;
                    }
                    else if(myMode == BtnMode.UPDATE)
                    {
                        MetroMessageBox.Show(this, $"일련번호 {TxtIdx.Text.Trim()}가 수정되었습니다.", "수정");
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

            GrdRentalTbl.Focus();
        }

        private void InitControls()
        {
            TxtIdx.Text = string.Empty;
            TxtIdx.ReadOnly = true;

            CboMember.SelectedIndex = CboBook.SelectedIndex = 0;


            DtpRentalDate.CustomFormat = "yyyy-MM-dd";
            DtpRentalDate.Format = DateTimePickerFormat.Custom;
            DtpRentalDate.Value = DateTime.Now;

            DtpReturnDate.Value = DateTime.Now;
            DtpReturnDate.CustomFormat = " ";
            DtpReturnDate.Format = DateTimePickerFormat.Custom;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            InitControls();

            myMode = BtnMode.NONE; // 모드 초기화

            GrdRentalTbl.Focus();
        }

        private void GrdRentalTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow data = GrdRentalTbl.Rows[e.RowIndex];

                // TODO : 클릭시 입력컨트롤에 데이터 할당
                TxtIdx.Text = data.Cells[0].Value.ToString();
                CboMember.SelectedValue = data.Cells[6].Value.ToString();
                CboBook.SelectedValue = data.Cells[7].Value.ToString();
                DtpRentalDate.Value = DateTime.Parse(data.Cells[4].Value.ToString());

                if(!string.IsNullOrEmpty(data.Cells[5].Value.ToString()))
                {
                    DtpReturnDate.CustomFormat = "yyyy-MM-dd";
                    DtpReturnDate.Format = DateTimePickerFormat.Custom;
                    DtpReturnDate.Value = DateTime.Parse(data.Cells[5].Value.ToString());
                }
                else
                {
                    DtpReturnDate.Value = DateTime.Now;
                    DtpReturnDate.CustomFormat = " ";
                    DtpReturnDate.Format = DateTimePickerFormat.Custom;
                }

                myMode = BtnMode.UPDATE; // 수정모드로 변경
                TxtIdx.ReadOnly = true;
            }
        }

        private void DtpReturnDate_ValueChanged(object sender, EventArgs e)
        {
            DtpReturnDate.CustomFormat = "yyyy-MM-dd";
            DtpReturnDate.Format = DateTimePickerFormat.Custom;
        }

        private void BtnExcelExport_Click(object sender, EventArgs e) // NPOI -> Import 불러와서 넣기 Export 내보내기 가능
        {
            IWorkbook workbook = new XSSFWorkbook(); // 2003 이후 버전(.xlsx) //new HSSFWorkbook(); // 이전 버전(.xls)
            ISheet sheet1 = workbook.CreateSheet("Sheet1");
            sheet1.CreateRow(0).CreateCell(0).SetCellValue("Rental Book Data");
            int x = 1;

            DataSet ds = GrdRentalTbl.DataSource as DataSet;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i);
                for (int j = 0; j < ds.Tables[0].Rows[0].ItemArray.Length; j++)
                {
                    if (j == 4 || j == 5)
                    {
                        /*if (string.IsNullOrEmpty(ds.Tables[0].Rows[i].ItemArray[j].ToString()))
                            row.CreateCell(j).SetCellValue(ds.Tables[0].Rows[i].ItemArray[j].ToString());
                        else
                            row.CreateCell(j).SetCellValue(ds.Tables[0].Rows[i].ItemArray[j].ToString().Substring(0, 10));*/

                        var value = string.IsNullOrEmpty(ds.Tables[0].Rows[i].ItemArray[j].ToString()) ?
                            "" : ds.Tables[0].Rows[i].ItemArray[j].ToString().Substring(0, 10);
                        row.CreateCell(j).SetCellValue(value);
                    }
                    else if (j > 5)
                    {
                        break;
                    }
                    else
                    {
                        row.CreateCell(j).SetCellValue(ds.Tables[0].Rows[i].ItemArray[j].ToString());
                    }
                }
            }

            FileStream file = File.Create(Environment.CurrentDirectory + $@"\export.xlsx"); // $@"\export.xls
            workbook.Write(file);
            file.Close();

            MessageBox.Show("Export Done!!");
        }
    }
}
