using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using WebScrapper.Models;
using WebScrapper.Services;

namespace WebScrapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebScrapperController : ControllerBase
    {

        private readonly WebScrapperService _service;

        public WebScrapperController (WebScrapperService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("offShore")]
        public ActionResult<DataScrapped> BuscaProveedorOffShoreLeaks(string nombreBusqueda)
        {
            if(string.IsNullOrEmpty(nombreBusqueda)) {
                return BadRequest(new { mensaje = "Se requiere un nombre para poder realizar la busqueda" });
            }
            DataScrapped dataScrapped = new DataScrapped();
            try
            {
                dataScrapped = _service.BuscaProveedorOffShoreLeaks(nombreBusqueda);
            }
            catch (Exception ex)
            {
                return Problem("Ocurrio un error interno", statusCode: 500);
            }
            return Ok(dataScrapped);
        }

        [HttpGet]
        [Route("worldBank")]
        public ActionResult<DataScrapped> BuscaProveedorWorldBank(string nombreBusqueda)
        {
            if (string.IsNullOrEmpty(nombreBusqueda))
            {
                return BadRequest(new { mensaje = "Se requiere un nombre para poder realizar la busqueda" });
            }
            DataScrapped dataScrapped = new DataScrapped();
            try
            {
                dataScrapped = _service.BuscaProveedorWorldBank(nombreBusqueda);
            }
            catch (Exception ex)
            {
                return Problem("Ocurrio un error interno", statusCode: 500);
            }
            return Ok(dataScrapped);
        }

        [HttpGet]
        [Route("ofac")]
        public ActionResult<DataScrapped> BuscaProveedorOfac(string nombreBusqueda)
        {
            if (string.IsNullOrEmpty(nombreBusqueda))
            {
                return BadRequest(new { mensaje = "Se requiere un nombre para poder realizar la busqueda" });
            }
            DataScrapped dataScrapped = new DataScrapped();
            try
            {
                dataScrapped = _service.BuscaProveedorOfac(nombreBusqueda);
            }
            catch (Exception ex)
            {
                return Problem("Ocurrio un error interno", statusCode: 500);
            }
            return Ok(dataScrapped);
        }
    }
}
