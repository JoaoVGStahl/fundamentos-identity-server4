namespace SkyCommerce.Models
{
    public enum StatusPedido
    {
        Processando = 1,
        SeparandoParaEnvio = 3,
        Enviado = 4,
        Finalizado = 5,
        Cancelado = 6,
        PagamentoNegado = 7,
        AguardandoConfirmacao = 8
    }
}