using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using LanchesMac.Areas.Admin.FastReportUtils;
using LanchesMac.Areas.Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class AdminLanchesReportController : Controller
    {
        private readonly IWebHostEnvironment webHostEnv;
        private readonly RelatorioLanchesService relatorioLanches;

        public AdminLanchesReportController(IWebHostEnvironment _webHostEnv, RelatorioLanchesService _relatorioLanches)
        {
            webHostEnv = _webHostEnv;
            relatorioLanches = _relatorioLanches;
        }

        public async Task<ActionResult> LanchesCategoriaReport()
        {
            var webReport = new WebReport();
            var mssqlDataConnection = new MsSqlDataConnection();

            webReport.Report.Dictionary.AddChild(mssqlDataConnection);
            webReport.Report.Load(Path.Combine(webHostEnv.ContentRootPath,"wwwroot/reports",
                                  "LanchesCategoria.frx"));

            var lanches = HelperFastReport.GetTable(await relatorioLanches.GetLanchesReport(),"LanchesReport");
            var categoria = HelperFastReport.GetTable(await relatorioLanches.GetCategoriaReport(), "CategoriaReport");

            webReport.Report.RegisterData(lanches, "LancheReport");
            webReport.Report.RegisterData(categoria, "CategoriaReport");
            return View(webReport);
        }

        public async Task<ActionResult> LanchesCategoriaPDF()
        {
            var webReport = new WebReport();
            var mssqlDataConnection = new MsSqlDataConnection();

            webReport.Report.Dictionary.AddChild(mssqlDataConnection);
            webReport.Report.Load(Path.Combine(webHostEnv.ContentRootPath, "wwwroot/reports",
                                  "LanchesCategoria.frx"));

            var lanches = HelperFastReport.GetTable(await relatorioLanches.GetLanchesReport(), "LanchesReport");
            var categoria = HelperFastReport.GetTable(await relatorioLanches.GetCategoriaReport(), "CategoriaReport");

            webReport.Report.RegisterData(lanches, "LancheReport");
            webReport.Report.RegisterData(categoria, "CategoriaReport");

            webReport.Report.Prepare();

            Stream stream = new MemoryStream();

            webReport.Report.Export(new PDFSimpleExport(), stream);
            stream.Position = 0;

            //return File(stream, "application/zip", "LancheCategoria.pdf");
            return new FileStreamResult(stream, "application/pdf");
        }
    }
}
