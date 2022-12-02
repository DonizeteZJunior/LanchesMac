using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Areas.Admin.Services
{
    public class RelatorioLanchesService
    {
        private readonly AppDbContext context;

        public RelatorioLanchesService(AppDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<Lanche>> GetLanchesReport()
        {
            var lanches = await context.Lanches.ToListAsync();

            if (lanches is null)
                return default(IEnumerable<Lanche>);

            return lanches;
        }

        public async Task<IEnumerable<Categoria>> GetCategoriaReport()
        {
            var categorias = await context.Categorias.ToListAsync();

            if (categorias is null)
                return default(IEnumerable<Categoria>);

            return categorias;
        }
    }
}
