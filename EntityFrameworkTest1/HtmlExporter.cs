using DbCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkTest1
{
    public class HtmlExporter
    {
        private static void DataToHtmlTable(String tableTitle, ICollection<IExportable> dataToExport, StringBuilder stringBuilder)
        {
            // title
            stringBuilder.Append("<h2>")
                .Append(tableTitle)
                .Append("</h2>");

            // table header
            stringBuilder.Append("<table>")
                .Append("<thead>")
                .Append("<tr>");
            if (dataToExport.Count() > 0)
            {
                foreach(String fieldName in dataToExport.ElementAt(0).GetFieldNames())
                {
                    stringBuilder.Append("<th>")
                        .Append(fieldName)
                        .Append("</th>");
                }
            }
            stringBuilder.Append("</tr>");
            stringBuilder.Append("</thead>");

            // table data
            stringBuilder.Append("<tbody>");
            if (dataToExport.Count() > 0)
            {
                foreach(IExportable exportable in dataToExport)
                {
                    stringBuilder.Append("<tr>");
                    string[] fieldNames = exportable.GetFieldNames();
                    foreach(string fName in fieldNames)
                    {
                        if (exportable.GetFieldValues().ContainsKey(fName))
                        {
                            stringBuilder.Append("<td>")
                                .Append(exportable.GetFieldValues()[fName])
                                .Append("</td>");
                        }
                    }
                    stringBuilder.Append("</tr>");
                }
            }
            stringBuilder.Append("</tbody>");

            // end table
            stringBuilder.Append("</table>");
        }

        public static String ExportToHtml(Dictionary<String, ICollection<IExportable>> dataToExport, String pageHeader)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<html>")
                .Append("<head>")
                .Append("<title>").Append(pageHeader).Append("</title>")
                .Append("</head>")
                .Append("<body>")
                .Append("<h1>").Append(pageHeader).Append("</h1>");

            foreach(string tableTitle in dataToExport.Keys)
            {
                DataToHtmlTable(tableTitle, dataToExport[tableTitle], sb);
            }

            sb.Append("</body>")
                .Append("</html>");

            return sb.ToString();
        }
    }
}
