namespace ProvaIntellTechApi.Service.Notifications.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
        string[] ObterMensagensNotificacoes();
        bool ObtemNotificacao(string mensagem);
    }
}
