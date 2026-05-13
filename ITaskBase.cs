namespace SimBioseTasks
{
    /// <summary>
    /// Define o contrato base do model da aplicação.
    /// Agrega operações de leitura, escrita, notificação e inicialização.
    /// </summary>
    public interface ITaskBase : ITaskReader, ITaskWriter, ITaskNotifier
    {
        /// <summary>
        /// Inicializa o model, carregando o estado persistido.
        /// </summary>
        void Initialize();
    }
}