using LanchesMac.Models;

namespace LanchesMac.ViewsModels
{
    public class PedidoLancheViewModel
    {
        public Pedido Pedido { get; set; }
        public IEnumerable<PedidoDetalhe> PedidoDetalhes { get; set; }  
    }
}
