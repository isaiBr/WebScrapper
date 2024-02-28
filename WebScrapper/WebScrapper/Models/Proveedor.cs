using System.Xml.Linq;

namespace WebScrapper.Models
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Entity { get; set; }
        public string Jurisdiction { get; set; }
        public string LinkedTo { get; set; }
        public string DataFrom { get; set; }
        public string FirmName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Grounds { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Programs { get; set; }
        public string List { get; set; }
        public string Score { get; set; }
    }
}