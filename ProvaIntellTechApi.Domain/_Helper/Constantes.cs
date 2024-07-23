namespace ProvaIntellTechApi.Domain._Helper
{
    public static class Constantes
    {
        public const string AtividadeNaoEncontradaErrorMsg = "Atividade Não Encontrada";
        public const string AtividadeRemovidaMsg = "Atividade Removida";
        public const int Numero128 = 128;
        public const int Numero129 = 129;
        public const int Numero512 = 512;
        public const int Numero513 = 513;
        public static string RetornandoAtividadesMsg(int quantidade) => $"Retornando '{quantidade}' Atividades";
        public static string CampoVazioErrorMsg(string nomeCampo) => $"O campo '{nomeCampo}' não deve estar vazio";
        public static string CampoMaiorErrorMsg(string nomeCampo, int tamanhoMaximo) => $"O campo '{nomeCampo}' não deve passar de {tamanhoMaximo} caracteres";

    }
}
