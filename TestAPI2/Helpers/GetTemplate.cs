using iText.StyledXmlParser.Jsoup;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPI2.Helpers
{
    public class GetTemplate
    {
        private IHostingEnvironment _hostingEnvironment;
        public GetTemplate(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        public string GetFeeReceiptTemplate()
        {
            
            var rootpath = _hostingEnvironment.ContentRootPath;
            var filepath = Path.GetFullPath(Path.Combine("Helpers", "provisional_receipt.html"));

            string template = File.ReadAllText(filepath);

            return template;
        }
    }
}
