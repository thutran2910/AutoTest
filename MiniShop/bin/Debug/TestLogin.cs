using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace MiniShop
{
    [TestClass]
    public class TestLogin
    {
        //Khai báo biến driver để điều hướng truy cập vào trang web
        private IWebDriver driver = new ChromeDriver();
        public void VaoTrangWeb()
        {
            driver.Navigate().GoToUrl("http://localhost:3000/login");
        }

        public void DangNhap(string username, string password)
        {
            VaoTrangWeb();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var emailField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("formBasicEmail")));
            emailField.SendKeys(username);

            var passField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("formBasicPassword")));
            passField.SendKeys(password);

            var loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//button[contains(text(),'ĐĂNG NHẬP')]")));

            // Click bằng JavaScript cho chắc
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", loginButton);
        }


        //Khai báo TestContext để lấy dữ liệu từ file data
        public TestContext TestContext { get; set; }

        //Khai báo DataSource, dẫn đường dẫn đến file dữ liệu
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"..\..\Data\DataTestSuccess.csv",
                    "DataTestSuccess#csv", DataAccessMethod.Sequential)]

        //TestCase1: Đăng nhập thành công
        [TestMethod]
        public void TestLoginSuccess()
        {
            //Khai báo 2 biến email và password để nhận giá trị từ file dữ liệu
            string username = TestContext.DataRow[0].ToString();
            string password = TestContext.DataRow[1].ToString();

            //Gọi hàm Đăng nhập trên để bỏ giá trị từ file dữ liệu vào trang web
            DangNhap(username, password);

            // Đợi nút dropdown hiển thị sau khi đăng nhập
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var welcomeButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(),'Xin chào, admin')]")));

            // Lấy nội dung văn bản từ nút
            string usernameText = welcomeButton.Text;

            // Kiểm tra username có chứa đúng tên người dùng không
            Assert.IsTrue(usernameText.Contains("admin"));
        }

        //Khai báo DataSource, dẫn đường dẫn đến file dữ liệu
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"..\..\Data\DataTestSaiUsername.csv",
                    "DataTestSaiUsername#csv", DataAccessMethod.Sequential)]

        //TestCase2: Đăng nhập thất bại vì sai username hoặc password
        [TestMethod]
        public void TestLoginSaiUsername()
        {
            //Khai báo 2 biến email và password để nhận giá trị từ file dữ liệu
            string username = TestContext.DataRow[0].ToString();
            string password = TestContext.DataRow[1].ToString();

            //Gọi hàm Đăng nhập trên để bỏ giá trị từ file dữ liệu vào trang web
            DangNhap(username, password);

            // Đợi thông báo lỗi hiển thị
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var alert = wait.Until(ExpectedConditions.ElementIsVisible(
                By.CssSelector("div.alert.alert-danger")));

            // Lấy nội dung thông báo
            string alertText = alert.Text;

            // Kiểm tra nội dung có đúng thông báo lỗi không
            Assert.IsTrue(alertText.Contains("Vui lòng nhập lại tên đăng nhập hoặc mật khẩu"));
        }

        //Khai báo DataSource, dẫn đường dẫn đến file dữ liệu
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"..\..\Data\DataTestThieuUsername.csv",
                    "DataTestThieuUsername#csv", DataAccessMethod.Sequential)]

        //TestCase3: Đăng nhập thất bại vì thiếu username hoặc password
        [TestMethod]
        public void TestLoginThieuUsername()
        {
            //Khai báo 2 biến email và password để nhận giá trị từ file dữ liệu
            string username = TestContext.DataRow[0].ToString();
            string password = TestContext.DataRow[1].ToString();

            //Gọi hàm Đăng nhập trên để bỏ giá trị từ file dữ liệu vào trang web
            DangNhap(username, password);

            // Đợi thông báo lỗi hiển thị
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var alert = wait.Until(ExpectedConditions.ElementIsVisible(
                By.CssSelector("div.alert.alert-danger")));

            // Lấy nội dung thông báo
            string alertText = alert.Text;

            // Kiểm tra nội dung có đúng thông báo lỗi không
            Assert.IsTrue(alertText.Contains("Không bỏ trống tên đăng nhập hoặc mật khẩu"));
        }

        //Khai báo DataSource, dẫn đường dẫn đến file dữ liệu
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"..\..\Data\DataTestThieuUsernamePassword.csv",
                    "DataTestThieuUsernamePassword#csv", DataAccessMethod.Sequential)]

        //TestCase4: Đăng nhập thất bại vì thiếu username và password
        [TestMethod]
        public void TestLoginThieuUsernamePassword()
        {
            //Khai báo 2 biến email và password để nhận giá trị từ file dữ liệu
            string username = TestContext.DataRow[0].ToString();
            string password = TestContext.DataRow[1].ToString();

            //Gọi hàm Đăng nhập trên để bỏ giá trị từ file dữ liệu vào trang web
            DangNhap(username, password);

            // Đợi thông báo lỗi hiển thị
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var alert = wait.Until(ExpectedConditions.ElementIsVisible(
                By.CssSelector("div.alert.alert-danger")));

            // Lấy nội dung thông báo
            string alertText = alert.Text;

            // Kiểm tra nội dung có đúng thông báo lỗi không
            Assert.IsTrue(alertText.Contains("Không bỏ trống tên đăng nhập và mật khẩu"));
        }
    }
}
