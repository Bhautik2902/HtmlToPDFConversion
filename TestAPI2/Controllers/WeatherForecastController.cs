using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TestAPI2.Helpers;

namespace TestAPI2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public FileResult Get()
        {
            try
            {
                // get HTML template as string
                GetTemplate gt = new GetTemplate(_hostingEnvironment);
                string htmlcontent = gt.GetFeeReceiptTemplate();

                SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
                SelectPdf.PdfDocument doc = converter.ConvertHtmlString(htmlcontent);
                var pdfbytes = doc.Save();
                doc.Close();

                return File(pdfbytes, System.Net.Mime.MediaTypeNames.Application.Pdf, "Output.pdf");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }


            // only working for html
            /*htmlcontent = htmlcontent.Replace("\r\n", "");
            htmlcontent = htmlcontent.Replace("\0", "");

            using (MemoryStream ms = new MemoryStream())
            {
                using (Document doc = new Document(PageSize.A4, 10f, 10f, 10f, 0f))
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    doc.Open();
                    using (StringReader sr = new StringReader(htmlcontent))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
                    }

                    doc.Close();
                    byte[] bytes = ms.ToArray();
                    ms.Close();

                    Response.ContentType = "application/pdf";

                    Response.Headers.Add("content-disposition", "attachment;filename=sample.pdf");


                    return File(bytes, System.Net.Mime.MediaTypeNames.Application.Pdf, "Output.pdf");
                }
            }*/



            // working for html file without CSS.
            /*
            StringReader sr = new StringReader(gt.GetFeeReceiptTemplate());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                htmlparser.Parse(sr);
                pdfDoc.Close();

                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                return File(bytes, System.Net.Mime.MediaTypeNames.Application.Pdf, "Output.pdf");
            }*/

        }



    }
}
