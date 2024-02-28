using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using WebScrapper.Models;

namespace WebScrapper.Services
{
    public class WebScrapperService
    {
        public DataScrapped BuscaProveedorOffShoreLeaks(string nombreBusqueda)
        {
            DataScrapped dataScrapped = new DataScrapped();

            //logica para recuperar la lista de proveedores que hicieron un match correcto
            List<Proveedor> proveedores = new List<Proveedor>();
            var url = "https://offshoreleaks.icij.org/";

            ChromeOptions chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("--headless=new");
            chromeOptions.AddArgument("--ignore-certificate-errors");
            chromeOptions.AddArgument("--enable-features=NetworkService");

            using (IWebDriver driver = new ChromeDriver(chromeOptions))
            {
                driver.Navigate().GoToUrl(url);

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                try
                {
                    // VALIDACIÓN PARA EL PRIMER FILTRO AL ENTRAR A LA PÁGINA
                    IWebElement checkBox = wait.Until(driver => driver.FindElement(By.Id("accept")));
                    checkBox.Click();

                    IWebElement buttonSubmit = wait.Until(driver => driver.FindElement(By.CssSelector("button.btn.btn-primary.btn-block.btn-lg")));
                    buttonSubmit.Click();
                }
                catch (Exception ex)
                {
                    // El elemento no está presente, continuar sin hacer la validación
                }
                Thread.Sleep(1000);
                IWebElement input = wait.Until(driver => driver.FindElement(By.CssSelector("input[name='q'][placeholder='Search the full Offshore Leaks database']")));
                input.SendKeys(nombreBusqueda.Trim());

                IWebElement button = wait.Until(driver => driver.FindElement(By.CssSelector("button.btn.btn-primary.text-uppercase.px-3.px-md-5")));
                button.Click();

                Thread.Sleep(1000);

                // OBTENCION DE DATOS DE LA BUSQUEDA
                IWebElement tabla = wait.Until(driver => driver.FindElement(By.CssSelector(".table.table-sm.table-striped.search__results__table")));

                // Obtener las filas de la tabla
                var filas = tabla.FindElements(By.CssSelector("tbody tr"));

                int id = 1;
                // Iterar a través de las filas e imprimir los datos
                foreach (var fila in filas)
                {
                    // Obtener los datos de la fila
                    string entity = fila.FindElement(By.CssSelector("td a")).Text;
                    string jurisdiction = fila.FindElement(By.CssSelector("td.jurisdiction")).Text;
                    string linkedTo = fila.FindElement(By.CssSelector("td.country")).Text;
                    string dataFrom = fila.FindElement(By.CssSelector("td.source")).Text;

                    // Agregar datos a la lista de proveedores
                    proveedores.Add(new Proveedor
                    {
                        Id = id++,
                        Entity = entity,
                        Jurisdiction = jurisdiction,
                        LinkedTo = linkedTo,
                        DataFrom = dataFrom
                    });
                }
                dataScrapped.hits = filas.Count;
                dataScrapped.Proveedores = proveedores;
            }
            
            return dataScrapped;
        }

        public DataScrapped BuscaProveedorWorldBank(string nombreBusqueda)
        {
            DataScrapped dataScrapped = new DataScrapped();

            //logica para recuperar la lista de proveedores que hicieron un match correcto
            List<Proveedor> proveedores = new List<Proveedor>();
            var url = "https://projects.worldbank.org/en/projects-operations/procurement/debarred-firms";

            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");

            using (IWebDriver driver = new ChromeDriver(chromeOptions))
            {
                driver.Navigate().GoToUrl(url);
                Thread.Sleep(3000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement input = wait.Until(driver => driver.FindElement(By.Id("category")));
                input.SendKeys(nombreBusqueda.Trim());


                IWebElement divTableContainer = wait.Until(driver => driver.FindElement(By.CssSelector("div.k-grid-content")));
                IWebElement table = divTableContainer.FindElement(By.CssSelector("table[role='grid']"));

                //IWebElement table = wait.Until(driver => driver.FindElement(By.CssSelector("table[role='grid']")));
                IList<IWebElement> rows = table.FindElements(By.CssSelector("tbody tr"));

                int id = 1;

                foreach (IWebElement row in rows)
                {

                    // Obtener las celdas de la fila
                    IList<IWebElement> cells = row.FindElements(By.CssSelector("td"));

                    string firmName = cells[0].Text;
                    string address = cells[2].Text;
                    string country = cells[3].Text;
                    string fromDate = cells[4].Text;
                    string toDate = cells[5].Text;
                    string grounds = cells[6].Text;

                    // Agregar datos a la lista de proveedores
                    proveedores.Add(new Proveedor
                    {
                        Id = id++,
                        FirmName = firmName,
                        Address = address,
                        Country = country,
                        FromDate = fromDate,
                        ToDate = toDate,
                        Grounds = grounds
                    });

                }
                dataScrapped.hits = rows.Count;
                dataScrapped.Proveedores = proveedores;
            }
            return dataScrapped;
        }


        public DataScrapped BuscaProveedorOfac(string nombreBusqueda)
        {
            DataScrapped dataScrapped = new DataScrapped();

            //logica para recuperar la lista de proveedores que hicieron un match correcto
            List<Proveedor> proveedores = new List<Proveedor>();
            var url = "https://sanctionssearch.ofac.treas.gov/";

            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");

            using (IWebDriver driver = new ChromeDriver(chromeOptions))
            {
                driver.Navigate().GoToUrl(url);
                //Thread.Sleep(3000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement input = wait.Until(driver => driver.FindElement(By.Id("ctl00_MainContent_txtLastName")));
                input.SendKeys(nombreBusqueda.Trim());

                IWebElement button = wait.Until(driver => driver.FindElement(By.Id("ctl00_MainContent_btnSearch")));
                button.Click();

                Thread.Sleep(1000);
                //IWebElement divTableContainer = wait.Until(driver => driver.FindElement(By.CssSelector("div.k-grid-content")));
                IWebElement table = wait.Until(driver => driver.FindElement(By.Id("gvSearchResults")));

                IList<IWebElement> rows = table.FindElements(By.CssSelector("tbody tr"));

                int id = 1;
                foreach (IWebElement row in rows)
                {

                    // Obtener las celdas de la fila
                    IList<IWebElement> cells = row.FindElements(By.CssSelector("td"));

                    string name = cells[0].Text;
                    string address = cells[1].Text;
                    string type = cells[2].Text;
                    string programs = cells[3].Text;
                    string list = cells[4].Text;
                    string score = cells[5].Text;

                    // Agregar datos a la lista de proveedores
                    proveedores.Add(new Proveedor
                    {
                        Id = id++,
                        Name = name,
                        Address = address,
                        Type = type,
                        Programs = programs,
                        List = list,
                        Score = score
                    });
                }
                dataScrapped.hits = rows.Count;
                dataScrapped.Proveedores = proveedores;

            }
            return dataScrapped;
        }
    }
}
