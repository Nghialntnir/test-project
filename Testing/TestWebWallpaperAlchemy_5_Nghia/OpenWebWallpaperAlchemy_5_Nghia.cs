using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using System.Windows.Forms;

namespace TestWebWallpaperAlchemy_5_Nghia
{
    public partial class OpenWebWallpaperAlchemy_5_Nghia : Form
    {
        private IWebDriver driver_5_Nghia;

        public enum HanhDong_5_Nghia
        {
            Confirm_5_Nghia,
            Close_5_Nghia
        }

        public OpenWebWallpaperAlchemy_5_Nghia()
        {
            InitializeComponent();

            string downloadPath_5_Nghia = @"D:\Computer Science\Testing\TestWebWallpaperAlchemy_5_Nghia\Img_5_Nghia";

            // Ẩn Console màng hình đen
            ChromeDriverService chromeDriverService_5_Nghia = ChromeDriverService.CreateDefaultService();
            chromeDriverService_5_Nghia.HideCommandPromptWindow = true;

            // Tạo thư mục nếu chưa có
            if (!System.IO.Directory.Exists(downloadPath_5_Nghia))
            {
                System.IO.Directory.CreateDirectory(downloadPath_5_Nghia);
            }
            ChromeOptions options_5_Nghia = new ChromeOptions();
            options_5_Nghia.AddUserProfilePreference("download.default_directory", downloadPath_5_Nghia);
            options_5_Nghia.AddUserProfilePreference("download.prompt_for_download", false); // Tự động tải, không hỏi lưu ở đâu
            options_5_Nghia.AddUserProfilePreference("safebrowsing.enabled", true);          // Tránh bị Chrome chặn file

            driver_5_Nghia = new ChromeDriver(chromeDriverService_5_Nghia, options_5_Nghia);

            //driver_5_Nghia.Navigate().GoToUrl("https://www.wallpaperalchemy.com/sign-in");
            //Thread.Sleep(2500);

            // Goi ham test
            //testLogin_5_Nghia("nirvanerx@protonmail.com", "kakashi@123");
            //Thread.Sleep(3000);
        }

        private void btn_openWeb_5_Nghia_Click(object sender, EventArgs e)
        {
            driver_5_Nghia.Navigate().GoToUrl("https://www.wallpaperalchemy.com/");
        }


        void testLogin_5_Nghia(string username_5_Nghia, string password_5_Nghia)
        {
            IWebElement elementUsername_5_Nghia = driver_5_Nghia.FindElement(By.Name("email"));
            elementUsername_5_Nghia.Clear(); // Làm sạch trước
            elementUsername_5_Nghia.SendKeys(username_5_Nghia);

            IWebElement elementPassword_5_Nghia = driver_5_Nghia.FindElement(By.Name("password"));
            elementPassword_5_Nghia.Clear(); // Làm sạch textbox trước
            elementPassword_5_Nghia.SendKeys(password_5_Nghia);

            IWebElement btnSigin_5_Nghia = driver_5_Nghia.FindElement(By.XPath("//*[@id=\"main\"]/div/div[2]/form/div[3]/button"));
            Thread.Sleep(1500);
            btnSigin_5_Nghia.Click();
        }

        void testUpload_5_Nghia(string link_5_Nghia)
        {
            try
            {
                IWebElement upload_5_Nghia = driver_5_Nghia.FindElement(By.CssSelector("input[type='file']"));
                Thread.Sleep(1000);
                upload_5_Nghia.SendKeys(link_5_Nghia);

                IWebElement btnUpload_5_Nghia = driver_5_Nghia.FindElement(By.XPath("//*[@id=\"main\"]/div/div[2]/form/button"));
                Thread.Sleep(1000);
                btnUpload_5_Nghia.Click();
            }
            catch (Exception ex_5_Nghia)
            {
                MessageBox.Show("Loi nap file: " + ex_5_Nghia.Message);
            }
        }

