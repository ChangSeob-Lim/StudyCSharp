using MetroFramework.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MyStockSystem.SubItems
{
    public partial class GalmetgilForm : MetroForm
    {
        public GalmetgilForm()
        {
            InitializeComponent();
        }

        private void MtlBack_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            MainForm main = new MainForm();
            main.Location = this.Location;
            main.ShowDialog();

            this.Close();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            WebClient wc = null;
            wc = new WebClient() { Encoding = Encoding.UTF8 };

            StringBuilder str = new StringBuilder();
            str.Append("http://apis.data.go.kr/6260000/BusanGalmaetGilService/getGalmaetGilInfo"); // 기본 URL
            str.Append("?serviceKey=C%2Fd4GdNE5oLhFZ5rM%2FyMrKeTyjEyXCbO1tC7PSppGVUGa3q8pLoID1qvSgcLidUsPi98Oiej8Gy0uy8Mf4dLhg%3D%3D"); // 인증키
            str.Append("&numOfRows=30"); // 읽어올 데이터 수
            str.Append("&pageNo=1"); // 페이지 수
            str.Append("&resultType=json"); // 반환 타입 = json

            string json = wc.DownloadString(str.ToString());
            JObject obj = JObject.Parse(json);

            //JToken info = obj.SelectToken("getGalmaetGilInfo"); // 모든 정보 불러오기
            //JToken info = obj.SelectToken("getGalmaetGilInfo.item"); // json 안의 item 정보만 불러오기
            JArray items = JArray.Parse(obj.SelectToken("getGalmaetGilInfo.item").ToString()); // item 안의 값을 배열로 가져오기

            GalmetgilSearchItems.Rows.Clear();

            try
            {
                foreach (var item in items)
                {
                    // kosNm, kosType, kosTxt, img, txt1, title, txt2
                    GalmetgilSearchItems.Rows.Add(
                        $"{item.SelectToken("kosNm")}",
                        $"{item.SelectToken("kosType")}",
                        $"{item.SelectToken("kosTxt")}",
                        $"{item.SelectToken("img")}",
                        $"{item.SelectToken("txt1")}",
                        $"{item.SelectToken("title")}",
                        $"{item.SelectToken("txt2")}"
                        );
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show($"에러발생 : {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            GalmetgilSearchItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            //TxtSearchItem.Clear();
        }

        private void TxtSearchItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13) // enter
            {
                BtnSearch_Click(sender, new EventArgs());
            }
        }

        private void GalmetgilForm_Load(object sender, EventArgs e)
        {
            GalmetgilSearchItems.Font = new Font(@"NanumGothic", 9, FontStyle.Regular);
        }

        private void GalmetgilSearchItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                var cellValue = GalmetgilSearchItems.Rows[e.RowIndex].Cells[3].Value.ToString();
                //MessageBox.Show(cellValue);
                DownloadImgForm form = new DownloadImgForm();
                form.ParentUrl = cellValue;
                form.ShowDialog();
            }
        }
    }
}
