using BookRentalShopApp.SubForms;
using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Windows.Forms;

namespace BookRentalShopApp
{
    public partial class MainForm : MetroForm
    {
        #region 멤버변수 영역



        #endregion

        #region 생성자 영역

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 이벤트 핸들러 영역

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.ShowDialog();

            LbUserID.Text = Commons.USERID;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MetroMessageBox.Show(this, "종료하시겠습니까?", "종료", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes) // 프로그램 종료
            {
                e.Cancel = false;
                Environment.Exit(0); // 완전 종료
            }
            else // 프로그램 종료 안함
            {
                e.Cancel = true;
            }
        }

        private void MnuItemCodeMng_Click(object sender, EventArgs e)
        {
            DivMngForm div = new DivMngForm();
            string title = "구분코드 관리";
            ShowFormControl(div, title);

            /*div.MdiParent = this;
            div.Text = "구분코드 관리";
            //div.Dock = DockStyle.Fill;
            div.Show();
            div.WindowState = FormWindowState.Maximized;*/
        }

        private void MnuItemBooksMng_Click(object sender, EventArgs e)
        {
            BooksMngForm books = new BooksMngForm();
            string title = "도서 관리";
            ShowFormControl(books, title);

            /*books.MdiParent = this;
            books.Text = "도서 관리";
            books.Show();
            books.WindowState = FormWindowState.Maximized;*/
        }

        private void MnuItemMemberMng_Click(object sender, EventArgs e)
        {
            MemberMngForm member = new MemberMngForm();
            string title = "회원 관리";
            ShowFormControl(member, title);
        }

        private void MnuItemRentalMng_Click(object sender, EventArgs e)
        {
            RentalMngForm rental = new RentalMngForm();
            ShowFormControl(rental, "대여 관리");
        }

        private void MnuItemUserMng_Click(object sender, EventArgs e)
        {

        }

        private void MnuItemExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        #endregion

        #region 사용자 함수 영역

        private void ShowFormControl(Form form, string title)
        {
            form.MdiParent = this;
            form.Text = title;
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }

        #endregion
    }
}