        void testDeleteImg_5_Nghia(int vitri_5_Nghia, HanhDong_5_Nghia status_5_Nghia)
        {
            try
            {
                string xpath_5_Nghia = string.Format("//*[@id='pending-wallpaper-cards-scroll']/div[1]/div/div[{0}]/div/div[2]/button", vitri_5_Nghia);
                IWebElement btnDelete_5_Nghia = driver_5_Nghia.FindElement(By.XPath(xpath_5_Nghia));
                btnDelete_5_Nghia.Click();
                Thread.Sleep(1500);

                switch (status_5_Nghia)
                {
                    case HanhDong_5_Nghia.Confirm_5_Nghia:
                        IWebElement btnConfirm_5_Nghia = driver_5_Nghia.FindElement(
                            By.CssSelector("button.Button_button--error__1lcxM")
                        );
                        btnConfirm_5_Nghia.Click();
                        break;

                    case HanhDong_5_Nghia.Close_5_Nghia:
                        IWebElement btnClose_5_Nghia = driver_5_Nghia.FindElement(
                            By.CssSelector("button.Button_button--gray__iK660")
                        );
                        btnClose_5_Nghia.Click();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi tai vi tri " + vitri_5_Nghia + ": " + ex.Message);
            }
        }

        void testDownloadImg_5_Nghia()
        {
            try
            {
                IWebElement btnDownload_5_Nghia = driver_5_Nghia.FindElement(
                    By.CssSelector("a[download]")
                );

                string imageUrl_5_Nghia = btnDownload_5_Nghia.GetAttribute("href");
                string fileName_5_Nghia = btnDownload_5_Nghia.GetAttribute("download") + ".png";
                string savePath_5_Nghia = System.IO.Path.Combine(
                    @"D:\Computer Science\Testing\TestWebWallpaperAlchemy_5_Nghia\Img_5_Nghia",
                    fileName_5_Nghia
                );

                using (var client_5_Nghia = new System.Net.WebClient())
                {
                    client_5_Nghia.DownloadFile(imageUrl_5_Nghia, savePath_5_Nghia);
                }

                MessageBox.Show("Download thành công!\nFile: " + fileName_5_Nghia);
            }
            catch (Exception ex_5_Nghia)
            {
                MessageBox.Show("Lỗi download: " + ex_5_Nghia.Message);
            }
        }

        void testSearch_5_Nghia(string keyword_5_Nghia)
        {
            try
            {
                IWebElement inputSearch_5_Nghia = driver_5_Nghia.FindElement(By.Id("search"));
                inputSearch_5_Nghia.Clear();
                inputSearch_5_Nghia.SendKeys(keyword_5_Nghia);
                Thread.Sleep(500);

                IWebElement btnSearch_5_Nghia = driver_5_Nghia.FindElement(By.CssSelector("button[type='submit'][aria-label='Search']"));
                btnSearch_5_Nghia.Click();

                Thread.Sleep(2500);
            }
            catch (Exception ex_5_Nghia)
            {
                MessageBox.Show("Lỗi search: " + ex_5_Nghia.Message);
            }
        }

        private void btn_test_delete_img_5_Nghia_Click(object sender, EventArgs e)
        {
            driver_5_Nghia.Navigate().GoToUrl("https://www.wallpaperalchemy.com/users/Nghia_Le/pending-wallpapers");
            Thread.Sleep(2500);

            // TC1_TestDeleteImg_5_Nghia_F
            // Vị trí không tồn tại
            //testDeleteImg_5_Nghia(100, HanhDong_5_Nghia.Confirm_5_Nghia);
            //Thread.Sleep(2000);

            // TC2_TestDeleteImg_5_Nghia_F
            // Vị trí hợp lệ nhưng bấm Close (không xóa)
            driver_5_Nghia.Navigate().GoToUrl("https://www.wallpaperalchemy.com/users/Nghia_Le/pending-wallpapers");
            Thread.Sleep(2500);
            testDeleteImg_5_Nghia(1, HanhDong_5_Nghia.Close_5_Nghia);
            Thread.Sleep(2000);

            // TC3_TestDeleteImg_5_Nghia_P
            // Vị trí hợp lệ, bấm Confirm (xóa thành công)
            driver_5_Nghia.Navigate().GoToUrl("https://www.wallpaperalchemy.com/users/Nghia_Le/pending-wallpapers");
            Thread.Sleep(2500);
            testDeleteImg_5_Nghia(1, HanhDong_5_Nghia.Confirm_5_Nghia);
            Thread.Sleep(2000);

            MessageBox.Show("Đã chạy xong 3 kịch bản Delete!");
        }

        private void btn_test_login_5_Nghia_Click(object sender, EventArgs e)
        {
            // Danh sach 4 kich ban
            var danhSachTest_5_Nghia = new[] {
                // TC1_TestLogin_5_Nghia_F
                // Sai password
                new { U = "nirvanerx@protonmail.com", P = "matkhausai" },

                // TC2_TestLogin_5_Nghia_F
                // Sai email
                new { U = "tendangnhapsai@gmail.com", P = "kakashi@123" },

                // TC3_TestLogin_5_Nghia_F
                // Sai email, password
                new { U = "tendangnhapsai@gmail.com", P = "matkhausai" },

                // TC4_TestLogin_5_Nghia_P
                // Dung email, password
                new { U = "nirvanerx@protonmail.com", P = "kakashi@123" }
            };

            foreach (var item_5_Nghia in danhSachTest_5_Nghia)
            {
                // Quay lai trang login truoc moi lan test
                driver_5_Nghia.Navigate().GoToUrl("https://www.wallpaperalchemy.com/sign-in");
                Thread.Sleep(2500);

                // Goi ham test
                testLogin_5_Nghia(item_5_Nghia.U, item_5_Nghia.P);

                // Doi xem ket qua truoc khi sang case tiep theo
                Thread.Sleep(3000);
            }

            MessageBox.Show("Da chay xong 4 kich ban");
        }

        private void btn_test_upload_5_Nghia_Click(object sender, EventArgs e)
        {
            var danhSachTest_5_Nghia = new[]
            {
                // TC1_TestUpload_5_Nghia_F
                // Sai duong dan
                new { L = @"D:\Computer Science\Testing\TestWebWallpaperAlchemy_5_Nghia\Img_5_Nghia" },

                // TC2_TestUpload_5_Nghia_F
                // Lon hon 25MB va khong phai image
                new { L = @" D:\Computer Science\Testing\TestWebWallpaperAlchemy_5_Nghia\Img_5_Nghia\more_than_25MB_and_not_img.mp4" },

                // TC3_TestUploadImg_5_Nghia_F
                // Be hon 25MB, khong phai anh
                new { L = @" D:\Computer Science\Testing\TestWebWallpaperAlchemy_5_Nghia\Img_5_Nghia\not_img.pdf" },

                // TC4_TestUploadImg_5_Nghia_F
                // La anh, nhung lon hon 25MB
                new { L = @"D:\Computer Science\Testing\TestWebWallpaperAlchemy_5_Nghia\Img_5_Nghia\more_than_25MB.jpg" },

                // TC5_TestUpload_5_Nghia_P
                // Be hon 25MB, la anh, Upload thanh cong
                new { L = @"D:\Computer Science\Testing\TestWebWallpaperAlchemy_5_Nghia\Img_5_Nghia\success.webp" },
            };

            foreach (var item_5_Nghia in danhSachTest_5_Nghia)
            {
                driver_5_Nghia.Navigate().GoToUrl("https://www.wallpaperalchemy.com/upload-wallpaper");
                Thread.Sleep(2500);
                testUpload_5_Nghia(item_5_Nghia.L);
                Thread.Sleep(2500);
            }
        }


        private void btn_test_search_5_Nghia_Click(object sender, EventArgs e)
        {
            var danhSachTest_5_Nghia = new[]
            {
            // TC1_TestSearchImg_5_Nghia_F
            // Không nhập gì
            new { K = "" },

            // TC2_TestSearchImg_5_Nghia_F
            // Nhập từ khóa không tồn tại
            new { K = "asdlfjlcxvjioerjlkgdf" },

            // TC3_TestSearchImg_5_Nghia_P
            // Nhập từ khóa hợp lệ
            new { K = "Anime" },
            };

            foreach (var item_5_Nghia in danhSachTest_5_Nghia)
            {
                driver_5_Nghia.Navigate().GoToUrl("https://www.wallpaperalchemy.com/");
                Thread.Sleep(2500);

                testSearch_5_Nghia(item_5_Nghia.K);

                Thread.Sleep(3000);
            }

            MessageBox.Show("Đã chạy xong 3 kịch bản Search!");
        }

        private void btn_test_download_5_Nghia_Click(object sender, EventArgs e)
        {
            // TC2_ TestDownloadImg _5_Nghia_P
            // Đúng link
            driver_5_Nghia.Navigate().GoToUrl("https://www.wallpaperalchemy.com/wallpaper/4k-black-hole-minimalistic-wallpaper-91");
            Thread.Sleep(3000);
            testDownloadImg_5_Nghia();

            MessageBox.Show("Đã thực hiện lệnh Download!");

            // TC1_ TestDownloadImg _5_Nghia_F
            Thread.Sleep(5000);
            driver_5_Nghia.Navigate().GoToUrl("https://www.wallpaperalchemy.com/wallpaper/4k-black-hole-min123imalis123tic-wallpaper-912311");
            Thread.Sleep(3000);
            testDownloadImg_5_Nghia();

            MessageBox.Show("Đã thực hiện lệnh Download!");
        }

        // Đóng trình duyệt khi tắt Form để tránh chạy ngầm
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (driver_5_Nghia != null)
            {
                driver_5_Nghia.Quit();
                driver_5_Nghia.Dispose();
            }
        }
    }
}