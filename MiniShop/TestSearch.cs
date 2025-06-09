using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace MiniShop
{
    [TestClass]
    public class TestSearch
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [TestInitialize]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        [TestMethod]
        public void TestSearchWithInput()
        {
            driver.Navigate().GoToUrl("http://localhost:3000/");

            // Tìm ô tìm kiếm bằng placeholder chứa từ 'tìm'
            var searchInput = wait.Until(driver =>
            {
                var inputs = driver.FindElements(By.TagName("input"));
                return inputs.FirstOrDefault(i =>
                    i.Displayed &&
                    i.Enabled &&
                    i.GetAttribute("placeholder") != null &&
                    i.GetAttribute("placeholder").ToLower().Contains("tìm"));
            });

            Assert.IsNotNull(searchInput, "Không tìm thấy ô tìm kiếm.");
            searchInput.Clear();
            searchInput.SendKeys("bút");

            // Tìm nút "Tìm" và click
            var searchButton = wait.Until(driver =>
            {
                var buttons = driver.FindElements(By.TagName("button"));
                return buttons.FirstOrDefault(b =>
                    b.Displayed &&
                    b.Enabled &&
                    b.Text.Trim().ToLower().Contains("tìm"));
            });

            Assert.IsNotNull(searchButton, "Không tìm thấy nút tìm.");
            searchButton.Click();

            // Kiểm tra tiêu đề kết quả chứa từ 'bút'
            var resultTitle = wait.Until(driver =>
                driver.FindElement(By.XPath("//h2[contains(text(), 'Từ khóa bạn đang muốn tìm kiếm')]")));

            Assert.IsTrue(resultTitle.Text.ToLower().Contains("bút"));

            // Kiểm tra danh sách sản phẩm có ít nhất 1 item
            var productItems = wait.Until(driver =>
                driver.FindElements(By.ClassName("product-item")));

            Assert.IsTrue(productItems.Count >= 1, "Không có sản phẩm nào được tìm thấy.");

            // Kiểm tra từng tên sản phẩm
            foreach (var item in productItems)
            {
                var nameElement = item.FindElement(By.ClassName("product-name"));
                Assert.IsTrue(nameElement.Text.ToLower().Contains("bút"));
            }
        }
    }
}
