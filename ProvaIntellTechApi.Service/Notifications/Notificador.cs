using ProvaIntellTechApi.Service.Notifications.Interfaces;

namespace ProvaIntellTechApi.Service.Notifications
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacoes;

        public Notificador()
        {
            _notificacoes = new List<Notificacao>();
        }

        public void Handle(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public bool TemNotificacao()
        {
            return _notificacoes.Any();
        }

        public string[] ObterMensagensNotificacoes()
        {
            return _notificacoes.Select(n => n.Mensagem).ToArray();
        }

        public bool ObtemNotificacao(string mensagem)
        {
            return _notificacoes.Exists(n => n.Mensagem == mensagem);
        }
    }
}
