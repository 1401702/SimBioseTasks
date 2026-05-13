using System.Collections.Generic;

namespace SimBioseTasks
{
    /// <summary>
    /// Define o contrato responsável pelas operações de leitura de tarefas.
    /// </summary>
    /// <remarks>
    /// Esta interface separa as operações de consulta das operações de escrita,
    /// seguindo o princípio de segregação de interfaces.
    /// </remarks>
    public interface ITaskReader
    {
        /// <summary>
        /// Obtém todas as tarefas atualmente disponíveis.
        /// </summary>
        /// <returns>
        /// Uma coleção apenas de leitura contendo todas as tarefas existentes.
        /// </returns>
        IReadOnlyList<BaseTask> GetAll();

        /// <summary>
        /// Obtém uma tarefa a partir do seu identificador único.
        /// </summary>
        /// <param name="id">Identificador da tarefa a procurar.</param>
        /// <returns>
        /// A tarefa correspondente ao identificador indicado, ou <c>null</c> se não existir.
        /// </returns>
        BaseTask? GetById(int id);
    }
}