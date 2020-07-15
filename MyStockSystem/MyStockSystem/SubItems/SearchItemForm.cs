using MetroFramework.Forms;
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
    public partial class SearchItemForm : MetroForm
    {
        public SearchItemForm()
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

        private void SearchItemForm_Load(object sender, EventArgs e)
        {
            DgvSearchItems.Font = new Font(@"NanumGothic", 10, FontStyle.Regular);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            WebClient wc = null;
            XmlDocument doc = null;

            wc = new WebClient() { Encoding = Encoding.UTF8 };
            doc = new XmlDocument();

            StringBuilder str = new StringBuilder();
            str.Append("http://api.seibro.or.kr/openapi/service/StockSvc/getStkIsinByNmN1"); // 기본 URL
            str.Append("?serviceKey=C%2Fd4GdNE5oLhFZ5rM%2FyMrKeTyjEyXCbO1tC7PSppGVUGa3q8pLoID1qvSgcLidUsPi98Oiej8Gy0uy8Mf4dLhg%3D%3D"); // 인증키
            str.Append($"&secnNm={TxtSearchItem.Text}"); // 검색어
            str.Append("&numOfRows=200"); // 읽어올 데이터 수
            str.Append("&pageNo=1"); // 페이지 수
            str.Append("&martTpcd=11"); // 주식시장종류 : 11은 유가증권시장

            string xml = wc.DownloadString(str.ToString());
            doc.LoadXml(xml);

            XmlElement root = doc.DocumentElement;
            XmlNodeList items = doc.GetElementsByTagName("item");

            DgvSearchItems.Rows.Clear();

            try
            {
                foreach (XmlNode item in items)
                            {
                                DgvSearchItems.Rows.Add(item["isin"].InnerText, // isin 표준코드
                                                        item["issuDt"] == null ? string.Empty : item["issuDt"].InnerText, // item["issuDt"].InnerText, //issuDt 발행일
                                                        item["korSecnNm"].InnerText, // korSecnNm 한글 종목명
                                                        item["secnKacdNm"].InnerText, // secnKacdNm 보통주/우선주
                                                        item["shotnIsin"].InnerText); // shotnIsin 단순코드
                            }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show($"에러발생 : {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DgvSearchItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            //TxtSearchItem.Clear();
        }

        private void TxtSearchItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13) // enter
            {
                BtnSearch_Click(sender, new EventArgs());
            }
        }
    }
}
